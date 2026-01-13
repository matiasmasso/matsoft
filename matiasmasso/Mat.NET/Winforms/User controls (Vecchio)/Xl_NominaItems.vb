Public Class Xl_NominaItems
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Private Enum Cols
        Cod
        Concepte
        Qty
        Preu
        Import
    End Enum

    Public Shadows Sub Load(items As List(Of DTONomina.Item))
        _ControlItems = New ControlItems
        If items IsNot Nothing Then
            For Each oItem As DTONomina.Item In items
                Dim oControlItem As New ControlItem(oItem)
                _ControlItems.Add(oControlItem)
            Next
        End If
        LoadGrid()
    End Sub


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
            With .Columns(Cols.Cod)
                .HeaderText = "Codi"
                .DataPropertyName = "Cod"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Concepte)
                .HeaderText = "Concepte"
                .DataPropertyName = "Concepte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Qty)
                .HeaderText = "Unitats"
                .DataPropertyName = "Qty"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###;-#,###;#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Preu)
                .HeaderText = "Preu"
                .DataPropertyName = "Preu"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Import)
                .HeaderText = "Import"
                .DataPropertyName = "Import"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
        End With
        'SetContextMenu()
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

    Private Function SelectedItems() As List(Of DTONomina.Item)
        Dim retval As New List(Of DTONomina.Item)
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

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            'SetContextMenu()
        End If
    End Sub

    Protected Class ControlItem
        Public Property Source As DTONomina.Item

        Public Property Cod As Integer
        Public Property Concepte As String
        Public Property Qty As Integer
        Public Property Preu As Decimal
        Public Property Import As Decimal

        Public Sub New(oNominaItem As DTONomina.Item)
            MyBase.New()
            _Source = oNominaItem
            With oNominaItem
                _Cod = .Concepte.Id
                _Concepte = .Concepte.Name
                _Qty = .Qty
                If .Price IsNot Nothing Then
                    _Preu = .Price.eur
                    _Import = .Import.eur
                End If
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class
