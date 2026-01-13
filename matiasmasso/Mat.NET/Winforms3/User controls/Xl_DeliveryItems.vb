Public Class Xl_DeliveryItems
    Inherits MatDataGridView

    Private _Delivery As DTODelivery
    Private _Items As List(Of DTODeliveryItem)
    Private _ControlItems As ControlItems

    Private _DirtyCell As Boolean
    Private _MenuItemEditPreus As ToolStripMenuItem
    Private _AllowEvents As Boolean

    Private BCOLORBLANK = System.Drawing.Color.FromArgb(240, 240, 240)
    Private BCOLORPDC = System.Drawing.Color.FromArgb(153, 255, 255)
    Private BCOLORSPV = System.Drawing.Color.FromArgb(224, 255, 255)
    Private BCOLOROBS = System.Drawing.Color.FromArgb(255, 255, 153)
    Private BCOLORITM = System.Drawing.Color.FromArgb(250, 250, 250)

    Private COLOR_STOCK = System.Drawing.Color.LightGreen
    Private COLOR_RESERVED = System.Drawing.Color.Yellow
    Private COLOR_NOSTOCK = System.Drawing.Color.Salmon
    Private COLOR_OBSOLETO = System.Drawing.Color.LightGray


    Public Event RequestToReload(sender As Object, e As MatEventArgs)
    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Event ToggleProgressBarRequest(visible As Boolean)


    Private Enum Cols
        Pnc
        SkuId
        Txt
        Ico
        Qty
        Price
        Dto
        Amt
        Stk
        Cli
    End Enum


    Public Shadows Async Function Load(oDelivery As DTODelivery) As Task
        _Delivery = oDelivery
        _Items = _Delivery.Items
        SetProperties()
        Await refresca()
        _AllowEvents = True
    End Function

    Public ReadOnly Property Items As List(Of DTODeliveryItem)
        Get
            Dim retval As New List(Of DTODeliveryItem)
            For Each oControlItem In _ControlItems
                If oControlItem.LinCod = ControlItem.LinCods.Itm AndAlso oControlItem.Qty <> 0 Then
                    Dim item As DTODeliveryItem = oControlItem.Source

                    If item.bundle.Count > 0 Then
                        For Each oChildItem In item.bundle
                            oChildItem.qty = item.qty
                            oChildItem.dto = item.dto
                            oChildItem.repCom = item.repCom
                        Next
                    End If

                    'item.Qty = oControlItem.Qty
                    'If item.Price.Val <> oControlItem.Price Then
                    'item.Price = DTOCurExchangeRate.AmtFromDivisa(oControlItem.Price, item.Price.Cur)
                    'End If
                    'item.Dto = oControlItem.Dto

                    retval.Add(item)
                End If
            Next
            Return retval
        End Get
    End Property

    Private Function CurrentItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = MyBase.CurrentRow
        If oRow IsNot Nothing Then retval = oRow.DataBoundItem
        Return retval
    End Function

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        If _Delivery.IsNew Then
            If _Delivery.Cod = DTOPurchaseOrder.Codis.Client Then
                Dim oItemsWithIncentius = Await FEB.DeliveryItems.SetIncentius(exs, _Delivery.Customer.CcxOrMe, _Items, Current.Session.User)
                If exs.Count = 0 Then
                    _Items = oItemsWithIncentius
                Else
                    UIHelper.WarnError(exs)
                End If
            End If
        Else
            _Items = _Delivery.Items
        End If


        Dim oPdc As New DTOPurchaseOrder
        Dim oSpv As New DTOSpv
        _ControlItems = New ControlItems

        For Each oItem In _Items
            Select Case _Delivery.Cod
                Case DTOPurchaseOrder.Codis.Reparacio
                    If oItem.Spv.UnEquals(oSpv) Then
                        LoadSpvHeader(oSpv, oItem)
                    End If
                Case DTOPurchaseOrder.Codis.Traspas
                Case Else
                    If oItem.purchaseOrderItem.purchaseOrder.UnEquals(oPdc) Then
                        oPdc = oItem.purchaseOrderItem.purchaseOrder
                        LoadPdcHeader(oPdc, oItem)
                    End If
            End Select

            _ControlItems.Add(New ControlItem(oItem, _Delivery.cod))
        Next

        MyBase.DataSource = _ControlItems

        For Each oRow As DataGridViewRow In MyBase.Rows
            Dim oControlitem As ControlItem = oRow.DataBoundItem
            oRow.ReadOnly = Not (oControlitem.LinCod = ControlItem.LinCods.Itm)
        Next
        SetContextMenu()
    End Function

    Private Sub LoadSpvHeader(ByRef oSpv As DTOSpv, oItem As DTODeliveryItem)
        oSpv = oItem.Spv
        If _ControlItems.Count > 0 Then _ControlItems.Add(New ControlItem)
        Dim SpvLines = oSpv.Lines(_Delivery.Customer.Lang)
        For i As Integer = 0 To SpvLines.Count - 1
            _ControlItems.Add(New ControlItem(oSpv, SpvLines, i))
        Next
    End Sub

    Private Sub LoadPdcHeader(ByRef oPdc As DTOPurchaseOrder, oItem As DTODeliveryItem)
        oPdc = oItem.PurchaseOrderItem.PurchaseOrder
        If _ControlItems.Count > 0 Then _ControlItems.Add(New ControlItem)
        _ControlItems.Add(New ControlItem(oPdc, _Delivery.Contact.lang))
        If oPdc.Obs > "" Then
            _ControlItems.Add(New ControlItem(oPdc.Obs))
        End If
        If oPdc.fchDeliveryMin <> Nothing Then
            _ControlItems.Add(New ControlItem(String.Format("no servir abans de {0:dd/MM/yy}", oPdc.fchDeliveryMin)))
        End If
        If oPdc.TotJunt <> Nothing Then
            _ControlItems.Add(New ControlItem("servir tot junt"))
        End If
    End Sub

    Private Sub SetProperties()
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            PropertiesSet = True

            MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
            'MyBase.RowTemplate.DefaultCellStyle.BackColor = Color.Transparent

            MyBase.AutoGenerateColumns = False
            MyBase.Columns.Clear()

            MyBase.SelectionMode = DataGridViewSelectionMode.CellSelect
            MyBase.ColumnHeadersVisible = True
            MyBase.RowHeadersVisible = False
            MyBase.MultiSelect = False
            MyBase.AllowUserToResizeRows = False
            MyBase.AllowDrop = False
            MyBase.ReadOnly = False

            MyBase.Columns.Add(New TabStopTextBoxColumn)
            With DirectCast(MyBase.Columns(Cols.Pnc), TabStopTextBoxColumn)
                .HeaderText = "Pendent"
                .DataPropertyName = "Pnc"
                .ReadOnly = True
                .TabStop = False
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
                .Visible = _Delivery.IsNew
            End With

            MyBase.Columns.Add(New TabStopTextBoxColumn)
            With DirectCast(MyBase.Columns(Cols.SkuId), TabStopTextBoxColumn)
                .HeaderText = "M+O"
                .DataPropertyName = "SkuId"
                .ReadOnly = True
                .TabStop = False
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
                .Visible = _Delivery.Cod = DTOPurchaseOrder.Codis.Proveidor
            End With

            MyBase.Columns.Add(New TabStopTextBoxColumn)
            With DirectCast(MyBase.Columns(Cols.Txt), TabStopTextBoxColumn)
                .HeaderText = "Concepte"
                .DataPropertyName = "Txt"
                .ReadOnly = True
                .TabStop = False
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            MyBase.Columns.Add(New TabStopImageColumn(False))
            With DirectCast(MyBase.Columns(Cols.Ico), TabStopImageColumn)
                .CellTemplate = New DataGridViewImageCellBlank(False)
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .ReadOnly = True
                .TabStop = False
                .Width = 16
                .DefaultCellStyle.NullValue = Nothing
                .ReadOnly = True
                .Visible = _Delivery.IsNew
            End With

            MyBase.Columns.Add(New TabStopTextBoxColumn)
            With DirectCast(MyBase.Columns(Cols.Qty), TabStopTextBoxColumn)
                .HeaderText = "Sortida"
                .DataPropertyName = "Qty"
                .ReadOnly = False
                .TabStop = True
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
            End With

            MyBase.Columns.Add(New TabStopTextBoxColumn)
            With DirectCast(MyBase.Columns(Cols.Price), TabStopTextBoxColumn)
                .HeaderText = "Preu"
                .DataPropertyName = "Price"
                .ReadOnly = True
                .TabStop = False
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With

            MyBase.Columns.Add(New TabStopTextBoxColumn)
            With DirectCast(MyBase.Columns(Cols.Dto), TabStopTextBoxColumn)
                .HeaderText = "Dte"
                .DataPropertyName = "Dto"
                .ReadOnly = True
                .TabStop = False
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#.#\%;-#.#\%;#"
            End With

            MyBase.Columns.Add(New TabStopTextBoxColumn)
            With DirectCast(MyBase.Columns(Cols.Amt), TabStopTextBoxColumn)
                .HeaderText = "Import"
                .DataPropertyName = "Amt"
                .ReadOnly = True
                .TabStop = False
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With

            MyBase.Columns.Add(New TabStopTextBoxColumn)
            With DirectCast(MyBase.Columns(Cols.Stk), TabStopTextBoxColumn)
                .HeaderText = "Stock"
                .DataPropertyName = "Stk"
                .ReadOnly = True
                .TabStop = False
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
                .Visible = _Delivery.IsNew
            End With

            MyBase.Columns.Add(New TabStopTextBoxColumn)
            With DirectCast(MyBase.Columns(Cols.Cli), TabStopTextBoxColumn)
                .HeaderText = "Clients"
                .DataPropertyName = "Cli"
                .ReadOnly = True
                .TabStop = False
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
                .Visible = _Delivery.IsNew
            End With

        End If
    End Sub

    Private Sub MatDataGridView1_RowPostPaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPostPaintEventArgs) Handles MyBase.RowPostPaint
        'merge datagridviewcells for epigrafs Cols.nom+cols.value
        Dim oBackgroundColor As System.Drawing.Color = Color.White

        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        Dim oControlitem As ControlItem = oRow.DataBoundItem

        Select Case oControlitem.LinCod
            Case ControlItem.LinCods.Blank
                oBackgroundColor = BCOLORBLANK
            Case ControlItem.LinCods.Pdc, ControlItem.LinCods.SpvFirstLine
                oBackgroundColor = BCOLORPDC
            Case ControlItem.LinCods.Obs
                oBackgroundColor = BCOLOROBS
            Case ControlItem.LinCods.SpvOtherLines
                oBackgroundColor = BCOLORSPV
        End Select

        Select Case oControlitem.LinCod
            Case ControlItem.LinCods.Pdc, ControlItem.LinCods.Obs, ControlItem.LinCods.Blank, ControlItem.LinCods.SpvFirstLine ', LinCods.Spv

                Dim X As Integer = e.RowBounds.Left ' MatDataGridView1.GetColumnDisplayRectangle(Cols.concepte, True).X
                Dim Y As Integer = e.RowBounds.Y
                Dim Width As Integer = e.RowBounds.Right - X
                Dim Height As Integer = e.RowBounds.Bottom - Y
                Dim oBrush As New SolidBrush(oBackgroundColor)
                Dim oMergeRectangle As New Rectangle(X, Y, Width, Height - 1)
                e.Graphics.FillRectangle(oBrush, oMergeRectangle)

                Dim s As String = oRow.Cells(Cols.Txt).Value
                oBrush = New SolidBrush(Color.Black)
                Dim oRectangle As New Rectangle(oMergeRectangle.X, oMergeRectangle.Y + 1, oMergeRectangle.Width, oMergeRectangle.Height)
                e.Graphics.DrawString(s, MyBase.Font, oBrush, oRectangle)
        End Select

    End Sub


    Private Async Sub MatDataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles MyBase.CellFormatting

        Select Case e.ColumnIndex
            Case Cols.Qty
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Select Case oControlItem.LinCod
                    Case ControlItem.LinCods.Itm
                        Dim item As DTODeliveryItem = oControlItem.Source
                        If item.purchaseOrderItem IsNot Nothing AndAlso item.purchaseOrderItem.ErrCod <> DTOPurchaseOrderItem.ErrCods.Success Then
                            e.CellStyle.BackColor = COLOR_OBSOLETO
                        Else
                            If _Delivery.IsNew And _Delivery.cod = DTOPurchaseOrder.Codis.client Then
                                Dim estaEnElPot As Boolean '= oRow.Cells(Cols.Pot).Value
                                If estaEnElPot Then
                                    e.CellStyle.BackColor = Color.FromArgb(255, 204, 255)
                                Else
                                    Dim BlNoStk As Boolean = DirectCast(oControlItem.Source, DTODeliveryItem).sku.noStk
                                    If BlNoStk Or oControlItem.Stk > oControlItem.Cli Then
                                        e.CellStyle.BackColor = COLOR_STOCK
                                    ElseIf oControlItem.Stk > 0 Then
                                        e.CellStyle.BackColor = COLOR_RESERVED
                                    Else
                                        e.CellStyle.BackColor = COLOR_NOSTOCK
                                    End If
                                End If

                            End If
                        End If
                End Select
            Case Cols.Ico
                If _Delivery.IsNew And _Delivery.cod = DTOPurchaseOrder.Codis.client Then
                    Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                    Dim oControlItem As ControlItem = oRow.DataBoundItem
                    e.Value = Await GetWarnIcon(oControlItem)
                Else
                    e.Value = My.Resources.empty
                End If
            Case Cols.Txt
                If _Delivery.cod = DTOPurchaseOrder.Codis.reparacio Then
                    Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                    Dim oControlItem As ControlItem = oRow.DataBoundItem
                    Select Case oControlItem.LinCod
                        Case ControlItem.LinCods.SpvOtherLines
                            e.CellStyle.Padding = New Padding(20, 0, 0, 0)
                            e.CellStyle.Font = New Font(e.CellStyle.Font, FontStyle.Italic)
                            e.CellStyle.BackColor = BCOLORSPV
                        Case ControlItem.LinCods.Itm
                            e.CellStyle.Padding = New Padding(40, 0, 0, 0)
                    End Select
                End If

            Case Cols.Price
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Select Case oControlItem.LinCod
                    Case ControlItem.LinCods.Itm
                        Dim oItem As DTODeliveryItem = oControlItem.Source
                        e.Value = DTOAmt.CurFormatted(oItem.price)
                End Select
            Case Cols.Amt
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Select Case oControlItem.LinCod
                    Case ControlItem.LinCods.Itm
                        Dim oItem As DTODeliveryItem = oControlItem.Source
                        e.Value = DTOAmt.CurFormatted(oItem.Import)
                End Select
            Case Cols.Dto
                If IsNumeric(e.Value) Then
                    If e.Value <> 0 Then
                        If CInt(e.Value) = e.Value Then
                            e.Value = Format(CDec(e.Value), "0") & "%"
                        Else
                            e.Value = CDec(e.Value) & "%"
                        End If
                    Else
                        e.Value = ""
                    End If
                Else
                    e.Value = ""
                End If
        End Select
    End Sub

    Private Async Function GetWarnIcon(ByVal oControlItem As ControlItem) As Task(Of Image)
        Dim exs As New List(Of Exception)
        Dim oImage As Image = My.Resources.empty
        If _Delivery.Cod = DTOPurchaseOrder.Codis.Client And oControlItem.LinCod = ControlItem.LinCods.Itm Then
            Dim iQty As Integer = oControlItem.Qty
            Dim oItem As DTODeliveryItem = oControlItem.Source
            If oItem.purchaseOrderItem.ErrCod = DTOPurchaseOrderItem.ErrCods.Success Then
                If Await FEB.ProductSku.IsAllowedOrderQty(exs, oItem.sku, iQty, Current.Session.User) Then
                    Dim oPdc = oItem.purchaseOrderItem.PurchaseOrder
                    If oPdc.FchDeliveryMin > DTO.GlobalVariables.Today() Then
                        oImage = My.Resources.Outlook_16
                    Else
                        If oPdc.totJunt Then oImage = My.Resources.clip
                    End If
                Else
                    oImage = My.Resources.warn
                End If
            Else
                oImage = My.Resources.aspa
            End If
        End If
        Return oImage
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        Dim oRow As DataGridViewRow = MyBase.CurrentRow
        If oRow IsNot Nothing Then

            Dim oControlItem As ControlItem = oRow.DataBoundItem
            Select Case oControlItem.LinCod
                Case ControlItem.LinCods.Pdc
                    Dim oPdc As DTOPurchaseOrder = oControlItem.Source
                    Dim oMenu_Pdc As New Menu_Pdc({oPdc}.ToList)
                    oMenuItem = New ToolStripMenuItem("comanda...")
                    oMenuItem.DropDownItems.AddRange(oMenu_Pdc.Range)
                    oContextMenu.Items.Add(oMenuItem)
                    AddPortsIfNeeded(oContextMenu)

                Case ControlItem.LinCods.SpvFirstLine, ControlItem.LinCods.SpvOtherLines
                    Dim oSpv As DTOSpv = oControlItem.Source
                    Dim oMenu_Spv As New Menu_Spv(oSpv)
                    oMenuItem = New ToolStripMenuItem("reparació...")
                    oMenuItem.DropDownItems.AddRange(oMenu_Spv.Range)
                    oContextMenu.Items.Add(oMenuItem)
                Case ControlItem.LinCods.Itm
                    Dim oItem As DTODeliveryItem = oControlItem.Source
                    Dim oMenu_Sku As New Menu_ProductSku(oItem.Sku)
                    oMenuItem = New ToolStripMenuItem("article...")
                    oMenuItem.DropDownItems.AddRange(oMenu_Sku.Range)
                    oContextMenu.Items.Add(oMenuItem)
            End Select

        End If

        If _MenuItemEditPreus Is Nothing Then
            _MenuItemEditPreus = New ToolStripMenuItem("editar preus i descomptes", Nothing, AddressOf Do_EditPreus)
        End If
        oContextMenu.Items.Add(_MenuItemEditPreus)

        oMenuItem = New ToolStripMenuItem("excel", My.Resources.Excel, AddressOf Do_Excel)
        oContextMenu.Items.Add(oMenuItem)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

#Region "AddPorts"
    Private Sub AddPortsIfNeeded(oContextMenu As ContextMenuStrip)
        If MissingPorts() Then
            Dim oMenuItem As New ToolStripMenuItem("afegir ports", Nothing, AddressOf AddPorts)
            oContextMenu.Items.Add(oMenuItem)
        End If
    End Sub

    Private Function MissingPorts() As Boolean
        Dim oSku = DTOProductSku.Wellknown(DTOProductSku.Wellknowns.Transport)
        Dim items = _ControlItems.Where(Function(x) x.LinCod = ControlItem.LinCods.Itm).Select(Function(y) CType(y.Source, DTODeliveryItem))
        Dim retval As Boolean = Not items.Any(Function(x) x.Sku.Equals(oSku))
        Return retval
    End Function

    Private Async Sub AddPorts(sender As Object, e As EventArgs)
        Dim exs As New List(Of Exception)
        Dim oCurrentItem As ControlItem = CurrentItem()
        If oCurrentItem IsNot Nothing AndAlso oCurrentItem.LinCod = ControlItem.LinCods.Pdc AndAlso Not _Delivery.IsNew Then
            RaiseEvent ToggleProgressBarRequest(True)
            Dim oPurchaseOrder = Await FEB.PurchaseOrder.Find(CType(oCurrentItem.Source, DTOBaseGuid).Guid, exs)
            If exs.Count = 0 Then
                Dim oPurchaseOrderItem = oPurchaseOrder.Items.FirstOrDefault(Function(x) x.Sku.Equals(DTOProductSku.Wellknown(DTOProductSku.Wellknowns.Transport)))
                If oPurchaseOrderItem Is Nothing Then
                    If FEB.PurchaseOrder.Load(oPurchaseOrder, exs) Then
                        Dim oPortsSku = Await FEB.ProductSku.LoadFromCustomer(exs, DTOProductSku.Wellknown(DTOProductSku.Wellknowns.Transport), _Delivery.Contact, Current.Session.Emp.Mgz)
                        oPurchaseOrderItem = oPurchaseOrder.addItem(oPortsSku, 1)
                        Await FEB.PurchaseOrder.Update(exs, oPurchaseOrder)
                        If exs.Count > 0 Then
                            RaiseEvent ToggleProgressBarRequest(False)
                            UIHelper.WarnError(exs)
                            Exit Sub
                        End If
                    Else
                        RaiseEvent ToggleProgressBarRequest(False)
                        UIHelper.WarnError(exs)
                        Exit Sub
                    End If
                End If

                Dim oDeliveryItem As New DTODeliveryItem
                With oDeliveryItem
                    .PurchaseOrderItem = oPurchaseOrderItem
                    .Sku = oPurchaseOrderItem.Sku
                    .Price = .PurchaseOrderItem.Price
                    .Qty = 1
                    .Lin = _Delivery.Items.Max(Function(x) x.Lin) + 1
                End With
                _Delivery.Items.Add(oDeliveryItem)
                '_Delivery.Transportista = Nothing

                If Await FEB.Delivery.Update(exs, _Delivery) Then
                    RaiseEvent ToggleProgressBarRequest(False)
                    RaiseEvent RequestToReload(Me, New MatEventArgs)
                Else
                    RaiseEvent ToggleProgressBarRequest(False)
                    UIHelper.WarnError(exs)
                End If
            Else
                RaiseEvent ToggleProgressBarRequest(False)
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

#End Region

    Private Sub Do_EditPreus(ByVal sender As Object, ByVal e As System.EventArgs)
        _MenuItemEditPreus.Checked = Not _MenuItemEditPreus.Checked
        MyBase.Columns(Cols.Price).ReadOnly = Not _MenuItemEditPreus.Checked
        MyBase.Columns(Cols.Dto).ReadOnly = Not _MenuItemEditPreus.Checked
        DirectCast(MyBase.Columns(Cols.Price), TabStopTextBoxColumn).TabStop = _MenuItemEditPreus.Checked
        DirectCast(MyBase.Columns(Cols.Dto), TabStopTextBoxColumn).TabStop = _MenuItemEditPreus.Checked
    End Sub

    Private Sub Do_Excel(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        RaiseEvent ToggleProgressBarRequest(True)

        Dim oBook = FEB.Delivery.Excel(exs, _Delivery)
        If exs.Count = 0 Then
            If UIHelper.ShowExcel(oBook, exs) Then
                RaiseEvent ToggleProgressBarRequest(False)
            Else
                RaiseEvent ToggleProgressBarRequest(False)
                UIHelper.WarnError(exs)
            End If
        Else
            RaiseEvent ToggleProgressBarRequest(False)
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Sub MatDataGridView1_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles MyBase.CellValidating
        If _AllowEvents Then
            Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            If oControlItem.LinCod = ControlItem.LinCods.Itm Then

                Dim item As DTODeliveryItem = oControlItem.Source
                Select Case e.ColumnIndex
                    Case Cols.Qty
                        Dim sEditedValue As String = oRow.Cells(e.ColumnIndex).EditedFormattedValue
                        If IsNumeric(sEditedValue) Then
                            Dim iNewQty = Integer.Parse(sEditedValue, System.Globalization.NumberStyles.Integer, New System.Globalization.CultureInfo("es-ES"))
                            If iNewQty <> item.qty Then
                                If item.purchaseOrderItem.ErrCod = DTOPurchaseOrderItem.ErrCods.Success Then
                                    _DirtyCell = True
                                Else
                                    e.Cancel = True
                                End If
                            End If
                        End If
                    Case Cols.Price
                        Dim sEditedValue As String = oRow.Cells(e.ColumnIndex).EditedFormattedValue
                        If IsNumeric(sEditedValue) Then
                            If sEditedValue <> DTOAmt.CurFormatted(item.Price) Then
                                Dim dcNewPrice = Decimal.Parse(sEditedValue, System.Globalization.NumberStyles.Currency, New System.Globalization.CultureInfo("es-ES")) 'System.Globalization.CultureInfo.InvariantCulture)
                                If dcNewPrice <> item.Price.Eur Then
                                    _DirtyCell = True
                                End If
                            End If
                        End If
                    Case Cols.Dto
                        Dim sEditedValue As String = oRow.Cells(e.ColumnIndex).EditedFormattedValue
                        If IsNumeric(sEditedValue) Then
                            Dim dcNewDto = Decimal.Parse(sEditedValue, System.Globalization.NumberStyles.Number, New System.Globalization.CultureInfo("es-ES"))
                            If dcNewDto <> item.Dto Then
                                _DirtyCell = True
                            End If
                        End If
                End Select
            End If
        End If

    End Sub

    Private Sub MatDataGridView1_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles MyBase.CellValidated
        If _AllowEvents And _DirtyCell Then
            Dim oRow = MyBase.Rows(e.RowIndex)
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            Dim item As DTODeliveryItem = oControlItem.Source
            Select Case e.ColumnIndex
                Case Cols.Qty
                    item.Qty = oControlItem.Qty
                    oControlItem.Amt = item.Import().Val
                    RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
                Case Cols.Price
                    item.Price = item.Price.Cur.AmtFromDivisa(oControlItem.Price)
                    oControlItem.Amt = item.Import().Val
                    RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
                Case Cols.Dto
                    item.Dto = oControlItem.Dto
                    oControlItem.Amt = item.Import().Val
                    RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            End Select
            _DirtyCell = False
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
        End If
    End Sub

    Private Sub Xl_DeliveryItems_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub Xl_DeliveryItems_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MyBase.KeyPress
        'ho crida DataGridView1_EditingControlShowing
        Select Case MyBase.CurrentCell.ColumnIndex
            Case Cols.Price, Cols.Dto
                If e.KeyChar = "." Then
                    e.KeyChar = ","
                End If
        End Select
    End Sub

    Private Sub Xl_TrpCosts_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles Me.EditingControlShowing
        'fa que funcioni KeyPress per DataGridViews
        If TypeOf e.Control Is TextBox Then
            Dim oControl As TextBox = DirectCast(e.Control, TextBox)
            AddHandler oControl.KeyPress, AddressOf Xl_DeliveryItems_KeyPress
        End If
    End Sub


    Protected Class ControlItem
        Property Source As Object
        Property Pnc As Integer
        Property SkuId As Integer
        Property Txt As String
        Property Qty As Integer
        Property Price As Decimal
        Property Dto As Decimal
        Property Amt As Decimal
        Property Stk As Integer
        Property Cli As Integer

        Property LinCod As LinCods

        Public Enum LinCods
            Itm
            Blank
            Pdc
            SpvFirstLine
            SpvOtherLines
            Obs
            Footer
        End Enum

        Public Sub New(value As DTODeliveryItem, oCod As DTOPurchaseOrder.Codis)
            MyBase.New()
            _Source = value
            _LinCod = LinCods.Itm
            With value
                If value.purchaseOrderItem IsNot Nothing Then
                    _Pnc = .purchaseOrderItem.pending
                End If
                _SkuId = .sku.id
                Select Case oCod
                    Case DTOPurchaseOrder.Codis.proveidor
                        _Txt = .sku.RefYNomPrv()
                    Case Else
                        _Txt = .Sku.RefYNomLlarg.Tradueix(Current.Session.Lang)
                End Select
                Qty = .Qty
                If .Price IsNot Nothing Then
                    Price = .Price.Eur
                    Amt = .Import.Eur
                End If
                Dto = .Dto
                Stk = .Sku.Stock
                Cli = .sku.clients
            End With
        End Sub

        Public Sub New()
            MyBase.New()
            _LinCod = LinCods.Blank
        End Sub

        Public Sub New(value As DTOPurchaseOrder, oLang As DTOLang)
            MyBase.New()
            _Source = value
            _LinCod = LinCods.Pdc
            _Txt = DTOPurchaseOrder.FullConcepte(value, oLang, True)
        End Sub

        Public Sub New(value As String)
            MyBase.New()
            _Source = value
            _LinCod = LinCods.Obs
            _Txt = value
        End Sub

        Public Sub New(oSpv As DTOSpv, SpvLines As List(Of String), idx As Integer)
            MyBase.New()
            _Source = oSpv
            _LinCod = IIf(idx = 0, LinCods.SpvFirstLine, LinCods.SpvOtherLines)
            _Txt = SpvLines(idx)
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class
