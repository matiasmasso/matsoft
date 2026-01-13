Public Class Xl_HighResImages
    Private _Values As HighResImages
    Private _ImageList As ImageList

    Public Shadows Sub Load(oValues As HighResImages)
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

            For Each oItem As HighResImage In _Values
                _ImageList.Images.Add(oItem.Hash, oItem.Thumbnail)
            Next
        End If
    End Sub

    Private Sub LoadItems()
        ListView1.Items.Clear()

        For Each oItem As HighResImage In _Values
            Dim oListViewItem As ListViewItem = ListView1.Items.Add(oItem.Features, oItem.Hash)
            oListViewItem.ToolTipText = oItem.Url
        Next
    End Sub

    Public Function SelectedItems() As HighResImages
        Dim retval As New HighResImages
        For Each oItem As ListViewItem In ListView1.SelectedItems
            Dim oHighResImage As HighResImage = _Values.Find(Function(x) x.Hash = oItem.ImageKey)
            retval.Add(oHighResImage)
        Next
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip

        If ListView1.SelectedItems.Count > 0 Then
            Dim oListItem As ListViewItem = ListView1.SelectedItems(0)

            If oListItem IsNot Nothing Then
                Dim oMenu_HighResImage As New Menu_OldHighResImage(SelectedItems.First)
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
