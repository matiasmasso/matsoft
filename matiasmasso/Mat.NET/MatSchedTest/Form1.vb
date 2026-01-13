Public Class Form1
    Private _User As DTOUser

    Public Sub New()
        MyBase.New
        InitializeComponent()
        'FEB.App.Initialize("https://matiasmasso-api.azurewebsites.net", 55836, useLocalApi:=True)
        FEB.App.Initialize("https://api.matiasmasso.es", 55836, useLocalApi:=True)
    End Sub

    Shared Function User() As DTOUser
        Return DTOUser.Wellknown(DTOUser.Wellknowns.info)
    End Function


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        OnTimerElapsed()
    End Sub

    Private Sub OnTimerElapsed()
        Dim oTask As DTOTask = Nothing
        Dim exs As New List(Of Exception)

        Try

            'executa les tasques remotes
            Dim oDueTasks = FEB.Tasks.ExecuteSync(User(), exs)
            If exs.Count = 0 Then
                log(oDueTasks.Count & " tasks found")

                'executa les tasques locals (EDI)
                'For Each oTask In oDueTasks
                '    Select Case oTask.Cod
                '        Case DTOTask.Cods.EdiWriteToOutbox
                '            log("executing WriteToOutbox")
                '            If FEB.EdiversaFileSystem.Execute(oTask, exs) Then
                '                log("executed WriteToOutbox")
                '                FEB.Task.Load(oTask, exs)
                '            End If
                '        Case DTOTask.Cods.EdiReadFromInbox
                '            log("executing EdiReadFromInbox")
                '            If FEB.EdiversaFileSystem.Execute(oTask, exs) Then
                '                log("executed EdiReadFromInbox")
                '                If FEB.EdiversaFileSystem.ProcessaInboxSync(User(), exs) Then
                '                    log("processed Inbox")
                '                Else
                '                    log("error on processing Inbox")
                '                    log(exs.First.Message)
                '                    LogEvent("error al processar inbox" & vbCrLf & exs.First.Message, EventLogEntryType.Error)
                '                End If
                '            End If
                '    End Select
                'Next

                'reporta els resultats si n'hi han
                If oDueTasks.Any(Function(x) x.LastLog.ResultCod <> DTOTask.ResultCods.Empty) Then
                    If oDueTasks.Any(Function(x) x.LastLog.ResultCod = DTOTask.ResultCods.Failed Or x.LastLog.ResultCod = DTOTask.ResultCods.DoneWithErrors) Then
                        LogEvent(DTOTask.Report(oDueTasks), EventLogEntryType.Error)
                    ElseIf Not oDueTasks.All(Function(x) x.LastLog.ResultCod = DTOTask.ResultCods.Empty) Then
                        LogEvent(DTOTask.Report(oDueTasks), EventLogEntryType.Information)
                    End If
                End If
            Else
            End If


        Catch ex As Exception
            Dim sb As New Text.StringBuilder
            sb.AppendLine("Error a MatSchedService.OnTimeElapsed")
            If oTask IsNot Nothing Then
                sb.AppendLine("Task " & oTask.Cod.ToString())
            End If
            If _User IsNot Nothing Then
                sb.AppendLine("User " & _User.Guid.ToString())
            End If
            sb.AppendLine(ex.Message)
            sb.AppendLine(ex.StackTrace)
            LogEvent(sb.ToString, EventLogEntryType.Error)
        End Try

        For Each ex In exs
            LogEvent(ex.Message, EventLogEntryType.Error)
        Next
    End Sub

    Private Sub log(src As String)
        TextBoxLog.Text = TextBoxLog.Text & src & vbCrLf
        Application.DoEvents()
    End Sub

    Private Sub LogEvent(oTaskResult As DTOTaskResult)
        Select Case oTaskResult.ResultCod
            Case DTOTask.ResultCods.Success
                LogEvent(oTaskResult.ResultReport, EventLogEntryType.Information)
            Case DTOTask.ResultCods.DoneWithErrors
                LogEvent(oTaskResult.ResultReport, EventLogEntryType.Warning)
            Case DTOTask.ResultCods.Failed
                LogEvent(oTaskResult.ResultReport, EventLogEntryType.Error)
        End Select
    End Sub


    Shared Sub LogEvent(msg As String, entryType As EventLogEntryType)
        Try
            System.Diagnostics.EventLog.WriteEntry("MatScheD", msg, entryType)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ButtonImportaInbox_Click(sender As Object, e As EventArgs) Handles ButtonImportaInbox.Click
        Dim exs As New List(Of Exception)
        Dim oTask = DTOTask.Wellknown(DTOTask.Cods.EdiReadFromInbox)
        ProgressBar1.Visible = True
        If FEB.EdiversaFileSystem.Execute(oTask, exs) Then
            If Not FEB.EdiversaFileSystem.ProcessaInboxSync(User(), exs) Then
                LogEvent("error al processar inbox" & vbCrLf & exs.First.Message, EventLogEntryType.Error)
            End If
        End If
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            MsgBox("Safata d'entrada importada satisfactoriament", MsgBoxStyle.Information)
        Else
            Dim sb As New Text.StringBuilder
            For Each ex In exs
                sb.AppendLine(ex.Message)
            Next
            MsgBox(sb.ToString, MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim exs As New List(Of Exception)
        Dim oTask = DTOTask.Wellknown(DTOTask.Cods.EdiWriteToOutbox)
        log("executing WriteToOutbox")
        If FEB.EdiversaFileSystem.Execute(oTask, exs) Then
            log("executed WriteToOutbox")
            FEB.Task.Load(oTask, exs)
        End If

    End Sub

    Private Sub ButtonTest_Click(sender As Object, e As EventArgs) Handles ButtonTest.Click
        Dim oSheet As New MatHelper.Excel.Sheet()
    End Sub
End Class
