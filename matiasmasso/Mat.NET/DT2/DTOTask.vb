Public Class DTOTask
    Inherits DTOBaseGuid
    Property Cod As Cods
    Property Nom As String
    Property Dsc As String
    Property Enabled As Boolean
    Property LastLog As DTOTaskLog
    Property NotBefore As DateTimeOffset
    Property NotAfter As DateTimeOffset
    Property Schedules As List(Of DTOTaskSchedule)
    Property Exceptions As List(Of DTOTaskResult.Exception)

    Public Enum Cods
        NotSet
        VivaceTransmisio
        VtosUpdate
        EmailStocks
        WebAtlasUpdate
        Avisame
        CaducaCredits
        PropersVencimentsClients
        EdiReadFromInbox
        EdiWriteToOutbox
        EdiProcessaInbox
        SorteoSetWinners
        NotifyVtos
        ImportWordPressCommentEmails
        CurrencyExchangeRates
        AmazonInvRpt
        ArcPmcs
        RequestForSupplierPurchaseOrder
        StoreLocatorExcelMailing
        SiiEmeses
        SiiRebudes
        EmailDescatalogats
        BankTransferReminder
    End Enum

    Public Enum ResultCods
        Running
        Success
        Empty
        DoneWithErrors
        Failed
    End Enum

    Shared Function Wellknown(oCod As Cods) As DTOTask
        Dim retval As DTOTask = Nothing
        Select Case oCod
            Case Cods.VivaceTransmisio
                retval = New DTOTask(New Guid("7CAC561F-37FB-4F42-B933-5B7F47B90F4F"))
                retval.cod = oCod
            Case Cods.EdiReadFromInbox
                retval = New DTOTask(New Guid("B56A4B69-B2A0-49C1-A415-6CE8594522C2"))
                retval.cod = oCod
            Case Cods.EdiWriteToOutbox
                retval = New DTOTask(New Guid("879AFDA9-6346-485B-A9A2-83C9C4211C72"))
                retval.cod = oCod
            Case Cods.NotifyVtos
                retval = New DTOTask(New Guid("7F9329FC-30D7-43A5-ACF3-F6A5EFABF6F2"))
                retval.cod = oCod
            Case Cods.BankTransferReminder
                retval = New DTOTask(New Guid("6f16549b-f4fa-45a6-bfc9-035a215ae6c6"))
        End Select
        Return retval
    End Function

    Public Sub New()
        MyBase.New()
        _Schedules = New List(Of DTOTaskSchedule)
        _Exceptions = New List(Of DTOTaskResult.Exception)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _Schedules = New List(Of DTOTaskSchedule)
        _Exceptions = New List(Of DTOTaskResult.Exception)
    End Sub

    Public Function IsDue() As Boolean
        'If _Cod = 9 Then Stop '===================================================================
        Dim retval As Boolean = False
        If _Enabled Then
            If _LastLog Is Nothing OrElse _LastLog.ResultCod <> ResultCods.Running Then
                Dim iMinutesToRun As Integer = TimeToNextRun().TotalMinutes
                retval = (iMinutesToRun <= 0)
            End If
        End If
        Return retval
    End Function

    Public Function TimeToNextRun() As TimeSpan
        Dim oNextRun = NextRun()
        Dim oRetVal As TimeSpan = oNextRun - DateTimeOffset.Now
        Return oRetVal
    End Function

    Public Function TimeToNextRun(oSchedule As DTOTaskSchedule) As TimeSpan
        Dim dtNextRun As DateTimeOffset = NextRun(oSchedule)
        Dim dtNow As DateTimeOffset = DateTimeOffset.Now
        If dtNow > dtNextRun Then dtNextRun = dtNow
        Dim oRetVal As TimeSpan = dtNextRun - dtNow
        Return oRetVal
    End Function

    Public Function NextRun() As DateTimeOffset
        Dim retval As DateTimeOffset
        Dim oEnabledSchedules As List(Of DTOTaskSchedule) = _Schedules.Where(Function(x) x.Enabled).ToList
        If oEnabledSchedules.Count > 0 Then
            retval = oEnabledSchedules.Min(Function(x) NextRun(x))

            If _NotAfter <> Nothing Then
                If retval > _NotAfter Then
                    retval = Nothing
                End If
            End If
        End If
        Return retval
    End Function

    Public Function NextRun(oSchedule As DTOTaskSchedule) As DateTimeOffset
        Dim retval As DateTimeOffset

        Dim DtFch As DateTimeOffset = NextAllowedFch(oSchedule)

        Select Case oSchedule.Mode
            Case DTOTaskSchedule.Modes.Interval
                If _LastLog IsNot Nothing Then
                    Dim blSwitch As Boolean = (_LastLog.Fch > DtFch)
                    If blSwitch Then
                        DtFch = _LastLog.Fch
                    End If
                End If
                If DtFch = DateTimeOffset.MinValue Then
                    retval = DateTimeOffset.Now
                Else
                    retval = DtFch.AddMinutes(oSchedule.timeInterval.TotalMinutes)
                    If retval < DateTimeOffset.Now Then retval = DateTimeOffset.Now
                End If
            Case Else
                Dim oTimeZoneBcn = TimeZoneInfo.FindSystemTimeZoneById("Romance Standard Time")
                Dim targetFch As New DateTimeOffset(DtFch.Year, DtFch.Month, DtFch.Day, oSchedule.timeInterval.Hours, oSchedule.timeInterval.Minutes, 0, oTimeZoneBcn.GetUtcOffset(DtFch))
                retval = targetFch 'New DateTimeOffset(targetFch, oTimeZoneBcn.GetUtcOffset(DtFch))
        End Select

        Return retval
    End Function


    Public Function NextAllowedFch(oSchedule As DTOTaskSchedule) As DateTimeOffset
        Dim DtLastRun As DateTimeOffset
        If _LastLog IsNot Nothing Then
            DtLastRun = _LastLog.Fch
        End If

        Dim retval As DateTimeOffset = DateTime.Today
        If _NotBefore > retval Then retval = _NotBefore

        For i As Integer = 0 To 6
            If oSchedule.WeekDays(retval.DayOfWeek) Then
                Exit For
            Else
                retval = retval.AddDays(1)
            End If
        Next i

        Return retval
    End Function

    Public Sub SetResult(exs As List(Of Exception))
        With _lastLog
            If exs.Count > 0 Then
                Dim sb As New Text.StringBuilder
                For Each ex In exs
                    If ex.Message > "" Then
                        Dim ex2 As New DTOTaskResult.Exception(ex.Message)
                        _exceptions.Add(ex2)
                        sb.AppendLine(ex.Message)
                    End If
                Next
                .resultMsg += sb.ToString 'evita maxacar-ho si l'hem assignat durant la tasca
            End If
            If .resultCod = DTOTask.ResultCods.running Then 'evita maxacar-ho si l'hem assignat durant la tasca
                .resultCod = If(exs.Count = 0, ResultCods.success, ResultCods.failed)
            End If
            .fch = DateTimeOffset.Now
        End With

    End Sub

    Public Sub SetResult(oTaskResult As DTOTaskResult)
        With _LastLog
            .ResultMsg = oTaskResult.Msg
            .ResultCod = oTaskResult.ResultCod
            .Fch = DateTimeOffset.Now
        End With

        _Exceptions.AddRange(oTaskResult.Exceptions)
    End Sub

    Shared Function Report(oTasks As List(Of DTOTask)) As String

        Dim sb As New Text.StringBuilder
        If oTasks.Count = 0 Then
            sb.AppendLine("No hi ha cap tasca per executar")
        Else
            Dim failedTasks = oTasks.Where(Function(x) x.LastLog.ResultCod = DTOTask.ResultCods.Failed Or x.LastLog.ResultCod = DTOTask.ResultCods.DoneWithErrors)
            If failedTasks.Count = 0 Then
                sb.AppendLine(String.Format("Executades {0} tasques correctament:", oTasks.Count))
                For Each oTask In oTasks
                    sb.AppendLine(oTask.Cod.ToString & ": " & oTask.LastLog.ResultMsg)
                Next
            Else
                sb.AppendLine(String.Format("Error al executar {0} de {1} tasques:", failedTasks.Count, oTasks.Count))
                For Each oTask In oTasks
                    sb.AppendLine(oTask.Cod.ToString & ": " & oTask.LastLog.ResultCod.ToString & " - " & oTask.LastLog.ResultMsg)
                    For Each ex In oTask.Exceptions
                        sb.AppendLine("    " & ex.Message)
                    Next
                Next
            End If
        End If

        Return sb.ToString
    End Function
End Class

