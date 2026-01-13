Public MustInherit Class Xl_ExpandableDataGridView
    Inherits _Xl_ReadOnlyDatagridview

    Protected AllowEvents As Boolean

    Protected Property ControlItems As ExpandableItems
    Protected Property SelectedItem As ExpandableItem

    Public Event RequestToInsertNestedItems(sender As Object, e As MatEventArgs)

    Protected Sub BindDataSource()
        MyBase.DataSource = Me.ControlItems
    End Sub

    Private Sub Datagridview_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If AllowEvents Then
        End If
    End Sub

    Private Sub Xl_ExpandableDataGridView_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles Me.CellMouseClick
        If (MyBase.SelectedRows.Count > 0) Then
            SelectedItem = MyBase.SelectedRows(0).DataBoundItem
            expandOrCollapse()
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If AllowEvents Then
            If MyBase.SelectedRows.Count > 0 Then
                SelectedItem = MyBase.SelectedRows(0).DataBoundItem
            End If
        End If
    End Sub

    Public Sub ExpandOrCollapse(Optional includeObsolets As Boolean = False)
        If SelectedItem IsNot Nothing AndAlso SelectedItem.HasIcon() Then
            SelectedItem.Toggle()
            If SelectedItem.IsExpanded Then
                RaiseEvent RequestToInsertNestedItems(Me, New MatEventArgs(includeObsolets))
            Else
                CollapseNestedLevels()
            End If
        End If
    End Sub

    Public Function ParentItem() As ExpandableItem
        Dim retval As ExpandableItem = Nothing
        If _SelectedItem IsNot Nothing Then
            Dim idx = _ControlItems.IndexOf(SelectedItem)
            For i = idx To 0 Step -1
                If _ControlItems(i).Level < SelectedItem.Level Then
                    retval = _ControlItems(i)
                    Exit For
                End If
            Next
        End If
        Return retval
    End Function

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oRow As DataGridViewRow = MyBase.CurrentRow
        If oRow IsNot Nothing Then
            SelectedItem = oRow.DataBoundItem
        End If
    End Sub

    Private Sub CollapseNestedLevels()

        Dim idx = ControlItems.IndexOf(SelectedItem) + 1
        Do While ControlItems.Count > idx
            If ControlItems(idx).Level <= SelectedItem.Level Then Exit Do
            ControlItems.RemoveAt(idx)
        Loop
        SelectedItem = Nothing
        ClearSelection()
    End Sub

    Protected Sub InsertControlItems(oControlItems As ExpandableItems)
        Dim idx = _ControlItems.IndexOf(SelectedItem)
        For i As Integer = oControlItems.Count - 1 To 0 Step -1
            _ControlItems.Insert(idx + 1, oControlItems(i))
        Next
    End Sub

    Protected Sub ExpandSingleItemLevels()
        Dim firstLevelItems = ControlItems.Where(Function(x) x.Level = 0).ToList()
        If firstLevelItems.Count = 1 Then
            SelectedItem = firstLevelItems.First()
            ExpandOrCollapse()
            Dim secondLevelItems = ControlItems.Where(Function(x) x.Level = 1)
            If secondLevelItems.Count = 1 Then
                SelectedItem = secondLevelItems.First()
                ExpandOrCollapse()
            End If
        End If
    End Sub


    Private Sub DataGridView1_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles Me.CellPainting
        If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
            Dim oRow As DataGridViewRow = DirectCast(sender, DataGridView).Rows(e.RowIndex)
            Dim oControlItem As ExpandableItem = oRow.DataBoundItem
            Select Case oControlItem.CellSpan
                Case ExpandableItem.CellSpanCods.SpanAll
                    e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None
                Case ExpandableItem.CellSpanCods.SpanIfCollapsed
                    If oControlItem.IsCollapsed Then
                        e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None
                    End If
            End Select

            If e.ColumnIndex = 0 Then
                DrawCaption(sender, e)
                If MyBase.SelectedRows.Contains(oRow) Then MarkSelected(sender, e)
                'If oControlItem.Equals(SelectedItem) Then MarkSelected(sender, e)
                If oControlItem.HasIcon() Then
                    DrawIcon(sender, e)
                End If
            Else
                Dim grid As DataGridView = sender
                Dim oClipRectangle As Rectangle
                Dim X As Integer
                For i = 0 To e.ColumnIndex - 1
                    X += MyBase.Columns(i).Width
                Next
                oClipRectangle = New Rectangle(X, e.ClipBounds.Y, MyBase.Columns(e.ColumnIndex).Width, e.ClipBounds.Height)
                e.Paint(oClipRectangle, DataGridViewPaintParts.All)
            End If

            e.Handled = True
        End If
    End Sub

    Private Sub DrawCaption(sender As Object, e As DataGridViewCellPaintingEventArgs)
        Dim grid As DataGridView = sender
        Dim oRow = grid.Rows(e.RowIndex)
        Dim oControlItem As ExpandableItem = oRow.DataBoundItem
        Dim oClipRectangle As Rectangle
        Dim padding = 5 + 20 * oControlItem.Level
        e.CellStyle.Padding = New Padding(padding + 9, 0, 0, 0)
        If oControlItem.CellSpan Then
            Dim iRowWidth As Integer = 0
            For i = 0 To MyBase.Columns.Count - 1
                If MyBase.Columns(i).Visible Then iRowWidth += MyBase.Columns(i).Width
            Next
            'oClipRectangle = New Rectangle(e.ClipBounds.X + padding, e.ClipBounds.Y, iRowWidth - padding, e.ClipBounds.Height)
            oClipRectangle = New Rectangle(e.ClipBounds.X + padding, e.ClipBounds.Y, MyBase.Columns(0).Width - padding, e.ClipBounds.Height)
        Else
            oClipRectangle = New Rectangle(e.ClipBounds.X + padding, e.ClipBounds.Y, MyBase.Columns(0).Width - padding, e.ClipBounds.Height)
        End If
        e.Paint(oClipRectangle, DataGridViewPaintParts.All)
    End Sub

    Private Sub MarkSelected(sender As Object, e As DataGridViewCellPaintingEventArgs)
        Dim grid As DataGridView = sender
        Dim oRow = grid.Rows(e.RowIndex)
        Dim oControlItem As ExpandableItem = oRow.DataBoundItem
        Dim mark As New Rectangle(e.CellBounds.Left, e.CellBounds.Top, 3, e.CellBounds.Height)
        Dim pen As New Pen(Brushes.Navy)
        e.Graphics.FillRectangle(Brushes.Navy, mark)
        e.Graphics.DrawRectangle(pen, mark)
    End Sub

    Private Sub DrawIcon(sender As Object, e As DataGridViewCellPaintingEventArgs)
        Dim grid As DataGridView = sender
        Dim oRow = grid.Rows(e.RowIndex)
        Dim oControlItem As ExpandableItem = oRow.DataBoundItem
        Dim ico = IIf(oControlItem.IsExpanded, My.Resources.Expanded9, My.Resources.Collapsed9)
        Dim padding = 5 + 20 * oControlItem.Level
        Dim icoRectangle As New Rectangle(e.CellBounds.Left + padding, e.CellBounds.Top + 4, ico.Width, ico.Height)
        e.Graphics.DrawImage(ico, icoRectangle)
    End Sub


    Protected Class ExpandableItems
        Inherits SortableBindingList(Of ExpandableItem)
    End Class

End Class


