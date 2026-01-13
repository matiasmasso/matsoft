Imports Newtonsoft.Json.Linq

Public Class PurchaseOrder

#Region "Crud"
    Shared Function Find(oGuid As Guid, Optional oMgz As DTOMgz = Nothing) As DTOPurchaseOrder
        Dim retval As DTOPurchaseOrder = PurchaseOrderLoader.Find(oGuid, oMgz)
        Return retval
    End Function

    Shared Function FromNum(oEmp As DTOEmp, iYear As Integer, iNum As Integer) As DTOPurchaseOrder
        Dim retval As DTOPurchaseOrder = PurchaseOrderLoader.FromNum(oEmp, iYear, iNum)
        Return retval
    End Function

    Shared Function FromEdi(exs As List(Of Exception), oEmp As DTOEmp, src As String) As DTOPurchaseOrder
        Dim retval As DTOPurchaseOrder = Nothing
        Dim oEdiOrder = EdiHelper.EdiFile.Order.Factory(exs, src)
        If exs.Count = 0 Then
            Dim oUser = DTOUser.Wellknown(DTOUser.Wellknowns.info)
            Dim oBuyer = BEBL.Contact.FromGLN(New DTOEan(oEdiOrder.Buyer))
            If oBuyer Is Nothing Then
                exs.Add(New Exception("Comprador no identificat amb EAN " & oEdiOrder.Buyer))
            Else
                Dim oCustomer = DTOCustomer.FromContact(oBuyer)
                Dim oEans = oEdiOrder.Items.Select(Function(x) New DTOEan(x.Sku)).ToList()
                BEBL.Customer.Load(oCustomer)
                Dim oSkus = BEBL.ProductSkus.Search(oCustomer, oEans, oEmp.Mgz)
                retval = DTOPurchaseOrder.Factory(oCustomer, oUser, oEdiOrder.FchDoc, DTOPurchaseOrder.Sources.edi, oEdiOrder.DocNum)
                If Not String.IsNullOrEmpty(oEdiOrder.DeliverTo) Then retval.Platform = DTOCustomerPlatform.FromContact(BEBL.Contact.FromGLN(New DTOEan(oEdiOrder.DeliverTo)))
                For Each item In oEdiOrder.Items
                    Dim oEan = DTOEan.Factory(item.Sku)
                    Dim oSku = oSkus.FirstOrDefault(Function(x) x.Ean13.Equals(oEan))
                    Dim oPrice As DTOAmt = DTOAmt.Factory()
                    If item.GrossPrice = 0 Then
                        oPrice = DTOAmt.Factory(item.NetPrice)
                        retval.addItem(oSku, item.Qty, oPrice)
                    Else
                        oPrice = DTOAmt.Factory(item.GrossPrice)
                        retval.addItem(oSku, item.Qty, oPrice, item.Dto)
                    End If
                Next
            End If
        End If
        Return retval
    End Function

    Shared Function FromRff(oEmp As DTOEmp, Rff As String) As DTOPurchaseOrder
        Dim retval As DTOPurchaseOrder = Nothing
        If Rff.Length = 10 Then
            Dim iYear As Integer = Rff.Substring(0, 4)
            Dim iNum As Integer = Rff.Substring(4, 6)
            retval = PurchaseOrderLoader.FromNum(oEmp, iYear, iNum)
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oPurchaseOrder As DTOPurchaseOrder, oMgz As DTOMgz) As Boolean
        Dim retval As Boolean = PurchaseOrderLoader.Load(oPurchaseOrder, oMgz)
        Return retval
    End Function

    Shared Function FindWithDeliveries(oGuid As Guid) As DTOPurchaseOrder
        Return PurchaseOrderLoader.FindWithDeliveries(oGuid)
    End Function

    Shared Function FindDuplicate(oCustomer As DTOCustomer, DtFch As Date, sConcepte As String) As DTOPurchaseOrder
        Dim retval As DTOPurchaseOrder = PurchaseOrderLoader.FindDuplicate(oCustomer, DtFch, sConcepte)
        Return retval
    End Function

    Shared Function ResetPendingQty(value As DTOPurchaseOrder, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = PurchaseOrderLoader.ResetPendingQty(value, exs)
        BEBL.ServerCache.NotifyUpdate(Models.ServerCache.Tables.Stocks)
        Return retval
    End Function

    Shared Function Update(oPurchaseOrder As DTOPurchaseOrder, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = PurchaseOrderLoader.Update(oPurchaseOrder, exs)
        BEBL.ServerCache.NotifyUpdate(Models.ServerCache.Tables.Stocks)
        Return retval
    End Function

    Shared Function Delete(oPurchaseOrder As DTOPurchaseOrder, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = PurchaseOrderLoader.Delete(oPurchaseOrder, exs)
        BEBL.ServerCache.NotifyUpdate(Models.ServerCache.Tables.Stocks)
        Return retval
    End Function


    Shared Function CobraPerVisa(oLog As DTOTpvLog, exs As List(Of Exception)) As Boolean
        Dim oPurchaseOrder As DTOPurchaseOrder = oLog.Request
        PurchaseOrderLoader.Load(oPurchaseOrder)
        Dim retval As Boolean = PurchaseOrderLoader.CobraPerVisa(oLog, exs)
        Return retval
    End Function

    Shared Function RecalculaPendents(exs As List(Of Exception), oPurchaseOrder As DTOPurchaseOrder) As Integer
        Dim retval As Integer = PurchaseOrderLoader.RecalculaPendents(exs, oPurchaseOrder)
        BEBL.ServerCache.NotifyUpdate(Models.ServerCache.Tables.Stocks)
        Return retval
    End Function

    Shared Function RemovePromo(exs As List(Of Exception), oPurchaseOrder As DTOPurchaseOrder) As Boolean
        Return PurchaseOrderLoader.RemovePromo(exs, oPurchaseOrder)
    End Function

    Shared Function Contact(oPurchaseOrder As DTOPurchaseOrder) As DTOContact
        Dim retval As DTOContact = If(oPurchaseOrder.Contact, oPurchaseOrder.Contact)
        Return retval
    End Function

    Shared Function SearchConcepte(sKey As String) As DTOPurchaseOrderConcepte
        Dim retval As DTOPurchaseOrderConcepte = PurchaseOrderLoader.SearchConcepte(sKey)
        Return retval
    End Function

#End Region

    Shared Function Factory(oCustomer As DTOCustomer, oUser As DTOUser, Optional DtFch As Date = Nothing, Optional oSource As DTOPurchaseOrder.Sources = DTOPurchaseOrder.Sources.no_Especificado, Optional sConcept As String = "") As DTOPurchaseOrder
        If DtFch = Nothing Then DtFch = DTO.GlobalVariables.Today()
        BEBL.Customer.Load(oCustomer)
        Dim retval As New DTOPurchaseOrder
        With retval
            .Emp = oCustomer.Emp
            .Cod = DTOPurchaseOrder.Codis.client
            .Fch = DtFch
            .Source = oSource
            If oCustomer.OrdersToCentral AndAlso oCustomer.Ccx IsNot Nothing Then
                .Customer = oCustomer.Ccx
            Else
                .Customer = oCustomer
            End If
            .UsrLog = DTOUsrLog.Factory(oUser)
            .Concept = sConcept
            .Cur = DTOCur.Eur
            .Items = New List(Of DTOPurchaseOrderItem)
        End With
        Return retval
    End Function

    Shared Function FromEdiversa(oEdiversaOrder As DTOEdiversaOrder, oUser As DTOUser) As DTOPurchaseOrder
        Dim firstBundle As Boolean = True
        Dim oCcx As DTOCustomer = Nothing
        Dim oCustomCosts As New List(Of DTOPricelistItemCustomer)
        Dim oTarifaDtos As New List(Of DTOCustomerTarifaDto)
        Dim oCliProductDtos As New List(Of DTOCliProductDto)
        firstBundle = False

        Dim oComprador As New DTOCustomer(oEdiversaOrder.Comprador.Guid)
        oComprador.FullNom = oEdiversaOrder.Comprador.FullNom
        Dim retval As DTOPurchaseOrder = PurchaseOrder.Factory(oComprador, oUser, oEdiversaOrder.FchDoc, DTOPurchaseOrder.Sources.edi, oEdiversaOrder.DocNum)
        With retval
            .FchDeliveryMin = oEdiversaOrder.FchDeliveryMin
            .FacturarA = oEdiversaOrder.FacturarA
            If oEdiversaOrder.NADMS IsNot Nothing Then
                .NADMS = oEdiversaOrder.NADMS.Value
            End If
            .Obs = oEdiversaOrder.Obs
            For Each oEdiversaItem As DTOEdiversaOrderItem In oEdiversaOrder.Items
                If oEdiversaItem.SkipItemUser Is Nothing Then
                    Dim item As New DTOPurchaseOrderItem()
                    With item
                        .PurchaseOrder = retval
                        .CustomLin = oEdiversaItem.Lin
                        .Qty = oEdiversaItem.Qty
                        .Pending = oEdiversaItem.Qty
                        .Sku = oEdiversaItem.Sku
                        .RepCom = RepCom.GetRepCom(oUser.Emp, oComprador, .Sku, .PurchaseOrder.Fch)
                        If oEdiversaItem.Preu Is Nothing Then
                            'preu net
                            .Price = oEdiversaItem.PreuNet
                        Else
                            'preu brut i descompte
                            .Price = oEdiversaItem.Preu
                            .Dto = oEdiversaItem.Dto
                        End If
                    End With
                    retval.Items.Add(item)


                    If item.Sku.IsBundle Then
                        If firstBundle Then
                            oCcx = oComprador.CcxOrMe()
                            oCustomCosts = BEBL.PriceListItemsCustomer.Active(oCcx, DTO.GlobalVariables.Today())
                            oTarifaDtos = BEBL.CustomerTarifaDtos.Active(oCcx)
                            oCliProductDtos = BEBL.CliProductDtos.All(oCcx)
                            firstBundle = False
                        End If

                        item.Bundle = BEBL.PurchaseOrderItem.BundleItemsFactory(item, oUser.Emp, oCustomCosts, oTarifaDtos, oCliProductDtos)
                    End If


                    'If item.Sku.isBundle Then
                    ' item.Sku.bundleSkus = BEBL.ProductSku.BundleSkus(item.Sku)
                    'line.bundle = New List(Of DTOPurchaseOrderItem)
                    'For Each oSkuBundle As DTOSkuBundle In item.Sku.bundleSkus
                    'Dim DcDto As Decimal = 0
                    'Dim oPrice As DTOAmt = Nothing '= DTOProductSku.GetCustomerCost(oSkuBundle.Sku, _CustomCosts, _TarifaDtos)
                    'If oPrice Is Nothing Then
                    'oPrice = DTOAmt.factory
                    'Else
                    '   'Dim oDto As DTOCliProductDto = oSku.CliProductDto(_CliProductDtos)
                    'If oDto IsNot Nothing Then DcDto = oDto.Dto
                    'End If
                    '
                    'Dim oBundleChild = DTOPurchaseOrderItem.Factory(retval, oSkuBundle.Sku, item.Qty, oPrice, DcDto)
                    'oBundleChild.repCom = line.repCom

                    'line.bundle.Add(oBundleChild)
                    ' Next

                    'End If

                End If
            Next
            .UsrLog = DTOUsrLog.Factory(oUser)
        End With

        Dim oFile As DTOEdiversaFile = EdiversaFileLoader.Find(oEdiversaOrder.Guid)
        oFile.LoadSegments()
        Dim oInterlocutor As DTOEdiversaFile.Interlocutors = EdiversaFile.Interlocutor(oFile)
        If oInterlocutor = DTOEdiversaFile.Interlocutors.ElCorteIngles Then
            retval.Concept = ElCorteIngles.GetOrderConceptFromEdiversa(oEdiversaOrder)
        End If
        If oEdiversaOrder.ReceptorMercancia IsNot Nothing Then
            retval.Platform = New DTOCustomerPlatform(oEdiversaOrder.ReceptorMercancia.Guid)
            retval.Platform.FullNom = oEdiversaOrder.ReceptorMercancia.FullNom
        End If
        oEdiversaOrder.Result = retval
        Return retval
    End Function


    Shared Function SetIncentius(exs As List(Of Exception), oUser As DTOUser, ByRef oPurchaseOrder As DTOPurchaseOrder) As Boolean
        Dim retval As Boolean
        'No carreguis PurchaseOrder des de la base de dades perque ho fa servir al afegir linia de comanda on the fly

        Dim oCcx = BEBL.Customer.CcxOrMe(oPurchaseOrder.Contact)
        If Not oCcx.NoIncentius Then
            Dim oIncentius = BEBL.Incentius.All(oUser, False)
            oIncentius = oIncentius.FindAll(Function(x) x.OnlyInStk = False).ToList
            Dim oOrderIncentius As New List(Of DTOIncentiu)
            Dim oOrderIncentiu As New DTOIncentiu
            Dim oItm As DTOPurchaseOrderItem
            Dim DcDto As Decimal

            'assigna les quantitats de cada oferta
            For Each oItm In oPurchaseOrder.Items
                oItm.Incentius = DTOProductSku.incentius(oItm.Sku, oIncentius)
                If oItm.Incentius.Count > 0 Then
                    For Each oItmIncentiu As DTOIncentiu In oItm.Incentius
                        Dim BlFoundInOrderIncentius As Boolean = False
                        For Each oOrderIncentiu In oOrderIncentius
                            If oOrderIncentiu.Equals(oItmIncentiu) Then
                                BlFoundInOrderIncentius = True
                                Exit For
                            End If
                        Next

                        If Not BlFoundInOrderIncentius Then
                            oOrderIncentiu = oItmIncentiu
                            oOrderIncentius.Add(oOrderIncentiu)
                        End If

                        oOrderIncentiu.Unitats += oItm.Qty
                    Next
                End If
            Next


            For Each oItm In oPurchaseOrder.Items

                'assigna la quantitat mès alta de les ofertes en que participa cada linia
                Dim iQty As Integer = 0
                For Each oItmIncentiu As DTOIncentiu In oItm.Incentius
                    For Each oOrderIncentiu In oOrderIncentius
                        If oOrderIncentiu.Equals(oItmIncentiu) Then
                            If oOrderIncentiu.Unitats > iQty Then
                                iQty = oOrderIncentiu.Unitats
                            End If
                            Exit For
                        End If
                    Next
                Next

                'assigna el descompte de la oferta mes favorable que li toca a cada linea
                Dim DcOrderDto As Decimal = oItm.Dto
                For Each oItmIncentiu As DTOIncentiu In oItm.Incentius
                    DcDto = DTOIncentiu.GetDto(oItmIncentiu, iQty)
                    If DcDto > DcOrderDto Then
                        DcOrderDto = DcDto
                        retval = True
                    End If
                Next
                oItm.Dto = DcOrderDto
            Next

        End If
        Return retval
    End Function

    Shared Async Function MailConfirmation(exs As List(Of Exception), oEmp As DTOEmp, oPurchaseOrder As DTOPurchaseOrder, sRecipients As List(Of String)) As Task(Of Boolean)
        Dim oMailMessage = DTOPurchaseOrder.confirmationMailMessage(oEmp, oPurchaseOrder, sRecipients, oPurchaseOrder.Customer.Lang)
        Dim value As New DTOTaskResult()
        Dim retval = Await BEBL.MailMessageHelper.Send(oEmp, oMailMessage, exs)
        Return retval
    End Function

    Public Shared Async Function SubmitProcess(purchaseOrder As DTOCompactPO, Optional pruebas As Boolean = False) As Threading.Tasks.Task(Of DTOCompactPO)
        Dim exs As New List(Of Exception)
        Dim oOrder As DTOPurchaseOrder = Nothing
        Dim oUser As DTOUser = Nothing
        Dim src = JsonHelper.Serialize(purchaseOrder, exs)
        Try
            oUser = BEBL.User.Find(purchaseOrder.User.Guid)
            If oUser Is Nothing Then
                exs.Add(New Exception("user unknown"))
            Else
                purchaseOrder.User.Nom = oUser.EmailAddress
                BEBL.Emp.Load(oUser.Emp)
                'Dim oStocks = BEBL.Mgz.Stocks(oUser.Emp.Mgz)
                Dim oCustomers As List(Of DTOCustomer) = BEBL.User.GetCustomers(oUser)
                Dim oCustomer As DTOCustomer = oCustomers.FirstOrDefault(Function(x) x.Guid.Equals(purchaseOrder.Customer.Guid))
                If oCustomer Is Nothing Then
                    exs.Add(New Exception(oUser.Lang.Tradueix("Usuario no autorizado para este cliente", "Usuari no autoritzat per aquest client", "User not authorized for this customer")))
                Else
                    purchaseOrder.Customer.Nom = oCustomer.FullNom
                    Dim oRepProducts As List(Of DTORepProduct) = BEBL.RepProducts.All(oUser.Emp)
                    Dim oRepCliComs As List(Of DTORepCliCom) = BEBL.RepCliComs.All(oUser.Emp)
                    'Dim oTarifa = BEBL.CustomerTarifa.Load(oCustomer)
                    'Dim oPricelistItems As List(Of DTOPricelistItemCustomer) = oTarifa.Items
                    Dim oEans = purchaseOrder.Items.
                        Where(Function(x) x.Sku IsNot Nothing AndAlso Not String.IsNullOrEmpty(x.Sku.Ean)).
                        Select(Function(y) DTOEan.Factory(y.Sku.Ean)).
                        ToList()

                    Dim oSkus = BEBL.ProductSkus.Search(oCustomer, oEans, oUser.Emp.Mgz)

                    oOrder = DTOPurchaseOrder.Factory(oCustomer, oUser,, DTOPurchaseOrder.Sources.cliente_por_WebApi)
                    With oOrder
                        If Not String.IsNullOrEmpty(purchaseOrder.Nom) Then
                            .Concept = purchaseOrder.Nom
                        ElseIf Not String.IsNullOrEmpty(purchaseOrder.Obs) Then
                            .Concept = purchaseOrder.Obs
                        Else
                            .Concept = "via Api"
                        End If

                        Dim lin As Integer

                        For Each item In purchaseOrder.Items
                            lin += 1
                            Dim oItem As New DTOPurchaseOrderItem
                            oItem.Sku = DTOProductSku.Wellknown(DTOProductSku.Wellknowns.UnknownSku)

                            If item.Sku Is Nothing Then
                                oItem.ErrCod = DTOPurchaseOrderItem.ErrCods.UnknownProduct
                                oItem.ErrDsc = oUser.Lang.Tradueix("Producto no especificado", "Producte no especificat", "Missing product")
                            ElseIf item.Sku.Ean Is Nothing Then
                                oItem.ErrCod = DTOPurchaseOrderItem.ErrCods.UnknownProduct
                                oItem.ErrDsc = oUser.Lang.Tradueix("Falta código Ean", "Falta codi Ean", "Missing Ean code")
                            ElseIf String.IsNullOrEmpty(item.Sku.Ean) Then
                                oItem.ErrCod = DTOPurchaseOrderItem.ErrCods.UnknownProduct
                                oItem.ErrDsc = oUser.Lang.Tradueix("Código Ean vacío", "Codi Ean buit", "Empty Ean code")
                            Else
                                Dim sEan = item.Sku.Ean
                                If oSkus.Any(Function(x) x.Ean13.Value = sEan) Then
                                    oItem.Sku = oSkus.FirstOrDefault(Function(x) x.Ean13.Value = sEan)
                                    Select Case oItem.Sku.CodExclusio
                                        Case DTOProductSku.CodisExclusio.Inclos
                                        Case DTOProductSku.CodisExclusio.Obsolet, DTOProductSku.CodisExclusio.OutOfCatalog
                                            If oItem.Sku.stockAvailable = 0 Then
                                                oItem.ErrCod = DTOPurchaseOrderItem.ErrCods.Obsolet
                                                oItem.ErrDsc = oUser.Lang.Tradueix("Producto obsoleto", "Producte obsolet", "outdated product")
                                            End If
                                        Case DTOProductSku.CodisExclusio.Canal
                                            oItem.ErrCod = DTOPurchaseOrderItem.ErrCods.ChannelExcluded
                                            oItem.ErrDsc = oUser.Lang.Tradueix("Distribución no autorizada en este canal", "Distribució no autoritzada en aquest canal", "Unallowed distribution channel")
                                        Case DTOProductSku.CodisExclusio.Exclusives
                                            oItem.ErrCod = DTOPurchaseOrderItem.ErrCods.CustomerExcluded
                                            oItem.ErrDsc = oUser.Lang.Tradueix("Producto de distribución selectiva", "Producte de distribució selectiva", "Restricted distribution")
                                        Case DTOProductSku.CodisExclusio.PremiumLine
                                            oItem.ErrCod = DTOPurchaseOrderItem.ErrCods.PremiumLineExcluded
                                            oItem.ErrDsc = oUser.Lang.Tradueix("Producto exclusivo de distribución con contrato", "Producte exclusiu de distribució sota contracte", "Distribution contract required for this product")
                                    End Select

                                Else
                                    oItem.ErrCod = DTOPurchaseOrderItem.ErrCods.UnknownProduct
                                    oItem.ErrDsc = String.Format(oUser.Lang.Tradueix("Código Ean {0} desconocido", "Codi Ean {0} desconegut", "Ean code {0} not found"), sEan)
                                End If

                                With item
                                    With .Sku
                                        .Guid = oItem.Sku.Guid
                                        .Nom = oItem.Sku.RefYNomLlarg.Esp
                                        .Moq = oItem.Sku.innerPackOrInherited()
                                        .Stock = oItem.Sku.stockAvailable()
                                    End With
                                    If oItem.Sku.Price IsNot Nothing Then .Price = oItem.Sku.Price.Eur
                                End With

                                With oItem
                                    .PurchaseOrder = oOrder
                                    .Qty = item.Qty
                                    .Pending = item.Qty
                                    .Price = DTOAmt.Factory(item.Price)
                                    If .ErrCod = DTOPurchaseOrderItem.ErrCods.Success Then
                                        .RepCom = BEBL.PurchaseOrderItem.SuggestedRepCom(oUser, oItem, oRepProducts, oRepCliComs, exs)
                                    End If
                                End With
                                .Items.Add(oItem)
                            End If
                        Next

                        With purchaseOrder
                            .Fch = oOrder.Fch
                            .Eur = oOrder.SumaDeImportes().Eur

                            If oOrder IsNot Nothing Then
                                If pruebas Then
                                    .Guid = oOrder.Guid
                                    .Id = 11111
                                Else
                                    oOrder.DocFile = BEBL.DocFile.LoadFromString(src)
                                    If BEBL.PurchaseOrder.Update(oOrder, exs) Then
                                        .Guid = oOrder.Guid
                                        .Id = oOrder.Num
                                    Else
                                        .ValidationErrors.Add("Error SYSERR_56 de sistema. Por favor reportadlo a oficinas.")
                                    End If
                                End If
                            End If
                        End With

                    End With
                End If
            End If

        Catch ex As Exception
            exs.Add(ex)
            purchaseOrder.ValidationErrors.Add("Error SYSERR_57 de sistema. Por favor reportadlo a oficinas.")
            Dim oWinbug = DTOWinBug.Factory("api/purchaseOrder exception: " & ex.Message)
            BEBL.WinBug.Update(oWinbug, exs)
        End Try

        If exs.Count = 0 Then
            Dim oMailMessage = DTOPurchaseOrder.mailMessageConfirmation(oOrder, , purchaseOrder.ValidationErrors)
            Await BEBL.MailMessageHelper.Send(oUser.Emp, oMailMessage, exs)
        End If

        Return purchaseOrder

    End Function

End Class


Public Class PurchaseOrders

    Shared Function Years(oEmp As DTOEmp, oCod As DTOPurchaseOrder.Codis, oContact As DTOContact, Optional IncludeGroupSalePoints As Boolean = False) As List(Of Integer)
        Return PurchaseOrdersLoader.Years(oCod, oContact, IncludeGroupSalePoints)
    End Function

    Shared Function All(oEmp As DTOEmp, oCod As DTOPurchaseOrder.Codis, Optional oContact As DTOContact = Nothing, Optional iYear As Integer = 0, Optional IncludeGroupSalePoints As Boolean = False) As List(Of DTOPurchaseOrder)
        Dim retval As List(Of DTOPurchaseOrder) = PurchaseOrdersLoader.All(oCod, oContact, iYear, IncludeGroupSalePoints)
        Return retval
    End Function

    Shared Function All(oIncentiu As DTOIncentiu, oUser As DTOUser) As List(Of DTOPurchaseOrder)
        Dim retval As List(Of DTOPurchaseOrder) = PurchaseOrdersLoader.All(oIncentiu, oUser)
        Return retval
    End Function

    Shared Function All(oUser As DTOUser) As List(Of DTOPurchaseOrder.HeaderModel)
        Dim retval = PurchaseOrdersLoader.All(oUser)
        Return retval
    End Function

    Shared Function FromIds(oEmp As DTOEmp, ids As List(Of String)) As List(Of DTOPurchaseOrder)
        Dim retval As New List(Of DTOPurchaseOrder)
        If ids.Count > 0 Then
            retval = PurchaseOrdersLoader.FromIds(oEmp, ids.ToHashSet())
        End If
        Return retval
    End Function




    Shared Function Headers(Optional oEmp As DTOEmp = Nothing,
                            Optional Cod As DTOPurchaseOrder.Codis = DTOPurchaseOrder.Codis.notSet,
                            Optional Contact As DTOContact = Nothing,
                            Optional Ccx As DTOCustomer = Nothing,
                            Optional Rep As DTORep = Nothing,
                            Optional Year As Integer = 0,
                            Optional FchCreatedFrom As Date = Nothing,
                            Optional FchCreatedTo As Date = Nothing) As JArray

        Return PurchaseOrdersLoader.Headers(oEmp, Cod, Contact, Ccx, Rep, Year, FchCreatedFrom, FchCreatedTo)
    End Function

    Shared Function Delete(exs As List(Of Exception), values As List(Of Guid)) As Boolean
        If PurchaseOrdersLoader.Delete(exs, values) Then
            BEBL.ServerCache.NotifyUpdate(Models.ServerCache.Tables.Stocks)
        End If
        Return exs.Count = 0
    End Function

    Shared Function Exists(oUser As DTOUser, DtFchFrom As Date, DtFchTo As Date) As Boolean
        Return PurchaseOrdersLoader.Exists(oUser, DtFchFrom, DtFchTo)
    End Function

    Shared Function LastOrdersEntered(oUser As DTOUser) As List(Of DTOPurchaseOrder)
        Return PurchaseOrdersLoader.LastOrdersEntered(oUser)
    End Function

    Shared Function Pending(oEmp As DTOEmp, Optional cod As DTOPurchaseOrder.Codis = DTOPurchaseOrder.Codis.notSet, Optional contact As DTOContact = Nothing, Optional oUser As DTOUser = Nothing) As List(Of DTOPurchaseOrder)
        Return PurchaseOrdersLoader.Pending(oEmp, cod, contact, oUser)
    End Function


    Shared Function PendingSkus(oCustomer As DTOCustomer) As List(Of DTOPurchaseOrderItem)
        Return PurchaseOrdersLoader.PendingSkus(oCustomer)
    End Function

    Shared Function PendingForPlatforms(oCustomer As DTOCustomer) As List(Of DTOPurchaseOrder)
        Return PurchaseOrdersLoader.PendingForPlatforms(oCustomer, Nothing)
    End Function

    Shared Function PendingForPlatforms(oHolding As DTOHolding) As List(Of DTOPurchaseOrder)
        Return PurchaseOrdersLoader.PendingForPlatforms(Nothing, oHolding)
    End Function

    Shared Function StocksAvailableForPlatforms(oEmp As DTOEmp, oCcx As DTOCustomer) As List(Of DTOStockAvailable)
        Return PurchaseOrdersLoader.StocksAvailableForPlatforms(oCcx, oEmp.Mgz, Nothing)
    End Function
    Shared Function StocksAvailableForPlatforms(oEmp As DTOEmp, oHolding As DTOHolding) As List(Of DTOStockAvailable)
        Return PurchaseOrdersLoader.StocksAvailableForPlatforms(Nothing, oEmp.Mgz, oHolding)
    End Function
End Class


