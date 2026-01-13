Public Class Frm_Gallery

    Private Sub Frm_Gallery_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub

    Private Sub Xl_Gallery1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Gallery1.RequestToAddNew
        Dim value As DTOGalleryItem = BLL.BLLGalleryItem.NewGalleryItem
        Dim oFrm As New Frm_GalleryItem(value)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Sub Xl_Gallery1_RefreshRequest() Handles Xl_Gallery1.RequestToRefresh
        refresca()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        refresca()
    End Sub

    Private Sub refresca()
        Dim sSearchText As String = TextBox1.Text
        Dim oItems As List(Of DTOGalleryItem) = BLL.BLLGalleryItems.All(TextBox1.Text)
        Xl_Gallery1.Load(oItems)
    End Sub

End Class