Public Class Xl_Pnds_Selection
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Private Enum Cols
        Check
        Amt
        Vto
        Client
    End Enum


    Public WriteOnly Property DataSource As Pnds
        Set(value As Pnds)
            _ControlItems = New ControlItems
            For Each oItem As Pnd In value
                Dim oControlItem As New ControlItem(oItem)
                _ControlItems.Add(oControlItem)
            Next
            LoadGrid()
        End Set
    End Property

    Public ReadOnly Property SelectedValues As Pnds
        Get
            Dim retval As New Pnds
            For Each oItem As ControlItem In _ControlItems
                If oItem.Checked Then
                    retval.Add(oItem.Source)
                End If
            Next
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
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True

            .Columns.Add(New DataGridViewImageColumn)
            With CType(.Columns(Cols.Check), DataGridViewImageColumn)
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 16
                .DefaultCellStyle.NullValue = Nothing
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Vto)
                .DataPropertyName = "Vto"
                .HeaderText = "venciment"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Amt)
                .DataPropertyName = "Amt"
                .HeaderText = "import"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Client)
                .DataPropertyName = "Fra"
                .HeaderText = "factura"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Vto)
                .DataPropertyName = "Fch"
                .HeaderText = "data"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Client)
                .DataPropertyName = "Client"
                .HeaderText = "client"
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

    Private Function SelectedItems() As Pnds
        Dim retval As New Pnds
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
            'Dim oMenu_RepComLiquidable As New Menu_Pnd(SelectedItems)
            'oContextMenu.Items.AddRange(oMenu_RepComLiquidable.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Check
                Dim oControlItem As ControlItem = DataGridView1.Rows(e.RowIndex).DataBoundItem
                If oControlItem.Checked = True Then
                    e.Value = My.Resources.Checked13
                Else
                    e.Value = My.Resources.UnChecked13
                End If
        End Select
    End Sub

    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        Select Case e.ColumnIndex
            Case Cols.Check
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                oControlItem.Checked = Not oControlItem.Checked
                DataGridView1.Refresh()
        End Select
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub


    Protected Class ControlItem
        Public Property Source As Pnd

        Public Property Checked As Boolean
        Public Property Ico As Image = Nothing
        Public Property Nom As String
        Public Property Devengat As Decimal
        Public Property Dietas As Decimal
        Public Property SegSoc As Decimal
        Public Property Irpf As Decimal
        Public Property Embargos As Decimal
        Public Property Deutes As Decimal
        Public Property Liquid As Decimal

        Public Sub New(oPnd As Pnd)
            MyBase.New()
            _Source = oPnd
            _Checked = True

            With oPnd
                ' _Nom = .Staff.NomAlias

            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits System.ComponentModel.BindingList(Of ControlItem)
    End Class


End Class
