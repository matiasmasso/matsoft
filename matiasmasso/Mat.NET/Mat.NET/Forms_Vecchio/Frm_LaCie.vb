Public Class Frm_LaCie

    Public Function SendFtp(ByRef exs as List(Of exception)) As EventLogEntryType
        Dim retVal As EventLogEntryType

        'Dim sPath As String = "\\SQL\BACKUP\maxisrvr.BAK"

        Dim oReceivePort As New maxisrvr.MatPort(maxisrvr.MatPort.Ids.Backup_SQL)


        For Each sFile As String In oReceivePort.GetFiles
            Dim oSendPort As New maxisrvr.MatPort(maxisrvr.MatPort.Ids.BackUp_LaCie)
            RetVal = oSendPort.Send(sFile, exs)
        Next


        Return RetVal
    End Function

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim exs as new list(Of Exception)
        Dim retVal As EventLogEntryType = SendFtp( exs)
        MsgBox( BLL.Defaults.ExsToMultiline(exs), IIf(retVal = EventLogEntryType.Information, MsgBoxStyle.Information, MsgBoxStyle.Exclamation))
    End Sub
End Class