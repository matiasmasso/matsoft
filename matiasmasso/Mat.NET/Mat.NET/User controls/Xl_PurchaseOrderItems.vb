Public Class Xl_PurchaseOrderItems
    Inherits TabStopDataGridView

    Private _Order As DTOPurchaseOrder
    Private _TarifaDtos As List(Of DTOCustomerTarifaDto)
    Private _ControlItems As ControlItems
    Private _SkuWithsToAppend As New List(Of DTOSkuWith)
    Private _SkipMinPack As Boolean
    Private _AllowEvents As Boolean

    Property Fch As Date

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Cols
        SkuNom
        Qty
        Ico
        Price
        Discount
        Amt
        Pending
        Stock
        Customers
        Previsio
        Rep
        Com
    End Enum

    Public Shadows Sub Load(oOrder As DTOPurchaseOrder)
        _Order = oOrder
        _Fch = oOrder.Fch
        _TarifaDtos = _Order.Customer.Ccx.TarifaDtos
        LoadGrid()
        refresca()
    End Sub

    Private Sub refresca()
        _ControlItems = New ControlItems
        For Each item As DTOPurchaseOrderItem In _Order.Items
            Dim oControlItem As New ControlItem(item)
            _ControlItems.Add(oControlItem)
        Next

        _AllowEvents = False
        Dim oBindingSource As New BindingSource
        oBindingSource.AllowNew = True
        oBindingSource.DataSource = _ControlItems
        MyBase.DataSource = oBindingSource
        _AllowEvents = True
    End Sub

    Public ReadOnly Property Items As List(Of DTOPurchaseOrderItem)
        Get
            Dim retval As New List(Of DTOPurchaseOrderItem)
            For Each oControlItem As ControlItem In _ControlItems
                Dim item As DTOPurchaseOrderItem = oControlItem.Source
                If _Order.IsNew Then
                    If item.Pending <> 0 Then retval.Add(oControlItem.Source)
                Else
                    retval.Add(oControlItem.Source)
                End If
            Next
            Return retval
        End Get
    End Property

    Private Sub LoadGrid()

        MyBase.AutoGenerateColumns = False
        With MyBase.RowTemplate
            .Height = MyBase.Font.Height * 1.3
        End With
        MyBase.SelectionMode = DataGridViewSelectionMode.CellSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = True
        MyBase.MultiSelect = False
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowUserToDeleteRows = True
        MyBase.RowHeadersWidth = 25
        MyBase.ReadOnly = False
        MyBase.Columns.Clear()


        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.SkuNom)
            .HeaderText = "concepte"
            .DataPropertyName = "SkuNom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Qty)
            .HeaderText = "quant"
            .DataPropertyName = "Qty"
            .Width = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "#,###"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .ReadOnly = False
        End With
        MyBase.Columns.Add(New DataGridViewImageColumn)
        With CType(MyBase.Columns(Cols.Ico), DataGridViewImageColumn)
            .HeaderText = ""
            .ReadOnly = True
            .Width = 18
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.NullValue = Nothing
            .DefaultCellStyle.DataSourceNullValue = Nothing
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Price)
            .HeaderText = "Preu"
            .DataPropertyName = "Price"
            .ReadOnly = True
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Discount)
            .HeaderText = "dte"
            .DataPropertyName = "discount"
            .Width = 35
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "#\%;-#\%;#"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .ReadOnly = True
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Amt)
            .HeaderText = "import"
            .DataPropertyName = "Amt"
            .ReadOnly = True
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            '.DefaultCellStyle.BackColor = BCOLORBLANK
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Pending)
            .HeaderText = "pendent"
            .DataPropertyName = "Pending"
            .ReadOnly = True
            .Visible = Not _Order.IsNew
            .Width = 50
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "#,###"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            '.DefaultCellStyle.BackColor = BCOLORBLANK
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Stock)
            .HeaderText = "Stock"
            .DataPropertyName = "Stock"
            .ReadOnly = True
            .Width = 40
            .DefaultCellStyle.Format = "#,###"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            '.DefaultCellStyle.BackColor = BCOLORBLANK
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Customers)
            .HeaderText = "Clients"
            .DataPropertyName = "Customers"
            .ReadOnly = True
            .Width = 40
            .DefaultCellStyle.Format = "#,###"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            '.DefaultCellStyle.BackColor = BCOLORBLANK
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Previsio)
            .HeaderText = "Previsio"
            .DataPropertyName = "Previsio"
            .ReadOnly = True
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "#,###"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            '.DefaultCellStyle.BackColor = BCOLORBLANK
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Rep)
            .HeaderText = "representant"
            .DataPropertyName = "Rep"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .ReadOnly = True
            .Visible = False
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Com)
            .HeaderText = "Comisió"
            .DataPropertyName = "Com"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "#\%;-#\%;#"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .ReadOnly = True
            .Visible = False
        End With

        SetContextMenu()
        MyBase.Refresh()
    End Sub


#Region "Validation"

    Private Sub MatDataGridView1_CellValidated(sender As Object, e As DataGridViewCellEventArgs) Handles MyBase.CellValidated
        If _AllowEvents Then

            Select Case e.ColumnIndex
                Case Cols.Qty
                    Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                    'oRow.Cells(Cols.Qty).Style.BackColor = GetStockBackColor(e.RowIndex)

                Case Cols.SkuNom
                    Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                    If oRow IsNot Nothing Then
                        Dim oControlItem As ControlItem = oRow.DataBoundItem
                        If oControlItem IsNot Nothing Then
                            Dim item As DTOPurchaseOrderItem = oControlItem.Source



                            'oRow.Cells(Cols.SkuNom).Style.BackColor = GetStockBackColor(e.RowIndex)

                            'If GuidHelper.IsGuid(oRow.Cells(Cols.ArtGuid).Value) Then
                            ' Dim oArt As Art = GetArtFromDataGridViewRow(oRow)
                            ' If oArt.Obsoleto Then
                            ' oRow.Cells(Cols.ArtNom).Style.BackColor = Color.LightGray
                            ' End If
                            ' End If

                        End If

                    End If
            End Select

        End If
    End Sub

    Private Sub MatDataGridView1_CellValidating(ByVal sender As Object, ByVal e As DataGridViewCellValidatingEventArgs) Handles MyBase.CellValidating
        If _AllowEvents Then
            Dim exs As New List(Of Exception)

            Select Case e.ColumnIndex
                Case Cols.Qty
                    ValidateQty(e, exs)

                Case Cols.SkuNom
                    'ValidateSkuNom(e, exs)

            End Select

            If exs.Count > 0 Then
                UIHelper.WarnError(exs)
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                oRow.Cells(e.ColumnIndex).Selected = True
                e.Cancel = True
            End If
        End If
    End Sub

    Private Function ValidateQty(e As DataGridViewCellValidatingEventArgs, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        If oRow Is Nothing Then
            retval = True
        Else
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            Dim item As DTOPurchaseOrderItem = oControlItem.Source

            If e.FormattedValue = "" Then
                retval = True
            ElseIf BLL.BLLProduct.AllowUserToFraccionarInnerPack(item.Sku, BLL.BLLSession.Current.User) Then
                retval = True
            Else
                If IsNumeric(e.FormattedValue) Then
                    Dim iNewQty As Integer = e.FormattedValue
                    CheckMinPack(item, iNewQty, exs)
                    CheckMaxUnits(item, iNewQty, exs)
                    retval = (exs.Count = 0)
                Else
                    exs.Add(New InvalidCastException("la quantitat ha de ser un valor numéric"))
                End If
            End If
        End If
        Return retval
    End Function

    Private Function CheckMaxUnits(item As DTOPurchaseOrderItem, ByVal iNewQty As Integer, exs As List(Of Exception)) As Boolean
        Dim retVal As Boolean = True
        Dim oSku As DTOProductSku = item.Sku

        If oSku IsNot Nothing Then
            If oSku.LastProduction Then
                If iNewQty <> item.Qty Then
                    Dim iMax As Integer = BLL.BLLProductSku.LastProductionAvailableUnits(oSku)
                    If iMax < 0 Then iMax = 0
                    Dim iExtraQtyRequested As Integer = iNewQty - item.Qty
                    If iExtraQtyRequested > iMax Then
                        If iMax > 0 Then
                            exs.Add(New Exception("Article fora de producció. No es poden demanar més de " & iMax.ToString & " unitats"))
                        Else
                            exs.Add(New Exception("Article esgotat i fora de producció. Ja no s'admeten comandes"))
                        End If
                        retVal = False
                    End If
                End If
            End If
        End If

        Return retVal
    End Function

    Private Function CheckMinPack(item As DTOPurchaseOrderItem, ByVal iNewQty As Integer, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        If item.Sku.ForzarInnerPack Then
            If iNewQty Mod item.Sku.InnerPack = 0 Then
                retval = True
            Else
                exs.Add(New Exception("la quantitat ha de ser multiplo de " & item.Sku.InnerPack))
            End If
        Else
            retval = True
        End If
        Return retval
    End Function

    Private Function ValidateSkuNom(e As DataGridViewCellValidatingEventArgs, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)

        'sWarn = CheckPermissionToChangeArt(oRow, e.FormattedValue)
        Return retval
    End Function

#End Region

    Public Function GetTotals() As DTOAmt
        Dim DcBase As Decimal = _ControlItems.Sum(Function(x) x.Amt)
        Dim retval As New DTOAmt(DcBase)
        Return retval
    End Function

    Private Sub SetTotals(oControlItem As ControlItem)
        With oControlItem
            If .Price = 0 Or .Qty = 0 Then
                .Amt = 0
            Else
                .Amt = Math.Round(.Price * .Qty * (100 - .Discount) / 100, 2)
            End If
        End With
        Dim oBase As DTOAmt = GetTotals()
        RaiseEvent AfterUpdate(Me, New MatEventArgs(oBase))
    End Sub

    Private Function IsAllowed(oCustomer As DTOCustomer, oSku As DTOProductSku) As Boolean
        Dim retval As Boolean
        If oCustomer.Rol.IsStaff Then
            retval = True
        ElseIf oCustomer.Rol.Id = DTORol.Ids.Taller Then
            retval = True
        ElseIf oCustomer.Rol.Id = DTORol.Ids.Rep Then
            retval = True
        ElseIf oCustomer.Rol.Id = DTORol.Ids.Manufacturer Then
            retval = True
        ElseIf oCustomer.Rol.Id = DTORol.Ids.Cli Then
            retval = BLL.BLLCliProductBlocked.Allowed(_Order.Customer, oSku)
        Else
            retval = True
        End If
        Return retval
    End Function

    Private Sub MatDataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles MyBase.CellValueChanged
        If _AllowEvents Then
            Select Case e.ColumnIndex
                Case Cols.SkuNom

                    Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                    Dim BlPreviousAllowEvents As Boolean = _AllowEvents
                    Dim sKey As String = oRow.Cells(Cols.SkuNom).Value 'e.FormattedValue

                    Dim oSku As DTOProductSku = Finder.FindSku(sKey, , _Fch)
                    If oSku Is Nothing Then
                        'e.Cancel = True
                        'FeedRowFromArt(oRow, Nothing)
                    ElseIf Not IsAllowed(_Order.Customer, oSku) Then
                        'e.Cancel = True
                        MsgBox("El client té bloquejat aquest article" & vbCrLf & oSku.NomLlarg, MsgBoxStyle.Exclamation, "MAT.NET")
                        'oRow.Cells(Cols.SkuNom).Value = ""
                        'MyBase.CurrentCell = oRow.Cells(Cols.SkuNom)
                    Else
                        Dim oControlItem As ControlItem = oRow.DataBoundItem
                        Dim item As DTOPurchaseOrderItem = oControlItem.Source
                        With item
                            .Sku = oSku
                            Dim oCost As DTOAmt = BLL.BLLPricelistItemCustomer.GetCustomerCost(.Sku, _TarifaDtos, _Fch)
                            If oCost IsNot Nothing Then
                                If _Order.Customer.Ccx.Tarifa = DTOCustomer.Tarifas.FiftyFifty Then
                                    .Price = New DTOAmt(Math.Round(oCost.Eur / 2, 2))
                                Else
                                    .Price = oCost
                                End If
                                .Dto = BLL.BLLPurchaseOrderItem.GetDiscount(.Sku, _Order.Customer.Ccx)
                            End If
                            .RepCom = BLL.BLLRepCom.GetRepCom(_Order.Customer, .Sku, _Fch)
                            _SkuWithsToAppend = BLL.BLLSkuWiths.Find(.Sku, _Fch)
                        End With

                        oControlItem.Load(item)
                        SetTotals(oControlItem)

                    End If
                    MyBase.Refresh()
                    _AllowEvents = BlPreviousAllowEvents

                Case Cols.Qty
                    Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                    Dim oControlItem As ControlItem = oRow.DataBoundItem
                    Dim item As DTOPurchaseOrderItem = oControlItem.Source
                    item.Qty = oControlItem.Qty

                    If _Order.IsNew Then
                        item.Pending = item.Qty
                    Else
                        If item.Lin = 0 Then
                            item.Pending = item.Qty
                        Else
                            Dim iUnitatsSortides As Integer = BLL.BLLPurchaseOrderItem.UnitatsSortides(item)
                            item.Pending = item.Qty - iUnitatsSortides
                        End If
                    End If
                    oControlItem.Pending = item.Pending

                    SetTotals(oControlItem)

                Case Cols.Price
                    Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                    Dim oControlItem As ControlItem = oRow.DataBoundItem
                    Dim item As DTOPurchaseOrderItem = oControlItem.Source
                    With oControlItem
                        item.Price = New DTOAmt(.Price)
                        If .Price <> 0 Then
                            .Amt = Math.Round(.Price * .Qty * (100 - .Discount) / 100, 2)
                        End If
                    End With

                    SetTotals(oControlItem)

                Case Cols.Discount
                    Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                    Dim oControlItem As ControlItem = oRow.DataBoundItem
                    Dim item As DTOPurchaseOrderItem = oControlItem.Source
                    With oControlItem
                        item.Dto = .Discount
                        If .Price <> 0 Then
                            .Amt = Math.Round(.Price * .Qty * (100 - .Discount) / 100, 2)
                        End If
                    End With

                    SetTotals(oControlItem)

                Case Cols.SkuNom

            End Select

        End If
    End Sub

    Private Sub MatDataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles MyBase.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.SkuNom
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                If oControlItem IsNot Nothing Then
                    Dim oItem As DTOPurchaseOrderItem = oControlItem.Source
                    Dim oSku As DTOProductSku = oItem.Sku
                    If oSku IsNot Nothing Then
                        If oSku.Obsoleto Then
                            e.CellStyle.BackColor = Color.LightGray
                            oRow.DefaultCellStyle.SelectionBackColor = Color.Gray
                        End If
                    End If

                End If
            Case Cols.Ico
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                If oRow.IsNewRow Then
                    e.Value = Nothing ' My.Resources.empty
                Else
                    Dim oControlItem As ControlItem = oRow.DataBoundItem
                    If oControlItem IsNot Nothing Then
                        Select Case oControlItem.Ico
                            Case ControlItem.Icons.Empty
                                e.Value = Nothing ' My.Resources.empty
                        End Select
                    End If
                End If
                ' e.Value = GetCellIcon(e.RowIndex)
            Case Cols.Price
                'e.CellStyle.BackColor = GetPvpBackColor(e.RowIndex)
            Case Cols.Qty
                If _Order.IsNew Then
                    Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                    Dim oControlItem As ControlItem = oRow.DataBoundItem
                    If oControlItem IsNot Nothing Then
                        e.CellStyle.BackColor = oControlItem.StockBackColor
                    End If
                End If

            Case Cols.Pending
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                If oControlItem IsNot Nothing Then
                    If oControlItem.Pending > 0 Then
                        e.CellStyle.BackColor = oControlItem.StockBackColor
                    End If
                End If

        End Select
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
            Dim item As DTOPurchaseOrderItem = oControlItem.Source
            Dim oMenu As New Menu_PurchaseOrderItem(item)
            oContextMenu.Items.AddRange(oMenu.Range)
        End If
        oContextMenu.Items.Add("editar preus i descomptes", Nothing, AddressOf Do_EditPrices)
        oContextMenu.Items.Add("vista comisions", Nothing, AddressOf Do_RepComs)
        oContextMenu.Items.Add("reasigna comisions", Nothing, AddressOf Do_RecalcRepComs)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_EditPrices()
        MyBase.Columns(Cols.Price).ReadOnly = False
        MyBase.Columns(Cols.Discount).ReadOnly = False
    End Sub

    Private Sub Do_RepComs(sender As Object, e As System.EventArgs)
        Dim reverse As Boolean = MyBase.Columns(Cols.Rep).Visible = True

        MyBase.Columns(Cols.Pending).Visible = reverse
        MyBase.Columns(Cols.Stock).Visible = reverse
        MyBase.Columns(Cols.Customers).Visible = reverse
        MyBase.Columns(Cols.Previsio).Visible = reverse

        MyBase.Columns(Cols.Rep).Visible = Not reverse
        MyBase.Columns(Cols.Com).Visible = Not reverse
    End Sub

    Private Sub Do_RecalcRepComs(sender As Object, e As System.EventArgs)
        BLL.BLLPurchaseOrderItems.ReasignaComisions(Me.Items)
        refresca()
    End Sub

    Private Sub Xl_PurchaseOrderItems2_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles Me.RowsAdded
        'prevents default empty image from decorating the ico cell when adding new row
        'Dim oLastRowAdded As DataGridViewRow = MyBase.Rows(e.RowIndex + e.RowCount - 1)
        'If oLastRowAdded.Cells.Count > Cols.Ico Then
        'oLastRowAdded.Cells(Cols.Ico).Value = DBNull.Value
        'End If
        If _AllowEvents Then
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
        End If
    End Sub

    Private Sub Xl_PurchaseOrderItems_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles Me.RowsRemoved
        If _AllowEvents Then
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
        End If
    End Sub

    Private Sub Xl_PurchaseOrderItems_RowValidated(sender As Object, e As DataGridViewCellEventArgs) Handles Me.RowValidated


        If _SkuWithsToAppend.Count > 0 Then
            Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            Dim item As DTOPurchaseOrderItem = oControlItem.Source
            Dim iMainQty As Integer = item.Qty

            For Each oSkuWith As DTOSkuWith In _SkuWithsToAppend
                item = New DTOPurchaseOrderItem()
                With item
                    .Sku = oSkuWith.Child
                    .Qty = iMainQty * oSkuWith.Qty
                    .Pending = .Qty
                    .Price = BLL.BLLPricelistItemCustomer.GetCustomerCost(oSkuWith.Child, _TarifaDtos, _Fch)
                    .Dto = BLL.BLLPurchaseOrderItem.GetDiscount(.Sku, _Order.Customer.Ccx)
                End With
                oControlItem = New ControlItem(item)
                _ControlItems.Add(oControlItem)
            Next
            _SkuWithsToAppend.Clear()
            MyBase.FirstDisplayedScrollingRowIndex = MyBase.Rows.Count - 1
        End If
    End Sub


    Protected Overrides Function ProcessDataGridViewKey(e As System.Windows.Forms.KeyEventArgs) As Boolean
        'opera des de fora de EditingControl
        If e.KeyData = Keys.Delete Then
            MyBase.Rows.Remove(MyBase.CurrentRow)
            Return True
        Else
            Return MyBase.ProcessDataGridViewKey(e)
        End If
    End Function


    Private Sub MatDataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then SetContextMenu()
    End Sub

    Protected Class ControlItem
        Property Source As DTOPurchaseOrderItem
        Property SkuNom As String
        Property Qty As Integer
        Property Ico As Icons
        Property Price As Decimal
        Property Discount As Decimal
        Property Amt As Decimal
        Property Pending As Integer
        Property Stock As Integer
        Property Customers As Integer
        Property Previsio As String

        Property Rep As String
        Property Com As Decimal

        Public Enum Icons
            Empty
        End Enum

        Public Sub New(item As DTOPurchaseOrderItem)
            MyBase.New()
            Load(item)
        End Sub

        Public Sub Load(item As DTOPurchaseOrderItem)
            _Source = item
            _SkuNom = item.Sku.Nom
            _Qty = item.Qty
            _Pending = item.Pending
            _Stock = item.Sku.Stock
            _Customers = item.Sku.Clients

            If item.ChargeCod = DTOPurchaseOrderItem.ChargeCods.Chargeable Then
                _Discount = item.Dto
                If item.Price IsNot Nothing Then
                    _Price = item.Price.Eur
                    _Amt = item.Amt.Eur
                End If
            End If

            If item.RepCom IsNot Nothing Then
                If item.RepCom.Rep IsNot Nothing Then
                    Dim oRep As DTORep = item.RepCom.Rep
                    If oRep.NickName > "" Then
                        _Rep = oRep.NickName
                    ElseIf oRep.FullNom > "" Then
                        _Rep = oRep.FullNom
                    Else
                        _Rep = "Rep #" & oRep.Id
                    End If
                    _Com = item.RepCom.Com
                End If
            End If
        End Sub

        Public Sub New()
            MyBase.New()
            _Source = New DTOPurchaseOrderItem
        End Sub

        Function StockBackColor() As System.Drawing.Color
            Dim retval As System.Drawing.Color = System.Drawing.Color.White
            If _Source.Sku IsNot Nothing Then
                If _Pending = 0 Then
                    If _Stock <= 0 Then
                        If _Source.Sku.Obsoleto Then
                            retval = Color.LightGray
                        Else
                            retval = Color.LightSalmon
                        End If
                    ElseIf _Stock > (_Pending + _Customers) Then
                        retval = Color.LightGreen
                    Else
                        retval = Color.Yellow
                    End If
                Else
                    If _Stock <= 0 Then
                        If _Source.Sku.Obsoleto Then
                            retval = Color.LightGray
                        Else
                            retval = Color.LightSalmon
                        End If
                    ElseIf _Stock >= (_Pending + _Customers) Then
                        retval = Color.LightGreen
                    Else
                        retval = Color.Yellow
                    End If
                End If
            End If
            Return retval
        End Function
    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class



End Class

