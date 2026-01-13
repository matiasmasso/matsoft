Public Class Frm_Descatalogats

    Private Async Sub Frm_Descatalogats_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim excludeConfirmed = ExcludeConfirmedMenuItem.Checked
        Dim oDescatalogats = Await FEB.ProductSkus.Descatalogats(exs, Current.Session.User, excludeConfirmed)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_Descatalogats1.Load(oDescatalogats)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub Xl_Descatalogats1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Descatalogats1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Sub RefrescaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefrescaToolStripMenuItem.Click
        Await refresca()
    End Sub

    Private Async Sub ExcludeConfirmedMenuItem_Click(sender As Object, e As EventArgs) Handles ExcludeConfirmedMenuItem.Click
        Await refresca()
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_Descatalogats1.Filter = e.Argument
    End Sub
End Class