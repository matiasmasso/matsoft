Public Class Xl_ProductFilteredUrls
    Private _Product As DTOProduct
    Private _AllowEvents As Boolean

    Public Shadows Sub Load(oProduct As DTOProduct, oFilters As List(Of DTOFilter))
        _Product = oProduct
        Xl_CheckedFilters1.Load(oFilters, New List(Of DTOFilter.Item))
        _AllowEvents = True
    End Sub

    Private Sub Xl_CheckedFilters1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_CheckedFilters1.AfterUpdate
        If _AllowEvents Then
            Dim oFilterItems = Xl_CheckedFilters1.SelectedValues
            TextBoxUrl.Text = _Product.UrlWithFilters(_Product, Current.Session.Lang, oFilterItems)
        End If
    End Sub

    Private Sub ButtonCopyLink_Click(sender As Object, e As EventArgs) Handles ButtonCopyLink.Click
        TextBoxUrl.SelectionStart = 0
        TextBoxUrl.SelectionLength = TextBoxUrl.Text.Length
        UIHelper.CopyLink(TextBoxUrl.Text)
        TextBoxUrl.SelectionLength = 0
    End Sub

    Private Sub ButtonBrowse_Click(sender As Object, e As EventArgs) Handles ButtonBrowse.Click
        Dim url = TextBoxUrl.Text
        UIHelper.ShowHtml(url)
    End Sub
End Class
