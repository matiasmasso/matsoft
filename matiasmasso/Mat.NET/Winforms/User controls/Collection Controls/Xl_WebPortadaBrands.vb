Public Class Xl_WebPortadaBrands
    Inherits ListView

    Private _Values As List(Of DTOWebPortadaBrand)
    Private _ImageList As ImageList
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event RequestToDelete(sender As Object, e As MatEventArgs)
    Public Event RequestToSort(sender As Object, e As MatEventArgs)

    Public Shadows Async Sub Load(values As List(Of DTOWebPortadaBrand))
        _Values = values

        Await refresca()
        SetContextMenu()
        _AllowEvents = True
    End Sub

    Public ReadOnly Property Values() As List(Of DTOWebPortadaBrand)
        Get
            Dim retval As New List(Of DTOWebPortadaBrand)
            For Each item As ListViewItem In MyBase.Items
                retval.Add(item.Tag)
            Next
            Return retval
        End Get
    End Property

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        _ImageList = New ImageList()
        _ImageList.ImageSize = New Size(150, 150)

        MyBase.Clear()
        MyBase.LargeImageList = _ImageList
        MyBase.View = View.LargeIcon
        MyBase.ListViewItemSorter = New ListViewIndexComparer()

        For Each value As DTOWebPortadaBrand In _Values
            Dim item As New ListViewItem(value.Brand.Nom, value.Brand.Guid.ToString())
            item.Tag = value
            Dim oImage = Await FEB2.WebPortadaBrand.Image(exs, value.Brand)
            _ImageList.Images.Add(value.Brand.Guid.ToString, LegacyHelper.ImageHelper.Converter(oImage))
            MyBase.Items.Add(item)
        Next

        If exs.Count > 0 Then
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub Xl_WebPortadaBrands_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles Me.ItemSelectionChanged
        If _AllowEvents Then setContextMenu
    End Sub

    Private Function CurrentItem() As DTOWebPortadaBrand
        Dim retval As DTOWebPortadaBrand = Nothing
        If MyBase.SelectedItems.Count > 0 Then
            Dim item As ListViewItem = MyBase.SelectedItems(0)
            retval = item.Tag
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim item As DTOWebPortadaBrand = CurrentItem()

        If item IsNot Nothing Then
            oContextMenu.Items.Add("zoom", Nothing, AddressOf Do_Zoom)
            oContextMenu.Items.Add("eliminar", Nothing, AddressOf Do_Delete)
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_Zoom()
        Dim oFrm As New Frm_WebPortadaBrand(CurrentItem)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Delete()
        RaiseEvent RequestToDelete(Me, New MatEventArgs(CurrentItem))
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Private Sub mybase_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragDrop
        ' Retrieve the index of the insertion mark;
        Dim targetIndex As Integer = MyBase.InsertionMark.Index

        ' If the insertion mark is not visible, exit the method.
        If targetIndex = -1 Then
            Return
        End If

        ' If the insertion mark is to the right of the item with
        ' the corresponding index, increment the target index.
        If MyBase.InsertionMark.AppearsAfterItem Then
            targetIndex += 1
        End If

        ' Retrieve the dragged item.
        Dim draggedItem As ListViewItem = DirectCast(e.Data.GetData(GetType(ListViewItem)), ListViewItem)

        ' Insert a copy of the dragged item at the target index.
        ' A copy must be inserted before the original item is removed
        ' to preserve item index values.
        MyBase.Items.Insert(targetIndex, DirectCast(draggedItem.Clone(), ListViewItem))

        ' Remove the original copy of the dragged item.
        MyBase.Items.Remove(draggedItem)

        RaiseEvent RequestToSort(Me, MatEventArgs.Empty)
    End Sub

    ' Removes the insertion mark when the mouse leaves the control.
    Private Sub myListView_DragLeave(sender As Object, e As EventArgs)
        MyBase.InsertionMark.Index = -1
    End Sub 'myListView_DragLeave



    Private Sub Xl_WebPortadaBrands_ItemDrag(sender As Object, e As ItemDragEventArgs) Handles Me.ItemDrag
        MyBase.DoDragDrop(e.Item, DragDropEffects.Move)
    End Sub

    Private Sub Xl_WebPortadaBrands_DragEnter(sender As Object, e As DragEventArgs) Handles Me.DragEnter
        ' Check for the custom DataFormat ListViewItem array.

        If e.Data.GetDataPresent(GetType(ListViewItem)) Then
            e.Effect = DragDropEffects.Move
        Else
            e.Effect = DragDropEffects.None
        End If

    End Sub

    Private Sub myListView_DragOver(sender As Object, e As DragEventArgs) Handles Me.DragOver
        ' Retrieve the client coordinates of the mouse pointer.
        Dim targetPoint As Point = MyBase.PointToClient(New Point(e.X, e.Y))

        ' Retrieve the index of the item closest to the mouse pointer.
        Dim targetIndex As Integer =
            MyBase.InsertionMark.NearestIndex(targetPoint)

        ' Confirm that the mouse pointer is not over the dragged item.
        If targetIndex > -1 Then
            ' Determine whether the mouse pointer is to the left or
            ' the right of the midpoint of the closest item and set
            ' the InsertionMark.AppearsAfterItem property accordingly.
            Dim itemBounds As Rectangle = MyBase.GetItemRect(targetIndex)
            If targetPoint.X > itemBounds.Left + (itemBounds.Width / 2) Then
                MyBase.InsertionMark.AppearsAfterItem = True
            Else
                MyBase.InsertionMark.AppearsAfterItem = False
            End If
        End If

        ' Set the location of the insertion mark. If the mouse is
        ' over the dragged item, the targetIndex value is -1 and
        ' the insertion mark disappears.
        MyBase.InsertionMark.Index = targetIndex
    End Sub 'myListView_DragOver

    ' Sorts ListViewItem objects by index.    
    Private Class ListViewIndexComparer
        Implements System.Collections.IComparer

        Public Function Compare(x As Object, y As Object) As Integer _
            Implements System.Collections.IComparer.Compare
            Return DirectCast(x, ListViewItem).Index - DirectCast(y, ListViewItem).Index
        End Function 'Compare

    End Class 'ListViewIndexComparer
End Class

