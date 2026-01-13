Public Class Xl_ProductFeaturedImages
    Private _values As List(Of DTOProductFeatureImage)
    Private _privateDrag As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Shadows Sub Load(values As List(Of DTOProductFeatureImage))
        _values = values
        refresca()
    End Sub

    Private Sub refresca()
        ListView1.Items.Clear()
        ImageList1.Images.Clear()
        Dim oLv As ListViewItem
        Dim Idx As Integer
        For Each item As DTOProductFeatureImage In _values
            Dim oThumbnail As System.Drawing.Image = BLL.ImageHelper.GetThumbnailToFit(item.Image, ImageList1.ImageSize.Width, ImageList1.ImageSize.Height)
            ImageList1.Images.Add(oThumbnail)
            'ImageList1.Images.Add(item.Image)
            oLv = New ListViewItem
            oLv.ImageIndex = Idx
            ListView1.Items.Add(oLv)
            Idx = Idx + 1
        Next
        setcontextmenu()
    End Sub

    Public ReadOnly Property Values As List(Of DTOProductFeatureImage)
        Get
            Return _values
        End Get
    End Property

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip

        If ListView1.SelectedItems.Count > 0 Then
            Dim idx As Integer = ListView1.SelectedItems(0).Index
            Dim oFeature As DTOProductFeatureImage = _values(idx)
            Dim oMenu_Feature As New Menu_ProductFeatureImage(oFeature)
            AddHandler oMenu_Feature.AfterUpdate, AddressOf RefreshRequest
            AddHandler oMenu_Feature.RequestToDelete, AddressOf Do_Delete
            oContextMenu.Items.AddRange(oMenu_Feature.Range)
            Dim oMoveUp As New ToolStripMenuItem("pujar", My.Resources.GoUp, AddressOf Do_MoveUp)
            If idx = 0 Then oMoveUp.Enabled = False
            oContextMenu.Items.Add(oMoveUp)
            Dim oMoveDown As New ToolStripMenuItem("baixar", My.Resources.GoDown, AddressOf Do_MoveDown)
            If idx = ListView1.Items.Count - 1 Then oMoveDown.Enabled = False
            oContextMenu.Items.Add(oMoveDown)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        ListView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_MoveUp()
        Dim idx As Integer = ListView1.SelectedItems(0).Index
        Dim oCurrent As DTOProductFeatureImage = _values(idx)
        _values(idx) = _values(idx - 1)
        _values(idx - 1) = oCurrent
        RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
        refresca()
    End Sub

    Private Sub Do_MoveDown()
        Dim idx As Integer = ListView1.SelectedItems(0).Index
        Dim oCurrent As DTOProductFeatureImage = _values(idx)
        _values(idx) = _values(idx + 1)
        _values(idx + 1) = oCurrent
        RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
        refresca()
    End Sub

    Private Sub Do_AddNew()
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Filter = "(Image Files)|*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.ico|Jpg, | *.jpg|Png, | *.png|Bmp, | *.bmp|Gif, | *.gif|Ico | *.ico"
            '.Multiselect = False
            .Title = "Afegir imatges de producte"
            If .ShowDialog Then
                For Each s As String In .FileNames
                    Dim oImage As System.Drawing.Image = System.Drawing.Image.FromFile(s)
                    Dim oItem As New DTOProductFeatureImage
                    With oItem
                        .Guid = System.Guid.NewGuid
                        .Image = oImage
                    End With
                    _values.Add(oItem)
                    RaiseEvent AfterUpdate(Me, New MatEventArgs(oItem))
                Next
                refresca()
            End If

        End With

    End Sub

    Private Sub Do_Delete(sender As Object, e As MatEventArgs)
        Dim oFeature As DTOProductFeatureImage = e.Argument
        _values.Remove(oFeature)
        refresca()
        RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
    End Sub

    Private Sub RefreshRequest()

    End Sub

    Private Sub ListView1_DragEnter(sender As Object, e As DragEventArgs) Handles ListView1.DragEnter
        If _privateDrag Then
            e.Effect = e.AllowedEffect
        End If
    End Sub

    Private Sub ListView1_KeyDown(sender As Object, e As KeyEventArgs) Handles ListView1.KeyDown
        If ListView1.SelectedItems.Count > 0 Then
            _values.RemoveAt(ListView1.SelectedItems(0).Index)
            refresca()
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
        End If
    End Sub



    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        SetContextMenu()
    End Sub

    Private Sub ListView_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles ListView1.ItemDrag
        _privateDrag = True
        DoDragDrop(e.Item, DragDropEffects.Move)
        _privateDrag = False
    End Sub
End Class
