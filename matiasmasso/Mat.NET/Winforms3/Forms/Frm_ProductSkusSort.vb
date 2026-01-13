Public Class Frm_ProductSkusSort
    Private _Category As DTOProductCategory
    Private _Skus As List(Of DTOProductSku)
    Private _ImageList As ImageList
    Private _NoImage As System.Drawing.Image

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oCategory As DTOProductCategory = Nothing)
        InitializeComponent()
        _Category = oCategory
        If _Category IsNot Nothing AndAlso _Category.Skus IsNot Nothing Then
            _Skus = _Category.Skus.Where(Function(x) x.obsoleto = False).ToList()
            _ImageList = New ImageList()
            _ImageList.ImageSize = New Size(DTOProductSku.THUMBNAILWIDTH, DTOProductSku.THUMBNAILHEIGHT)
            With ListView1
                .LargeImageList = _ImageList
                .LabelEdit = False
                .Sorting = SortOrder.None
                .AllowDrop = True
                .InsertionMark.Color = Color.Green
            End With
        End If
    End Sub

    Private Async Sub Frm_ProductSkusSort_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        For Each oSku As DTOProductSku In _Skus
            Await AddSku(exs, oSku)
        Next
    End Sub

    Private Async Function AddSku(exs As List(Of Exception), oSku As DTOProductSku) As Task
        Dim oImage = Await FEB.ProductSku.Thumbnail(exs, oSku, DTOProductSku.THUMBNAILWIDTH, DTOProductSku.THUMBNAILHEIGHT)
        Dim oLegacyImage As System.Drawing.Image = Nothing
        If exs.Count = 0 Then
            If oImage Is Nothing Then
                If _NoImage Is Nothing Then
                    Dim oNoImage = Await FEB.DefaultImage.Image(Defaults.ImgTypes.art150, exs)
                    If oNoImage Is Nothing Then
                        _NoImage = New Bitmap(150, 171)
                    Else
                        _NoImage = LegacyHelper.ImageHelper.Converter(oNoImage)
                    End If
                End If
                oLegacyImage = _NoImage
            Else
                oLegacyImage = LegacyHelper.ImageHelper.Converter(oImage)
            End If

            _ImageList.Images.Add(oSku.Guid.ToString, oLegacyImage)
            Dim imgIdx = _ImageList.Images.Count - 1
            Dim sNom = oSku.Nom.Tradueix(DTOLang.ESP)
            Dim item As New ListViewItem(sNom, imgIdx)
            item.Tag = oSku
            ListView1.Items.Add(item)
        End If
    End Function

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        Dim oDict As New Dictionary(Of Guid, Integer)
        For idx As Integer = 1 To ListView1.Items.Count
            Dim item = ListView1.Items(idx - 1)
            Dim oSku As DTOProductSku = item.Tag
            oDict.Add(oSku.Guid, idx)
        Next

        UIHelper.ToggleProggressBar(PanelButtons, True)
        If Await FEB.ProductCategory.SortSkus(exs, _Category, oDict) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(oDict))
            UIHelper.ToggleProggressBar(PanelButtons, False)
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(PanelButtons, False)
            UIHelper.WarnError(exs)
        End If

    End Sub


    Private Sub ListView1_ItemDrag(sender As Object, e As ItemDragEventArgs) Handles ListView1.ItemDrag
        DoDragDrop(e.Item, DragDropEffects.Move)
    End Sub

    Private Sub ListView1_DragEnter(sender As Object, e As DragEventArgs) Handles ListView1.DragEnter
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
        ButtonOk.Enabled = True
    End Sub

    Private Sub UpdateLayout()
        If Me.ListView1.View = View.LargeIcon OrElse Me.ListView1.View = View.SmallIcon OrElse Me.ListView1.View = View.Tile Then
            ListView1.BeginUpdate()
            ListView1.Alignment = ListViewAlignment.[Default]
            ListView1.Alignment = ListViewAlignment.Top
            ListView1.EndUpdate()
        End If
    End Sub


End Class