Public Class Frm_MailingPros2
    Private Async Sub Frm_MailingPros2_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub


    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim oContacts = Await FEB2.Users.Professionals(GlobalVariables.Emp, exs)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_EmailsContacts1.Load(oContacts)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_EmailsContacts1.Filter = e.Argument
    End Sub
End Class