Public Class Exceptions


    Shared Sub LogError(sMessage As String, exs As List(Of Exception))
        Dim sb As New Text.StringBuilder
        sb.AppendLine(sMessage)
        sb.AppendLine(ExceptionsHelper.ToFlatString(exs))
        Try
            'EventLog.WriteEntry(DTOApp.Current.Type.ToString, sb.ToString, EventLogEntryType.Error)
        Catch ex As Exception
            sb.AppendLine("(this event should have been logged on '" & DTOApp.Current.Id.ToString & "' Eventlog Source, but since it does not exist yet it has been logged here)")
            'EventLog.WriteEntry("Application", sb.ToString, EventLogEntryType.Error)
        End Try
    End Sub


End Class
