Public Class Frm_AlbBloqueigs

    Private Sub Frm_AlbBloqueigs_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub

    Private Sub refresca()
        Dim items As List(Of DTOAlbBloqueig) = BLL.BLLAlbBloqueigs.All
        Xl_AlbBloqueigs1.Load(items)
    End Sub

    Private Sub Xl_AlbBloqueigs1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_AlbBloqueigs1.RequestToRefresh
        refresca()
    End Sub
End Class