Public Class Xl_PurchaseOrderItemsForPlatforms
    Private _Orders As List(Of DTOPurchaseOrder)
    Private _StocksAvailable As List(Of DTOStockAvailable)
    Private _ControlItems As ControlItems
    Private _HighliteSku As DTOProductSku
    Private _HighliteOrder As DTOPurchaseOrder
    Private _HighlitePlatform As DTOCustomerPlatform
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private BCOLORBLANK As Color = Color.FromArgb(240, 240, 240)
    Private BCOLORPDC As Color = Color.FromArgb(153, 255, 255)
    Private BCOLORSPV As Color = Color.FromArgb(224, 255, 255)
    Private BCOLOROBS As Color = Color.FromArgb(255, 255, 153)
    Private BCOLORITM As Color = Color.FromArgb(250, 250, 250)

    Private Enum Cols
        FchOrQty
        Txt
        Out
        Pvp
        Dto
        Amt
        Stk
        Pn2
    End Enum

    Public Shadows Sub Load(oPurchaseOrders As List(Of DTOPurchaseOrder), oStocksAvailable As List(Of DTOStockAvailable))
        _Orders = oPurchaseOrders
        _StocksAvailable = oStocksAvailable
        _ControlItems = New ControlItems
        LoadControlItems()
        LoadStocks()
        LoadGrid()
    End Sub



    Public ReadOnly Property StocksAvailable As List(Of DTOStockAvailable)
        Get
            Return _StocksAvailable
        End Get
    End Property


    Public Sub selectSku(oSku As DTOProductSku)
        _HighliteSku = oSku
        MatDataGridView1.Refresh()

        Dim iVisibleRowsCount = MatDataGridView1.DisplayedRowCount(False)
        Dim iFirstDisplayedRowIndex = 0
        If MatDataGridView1.FirstDisplayedCell IsNot Nothing Then
            iFirstDisplayedRowIndex = MatDataGridView1.FirstDisplayedCell.RowIndex
        End If
        Dim iLastDisplayedRowIndex = iFirstDisplayedRowIndex + iVisibleRowsCount - 4

        Dim oFirstRowToDisplay As DataGridViewRow = Nothing

        For Each oRow As DataGridViewRow In MatDataGridView1.Rows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            Select Case oControlItem.LinCod
                Case ControlItem.LinCods.Client
                    oFirstRowToDisplay = oRow
                Case ControlItem.LinCods.Item
                    If DirectCast(oControlItem.Source, DTOPurchaseOrderItem).Sku.Guid = oSku.Guid Then
                        If oFirstRowToDisplay.Index < iFirstDisplayedRowIndex Or oFirstRowToDisplay.Index > iLastDisplayedRowIndex Then
                            MatDataGridView1.FirstDisplayedCell = oFirstRowToDisplay.Cells(Cols.Txt)
                        End If
                        Exit For
                    End If
            End Select
        Next

    End Sub

    Public Sub selectOrder(oOrder As DTOPurchaseOrder)
        _HighliteOrder = oOrder
        MatDataGridView1.Refresh()

        Dim iVisibleRowsCount = MatDataGridView1.DisplayedRowCount(False)
        Dim iFirstDisplayedRowIndex = 0
        If MatDataGridView1.FirstDisplayedCell IsNot Nothing Then
            iFirstDisplayedRowIndex = MatDataGridView1.FirstDisplayedCell.RowIndex
        End If
        Dim iLastDisplayedRowIndex = iFirstDisplayedRowIndex + iVisibleRowsCount - 4

        Dim oFirstRowToDisplay As DataGridViewRow = Nothing

        For Each oRow As DataGridViewRow In MatDataGridView1.Rows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            Select Case oControlItem.LinCod
                Case ControlItem.LinCods.Client
                    oFirstRowToDisplay = oRow
                Case ControlItem.LinCods.Comanda
                    If DirectCast(oControlItem.Source, DTOPurchaseOrder).Equals(oOrder) Then
                        If oFirstRowToDisplay.Index < iFirstDisplayedRowIndex Or oFirstRowToDisplay.Index > iLastDisplayedRowIndex Then
                            MatDataGridView1.FirstDisplayedCell = oFirstRowToDisplay.Cells(Cols.Txt)
                        End If
                        Exit For
                    End If
            End Select
        Next
    End Sub

    Public Sub selectPlatform(oPlatform As DTOCustomerPlatform)
        _HighlitePlatform = oPlatform
        MatDataGridView1.Refresh()

        Dim iVisibleRowsCount = MatDataGridView1.DisplayedRowCount(False)
        Dim iFirstDisplayedRowIndex As Integer
        Dim iLastDisplayedRowIndex As Integer
        If MatDataGridView1.FirstDisplayedCell IsNot Nothing Then
            iFirstDisplayedRowIndex = MatDataGridView1.FirstDisplayedCell.RowIndex
            iLastDisplayedRowIndex = iFirstDisplayedRowIndex + iVisibleRowsCount - 4
        End If

        For Each oRow As DataGridViewRow In MatDataGridView1.Rows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            If oControlItem.LinCod = ControlItem.LinCods.Client Then
                If DirectCast(oControlItem.Source, DTOPurchaseOrder).Platform.Guid = oPlatform.Guid Then
                    If oRow.Index < iFirstDisplayedRowIndex Or oRow.Index > iLastDisplayedRowIndex Then
                        MatDataGridView1.FirstDisplayedCell = oRow.Cells(Cols.Txt)
                    End If
                    Exit For
                End If
            End If
        Next
    End Sub

    Public ReadOnly Property Deliveries As List(Of DTODelivery)
        Get
            Dim retval As New List(Of DTODelivery)
            Dim oPurchaseOrder As New DTOPurchaseOrder
            Dim oMgz As DTOMgz = Current.Session.Emp.Mgz
            Dim oDelivery As DTODelivery = Nothing
            For Each oControlItem As ControlItem In _ControlItems
                If oControlItem.LinCod = ControlItem.LinCods.Item And oControlItem.Out > 0 Then
                    Dim oPurchaseOrderItem As DTOPurchaseOrderItem = oControlItem.Source
                    If Not oPurchaseOrder.Guid.Equals(oPurchaseOrderItem.purchaseOrder.Guid) Then
                        oPurchaseOrder = oPurchaseOrderItem.PurchaseOrder
                        oDelivery = DTODelivery.FactoryElCorteIngles(oPurchaseOrder, oMgz, DTO.GlobalVariables.Today(), Current.Session.User)
                        With oDelivery
                            .cashCod = DTOCustomer.CashCodes.credit
                            .portsCod = DTOCustomer.PortsCodes.Pagats
                            .valorado = .customer.albValorat
                        End With
                        retval.Add(oDelivery)
                    End If
                    Dim oItem As DTODeliveryItem = DTODeliveryItem.Factory(oPurchaseOrderItem, oControlItem.Out)
                    oDelivery.items.Add(oItem)
                End If
            Next
            Return retval
        End Get
    End Property

    Public ReadOnly Property DeliveryItems As List(Of DTODeliveryItem)
        Get
            Dim retval As New List(Of DTODeliveryItem)
            Dim oPurchaseOrder As New DTOPurchaseOrder
            Dim oMgz As DTOMgz = Current.Session.Emp.Mgz
            Dim oDelivery As DTODelivery = Nothing
            For Each oControlItem As ControlItem In _ControlItems
                If oControlItem.LinCod = ControlItem.LinCods.Item And oControlItem.Out > 0 Then
                    Dim oPurchaseOrderItem As DTOPurchaseOrderItem = oControlItem.Source
                    Dim oItem As DTODeliveryItem = DTODeliveryItem.Factory(oPurchaseOrderItem, oControlItem.Out)
                    retval.Add(oItem)
                End If
            Next
            Return retval
        End Get
    End Property

    Public Function Platforms() As List(Of DTOCustomerPlatform)
        Dim retval As List(Of DTOCustomerPlatform) = Deliveries.GroupBy(Function(x) x.platform.Guid).Select(Function(y) y.First.platform).ToList
        For Each oPlatform As DTOCustomerPlatform In retval
            oPlatform.Deliveries = New List(Of DTODelivery)
            oPlatform.BaseImponible = DTOAmt.Empty
        Next
        'Dim retval As New List(Of DTOCustomerPlatform)
        For Each oItem As DTODelivery In Deliveries
            Dim oPlatform As DTOCustomerPlatform = retval.Find(Function(x) x.Equals(oItem.platform))
            'oPlatform.Deliveries = New List(Of DTODelivery)
            'If Not retval.Exists(Function(x) x.Guid.Equals(oItem.Platform.Guid)) Then
            'retval.Add(oItem.Platform)
            'End If
            oPlatform.BaseImponible.Add(DTODelivery.BaseImponible(oItem))
            oPlatform.Deliveries.Add(oItem)
        Next
        Return retval
    End Function

    Private Sub LoadControlItems()
        For Each oPurchaseOrder As DTOPurchaseOrder In _Orders.OrderBy(Function(x) x.fch).OrderBy(Function(x) x.fchDeliveryMin)
            Dim oControlItem As ControlItem = Nothing
            oControlItem = New ControlItem(oPurchaseOrder, ControlItem.LinCods.Blank, "")
            _ControlItems.Add(oControlItem)
            oControlItem = New ControlItem(oPurchaseOrder, ControlItem.LinCods.Client, oPurchaseOrder.contact.FullNom)
            _ControlItems.Add(oControlItem)
            oControlItem = New ControlItem(oPurchaseOrder, ControlItem.LinCods.Platform, "Plataforma: " & oPurchaseOrder.platform.nom)
            _ControlItems.Add(oControlItem)
            oControlItem = New ControlItem(oPurchaseOrder, ControlItem.LinCods.Comanda, oPurchaseOrder.concept)
            _ControlItems.Add(oControlItem)
            If oPurchaseOrder.obs > "" Then
                oControlItem = New ControlItem(oPurchaseOrder, ControlItem.LinCods.Obs, oPurchaseOrder.obs)
                _ControlItems.Add(oControlItem)
            End If
            For Each oItem As DTOPurchaseOrderItem In oPurchaseOrder.items
                oControlItem = New ControlItem(oItem)
                _ControlItems.Add(oControlItem)
            Next
        Next
    End Sub

    Private Sub LoadStocks()
        'posa a zero
        For Each oStock As DTOStockAvailable In _StocksAvailable
            oStock.AvailableStock = oStock.OriginalStock
            oStock.Pendent = 0
        Next

        For Each oControlItem As ControlItem In _ControlItems
            Select Case oControlItem.LinCod
                Case ControlItem.LinCods.Item

                    Dim oItem As DTOPurchaseOrderItem = oControlItem.Source
                    Dim oStock As DTOStockAvailable = _StocksAvailable.FirstOrDefault(Function(x) x.Sku.Guid.Equals(oItem.sku.Guid))
                    If oStock IsNot Nothing Then
                        oControlItem.Pn2 = oStock.Clients
                        oControlItem.Stk = oStock.AvailableStock
                        Dim iOut As Integer
                        Select Case oStock.AvailableStock
                            Case Is <= 0
                                iOut = 0
                            Case Is < oItem.pending
                                iOut = oStock.AvailableStock
                                oStock.AvailableStock = 0
                            Case Is >= oItem.pending
                                iOut = oItem.pending
                                oStock.AvailableStock -= oItem.pending
                        End Select
                        oControlItem.Out = iOut
                        oControlItem.SetAmt()
                        oStock.Pendent += oItem.pending
                    End If
            End Select
        Next
    End Sub

    Private Sub ReLoadStocks()
        'igual que LoadStocks pero sense tocar pendents ni sortides
        For Each oStock As DTOStockAvailable In _StocksAvailable
            oStock.AvailableStock = oStock.OriginalStock
            'oStock.Pendent = oStock.Clients
        Next
        For Each oControlItem As ControlItem In _ControlItems
            Select Case oControlItem.LinCod
                Case ControlItem.LinCods.Item
                    Dim oItem As DTOPurchaseOrderItem = oControlItem.Source
                    Dim oStock As DTOStockAvailable = _StocksAvailable.Find(Function(x) x.Sku.Guid.Equals(oItem.Sku.Guid))
                    If oStock IsNot Nothing Then
                        oControlItem.Stk = oStock.AvailableStock
                        'oControlItem.Pn2 = oStock.Pendent
                        oStock.AvailableStock -= oControlItem.Out
                        'oStock.Pendent -= oControlItem.Out
                    End If
                    oControlItem.SetAmt()
            End Select
        Next
        MatDataGridView1.Refresh()
    End Sub


    Private Sub LoadGrid()
        _AllowEvents = False
        With MatDataGridView1
            With .RowTemplate
                .Height = MatDataGridView1.Font.Height * 1.3
            End With
            .DataSource = _ControlItems
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .ReadOnly = False
            .Columns.Clear()
            .Columns.Add(New TabStopTextBoxColumn)
            With DirectCast(.Columns(Cols.FchOrQty), TabStopTextBoxColumn)
                .HeaderText = "pendent"
                .DataPropertyName = "FchOrQty"
                .ReadOnly = True
                .TabStop = False
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            .Columns.Add(New TabStopTextBoxColumn)
            With DirectCast(.Columns(Cols.Txt), TabStopTextBoxColumn)
                .HeaderText = "concepte"
                .DataPropertyName = "txt"
                .ReadOnly = True
                .TabStop = False
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            .Columns.Add(New TabStopTextBoxColumn)
            With DirectCast(.Columns(Cols.Out), TabStopTextBoxColumn)
                .HeaderText = "sortida"
                .DataPropertyName = "out"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,###"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .ReadOnly = False
                .TabStop = True
            End With
            .Columns.Add(New TabStopTextBoxColumn)
            With DirectCast(.Columns(Cols.Pvp), TabStopTextBoxColumn)
                .HeaderText = "preu"
                .DataPropertyName = "pvp"
                .ReadOnly = True
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .TabStop = False
            End With
            .Columns.Add(New TabStopTextBoxColumn)
            With DirectCast(.Columns(Cols.Dto), TabStopTextBoxColumn)
                .HeaderText = "dte"
                .DataPropertyName = "dto"
                .ReadOnly = True
                .Width = 35
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#.#\%;-#.#\%;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .TabStop = False
            End With
            .Columns.Add(New TabStopTextBoxColumn)
            With DirectCast(.Columns(Cols.Amt), TabStopTextBoxColumn)
                .HeaderText = "import"
                .DataPropertyName = "amt"
                .ReadOnly = True
                .TabStop = False
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.BackColor = BCOLORBLANK
            End With
            .Columns.Add(New TabStopTextBoxColumn)
            With DirectCast(.Columns(Cols.Stk), TabStopTextBoxColumn)
                .HeaderText = "stock"
                .DataPropertyName = "stk"
                .ReadOnly = True
                .TabStop = False
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,###"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.BackColor = BCOLORBLANK
            End With
            .Columns.Add(New TabStopTextBoxColumn)
            With DirectCast(.Columns(Cols.Pn2), TabStopTextBoxColumn)
                .HeaderText = "clients"
                .DataPropertyName = "pn2"
                .ReadOnly = True
                .TabStop = False
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,###"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.BackColor = BCOLORBLANK
            End With
        End With
        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function CurrentControlItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = MatDataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Async Function SetContextMenu() As Task
        Dim exs As New List(Of Exception)
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentControlItem()

        If oControlItem IsNot Nothing Then
            Select Case oControlItem.LinCod
                Case ControlItem.LinCods.Blank
                Case ControlItem.LinCods.Client
                    Dim oItem As DTOPurchaseOrder = oControlItem.Source
                    Dim oContactMenu = Await FEB.ContactMenu.Find(exs, oItem.Customer)
                    Dim oMenuContact As New Menu_Contact(oItem.Customer, oContactMenu)
                    oContextMenu.Items.AddRange(oMenuContact.Range)
                Case ControlItem.LinCods.Comanda
                    Dim oItem As DTOPurchaseOrder = oControlItem.Source
                    Dim oMenuPdc As New Menu_Pdc({oItem}.ToList)
                    oContextMenu.Items.AddRange(oMenuPdc.Range)
                Case ControlItem.LinCods.Item
                    Dim oItem As DTOPurchaseOrderItem = oControlItem.Source
                    Dim oMenu As New Menu_ProductSku(oItem.Sku)
                    oContextMenu.Items.AddRange(oMenu.Range)
            End Select
        End If

        MatDataGridView1.ContextMenuStrip = oContextMenu
    End Function

    Private Sub MatDataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles MatDataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Txt
                'If e.RowIndex = 3 Then Stop
                Dim oRow As DataGridViewRow = MatDataGridView1.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Select Case oControlItem.LinCod
                    Case ControlItem.LinCods.Client
                        Dim oBoldFont As New Font(e.CellStyle.Font, FontStyle.Bold)
                        oRow.DefaultCellStyle.Font = oBoldFont
                        oRow.DefaultCellStyle.BackColor = Color.Beige
                    Case ControlItem.LinCods.Platform
                        If _HighlitePlatform Is Nothing Then
                            oRow.DefaultCellStyle.BackColor = Color.Beige
                        Else
                            If oControlItem.Source.Platform.Guid.Equals(_HighlitePlatform.Guid) Then
                                oRow.DefaultCellStyle.BackColor = Color.LightGreen
                            Else
                                oRow.DefaultCellStyle.BackColor = Color.Beige
                            End If
                        End If
                    Case ControlItem.LinCods.Comanda
                        If _HighliteOrder Is Nothing Then
                            oRow.DefaultCellStyle.BackColor = Color.Beige
                        Else
                            If oControlItem.Source.Equals(_HighliteOrder) Then
                                oRow.DefaultCellStyle.BackColor = Color.Yellow
                            Else
                                oRow.DefaultCellStyle.BackColor = Color.Beige
                            End If
                        End If
                    Case ControlItem.LinCods.Obs
                        If _HighliteOrder Is Nothing Then
                            oRow.DefaultCellStyle.BackColor = Color.Beige
                        Else
                            If oControlItem.Source.Equals(_HighliteOrder) Then
                                oRow.DefaultCellStyle.BackColor = Color.Yellow
                            Else
                                oRow.DefaultCellStyle.BackColor = Color.Beige
                            End If
                        End If
                    Case ControlItem.LinCods.Item
                        If _HighliteSku Is Nothing Then
                            oRow.DefaultCellStyle.BackColor = Color.White
                        Else
                            If oControlItem.Source.Sku.Guid.Equals(_HighliteSku.Guid) Then
                                oRow.DefaultCellStyle.BackColor = Color.Yellow
                            Else
                                oRow.DefaultCellStyle.BackColor = Color.White
                            End If
                        End If

                    Case ControlItem.LinCods.Blank
                        oRow.DefaultCellStyle.BackColor = Color.White
                End Select
        End Select
    End Sub

    Private Sub MatDataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles MatDataGridView1.CellValueChanged
        If _AllowEvents Then
            Select Case e.ColumnIndex
                Case Cols.Out
                    ReLoadStocks()
                    RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            End Select
        End If
    End Sub

    Private Async Sub MatDataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MatDataGridView1.SelectionChanged
        If _AllowEvents Then Await SetContextMenu()
    End Sub

    Protected Class ControlItem
        Public Property Source As Object

        Public Property LinCod As LinCods
        Public Property FchOrQty As String
        Public Property Txt As String
        Public Property Out As Integer
        Public Property Pvp As Decimal
        Public Property Dto As Decimal
        Public Property Amt As Decimal
        Public Property Stk As Integer
        Public Property Pn2 As Integer

        Public Enum LinCods
            Blank
            Client
            Platform
            Comanda
            Item
            Obs
        End Enum

        Public Sub New(oPurchaseOrder As DTOPurchaseOrder, oLinCod As LinCods, sTxt As String)
            MyBase.New()
            _Source = oPurchaseOrder
            _LinCod = oLinCod
            _Txt = sTxt
            Select Case _LinCod
                Case LinCods.Client
                    If oPurchaseOrder.fchDeliveryMin <> Nothing Then
                        _FchOrQty = Format(oPurchaseOrder.fchDeliveryMin, "dd/MM/yy")
                    End If
                Case LinCods.Comanda
                    _FchOrQty = Format(oPurchaseOrder.fch, "dd/MM/yy")
                Case LinCods.Obs

            End Select
        End Sub

        Public Sub New(oPurchaseOrderItem As DTOPurchaseOrderItem)
            MyBase.New()
            _Source = oPurchaseOrderItem
            With oPurchaseOrderItem
                _Source = oPurchaseOrderItem
                _LinCod = LinCods.Item
                _FchOrQty = .pending
                _Txt = .sku.nomLlarg.Tradueix(Current.Session.Lang)
                _Pvp = .Price.Eur
                _Dto = .Dto
            End With
        End Sub

        Public Sub SetAmt()
            _Amt = DTOAmt.Import(_Out, DTOAmt.Factory(_Pvp), _Dto).Eur
        End Sub
    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class


End Class
