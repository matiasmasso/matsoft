Public Class MsgController
    Inherits _BaseController


    <HttpPost>
    <Route("api/msg/update")>
    Public Function Update(oMsg As DTOMsg) As DUI.TaskResult
        Dim retval As New DUI.TaskResult

        Dim exs As New List(Of Exception)
        If BLLMsg.Update(oMsg, exs) Then
            If BLLMsg.MailInfo(oMsg, exs) Then
                retval.Success = True
            Else
                retval.Message = BLLExceptions.ToFlatString(exs)
            End If
        Else
            retval.Message = BLLExceptions.ToFlatString(exs)
        End If

        Return retval
    End Function


End Class

