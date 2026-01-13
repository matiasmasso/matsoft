Public Class Worten


    Shared Async Function Catalog(exs As List(Of Exception)) As Task(Of DTO.Integracions.Worten.Catalog)
        Return Await GetRequest(Of DTO.Integracions.Worten.Catalog)(exs, "hierarchies")
    End Function

    Shared Async Function Shop(exs As List(Of Exception)) As Task(Of DTO.Integracions.Worten.Shop)
        Return Await GetRequest(Of DTO.Integracions.Worten.Shop)(exs, "account")
    End Function

    Shared Async Function ShopOffer(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of DTO.Integracions.Worten.ShopOffer)
        Dim retval As DTO.Integracions.Worten.ShopOffer = Await GetRequest(Of DTO.Integracions.Worten.ShopOffer)(exs, "offers")
        Dim eans = retval.offers.Select(Function(x) DTOEan.Factory(x.Ean())).ToList()
        Dim oWorten = DTOCustomer.Wellknown(DTOCustomer.Wellknowns.worten)
        Dim oSkus = Await ProductSkus.Search(exs, eans, oWorten, oEmp.Mgz)
        For Each oOffer In retval.offers
            oOffer.Sku = oSkus.FirstOrDefault(Function(x) x.Ean13 IsNot Nothing AndAlso x.Ean13.Value = oOffer.Ean())
        Next
        Return retval
    End Function

    Shared Async Function Orders(exs As List(Of Exception)) As Task(Of DTO.Integracions.Worten.OrderList)
        Return Await GetRequest(Of DTO.Integracions.Worten.OrderList)(exs, "orders")
    End Function

    Shared Async Function AcceptAllOrderLines(exs As List(Of Exception), order As DTO.Integracions.Worten.OrderClass) As Task(Of Boolean)
        Dim value As New DTO.Integracions.Worten.OrderClass.Acceptance
        For Each line In order.order_lines
            value.AcceptLine(line)
        Next
        Dim url = String.Format("orders/{0}/accept", order.order_id)
        Dim retval = Await PutRequest(Of DTO.Integracions.Worten.OrderClass.Acceptance)(exs, value, url)
        Return retval
    End Function

    Shared Async Function OrderDocuments(exs As List(Of Exception), order As DTO.Integracions.Worten.OrderClass) As Threading.Tasks.Task(Of DTO.Integracions.Worten.DocumentList)
        Dim url = String.Format("orders/documents?order_ids={0}", order.order_id)
        Dim retval = Await Worten.GetRequest(Of DTO.Integracions.Worten.DocumentList)(exs, url)
        Return retval
    End Function

    Shared Async Function OrderDocument(exs As List(Of Exception), order As DTO.Integracions.Worten.OrderClass) As Threading.Tasks.Task(Of Byte())
        Dim retval As Byte() = Nothing
        Dim orderDocs = Await OrderDocuments(exs, order)
        If exs.Count = 0 Then
            Dim orderDoc = orderDocs.order_documents.FirstOrDefault(Function(x) x.type = DTO.Integracions.Worten.Document.DocTypes.SYSTEM_DELIVERY_BILL.ToString())
            If orderDoc Is Nothing Then
                exs.Add(New Exception("Aquesta comanda no disposa de 'Delivery Bill'"))
            Else
                Dim url = String.Format("orders/documents/download?document_ids={0}", orderDoc.id)
                retval = Await Worten.GetBinaryRequest(exs, url)
            End If
        End If
        Return retval
    End Function

    Shared Async Function OrderDocfile(exs As List(Of Exception), order As DTO.Integracions.Worten.OrderClass) As Threading.Tasks.Task(Of DTODocFile)
        Dim retval As DTODocFile = Nothing
        Dim orderDocument = Await Worten.OrderDocument(exs, order)
        If exs.Count = 0 Then
            retval = LegacyHelper.DocfileHelper.Factory(exs, orderDocument)
        End If
        Return retval
    End Function

    Shared Async Function UpdateStocks(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of DTO.DTOTaskLog)
        Return Await Api.Fetch(Of DTO.DTOTaskLog)(exs, "worten/updateStocks", oEmp.Id)
    End Function

    Shared Async Function Update(exs As List(Of Exception), oOffer As DTO.Integracions.Worten.Offer) As Task(Of DTO.Integracions.Worten.ImportResult)
        Dim oShopOffer As New DTO.Integracions.Worten.ShopOffer
        oShopOffer.offers = New List(Of DTO.Integracions.Worten.Offer)
        oShopOffer.offers.Add(oOffer)
        oOffer.update_delete = "update"
        oOffer.state_code = "11"
        Return Await PostRequest(Of DTO.Integracions.Worten.ShopOffer, DTO.Integracions.Worten.ImportResult)(oShopOffer, exs, "offers")
    End Function

    Shared Async Function ConsumerTicket(exs As List(Of Exception), oOrder As DTO.Integracions.Worten.OrderClass, oUser As DTOUser) As Task(Of DTOConsumerTicket)
        Dim retval As DTOConsumerTicket = Nothing
        Dim oShopOffer = Await Worten.ShopOffer(exs, oUser.Emp)
        If exs.Count = 0 Then
            Dim oMarketPlace = DTOMarketPlace.Wellknown(DTOMarketPlace.Wellknowns.Worten)
            retval = DTOConsumerTicket.Factory(oUser, oMarketPlace, oOrder.order_id)
            With retval
                With .PurchaseOrder
                    .Emp = oUser.Emp
                    .Fch = oOrder.created_date.Date()
                    .Concept = oOrder.order_id
                    .Items = New List(Of DTOPurchaseOrderItem)
                    .DocFile = Await OrderDocfile(exs, oOrder)
                End With
                Dim oRepCom As New DTORepCom(New DTORep(oMarketPlace.Guid), 0)
                For Each oLine In oOrder.order_lines
                    Dim oOffer = oShopOffer.offers.FirstOrDefault(Function(x) x.offer_id = oLine.offer_id)
                    If oOffer IsNot Nothing AndAlso oOffer.Sku IsNot Nothing Then
                        If oLine.price_unit <> oOffer.Sku.Rrpp.Eur Then exs.Add(New Exception(String.Format("{0} de preu {1} demanat per {2}", oOffer.Sku.NomLlarg, oOffer.Sku.Rrpp.Formatted, DTOAmt.Factory(oLine.price_unit).Formatted)))
                        Dim item = DTOPurchaseOrderItem.Factory(.PurchaseOrder, oOffer.Sku, oLine.quantity, oOffer.Sku.Rrpp, 0, oRepCom)
                        .PurchaseOrder.Items.Add(item)
                    End If
                Next
                .Fch = .PurchaseOrder.Fch

                Dim oShippingAddress = oOrder.customer.shipping_address
                If oShippingAddress Is Nothing Then
                    exs.Add(New Exception("falta la adreça d'enviament a la comanda"))
                Else
                    .Nom = oShippingAddress.firstname
                    .Cognom1 = oShippingAddress.lastname
                    .Address = Await Address(exs, oShippingAddress, retval, DTOAddress.Codis.Entregas)
                    .Tel = oShippingAddress.phone
                    .Delivery = .Deliver(oUser, DTOCustomer.CashCodes.credit)
                    With .Delivery
                        .Mgz = oUser.Emp.Mgz
                        .PortsCod = DTOCustomer.PortsCodes.pagats
                        .ExportCod = .Address.Zip.ExportCod
                    End With

                    Dim oBillingAddress = oOrder.customer.billing_address
                    If oBillingAddress Is Nothing Then
                        .FraAddress = Await Address(exs, oOrder.customer.shipping_address, retval, DTOAddress.Codis.Fiscal)
                    Else
                        .FraAddress = Await Address(exs, oBillingAddress, retval, DTOAddress.Codis.Fiscal)
                        If String.IsNullOrEmpty(oBillingAddress.firstname & oBillingAddress.lastname) Then
                            .FraNom = .FullNom()
                        Else
                            .FraNom = (oBillingAddress.firstname & " " & oBillingAddress.lastname).Trim()
                        End If
                    End If
                End If
            End With
        End If

        Return retval
    End Function

    Private Shared Async Function Address(exs As List(Of Exception), src As DTO.Integracions.Worten.Address, oConsumerTicket As DTOConsumerTicket, oCodi As DTOAddress.Codis) As Task(Of DTOAddress)
        Dim retval As DTOAddress = Nothing
        If src IsNot Nothing Then
            retval = DTOAddress.Factory(oConsumerTicket, DTOAddress.Codis.Fiscal)
            retval.Text = src.street_1
            If Not String.IsNullOrEmpty(src.street_2) Then
                retval.Text += vbCrLf & src.street_2
            End If
            Dim oCountry = Await Country.FromIso(src.country, exs)
            If exs.Count = 0 Then
                If oCountry Is Nothing Then
                    exs.Add(New Exception(String.Format("pais desconegut amb ISO '{0}'", src.country)))
                Else
                    retval.Zip = Await Zip.FindOrCreate(exs, oCountry, src.zip_code, src.city)
                End If
            End If
        End If
        Return retval
    End Function

    Shared Async Function GetRequest(Of T)(exs As List(Of Exception), urlSegment As String) As Task(Of T)
        Dim url = String.Format("{0}/api/{1}", DTO.Integracions.Worten.Globals.ApiUrl, urlSegment)
        Dim headers As New Dictionary(Of String, String)
        headers.Add("Authorization", DTO.Integracions.Worten.Globals.ApiKey)
        Return Await Api.GetRequest(Of T)(exs, headers, url)
    End Function

    Shared Async Function GetBinaryRequest(exs As List(Of Exception), urlSegment As String) As Task(Of Byte())
        Dim url = String.Format("{0}/api/{1}", DTO.Integracions.Worten.Globals.ApiUrl, urlSegment)
        Dim headers As New Dictionary(Of String, String)
        headers.Add("Authorization", DTO.Integracions.Worten.Globals.ApiKey)
        Return Await Api.GetBinaryRequest(exs, headers, url)
    End Function

    Shared Async Function PutRequest(Of T)(exs As List(Of Exception), value As T, urlSegment As String) As Task(Of Boolean)
        Dim url = String.Format("{0}/api/{1}", DTO.Integracions.Worten.Globals.ApiUrl, urlSegment)
        Dim headers As New Dictionary(Of String, String)
        headers.Add("Authorization", DTO.Integracions.Worten.Globals.ApiKey)
        Return Await Api.PutRequest(Of T)(exs, value, headers, url)
    End Function

    Shared Async Function PostRequest(Of U, T)(value As U, exs As List(Of Exception), urlSegment As String) As Task(Of T)
        Dim url = String.Format("{0}/api/{1}", DTO.Integracions.Worten.Globals.ApiUrl, urlSegment)
        Dim headers As New Dictionary(Of String, String)
        headers.Add("Authorization", DTO.Integracions.Worten.Globals.ApiKey)
        Return Await Api.PostRequest(Of U, T)(value, exs, headers, url)
    End Function
End Class
