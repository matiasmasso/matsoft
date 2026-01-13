Public Class Xl_FFRegistres
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Idx
        Len
        Line
    End Enum

    Public Shadows Sub Load(value As FFRegistres)
        _ControlItems = New ControlItems
        For Each oItem As FFRegistre In value
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
    End Sub

    Public ReadOnly Property Value As FFRegistre
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As FFRegistre = oControlItem.Source
            Return retval
        End Get
    End Property


    Private Sub LoadGrid()
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With

            .AutoGenerateColumns = False
            .Columns.Clear()

            .DataSource = _ControlItems
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Idx)
                .HeaderText = "Index"
                .DataPropertyName = "Idx"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Len)
                .HeaderText = "Len"
                .DataPropertyName = "Len"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Line)
                .HeaderText = "Registre"
                .DataPropertyName = "Line"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function SelectedControlItems() As ControlItems
        Dim retval As New ControlItems
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem)
        Return retval
    End Function

    Private Function SelectedItems() As FFRegistres
        Dim retval As New FFRegistres
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem.Source)
        Return retval
    End Function

    Private Function CurrentControlItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentControlItem()

        If oControlItem IsNot Nothing Then
            'Dim oMenu_FFRegistre As New Menu_FFRegistre(SelectedItems.First)
            'oContextMenu.Items.AddRange(oMenu_FFRegistre.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim oFFRegistre As FFRegistre = oControlItem.Source
            RaiseEvent ValueChanged(Me, New MatEventArgs(oFFRegistre))
        End If
    End Sub

    Protected Class ControlItem
        Public Property Source As FFRegistre

        Public Property Idx As Integer
        Public Property Len As Integer
        Public Property Line As String

        Public Sub New(oFFRegistre As FFRegistre)
            MyBase.New()
            _Source = oFFRegistre
            Static iIdx As Integer
            iIdx += 1

            With oFFRegistre
                _Idx = iIdx
                _Line = .ToString
                _Len = _Line.Length
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

