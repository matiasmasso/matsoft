Public Class Frm_MediaResources
    Private _AllowEvents As Boolean
    Private Async Sub Frm_MediaResources_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Await refrescaTree()
    End Sub

    Private Async Function refrescaTree() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim includeObsoletos = IncluirObsoletosToolStripMenuItem.Checked
        Dim oTree = Await FEB2.ProductCatalog.CompactTree(exs, Current.Session.Emp, includeObsoletos)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_ProductsTree1.Load(oTree)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Function refresca(oProduct As DTOProduct) As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim items = Await FEB2.MediaResources.ProductSpecificWithThumbnails(exs, oProduct)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_MediaResources1.Load(oProduct, items)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function


    Private Async Sub Xl_MediaResources1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_MediaResources1.RequestToRefresh
        Dim oProduct As DTOProduct = Xl_ProductsTree1.SelectedValue
        Await refresca(oProduct)
    End Sub

    Private Async Sub Xl_ProductsTree1_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles Xl_ProductsTree1.NodeMouseClick
        Await refresca(e.Node.Tag)
    End Sub


    Private Async Sub IncluirObsoletosToolStripMenuItem_CheckedChanged(sender As Object, e As EventArgs) Handles IncluirObsoletosToolStripMenuItem.CheckedChanged
        Await refrescaTree()
    End Sub

    Private Sub Xl_MediaResources1_RequestToToggleProgressBar(sender As Object, e As MatEventArgs) Handles Xl_MediaResources1.RequestToToggleProgressBar
        ProgressBar1.Visible = e.Argument
    End Sub
End Class