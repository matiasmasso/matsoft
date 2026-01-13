Public Class Xl_CustomerPmcs
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTODeliveryItem)

    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Private Enum Cols
        Alb
        Fch
        Brand
        Category
        Sku
        Qty
        Eur
        Dto
        Net
        Pmc
        Pmx
        Mrg
        Bnf
    End Enum

    Public Shadows Sub Load(values As List(Of DTODeliveryItem))
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
        Dim oFilteredValues As List(Of DTODeliveryItem) = _Values

        _ControlItems = New ControlItems

        Dim oTotal As New ControlItem()
        oTotal.Sku = "totals"
        _ControlItems.Add(oTotal)

        For Each oItem As DTODeliveryItem In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
            oTotal.addToTotal(oItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub



    Public ReadOnly Property Value As DTODeliveryItem
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTODeliveryItem = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowDeliveryItem.DefaultCellStyle.BackColor = Color.Transparent

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Alb)
            .HeaderText = "Albará"
            .DataPropertyName = "Alb"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Fch)
            .HeaderText = "Data"
            .DataPropertyName = "Fch"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Brand)
            .HeaderText = "Marca comercial"
            .DataPropertyName = "Brand"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Category)
            .HeaderText = "Categoría"
            .DataPropertyName = "Category"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Sku)
            .HeaderText = "Producte"
            .DataPropertyName = "Sku"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Qty)
            .HeaderText = "Quant"
            .DataPropertyName = "Qty"
            .Width = 50
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Eur)
            .HeaderText = "Preu"
            .DataPropertyName = "Eur"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Dto)
            .HeaderText = "Dto"
            .DataPropertyName = "Dto"
            .Width = 50
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#.#\%;-#.#\%;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Net)
            .HeaderText = "Net"
            .DataPropertyName = "Net"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Pmc)
            .HeaderText = "Cost mig"
            .DataPropertyName = "Pmc"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Pmx)
            .HeaderText = "Cost"
            .DataPropertyName = "Pmx"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Mrg)
            .HeaderText = "Marge"
            .DataPropertyName = "Mrg"
            .Width = 50
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#\%;-#\%;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Bnf)
            .HeaderText = "Benefici"
            .DataPropertyName = "Bnf"
            .Width = 70
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

    Private Function SelectedItems() As List(Of DTODeliveryItem)
        Dim retval As New List(Of DTODeliveryItem)
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
            If oControlItem.Source IsNot Nothing Then
                Dim oMenu_DeliveryItem As New Menu_DeliveryItem(SelectedItems.First)
                AddHandler oMenu_DeliveryItem.AfterUpdate, AddressOf RefreshRequest
                oContextMenu.Items.AddRange(oMenu_DeliveryItem.Range)
                oContextMenu.Items.Add("-")
            End If
        End If
        oContextMenu.Items.Add("Excel", My.Resources.Excel, AddressOf Do_Excel)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub


    Private Async Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTODeliveryItem = CurrentControlItem.Source
            Dim oDelivery As DTODelivery = oSelectedValue.Delivery
            Dim oCustomer As DTOCustomer = oDelivery.Contact
            Dim exs As New List(Of Exception)
            If Await FEB2.AlbBloqueig.BloqueigStart(Current.Session.User, oCustomer, DTOAlbBloqueig.Codis.ALB, exs) Then
                Dim oFrm As New Frm_AlbNew2(oDelivery)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
            Else
                UIHelper.WarnError(exs)
            End If


        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub Do_Excel()
        Dim oSheet As New MatHelperStd.ExcelHelper.Sheet("Customer PMC")
        For Each oControlItem As ControlItem In _ControlItems
            Dim oRow = oSheet.AddRow()
            With oControlItem
                oRow.AddCell(.Alb)
                oRow.AddCell(.Fch)
                oRow.AddCell(.Brand)
                oRow.AddCell(.Category)
                oRow.AddCell(.Sku)
                oRow.AddCell(.Qty)
                oRow.AddCell(.Eur)
                oRow.AddCell(.Dto)
                oRow.AddCell(.Net)
                oRow.AddCell(.Pmc)
                oRow.AddCell(.Mrg)
                oRow.AddCell(.Bnf)
            End With
        Next
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Protected Class ControlItem
        Property Source As DTODeliveryItem

        Property Alb As Integer
        Property Fch As Date
        Property Brand As String
        Property Category As String
        Property Sku As String
        Property Qty As Integer
        Property Eur As Decimal
        Property Dto As Decimal
        Property Net As Decimal
        Property Pmc As Decimal
        Property Pmx As Decimal
        Property Mrg As Decimal
        Property Bnf As Decimal


        Public Sub New(value As DTODeliveryItem)
            MyBase.New()
            _Source = value

            Dim oPrice As DTOAmt = value.Price.Clone
            Dim iQty As Integer = value.Qty
            Dim DcEur As Decimal = oPrice.Eur
            Dim DcDto As Decimal = value.Dto
            Dim oDto As DTOAmt = oPrice.Percent(DcDto)
            Dim oNet As DTOAmt = DTOAmt.Import(iQty, oPrice, DcDto)
            Dim DcNet As Decimal = oNet.Eur
            Dim DcPmc As Decimal = value.Pmc
            Dim DcPmx As Decimal = iQty * DcPmc

            Dim DcMrg As Decimal
            If DcPmx <> 0 Then
                DcMrg = 100 * (DcNet - DcPmx) / DcPmx
            End If
            Dim DcBnf As Decimal = DcNet - DcPmx


            With value
                _Alb = .Delivery.Id
                _Fch = .Delivery.fch
                _Brand = .sku.category.brand.nom.Tradueix(Current.Session.Lang)
                _Category = .sku.category.nom.Tradueix(Current.Session.Lang)
                _Sku = .sku.nom.Tradueix(Current.Session.Lang)
                _Qty = iQty
                _Eur = DcEur
                _Dto = DcDto
                _Net = DcNet
                _Pmc = DcPmc
                _Pmx = DcPmx
                _Mrg = DcMrg
                _Bnf = DcBnf
            End With
        End Sub

        Public Sub New()
            MyBase.New
        End Sub

        Public Sub AddToTotal(value As DTODeliveryItem)
            Dim oPrice As DTOAmt = value.Price.Clone
            Dim iQty As Integer = value.Qty
            Dim DcEur As Decimal = oPrice.Eur
            Dim DcDto As Decimal = value.Dto
            Dim oDto As DTOAmt = oPrice.Percent(DcDto)
            Dim oNet As DTOAmt = DTOAmt.Import(iQty, oPrice, DcDto)
            Dim DcNet As Decimal = oNet.Eur
            Dim DcPmc As Decimal = value.Pmc
            Dim DcPmx As Decimal = iQty * DcPmc
            Dim DcBnf As Decimal = DcNet - DcPmx

            _Net += DcNet
            _Pmx += DcPmx
            _Bnf += DcBnf

            If _Pmx <> 0 Then
                _Mrg = 100 * (_Net - _Pmx) / _Pmx
            End If
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

