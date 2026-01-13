Public Class Frm_OutVivaceExpedicions

    Private Sub Frm_Templates_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub


    Private Sub Xl_OutVivaceExpedicions1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_OutVivaceExpedicions1.RequestToRefresh
        refresca()
    End Sub

    Private Sub refresca()
        Dim items As List(Of DTOOutVivaceLog.expedicion) = BLL.BllOutVivaceExpedicions.All
        Xl_OutVivaceExpedicions1.Load(items)
    End Sub

End Class