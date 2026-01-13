Public Class Frm_Noticias
    Private _AllowEvents As Boolean

    Private Sub Frm_Noticias_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ComboBoxCod.SelectedIndex = 0
        refresca()
        _AllowEvents = True
    End Sub

    Private Sub Xl_Noticias1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Noticias1.RequestToRefresh
        If _AllowEvents Then
            refresca()
        End If
    End Sub

    Private Sub refresca()
        Dim oNoticias As Noticias = NoticiasLoader.LastNews(BLL.BLLSession.Current, Cod(), False, 0)
        Xl_Noticias1.Load(oNoticias, Cod())
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxCod.SelectedIndexChanged
        If _AllowEvents Then
            refresca()
        End If
    End Sub

    Private Function Cod() As Noticia.Cods
        Dim retval As Noticia.Cods = ComboBoxCod.SelectedIndex
        Return retval
    End Function
End Class