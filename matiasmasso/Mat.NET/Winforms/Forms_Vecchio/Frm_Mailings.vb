Public Class Frm_Mailings

    Private Sub Frm_Mailings_Load(sender As Object, e As EventArgs) Handles Me.Load
        Refresca()
    End Sub

    Private Sub Refresca()
        Dim oMailings As List(Of Mailing) = MailingsLoader.All
        Xl_Mailings1.Load(oMailings)
    End Sub

    Private Sub Xl_Mailings1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Mailings1.RequestToAddNew
        Dim oMailing As New Mailing(Today)
        Dim oFrm As New Frm_MailingEditor(oMailing)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub


    Private Sub Xl_Mailings1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Mailings1.RequestToRefresh
        Refresca()
    End Sub
End Class