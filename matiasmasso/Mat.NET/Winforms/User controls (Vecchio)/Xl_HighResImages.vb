Public Class Xl_HighResImages
    Private _Values As List(Of DTOHighResImage)
    Private _ImageList As ImageList

    Public Shadows Sub Load(oValues As List(Of DTOHighResImage))
        _Values = oValues
        LoadImages()
        LoadItems()
    End Sub

    Private Sub LoadImages()
        _ImageList = New ImageList

        If _Values.Count > 0 Then
            _ImageList.ImageSize = New Size(150, 150)
            ListView1.LargeImageList = _ImageList
            ListView1.View = View.LargeIcon
            ListView1.ShowItemToolTips = True

            For Each oItem As DTOHighResImage In _Values
                _ImageList.Images.Add(oItem.Hash, oItem.Thumbnail)
            Next
        End If
    End Sub

    Private Sub LoadItems()
        ListView1.Items.Clear()

        For Each oItem As DTOHighResImage In _Values
            Dim oListViewItem As ListViewItem = ListView1.Items.Add(BLLHighResImage.MultilineFeatures(oItem), oItem.Hash)
            oListViewItem.ToolTipText = oItem.Url
        Next
    End Sub

    Public Function SelectedItems() As List(Of DTOHighResImage)
        Dim retval As New List(Of DTOHighResImage)
        For Each oItem As ListViewItem In ListView1.SelectedItems
            Dim oHighResImage As DTOHighResImage = _Values.Find(Function(x) x.Hash = oItem.ImageKey)
            retval.Add(oHighResImage)
        Next
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip

        If ListView1.SelectedItems.Count > 0 Then
            Dim oListItem As ListViewItem = ListView1.SelectedItems(0)

            If oListItem IsNot Nothing Then
                Dim oMenu_HighResImage As New Menu_HighResImage(SelectedItems.First)
                AddHandler oMenu_HighResImage.AfterUpdate, AddressOf refreshrequest
                oContextMenu.Items.AddRange(oMenu_HighResImage.Range)
            End If
        End If
        ListView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub refreshrequest()

    End Sub

    Private Sub ListView1_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles ListView1.ItemSelectionChanged
        SetContextMenu()
    End Sub


End Class
