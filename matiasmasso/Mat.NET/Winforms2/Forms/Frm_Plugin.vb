Public Class Frm_Plugin
    Private _Skus As List(Of DTOProductSku)
    Private _ImageList As ImageList

    Public Sub New()
        InitializeComponent()
        _Skus = New List(Of DTOProductSku)
        _ImageList = New ImageList()
        _ImageList.ImageSize = New Size(DTOProductSku.THUMBNAILWIDTH, DTOProductSku.THUMBNAILHEIGHT)
        With ListView1
            .LargeImageList = _ImageList
            .LabelEdit = True
            .Sorting = SortOrder.None
            .AllowDrop = True
            .InsertionMark.Color = Color.Green
        End With
    End Sub

    Private Sub AfegirProducteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AfegirProducteToolStripMenuItem.Click
        Dim oFrm As New Frm_Catalog(DTOProduct.SelectionModes.SelectSkus)
        AddHandler oFrm.OnItemsSelected, AddressOf AddSkus
        oFrm.Show()
    End Sub

    Private Async Sub AddSkus(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        RichTextBoxInstructions.Visible = False
        Dim oSkus As List(Of DTOProduct) = e.Argument
        For Each oSku As DTOProductSku In oSkus
            Dim oImage = Await FEB.ProductSku.Thumbnail(exs, oSku, DTOProductSku.THUMBNAILWIDTH, DTOProductSku.THUMBNAILHEIGHT)
            If exs.Count = 0 Then
                _ImageList.Images.Add(oSku.Guid.ToString, LegacyHelper.ImageHelper.Converter(oImage))
                Dim imgIdx = _ImageList.Images.Count - 1
                Dim sNom = oSku.NomLlarg.Tradueix(DTOLang.ESP)
                Dim item As New ListViewItem(sNom, imgIdx)
                item.Tag = oSku
                ListView1.Items.Add(item)
            End If
        Next
        TextBoxHtml.Text = Html()
    End Sub

    Private Sub ListView1_ItemDrag(sender As Object, e As ItemDragEventArgs) Handles ListView1.ItemDrag
        DoDragDrop(e.Item, DragDropEffects.Move)
    End Sub

    Private Sub ListView1_DragEnter(sender As Object, e As DragEventArgs) Handles ListView1.DragEnter
        RichTextBoxInstructions.Visible = False
        e.Effect = e.AllowedEffect
    End Sub

    Private Sub ListView1_DragOver(sender As Object, e As DragEventArgs) Handles ListView1.DragOver
        Dim targetPoint As Point = ListView1.PointToClient(New Point(e.X, e.Y))
        Dim targetIndex As Integer = ListView1.InsertionMark.NearestIndex(targetPoint)

        If targetIndex > -1 Then
            Dim itemBounds As Rectangle = ListView1.GetItemRect(targetIndex)

            If targetPoint.X > itemBounds.Left + (itemBounds.Width / 2) Then
                ListView1.InsertionMark.AppearsAfterItem = True
            Else
                ListView1.InsertionMark.AppearsAfterItem = False
            End If
        End If

        ListView1.InsertionMark.Index = targetIndex
    End Sub

    'Removes the insertion mark when the mouse leaves the control
    Private Sub ListView1_DragLeave(sender As Object, e As EventArgs) Handles ListView1.DragLeave
        ListView1.InsertionMark.Index = -1
    End Sub

    'Moves the item to the location of the insertion mark
    Private Sub ListView1_DragDrop(sender As Object, e As DragEventArgs) Handles ListView1.DragDrop
        Dim targetIndex As Integer = ListView1.InsertionMark.Index

        'If the insertion mark is not visible, exit the method
        If targetIndex = -1 Then Return

        'If the insertion mark is to the right of the item with
        'the corresponding index, increment the target index.
        If ListView1.InsertionMark.AppearsAfterItem Then targetIndex += 1

        'Insert a copy of the dragged item at the target index.
        'A copy must be inserted before the original item Is removed
        'to preserve item index values.
        Dim draggedItem As ListViewItem = e.Data.GetData(GetType(ListViewItem))
        Dim clonedItem As ListViewItem = draggedItem.Clone()
        ListView1.Items.Insert(targetIndex, clonedItem) 'CType(draggedItem.Clone(), ListViewItem))
        ListView1.Items.Remove(draggedItem)
        ListView1.InsertionMark.Index = -1
        clonedItem.Selected = True
        UpdateLayout()
        TextBoxHtml.Text = Html()
    End Sub

    Private Sub UpdateLayout()
        If Me.ListView1.View = View.LargeIcon OrElse Me.ListView1.View = View.SmallIcon OrElse Me.ListView1.View = View.Tile Then
            ListView1.BeginUpdate()
            ListView1.Alignment = ListViewAlignment.[Default]
            ListView1.Alignment = ListViewAlignment.Top
            ListView1.EndUpdate()
        End If
    End Sub

    Private Sub CopiarCodiHtmlToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopiarCodiHtmlToolStripMenuItem.Click
        UIHelper.CopyToClipboard(Html())
    End Sub

    Private Sub ListView1_AfterLabelEdit(sender As Object, e As LabelEditEventArgs) Handles ListView1.AfterLabelEdit
        TextBoxHtml.Text = Html()
    End Sub

    Private Function Html() As String
        Dim width = DTOProductSku.THUMBNAILWIDTH
        Dim height = DTOProductSku.THUMBNAILHEIGHT

        Dim sb As New Text.StringBuilder
        sb.AppendLine("<!------------------------------------- -->")
        sb.AppendLine("<div class='Plugin' data-pluginId='1'>")
        sb.AppendLine("    <i class='fa-solid fa-chevron-left ChevronLeft'></i>")
        sb.AppendLine("    <div>")
        For Each item As ListViewItem In ListView1.Items
            Dim oSku As DTOProductSku = item.Tag
            Dim href = oSku.UrlCanonicas.RelativeUrl(DTOLang.ESP)
            Dim alt = oSku.NomLlarg.Tradueix(DTOLang.ESP)
            Dim src = oSku.thumbnailUrl()
            Dim text = item.Text
            Dim s = String.Format("        <a href='{0}' title='{1}'><div><img src='{2}' width='{3}' height='{4}' alt='{1}'/></div><div>{5}</div></a>", href, alt, src, width, height, text)
            sb.AppendLine(s)
        Next
        sb.AppendLine("    </div>")
        sb.AppendLine("    <i class='fa-solid fa-chevron-right ChevronRight'></i>")
        sb.AppendLine("</div>")
        sb.AppendLine("<!------------------------------------- -->")
        Return sb.ToString()
    End Function

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click

    End Sub
End Class