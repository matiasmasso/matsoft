Public Class Xl_ECITransmGroups
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOECITransmGroup)
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Shadows Event RequestToDelete(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Nom
    End Enum

    Public Shadows Sub Load(values As List(Of DTOECITransmGroup))
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOECITransmGroup) = _Values
        _ControlItems = New ControlItems With {.AllowNew = True}
        For Each oItem As DTOECITransmGroup In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        _AllowEvents = True
    End Sub


    Public ReadOnly Property Value As DTOECITransmGroup
        Get
            Dim retval As DTOECITransmGroup = Nothing
            Dim oControlItem As ControlItem = CurrentControlItem()
            If oControlItem IsNot Nothing Then retval = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = True
        MyBase.RowHeadersWidth = 32
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = True
        MyBase.ReadOnly = False
        MyBase.AllowUserToAddRows = True
        MyBase.AllowUserToDeleteRows = True

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Agrupacions"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
    End Sub

    Private Function SelectedControlItems() As ControlItems
        Dim retval As New ControlItems
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem)
        Return retval
    End Function

    Private Function SelectedItems() As List(Of DTOECITransmGroup)
        Dim retval As New List(Of DTOECITransmGroup)
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem.Source)
        Return retval
    End Function

    Private Function CurrentControlItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = MyBase.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()

    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim oSource As DTOECITransmGroup = Nothing
            If oControlItem IsNot Nothing Then oSource = oControlItem.Source
            RaiseEvent ValueChanged(Me, New MatEventArgs(oSource))
            SetContextMenu()
        End If
    End Sub

    Private Sub Xl_ECITransmGroups_Editable_UserDeletingRow(sender As Object, e As DataGridViewRowCancelEventArgs) Handles Me.UserDeletingRow
        Dim oRow As DataGridViewRow = e.Row
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        Dim oContact As DTOECITransmGroup = oControlItem.Source
        RaiseEvent RequestToDelete(Me, New MatEventArgs(oContact))
    End Sub

    Private Async Sub Xl_ECITransmGroups_RowValidated(sender As Object, e As DataGridViewCellEventArgs) Handles Me.RowValidated
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        Try
            Dim oControlItem As ControlItem = oRow.DataBoundItem

            If oControlItem IsNot Nothing Then
                If oControlItem.Source Is Nothing Then
                    If oControlItem.Nom > "" Then
                        oControlItem.Source = New DTOECITransmGroup
                        Await Save(oControlItem)
                    End If
                Else
                    If oControlItem.Source.Nom <> oControlItem.Nom Then
                        Await Save(oControlItem)
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Async Function Save(oControlItem As ControlItem) As Task
        Dim item As DTOECITransmGroup = oControlItem.Source
        item.Nom = oControlItem.Nom
        If item.Ord = 0 Then
            Dim MaxOrd As Integer = _ControlItems.Max(Function(x) x.Source.Ord)
            item.Ord = MaxOrd + 1
        End If

        Dim exs As New List(Of Exception)
        If Await FEB2.ECITransmGroup.Update(item, exs) Then
        Else
            UIHelper.WarnError(exs)
        End If

    End Function

#Region "DragDrop"
    Private fromIndex As Integer
    Private dragIndex As Integer
    Private dragRect As Rectangle

    Private Async Sub DataGridView1_DragDrop(ByVal sender As Object, ByVal e As DragEventArgs) Handles Me.DragDrop
        Dim p As Point = MyBase.PointToClient(New Point(e.X, e.Y))
        dragIndex = MyBase.HitTest(p.X, p.Y).RowIndex
        If (e.Effect = DragDropEffects.Move) Then
            Dim dragRow As DataGridViewRow = e.Data.GetData(GetType(DataGridViewRow))
            If dragIndex < 0 Then dragIndex = MyBase.RowCount - 1
            Dim oControlItem As ControlItem = dragRow.DataBoundItem
            _ControlItems.RemoveAt(fromIndex)
            _ControlItems.Insert(dragIndex, oControlItem)

            Await SaveOrder()
            MyBase.RefreshRequest(Me, MatEventArgs.Empty)
        End If
    End Sub

    Private Async Function SaveOrder() As Task
        Dim exs As New List(Of Exception)
        For i As Integer = 0 To _ControlItems.Count - 1
            Dim oGroup As DTOECITransmGroup = _ControlItems(i).Source
            If oGroup.Ord <> i Then
                oGroup.Ord = i
                Await FEB2.ECITransmGroup.Update(oGroup, exs)
            End If
        Next
        If exs.Count <> 0 Then UIHelper.WarnError(exs)
    End Function

    Private Sub DataGridView1_DragOver(ByVal sender As Object, ByVal e As DragEventArgs) Handles Me.DragOver
        e.Effect = DragDropEffects.Move
    End Sub

    Private Sub DataGridView1_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Me.MouseDown
        fromIndex = MyBase.HitTest(e.X, e.Y).RowIndex
        If fromIndex > -1 Then
            Dim dragSize As Size = SystemInformation.DragSize
            dragRect = New Rectangle(New Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize)
        Else
            dragRect = Rectangle.Empty
        End If
    End Sub

    Private Sub DataGridView1_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Me.MouseMove
        If (e.Button And MouseButtons.Left) = MouseButtons.Left Then
            If (dragRect <> Rectangle.Empty AndAlso Not dragRect.Contains(e.X, e.Y)) Then
                MyBase.DoDragDrop(MyBase.Rows(fromIndex), DragDropEffects.Move)
            End If
        End If
    End Sub
#End Region

    Protected Class ControlItem
        Property Source As DTOECITransmGroup

        Property Nom As String

        Public Sub New(value As DTOECITransmGroup)
            MyBase.New()
            _Source = value
            With value
                _Nom = .Nom
            End With
        End Sub

        Public Sub New() 'obligatori per editable grid
            MyBase.New
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


