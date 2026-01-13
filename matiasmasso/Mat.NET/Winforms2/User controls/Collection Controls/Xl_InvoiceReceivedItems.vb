Public Class Xl_InvoiceReceivedItems
    Inherits _Xl_ReadOnlyDatagridview

    Private _Value As DTOInvoiceReceived
    Private _DefaultValue As DTOInvoiceReceived.Item
    Private _CurrentMenuItem As ToolStripMenuItem
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event OnItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        ico
        sku
        qty
        qtyConfirmed
        price
        Dto
        Amt
    End Enum

    Public Shadows Sub Load(value As DTOInvoiceReceived, Optional oDefaultValue As DTOInvoiceReceived.Item = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse)
        _Value = value
        _SelectionMode = oSelectionMode
        _DefaultValue = oDefaultValue

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOInvoiceReceived.Item) = FilteredValues()
        Dim oPurchaseOrder As New DTOGuidNom()
        Dim sPurchaseOrderId = "@@"
        Dim sOrderConfirmation = "@@"
        Dim sDeliveryNote = "@@"
        _ControlItems = New ControlItems
        Dim oControlItem As ControlItem = Nothing
        For Each oItem As DTOInvoiceReceived.Item In oFilteredValues
            If (oItem.PurchaseOrderId <> sPurchaseOrderId) Or (oItem.PurchaseOrder IsNot Nothing AndAlso oPurchaseOrder.UnEquals(oItem.PurchaseOrder)) Then
                oPurchaseOrder = oItem.PurchaseOrder
                sPurchaseOrderId = oItem.PurchaseOrderId
                oControlItem = ControlItem.Epigraf(oItem, ControlItem.LinCods.PurchaseOrder)
                _ControlItems.Add(oControlItem)
            End If
            If sOrderConfirmation <> (oItem.OrderConfirmation) Then
                sOrderConfirmation = oItem.OrderConfirmation
                oControlItem = ControlItem.Epigraf(oItem, ControlItem.LinCods.OrderConfirmation)
                _ControlItems.Add(oControlItem)
            End If
            If sDeliveryNote <> (oItem.DeliveryNote) Then
                sDeliveryNote = oItem.DeliveryNote
                oControlItem = ControlItem.Epigraf(oItem, ControlItem.LinCods.DeliveryNote)
                _ControlItems.Add(oControlItem)
            End If
            oControlItem = New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next


        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOInvoiceReceived.Item)
        Dim retval As List(Of DTOInvoiceReceived.Item)
        If _Filter = "" Then
            retval = _Value.Items
        Else
            retval = _Value.Items.FindAll(Function(x) x.Sku.Nom.ToLower.Contains(_Filter.ToLower))
        End If
        Return retval
    End Function


    Public Property Filter As String
        Get
            Return _Filter
        End Get
        Set(value As String)
            _Filter = value
            If _Value IsNot Nothing AndAlso _Value.Items IsNot Nothing Then Refresca()
        End Set
    End Property

    Public Sub ClearFilter()
        If _Filter > "" Then
            _Filter = ""
            Refresca()
        End If
    End Sub

    Public ReadOnly Property Value As DTOInvoiceReceived.Item
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOInvoiceReceived.Item = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowTemplate.DefaultCellStyle.BackColor = Color.Transparent

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True

        MyBase.Columns.Add(New DataGridViewImageColumn(False))
        With DirectCast(MyBase.Columns(Cols.ico), DataGridViewImageColumn)
            .CellTemplate = New DataGridViewImageCellBlank(False)
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
            .ReadOnly = True
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.sku)
            .HeaderText = "Concepte"
            .DataPropertyName = "Concept"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.qty)
            .HeaderText = "Quant."
            .DataPropertyName = "Qty"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.qtyConfirmed)
            .HeaderText = "Confirmat"
            .DataPropertyName = "QtyConfirmed"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.price)
            .HeaderText = "Preu"
            .DataPropertyName = "Price"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = _Value.Cur.formatString
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Dto)
            .HeaderText = "Dto"
            .DataPropertyName = "Dto"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#.##\%;-#.##\%;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Amt)
            .HeaderText = "Import"
            .DataPropertyName = "Amt"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = _Value.Cur.formatString
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

    Private Function SelectedItems() As List(Of DTOInvoiceReceived.Item)
        Dim retval As New List(Of DTOInvoiceReceived.Item)
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
            Dim item = SelectedItems.First
            If item IsNot Nothing Then
                If item.Exceptions.Count > 0 Then
                    Dim oMenuErrors As ToolStripMenuItem = oContextMenu.Items.Add("Errors")
                    For Each ex As DTOException In item.Exceptions
                        Dim oMenuEx As New ToolStripMenuItem(ex.Msg)
                        oMenuEx.Tag = ex
                        oMenuErrors.DropDownItems.Add(oMenuEx)
                        Select Case CType(ex.Cod, DTOInvoiceReceived.Item.ExCods)
                            Case DTOInvoiceReceived.Item.ExCods.MissingPurchaseOrder,
                                 DTOInvoiceReceived.Item.ExCods.MissingItemInPurchaseOrder,
                                DTOInvoiceReceived.Item.ExCods.PurchaseOrderNotFound
                                Dim oMenuAction = New ToolStripMenuItem("seleccionar comanda", Nothing, AddressOf FixErr)
                                oMenuAction.Tag = ex
                                oMenuEx.DropDownItems.Add(oMenuAction)
                            Case DTOInvoiceReceived.Item.ExCods.QtyGap
                                Dim oMenuAction = New ToolStripMenuItem("veure comanda", Nothing, AddressOf BrowsePdc)
                                oMenuAction.Tag = ex
                                oMenuEx.DropDownItems.Add(oMenuAction)
                            Case DTOInvoiceReceived.Item.ExCods.PriceGap
                                Dim oMenuAction = New ToolStripMenuItem("redactar email reclamació", Nothing, AddressOf ClaimMessage)
                                oMenuAction.Tag = ex
                                oMenuEx.DropDownItems.Add(oMenuAction)
                        End Select
                    Next
                End If
            End If
            'Dim oMenu_InvoiceReceived As New Menu_InvoiceReceived(SelectedItems.First)
            'AddHandler oMenu_InvoiceReceived.AfterUpdate, AddressOf RefreshRequest
            'oContextMenu.Items.AddRange(oMenu_InvoiceReceived.Range)
            'oContextMenu.Items.Add("-")
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub FixErr(sender As Object, e As EventArgs)
        _CurrentMenuItem = sender
        Dim oProveidor As New DTOProveidor(_Value.Proveidor.Guid)
        Dim oFrm As New Frm_PurchaseOrders(oProveidor, Defaults.SelectionModes.selection)
        AddHandler oFrm.onItemSelected, AddressOf onPurchaseOrderSelected
        oFrm.Show()
    End Sub

    Private Sub BrowsePdc(sender As Object, e As EventArgs)
        _CurrentMenuItem = sender
        Dim ex As DTOException = _CurrentMenuItem.Tag
        Dim item As DTOInvoiceReceived.Item = SelectedItems().First()
        If item.PurchaseOrder Is Nothing Then
            UIHelper.WarnError(String.Format("Comanda {0} desconeguda", item.PurchaseOrderId))
        Else
            Dim oFrm As New Frm_PurchaseOrder_Proveidor(New DTOPurchaseOrder(item.PurchaseOrder.Guid))
            oFrm.Show()
        End If
    End Sub

    Private Async Sub ClaimMessage(sender As Object, e As EventArgs)
        _CurrentMenuItem = sender
        Dim exs As New List(Of Exception)
        Dim ex As DTOException = _CurrentMenuItem.Tag
        Dim item As DTOInvoiceReceived.Item = SelectedItems().First()
        Dim sb As New Text.StringBuilder
        sb.AppendLine("<table>")
        sb.AppendLine("<tr>")
        sb.AppendFormat("<td>Invoice</td><td>{0} from {1:dd/MM/yyyy}</td>", _Value.DocNum, _Value.Fch)
        sb.AppendLine("</tr>")
        sb.AppendLine("<tr>")
        sb.AppendFormat("<td>Delivery note</td><td>{0}</td>", item.DeliveryNote)
        sb.AppendLine("</tr>")
        sb.AppendLine("<tr>")
        sb.AppendFormat("<td>Order confirmation</td><td>{0}</td>", item.OrderConfirmation)
        sb.AppendLine("</tr>")
        sb.AppendLine("<tr>")
        If item.PurchaseOrder Is Nothing Then
            sb.AppendFormat("<td>M+O order</td><td>{0}</td>", item.PurchaseOrderId)
        Else
            sb.AppendFormat("<td>M+O order</td><td>{0}</td>", item.PurchaseOrder.Nom)
        End If
        sb.AppendLine("</tr>")
        sb.AppendLine("</table>")

        Dim orderedAmount = DTOAmt.import(item.Qty, item.PurchaseOrderItem.Price, item.PurchaseOrderItem.Dto)
        Dim invoicedAmount = item.Amount
        Dim gap = invoicedAmount.Clone().Substract(orderedAmount)

        sb.AppendLine("<br/><br/>")
        sb.AppendLine("<table>")
        sb.AppendLine("<tr>")
        sb.AppendFormat("<td>&nbsp;</td><td align='right' >Units</td><td align='right' >Price</td><td align='right' >Discount</td><td align='right' >Amount</td>", _Value.DocNum, _Value.Fch)
        sb.AppendLine("</tr>")
        sb.AppendLine("<tr>")
        sb.AppendFormat("<td>Ordered</td><td align='right'>{0}</td><td align='right'>{1}</td><td align='right'>{2}%</td><td align='right'>{3}</td>", item.Qty, item.PurchaseOrderItem.Price.CurFormatted, item.PurchaseOrderItem.Dto, orderedAmount.CurFormatted)
        sb.AppendLine("</tr>")
        sb.AppendLine("<tr>")
        sb.AppendFormat("<td>Invoiced</td><td align='right'>{0}</td><td align='right'>{1}</td><td align='right'>{2}%</td><td align='right'>{3}</td>", item.Qty, DTOAmt.Factory(item.Price).CurFormatted, item.DtoOrDefault, invoicedAmount.CurFormatted)
        sb.AppendLine("</tr>")
        sb.AppendLine("<tr>")
        sb.AppendFormat("<td>Difference</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td align='right'>{0}</td>", gap.CurFormatted)
        sb.AppendLine("</tr>")
        sb.AppendLine("</table>")

        Dim subject = String.Format("{0} price issue", _Value.DocNum)
        Dim body = sb.ToString
        Dim oMailMessage = DTOMailMessage.Factory("", subject, body)
        If Not Await OutlookHelper.Send(oMailMessage, exs) Then
            UIHelper.WarnError(exs, "error al redactar el missatge")
        End If

    End Sub

    Private Async Sub onPurchaseOrderSelected(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)

        Dim oPurchaseOrder As DTOPurchaseOrder = e.Argument
        Dim oGuidNom = New DTOGuidNom(oPurchaseOrder.Guid, String.Format("Comanda {0} del {1:dd/MM/yy}", oPurchaseOrder.Concept, oPurchaseOrder.Fch))
        For Each Item As DTOInvoiceReceived.Item In SelectedItems()
            Item.PurchaseOrder = oGuidNom
            Item.PurchaseOrderId = oPurchaseOrder.formattedId
        Next

        If Await FEB.InvoiceReceived.Update(exs, _Value) Then
            MyBase.RefreshRequest(Me, New MatEventArgs)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOInvoiceReceived.Item = CurrentControlItem.Source
            'Dim oFrm As New Frm_InvoiceReceived.item(oSelectedValue)
            'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            'oFrm.Show()

        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles MyBase.RowPrePaint
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem

        Select Case oControlItem.LinCod
            Case ControlItem.LinCods.Item
                oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
            Case ControlItem.LinCods.PurchaseOrder
                oRow.DefaultCellStyle.BackColor = Color.LightBlue
            Case ControlItem.LinCods.OrderConfirmation
                oRow.DefaultCellStyle.BackColor = Color.LightGray
            Case ControlItem.LinCods.DeliveryNote
                oRow.DefaultCellStyle.BackColor = Color.LightGray
        End Select
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles MyBase.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.ico
                Dim oRow As DataGridViewRow = DirectCast(sender, DataGridView).Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                If oControlItem.LinCod = ControlItem.LinCods.item Then
                    Dim item As DTOInvoiceReceived.Item = oControlItem.Source
                    If item.Exceptions.Count > 0 Then
                        e.Value = My.Resources.warn_16
                    End If

                End If
            Case Cols.qtyConfirmed
                Dim oRow As DataGridViewRow = DirectCast(sender, DataGridView).Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                If oControlItem.QtyConfirmed <> oControlItem.Qty Then
                    e.CellStyle.BackColor = Color.LightSalmon
                End If

        End Select
    End Sub


    Protected Class ControlItem
        Property Source As DTOInvoiceReceived.Item

        Property LinCod As LinCods
        Property Concept As String
        Property Qty As Integer
        Property QtyConfirmed As Integer
        Property Price As Decimal
        Property Dto As Decimal
        Property Amt As Decimal


        Public Enum LinCods
            Item
            PurchaseOrder
            OrderConfirmation
            DeliveryNote
        End Enum

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(value As DTOInvoiceReceived.Item)
            MyBase.New()
            _Source = value
            With value
                _LinCod = LinCods.Item
                If .Sku Is Nothing Then
                    _Concept = .SkuNom
                Else
                    _Concept = .Sku.Nom
                End If
                _Qty = .Qty
                _QtyConfirmed = .QtyConfirmed
                _Price = .Price
                _Dto = .DtoOrDefault
                If .Amount IsNot Nothing Then
                    _Amt = .Amount.Eur
                End If
            End With
        End Sub

        Public Shared Function Epigraf(value As DTOInvoiceReceived.Item, linCod As LinCods)
            Dim retval As New ControlItem
            retval.LinCod = linCod
            Select Case linCod
                Case LinCods.PurchaseOrder
                    retval.Concept = String.Format("Comanda {0}", value.PurchaseOrderId)
                Case LinCods.OrderConfirmation
                    retval.Concept = String.Format("Confirmació {0}", value.OrderConfirmation)
                Case LinCods.DeliveryNote
                    retval.Concept = String.Format("Albarà {0}", value.DeliveryNote)
            End Select
            Return retval
        End Function

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


