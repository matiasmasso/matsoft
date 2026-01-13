Public Class Frm_CliProductWebBlocks

    Private Sub Frm_CliProductWebBlocks_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub

    Private Sub refresca()
        Dim oItems As List(Of DTOCliProduct) = CliProductWebBlockLoader.All(CliProductWebBlockLoader.OrderBy.Cli)
        Xl_CliProducts1.Load(oItems)
    End Sub

    Private Sub Xl_CliProducts1_RequestToDelete(sender As Object, e As MatEventArgs) Handles Xl_CliProducts1.RequestToDelete
        Dim rc As MsgBoxResult = MsgBox("eliminem aquest bloqueig?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs as New List(Of exception)
            If CliProductWebBlockLoader.Delete(e.Argument, exs) Then
                refresca()
            Else
                MsgBox("error al eliminar el document" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
            End If
        Else
            MsgBox("Operació cancelada per l'usuari", MsgBoxStyle.Exclamation, "MAT.NET")
        End If

    End Sub

    Private Sub Xl_CliProducts1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_CliProducts1.RequestToRefresh
        refresca()
    End Sub
End Class