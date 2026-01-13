Public Class Frm_CreditsLastAlbs
    Private Async Sub Frm_CreditsLastAlbs_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        Dim items = Await FEB2.CliCreditLogs.CreditLastAlbs(Current.Session.Emp, exs)
        If exs.Count = 0 Then
            Xl_CreditsLastAlbs1.Load(items)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class