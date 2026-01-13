Public Class PromofarmaOrder

    Shared Async Function OrderLines(exs As List(Of Exception), oOrder As DTO.Integracions.Promofarma.Order) As Task(Of List(Of DTO.Integracions.Promofarma.OrderLine))
        Dim segments = String.Format("orders/{0}/lines", oOrder.data.order_id)
        Dim url = DTO.Integracions.Promofarma.Api.Url(segments)
        Dim headers = DTO.Integracions.Promofarma.Api.AuthHeader
        Dim value = Await Api.GetRequest(Of DTO.Integracions.Promofarma.OrderLines)(exs, headers, url)
        Return value.data
    End Function

    Shared Async Function ConsumerTicket(exs As List(Of Exception), oOrder As DTO.Integracions.Promofarma.Order, oUser As DTOUser) As Task(Of DTOConsumerTicket)
        Dim oMarketPlace = DTOMarketPlace.Wellknown(DTOMarketPlace.Wellknowns.Worten)
        Dim orderId = oOrder.data.order_id
        Dim retval = Await Api.Fetch(Of DTOConsumerTicket)(exs, "ConsumerTicket", oMarketPlace.Guid.ToString, orderId)
        If retval Is Nothing Then retval = Await ConsumerTicketFactory(exs, oOrder, oUser)
        Return retval
    End Function


    Shared Async Function ConsumerTicketFactory(exs As List(Of Exception), oOrder As DTO.Integracions.Promofarma.Order, oUser As DTOUser) As Task(Of DTOConsumerTicket)
        Dim retval As DTOConsumerTicket = Nothing
        Dim oCache = Await Cache.Fetch(exs, oUser)
        If exs.Count = 0 Then
            Dim oMarketPlace = DTOMarketPlace.Wellknown(DTOMarketPlace.Wellknowns.Worten)
            retval = DTOConsumerTicket.Factory(oUser, oMarketPlace, oOrder.data.order_id)
            With retval
                With .PurchaseOrder
                    .Emp = oUser.Emp
                    .Fch = oOrder.data.order_date
                    .Concept = oOrder.data.order_id
                    .Items = New List(Of DTOPurchaseOrderItem)
                    '.DocFile = Await OrderDocfile(exs, oOrder)
                    Dim lines = Await PromofarmaOrder.OrderLines(exs, oOrder)
                    For Each oLine In lines
                        If oLine.ean.Count > 0 Then
                            Dim oSku = oCache.FindSku(DTOEan.Factory(oLine.ean.First()))
                            Dim oRrpp = DTOAmt.Factory(oCache.RetailPrice(oSku.Guid))
                            Dim oRepCom As New DTORepCom(New DTORep(oMarketPlace.Guid), 0)
                            Dim item = DTOPurchaseOrderItem.Factory(retval.PurchaseOrder, oSku, oLine.quantity, oRrpp, 0, oRepCom)
                            .Items.Add(item)
                            'warn if oSku.RRPP <> line.price
                        End If
                    Next
                End With
                .Fch = .PurchaseOrder.Fch
                Dim noms = oOrder.data.customer_name.Split(" ")
                .Nom = noms.First()
                If noms.Count > 1 Then
                    .Cognom1 = noms(1)
                    If noms.Count > 2 Then
                        Dim idx = oOrder.data.customer_name.IndexOf(.Cognom1) + .Cognom1.Length
                        .Cognom2 = oOrder.data.customer_name.Substring(idx).Trim()
                    End If
                End If
                'Dim oShippingAddress = oOrder.customer.shipping_address
                'If oShippingAddress Is Nothing Then
                '        exs.Add(New Exception("falta la adreça d'enviament a la comanda"))
                '    Else
                '    .Address = Await Address(exs, oShippingAddress, retval, DTOAddress.Codis.Entregas)
                '    .Tel = oShippingAddress.phone
                '        .Delivery = .Deliver(oUser, DTOCustomer.CashCodes.credit)
                '        With .Delivery
                '            .Mgz = oUser.Emp.Mgz
                '            .PortsCod = DTOCustomer.PortsCodes.pagats
                '            .ExportCod = .Address.Zip.ExportCod
                '        End With

                '        Dim oBillingAddress = oOrder.customer.billing_address
                '        If oBillingAddress Is Nothing Then
                '            .FraAddress = Await Address(exs, oOrder.customer.shipping_address, retval, DTOAddress.Codis.Fiscal)
                '        Else
                '            .FraAddress = Await Address(exs, oBillingAddress, retval, DTOAddress.Codis.Fiscal)
                '            If String.IsNullOrEmpty(oBillingAddress.firstname & oBillingAddress.lastname) Then
                '                .FraNom = .FullNom()
                '            Else
                '                .FraNom = (oBillingAddress.firstname & " " & oBillingAddress.lastname).Trim()
                '            End If
                '        End If
                '    End If
            End With
        End If
        Return retval
    End Function



End Class
Public Class PromofarmaOrders
    Shared Async Function Fetch(exs As List(Of Exception)) As Threading.Tasks.Task(Of DTO.Integracions.Promofarma.OrderList)
        Dim FchFrom = UrlFriendlyFch(New DateTime(DTO.GlobalVariables.Today().Year, 1, 1, 0, 0, 0))
        Dim FchTo = UrlFriendlyFch(Date.Now)
        Dim segments = String.Format("orders/?from={0}&to={1}", FchFrom, FchTo)
        Dim url = DTO.Integracions.Promofarma.Api.Url(segments)
        Dim headers = DTO.Integracions.Promofarma.Api.AuthHeader
        Return Await Api.GetRequest(Of DTO.Integracions.Promofarma.OrderList)(exs, headers, url)
    End Function

    Shared Function UrlFriendlyFch(src As Date) As String
        Dim value = src.ToString("yyyy-MM-ddTHH\:mm\:sszzz", System.Globalization.CultureInfo.InvariantCulture)
        Dim retval = System.Web.HttpUtility.UrlEncode(value)
        Return retval
    End Function



End Class
