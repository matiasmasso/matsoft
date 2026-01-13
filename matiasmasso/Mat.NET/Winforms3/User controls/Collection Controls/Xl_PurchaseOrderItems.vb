Public Class Xl_PurchaseOrderItems
    Inherits TabStopDataGridView

    Private _Cache As Models.ClientCache
    Private _Order As DTOPurchaseOrder
    Private _Ccx As DTOCustomer
    Private _Cur As DTOCur
    Private _Fch As Date
    Private _ControlItems As ControlItems
    Private _SkipMoq As Boolean
    Private _MenuItemEditPrices As ToolStripMenuItem
    Private _PropertiesSet As Boolean
    Private _CellEventArgs As DataGridViewCellEventArgs
    Private _CellProgressBar As ProgressBar
    Private _ExcelSheetToImport As MatHelper.Excel.Sheet
    Private _IsLoaded As Boolean
    Private _AllowEvents As Boolean
    Property MultiTruck As Boolean = False 'divideix en comandes de 80m3
    Property Trucks As List(Of Truck)

    Property CustomerTarifaDtos As List(Of DTOCustomerTarifaDto)
    Property CliProductDtos As List(Of DTOCliProductDto)
    Property CustomCosts As List(Of DTOPricelistItemCustomer)




    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Event PriceListLoaded(sender As Object, e As MatEventArgs)

    Private Enum Cols
        SkuId
        SkuNom
        Qty
        Ico
        Divisa
        Price
        Discount
        Amt
        Pending
        Stock
        Customers
        Previsio
        Rep
        Com
        m3
        xM3
        Lin
    End Enum

    Public Shadows Async Function Load(oOrder As DTOPurchaseOrder, Optional oCcx As DTOCustomer = Nothing) As Task
        Dim exs As New List(Of Exception)
        _Order = oOrder
        _Fch = oOrder.Fch
        _Cur = oOrder.Cur
        _Cache = Await FEB.Cache.Fetch(exs, GlobalVariables.Emp)
        If exs.Count = 0 Then
            If Not _PropertiesSet Then SetProperties()
            refresca()

            If oOrder.Cod = DTOPurchaseOrder.Codis.client Then
                _Ccx = oCcx
                If _ExcelSheetToImport IsNot Nothing Then
                    Await onImportExcel(_ExcelSheetToImport)
                End If
            End If
        Else
            UIHelper.WarnError(exs)
        End If

    End Function

    Public WriteOnly Property Fch As Date
        Set(value As Date)
            _Fch = value
        End Set
    End Property

    Private Sub refresca()
        _AllowEvents = False

        _CellProgressBar = New ProgressBar
        _CellProgressBar.Style = ProgressBarStyle.Marquee
        _CellProgressBar.Visible = False
        _CellProgressBar.BringToFront()
        MyBase.Controls.Add(_CellProgressBar)

        MyBase.Columns(Cols.Divisa).Visible = _Order.Cur.UnEquals(DTOCur.Eur)
        MyBase.Columns(Cols.Divisa).DefaultCellStyle.Format = _Order.Cur.formatString
        MyBase.Columns(Cols.Amt).DefaultCellStyle.Format = _Order.Cur.formatString


        Dim oControlItem As ControlItem = Nothing
        _ControlItems = New ControlItems
        If _MultiTruck Then
            For Each oTruck In _Trucks
                oControlItem = New ControlItem(oTruck)
                _ControlItems.Add(oControlItem)
                For Each item As DTOPurchaseOrderItem In oTruck.Items
                    oControlItem = New ControlItem(item, _Order.Cod, _Cache)
                    _ControlItems.Add(oControlItem)
                Next
            Next
            oControlItem = ControlItem.MissingTruck()
            _ControlItems.Add(oControlItem)
            Dim truckItems = _Trucks.SelectMany(Function(x) x.Items)
            For Each item As DTOPurchaseOrderItem In _Order.Items
                If Not truckItems.Contains(item) Then
                    oControlItem = New ControlItem(item, _Order.Cod, _Cache)
                    _ControlItems.Add(oControlItem)
                End If
            Next
        Else
            For Each item As DTOPurchaseOrderItem In _Order.Items
                oControlItem = New ControlItem(item, _Order.Cod, _Cache)
                _ControlItems.Add(oControlItem)
            Next
        End If

        Dim oBindingSource As New BindingSource
        oBindingSource.AllowNew = True
        oBindingSource.DataSource = _ControlItems
        MyBase.DataSource = oBindingSource

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Sub SetProperties()
        _MenuItemEditPrices = New ToolStripMenuItem("editar preus i descomptes", Nothing, AddressOf Do_EditPrices)
        _MenuItemEditPrices.CheckOnClick = True
        _MenuItemEditPrices.Checked = False

        MyBase.AllowDrop = True
        MyBase.AutoGenerateColumns = False
        With MyBase.RowTemplate
            .Height = MyBase.Font.Height * 1.3
        End With
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = True
        If _MultiTruck Then
            MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            MyBase.MultiSelect = True
        Else
            MyBase.SelectionMode = DataGridViewSelectionMode.CellSelect
            MyBase.MultiSelect = False
        End If
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowUserToDeleteRows = True
        MyBase.AllowDrop = True
        MyBase.RowHeadersWidth = 25
        MyBase.ReadOnly = False
        MyBase.Columns.Clear()


        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.SkuId)
            .HeaderText = "ref"
            .DataPropertyName = "SkuId"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60
            .Visible = _Order.Cod = DTOPurchaseOrder.Codis.proveidor
            .ReadOnly = True
        End With
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
        With DirectCast(MyBase.Columns(Cols.Ico), DataGridViewImageColumn)
            .HeaderText = ""
            .ReadOnly = True
            .Width = 18
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.NullValue = Nothing
            .DefaultCellStyle.DataSourceNullValue = Nothing
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Divisa)
            .HeaderText = "Divisa"
            .DataPropertyName = "Divisa"
            .ReadOnly = True
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None

            Dim oCur = DTOCur.Eur
            If _Order.Cur IsNot Nothing Then
                oCur = _Order.Cur
            End If
            .DefaultCellStyle.Format = oCur.formatString
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .Visible = oCur.UnEquals(DTOCur.Eur())
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Price)
            .HeaderText = "Preu"
            .DataPropertyName = "Price"
            .ReadOnly = True
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Discount)
            .HeaderText = "dte"
            .DataPropertyName = "discount"
            .Width = 35
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "#.##\%;-#.##\%;#"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .ReadOnly = True
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Amt)
            .HeaderText = "import"
            .DataPropertyName = "Amt"
            .ReadOnly = True
            .Width = 80
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = _Cur.formatString
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
            .Visible = _Order.Cod <> DTOPurchaseOrder.Codis.proveidor
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
            .Visible = _Order.Cod <> DTOPurchaseOrder.Codis.proveidor
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
            .Visible = _Order.Cod <> DTOPurchaseOrder.Codis.proveidor
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
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.m3)
            .HeaderText = "m3"
            .DataPropertyName = "M3"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "#0.0000 \m\3;-#0.0000 \m\3;#"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .ReadOnly = True
            .Visible = _Order.Cod = DTOPurchaseOrder.Codis.proveidor
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.xM3)
            .HeaderText = "Volum"
            .DataPropertyName = "xM3"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "#0.000 \m\3;-#0.000 \m\3;#"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .ReadOnly = True
            .Visible = _Order.Cod = DTOPurchaseOrder.Codis.proveidor
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Lin)
            .HeaderText = "Lin"
            .DataPropertyName = "Lin"
            .ReadOnly = True
            .Width = 30
            .DefaultCellStyle.Format = "000"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            '.DefaultCellStyle.BackColor = BCOLORBLANK
            .Visible = _Order.Cod <> DTOPurchaseOrder.Codis.proveidor
        End With


        _PropertiesSet = True
    End Sub

    Public ReadOnly Property Items As List(Of DTOPurchaseOrderItem)
        Get
            Dim retval As New List(Of DTOPurchaseOrderItem)
            For Each oControlItem As ControlItem In _ControlItems
                If oControlItem.LinCod = ControlItem.LinCods.Item Then
                    Dim item As DTOPurchaseOrderItem = oControlItem.Source
                    item.PurchaseOrder = _Order
                    If _Order.IsNew Then
                        If item.Pending <> 0 Then retval.Add(oControlItem.Source)
                    Else
                        retval.Add(item)
                    End If
                End If
            Next
            Return retval
        End Get
    End Property

    Public Function EmptyQties() As Integer
        Dim retval As Integer = _ControlItems.ToList.Where(Function(x) x.Qty = 0).Count
        Return retval
    End Function



#Region "Validation"

    Private Sub MatDataGridView1_CellValidated(sender As Object, e As DataGridViewCellEventArgs) Handles MyBase.CellValidated
        If _AllowEvents Then

            Select Case e.ColumnIndex
                Case Cols.Qty
                    Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                'oRow.Cells(Cols.Qty).Style.BackColor = GetStockBackColor(e.RowIndex)

                Case Cols.SkuNom
                    Dim exs As New List(Of Exception)
                    Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                    If oRow IsNot Nothing Then
                        Dim oControlItem As ControlItem = oRow.DataBoundItem
                        If oControlItem IsNot Nothing Then
                            Dim item As DTOPurchaseOrderItem = oControlItem.Source
                            'If item.Sku IsNot Nothing Then
                            'If item.Sku.Obsoleto Then exs.Add(New Exception("Article obsolet"))
                            'If item.Sku.LastProduction And item.Sku.Proveidors + item.Sku.Stock = 0 Then exs.Add(New Exception("Article esgotat i sense producció"))
                            'End If
                            'If item.Sku.Virtual Then refresca()

                            'oRow.Cells(Cols.SkuNom).Style.BackColor = GetStockBackColor(e.RowIndex)

                            'If GuidHelper.IsGuid(oRow.Cells(Cols.ArtGuid).Value) Then
                            ' Dim oArt As Art = GetArtFromDataGridViewRow(oRow)
                            ' If oArt.Obsoleto Then
                            ' oRow.Cells(Cols.ArtNom).Style.BackColor = Color.LightGray
                            ' End If
                            ' End If

                        End If

                    End If
                    If exs.Count > 0 Then UIHelper.WarnError(exs)
            End Select

        End If
    End Sub

    Private Async Sub MatDataGridView1_CellValidating(ByVal sender As Object, ByVal e As DataGridViewCellValidatingEventArgs) Handles MyBase.CellValidating
        If _AllowEvents Then
            Dim exs As New List(Of Exception)

            Select Case e.ColumnIndex
                Case Cols.Qty
                    Await ValidateQty(e, exs)

                Case Cols.SkuNom
                    ValidateSkuNom(e, exs)

            End Select

            If exs.Count > 0 Then
                UIHelper.WarnError(exs)
                If MyBase.Rows.Count > e.RowIndex Then
                    Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                    oRow.Cells(e.ColumnIndex).Selected = True
                    e.Cancel = True
                End If
            End If
        End If
    End Sub

    Private Async Function ValidateQty(e As DataGridViewCellValidatingEventArgs, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        If oRow Is Nothing Then
            retval = True
        Else
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            If oControlItem IsNot Nothing Then
                Dim item As DTOPurchaseOrderItem = oControlItem.Source

                If e.FormattedValue = "" Then
                    retval = True
                Else
                    If IsNumeric(e.FormattedValue) Then
                        Dim iNewQty As Integer = e.FormattedValue
                        Await CheckMinPack(item, iNewQty, exs)
                        Await CheckMaxUnits(item, iNewQty, exs)
                        Await CheckSortides(item, iNewQty, exs)
                        retval = (exs.Count = 0)
                    Else
                        exs.Add(New InvalidCastException("la quantitat ha de ser un valor numéric"))
                    End If
                End If
            End If
        End If
        Return retval
    End Function

    Private Async Function CheckMaxUnits(item As DTOPurchaseOrderItem, ByVal iNewQty As Integer, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retVal As Boolean = True
        Dim oSku As DTOProductSku = item.Sku

        If oSku IsNot Nothing Then
            If oSku.LastProduction Then
                If iNewQty <> item.Qty Then
                    Dim iMax As Integer = Await FEB.ProductSku.LastProductionAvailableUnits(exs, oSku)
                    If exs.Count = 0 Then
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
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If
            End If
        End If

        Return retVal
    End Function

    Private Async Function CheckMinPack(item As DTOPurchaseOrderItem, ByVal iNewQty As Integer, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        If item.Sku Is Nothing Then
            retval = True
        ElseIf _SkipMoq Then
            retval = True
        Else
            If item.Sku.ForzarInnerPack Then
                If iNewQty Mod item.Sku.InnerPack = 0 Then
                    retval = True
                Else
                    If Await FEB.Product.AllowUserToFraccionarInnerPack(exs, item.Sku, Current.Session.User) Then
                        retval = True
                    Else
                        exs.Add(New Exception("la quantitat ha de ser multiplo de " & item.Sku.InnerPack))
                    End If
                End If
            Else
                retval = True
            End If
        End If

        Return retval
    End Function

    Private Async Function CheckSortides(item As DTOPurchaseOrderItem, ByVal iNewQty As Integer, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim iSortides As Integer = Await FEB.PurchaseOrderItem.UnitatsSortides(exs, item)
        If exs.Count = 0 Then
            If iSortides > 0 And iSortides > iNewQty Then
                Dim singular As Boolean = (iSortides = 1)
                Dim sMsg As String = String.Format("No podem baixar la quantitat a {0} perque ja ha{1} sortit {2} unitat{3} d'aquesta linia", iNewQty, IIf(singular, "", "n"), iSortides, IIf(singular, "", "s"))
                exs.Add(New Exception(sMsg))
            Else
                retval = True
            End If
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
        Dim retval As DTOAmt = Nothing
        If _ControlItems IsNot Nothing Then
            Dim DcBase As Decimal = _ControlItems.
            Where(Function(x) x.Source IsNot Nothing AndAlso x.Source.ErrCod = DTOPurchaseOrderItem.ErrCods.Success).
            Sum(Function(x) x.Amt)

            retval = DTOAmt.Factory(DcBase)
        End If
        Return retval
    End Function

    Private Sub SetTotals(oControlItem As ControlItem)
        With oControlItem
            If .Price = 0 Or .Qty = 0 Then
                .Amt = 0
            Else
                .Amt = Math.Round(.Price * .Qty * (100 - .Discount) / 100, 2, MidpointRounding.AwayFromZero)
            End If
            .xM3 = .Qty * .M3
        End With
        Dim oBase As DTOAmt = GetTotals()
        RaiseEvent AfterUpdate(Me, New MatEventArgs(oBase))
    End Sub

    Private Function IsAllowed(oCustomer As DTOCustomer, oSku As DTOProductSku, ByRef CodExclusio As DTOProductSku.CodisExclusio) As Boolean
        Dim retval As Boolean
        If oCustomer Is Nothing OrElse oCustomer.IsConsumer Then
            retval = True
        Else
            Select Case oSku.Category.Codi
                Case DTOProductCategory.Codis.spareparts, DTOProductCategory.Codis.POS, DTOProductCategory.Codis.others
                    retval = True
                Case Else
                    If oCustomer.Rol.isStaff Then
                        retval = True
                    ElseIf oCustomer.Rol.id = DTORol.Ids.taller Then
                        retval = True
                    ElseIf oCustomer.Rol.id = DTORol.Ids.rep Then
                        retval = True
                    ElseIf oCustomer.Rol.id = DTORol.Ids.manufacturer Then
                        retval = True
                    ElseIf (oCustomer.Rol.id = DTORol.Ids.cliFull Or oCustomer.Rol.id = DTORol.Ids.cliLite) Then
                        Select Case oSku.CodExclusio
                            Case DTOProductSku.CodisExclusio.Inclos
                                retval = True
                            Case DTOProductSku.CodisExclusio.Obsolet
                                retval = oSku.stockAvailable > 0
                        End Select
                        'End If
                    Else
                        retval = True
                    End If

            End Select
        End If
        Return retval
    End Function

    Private Sub ShowProgressBar(sender As Object, e As DataGridViewCellEventArgs)
        Dim rect = MyBase.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, True)
        With _CellProgressBar
            .Location = rect.Location
            .Size = rect.Size
            .Visible = True
        End With
        Application.DoEvents()
    End Sub
    Private Sub HideProgressBar()
        _CellProgressBar.Visible = False
        Application.DoEvents()
    End Sub


    Private Async Sub MatDataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles MyBase.CellValueChanged
        Dim exs As New List(Of Exception)
        If _AllowEvents Then
            Select Case e.ColumnIndex
                Case Cols.SkuNom
                    Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                    Dim BlPreviousAllowEvents As Boolean = _AllowEvents
                    Dim sKey As String = oRow.Cells(Cols.SkuNom).Value 'e.FormattedValue

                    Dim oCodExclusio As DTOProductSku.CodisExclusio = DTOProductSku.CodisExclusio.Inclos
                    Dim oSku As DTOProductSku = Nothing
                    Me.ShowProgressBar(sender, e)
                    If _Order.Cod = DTOPurchaseOrder.Codis.proveidor Then
                        oSku = Await Finder.FindSku(exs, GlobalVariables.Emp, sKey, GlobalVariables.Emp.Mgz)
                    Else
                        oSku = Await FindSku(exs, _Order.Customer, sKey, e)
                    End If
                    If oSku Is Nothing Then
                        'e.Cancel = True
                        _ControlItems.Remove(oRow.DataBoundItem)
                        Me.HideProgressBar()
                        exs.Add(New Exception(String.Format("No s'ha trobat cap article per '{0}'", sKey)))
                    ElseIf _Order.Cod = DTOPurchaseOrder.Codis.client AndAlso Not IsAllowed(_Ccx, oSku, oCodExclusio) Then
                        If Not IsAllowed(_Order.Customer, oSku, oCodExclusio) Then
                            Dim sReason As String = ""
                            Select Case oSku.CodExclusio
                                Case DTOProductSku.CodisExclusio.Canal
                                    sReason = "El canal de distibució del client té bloquejat aquest article"
                                    exs.Add(New Exception(sReason))
                                Case DTOProductSku.CodisExclusio.Exclusives
                                    sReason = "El client té bloquejat aquest article a la pantalla d'exclusives"
                                    exs.Add(New Exception(sReason))
                                Case DTOProductSku.CodisExclusio.PremiumLine
                                    sReason = String.Format("'{0}' forma part de una Premium Line a la que aquest client no hi te acces", oSku.RefYNomLlarg.Tradueix(Current.Session.Lang))
                                    exs.Add(New Exception(sReason))
                                Case DTOProductSku.CodisExclusio.Obsolet
                                    sReason = String.Format("Article {0} obsolet", oSku.RefYNomLlarg.Tradueix(Current.Session.Lang))
                                    exs.Add(New Exception(sReason))
                                Case DTOProductSku.CodisExclusio.OutOfCatalog
                                    sReason = "Aquest article es trova fora del catàleg d'aquest client"
                                    exs.Add(New Exception(sReason))
                            End Select
                            Dim oControlItem As ControlItem = oRow.DataBoundItem
                            _ControlItems.Remove(oControlItem)

                            MyBase.Refresh()
                            'oRow.Cells(Cols.SkuNom).Value = ""
                            'MyBase.CurrentCell = oRow.Cells(Cols.SkuNom)
                        End If
                    Else
                        If _Order.Cod = DTOPurchaseOrder.Codis.client Then
                            'e.Cancel = True
                            If oSku.LastProduction And oSku.Proveidors + oSku.Stock = 0 Then exs.Add(New Exception("Article esgotat i sense producció"))
                        End If

                        Dim oControlItem As ControlItem = oRow.DataBoundItem
                        Dim item As DTOPurchaseOrderItem = oControlItem.Source
                        With item
                            .PurchaseOrder = _Order
                            .Sku = oSku
                            'If .Sku.virtual Then .PackParent = item

                            If _Order.Cod = DTOPurchaseOrder.Codis.proveidor Then
                                Dim oPriceListItem = Await FEB.PriceListItemSupplier.GetPreusDeCost(exs, oSku)
                                If exs.Count = 0 Then
                                    If oPriceListItem Is Nothing Then
                                        .Price = DTOAmt.Factory(0)
                                        .Dto = 0
                                    Else
                                        .Price = DTOAmt.Factory(oPriceListItem.Price / oPriceListItem.Parent.Cur.ExchangeRate.Rate, oPriceListItem.Parent.Cur.Tag, oPriceListItem.Price)
                                        .Dto = oPriceListItem.Parent.Discount_OnInvoice
                                    End If
                                Else
                                    UIHelper.WarnError(exs)
                                End If
                            Else
                                If _Order.Customer.IsConsumer Then
                                    If _Order.ExportCod = DTOInvoice.ExportCods.nacional Then
                                        Dim dcIvaTipus = DTOTax.closestTipus(DTOTax.Codis.iva_Standard, _Order.Fch)
                                        Dim dcCost = Math.Round(oSku.Rrpp.Eur / ((100 + dcIvaTipus) / 100), 2)
                                        .Price = DTOAmt.Factory(dcCost)
                                    Else
                                        .Price = oSku.Rrpp
                                    End If
                                Else
                                    Dim diversos = oSku.Category.Brand.Equals(DTOProductBrand.Wellknown(DTOProductBrand.Wellknowns.Varios))
                                    Dim oCost As DTOAmt = If(diversos, .Sku.Rrpp, .Sku.Price)
                                    If oCost Is Nothing Then
                                        .Price = DTOAmt.Factory(0)
                                        .Dto = 0
                                    Else
                                        .Price = oCost
                                        If diversos = False Then .Dto = .Sku.CustomerDto
                                    End If

                                    If diversos = False Then
                                        .RepCom = Await FEB.RepCom.GetRepCom(Current.Session.Emp, _Order.Customer, .Sku, _Fch, exs:=exs)
                                    End If
                                End If
                            End If

                        End With

                        oControlItem.Load(item, _Order.Cod, _Cache)
                        item.Bundle.Clear()

                        If item.Sku.IsBundle Then
                            Dim dcSumRrpp = item.Sku.BundleSkus.Sum(Function(x) x.Qty * x.Rrpp)
                            Dim dcCostFactor = item.Sku.Price.Eur / dcSumRrpp
                            For Each oBundleSku In item.Sku.BundleSkus
                                Dim DcPrice = oBundleSku.Rrpp * dcCostFactor / oBundleSku.Qty
                                Dim item2 As New DTOPurchaseOrderItem
                                With item2
                                    .Sku = oBundleSku.Sku
                                    .Price = DTOAmt.Factory(DcPrice)
                                    .Dto = item.Sku.CustomerDto
                                    .Qty = oBundleSku.Qty
                                    '.repCom = BEBL.RepCom.GetRepCom(oEmp, oOrder.customer, oSkuBundle.Sku, oOrder.fch, oRepProducts, exs:=exs)
                                End With
                                item.Bundle.Add(item2)
                            Next
                            'item.Bundle = Await FEB.PurchaseOrderItem.BundleItems(exs, item)
                        End If

                        SetTotals(oControlItem)
                        MyBase.Refresh()
                        Me.HideProgressBar()
                        _AllowEvents = BlPreviousAllowEvents

                    End If



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
                            Dim iUnitatsSortides = Await FEB.PurchaseOrderItem.UnitatsSortides(exs, item)
                            If exs.Count = 0 Then
                                item.Pending = item.Qty - iUnitatsSortides
                            Else
                                UIHelper.WarnError(exs)
                            End If
                        End If
                    End If
                    oControlItem.Pending = item.Pending

                    If item.Sku.IsBundle Then
                        For Each oChild As DTOPurchaseOrderItem In item.Bundle
                            oChild.Qty = item.Qty
                            oChild.Pending = item.Pending
                        Next
                    End If


                    SetTotals(oControlItem)

                Case Cols.Price
                    Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                    Dim oControlItem As ControlItem = oRow.DataBoundItem
                    Dim item As DTOPurchaseOrderItem = oControlItem.Source
                    With oControlItem
                        item.Price = DTOAmt.Factory(.Price)
                        If .Price <> 0 Then
                            .Amt = Math.Round(.Price * .Qty * (100 - .Discount) / 100, 2, MidpointRounding.AwayFromZero)
                        End If
                    End With

                    SetTotals(oControlItem)

                Case Cols.Divisa
                    Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                    Dim oControlItem As ControlItem = oRow.DataBoundItem
                    Dim item As DTOPurchaseOrderItem = oControlItem.Source
                    With oControlItem
                        item.Price = _Cur.amtFromDivisa(item.Price.Val)
                    End With

                    SetTotals(oControlItem)

                Case Cols.Discount
                    Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                    Dim oControlItem As ControlItem = oRow.DataBoundItem
                    Dim item As DTOPurchaseOrderItem = oControlItem.Source
                    With oControlItem
                        item.Dto = .Discount
                        If .Price <> 0 Then
                            .Amt = Math.Round(.Price * .Qty * (100 - .Discount) / 100, 2, MidpointRounding.AwayFromZero)
                        End If
                    End With

                    SetTotals(oControlItem)

                Case Cols.SkuNom

            End Select

            If exs.Count > 0 Then
                Me.HideProgressBar()
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Async Function FindSku(exs As List(Of Exception), oCustomer As DTOCustomer, ByVal sSearchKey As String, e As DataGridViewCellEventArgs) As Task(Of DTOProductSku)
        Dim retval As DTOProductSku = Nothing
        Dim oSkus = _Cache.SkuSearch(sSearchKey)
        If exs.Count = 0 Then
            If oSkus Is Nothing Then
                exs.Add(New Exception("Error al buscar el producte per " & sSearchKey))
            Else
                Select Case oSkus.Count
                    Case 0
                    Case 1
                        retval = oSkus.First
                        SetPriceAndDto(retval)

                    Case Else
                        Me.HideProgressBar()

                        Dim rowHeight1 As Integer = MyBase.Rows(e.RowIndex).Height
                        Dim cellRectangle1 As Rectangle = MyBase.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, False)
                        cellRectangle1.X += MyBase.Left
                        cellRectangle1.Y += MyBase.Top + rowHeight1
                        Dim displayPoint As Point = PointToScreen(New Point(cellRectangle1.X, cellRectangle1.Y))

                        Dim oFrm As New Frm_Skus_Select(oSkus, displayPoint)
                        oFrm.ShowDialog()
                        Me.ShowProgressBar(Me, e)
                        retval = oFrm.SelectedObject ' Await FEB.ProductSku.LoadFromCustomer(exs, oFrm.SelectedObject, oCustomer, GlobalVariables.Emp.Mgz)
                        SetPriceAndDto(retval)


                End Select
            End If
        End If
        Return retval
    End Function

    Private Sub SetPriceAndDto(ByRef oSku As DTOProductSku)
        If _CustomCosts Is Nothing Then _CustomCosts = New List(Of DTOPricelistItemCustomer)
        If _CustomerTarifaDtos Is Nothing Then _CustomerTarifaDtos = New List(Of DTOCustomerTarifaDto)
        If _CliProductDtos Is Nothing Then _CliProductDtos = New List(Of DTOCliProductDto)
        oSku.Rrpp = DTOAmt.Factory(_Cache.RetailPrice(oSku.Guid))
        oSku.Price = oSku.GetCustomerCost(_CustomCosts, _CustomerTarifaDtos)
        Dim oDto As DTOCliProductDto = oSku.CliProductDto(_CliProductDtos)
        If oDto IsNot Nothing Then oSku.CustomerDto = oDto.Dto
    End Sub

    Private Sub MatDataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles MyBase.CellFormatting
        Select Case e.ColumnIndex

            Case Cols.SkuNom
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                If oControlItem IsNot Nothing AndAlso oControlItem.LinCod = ControlItem.LinCods.Item Then
                    Dim oItem As DTOPurchaseOrderItem = oControlItem.Source
                    Dim oSku As DTOProductSku = oItem.Sku
                    If oSku IsNot Nothing Then
                        If oSku.obsoleto Then
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
                    If oControlItem.Source IsNot Nothing Then
                        Dim oItem = oControlItem.Source

                        If oItem.ErrCod <> DTOPurchaseOrderItem.ErrCods.Success Then
                            e.Value = My.Resources.aspa
                        ElseIf oControlItem IsNot Nothing AndAlso oItem IsNot Nothing AndAlso oItem.Sku IsNot Nothing Then
                            Dim oSku = oItem.Sku
                            Select Case DTOProductSku.Moq(oSku)
                                Case 0, 1
                                    e.Value = Nothing ' My.Resources.empty
                                Case 2
                                    e.Value = My.Resources.dau2_17x17
                                Case 3
                                    e.Value = My.Resources.dau3_17x17
                                Case 4
                                    e.Value = My.Resources.dau4_17x17
                                Case 5
                                    e.Value = My.Resources.dau5_17x17
                                Case Else
                                    e.Value = My.Resources.dau6_17x17
                            End Select
                        Else
                            e.Value = Nothing
                        End If

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
            If oControlItem.LinCod = ControlItem.LinCods.Item Then
                Dim item As DTOPurchaseOrderItem = oControlItem.Source
                Dim oMenu As New Menu_PurchaseOrderItem(item)
                AddHandler oMenu.AfterUpdate, AddressOf RefreshRequest
                oContextMenu.Items.AddRange(oMenu.Range)
                oContextMenu.Items.Add("-")
                oContextMenu.Items.Add("elimina linia", Nothing, AddressOf Do_Delete)
                If item.ErrCod <> DTOPurchaseOrderItem.ErrCods.Success Then
                    oContextMenu.Items.Add("desbloqueja linia", Nothing, AddressOf Do_RemoveErrCod)
                End If
                If _MultiTruck Then
                    Dim vol As Decimal
                    For Each oRow In MyBase.SelectedRows
                        Dim pControlItem As ControlItem = oRow.databounditem
                        If pControlItem IsNot Nothing Then
                            vol += pControlItem.xM3
                        End If
                    Next
                    oContextMenu.Items.Add(String.Format("assigna {0:0.00} m3 a nou camió", vol), Nothing, AddressOf Do_AddTruck)
                    For Each oTruck In _Trucks
                        Dim oMenuItem As New ToolStripMenuItem(String.Format("assigna'ls al camió {0}", oTruck.Caption), Nothing, AddressOf Do_SwitchToTruck)
                        oMenuItem.Tag = oTruck
                        oContextMenu.Items.Add(oMenuItem)
                    Next
                    oContextMenu.Items.Add("-")
                End If
            End If
        End If
        oContextMenu.Items.Add(_MenuItemEditPrices)

        Dim oMenuItemRepCom As New ToolStripMenuItem("Comisions")
        oContextMenu.Items.Add(oMenuItemRepCom)
        oMenuItemRepCom.DropDownItems.Add(New ToolStripMenuItem("vista", Nothing, AddressOf Do_RepComs))
        oMenuItemRepCom.DropDownItems.Add(New ToolStripMenuItem("edita", Nothing, AddressOf Do_EditRepComs))
        oMenuItemRepCom.DropDownItems.Add(New ToolStripMenuItem("valors per defecte", Nothing, AddressOf Do_RecalcRepComs))


        Dim oSkipMoqMenuItem As New ToolStripMenuItem("permet fraccionar en aquesta comanda", Nothing, AddressOf Do_SkipMoq)
        oSkipMoqMenuItem.CheckOnClick = True
        oSkipMoqMenuItem.Checked = _SkipMoq
        oContextMenu.Items.Add(oSkipMoqMenuItem)

        oContextMenu.Items.Add("clonar de altre comanda", Nothing, AddressOf Do_Clon)
        oContextMenu.Items.Add("importar Excel de client", Nothing, AddressOf Do_ImportExcel)
        oContextMenu.Items.Add("divideix en camions", Nothing, AddressOf Do_MultiTruck)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_MultiTruck()
        _MultiTruck = Not _MultiTruck
        If _Trucks Is Nothing Then
            _Trucks = New List(Of Truck)
            Dim oTruck As New Truck
            oTruck.Id = _Trucks.Count + 1
            _Trucks.Add(oTruck)

            Dim oItems As New List(Of DTOPurchaseOrderItem)
            For Each oItem In _Order.Items
                oItems.Add(oItem)
            Next

            _Order.Items = New List(Of DTOPurchaseOrderItem)
            For Each oItem In oItems
                Dim oTruckItem = Truck.Item.Factory(oItem, oTruck)
                _Order.Items.Add(oTruckItem)
                oTruck.Items.Add(oTruckItem)
            Next
        End If

        If _MultiTruck Then
            MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            MyBase.MultiSelect = True
        Else
            MyBase.SelectionMode = DataGridViewSelectionMode.CellSelect
            MyBase.MultiSelect = False
        End If

        refresca()
    End Sub

    Private Sub Do_AddTruck()
        Dim oTruck As New Truck
        oTruck.Id = _Trucks.Count + 1
        _Trucks.Add(oTruck)
        SwitchSelectedRowsToTruck(oTruck)
        refresca()
    End Sub

    Private Sub Do_SwitchToTruck(sender As Object, e As EventArgs)
        Dim oMenuItem As ToolStripMenuItem = sender
        Dim oTruck = oMenuItem.Tag
        SwitchSelectedRowsToTruck(oTruck)
        refresca()
    End Sub

    Private Sub SwitchSelectedRowsToTruck(oTruck As Truck)
        For Each oRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.databounditem
            If oControlItem.LinCod = ControlItem.LinCods.Item Then
                Dim oItem As Truck.Item = CType(oControlItem.Source, Truck.Item)
                oItem.Truck.Items.Remove(oItem)
                oItem.Truck = oTruck
                oItem.Truck.Items.Add(oItem)
            End If
        Next
    End Sub

    Private Sub Do_RemoveErrCod()
        Dim oControlItem As ControlItem = CurrentControlItem()
        Dim item As DTOPurchaseOrderItem = oControlItem.Source
        item.ErrCod = DTOPurchaseOrderItem.ErrCods.Success
        item.ErrDsc = ""
        MyBase.Refresh()
        RaiseEvent AfterUpdate(Me, New MatEventArgs())
    End Sub

    Private Sub Do_SkipMoq(sender As Object, e As EventArgs)
        _SkipMoq = Not _SkipMoq
        CType(sender, ToolStripMenuItem).Checked = _SkipMoq
    End Sub

    Private Async Sub Do_Delete()
        Dim exs As New List(Of Exception)
        Dim oRow As DataGridViewRow = MyBase.CurrentRow
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        Dim oPurchaseOrderItem As DTOPurchaseOrderItem = oControlItem.Source
        Dim oDeliveryItems = Await FEB.PurchaseOrderItem.DeliveryItems(exs, oPurchaseOrderItem)
        If exs.Count = 0 Then
            If oDeliveryItems.Count = 0 Then
                _ControlItems.Remove(oRow.DataBoundItem)
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            Else
                Dim sb As New System.Text.StringBuilder
                sb.AppendLine("No es pot eliminar perque ja ha sortit mercancía:")
                For Each item As DTODeliveryItem In oDeliveryItems
                    sb.AppendLine(String.Format("{0} unitat{1} a l'albará {2:00000} del {3:dd/MM/yy}", item.Qty, IIf(item.Qty > 1, "s", ""), item.Delivery.Id, item.Delivery.Fch))
                Next
                UIHelper.WarnError(sb.ToString())
            End If
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Sub Do_EditPrices()
        MyBase.Columns(Cols.Price).ReadOnly = Not _MenuItemEditPrices.Checked
        MyBase.Columns(Cols.Divisa).ReadOnly = Not _MenuItemEditPrices.Checked
        MyBase.Columns(Cols.Discount).ReadOnly = Not _MenuItemEditPrices.Checked
    End Sub

    Private Sub Do_Clon()
        Dim oFrm As New Frm_PurchaseOrders(DTO.Defaults.SelectionModes.selection)
        AddHandler oFrm.onItemSelected, AddressOf Do_ImportSelectedOrderWithNoDtos
        oFrm.Show()
    End Sub

    Private Sub Do_ImportExcel()
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "Importar Excel de client"
            .Filter = "Excel (*.xls,*.xlsx)|*.xls;*.xlsx|documents csv (*.csv)|*.csv|tots els fitxers (*.*)|*.*"
            If .ShowDialog = DialogResult.OK Then
                Dim sFields = {"Ean", "Quantitat", "Linia"}
                Dim oFrm As New Frm_ExcelColumsMapping(sFields, .FileName)
                AddHandler oFrm.AfterUpdate, AddressOf onImportExcel
                oFrm.Show()
            End If
        End With
    End Sub

    Private Async Sub onImportExcel(sender As Object, e As MatEventArgs)
        Dim oSheet As MatHelper.Excel.Sheet = e.Argument
        Await onImportExcel(oSheet)
    End Sub

    Private Async Function onImportExcel(oSheet As MatHelper.Excel.Sheet) As Task
        Dim exs As New List(Of Exception)
        Dim retval As New List(Of DTOPurchaseOrderItem.Result)

        Dim oEans As New List(Of DTOEan)
        For Each oRow In oSheet.Rows
            If oSheet.ColumnHeadersOnFirstRow AndAlso oRow.Equals(oSheet.Rows.First) Then
                'skip oSheet header row
            Else
                Dim sEan As String = oRow.Cells(0).Content
                Dim oEan = DTOEan.Factory(sEan)
                If DTOEan.isValid(oEan) Then
                    oEans.Add(oEan)
                End If
            End If
        Next

        Dim oSkus = Await FEB.ProductSkus.Search(exs, oEans, _Order.Customer, GlobalVariables.Emp.Mgz)
        If exs.Count = 0 Then

            For Each oRow In oSheet.Rows

                If oSheet.ColumnHeadersOnFirstRow AndAlso oRow.Equals(oSheet.Rows.First) Then
                    'skip oSheet header row
                Else
                    Dim rowNum = oSheet.Rows.IndexOf(oRow) + 1
                    Dim sEan As String = oRow.Cells(0).Content
                    Dim sLin As String = oRow.Cells(2).Content
                    If IsNumeric(sLin) Then rowNum = CInt(sLin)
                    Dim oEan = DTOEan.Factory(sEan)
                    If DTOEan.isValid(oEan) Then
                        Dim oSku = oSkus.FirstOrDefault(Function(x) x.Ean13.Equals(oEan))
                        If oSku Is Nothing OrElse oSku.CodExclusio = DTOProductSku.CodisExclusio.OutOfCatalog Then
                            retval.Add(DTOPurchaseOrderItem.Fail(rowNum, DTOPurchaseOrderItem.Result.Cods.UnknownProduct, oRow, String.Format("Ean13 '{0}' desconocido", sEan)))
                        Else
                            If oSku.CodExclusio = DTOProductSku.CodisExclusio.Canal Then oSku.CodExclusio = DTOProductSku.CodisExclusio.Inclos 'temporal fins a areglar-ho
                            Select Case oSku.CodExclusio
                                Case DTOProductSku.CodisExclusio.Obsolet
                                    retval.Add(DTOPurchaseOrderItem.Fail(rowNum, DTOPurchaseOrderItem.Result.Cods.ObsoletProduct, oRow, String.Format("'{0}' producto obsoleto desde {1:dd/MM/yy}", sEan, oSku.FchObsoleto)))
                                    Dim sQty As String = oRow.Cells(1).Content
                                    If IsNumeric(sQty) Then
                                        Dim iQty = CInt(sQty)
                                        If iQty Mod oSku.Moq = 0 Then
                                        Else
                                            retval.Add(DTOPurchaseOrderItem.Fail(rowNum, DTOPurchaseOrderItem.Result.Cods.Moq, oRow, String.Format("'{0}' se debe pedir en múltiplos de {1} unidades", sEan, oSku.Moq)))
                                        End If
                                        If oSku.stockAvailable <= 0 Then
                                            retval.Add(DTOPurchaseOrderItem.Fail(rowNum, DTOPurchaseOrderItem.Result.Cods.OutOfStock, oRow, String.Format("'{0}' existencias temporalmente agotadas", sEan)))
                                        ElseIf oSku.stockAvailable < iQty Then
                                            retval.Add(DTOPurchaseOrderItem.Fail(rowNum, DTOPurchaseOrderItem.Result.Cods.NoStockEnough, oRow, String.Format("'{0}' existencias insuficientes", sEan)))
                                        End If
                                        Dim item As New DTOPurchaseOrderItem()
                                        With item
                                            .Sku = oSku
                                            .Qty = CInt(sQty)
                                            .Pending = .Qty
                                            .Price = .Sku.Price
                                            .Dto = .Sku.CustomerDto
                                            .RepCom = Await FEB.RepCom.GetRepCom(Current.Session.Emp, _Order.Customer, .Sku, _Fch)
                                            'If IsNumeric(sLin) Then
                                            .CustomLin = rowNum
                                            'End If
                                        End With
                                        _Order.Items.Add(item)
                                        retval.Add(DTOPurchaseOrderItem.Success(rowNum, item))
                                    Else
                                        retval.Add(DTOPurchaseOrderItem.Fail(rowNum, DTOPurchaseOrderItem.Result.Cods.BadFormat, oRow, String.Format("'{0}' no es un formato válido para el número de unidades", sQty)))
                                    End If

                                Case DTOProductSku.CodisExclusio.Canal, DTOProductSku.CodisExclusio.Exclusives, DTOProductSku.CodisExclusio.PremiumLine
                                    retval.Add(DTOPurchaseOrderItem.Fail(rowNum, DTOPurchaseOrderItem.Result.Cods.ObsoletProduct, oRow, String.Format("'{0}' producto de distribución limitada", sEan)))
                                Case DTOProductSku.CodisExclusio.Inclos
                                    Dim sQty As String = oRow.Cells(1).Content
                                    If IsNumeric(sQty) Then
                                        Dim iQty = CInt(sQty)
                                        If iQty Mod oSku.Moq = 0 Then
                                        Else
                                            retval.Add(DTOPurchaseOrderItem.Fail(rowNum, DTOPurchaseOrderItem.Result.Cods.Moq, oRow, String.Format("'{0}' se debe pedir en múltiplos de {1} unidades", sEan, oSku.Moq)))
                                        End If
                                        If oSku.stockAvailable <= 0 Then
                                            retval.Add(DTOPurchaseOrderItem.Fail(rowNum, DTOPurchaseOrderItem.Result.Cods.OutOfStock, oRow, String.Format("'{0}' existencias temporalmente agotadas", sEan)))
                                        ElseIf oSku.stockAvailable < iQty Then
                                            retval.Add(DTOPurchaseOrderItem.Fail(rowNum, DTOPurchaseOrderItem.Result.Cods.NoStockEnough, oRow, String.Format("'{0}' existencias insuficientes", sEan)))
                                        End If
                                        Dim item As New DTOPurchaseOrderItem()
                                        With item
                                            .Sku = oSku
                                            .Qty = CInt(sQty)
                                            .Pending = .Qty
                                            .Price = .Sku.Price
                                            .Dto = .Sku.CustomerDto
                                            .RepCom = Await FEB.RepCom.GetRepCom(Current.Session.Emp, _Order.Customer, .Sku, _Fch)
                                            'If IsNumeric(sLin) Then
                                            .CustomLin = rowNum
                                            'End If
                                        End With
                                        _Order.Items.Add(item)
                                        retval.Add(DTOPurchaseOrderItem.Success(rowNum, item))
                                    Else
                                        retval.Add(DTOPurchaseOrderItem.Fail(rowNum, DTOPurchaseOrderItem.Result.Cods.BadFormat, oRow, String.Format("'{0}' no es un formato válido para el número de unidades", sQty)))
                                    End If
                            End Select

                        End If
                    Else
                        retval.Add(DTOPurchaseOrderItem.Fail(rowNum, DTOPurchaseOrderItem.Result.Cods.BadFormat, oRow, String.Format("'{0}' no es un codigo Ean13 válido", sEan)))
                    End If
                End If
            Next

            refresca()
            Dim oBase As DTOAmt = GetTotals()
            RaiseEvent AfterUpdate(Me, New MatEventArgs(oBase))

        End If

        If exs.Count > 0 Then
            UIHelper.WarnError(exs)
        End If

        If retval.Any(Function(x) x.Cod <> DTOPurchaseOrderItem.Result.Cods.Success) Then
            Dim failures = retval.Where(Function(x) x.Cod <> DTOPurchaseOrderItem.Result.Cods.Success).ToList()
            Dim recipients = Await FEB.Subscriptors.Recipients(exs, GlobalVariables.Emp, DTOSubscription.Wellknowns.PedidosIncidencias, _Order.Customer)
            If exs.Count = 0 Then
                Dim oMailMessage = DTOPurchaseOrderItem.Result.mailMessage(failures, recipients)
                If Not Await OutlookHelper.Send(oMailMessage, exs) Then
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Function

    Private Sub Do_ImportSelectedOrderWithNoDtos(sender As Object, e As MatEventArgs)
        Do_ImportSelectedOrder(e.Argument, False)
    End Sub

    Private Sub Do_ImportSelectedOrderWithDtos(sender As Object, e As MatEventArgs)
        Do_ImportSelectedOrder(e.Argument, True)
    End Sub


    Private Async Sub Do_ImportSelectedOrder(oSrcOrder As DTOPurchaseOrder, IncludeDtos As Boolean)
        Dim exs As New List(Of Exception)
        If FEB.PurchaseOrder.Load(exs, oSrcOrder, GlobalVariables.Emp.Mgz) Then
            Dim items As List(Of DTOPurchaseOrderItem) = Me.Items
            For Each srcItem As DTOPurchaseOrderItem In oSrcOrder.Items
                Dim item As New DTOPurchaseOrderItem
                With item
                    .PurchaseOrder = _Order
                    .Sku = srcItem.Sku
                    If FEB.ProductSku.Load(.Sku, exs) Then
                        .Qty = srcItem.Qty
                        .Pending = srcItem.Qty
                        .Price = .Sku.Price
                        .Dto = .Sku.CustomerDto
                        .RepCom = Await FEB.RepCom.GetRepCom(Current.Session.Emp, _Order.Customer, .Sku, _Fch)
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End With
                items.Add(item)
            Next

            _Order.Items = items
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            refresca()
        Else
            UIHelper.WarnError(exs)
        End If

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

    Private Async Sub Do_RecalcRepComs(sender As Object, e As System.EventArgs)
        Await FEB.PurchaseOrderItems.ReasignaComisions(Current.Session.Emp, Me.Items)
        refresca()
        RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_EditRepComs()
        Dim oRow As DataGridViewRow = MyBase.CurrentRow
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        Dim oPurchaseOrderItem As DTOPurchaseOrderItem = oControlItem.Source
        Dim oFrm As New Frm_RepCom(oPurchaseOrderItem)
        AddHandler oFrm.AfterUpdate, AddressOf onRepComEdited
        oFrm.Show()
    End Sub

    Private Sub onRepComEdited(sender As Object, e As MatEventArgs)
        Dim item As DTOPurchaseOrderItem = e.Argument
        For Each oControlItem In _ControlItems
            If oControlItem.Source.Guid.Equals(item.Guid) Then
                If item.RepCom Is Nothing Then
                    oControlItem.Rep = ""
                    oControlItem.Com = 0
                Else
                    oControlItem.Rep = item.RepCom.Rep.NickName
                    oControlItem.Com = item.RepCom.Com
                End If
                MyBase.Refresh()
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            End If
        Next
    End Sub

    Private Sub Xl_PurchaseOrderItems_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles Me.RowsAdded
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


    Private Sub Xl_PurchaseOrderItems_UserDeletingRow(sender As Object, e As DataGridViewRowCancelEventArgs) Handles Me.UserDeletingRow
    End Sub

    Private Sub Xl_PurchaseOrderItems_UserDeletedRow(sender As Object, e As DataGridViewRowEventArgs) Handles Me.UserDeletedRow
    End Sub


    Private Sub dgv_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles Me.EditingControlShowing
        AddHandler CType(e.Control, TextBox).KeyDown, AddressOf TextBox_KeyDown
    End Sub

    Private Sub TextBox_KeyDown(sender As Object, e As KeyEventArgs)
        Select Case e.KeyCode
            Case Keys.F1
                Stop
                Dim oTextBox As TextBox = sender

                If IsNumeric(oTextBox.Text) Then
                    If _CellEventArgs IsNot Nothing Then
                        Me.Cursor = Cursors.Default
                        Application.DoEvents()
                        MatDataGridView1_CellValueChanged(Me, _CellEventArgs)
                    End If

                    Dim skuId = oTextBox.Text
                Else
                    UIHelper.WarnError(oTextBox.Text & " no es una referencia M+O de producte")
                End If
        End Select

    End Sub

    Private Async Sub Xl_PurchaseOrderItems_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Dim exs As New List(Of Exception)
        Select Case e.KeyCode
            Case Keys.Delete
                Dim oRow As DataGridViewRow = MyBase.CurrentRow
                Dim oControlitem As ControlItem = oRow.DataBoundItem
                Dim oItem As DTOPurchaseOrderItem = oControlitem.Source
                Dim iUnitatsSortides = Await FEB.PurchaseOrderItem.UnitatsSortides(exs, oItem)
                If exs.Count = 0 Then
                    If iUnitatsSortides > 0 Then
                        UIHelper.WarnError("no es poden eliminar linies de comanda de les que ja ha sortit mercancia")
                        e.Handled = True
                    End If
                Else
                    UIHelper.WarnError(exs)
                End If
        End Select

    End Sub


#Region "DragDrop"

    Private mLastMouseDownRectangle As System.Drawing.Rectangle

    Private Sub DataGridView1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        Dim iInterval As Integer = 1
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            Dim hit As DataGridView.HitTestInfo = sender.HitTest(e.X, e.Y)
            If hit.RowIndex >= 0 And hit.ColumnIndex >= 0 Then
                mLastMouseDownRectangle = New Rectangle(e.X - iInterval, e.Y - iInterval, 2 * iInterval, 2 * iInterval)
            End If
        End If
    End Sub

    Private Sub DataGridView1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            If Not mLastMouseDownRectangle.Contains(e.X, e.Y) Then
                'Dim hit As DataGridView.HitTestInfo = sender.HitTest(mLastMouseDownRectangle.X, mLastMouseDownRectangle.Y)
                If MyBase.SelectedRows.Count > 0 Then ' hit.RowIndex >= 0 And hit.ColumnIndex >= 0 Then
                    'MyBase.CurrentCell = sender.Rows(hit.RowIndex).Cells(hit.ColumnIndex)
                    'Dim oRow As DataGridViewRow = MyBase.CurrentRow

                    Dim oControlItems As New List(Of ControlItem)
                    For Each oRow In MyBase.SelectedRows
                        oControlItems.Add(oRow.DataBoundItem)
                    Next
                    sender.DoDragDrop(oControlItems, DragDropEffects.Move)
                    'Dim oImportacio As DTOImportacio = CurrentControlItem.Source
                    'sender.DoDragDrop(oImportacio, DragDropEffects.Copy)
                End If
            End If
        End If
    End Sub

    Private Sub DataGridView1_DragEnter(ByVal sender As Object, ByVal e As DragEventArgs) Handles MyBase.DragEnter
        If (e.Data.GetDataPresent(GetType(List(Of DTOProductSku)))) Then
            e.Effect = DragDropEffects.Copy
        ElseIf e.Data.GetDataPresent(GetType(List(Of ControlItem))) Then
            e.Effect = DragDropEffects.Move
        ElseIf e.Data.GetDataPresent(DataFormats.FileDrop, False) Then
            e.Effect = DragDropEffects.Copy
        ElseIf (e.Data.GetDataPresent("FileGroupDescriptor")) Then
            e.Effect = DragDropEffects.Copy
        ElseIf (e.Data.GetDataPresent(GetType(List(Of ControlItem)))) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub DataGridView1_DragDrop(ByVal sender As Object, ByVal e As DragEventArgs) Handles MyBase.DragDrop
        If e.Data.GetDataPresent(GetType(List(Of DTOProductSku))) Then
            Dim oSkus As List(Of DTOProductSku) = e.Data.GetData(GetType(List(Of DTOProductSku)))
            Dim items As List(Of DTOPurchaseOrderItem) = Me.Items
            For Each oSku As DTOProductSku In oSkus
                Dim item As New DTOPurchaseOrderItem()
                With item
                    .PurchaseOrder = _Order
                    .Sku = oSku
                    .Qty = oSku.Stock
                    .Pending = .Qty
                End With
                items.Add(item)
            Next
            _Order.Items = items
            refresca()

        ElseIf e.Data.GetDataPresent(GetType(List(Of ControlItem))) Then
            Dim oControlItems As List(Of ControlItem) = e.Data.GetData(GetType(List(Of ControlItem)))
            If oControlItems IsNot Nothing AndAlso oControlItems.Count > 0 Then
                If (e.Effect = DragDropEffects.Move) Then
                    Dim p As Point = MyBase.PointToClient(New Point(e.X, e.Y))
                    Dim dragIndex = MyBase.HitTest(p.X, p.Y).RowIndex
                    If dragIndex < 0 Then dragIndex = MyBase.RowCount - 1

                    Dim targetRow As DataGridViewRow = MyBase.Rows(dragIndex)
                    Dim oTargetControlItem As ControlItem = targetRow.DataBoundItem
                    Dim oTargetTruck As Truck = Nothing
                    If oTargetControlItem Is Nothing OrElse oTargetControlItem.Truck Is Nothing Then
                        oTargetTruck = _Trucks.Last()
                    Else
                        oTargetTruck = oTargetControlItem.Truck
                    End If
                    For Each oControlItem In oControlItems
                        If oControlItem IsNot Nothing AndAlso oControlItem.LinCod = ControlItem.LinCods.Item Then
                            Dim oItem = oControlItem.Source
                            If oControlItem.Truck IsNot Nothing Then
                                oControlItem.Truck.Items.Remove(oItem)
                                oTargetTruck.Items.Add(oItem)
                            End If
                        End If
                    Next
                End If

            End If
            refresca()

        ElseIf e.Data.GetDataPresent(DataFormats.FileDrop, False) Then
            Dim exs As New List(Of Exception)
            Dim oDocFiles As New List(Of DTODocFile)
            MyBase.ToggleProgressBarRequest(Me, New MatEventArgs(True))
            If DragDropHelper.GetDroppedDocFiles(e, oDocFiles, exs) Then
                Dim oSheet As MatHelper.Excel.Sheet = Nothing
                Dim sFilename = MatHelper.Excel.ClosedXml.SaveExcelStream(exs, oDocFiles.First.Stream)
                If exs.Count = 0 Then
                    Dim sFields = {"Ean", "Quantitat", "Linia"}
                    Dim oFrm As New Frm_ExcelColumsMapping(sFields, sFilename)
                    AddHandler oFrm.AfterUpdate, AddressOf onImportExcel
                    oFrm.Show()
                    MyBase.ToggleProgressBarRequest(Me, New MatEventArgs(False))
                Else
                    MyBase.ToggleProgressBarRequest(Me, New MatEventArgs(False))
                    UIHelper.WarnError(exs, "error al llegir l'Excel")
                End If
            Else
                MyBase.ToggleProgressBarRequest(Me, New MatEventArgs(False))
                UIHelper.WarnError(exs, "error al importar fitxers")
            End If

        ElseIf (e.Data.GetDataPresent("FileGroupDescriptor")) Then
            Dim exs As New List(Of Exception)
            Dim oDocFiles As New List(Of DTODocFile)
            MyBase.ToggleProgressBarRequest(Me, New MatEventArgs(True))
            If DragDropHelper.GetDroppedDocFiles(e, oDocFiles, exs) Then
                Dim oSheet As MatHelper.Excel.Sheet = Nothing
                Dim sFilename = MatHelper.Excel.ClosedXml.SaveExcelStream(exs, oDocFiles.First.Stream)
                If exs.Count = 0 Then
                    Dim sFields = {"Ean", "Quantitat", "Linia"}
                    Dim oFrm As New Frm_ExcelColumsMapping(sFields, sFilename)
                    AddHandler oFrm.AfterUpdate, AddressOf onImportExcel
                    oFrm.Show()
                    MyBase.ToggleProgressBarRequest(Me, New MatEventArgs(False))
                Else
                    MyBase.ToggleProgressBarRequest(Me, New MatEventArgs(False))
                    UIHelper.WarnError(exs, "error al llegir l'Excel")
                End If
            Else
                MyBase.ToggleProgressBarRequest(Me, New MatEventArgs(False))
                UIHelper.WarnError(exs, "error al importar fitxers")
            End If
        End If
    End Sub



    Private Sub Xl_PurchaseOrderItems_PriceListLoaded(sender As Object, e As MatEventArgs) Handles Me.PriceListLoaded
        If _CellEventArgs IsNot Nothing Then
            Me.Cursor = Cursors.Default
            Application.DoEvents()
            MatDataGridView1_CellValueChanged(Me, _CellEventArgs)
        End If
    End Sub

    Private Sub Xl_PurchaseOrderItems_CellToolTipTextNeeded(sender As Object, e As DataGridViewCellToolTipTextNeededEventArgs) Handles Me.CellToolTipTextNeeded
        If e.RowIndex >= 0 Then
            Dim oRow = MyBase.Rows(e.RowIndex)
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            If oControlItem IsNot Nothing Then
                If oControlItem.LinCod = ControlItem.LinCods.Item Then
                    Dim oItem = oControlItem.Source
                    If oItem.ErrCod = DTOPurchaseOrderItem.ErrCods.Success Then
                        Select Case e.ColumnIndex
                            Case Cols.Stock
                                Dim oCell = oRow.Cells(e.ColumnIndex)
                                If oControlItem IsNot Nothing AndAlso oControlItem.Source IsNot Nothing AndAlso oControlItem.Source.Sku IsNot Nothing Then

                                    If oControlItem.Source.Sku.ClientsBlockStock = 0 Then
                                        oCell.ToolTipText = String.Format("{0} unitats en stock", oControlItem.Source.Sku.Stock)
                                    Else
                                        oCell.ToolTipText = String.Format("{0} unitats en stock - {1} unitats bloquejades", oControlItem.Source.Sku.Stock, oControlItem.Source.Sku.ClientsBlockStock)
                                    End If
                                End If
                            Case Cols.Customers
                                Dim oCell = oRow.Cells(e.ColumnIndex)
                                If oControlItem IsNot Nothing AndAlso oControlItem.Source IsNot Nothing AndAlso oControlItem.Source.Sku IsNot Nothing Then
                                    Dim sb As New Text.StringBuilder
                                    sb.AppendFormat("{0} pendents de clients", oControlItem.Source.Sku.Clients)
                                    If oControlItem.Source.Sku.ClientsAlPot <> 0 Then sb.AppendFormat(" - {0} al pot", oControlItem.Source.Sku.ClientsAlPot)
                                    If oControlItem.Source.Sku.ClientsEnProgramacio <> 0 Then sb.AppendFormat(" - {0} en programació", oControlItem.Source.Sku.ClientsEnProgramacio)
                                    If oControlItem.Source.Sku.ClientsBlockStock <> 0 Then sb.AppendFormat(" - {0} de stock bloquejat", oControlItem.Source.Sku.ClientsBlockStock)
                                    oCell.ToolTipText = sb.ToString
                                End If

                        End Select
                    Else
                        e.ToolTipText = If(oItem.ErrDsc, oItem.ErrCod.ToString)
                    End If
                End If
            End If
        End If
    End Sub

#End Region

    Public Class Truck
        Property Id As Integer
        Property Items As List(Of Truck.Item)

        Public Sub New()
            _Items = New List(Of Item)
        End Sub

        Public Function Volume() As Decimal
            Return _Items.Sum(Function(x) x.VolumeM3)
        End Function

        Public Function Amt() As DTOAmt
            Dim retval As DTOAmt = DTOAmt.Factory()
            For Each oItem In _Items
                retval.Add(oItem.Amount())

            Next
            Return retval
        End Function

        Public Function Caption() As String
            Dim retval = String.Format("#{0} vol: {1:0.00 \m3} import: {2}", _Id, Volume(), Amt.Formatted)
            Return retval
        End Function

        Public Class Item
            Inherits DTOPurchaseOrderItem
            Property Truck As Truck

            Shared Shadows Function Factory(oItem As DTOPurchaseOrderItem, oTruck As Truck)
                Dim retval As New Item()
                With retval
                    .Guid = oItem.Guid
                    .Bundle = oItem.Bundle
                    .ChargeCod = oItem.ChargeCod
                    .CustomLin = oItem.CustomLin
                    .DeliveredQty = oItem.DeliveredQty
                    .Deliveries = oItem.Deliveries
                    .Dto = oItem.Dto
                    .ErrCod = oItem.ErrCod
                    .ErrDsc = oItem.ErrDsc
                    .ETD = oItem.ETD
                    .Incentius = oItem.Incentius
                    .IsNew = oItem.IsNew
                    .IsLoaded = oItem.IsLoaded
                    .Lin = oItem.Lin
                    .Pending = oItem.Pending
                    .Price = oItem.Price
                    .PurchaseOrder = oItem.PurchaseOrder
                    .Qty = oItem.Qty
                    .RepCom = oItem.RepCom
                    .Sku = oItem.Sku
                    .Truck = oTruck
                End With
                Return retval
            End Function
        End Class
    End Class

    Protected Class ControlItem
        Property Source As DTOPurchaseOrderItem
        Property Truck As Truck
        Property SkuId As Nullable(Of Integer)
        Property SkuNom As String
        Property Qty As Integer
        Property Ico As Icons
        Property Divisa As Decimal
        Property Price As Decimal
        Property Discount As Decimal
        Property Amt As Decimal
        Property Pending As Integer
        Property Stock As Integer

        Property SkuStock As Models.SkuStock
        Property Customers As Integer
        Property Previsio As String

        Property Rep As String
        Property Com As Decimal

        Property M3 As Decimal
        Property xM3 As Decimal
        Property Lin As Integer
        'Property IsVirtual As Boolean
        Property LinCod As LinCods

        Public Enum Icons
            Empty
        End Enum

        Public Enum Visibilities
            None
            Collapsed
            Expanded
            Visible
            Hidden
        End Enum

        Public Enum LinCods
            Item
            Truck
        End Enum


        Public Sub New(item As DTOPurchaseOrderItem, oCod As DTOPurchaseOrder.Codis, oCache As Models.ClientCache)
            MyBase.New()
            _LinCod = LinCods.Item
            Load(item, oCod, oCache)
        End Sub

        Public Sub New(oTruck As Truck)
            MyBase.New()
            _LinCod = LinCods.Truck
            _Truck = oTruck
            SkuNom = oTruck.Caption()
        End Sub

        Shared Function MissingTruck() As ControlItem
            Dim retval As New ControlItem
            retval.LinCod = LinCods.Truck
            retval.SkuNom = "(sense adjudicar camió)"
            Return retval
        End Function

        Public Sub Load(item As DTOPurchaseOrderItem, oCod As DTOPurchaseOrder.Codis, oCache As Models.ClientCache)
            _Source = item
            If TypeOf item Is Truck.Item Then
                _Truck = CType(item, Truck.Item).Truck
            End If

            If item.Sku IsNot Nothing AndAlso item.Sku.Id <> 0 Then
                _SkuId = item.Sku.Id
            End If

            Select Case oCod
                Case DTOPurchaseOrder.Codis.proveidor
                    _SkuNom = item.Sku.refYNomPrv
                Case Else
                    If item.Sku IsNot Nothing Then
                        _SkuNom = item.Sku.RefYNomLlarg.Tradueix(Current.Session.Lang)
                    End If
            End Select

            _Qty = item.Qty
            _Pending = item.Pending
            If item.Sku IsNot Nothing Then
                '_IsVirtual = item.Sku.virtual
                _SkuStock = oCache.SkuStock(item.Sku.Guid)
                _Stock = _SkuStock.Stock - _SkuStock.ClientsBlockStock 'seguin criteri de Victoria 04/04/22
                _Customers = _SkuStock.Clients - _SkuStock.ClientsAlPot - _SkuStock.ClientsEnProgramacio - _SkuStock.ClientsBlockStock
            End If

            If item.ChargeCod = DTOPurchaseOrderItem.ChargeCods.chargeable Then
                _Discount = item.Dto
                If item.Price IsNot Nothing Then
                    _Divisa = Math.Round(item.Price.Val, 2)
                    _Price = Math.Round(item.Price.Eur, 2)
                    _Amt = item.Amount.Val
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

            _M3 = item.Sku.VolumeM3OrInherited() ' DTOProductSku.VolumeM3OrInherited(item.sku)
            _xM3 = item.VolumeM3
            _Lin = item.CustomLin

        End Sub

        Public Sub New()
            MyBase.New()
            _Source = New DTOPurchaseOrderItem
        End Sub

        Function StockBackColor() As System.Drawing.Color
            Dim retval As System.Drawing.Color = System.Drawing.Color.White
            If _Source IsNot Nothing AndAlso _Source.Sku IsNot Nothing Then
                If _Source.Sku.NoStk Then
                    retval = Color.LightGreen
                Else
                    If _Pending = 0 Then
                        If _Stock <= 0 Then
                            If _Source.Sku.obsoleto Then
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
                        Dim iCustomers As Integer = _Customers
                        If _Source.PurchaseOrder IsNot Nothing Then
                            If _Source.PurchaseOrder.IsNew Then
                                iCustomers += _Pending
                            End If
                        End If

                        If _Stock <= 0 Then
                            If _Source.Sku.obsoleto Then
                                retval = Color.LightGray
                            Else
                                retval = Color.LightSalmon
                            End If
                        ElseIf _Stock >= iCustomers Then
                            retval = Color.LightGreen
                        Else
                            retval = Color.Yellow
                        End If
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

