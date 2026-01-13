Public Class Task 'BEBL

#Region "CRUD"
    Shared Function Find(oGuid As Guid) As DTOTask
        Dim retval As DTOTask = TaskLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Find(oCod As DTOTask.Cods) As DTOTask
        Dim retval As DTOTask = TaskLoader.Find(oCod)
        Return retval
    End Function

    Shared Function Update(oTask As DTOTask, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = TaskLoader.Update(oTask, exs)
        Return retval
    End Function

    Shared Function Delete(oTask As DTOTask, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = TaskLoader.Delete(oTask, exs)
        Return retval
    End Function
#End Region

    Shared Async Function Execute(exs As List(Of Exception), oTask As DTOTask, oUser As DTOUser) As Task(Of DTOTask)
        'marquem la tasca abans de fer-la en lloc de després
        'per evitar que tasques que duren mes de un minut es disparin a cada interval

        Select Case oTask.Cod
            Case DTOTask.Cods.Tamariu
                Await BEBL.Tamariu.CheckPort()
            Case DTOTask.Cods.EdiReadFromInbox, DTOTask.Cods.EdiWriteToOutbox
                'gestionat des del windows service al servidor.
                'Altres per implementar:
            Case DTOTask.Cods.Avisame
            Case DTOTask.Cods.PropersVencimentsClients
            Case DTOTask.Cods.RequestForSupplierPurchaseOrder
            Case Else
                oTask.LastLog = DTOTaskLog.Factory()
                If Log(oTask, exs) Then
                    Select Case oTask.Cod
                        Case DTOTask.Cods.VivaceTransmisio
                            BEBL.Transmisio.Send(oUser.Emp, exs)
                        Case DTOTask.Cods.VtosUpdate
                            BEBL.Csbs.SaveVtos(oUser, exs)
                        Case DTOTask.Cods.EmailStocks
                            BEBL.Mailings.SendStocks(oUser.Emp, exs)
                        Case DTOTask.Cods.WebAtlasUpdate
                            BEBL.WebAtlas.Update(oUser.Emp, exs)
                        Case DTOTask.Cods.CaducaCredits
                            Await BEBL.CliCreditLogs.CaducaCredits(oUser, exs)
                        Case DTOTask.Cods.EdiProcessaInbox
                            BEBL.Ediversa.ProcessaInbox(oUser, exs)
                        Case DTOTask.Cods.SorteoSetWinners
                            BEBL.Raffles.SetWinners(exs, oTask)
                        Case DTOTask.Cods.NotifyVtos
                            BEBL.Csbs.NotifyVtos(exs, oUser.Emp, oTask)
                        Case DTOTask.Cods.CurrencyExchangeRates
                            BEBL.CurExchangeRates.UpdateRates(exs)
                        Case DTOTask.Cods.AmazonInvRpt
                            BEBL.Amazon.SendInvRpt(oUser.Emp, exs)
                        Case DTOTask.Cods.ArcPmcs
                            BEBL.Mgz.SetPrecioMedioCoste(oUser.Emp.Mgz, exs)
                        Case DTOTask.Cods.StoreLocatorExcelMailing
                            Await BEBL.ProductDistributors.StoreLocatorExcelMailing(oUser.Emp, exs)
                        Case DTOTask.Cods.SiiEmeses
                            BEBL.Sii.SendEmeses(oUser.Emp, exs)
                        Case DTOTask.Cods.SiiRebudes
                            BEBL.Sii.SendRebudes(oUser.Emp, exs)
                        Case DTOTask.Cods.EmailDescatalogats
                            Await BEBL.Mailings.Descatalogats(oUser.Emp, exs)
                        Case DTOTask.Cods.BankTransferReminder
                            Await BEBL.Mailings.BankTransferReminder(oUser.Emp, exs)
                        Case DTOTask.Cods.ElCorteInglesAlineacionDisponibilidad
                            oTask.LastLog = BEBL.Integracions.ElCorteIngles.AlineamientoDeDisponibilidad.Procesa(oUser.Emp, oTask.LastLog)
                        Case DTOTask.Cods.MarketPlacesSync
                            If oUser.Emp.Mgz Is Nothing Then BEBL.Emp.Load(oUser.Emp)
                            oTask.LastLog = Await BEBL.MarketPlaces.SyncTask(oTask.LastLog, oUser.Emp)
                        Case DTOTask.Cods.Web2Sync
                            oTask.LastLog = BEBL.DirtyTables.KeepAlive(oTask.LastLog)

                    End Select

                    oTask.SetResult(exs)
                    TaskLog.Update(oTask, exs)
                Else
                    exs.Add(New Exception("Error al registrar la tasca al log"))
                    oTask.SetResult(exs)
                End If

        End Select

        Return oTask
    End Function


    Shared Function Log(ByRef oTask As DTOTask, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        'Dim oJsonSerializerSettings = New Newtonsoft.Json.JsonSerializerSettings With {.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore}
        Try
            'Indispensable perque no ho faci mes de un cop al dia
            Dim oTaskSchedules As List(Of DTOTaskSchedule) = oTask.Schedules
            If oTaskSchedules.Count > 0 Then
                Dim oTaskSchedule As DTOTaskSchedule = oTaskSchedules.FirstOrDefault(Function(x) x.Enabled And x.Mode = DTOTaskSchedule.Modes.GivenTime)
                If oTaskSchedule IsNot Nothing Then
                    Dim DtNextFch As New Date(DTO.GlobalVariables.Today().AddDays(1).Year, DTO.GlobalVariables.Today().AddDays(1).Month, DTO.GlobalVariables.Today().AddDays(1).Day)
                    oTask.NotBefore = DtNextFch
                End If
            End If

            'al principi de executar la tasca per evitar que es torni a disparar al cap de un minut
            retval = TaskLoader.Log(oTask, exs)

        Catch ex As Exception
            exs.Add(New Exception("BEBL.Task.Log: Error al loggejar la tasca"))
            exs.Add(ex)
        End Try

        Return retval
    End Function


End Class

Public Class Tasks
    Shared Async Function Execute(oUser As DTOUser, exs As List(Of Exception)) As Task(Of List(Of DTOTask))
        Dim retval = BEBL.Tasks.DueTasks

        'Update server cache if needed
        Dim oServerCache = BEBL.ServerCache.ForEmp(DTOEmp.Ids.MatiasMasso)
        BEBL.ServerCache.CheckForServerUpdates(oServerCache)

        'execute pending tasks
        For Each oTask In DueTasks()
            'LogEvent("executing task " & oTask.Cod.ToString, EventLogEntryType.Information)
            oTask = Await BEBL.Task.Execute(exs, oTask, oUser)
        Next

        Return retval
    End Function

    Shared Function All() As List(Of DTOTask)
        Dim retval = TasksLoader.All()
        Return retval
    End Function

    Shared Function DueTasks() As List(Of DTOTask)
        Dim oAllTasks = BEBL.Tasks.All()
        Dim retval = oAllTasks.Where(Function(x) x.IsDue).ToList
        'LogEvent(retval.Count & " due tasks from " & oAllTasks.Count, EventLogEntryType.Information)
        Return retval
    End Function

    Shared Sub LogEvent(msg As String, entryType As EventLogEntryType)
        Try
            System.Diagnostics.EventLog.WriteEntry("MatScheD", msg, entryType)
        Catch ex As Exception

        End Try
    End Sub
End Class