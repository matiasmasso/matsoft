Public Class Frm_Events
    Private Sub Frm_Events_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        refresca()
    End Sub

    Private Sub Xl_Noticias1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Noticias1.RequestToRefresh
        refresca()
    End Sub

    Private Sub refresca()
        Dim oNoticias As Noticias = NoticiasLoader.LastNews(BLL.BLLSession.Current, Noticia.Cods.Eventos, False, 0)
        Xl_Noticias1.Load(oNoticias)
    End Sub
End Class