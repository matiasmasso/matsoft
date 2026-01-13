Public Class DTOPurchaseOrder
    Inherits DTOBaseGuid

    Property emp As DTOEmp

    Property fch As DateTime
    Property num As Integer
    Property fchDelivery As Date
    Property customer As DTOCustomer
    Property proveidor As DTOProveidor
    Property platform As DTOCustomerPlatform
    Property concept As String
    Property numComandaProveidor As String
    Property deliverTo As DTOContact
    Property obs As String
    Property customerDocUrl As String
    Property cod As Codis
    Property paymentTerms As DTOPaymentTerms
    Property source As Sources
    Property pot As Boolean
    Property blockStock As Boolean
    Property totJunt As Boolean
    Property cur As DTOCur
    Property incentiu As DTOIncentiu
    Property sumaDeImports As DTOAmt

    Property cashStatus As CashStatuss
    Property docFile As DTODocFile
    Property items As List(Of DTOPurchaseOrderItem)


    Property isOpenOrder As Boolean

    Property usrLog As DTOUsrLog

    Property proformaObs As List(Of String)

    Property etiquetesTransport As DTODocFile
    Property Incoterm As DTOProveidor.Incoterms

    Public Enum Codis
        notSet
        proveidor
        client
        traspas
        reparacio
    End Enum

    Public Enum Sources
        no_Especificado
        telefonico
        fax
        eMail
        representante
        representante_por_Web
        cliente_por_Web
        matPocket
        fira
        cliente_XML
        edi
        garantia
        iPhone
        cliente_por_WebApi
        ExcelMayborn
    End Enum

    Public Enum ShippingStatusCods
        unShipped
        halfShipped
        fullyShipped
        emptyOrder
    End Enum

    Public Enum CashStatuss
        pendent_de_avisar
        avisat
        cobrat
    End Enum

    Public Sub New()
        MyBase.New()
        _items = New List(Of DTOPurchaseOrderItem)
        _usrLog = New DTOUsrLog
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _items = New List(Of DTOPurchaseOrderItem)
        _usrLog = New DTOUsrLog
    End Sub

    Shared Function Factory(oCustomer As DTOCustomer, oUser As DTOUser, Optional DtFch As Date = Nothing, Optional oSource As DTOPurchaseOrder.Sources = DTOPurchaseOrder.Sources.no_Especificado, Optional sConcept As String = "") As DTOPurchaseOrder
        If DtFch = Nothing Then DtFch = Today
        Dim retval As New DTOPurchaseOrder
        With retval
            .emp = oUser.emp
            .cod = DTOPurchaseOrder.Codis.client
            .fch = DtFch
            .source = oSource
            If oCustomer.OrdersToCentral AndAlso oCustomer.Ccx IsNot Nothing Then
                .customer = oCustomer.Ccx
            Else
                .customer = oCustomer
            End If
            .usrLog = DTOUsrLog.Factory(oUser)
            .concept = sConcept
            .cur = DTOCur.Eur
            .items = New List(Of DTOPurchaseOrderItem)
        End With
        Return retval
    End Function

    Shared Function Factory(oEmp As DTOEmp, oProveidor As DTOProveidor, oUser As DTOUser, Optional DtFch As Date = Nothing, Optional oSource As DTOPurchaseOrder.Sources = DTOPurchaseOrder.Sources.no_Especificado, Optional sConcept As String = "") As DTOPurchaseOrder
        If DtFch = Nothing Then DtFch = Today
        Dim retval As New DTOPurchaseOrder
        With retval
            .emp = oEmp
            .cod = DTOPurchaseOrder.Codis.proveidor
            .fch = DtFch
            .fchDelivery = .fch.AddDays(21)
            .source = oSource
            .proveidor = oProveidor
            .concept = sConcept
            If oProveidor IsNot Nothing Then
                .cur = oProveidor.Cur
            End If
            .deliverTo = oEmp.Mgz
            .items = New List(Of DTOPurchaseOrderItem)
            .usrLog = DTOUsrLog.Factory(oUser)
        End With
        Return retval
    End Function

    Public Function Contact() As DTOContact
        Dim retval As DTOContact = Nothing
        Select Case _cod
            Case Codis.notSet
                retval = If(_proveidor, _customer)
            Case Codis.proveidor
                retval = _proveidor
            Case Else
                retval = _customer
        End Select
        Return retval
    End Function

    Shared Function Clon(oSrc As DTOPurchaseOrder) As DTOPurchaseOrder
        Dim retval As New DTOPurchaseOrder()
        With retval
            .emp = oSrc.emp
            .cod = oSrc.cod
            .fch = oSrc.fch
            .customer = oSrc.customer
            .proveidor = oSrc.proveidor
            .concept = oSrc.concept
            .pot = oSrc.pot
            .totJunt = oSrc.totJunt
            .fchDelivery = oSrc.fchDelivery
            .deliverTo = oSrc.deliverTo
            .obs = oSrc.obs
            .customerDocUrl = oSrc.customerDocUrl
            .source = oSrc.source
            .cur = oSrc.cur
            .platform = oSrc.platform
            For Each oItem As DTOPurchaseOrderItem In oSrc.items
                Dim ClonedItem = DTOPurchaseOrderItem.Clon(oItem, retval)
                .items.Add(ClonedItem)
            Next
        End With
        Return retval
    End Function


    Shared Function FormattedId(oPurchaseOrder As DTOPurchaseOrder) As String
        Dim retval As String = String.Format("{0:0000}{1:000000}", oPurchaseOrder.fch.Year, oPurchaseOrder.num)
        Return retval
    End Function

    Shared Function Filename(oPurchaseOrder As DTOPurchaseOrder, oMime As MimeCods) As String
        Dim retval As String = "M+O pedido " & oPurchaseOrder.FormattedId & "." & oMime.ToString
        Return retval
    End Function

    Public Function Url(Optional AbsoluteUrl As Boolean = False) As String
        Return MmoUrl.Factory(AbsoluteUrl, "pedido", MyBase.Guid.ToString())
    End Function

    Shared Function Title(oPurchaseOrder As DTOPurchaseOrder, Optional oLang As DTOLang = Nothing) As String
        If oLang Is Nothing Then oLang = DTOApp.current.lang
        Dim sConcept As String = oLang.tradueix("Pedido", "Comanda", "Order")
        Dim sFrom As String = oLang.tradueix("del", "del", "from")
        Dim retval As String = String.Format("{0} {1} {2} {3:dd/MM/yy}", sConcept, oPurchaseOrder.concept, sFrom, oPurchaseOrder.fch)
        Return retval
    End Function

    Shared Function Title(items As List(Of DTOPurchaseOrder), Optional oLang As DTOLang = Nothing, Optional ByVal BlProforma As Boolean = False) As String
        Dim retval As String
        If items.Count = 0 Then
            retval = DTOApp.current.lang.tradueix("(ningún pedido)", "(cap comanda)", "(none orders)")
        Else
            Dim sDoc As String = oLang.tradueix("pedido", "comanda", "order")
            Dim sDocs As String = oLang.tradueix("pedidos", "comandes", "orders")
            If BlProforma Then
                sDoc = "proforma"
                sDocs = "proformas"
            End If

            Dim sFirstDoc As String = items.First.fch.Year.ToString & "." & items.First.num
            Dim sLastDoc As String = items.Last.fch.Year.ToString & "." & items.Last.num

            Select Case items.Count
                Case 1
                    retval = sDoc & " " & sFirstDoc
                Case Else
                    If Consecutivos(items) Then
                        retval = sDocs & " " & sFirstDoc & "-" & sLastDoc
                    Else
                        Dim i As Integer
                        retval = sDocs & " "
                        For i = 0 To items.Count - 1
                            If i > 2 Then
                                retval = retval & ",..."
                                Exit For
                            End If
                            If i > 0 Then retval = retval & ","
                            retval = retval & items(i).fch.Year.ToString & "." & items(i).num.ToString
                        Next
                    End If
            End Select
        End If

        Return retval
    End Function

    Shared Function FullConcepte(oPurchaseOrder As DTOPurchaseOrder, oLang As DTOLang, Optional ByVal IncludeFch As Boolean = True) As String
        Dim retval As String = ""
        Select Case oPurchaseOrder.cod
            Case DTOPurchaseOrder.Codis.client, DTOPurchaseOrder.Codis.proveidor
                Select Case oPurchaseOrder.cod
                    Case DTOPurchaseOrder.Codis.proveidor
                        retval = oLang.tradueix("Nuestro pedido", "Comanda", "Our order")
                        retval = retval & " " & oPurchaseOrder.num
                    Case Else
                        retval = oLang.tradueix("Su pedido", "Comanda", "Your order")
                End Select
                If oPurchaseOrder.concept > "" Then retval = retval & " " & oPurchaseOrder.concept
                If IncludeFch Then
                    retval = retval & " " & oLang.tradueix("del", "del", "from") & " " & Format(oPurchaseOrder.fch, "dd/MM/yy")
                End If
        End Select
        Return retval
    End Function


    Shared Function Consecutivos(ByVal oList As List(Of DTOPurchaseOrder)) As Boolean
        Dim retVal As Boolean = True
        Dim i As Integer
        If oList.Count > 2 Then
            For i = 0 To oList.Count - 2
                If oList.Item(i + 1).num <> oList.Item(i).num + 1 Then
                    retVal = False
                End If
            Next
        End If
        Return retVal
    End Function

    Shared Function Nums(values As List(Of DTOPurchaseOrder)) As String
        Dim sb As New System.Text.StringBuilder
        If values.Count = 1 Then
            sb.Append(values.First.num)
        ElseIf DTOPurchaseOrder.Consecutivos(values) Then
            sb.Append(values.First.num & "-" & values(values.Count - 1).num)
        Else
            For i As Integer = 0 To values.Count - 1
                If i > 0 Then sb.Append(",")
                sb.Append(values(i).num)
            Next
        End If
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function MailMessageConfirmation(oPurchaseOrder As DTOPurchaseOrder, Optional sRecipients As List(Of String) = Nothing) As DTOMailMessage
        Dim oUser As DTOUser = oPurchaseOrder.usrLog.UsrCreated
        Dim sSubjectTemplate As String = oUser.lang.tradueix("Pedido #{0} de {1}", "Comanda #{0} de {1}", "Order #{0} from {1}", "Encomenda #{0} de {1}")

        If sRecipients Is Nothing Then
            sRecipients = New List(Of String)
            sRecipients.Add(oUser.emailAddress)
        End If

        Dim retval = DTOMailMessage.Factory(sRecipients)
        With retval
            .bcc = {DTOMailMessage.wellknownAddress(DTOMailMessage.wellknownRecipients.Info)}
            .subject = String.Format(sSubjectTemplate, oPurchaseOrder.num, oPurchaseOrder.Contact.FullNom)
            .bodyUrl = MmoUrl.BodyTemplateUrl(DTODefault.MailingTemplates.CustomerPurchaseOrder, oPurchaseOrder.Guid.ToString())
        End With
        Return retval
    End Function

    Shared Function MailMessageRepConfirmation(oPurchaseOrder As DTOPurchaseOrder, Optional ccToUserCreated As Boolean = False) As DTOMailMessage
        Dim oUser As DTOUser = oPurchaseOrder.usrLog.usrCreated
        Dim oLang As DTOLang = oPurchaseOrder.Contact.lang
        Dim sTo As String = DTOMailMessage.wellknownAddress(DTOMailMessage.wellknownRecipients.Info)
        If ccToUserCreated Then sTo = oPurchaseOrder.usrLog.usrCreated.emailAddress

        Dim sRecipients As New List(Of String)
        Dim sSubjectTemplate As String = oLang.tradueix("Pedido #{0} de {1} para {2}", "Comanda #{0} de {1} per {2}", "Order #{0} from {1} for {2}")

        Dim retval = DTOMailMessage.Factory(sTo)
        With retval
            If ccToUserCreated Then .bcc = {DTOMailMessage.wellknownAddress(DTOMailMessage.wellknownRecipients.Info)}
            .subject = String.Format(sSubjectTemplate, oPurchaseOrder.num, DTOUser.NicknameOrElse(oUser), oPurchaseOrder.Contact.nom)
            .bodyUrl = MmoUrl.BodyTemplateUrl(DTODefault.MailingTemplates.RepPurchaseOrder, oPurchaseOrder.Guid.ToString())
        End With
        Return retval
    End Function

    Shared Function PdfFilename(oPurchaseOrders As List(Of DTOPurchaseOrder), Optional oLang As DTOLang = Nothing) As String
        If oLang Is Nothing Then oLang = DTOLang.ESP
        Dim sCaption As String = ""
        Select Case oPurchaseOrders.Count
            Case 0
            Case 1
                sCaption = oLang.tradueix("pedido", "comanda", "order")
            Case Else
                sCaption = oLang.tradueix("pedidos", "comandes", "orders")
        End Select
        Dim sNums As String = DTOPurchaseOrder.Nums(oPurchaseOrders).Replace(",", " ")
        Dim retval As String = String.Format("M+O {0} {1}.pdf", sCaption, sNums)
        Return retval
    End Function


    Public Function AddItem(oSku As DTOProductSku, qty As Integer, Optional price As DTOAmt = Nothing, Optional dto As Decimal = 0) As DTOPurchaseOrderItem
        If _items Is Nothing Then _items = New List(Of DTOPurchaseOrderItem)
        Dim item As New DTOPurchaseOrderItem()
        With item
            .purchaseOrder = Me
            .sku = oSku
            .qty = qty
            .pending = qty
            .price = price
            .dto = dto
        End With
        _items.Add(item)
        Return item
    End Function

    Public Function SumaDeImportes() As DTOAmt
        Dim retval = DTOAmt.factory
        If _items IsNot Nothing Then
            For Each item As DTOPurchaseOrderItem In _items
                Dim oAmt As DTOAmt = item.Amount()
                retval.Add(oAmt)
            Next
        End If
        Return retval
    End Function

    Public Function IvaBaseQuotas() As List(Of DTOTaxBaseQuota)
        Dim retval As New List(Of DTOTaxBaseQuota)
        If _cod <> Codis.proveidor Then
            Dim oCcx As DTOCustomer = Me.customer.CcxOrMe
            If oCcx.IVA Then
                Dim oBaseImponible As DTOAmt = Me.SumaDeImportes()
                Dim oIva As DTOTax = DTOTax.Closest(DTOTax.Codis.Iva_Standard, _fch)
                Dim oQuotaIva As New DTOTaxBaseQuota(oIva, oBaseImponible)
                retval.Add(oQuotaIva)

                If oCcx.Req Then
                    Dim oReq As DTOTax = DTOTax.Closest(DTOTax.Codis.Recarrec_Equivalencia_Standard, _fch)
                    Dim oQuotaReq As New DTOTaxBaseQuota(oReq, oBaseImponible)
                    retval.Add(oQuotaReq)
                End If
            End If
        End If
        Return retval
    End Function

    Shared Function DevengaIva(value As DTOPurchaseOrder) As Boolean
        Dim retval As Boolean
        Select Case value.cod
            Case DTOPurchaseOrder.Codis.client
                retval = value.customer.CcxOrMe.IVA
            Case DTOPurchaseOrder.Codis.proveidor

        End Select
        Return retval
    End Function

    Shared Function DevengaReq(value As DTOPurchaseOrder) As Boolean
        Dim retval As Boolean
        Select Case value.cod
            Case DTOPurchaseOrder.Codis.client
                retval = value.customer.CcxOrMe.Req
            Case DTOPurchaseOrder.Codis.proveidor

        End Select
        Return retval
    End Function

    Shared Function IvaPct(oPurchaseOrder As DTOPurchaseOrder) As Decimal
        Dim retval As Decimal = DTOTax.Closest(DTOTax.Codis.Iva_Standard, oPurchaseOrder.fch).Tipus
        Return retval
    End Function

    Shared Function IvaAmt(oPurchaseOrder As DTOPurchaseOrder) As DTOAmt
        Dim dcIvaPct As Decimal = IvaPct(oPurchaseOrder)
        Dim retval As DTOAmt = oPurchaseOrder.SumaDeImportes().Percent(dcIvaPct)
        Return retval
    End Function

    Shared Function ReqPct(oPurchaseOrder As DTOPurchaseOrder) As Decimal
        Dim retval As Decimal = DTOTax.Closest(DTOTax.Codis.Recarrec_Equivalencia_Standard, oPurchaseOrder.fch).Tipus
        Return retval
    End Function

    Shared Function ReqAmt(oPurchaseOrder As DTOPurchaseOrder) As DTOAmt
        Dim dcIvaReq As Decimal = ReqPct(oPurchaseOrder)
        Dim retval As DTOAmt = oPurchaseOrder.SumaDeImportes().Percent(dcIvaReq)
        Return retval
    End Function

    Shared Function TotalIvaInclos(oPurchaseOrder As DTOPurchaseOrder) As DTOAmt
        Dim retval As DTOAmt = Nothing
        Dim oBaseImponible As DTOAmt = oPurchaseOrder.SumaDeImportes()
        If DevengaIva(oPurchaseOrder) Then
            Dim oIva As DTOAmt = IvaAmt(oPurchaseOrder)
            If DevengaReq(oPurchaseOrder) Then
                Dim oReq As DTOAmt = ReqAmt(oPurchaseOrder)
                retval = DTOAmt.factory(oBaseImponible, oIva, oReq)
            Else
                retval = DTOAmt.factory(oBaseImponible, oIva)
            End If
        Else
            retval = oBaseImponible
        End If
        Return retval
    End Function


    Public Function FormattedId() As String
        Dim retval As String = String.Format("{0:0000}{1:000000}", _fch.Year, _num)
        Return retval
    End Function

    Public Function Caption(Optional oLang As DTOLang = Nothing) As String
        If oLang Is Nothing AndAlso Me.Contact IsNot Nothing Then oLang = Me.Contact.lang
        If oLang Is Nothing Then oLang = DTOLang.ESP
        Dim sb As New System.Text.StringBuilder
        Select Case _cod
            Case DTOPurchaseOrder.Codis.client
                sb.Append(oLang.tradueix("Su pedido ", "La seva comanda ", "Your order "))
            Case DTOPurchaseOrder.Codis.proveidor
                sb.Append(oLang.tradueix("Nuestro pedido ", "La nostre comanda ", "Our order "))
                sb.Append(_num.ToString())
                sb.Append(" ")
        End Select
        If _concept > "" Then
            sb.Append(_concept & " ")
        End If
        sb.Append(oLang.tradueix("del ", "del ", "from ") & Format(_fch, "dd/MM/yy"))
        Dim retval As String = sb.ToString
        Return retval
    End Function
    Public Function FullNomAndCaption(Optional oLang As DTOLang = Nothing) As String
        If oLang Is Nothing And Me.Contact IsNot Nothing Then oLang = Me.Contact.lang
        If oLang Is Nothing Then oLang = DTOLang.ESP
        Dim sb As New System.Text.StringBuilder
        sb.Append(oLang.tradueix("Pedido ", "Comanda ", "Order "))
        sb.Append(_num.ToString())
        sb.Append(" ")
        sb.Append(Me.Contact.FullNom)
        If _concept > "" Then
            sb.Append(_concept & " ")
        End If
        sb.Append(oLang.tradueix("del ", "del ", "from ") & Format(_fch, "dd/MM/yy"))
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Public Function DeliverableAmt() As DTOAmt
        Dim retval = DTOAmt.factory(items.Sum(Function(x) x.DeliverableAmt.eur))
        Return retval
    End Function

    Shared Function DiscountsExist(value As DTOPurchaseOrder) As Boolean
        Dim retval As Boolean
        If value.items IsNot Nothing Then
            retval = value.items.Exists(Function(x) x.dto <> 0)
        End If
        Return retval
    End Function

    Shared Function PendentsExist(value As DTOPurchaseOrder) As Boolean
        Dim retval As Boolean
        If value.items IsNot Nothing Then
            retval = value.items.Any(Function(x) x.pending <> x.qty)
        End If
        Return retval
    End Function

    Shared Function VolumeM3(oOrder As DTOPurchaseOrder) As Decimal
        Dim retval As Decimal = oOrder.items.Sum(Function(x) x.qty * DTOProductSku.VolumeM3OrInherited(x.sku))
        Return retval
    End Function

    Shared Function Eur(oOrder As DTOPurchaseOrder) As Decimal
        Dim retval As Decimal = oOrder.items.Sum(Function(x) x.qty * x.price.eur * (100 - x.dto) / 100)
        Return retval
    End Function

    Shared Function ShippingStatus(oOrder As DTOPurchaseOrder) As DTOPurchaseOrder.ShippingStatusCods
        Dim retval As DTOPurchaseOrder.ShippingStatusCods
        If oOrder.items.Count = 0 Then
            retval = DTOPurchaseOrder.ShippingStatusCods.emptyOrder
        ElseIf oOrder.items.All(Function(x) x.pending = 0) Then
            retval = DTOPurchaseOrder.ShippingStatusCods.fullyShipped
        ElseIf oOrder.items.All(Function(x) x.qty = x.pending) Then
            retval = DTOPurchaseOrder.ShippingStatusCods.unShipped
        Else
            retval = DTOPurchaseOrder.ShippingStatusCods.halfShipped
        End If
        Return retval
    End Function

    Shared Function ConfirmationMailMessage(oEmp As DTOEmp, oPurchaseOrder As DTOPurchaseOrder, sRecipients As List(Of String), oLang As DTOLang) As DTOMailMessage
        Dim sSubjectTemplate As String = oLang.tradueix("Pedido #{0} de {1}", "Comanda #{0} de {1}", "Order #{0} from {1}", "Encomenda #{0} de {1}")
        Dim sSubject As String = String.Format(sSubjectTemplate, oPurchaseOrder.num, oPurchaseOrder.Contact.FullNom)

        Dim retval = DTOMailMessage.Factory(sRecipients)
        With retval
            .subject = String.Format(sSubjectTemplate, oPurchaseOrder.num, oPurchaseOrder.Contact.FullNom)
            .bodyUrl = oEmp.AbsoluteUrl("mail", DTODefault.MailingTemplates.CustomerPurchaseOrder.ToString, oPurchaseOrder.Guid.ToString())
        End With
        Return retval
    End Function


End Class

Public Class DTOPurchaseOrderItem
    Inherits DTOBaseGuid
    Property purchaseOrder As DTOPurchaseOrder
    Property qty As Integer
    Property pending As Integer
    Property price As DTOAmt
    Property dto As Decimal
    Property chargeCod As ChargeCods
    Property sku As DTOProductSku
    Property repCom As DTORepCom
    Property lin As Integer
    Property deliveries As List(Of DTODeliveryItem)

    Property deliveredQty As Integer
    Property incentius As List(Of DTOIncentiu)
    Property packChildren As List(Of DTOPurchaseOrderItem)
    Property packParent As DTOPurchaseOrderItem
    Property ETD As Date 'Estimated Delivery Time
    Property bundle As List(Of DTOPurchaseOrderItem)

    Public Enum ChargeCods
        chargeable
        FOC 'free of charge
    End Enum

    Public Sub New()
        MyBase.New()
        _PackChildren = New List(Of DTOPurchaseOrderItem)
        _Bundle = New List(Of DTOPurchaseOrderItem)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _PackChildren = New List(Of DTOPurchaseOrderItem)
        _Bundle = New List(Of DTOPurchaseOrderItem)
    End Sub

    Shared Function Factory(oOrder As DTOPurchaseOrder, oSku As DTOProductSku, iQty As Integer, oPrice As DTOAmt, DcDto As Decimal, Optional oRepCom As DTORepCom = Nothing) As DTOPurchaseOrderItem
        Dim retval As New DTOPurchaseOrderItem()
        With retval
            .purchaseOrder = oOrder
            .sku = oSku
            .qty = iQty
            .price = oPrice
            .dto = DcDto
            .pending = .qty
            .chargeCod = DTOPurchaseOrderItem.ChargeCods.chargeable
            .repCom = oRepCom
        End With
        Return retval
    End Function

    Shared Function Factory(value As DTOBasketLine) As DTOPurchaseOrderItem
        Dim retval As New DTOPurchaseOrderItem
        With retval
            .Qty = value.qty
            .Pending = .Qty
            .Sku = New DTOProductSku(value.sku)
            .Sku.NomLlarg = value.nom
            .Sku.Stock = value.availableStock
            .Sku.Category = New DTOProductCategory(value.category) 'important per assignar el representant
            .Sku.Category.Brand = New DTOProductBrand(value.brand)
            .Price = DTOAmt.factory(value.price)
            .Dto = value.dto
        End With
        Return retval
    End Function


    Shared Function Clon(oSrc As DTOPurchaseOrderItem, oClonedPurchaseOrder As DTOPurchaseOrder) As DTOPurchaseOrderItem
        Dim retval As New DTOPurchaseOrderItem()
        With retval
            .PurchaseOrder = oClonedPurchaseOrder
            .ChargeCod = oSrc.ChargeCod
            .Dto = oSrc.Dto
            .Qty = oSrc.Qty
            .Pending = oSrc.Qty
            .Sku = oSrc.Sku
            .RepCom = oSrc.RepCom
            .Price = oSrc.Price
        End With
        Return retval
    End Function

    Public Sub LoadBundleItems()
        _Bundle = New List(Of DTOPurchaseOrderItem)
        For Each oSkuBundle As DTOSkuBundle In _Sku.bundleSkus
            Dim oPrice = DTOAmt.factory
            If oSkuBundle.Sku.price IsNot Nothing Then
                oPrice = oSkuBundle.Sku.price.DeductPercent(_Sku.bundleDto)
            End If
            Dim item = DTOPurchaseOrderItem.Factory(_PurchaseOrder, oSkuBundle.Sku, oSkuBundle.Qty * _Qty, oPrice, _Dto)
            _Bundle.Add(item)
        Next
    End Sub

    Public Function isBundleParent() As Boolean
        Dim retval As Boolean
        If _Bundle IsNot Nothing Then
            retval = _Bundle.Count > 0
        End If
        Return retval
    End Function

    Shared Function BundleItemFactory(oSkuBundle As DTOSkuBundle, item As DTOPurchaseOrderItem, oEmp As DTOEmp, oCustomCosts As List(Of DTOPricelistItemCustomer), oTarifaDtos As List(Of DTOCustomerTarifaDto), oCliProductDtos As List(Of DTOCliProductDto), oRepCom As DTORepCom) As DTOPurchaseOrderItem
        Dim oOrder = item.purchaseOrder
        Dim oSku = item.sku

        Dim DcDto As Decimal = 0
        Dim oPrice As DTOAmt = Nothing
        If oOrder.cod <> DTOPurchaseOrder.Codis.proveidor Then
            oPrice = DTOProductSku.GetCustomerCost(oSkuBundle.Sku, oCustomCosts, oTarifaDtos)
        End If

        If oPrice Is Nothing Then
            oPrice = DTOAmt.factory
        Else
            If oOrder.cod = DTOPurchaseOrder.Codis.client Then
                Dim oDto As DTOCliProductDto = oSku.CliProductDto(oCliProductDtos)
                If oDto IsNot Nothing Then DcDto = oDto.Dto
                If oOrder.customer.globalDto > DcDto Then DcDto = oOrder.customer.globalDto
            End If
        End If

        Dim retval = DTOPurchaseOrderItem.Factory(oOrder, oSkuBundle.Sku, item.qty, oPrice, DcDto, oRepCom)
        Return retval
    End Function


    Public ReadOnly Property PreuNet() As DTOAmt
        Get
            Dim retval As DTOAmt = Nothing
            If _Price IsNot Nothing Then
                retval = _Price.Clone.Substract(_Price.Percent(_Dto))
            End If
            Return retval
        End Get
    End Property

    Public ReadOnly Property Amount() As DTOAmt
        Get
            Dim retval As DTOAmt = DTOAmt.FromQtyPriceDto(_Qty, _Price, _Dto)
            Return retval
        End Get
    End Property

    Public ReadOnly Property PendingAmount() As DTOAmt
        Get
            Dim retval As DTOAmt = DTOAmt.FromQtyPriceDto(_Pending, _Price, _Dto)
            Return retval
        End Get
    End Property

    Public Function DeliverableQty() As Integer
        Dim stk = _Sku.StockAvailable
        Dim retval = Math.Min(_Pending, stk)
        Return retval
    End Function

    Public Function DeliverableAmt() As DTOAmt
        Dim retval = DTOAmt.factory(PreuNet.Eur * DeliverableQty())
        Return retval
    End Function

    Shared Function MultilineFullText(value As DTOPurchaseOrderItem) As String
        Dim sb As New Text.StringBuilder
        sb.AppendLine(value.PurchaseOrder.Contact.FullNom)
        sb.AppendLine(DTOPurchaseOrder.FullConcepte(value.purchaseOrder, value.purchaseOrder.Contact.lang))
        sb.AppendLine(String.Format("{0} {1} x {2}", value.Sku.NomLlarg, value.Qty, value.Price.Formatted))
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function FormattedDto(item As DTOPurchaseOrderItem) As String
        Dim retval As String
        If item.Dto = 0 Then
            retval = "&nbsp;"
        Else
            retval = item.Dto & "%"
        End If
        Return retval
    End Function

    Shared Function Catalog(Items As List(Of DTOPurchaseOrderItem)) As List(Of DTOProductBrand)
        Dim oSkus = Items.GroupBy(Function(x) x.Sku.Guid).Select(Function(y) y.First).Select(Function(z) z.Sku).ToList
        Dim oCategories = oSkus.GroupBy(Function(x) x.Category.Guid).Select(Function(y) y.First).Select(Function(z) z.Category).ToList
        Dim retval = oCategories.GroupBy(Function(x) x.Brand.Guid).Select(Function(y) y.First).Select(Function(z) z.Brand).ToList
        For Each oBrand In retval
            For Each oCategory In oCategories.Where(Function(x) x.Brand.Equals(oBrand))
                oBrand.Categories.Add(oCategory)
                For Each oSku In oSkus.Where(Function(x) x.Category.Equals(oCategory))
                    oCategory.Skus.Add(oSku)
                Next
            Next
        Next
        Return retval
    End Function
End Class
