Public Class Xl_PgcGeos
    Inherits DataGridView

    Private _Values As List(Of DTOPgcGeo)
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Private Enum Cols
        CtaId
        CtaNom
        Tot
        CEE
        Esp
        CCAA
    End Enum

    Public Shadows Sub Load(values As List(Of DTOPgcGeo))
        _Values = values
        _ControlItems = New ControlItems
        For Each item As DTOPgcGeo In _Values
            Dim oControlItem As New ControlItem(item)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
    End Sub


    Private Sub LoadGrid()
        _AllowEvents = False
        Static Done As Boolean

        If Not Done Then
            MyBase.AutoGenerateColumns = False
            With MyBase.RowTemplate
                .Height = MyBase.Font.Height * 1.3
            End With
            MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            MyBase.ColumnHeadersVisible = True
            MyBase.RowHeadersVisible = False
            MyBase.MultiSelect = False
            MyBase.AllowUserToResizeRows = False
            MyBase.ReadOnly = True
            MyBase.Columns.Clear()
            MyBase.Columns.Add(New DataGridViewTextBoxColumn)
            With MyBase.Columns(Cols.CtaId)
                .HeaderText = "compte"
                .DataPropertyName = "CtaId"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 60
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            MyBase.Columns.Add(New DataGridViewTextBoxColumn)
            With MyBase.Columns(Cols.CtaNom)
                .HeaderText = ""
                .DataPropertyName = "CtaNom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            MyBase.Columns.Add(New DataGridViewTextBoxColumn)
            With MyBase.Columns(Cols.Tot)
                .HeaderText = "Totals"
                .DataPropertyName = "Tot"
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            MyBase.Columns.Add(New DataGridViewTextBoxColumn)
            With MyBase.Columns(Cols.CEE)
                .HeaderText = "CEE"
                .DataPropertyName = "CEE"
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            MyBase.Columns.Add(New DataGridViewTextBoxColumn)
            With MyBase.Columns(Cols.Esp)
                .HeaderText = "Espanya"
                .DataPropertyName = "Esp"
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            MyBase.Columns.Add(New DataGridViewTextBoxColumn)
            With MyBase.Columns(Cols.CCAA)
                .HeaderText = "CCAA"
                .DataPropertyName = "CCAA"
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            Done = True
        End If

        MyBase.DataSource = _ControlItems

        SetContextMenu()
        _AllowEvents = True
    End Sub


    Private Function CurrentControlItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = MyBase.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentControlItem()
        If oControlItem IsNot Nothing Then
            'Dim item As DTOPgcGeo = oControlItem.Source
            'Dim oMenu As New Menu_PurchaseOrderItem(item)
            'oContextMenu.Items.AddRange(oMenu.Range)
        End If
        oContextMenu.Items.Add("Excel", My.Resources.Excel, AddressOf Do_Excel)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_Excel()
        Dim oSheet As DTOExcelSheet = BLL.BLLPgcGeos.ExcelSheet(_Values)
        UIHelper.ShowExcel(oSheet)
    End Sub

    Private Sub ControlSelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then SetContextMenu()
    End Sub

    Protected Class ControlItem
        Property Source As DTOPgcGeo

        Property CtaId As String
        Property CtaNom As String
        Property Tot As Decimal
        Property CEE As Decimal
        Property Esp As Decimal
        Property CCAA As Decimal

        Public Sub New(item As DTOPgcGeo)
            MyBase.New()
            _Source = item
            With item
                _CtaId = .CtaId
                _CtaNom = .CtaNom
                _Tot = .Tot
                _CEE = .CEE
                _Esp = .Esp
                _CCAA = .CCAA
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


