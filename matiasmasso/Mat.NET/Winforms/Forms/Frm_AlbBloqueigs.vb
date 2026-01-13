Public Class Frm_AlbBloqueigs

    Private Sub Frm_AlbBloqueigs_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub

    Private Async Sub refresca()
        Dim exs As New List(Of Exception)
        Dim items = Await FEB2.AlbBloqueigs.All(GlobalVariables.Emp, exs)
        If exs.Count = 0 Then
            Xl_AlbBloqueigs1.Load(items)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_AlbBloqueigs1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_AlbBloqueigs1.RequestToRefresh
        refresca()
    End Sub
End Class