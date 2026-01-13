Public Class ServiceHelper

    Shared Function User() As DTOUser
        Return DTOUser.Wellknown(DTOUser.Wellknowns.info)
    End Function

    Shared Sub InitApi(useLocalApi As Boolean)
        'FEB.App.Initialize("https://matiasmasso-api.azurewebsites.net", 55836, useLocalApi)
        FEB.App.Initialize("https://api.matiasmasso.es", 55836, useLocalApi)
    End Sub

    Shared Sub TimeElapsed()
        Dim exs As New List(Of Exception)
        Dim oTask As DTOTask = Nothing
        Dim oUser = User()

        Try

            'executa les tasques remotes
            Dim oDueTasks = FEB.Tasks.ExecuteSync(oUser, exs)
            If exs.Count = 0 Then
                'executa les tasques locals (EDI)
                'For Each oTask In oDueTasks
                '    Select Case oTask.Cod
                '        Case DTOTask.Cods.EdiWriteToOutbox
                '            If FEB.EdiversaFileSystem.Execute(oTask, exs) Then
                '                FEB.Task.Load(oTask, exs)
                '            End If
                '        Case DTOTask.Cods.EdiReadFromInbox
                '            If FEB.EdiversaFileSystem.Execute(oTask, exs) Then
                '                If Not FEB.EdiversaFileSystem.ProcessaInboxSync(oUser, exs) Then
                '                    LogEvent("error al processar inbox" & vbCrLf & exs.First.Message, EventLogEntryType.Error)
                '                End If
                '            End If
                '    End Select
                'Next

                'reporta els resultats si n'hi han
                If oDueTasks.Any(Function(x) x.lastLog.resultCod <> DTOTask.ResultCods.empty) Then
                    If oDueTasks.Any(Function(x) x.lastLog.resultCod = DTOTask.ResultCods.failed Or x.lastLog.resultCod = DTOTask.ResultCods.doneWithErrors) Then
                        LogEvent(DTOTask.Report(oDueTasks), EventLogEntryType.Error)
                    ElseIf Not oDueTasks.All(Function(x) x.lastLog.resultCod = DTOTask.ResultCods.empty) Then
                        LogEvent(DTOTask.Report(oDueTasks), EventLogEntryType.Information)
                    End If
                End If
            Else
                LogError(exs, oTask)
            End If

        Catch ex As Exception
            LogError(ex, oTask)
        End Try

    End Sub

    Shared Sub LogError(ex As Exception, oTask As DTOTask)
        Dim exs As New List(Of Exception)
        exs.Add(ex)
        LogError(exs, oTask)
    End Sub

    Shared Sub LogError(exs As List(Of Exception), oTask As DTOTask)
        For Each ex In exs
            Dim sb As New Text.StringBuilder
            sb.AppendLine("Error a MatSchedService.OnTimeElapsed")
            If oTask IsNot Nothing Then
                sb.AppendLine("Task " & oTask.cod.ToString())
            End If
            'If oUser IsNot Nothing Then
            'sb.AppendLine("User " & oUser.Guid.ToString())
            'End If
            sb.AppendLine(ex.Message)
            sb.AppendLine(ex.StackTrace)
            LogEvent(sb.ToString, EventLogEntryType.Error)

            LogEvent(ex.Message, EventLogEntryType.Error)
        Next
    End Sub

    Shared Sub LogEvent(msg As String, entryType As EventLogEntryType)
        Try
            System.Diagnostics.EventLog.WriteEntry("MatSched", msg, entryType)
        Catch ex As Exception

        End Try
    End Sub


End Class
