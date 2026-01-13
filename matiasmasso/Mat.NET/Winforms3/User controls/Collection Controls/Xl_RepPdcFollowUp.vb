Public Class Xl_RepPdcFollowUp

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOPurchaseOrderItem)
    Private _Value As DTOPurchaseOrderItem

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event OnItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Sku
        Qty
        Preu
        Dto
        PdcAmt
        AlbAmt
        FraAmt
        LiqAmt
    End Enum

    Public Shadows Sub Load(values As List(Of DTOPurchaseOrderItem))
        _Values = values

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        RefrescaPdc()
    End Sub

    Public Shadows Sub Load(value As DTOPurchaseOrderItem)
        _Value = value

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        RefrescaPnc()
    End Sub

    Private Sub RefrescaPdc()
        _AllowEvents = False

        Dim oFilteredValues As List(Of DTOPurchaseOrderItem) = FilteredValues()
        MyBase.Columns(Cols.Dto).Visible = oFilteredValues.Any(Function(x) x.Dto <> 0)

        _ControlItems = New ControlItems
        Dim oSummary As New ControlItem(ControlItem.LinCods.Tot, "Totals")
        _ControlItems.Add(oSummary)
        For Each oItem As DTOPurchaseOrderItem In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            With oSummary
                .PdcAmt += oControlItem.PdcAmt
                .AlbAmt += oControlItem.AlbAmt
                .FraAmt += oControlItem.FraAmt
                .LiqAmt += oControlItem.LiqAmt
            End With
            _ControlItems.Add(oControlItem)
        Next

        MyBase.DataSource = _ControlItems

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Sub RefrescaPnc()
        _AllowEvents = False

        _ControlItems = New ControlItems
        Dim oSummary As New ControlItem(ControlItem.LinCods.Tot, "Totals")
        _ControlItems.Add(oSummary)

        Dim oControlItem As New ControlItem(ControlItem.LinCods.Pdc, _Value.PurchaseOrder.Caption(DTOLang.ESP))
        oControlItem.Source = _Value.PurchaseOrder
        oControlItem.PdcAmt = _Value.Amount.Eur
        oSummary.PdcAmt += _Value.Amount.Eur
        _ControlItems.Add(oControlItem)

        Dim oLiq As New DTORepLiq
        Dim oFra As New DTOInvoice
        For Each oArc In _Value.Deliveries
            If Not oArc.Delivery.Invoice Is Nothing Then
                If Not oArc.Delivery.Invoice.Equals(oFra) Then
                    oFra = oArc.Delivery.Invoice
                    oControlItem = New ControlItem(ControlItem.LinCods.Fra, String.Format("Factura {0} del {1:dd/MM/yy}", oFra.Num, oFra.Fch))
                    oControlItem.Source = oFra
                    oControlItem.FraAmt += oArc.Import.Eur
                    oSummary.FraAmt += oControlItem.FraAmt
                    _ControlItems.Add(oControlItem)
                End If
                If oArc.RepLiq IsNot Nothing Then
                    If Not oArc.RepLiq.Equals(oLiq) Then
                        oLiq = oArc.RepLiq
                        oControlItem = New ControlItem(ControlItem.LinCods.Liq, String.Format("Liquidació {0} del {1:dd/MM/yy}", oLiq.Id, oLiq.Fch))
                        oControlItem.Source = oLiq
                        oControlItem.LiqAmt += oArc.Import.Eur
                        oSummary.LiqAmt += oControlItem.LiqAmt
                        _ControlItems.Add(oControlItem)
                    End If
                End If
            End If

            Dim oAlb As New ControlItem(ControlItem.LinCods.Alb, String.Format("Albarà {0} del {1:dd/MM/yy}", oArc.Delivery.Id, oArc.Delivery.Fch))
            oAlb.Source = oArc.Delivery
            oAlb.Qty = oArc.Qty
            oAlb.Preu = oArc.Price.Eur
            oAlb.Dto = oArc.Dto
            oAlb.AlbAmt = oArc.Import.Eur
            _ControlItems.Add(oAlb)
            oSummary.AlbAmt += oAlb.AlbAmt
        Next

        MyBase.DataSource = _ControlItems

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOPurchaseOrderItem)
        Dim retval As List(Of DTOPurchaseOrderItem)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.sku.nom.Contains(_Filter.ToLower))
        End If
        Return retval
    End Function


    Public Property Filter As String
        Get
            Return _Filter
        End Get
        Set(value As String)
            _Filter = value
            'If _Values IsNot Nothing Then Refresca()
        End Set
    End Property

    Public Sub ClearFilter()
        If _Filter > "" Then
            _Filter = ""
            'Refresca()
        End If
    End Sub

    Public ReadOnly Property Value As DTOPurchaseOrderItem
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOPurchaseOrderItem = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowPurchaseOrderItem.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.Sku)
            .HeaderText = "Producte"
            .DataPropertyName = "Sku"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Qty)
            .HeaderText = "Quant"
            .DataPropertyName = "Qty"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Preu)
            .HeaderText = "Preu"
            .DataPropertyName = "Preu"
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
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#.#\%;-#.#\%;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.PdcAmt)
            .HeaderText = "Demanat"
            .DataPropertyName = "PdcAmt"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.AlbAmt)
            .HeaderText = "Servit"
            .DataPropertyName = "AlbAmt"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.FraAmt)
            .HeaderText = "Facturat"
            .DataPropertyName = "FraAmt"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.LiqAmt)
            .HeaderText = "Liquidat"
            .DataPropertyName = "LiqAmt"
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

    Private Function SelectedItems() As List(Of DTOPurchaseOrderItem)
        Dim retval As New List(Of DTOPurchaseOrderItem)
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            If oControlItem.Source IsNot Nothing Then 'descarta el summary row
                retval.Add(oControlItem.Source)
            End If
        Next

        If retval.Count = 0 And CurrentControlItem.Source IsNot Nothing Then
            retval.Add(CurrentControlItem.Source)
        End If
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
            Select Case oControlItem.LinCod
                Case ControlItem.LinCods.Pnc
                    Dim oMenu_PurchaseOrderItem As New Menu_PurchaseOrderItem(DirectCast(oControlItem.Source, DTOPurchaseOrderItem))
                    oContextMenu.Items.AddRange(oMenu_PurchaseOrderItem.Range)
                Case ControlItem.LinCods.Pdc
                    Dim oMenu_PurchaseOrder As New Menu_PurchaseOrder(DirectCast(oControlItem.Source, DTOPurchaseOrder))
                    oContextMenu.Items.AddRange(oMenu_PurchaseOrder.Range)
                Case ControlItem.LinCods.Alb
                    Dim oDeliveries As List(Of DTODelivery) = {DirectCast(oControlItem.Source, DTODelivery)}.ToList
                    Dim oMenu_Delivery As New Menu_Delivery(oDeliveries)
                    oContextMenu.Items.AddRange(oMenu_Delivery.Range)
                Case ControlItem.LinCods.Fra
                    Dim oInvoices = {DirectCast(oControlItem.Source, DTOInvoice)}.ToList
                    Dim oMenu_Invoice As New Menu_Invoice(oInvoices)
                    oContextMenu.Items.AddRange(oMenu_Invoice.Range)
                Case ControlItem.LinCods.Liq
                    Dim oMenu_RepLiq As New Menu_RepLiq(DirectCast(oControlItem.Source, DTORepLiq))
                    oContextMenu.Items.AddRange(oMenu_RepLiq.Range)
            End Select
            'oContextMenu.Items.Add("-")
        End If
        'oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOPurchaseOrderItem = CurrentControlItem.Source
            Dim oFrm As New Frm_RepPdcFollowUp(oSelectedValue)
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
        Property Source As Object
        Property LinCod As LinCods
        Property Sku As String
        Property Qty As Integer
        Property Preu As Decimal
        Property Dto As Decimal
        Property PdcAmt As Decimal
        Property AlbAmt As Decimal
        Property FraAmt As Decimal
        Property LiqAmt As Decimal

        Public Enum LinCods
            Tot
            Pnc
            Pdc
            Alb
            Fra
            Liq
        End Enum

        Public Sub New(oLinCod As LinCods, caption As String)
            MyBase.New
            _LinCod = oLinCod
            _Sku = caption
        End Sub

        Public Sub New(value As DTOPurchaseOrderItem)
            MyBase.New()
            _Source = value
            With value
                _LinCod = LinCods.Pnc
                _Sku = .sku.nomLlarg.Tradueix(Current.Session.Lang)
                _Qty = .Qty
                _Preu = .Price.Eur
                _Dto = .Dto
                _PdcAmt = .Amount.Eur
                _AlbAmt = .Deliveries?.Sum(Function(x) x.Import.Eur)
                _FraAmt = .Deliveries?.Where(Function(x) x.Delivery.Invoice IsNot Nothing).Sum(Function(y) y.Import.Eur)
                _LiqAmt = .Deliveries?.Where(Function(x) x.RepLiq IsNot Nothing).Sum(Function(y) y.Import.Eur)
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


