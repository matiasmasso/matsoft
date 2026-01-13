Public Class Frm_UserSearch
    Private Async Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Dim exs As New List(Of Exception)
        Dim src As String = e.Argument
        If src.Length > 3 Then
            Dim oUsers = Await FEB.Users.Search(exs, GlobalVariables.Emp, src)
            If exs.Count = 0 Then
                Xl_Users1.Load(oUsers)
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            Xl_Users1.Load(New List(Of DTOUser))
        End If
    End Sub
End Class