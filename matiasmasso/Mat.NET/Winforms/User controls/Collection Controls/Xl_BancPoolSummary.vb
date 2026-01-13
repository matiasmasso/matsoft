Public Class Xl_BancPoolSummary
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOBancPool)
    Private _DefaultValue As DTOBancPool
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Nom
        Tot
        Ct1
        Ct2
        Ct3
        Ct4
    End Enum

    Public Shadows Sub Load(values As List(Of DTOBancPool), Optional oDefaultValue As DTOBancPool = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        _SelectionMode = oSelectionMode
        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        _ControlItems = New ControlItems
        Dim oControlTotals As New ControlItem()
        Dim oControlItem As New ControlItem(New DTOBank)
        For Each oItem As DTOBancPool In _Values
            If oItem.Bank.UnEquals(oControlItem.Source) Then
                oControlItem = New ControlItem(oItem.Bank)
                _ControlItems.Add(oControlItem)
            End If
            Select Case oItem.ProductCategory
                Case DTOBancPool.ProductCategories.Credit_Pur
                    oControlItem.Category1 += oItem.Amt.Eur
                    oControlTotals.Category1 += oItem.Amt.Eur
                Case DTOBancPool.ProductCategories.Credit_Comercial
                    oControlItem.Category2 += oItem.Amt.Eur
                    oControlTotals.Category2 += oItem.Amt.Eur
                Case DTOBancPool.ProductCategories.Prestecs
                    oControlItem.Category3 += oItem.Amt.Eur
                    oControlTotals.Category3 += oItem.Amt.Eur
                Case DTOBancPool.ProductCategories.Avals
                    oControlItem.Category4 += oItem.Amt.Eur
                    oControlTotals.Category4 += oItem.Amt.Eur
            End Select
        Next
        _ControlItems.Add(oControlTotals)

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub


    Public ReadOnly Property Value As DTOBank
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOBank = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.DataSource = _ControlItems
        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Nom"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Tot)
            .HeaderText = "Totals"
            .DataPropertyName = "Totals"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Ct1)
            .HeaderText = "Credit pur"
            .DataPropertyName = "Category1"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Ct2)
            .HeaderText = "Credit comercial"
            .DataPropertyName = "Category2"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Ct3)
            .HeaderText = "Prestecs"
            .DataPropertyName = "Category3"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Ct4)
            .HeaderText = "Avals"
            .DataPropertyName = "Category4"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
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

    Private Function SelectedItems() As List(Of DTOBank)
        Dim retval As New List(Of DTOBank)
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
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentControlItem()

        If oControlItem IsNot Nothing Then
            oContextMenu.Items.Add("extracte", Nothing, AddressOf Do_Extracte)
            Dim oMenuItem As New ToolStripMenuItem("entitat")
            oContextMenu.Items.Add(oMenuItem)

            Dim oMenu_Bank As New Menu_Bank(SelectedItems.First)
            AddHandler oMenu_Bank.AfterUpdate, AddressOf RefreshRequest
            oMenuItem.DropDownItems.AddRange(oMenu_Bank.Range)
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_Extracte()
        Dim oBank As DTOBank = CurrentControlItem.Source
        Dim oFrm As New Frm_BancPoolExtracte(oBank)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOBank = CurrentControlItem.Source
            Dim oFrm As New Frm_BancPoolExtracte(oSelectedValue)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()

        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub



    Protected Class ControlItem
        Property Source As DTOBank

        Property Nom As String
        Property Category1 As Decimal
        Property Category2 As Decimal
        Property Category3 As Decimal
        Property Category4 As Decimal
        ReadOnly Property Totals As Decimal
            Get
                Dim retval As Decimal = Category1 + Category2 + Category3 + Category4
                Return retval
            End Get
        End Property
        Public Sub New(Optional value As DTOBank = Nothing)
            MyBase.New()
            _Source = value
            If value Is Nothing Then
                _Nom = "totals"
            Else
                _Nom = value.RaoSocial
            End If
        End Sub



    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


