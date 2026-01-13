Public Class Delivery
    Inherits _FeblBase

    Shared Function Factory(exs As List(Of Exception), oPurchaseOrder As DTOPurchaseOrder, oMgz As DTOMgz, DtFch As Date, oUser As DTOUser) As DTODelivery
        Dim retval As DTODelivery = Nothing
        If oPurchaseOrder.Cod = DTOPurchaseOrder.Codis.proveidor Then
            retval = Factory(exs, oPurchaseOrder.Proveidor, oUser, oMgz)
        Else
            retval = Factory(exs, oPurchaseOrder.Customer, oUser, oMgz)
        End If

        With retval
            .Mgz = oMgz
            .Fch = DtFch

            If oPurchaseOrder.Platform IsNot Nothing AndAlso oPurchaseOrder.Platform.UnEquals(.Platform) Then
                .Platform = oPurchaseOrder.Platform
                .Address = .Platform.Address
                .Tel = .Platform.Telefon
            End If
        End With
        Return retval
    End Function

    Shared Function Factory(exs As List(Of Exception), oProveidor As DTOProveidor, oUser As DTOUser, oMgz As DTOMgz) As DTODelivery
        FEB2.Proveidor.Load(oProveidor, exs)
        Dim retval As New DTODelivery
        With retval
            .Emp = oUser.Emp
            .Cod = DTOPurchaseOrder.Codis.proveidor
            .Proveidor = oProveidor
            .Nom = oProveidor.NomComercialOrDefault()
            .Address = oProveidor.Address
            .Mgz = oMgz
            .Fch = DateTime.Today
            .PortsCod = DTOCustomer.PortsCodes.altres
            .UsrLog = DTOUsrLog2.Factory(oUser)
            .Valorado = True
            .Items = New List(Of DTODeliveryItem)
        End With
        Return retval
    End Function

    Shared Function Factory(exs As List(Of Exception), oCustomer As DTOCustomer, oUser As DTOUser, oMgz As DTOMgz) As DTODelivery
        Dim retval As DTODelivery = Nothing
        If FEB2.Customer.Load(oCustomer, exs) Then
            Dim oCcx = FEB2.Customer.CcxOrMe(exs, oCustomer)
            retval = New DTODelivery
            With retval
                .Emp = oCustomer.Emp
                .Cod = DTOPurchaseOrder.Codis.client
                .Customer = oCustomer
                .Nom = oCustomer.NomComercialOrDefault()
                .Mgz = oMgz
                .Fch = DateTime.Today

                .CashCod = oCcx.CashCod
                .Facturable = True
                .RetencioCod = DTODelivery.RetencioCods.free
                .Valorado = oCcx.AlbValorat

                .PortsCod = oCcx.PortsCod
                .Transportista = DTOTransportista.Wellknown(DTOTransportista.Wellknowns.tnt)
                .Address = FEB2.Customer.ShippingAddressOrDefault(oCustomer)
                If oCustomer.DeliveryPlatform Is Nothing Then
                    .Tel = oCustomer.Telefon
                    .ExportCod = DTOAddress.ExportCod(.Address)
                Else
                    If FEB2.Contact.Load(oCustomer.DeliveryPlatform, exs) Then
                        .Platform = oCustomer.DeliveryPlatform
                        '.Address = .Platform.Address (quan hi ha plataforma la adreça ha de ser la destinació final)
                        .ExportCod = DTOAddress.ExportCod(FEB2.Customer.ShippingAddressOrDefault(oCustomer))
                        .Tel = .Platform.Telefon
                    End If
                End If

                .UsrLog = DTOUsrLog2.Factory(oUser)
                .Items = New List(Of DTODeliveryItem)
            End With
        Else
            exs.Add(New Exception("Client desconegut"))
        End If
        Return retval
    End Function

    Shared Async Function DeliveryDate(exs As List(Of Exception), oDelivery As DTODelivery) As Task(Of Date)
        '2 dies habils si es genera abans de la transmisió
        Dim fchTransmisio As Date
        If FEB2.Delivery.Load(oDelivery, exs) Then
            If oDelivery.Transmisio Is Nothing Then
                Dim oDateTimeOffset = Await FEB2.Transmisio.NextTime(exs)
                If exs.Count = 0 Then fchTransmisio = oDateTimeOffset.Date
            Else
                fchTransmisio = oDelivery.Transmisio.Fch.Date
            End If
        End If
        Dim retval = TimeHelper.AddDiasHabils(fchTransmisio, 2)
        Return retval
    End Function


    Shared Async Function DeliveryWithTracking(exs As List(Of Exception), oDelivery As DTODelivery) As Task(Of DTODelivery)
        Dim retval = Await Api.Fetch(Of DTODelivery)(exs, "delivery/withtracking", oDelivery.Guid.ToString())
        Return retval
    End Function


    Shared Async Function OrdrSps(exs As List(Of Exception), oDelivery As DTODelivery, oSender As DTOContact) As Task(Of List(Of DTOEdiOrdrsp))
        Dim retval As New List(Of DTOEdiOrdrsp)
        Dim fchDelivery = Await FEB2.Delivery.DeliveryDate(exs, oDelivery)
        Dim oPurchaseOrders = oDelivery.getPurchaseOrders()
        For Each oPurchaseOrder In oPurchaseOrders
            oPurchaseOrder = Await FEB2.PurchaseOrder.Find(oPurchaseOrder.Guid, exs)
            For Each item In oPurchaseOrder.Items
                Dim oDeliveredItem = oDelivery.Items.FirstOrDefault(Function(x) x.PurchaseOrderItem.Equals(item))
                If oDeliveredItem Is Nothing Then
                    If item.Sku.obsoleto Or item.Sku.LastProduction Then
                        item.ShippingResult = DTOPurchaseOrderItem.ShippingResults.NoLongerAvailable
                    Else
                        item.ShippingResult = DTOPurchaseOrderItem.ShippingResults.TemporarilyOutOfStock
                    End If
                Else
                    item.ShippingResult = DTOPurchaseOrderItem.ShippingResults.Dispatched
                End If
            Next
            Dim oEdiOrdrsp = DTOEdiOrdrsp.Factory(oPurchaseOrder, oSender, oDelivery.Fch, fchDelivery)
            retval.Add(oEdiOrdrsp)
        Next
        Return retval
    End Function



    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTODelivery)
        Dim retval = Await Api.Fetch(Of DTODelivery)(exs, "delivery", oGuid.ToString())
        If retval IsNot Nothing Then
            Select Case retval.Cod
                Case DTOPurchaseOrder.Codis.reparacio
                    RestoreSpvProduct(retval)
                Case Else
                    For Each item In retval.Items
                        If item.PurchaseOrderItem IsNot Nothing Then
                            If item.PurchaseOrderItem.PurchaseOrder IsNot Nothing Then
                                If retval.Cod = DTOPurchaseOrder.Codis.proveidor Then
                                    item.PurchaseOrderItem.PurchaseOrder.Proveidor = retval.Proveidor
                                Else
                                    item.PurchaseOrderItem.PurchaseOrder.Customer = retval.Customer
                                End If
                            End If
                        End If
                    Next
            End Select
        End If
        Return retval
    End Function


    Shared Async Function FromFormattedId(exs As List(Of Exception), oEmp As DTOEmp, sFormattedId As String) As Task(Of DTODelivery)
        Dim retval As DTODelivery = Nothing
        If sFormattedId.Length = 10 Then
            Dim sYear As String = sFormattedId.Substring(0, 4)
            Dim sId As String = sFormattedId.Substring(4, 6)
            If TextHelper.VbIsNumeric(sYear) And TextHelper.VbIsNumeric(sId) Then
                retval = Await FromNum(oEmp, CInt(sYear), CInt(sId), exs)
            Else
                exs.Add(New Exception(String.Format("{0} no es un format vàlid de albarà", sFormattedId)))
            End If
        Else
            exs.Add(New Exception(String.Format("{0} no es un format vàlid de albarà", sFormattedId)))
        End If
        Return retval
    End Function

    Shared Async Function FromNum(oEmp As DTOEmp, year As Integer, id As Integer, exs As List(Of Exception)) As Task(Of DTODelivery)
        Dim retval = Await Api.Fetch(Of DTODelivery)(exs, "delivery/fromNum", oEmp.Id, year, id)
        'retval.IsLoaded = False (retval may be nothing when Checking for existing recycled nums)
        If retval IsNot Nothing Then
            Select Case retval.Cod
                Case DTOPurchaseOrder.Codis.reparacio
                    RestoreSpvProduct(retval)
                Case Else
                    For Each item In retval.Items
                        If item.PurchaseOrderItem IsNot Nothing Then
                            If item.PurchaseOrderItem.PurchaseOrder IsNot Nothing Then
                                If retval.Cod = DTOPurchaseOrder.Codis.proveidor Then
                                    item.PurchaseOrderItem.PurchaseOrder.Proveidor = retval.Proveidor
                                Else
                                    item.PurchaseOrderItem.PurchaseOrder.Customer = retval.Customer
                                End If
                            End If
                        End If
                    Next
            End Select

        End If

        Return retval
    End Function

    Shared Function Load(ByRef odelivery As DTODelivery, exs As List(Of Exception)) As Boolean
        If Not odelivery.IsLoaded And Not odelivery.IsNew Then
            Dim pdelivery = Api.FetchSync(Of DTODelivery)(exs, "delivery", odelivery.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTODelivery)(pdelivery, odelivery, exs)
                Select Case odelivery.Cod
                    Case DTOPurchaseOrder.Codis.reparacio
                        RestoreSpvProduct(odelivery)
                    Case Else
                        For Each item In odelivery.Items
                            item.Delivery = odelivery
                            If item.PurchaseOrderItem IsNot Nothing Then
                                If item.PurchaseOrderItem.PurchaseOrder IsNot Nothing Then
                                    If odelivery.Cod = DTOPurchaseOrder.Codis.proveidor Then
                                        item.PurchaseOrderItem.PurchaseOrder.Proveidor = odelivery.Proveidor
                                    Else
                                        item.PurchaseOrderItem.PurchaseOrder.Customer = odelivery.Customer
                                    End If
                                End If
                            End If
                        Next
                End Select
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Sub RestoreSpvProduct(ByRef oDelivery As DTODelivery)
        If oDelivery IsNot Nothing Then
            Select Case oDelivery.Cod
                Case DTOPurchaseOrder.Codis.reparacio
                    Dim oSpvs = oDelivery.Items.GroupBy(Function(x) x.Spv.Guid).Select(Function(y) y.First.Spv).ToList
                    For Each oSpv In oSpvs
                        oSpv.product = DTOProduct.fromJObject(oSpv.product)
                        For Each oItem In oDelivery.Items.Where(Function(x) x.Spv.Guid.Equals(oSpv.Guid))
                            oItem.Spv = oSpv
                        Next
                    Next
            End Select
        End If
    End Sub

    Shared Async Function Update(exs As List(Of Exception), ByVal oDelivery As DTODelivery) As Task(Of Integer)
        Dim pDelivery As New DTODelivery(oDelivery.Guid)
        DTOBaseGuid.CopyPropertyValues(Of DTODelivery)(oDelivery, pDelivery, exs)
        With pDelivery
            .Emp = .Emp.trimmed
            Dim sFullNom = .Contact.FullNom
            If .Cod = DTOPurchaseOrder.Codis.proveidor Then
                .Proveidor = New DTOProveidor(.Proveidor.Guid)
                .Proveidor.FullNom = sFullNom
            Else
                .Customer = New DTOCustomer(.Customer.Guid)
                .Customer.FullNom = sFullNom
            End If
            Select Case .Cod
                Case DTOPurchaseOrder.Codis.client, DTOPurchaseOrder.Codis.proveidor
                    For Each item In .Items
                        Dim idx = .Items.IndexOf(item)
                        item.PurchaseOrderItem.PurchaseOrder = New DTOPurchaseOrder(item.PurchaseOrderItem.PurchaseOrder.Guid)
                    Next
            End Select
        End With
        Return Await Api.Execute(Of DTODelivery, Integer)(pDelivery, exs, "Delivery/update")
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oDelivery As DTODelivery, Optional ShowWarnings As Boolean = True) As Task(Of Boolean)
        Return Await Api.Execute(Of DTODelivery)(oDelivery, exs, "Delivery/delete", OpcionalBool(ShowWarnings))
    End Function

    Shared Async Function CobraPerVisa(exs As List(Of Exception), oEmp As DTOEmp, oTpvLog As DTOTpvLog) As Task(Of DTOTpvLog)
        Return Await Api.Execute(Of DTOTpvLog, DTOTpvLog)(oTpvLog, exs, "Delivery/CobraPerVisa", oEmp.Id)
    End Function

    Shared Async Function CobraPerTransferenciaPrevia(exs As List(Of Exception), value As DTOCobramentPerTransferencia) As Task(Of DTOCca)
        Return Await Api.Execute(Of DTOCobramentPerTransferencia, DTOCca)(value, exs, "Delivery/cobraPerTransferencia")
    End Function

    Shared Async Function UpdateJustificante(exs As List(Of Exception), oDelivery As DTODelivery, ByVal oJustificanteCode As DTODelivery.JustificanteCodes) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "delivery/UpdateJustificante", oDelivery.Guid.ToString, oJustificanteCode)
    End Function

    Shared Function TotalSync(exs As List(Of Exception), oDelivery As DTODelivery) As DTOAmt
        Return Api.FetchSync(Of DTOAmt)(exs, "delivery/total", oDelivery.Guid.ToString())
    End Function

    Shared Async Function Total(exs As List(Of Exception), oDelivery As DTODelivery) As Task(Of DTOAmt)
        Return Await Api.Fetch(Of DTOAmt)(exs, "delivery/total", oDelivery.Guid.ToString())
    End Function

    Shared Async Function SetCurExchangeRate(exs As List(Of Exception), oDelivery As DTODelivery, oCur As DTOCur, oRate As DTOCurExchangeRate) As Task(Of Boolean)
        Return Await Api.Execute(Of DTOCurExchangeRate, Boolean)(oRate, exs, "Delivery/SetCurExchangeRate", oDelivery.Guid.ToString, oCur.Tag)
    End Function

    Shared Async Function Pdf(oDelivery As DTODelivery, proforma As Boolean, exs As List(Of Exception)) As Task(Of Byte())
        Return Await Api.FetchBinary(exs, "delivery/pdf", oDelivery.Guid.ToString, If(proforma, 1, 0))
    End Function


    Shared Function DocPdcText(oDelivery As DTODelivery, ByVal oOrder As DTOPurchaseOrder) As String
        Dim sRetVal As String = ""

        Dim oLang As DTOLang
        oLang = oDelivery.Contact.Lang
        Select Case oDelivery.Cod
            Case DTOPurchaseOrder.Codis.client, DTOPurchaseOrder.Codis.reparacio
                sRetVal = oLang.Tradueix("Su pedido", "La seva comanda", "Your order")
                If oOrder.Concept > "" Then
                    sRetVal += " " & oOrder.Concept.Trim
                End If
            Case DTOPurchaseOrder.Codis.proveidor
                sRetVal = oLang.Tradueix("Nuestro pedido", "La nostre comanda", "Our order") & " " & oOrder.Num
            Case Else
                sRetVal = ""
        End Select

        If sRetVal > "" Then
            Dim StFrom As String = oLang.Tradueix("de fecha", "de data", "from")
            sRetVal += " " & StFrom & " " & TextHelper.VbFormat(oOrder.Fch, "dd/MM/yy")
        End If
        Return sRetVal
    End Function

    Shared Function LoadCustSkuRefs(ByRef oDelivery As DTODelivery, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = True
        Dim oCustomer As DTOCustomer = oDelivery.Customer.CcxOrMe
        Dim oCustomerProducts As List(Of DTOCustomerProduct) = FEB2.CustomerProducts.AllSync(exs, oCustomer)
        For Each item As DTODeliveryItem In oDelivery.Items
            Dim oCustomerProduct As DTOCustomerProduct = oCustomerProducts.FirstOrDefault(Function(x) x.Sku.Equals(item.Sku))
            If oCustomerProduct Is Nothing Then
                exs.Add(New Exception("falta ref.client de " & item.Sku.NomLlarg.Esp))
                retval = False
            Else
                item.Sku.RefCustomer = oCustomerProduct.Ref 'TO DEPRECATE
                item.Sku.CustomerProduct = oCustomerProduct
            End If
        Next
        Return retval
    End Function

    Shared Function LoadMadeIns(ByRef oDelivery As DTODelivery, oCountries As List(Of DTOCountry), exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = True
        For Each item As DTODeliveryItem In oDelivery.Items
            Dim oSku As DTOProductSku = item.Sku
            Dim oCountry As DTOCountry = oSku.madeInOrInherited
            If oCountry IsNot Nothing Then
                oSku.MadeIn = oCountries.FirstOrDefault(Function(x) x.Equals(oCountry))
            End If
        Next
        Return retval
    End Function


    Shared Function DeliveryNom(oDelivery As DTODelivery) As String
        Dim retVal As String = ""

        If oDelivery.Platform Is Nothing Then
            retVal = oDelivery.Nom
        Else
            retVal = oDelivery.Platform.Nom
        End If
        Return retVal
    End Function

    Shared Function DeliveryAdr(oDelivery As DTODelivery) As String
        Dim exs As New List(Of Exception)
        Dim retVal As String = ""
        If oDelivery.Platform Is Nothing Then
            retVal = oDelivery.Address.Text
        Else
            If oDelivery.Platform.Address Is Nothing Then
                FEB2.Contact.Load(oDelivery.Platform, exs)
            End If
            retVal = oDelivery.Platform.Address.Text
        End If
        If retVal > "" Then
            retVal = retVal.Replace(vbCrLf, " - ")
        End If
        Return retVal
    End Function

    Shared Function DeliveryZip(oDelivery As DTODelivery) As DTOZip
        Dim retVal As DTOZip = Nothing
        If oDelivery.Platform Is Nothing Then
            retVal = oDelivery.Address.Zip
        Else
            retVal = oDelivery.Platform.Address.Zip
        End If
        Return retVal
    End Function

    Shared Function DeliveryLocationNom(oDelivery As DTODelivery) As String
        Dim retVal As String = ""
        Dim oAddress As DTOAddress = Nothing
        If oDelivery.Platform Is Nothing Then
            oAddress = oDelivery.Address
        Else
            oAddress = oDelivery.Platform.Address
        End If
        If oAddress IsNot Nothing Then
            If oAddress.Zip IsNot Nothing Then
                If oAddress.Zip.Location IsNot Nothing Then
                    retVal = oAddress.Zip.Location.Nom
                End If
            End If
        End If
        Return retVal
    End Function


    Shared Function DeliveryGLN(oDelivery As DTODelivery) As DTOEan
        Dim retVal As DTOEan = Nothing
        If oDelivery.Platform Is Nothing Then
            retVal = oDelivery.Customer.GLN
        Else
            retVal = oDelivery.Platform.GLN
        End If
        Return retVal
    End Function


    Shared Async Function MailToSubscriptors(oEmp As DTOEmp, oDelivery As DTODelivery, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "delivery/MailToSubscriptors", oEmp.Id, oDelivery.Guid.ToString())
    End Function

    Shared Function PdfUrl(oDelivery As DTODelivery, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval = FEB2.UrlHelper.Factory(AbsoluteUrl, "doc", CInt(DTODocFile.Cods.alb), oDelivery.Guid.ToString())
        Return retval
    End Function

    Shared Function Url(oDelivery As DTODelivery, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = ""
        If oDelivery IsNot Nothing Then
            retval = UrlHelper.Factory(AbsoluteUrl, "albaran", oDelivery.Guid.ToString())
        End If
        Return retval
    End Function

    Shared Function EmailConfirmationRequestUrl(oDelivery As DTODelivery) As String
        Return UrlHelper.Factory(True, "mail/DeliveryConfirmationRequest", oDelivery.Guid.ToString())
    End Function

    Shared Function UrlTpv(value As DTODelivery, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = ""
        If value IsNot Nothing Then
            Dim oParameters As New Dictionary(Of String, String)
            oParameters.Add("Guid", value.Guid.ToString())
            oParameters.Add("Mode", DTOTpvRequest.Modes.Alb)
            Dim sParameter As String = CryptoHelper.UrlFriendlyBase64Json(oParameters)
            retval = UrlHelper.Factory(AbsoluteUrl, "tpv", sParameter)
        End If
        Return retval
    End Function

    Shared Function ExcelTraspas(oDelivery As DTODelivery, Optional oLang As DTOLang = Nothing) As ExcelHelper.Sheet
        Dim sTitle As String = oDelivery.Formatted()
        Dim oDomain = DTOWebDomain.Factory(oLang, True)
        Dim oSortides = oDelivery.Items.Where(Function(x) x.Cod < 50).ToList
        Dim oEntrades = oDelivery.Items.Where(Function(x) x.Cod >= 50).ToList
        Dim oMgzFrom = oSortides.First().Mgz
        Dim oMgzTo = oEntrades.First.Mgz
        Dim exs As New List(Of Exception)
        FEB2.Contact.Load(oMgzFrom, exs)
        FEB2.Contact.Load(oMgzTo, exs)
        sTitle += String.Format(" traspas de {0} a {1}", oMgzFrom.Nom, oMgzTo.Nom)
        Dim retval As New ExcelHelper.Sheet(sTitle, sTitle)
        With retval
            .DisplayTotals = True
            .AddColumn("ref M+O")
            .AddColumn("producte")
            .AddColumn(oMgzFrom.Nom, ExcelHelper.Sheet.NumberFormats.Integer)
            .AddColumn("preu", ExcelHelper.Sheet.NumberFormats.Euro)
            .AddColumn(oMgzTo.Nom, ExcelHelper.Sheet.NumberFormats.Integer)
            .AddColumn("preu", ExcelHelper.Sheet.NumberFormats.Euro)
        End With
        For Each item In oSortides
            Dim oRow = retval.AddRow
            With item
                Dim url = .Sku.GetUrl(oDomain.DefaultLang,, True)

                oRow.AddCell(.Sku.Id)
                oRow.AddCell(.Sku.NomLlarg.Esp, url)
                oRow.AddCell(.Qty)
                oRow.AddCellAmt(.Price)

                Dim oEntrada = oEntrades.FirstOrDefault(Function(x) x.Lin = item.Lin - 1)
                If oEntrada IsNot Nothing Then
                    oRow.AddCell(oEntrada.Qty)
                    oRow.AddCellAmt(oEntrada.Price)
                End If
            End With
        Next
        Return retval

    End Function

    Shared Function Excel(exs As List(Of Exception), oDelivery As DTODelivery) As ExcelHelper.Book
        Dim retval As New ExcelHelper.Book
        If Load(oDelivery, exs) Then
            retval.Sheets.Add(FEB2.Delivery.ExcelMercancia(oDelivery))
            retval.Sheets.Add(FEB2.Delivery.ExcelBultos(exs, oDelivery))
        End If
        Return retval
    End Function

    Shared Function ExcelMercancia(oDelivery As DTODelivery, Optional oLang As DTOLang = Nothing) As ExcelHelper.Sheet
        Dim retval As MatHelperStd.ExcelHelper.Sheet
        Select Case oDelivery.Cod
            Case DTOPurchaseOrder.Codis.traspas
                retval = FEB2.Delivery.ExcelTraspas(oDelivery, oLang)
            Case Else
                retval = FEB2.Delivery.ExcelRaw(oDelivery, oLang)
        End Select
        Return retval
    End Function

    Shared Function ExcelBultos(exs As List(Of Exception), oDelivery As DTODelivery) As ExcelHelper.Sheet
        Dim retval As New MatHelperStd.ExcelHelper.Sheet()
        If FEB2.Delivery.Load(oDelivery, exs) Then
            Dim oLang = DTODelivery.lang(oDelivery)
            Dim sTitle = String.Format("albaran M+O {0}.{1}", oDelivery.Fch.Year, oDelivery.Id)
            retval = New MatHelperStd.ExcelHelper.Sheet("packing list", sTitle)
            With retval
                .AddColumn("pallet")
                .AddColumn(oLang.Tradueix("bulto", "bulto", "package"), ExcelHelper.Sheet.NumberFormats.Integer)
                .AddColumn(oLang.Tradueix("matrícula", "matricula", "SSCC plate"))
                .AddColumn(oLang.Tradueix("producto", "producte", "product"))
                .AddColumn(oLang.Tradueix("unidades", "unitats", "units"), ExcelHelper.Sheet.NumberFormats.Integer)
                .AddColumn(oLang.Tradueix("precio", "preu", "price"), ExcelHelper.Sheet.NumberFormats.Euro)
                .AddColumn(oLang.Tradueix("descuento", "descompte", "discount"), ExcelHelper.Sheet.NumberFormats.Percent)
                .AddColumn(oLang.Tradueix("importe", "import", "amount"), ExcelHelper.Sheet.NumberFormats.Euro)
                .AddColumn(oLang.Tradueix("volumen unitario", "volum unitari", "unit volume"), ExcelHelper.Sheet.NumberFormats.m3)
                .AddColumn(oLang.Tradueix("volumen", "volum", "volume"), ExcelHelper.Sheet.NumberFormats.m3)
                .AddColumn(oLang.Tradueix("linea", "linia", "line"), ExcelHelper.Sheet.NumberFormats.Integer)
            End With

        End If
        For Each oPallet In oDelivery.Pallets
            For Each oPackage In oPallet.Packages.OrderBy(Function(x) x.Num).ToList()
                Dim oRow = retval.AddRow()
                oRow.AddCell(oPallet.SSCC)
                oRow.AddCell(oPackage.Num)
                oRow.AddCell(oPackage.SSCC)
                For Each oItem In oPackage.Items
                    oRow.AddCell(oItem.DeliveryItem.Sku.NomLlarg.Tradueix(oDelivery.Customer.Lang))
                    oRow.AddCell(oItem.QtyInPackage)
                    With oItem.DeliveryItem
                        oRow.AddCellAmt(.Price)
                        oRow.AddCell(.Dto)
                        oRow.AddFormula("RC[-3]*RC[-2]*(100-RC[-1])/100")
                        oRow.AddCell(DTOProductSku.volumeM3OrInherited(.Sku))
                        oRow.AddFormula("RC[-5]*RC[-1]")
                        oRow.AddCell(oItem.DeliveryItem.Lin)
                    End With
                Next
            Next
        Next

        For Each oPackage In oDelivery.Packages.OrderBy(Function(x) x.Num).ToList()
            For Each oItem In oPackage.Items
                Dim oRow = retval.AddRow()
                oRow.AddCell()
                oRow.AddCell(oPackage.Num)
                oRow.AddCell(oPackage.SSCC)
                With oItem.DeliveryItem
                    oRow.AddCell(.Sku.NomLlarg.Tradueix(oDelivery.Customer.Lang))
                    oRow.AddCell(oItem.QtyInPackage)
                    oRow.AddCellAmt(.Price)
                    oRow.AddCell(.Dto)
                    oRow.AddFormula("RC[-3]*RC[-2]*(100-RC[-1])/100")
                    oRow.AddCell(DTOProductSku.volumeM3OrInherited(.Sku))
                    oRow.AddFormula("RC[-5]*RC[-1]")
                    oRow.AddCell(.Lin)
                End With
            Next
        Next
        Return retval
    End Function

    Shared Function ExcelRaw(oDelivery As DTODelivery, Optional oLang As DTOLang = Nothing, Optional valorat As Boolean = False) As ExcelHelper.Sheet
        If oLang Is Nothing Then oLang = DTODelivery.lang(oDelivery)
        Dim sTitle As String = oDelivery.Formatted()
        Dim oDomain = DTOWebDomain.Factory(oLang, True)
        Dim retval As New ExcelHelper.Sheet(sTitle, sTitle)
        With retval
            .DisplayTotals = True
            .AddColumn(oLang.Tradueix("linea", "linia", "line"), ExcelHelper.Sheet.NumberFormats.Integer)
            .AddColumn(oLang.Tradueix("pedido", "comanda", "order"))
            .AddColumn("ref M+O")
            If oDelivery.Cod = DTOPurchaseOrder.Codis.proveidor Then
                .AddColumn(oLang.Tradueix("ref.proveedor", "ref proveidor", "supplier code"))
            End If
            .AddColumn(oLang.Tradueix("producto", "producte", "product"))
            .AddColumn(oLang.Tradueix("cant.", "quant", "units"), ExcelHelper.Sheet.NumberFormats.Integer)

            If valorat Then
                .AddColumn(oLang.Tradueix("precio", "preu", "price"), ExcelHelper.Sheet.NumberFormats.Euro)
                .AddColumn(oLang.Tradueix("descuento", "descompte", "discount"), ExcelHelper.Sheet.NumberFormats.Percent)
                .AddColumn(oLang.Tradueix("importe", "import", "amount"), ExcelHelper.Sheet.NumberFormats.Euro)
            End If

            .AddColumn(oLang.Tradueix("uds./embalaje", "uts./embalatge", "units/package"), ExcelHelper.Sheet.NumberFormats.Integer)
            .AddColumn(oLang.Tradueix("long.embalaje", "llong.embalatge", "package length"), ExcelHelper.Sheet.NumberFormats.mm)
            .AddColumn(oLang.Tradueix("ancho embalaje", "ampl.embalatge", "package width"), ExcelHelper.Sheet.NumberFormats.mm)
            .AddColumn(oLang.Tradueix("alt.embalaje", "alç.embalatge", "package height"), ExcelHelper.Sheet.NumberFormats.mm)
            .AddColumn(oLang.Tradueix("volumen por embalaje", "volum embalatge", "package volume"), ExcelHelper.Sheet.NumberFormats.m3)
            .AddColumn(oLang.Tradueix("volumen total", "volum total", "total volume"), ExcelHelper.Sheet.NumberFormats.m3D2)
            .AddColumn(oLang.Tradueix("peso unitario", "pes unitari", "unit weight"), ExcelHelper.Sheet.NumberFormats.Kg)
            .AddColumn(oLang.Tradueix("peso", "pes", "weight"), ExcelHelper.Sheet.NumberFormats.KgD1)
            .AddColumn("made in")
            .AddColumn(oLang.Tradueix("código arancelario", "codi arancelari", "customs code"))
        End With

        For Each oLine As DTODeliveryItem In oDelivery.Items
            Dim oRow = retval.AddRow
            Dim url As String = ""
            With oLine
                oRow.AddCell(.Lin)
                If oDelivery.Cod = DTOPurchaseOrder.Codis.traspas Then
                    oRow.AddCell()
                Else
                    url = FEB2.PurchaseOrder.Url(.PurchaseOrderItem.PurchaseOrder, True)
                    If url > "" Then
                        oRow.AddCell(.PurchaseOrderItem.PurchaseOrder.Num, url)
                    Else
                        oRow.AddCell(.PurchaseOrderItem.PurchaseOrder.Num)
                    End If

                End If
                oRow.AddCell(.Sku.Id)

                url = .Sku.GetUrl(oDomain.DefaultLang,, True)
                If oDelivery.Cod = DTOPurchaseOrder.Codis.proveidor Then
                    oRow.AddCell(.Sku.RefProveidor, url)
                    oRow.AddCell(.Sku.NomProveidor, url)
                Else
                    oRow.AddCell(.Sku.NomLlarg.Esp, url)
                End If
                oRow.AddCell(.Qty)
                If valorat Then
                    oRow.AddCellAmt(.Price)
                    oRow.AddCell(.Dto)
                    oRow.AddFormula("RC[-3]*RC[-2]*(100-RC[-1])/100")
                    oRow.AddCell(DTOProductSku.InnerPackOrInherited(.Sku))
                    oRow.AddCell(DTOProductSku.DimensionLOrInherited(.Sku))
                    oRow.AddCell(DTOProductSku.DimensionWOrInherited(.Sku))
                    oRow.AddCell(DTOProductSku.DimensionHOrInherited(.Sku))
                    oRow.AddCell(DTOProductSku.volumeM3OrInherited(.Sku))
                    oRow.AddFormula("RC[-5]*RC[-1]")
                    oRow.AddCell(DTOProductSku.weightKgOrInherited(.Sku))
                    oRow.AddFormula("RC[-7]*RC[-1]")
                Else
                    oRow.AddCell(DTOProductSku.InnerPackOrInherited(.Sku))
                    oRow.AddCell(DTOProductSku.DimensionLOrInherited(.Sku))
                    oRow.AddCell(DTOProductSku.DimensionWOrInherited(.Sku))
                    oRow.AddCell(DTOProductSku.DimensionHOrInherited(.Sku))
                    oRow.AddCell(DTOProductSku.volumeM3OrInherited(.Sku))
                    oRow.AddFormula("RC[-2]*RC[-1]")
                    oRow.AddCell(DTOProductSku.weightKgOrInherited(.Sku))
                    oRow.AddFormula("RC[-4]*RC[-1]")
                End If
                oRow.AddCell(.Sku.MadeIn.ISO)
                If .Sku.CodiMercancia IsNot Nothing Then
                    oRow.AddCell(.Sku.CodiMercancia.Id)
                End If
            End With
        Next
        Return retval
    End Function

    Shared Function FullNameAddress(oDelivery As DTODelivery) As String
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine(DeliveryNom(oDelivery))
        sb.AppendLine(DeliveryAdr(oDelivery))
        sb.AppendLine(DTOZip.FullNom(oDelivery.Address.Zip))
        Dim retval As String = sb.ToString.Replace(vbCrLf, "<br/>")
        Return retval
    End Function

    Shared Function TotalsText(oDelivery As DTODelivery,
                               oItems As List(Of DTODeliveryItem),
                               oExportCod As DTOInvoice.ExportCods,
                               oCcx As DTOCustomer,
                               oLang As DTOLang) As String
        Dim sb As New System.Text.StringBuilder

        If oDelivery.baseImponible.isZero Then
            sb.Append(oLang.Tradueix("(sin cargo)", "(sense càrrec)", " (free of charge)"))
        Else
            Dim oTaxes = DTOTax.closest(oDelivery.Fch)
            Dim Iva As Boolean = (oExportCod = DTOInvoice.ExportCods.nacional)
            Dim Req As Boolean
            If oDelivery.Cod = DTOPurchaseOrder.Codis.client Then
                Req = oCcx.Req
            End If
            Dim oTotal = oDelivery.baseImponible.Clone
            sb.Append(DTOAmt.CurFormatted(oTotal))
            Select Case oExportCod
                Case DTOInvoice.ExportCods.nacional
                    Dim oQuotas = DTODelivery.getIvaBaseQuotas(oItems, oTaxes, Iva, Req)
                    For Each oQuota In oQuotas
                        Select Case oQuota.tax.codi
                            Case DTOTax.Codis.iva_Standard, DTOTax.Codis.iva_Reduit, DTOTax.Codis.iva_SuperReduit
                                sb.AppendFormat(" +  IVA {0}%  {1}", oQuota.tax.tipus.ToString, oQuota.quota.Formatted)
                                oTotal.Add(oQuota.quota)
                            Case DTOTax.Codis.recarrec_Equivalencia_Standard, DTOTax.Codis.recarrec_Equivalencia_Reduit, DTOTax.Codis.recarrec_Equivalencia_SuperReduit
                                sb.AppendFormat(" +  rec.{0}%  {1}", oQuota.tax.tipus.ToString, oQuota.quota.Formatted)
                                oTotal.Add(oQuota.quota)
                        End Select
                        'sb.Append(" total " & oTotal.CurFormatted)
                    Next
                    sb.Append("  total " & DTOAmt.CurFormatted(oTotal))
                Case DTOInvoice.ExportCods.intracomunitari, DTOInvoice.ExportCods.extracomunitari
                    sb.Append(oLang.Tradueix("  (exento de IVA)", "  (exempt de IVA)", "  (VAT n/a)"))
            End Select
        End If


        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function TotalsText(exs As List(Of Exception), oDelivery As DTODelivery) As String
        Dim oLang As DTOLang = oDelivery.Contact.Lang
        Dim sb As New System.Text.StringBuilder

        If oDelivery.baseImponible.isZero Then
            sb.Append(oLang.Tradueix("(sin cargo)", "(sense càrrec)", " (free of charge)"))
        Else
            Dim oTaxes = DTOTax.closest(oDelivery.Fch)
            Dim Iva As Boolean = (oDelivery.ExportCod = DTOInvoice.ExportCods.nacional)
            Dim Req As Boolean
            If oDelivery.Cod = DTOPurchaseOrder.Codis.client Then
                Req = FEB2.Customer.CcxOrMe(exs, oDelivery.Customer).Req
            End If
            Dim oTotal = oDelivery.baseImponible.Clone
            sb.Append(DTOAmt.CurFormatted(oTotal))
            Select Case oDelivery.ExportCod
                Case DTOInvoice.ExportCods.nacional
                    Dim oQuotas = DTODelivery.getIvaBaseQuotas(oDelivery.Items, oTaxes, Iva, Req)
                    For Each oQuota In oQuotas
                        Select Case oQuota.tax.codi
                            Case DTOTax.Codis.iva_Standard, DTOTax.Codis.iva_Reduit, DTOTax.Codis.iva_SuperReduit
                                sb.AppendFormat(" +  IVA {0}%  {1}", oQuota.tax.tipus.ToString, oQuota.quota.Formatted)
                                oTotal.Add(oQuota.quota)
                            Case DTOTax.Codis.recarrec_Equivalencia_Standard, DTOTax.Codis.recarrec_Equivalencia_Reduit, DTOTax.Codis.recarrec_Equivalencia_SuperReduit
                                sb.AppendFormat(" +  rec.{0}%  {1}", oQuota.tax.tipus.ToString, oQuota.quota.Formatted)
                                oTotal.Add(oQuota.quota)
                        End Select
                        'sb.Append(" total " & oTotal.CurFormatted)
                    Next
                    sb.Append("  total " & DTOAmt.CurFormatted(oTotal))
                Case DTOInvoice.ExportCods.intracomunitari, DTOInvoice.ExportCods.extracomunitari
                    sb.Append(oLang.Tradueix("  (exento de IVA)", "  (exempt de IVA)", "  (VAT n/a)"))
            End Select
        End If


        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function TotalLiquid(exs As List(Of Exception), oDelivery As DTODelivery) As DTOAmt
        Dim retval = DTOAmt.Empty
        Dim oTaxes = DTOTax.closest(oDelivery.Fch)
        Dim Iva As Boolean
        Dim Req As Boolean

        Select Case oDelivery.Cod
            Case DTOPurchaseOrder.Codis.proveidor
                Dim oProveidor As DTOProveidor = oDelivery.Proveidor
                Dim oExportCod = DTOAddress.ExportCod(oProveidor.Address)
                Iva = (oExportCod = DTOInvoice.ExportCods.nacional)
                Req = False
            Case Else
                Dim oCcx = FEB2.Customer.CcxOrMe(exs, oDelivery.Customer)
                Iva = oCcx.Iva
                Req = oCcx.Req
        End Select

        Dim oBaseImponible = DTOAmt.Empty
        For Each item In oDelivery.Items
            oBaseImponible.Add(item.Import)
        Next

        retval = oBaseImponible.Clone
        Select Case oDelivery.ExportCod
            Case DTOInvoice.ExportCods.nacional
                Dim oQuotas = DTODelivery.getIvaBaseQuotas(oDelivery.Items, oTaxes, Iva, Req)
                For Each oQuota In oQuotas
                    Select Case oQuota.tax.codi
                        Case DTOTax.Codis.iva_Standard, DTOTax.Codis.iva_Reduit, DTOTax.Codis.iva_SuperReduit
                            retval.Add(oQuota.quota)
                        Case DTOTax.Codis.recarrec_Equivalencia_Standard, DTOTax.Codis.recarrec_Equivalencia_Reduit, DTOTax.Codis.recarrec_Equivalencia_SuperReduit
                            retval.Add(oQuota.quota)
                    End Select
                Next
        End Select

        Return retval
    End Function

    Shared Function PickUpFchFrom(oDelivery As DTODelivery) As DateTimeOffset
        'Ha passat en transmisió?
        Dim exs As New List(Of Exception)
        Dim retval As DateTimeOffset

        If FEB2.Delivery.Load(oDelivery, exs) Then

            Dim DtFchTransm As DateTimeOffset
            If oDelivery.Transmisio Is Nothing Then
                Dim oTask = FEB2.Task.FindSync(DTOTask.Cods.VivaceTransmisio, exs)
                FEB2.Task.Load(oTask, exs)
                DtFchTransm = oTask.NextRun
            Else
                DtFchTransm = oDelivery.Transmisio.Fch
            End If

            Select Case DtFchTransm.DayOfWeek
                Case DayOfWeek.Thursday, DayOfWeek.Friday
                    retval = DtFchTransm.AddDays(4)
                Case Else
                    retval = DtFchTransm.AddDays(2)
            End Select
        End If
        Return retval
    End Function

    Shared Function UrlAlbSeguiment(exs As List(Of Exception), oDelivery As DTODelivery) As String
        Dim retval = DTO.Integracions.Vivace.Vivace.TrackingUrl(oDelivery)
        If oDelivery.Tracking.isNotEmpty() AndAlso oDelivery.Transportista IsNot Nothing Then
            Select Case oDelivery.Transportista.Guid
                Case DTOTransportista.Wellknown(DTOTransportista.Wellknowns.souto).Guid
                    Dim sb As New System.Text.StringBuilder
                    sb.Append("https://77.226.243.238/kiosco/Loginauto.aspx")
                    sb.Append("?Username=208081339")
                    sb.Append("&password=208081339")
                    sb.Append("&cli=208081339")
                    sb.Append("&alb=" & TextHelper.VbFormat(oDelivery.Fch.Year, "0000") & TextHelper.VbFormat(oDelivery.Id, "000000"))
                    retval = sb.ToString
                Case DTOTransportista.Wellknown(DTOTransportista.Wellknowns.tnt).Guid
                    'Dim sDelivery As String = String.Format("{0:0000}{1:000000}", oDelivery.Fch.Year, oDelivery.Id)
                    'retval = String.Format("https://www.tnt.com/express/es_es/site/home/aplicaciones/tracking.html?respCountry=es&respLang=es&navigation=1&page=1&sourceID=1&sourceCountry=ww&plazaKey=&refs=&requesttype=GEN&searchType=REF&cons={0}", sDelivery)
                    retval = String.Format("https://www.tnt.com/express/es_es/site/herramientas-envio/seguimiento.html?utm_redirect=legacy_webtracker_subdomain_track&requestType=GEN&searchType=CON&respLang=es&respCountry=ES&sourceID=1&sourceCountry=ww&cons={0}", oDelivery.Tracking)
                Case DTOTransportista.Wellknown(DTOTransportista.Wellknowns.txt).Guid
                    retval = String.Format("http://tracking.txt.es/?EXPED=@64072@zdq4gd9o6dgp8qx@E@{0}@{1}@", oDelivery.Tracking, oDelivery.Fch.Year)
                Case Else
            End Select
        End If
        Return retval
    End Function

    Shared Function Abonar(exs As List(Of Exception), oDelivery As DTODelivery, oUser As DTOUser, oMgz As DTOMgz) As DTODelivery
        Dim retval As DTODelivery = Nothing
        If FEB2.Delivery.Load(oDelivery, exs) Then
            retval = FEB2.Delivery.Factory(exs, oDelivery.Customer, oUser, oMgz)
            With retval
                .Address = oDelivery.Address
                .Tel = oDelivery.Tel
                .Platform = oDelivery.Platform
                .Cod = oDelivery.Cod
                .Mgz = oDelivery.Mgz
                .Nom = oDelivery.Nom
                .Valorado = oDelivery.Valorado
                .RetencioCod = DTODelivery.RetencioCods.free
                .Facturable = True
                .Items = New List(Of DTODeliveryItem)
                For Each item As DTODeliveryItem In oDelivery.Items
                    item.Delivery = retval
                    item.Qty = -item.Qty
                    .Items.Add(item)
                Next
            End With
        End If
        Return retval
    End Function

    Shared Async Function CheckValidationErrors(exs As List(Of Exception), oEmp As DTOEmp, oDelivery As DTODelivery) As Task(Of Boolean)

        If oDelivery.Cod = DTOPurchaseOrder.Codis.client Then

            If oDelivery.IsNew() Then

                Dim oCcx = FEB2.Customer.CcxOrMe(exs, oDelivery.Customer)
                Dim oLiquidAlbara As DTOAmt = FEB2.Delivery.TotalLiquid(exs, oDelivery)

                'Check advertencies albarans
                If oCcx.WarnAlbs > "" Then exs.Add(New Exception("Hi han advertencies de albarans no resoltes"))
                If oDelivery.Customer.WarnAlbs > "" And oDelivery.Customer.WarnAlbs <> oCcx.WarnAlbs Then
                    exs.Add(New Exception("Hi han advertencies de albarans no resoltes"))
                End If

                'Check ServirTotJunt
                'If WarnServirTotJunt() Then exs.Add(New Exception("S'está intentant servir parcialment una comanda registrada per servirla tota junta."))

                'Check NoServirAbansDe
                Dim WarnNoServirAbansDe = oDelivery.Items.Any(Function(x) x.PurchaseOrderItem.PurchaseOrder.FchDeliveryMin > oDelivery.Fch)
                If WarnNoServirAbansDe Then exs.Add(New Exception("S'está intentant servir comandes abans de la data convinguda."))

                'Check Credit
                If oDelivery.CashCod = DTOCustomer.CashCodes.credit Then
                    Dim oCreditDisponible = Await FEB2.Risc.CreditDisponible(oCcx, exs)
                    If oLiquidAlbara.IsPositive And oLiquidAlbara.IsGreaterThan(oCreditDisponible) Then
                        exs.Add(New Exception("Fora de crédit (credit disponible " & DTOAmt.CurFormatted(oCreditDisponible) & ")"))
                    End If
                End If

                'Check reposicions pendents
                Dim PendentsDePagament = Await FEB2.Pnds.All(exs, oEmp, oCcx, DTOPnd.Codis.Deutor, onlyPendents:=True)
                PendentsDePagament = PendentsDePagament.Where(Function(x) x.Vto < DateTime.Today And (x.Cfp = DTOPnd.FormasDePagament.reposicioFons Or x.Cfp = DTOPnd.FormasDePagament.transferencia)).ToList
                Dim DblVençut As Decimal = PendentsDePagament.Sum(Function(x) x.Amt.Eur)
                If DblVençut > 0 Then exs.Add(New Exception("hi han factures atrassades pendents de pagament"))

                'Check totes les comandes han de tenir la mateixa forma de pagament

                'Check transport
                If oDelivery.Items.All(Function(x) x.Sku.NoStk) Then
                    'passa de verificar ports si no estem enviant mercancia
                ElseIf oDelivery.PortsCod = DTOCustomer.PortsCodes.pagats And oLiquidAlbara.IsPositive Then
                    Dim BlNoTransportista As Boolean = (oDelivery.Transportista Is Nothing)
                    If BlNoTransportista Then exs.Add(New Exception("No s'ha trobat transportista per aquesta zona"))

                    'Check Ports
                    If oLiquidAlbara.IsPositive Then
                        If Not CheckPorts(oDelivery) Then exs.Add(New Exception("No arriba per ports!"))
                    End If
                End If

                'Check etiquetes transport avisar quan es junten varies comandes en un sol albarà
                Dim oPdcs = oDelivery.Items.GroupBy(Function(x) x.PurchaseOrderItem.PurchaseOrder.Guid).Select(Function(y) y.First).Select(Function(z) z.PurchaseOrderItem.PurchaseOrder).ToList
                If oPdcs.Any(Function(x) x.EtiquetesTransport IsNot Nothing) Then
                    If oPdcs.Count > 1 Then exs.Add(New Exception("No hauriem de juntar comandes que porten diferents etiquetes de transport. L'albarà sortirà sense etiquetes per precaució"))
                End If
            Else
                'Check transmisió
                If oDelivery.Transmisio IsNot Nothing Then
                    exs.Add(New Exception("Aquest albará ja ha estat transmés al magatzem"))
                End If

                If oDelivery.Invoice IsNot Nothing Then
                    exs.Add(New Exception("Aquest albará ja ha estat facturat"))
                End If

            End If
        End If

        Return (exs.Count = 0)
    End Function

    Shared Function CheckPorts(oDelivery As DTODelivery) As Boolean
        Dim retval As Boolean = True
        If oDelivery.PortsCod = DTOCustomer.PortsCodes.pagats Then
            Dim oCondicions = oDelivery.Customer.PortsCondicions
            retval = CheckPorts(oCondicions, oDelivery.Items, oDelivery.Dto, oDelivery.Dpp)
        End If
        Return retval
    End Function

    Shared Function CheckPorts(oPortsCondicio As DTOPortsCondicio, ByVal oItms As List(Of DTODeliveryItem), ByVal DcDto As Decimal, ByVal DcDpp As Decimal) As Boolean
        Dim exs As New List(Of Exception)
        FEB2.PortsCondicio.Load(exs, oPortsCondicio)
        Dim oUnitsMinPreu As DTOAmt = oPortsCondicio.UnitsMinPreu

        Dim oBaseImponible As DTOAmt = DTODeliveryItem.baseImponible(oItms)
        Dim retval As Boolean = oBaseImponible.IsGreaterOrEqualThan(oPortsCondicio.PdcMinVal)

        Return retval
    End Function

End Class

Public Class Deliveries
    Inherits _FeblBase

    Shared Async Function Years(exs As List(Of Exception), oEmp As DTOEmp, Optional oContact As DTOContact = Nothing) As Task(Of List(Of Integer))
        Return Await Api.Fetch(Of List(Of Integer))(exs, "Deliveries/years", oEmp.Id, OpcionalGuid(oContact))
    End Function

    Shared Async Function Headers(exs As List(Of Exception), oEmp As DTOEmp, year As Integer) As Task(Of List(Of DTODelivery))
        Return Await Api.Fetch(Of List(Of DTODelivery))(exs, "Deliveries/fromEmp", oEmp.Id, year)
    End Function

    Shared Async Function Headers(exs As List(Of Exception), oContact As DTOContact) As Task(Of List(Of DTODelivery))
        Return Await Api.Fetch(Of List(Of DTODelivery))(exs, "Deliveries/fromCustomer", oContact.Guid.ToString())
    End Function

    Shared Async Function Headers(exs As List(Of Exception),
                                  oEmp As DTOEmp,
                            Optional user As DTOUser = Nothing,
                            Optional contact As DTOContact = Nothing,
                            Optional group As Boolean = False,
                            Optional codis As List(Of DTOPurchaseOrder.Codis) = Nothing,
                            Optional year As Integer = 0,
                            Optional pendentsDeCobro As DTODelivery.RetencioCods = DTODelivery.RetencioCods.notSet,
                            Optional altresPorts As Boolean = False
                            ) As Task(Of List(Of DTODelivery))

        Dim retval = Await Api.Execute(Of List(Of DTOPurchaseOrder.Codis), List(Of DTODelivery))(codis, exs, "Deliveries/Headers", oEmp.Id, OpcionalGuid(user), OpcionalGuid(contact), OpcionalBool(group), year, pendentsDeCobro, OpcionalBool(altresPorts))
        'retval.ForEach(Function(x) x.Kg = 26)
        retval.[Select](Function(c)
                            c.IsNew = False
                            c.Contact.IsNew = False
                            c.Import = DTOAmt.Factory(c.Import.Eur)
                            c.ImportAdicional = DTOAmt.Factory(c.ImportAdicional.Eur)
                            Return c
                        End Function).ToList
        Return retval
    End Function

    Shared Async Function All(exs As List(Of Exception), oExercici As DTOExercici) As Task(Of List(Of DTODelivery))
        Return Await Api.Fetch(Of List(Of DTODelivery))(exs, "Deliveries", oExercici.Emp.Id, oExercici.Year)
    End Function

    Shared Async Function All(exs As List(Of Exception), oInvoice As DTOInvoice) As Task(Of List(Of DTODelivery))
        Return Await Api.Fetch(Of List(Of DTODelivery))(exs, "Deliveries/fromInvoice", oInvoice.Guid.ToString())
    End Function

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp, oArea As DTOArea) As Task(Of List(Of DTODelivery))
        Return Await Api.Fetch(Of List(Of DTODelivery))(exs, "Deliveries/fromArea", oEmp.Id, oArea.Guid.ToString())
    End Function

    Shared Async Function Last(exs As List(Of Exception), oContact As DTOContact) As Task(Of DTODelivery)
        Return Await Api.Fetch(Of DTODelivery)(exs, "Deliveries/last", oContact.Guid.ToString())
    End Function

    Shared Function CentrosSync(exs As List(Of Exception), oUser As DTOUser) As List(Of DTOCustomer)
        Return Api.FetchSync(Of List(Of DTOCustomer))(exs, "Deliveries/Centros", oUser.Guid.ToString())
    End Function

    Shared Async Function Update(exs As List(Of Exception), oDeliveries As List(Of DTODelivery)) As Task(Of List(Of DTODelivery))
        For Each oDelivery In oDeliveries
            oDelivery.Emp = oDelivery.Emp.trimmed
            'Dim pDelivery = Await FEB2.Delivery.Update(exs, oDelivery)
        Next
        Dim retval = Await Api.Execute(Of List(Of DTODelivery), List(Of DTODelivery))(oDeliveries, exs, "Deliveries/update")
        Return retval
    End Function


    Shared Async Function Pdf(exs As List(Of Exception), oDeliveries As List(Of DTODelivery), proforma As Boolean, Optional CodValorat As DTODelivery.CodsValorat = DTODelivery.CodsValorat.Inherit) As Task(Of Byte())
        If proforma Then
            Return Await Api.ExecuteBinary(Of List(Of DTODelivery))(oDeliveries, exs, "deliveries/pdf/proforma")
        Else
            Return Await Api.ExecuteBinary(Of List(Of DTODelivery))(oDeliveries, exs, "deliveries/pdf", CodValorat)
        End If
    End Function

    Shared Function Sum(oDeliveries As List(Of DTODelivery)) As DTOAmt
        Dim retval = DTOAmt.Empty
        For Each item As DTODelivery In oDeliveries
            retval.Add(item.Import)
        Next
        Return retval
    End Function

    Shared Async Function PendentsDeTransmetre(oMgz As DTOMgz, exs As List(Of Exception)) As Task(Of List(Of DTODelivery))
        Return Await Api.Fetch(Of List(Of DTODelivery))(exs, "deliveries/pendentsDeTransmetre", oMgz.Guid.ToString())
    End Function

    Shared Async Function PendentsDeFacturar(exs As List(Of Exception), oEmp As DTOEmp, Optional oCustomer As DTOCustomer = Nothing) As Task(Of List(Of DTODelivery))
        Return Await Api.Fetch(Of List(Of DTODelivery))(exs, "deliveries/pendentsDeFacturar", oEmp.Id, OpcionalGuid(oCustomer))
    End Function

    Shared Async Function IntrastatPending(oEmp As DTOEmp, oYearMonth As DTOYearMonth, exs As List(Of Exception)) As Task(Of List(Of DTODelivery))
        Return Await Api.Fetch(Of List(Of DTODelivery))(exs, "deliveries/IntrastatPending", oEmp.Id, oYearMonth.Year, oYearMonth.Month)
    End Function

    Shared Async Function Entrades(exs As List(Of Exception), oProveidor As DTOProveidor) As Task(Of List(Of DTODelivery))
        Return Await Api.Fetch(Of List(Of DTODelivery))(exs, "deliveries/Entrades", oProveidor.Guid.ToString())
    End Function






    Shared Async Function ExcelHistoricDetallat(oCustomer As DTOCustomer, exs As List(Of Exception)) As Task(Of ExcelHelper.Sheet)
        Return Await Api.Fetch(Of ExcelHelper.Sheet)(exs, "excel", CInt(ExcelHelper.Book.UrlCods.customerDeliveries), oCustomer.Guid.ToString())
    End Function

    Shared Function HaveSameCcx(exs As List(Of Exception), ByVal oDeliveries As List(Of DTODelivery)) As Boolean
        Dim retval As Boolean
        If oDeliveries.Count = 1 Then
            retval = True
        Else
            Dim oCcxs = oDeliveries.Select(Function(x) FEB2.Customer.CcxOrMe(exs, x.Customer))
            Dim oFirstCcx As DTOCustomer = oCcxs.First
            If oCcxs.All(Function(x) x.Equals(oFirstCcx)) Then
                If oFirstCcx.IsConsumer Then

                    Dim oConsumerTickets = oDeliveries.Select(Function(x) x.ConsumerTicket).ToList
                    If oConsumerTickets.Any(Function(x) x Is Nothing) Then
                        exs.Add(New Exception("Hi han albarans barrejats amb tickets de consumidor"))
                    Else
                        Dim sameNif = oConsumerTickets.All(Function(x) x.Nif = oConsumerTickets.First().Nif)
                        Dim sameNom = oConsumerTickets.All(Function(x) x.FullNom = oConsumerTickets.First().FullNom)
                        retval = sameNif And sameNom
                    End If
                Else
                    retval = True
                End If
            End If

        End If
        Return retval
    End Function





    Shared Async Function NumsToRecycle(exs As List(Of Exception), oEmp As DTOEmp, fch As Date) As Task(Of List(Of Integer))
        Return Await Api.Fetch(Of List(Of Integer))(exs, "deliveries/NumsToRecycle", oEmp.Id, FormatFch(fch))
    End Function

    Shared Async Function LlibreDeAlbarans(exs As List(Of Exception), oExercici As DTOExercici) As Task(Of ExcelHelper.Sheet)
        Dim sFilename As String = String.Format("{0}.{1} Llibre de Albarans.xlsx", oExercici.Emp.Org.PrimaryNifValue(), oExercici.Year)
        Dim retval As New ExcelHelper.Sheet(oExercici.Year, sFilename)

        With retval
            .AddColumn("Albará", ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("Data", ExcelHelper.Sheet.NumberFormats.DDMMYY)
            .AddColumn("Destinatari", ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn("Import", ExcelHelper.Sheet.NumberFormats.Euro)
            .AddColumn("Factura", ExcelHelper.Sheet.NumberFormats.W50)
        End With

        Dim items As List(Of DTODelivery) = Await FEB2.Deliveries.All(exs, oExercici)
        If exs.Count = 0 Then
            For Each item As DTODelivery In items
                Dim oRow As ExcelHelper.Row = retval.AddRow
                With item
                    oRow.AddCell(.Id, FEB2.Delivery.Url(item, True))
                    oRow.AddCell(.Fch)
                    oRow.AddCell(.Nom)
                    oRow.AddCellAmt(.Import)
                    If .Invoice Is Nothing Then
                        oRow.AddCell()
                    Else
                        oRow.AddCell(.Invoice.NumeroYSerie(), FEB2.Invoice.PdfUrl(.Invoice, True))
                    End If
                End With
            Next
        End If
        Return retval
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oDeliveries As List(Of DTODelivery), Optional ShowWarnings As Boolean = True) As Task(Of Boolean)
        Dim retval As Boolean = True
        For Each oDelivery As DTODelivery In oDeliveries
            Dim ex2 As New List(Of Exception)
            If Not Await FEB2.Delivery.Delete(ex2, oDelivery, ShowWarnings) Then
                FEB2.Delivery.Load(oDelivery, ex2)
                exs.Add(New Exception("error al eliminar alb." & oDelivery.Id))
                exs.AddRange(ex2)
                retval = False
            End If
        Next
        Return retval
    End Function

    Shared Async Function reZip(exs As List(Of Exception), oZipTo As DTOZip, oDeliveries As List(Of DTODelivery)) As Task(Of Integer)
        Dim oGuids = oDeliveries.Select(Function(x) x.Guid).ToList
        Dim retval = Await Api.Execute(Of List(Of Guid), Integer)(oGuids, exs, "Deliveries/rezip", oZipTo.Guid.ToString())
        Return retval
    End Function
End Class
