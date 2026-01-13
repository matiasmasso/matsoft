Public Class PurchaseOrderController
    Inherits _BaseController

    <HttpPost>
    <Route("api/contact/purchaseorders")>
    Public Function Fetch(contact As DUI.Guidnom) As List(Of DUI.PurchaseOrder)
        Dim oContact As New DTOContact(contact.Guid)
        Dim items As List(Of DTOPurchaseOrder) = BLLPurchaseOrders.Headers(DTOPurchaseOrder.Codis.Client, oContact)
        Dim retval As New List(Of DUI.PurchaseOrder)
        For Each item As DTOPurchaseOrder In items
            Dim dui As New DUI.PurchaseOrder
            With dui
                .Guid = item.Guid
                .Nom = item.Concept
                .Fch = item.Fch
                .Id = item.Num
                .Eur = item.SumaDeImports.Eur
                .Customer = New DUI.Guidnom
                .Customer.Guid = item.Customer.Guid
                .Customer.Nom = item.Customer.FullNom
                If item.DocFile IsNot Nothing Then
                    .FileUrl = BLLDocFile.DownloadUrl(item.DocFile, True)
                    .ThumbnailUrl = BLLDocFile.ThumbnailUrl(item.DocFile, True)
                End If
                If item.Incentiu IsNot Nothing Then
                    .Promo = New DUI.Promo
                    .Promo.Guid = item.Incentiu.Guid
                    .Promo.Nom = BLLIncentiu.Title(item.Incentiu, DTOLang.ESP)
                End If
            End With
            retval.Add(dui)
        Next
        Return retval
    End Function



    <HttpPost>
    <Route("api/purchaseorder")>
    Public Function Load(purchaseOrder As DUI.PurchaseOrder) As DUI.PurchaseOrder
        Dim retval As DUI.PurchaseOrder = Nothing
        Try


            Dim oOrder As DTOPurchaseOrder = BLLPurchaseOrder.Find(purchaseOrder.Guid)
            If oOrder Is Nothing Then
                BLL.BLLWinBug.Log("api/purchaseOrder order is nothing")
            Else
                retval = New DUI.PurchaseOrder
                With retval
                    .Guid = oOrder.Guid
                    .Nom = oOrder.Concept
                    .Id = oOrder.Num
                    .Eur = BLLPurchaseOrder.SumaDeImportes(oOrder).Eur
                    .Fch = oOrder.Fch
                    .Customer = New DUI.Guidnom
                    .Customer.Guid = oOrder.Customer.Guid
                    .Customer.Nom = oOrder.Customer.FullNom
                    .TotJunt = oOrder.TotJunt
                    If oOrder.Incentiu IsNot Nothing Then
                        .Promo = New DUI.Promo()
                        .Promo.Guid = oOrder.Incentiu.Guid
                        .Promo.Nom = BLLIncentiu.Title(oOrder.Incentiu, DTOLang.ESP)
                    End If

                    If .FchMin <> Nothing Then
                        .FchMin = oOrder.FchDelivery
                    End If

                    If oOrder.DocFile IsNot Nothing Then
                        .FileUrl = BLLDocFile.DownloadUrl(oOrder.DocFile, True)
                        .ThumbnailUrl = BLLDocFile.ThumbnailUrl(oOrder.DocFile, True)
                    End If
                    .items = New List(Of DUI.PurchaseOrderItem)
                End With

                For Each item As DTOPurchaseOrderItem In oOrder.Items
                    Dim dui As New DUI.PurchaseOrderItem
                    With dui
                        .Guid = item.Guid
                        .Qty = item.Qty
                        .Sku = New DUI.Sku
                        .Sku.Guid = item.Sku.Guid
                        .Sku.Nom = item.Sku.NomLlarg
                        .Eur = item.Price.Eur
                        .Dto = item.Dto
                    End With
                    retval.items.Add(dui)
                Next
            End If
        Catch ex As Exception
            BLL.BLLWinBug.Log("api/purchaseOrder exception: " & ex.Message)
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/purchaseorder/submit")>
    Public Function Submit(purchaseOrder As DUI.PurchaseOrder) As DUI.PurchaseOrder
        Dim retval As DUI.PurchaseOrder = SubmitProcess(purchaseOrder)
        Return retval
    End Function
    <HttpPost>
    <Route("api/purchaseorder/submit/pruebas")>
    Public Function SubmitPruebas(purchaseOrder As DUI.PurchaseOrder) As DUI.PurchaseOrder
        Dim retval As DUI.PurchaseOrder = SubmitProcess(purchaseOrder, pruebas:=True)
        Return retval
    End Function

    Private Function SubmitProcess(purchaseOrder As DUI.PurchaseOrder, Optional pruebas As Boolean = False) As DUI.PurchaseOrder
        Dim oOrder As DTOPurchaseOrder = Nothing
        Dim exs As New List(Of Exception)
        Try
            Dim oUser As DTOUser = BLLUser.Find(purchaseOrder.user.Guid)
            If oUser Is Nothing Then
                exs.Add(New Exception("user unknown"))
            Else
                Dim oCustomers As List(Of DTOCustomer) = BLLUser.GetCustomers(oUser)
                Dim oCustomer As DTOCustomer = oCustomers.FirstOrDefault(Function(x) x.Guid.Equals(purchaseOrder.Customer.Guid))
                If oCustomer Is Nothing Then
                    exs.Add(New Exception(oUser.Lang.Tradueix("Usuario no autorizado para este cliente", "Usuari no autoritzat per aquest client", "User not authorized for this customer")))
                Else
                    Dim oRepProducts As List(Of DTORepProduct) = BLL.BLLRepProducts.All(BLL.BLLApp.Emp, , False)
                    Dim oRepCliComs As List(Of DTORepCliCom) = BLL.BLLRepCliComs.All(BLL.BLLApp.Emp)
                    oOrder = New DTOPurchaseOrder
                    With oOrder
                        .Cod = DTOPurchaseOrder.Codis.Client
                        .UsrCreated = oUser
                        .Customer = oCustomer
                        .Fch = Today
                        .Concept = IIf(purchaseOrder.Obs = "", "via Api", purchaseOrder.Obs)
                        .Source = DTOPurchaseOrder.Sources.cliente_XML

                        For Each item In purchaseOrder.items
                            Dim oSku As DTOProductSku = BLLProductSku.FromId(item.Sku.Id)
                            If oSku Is Nothing Then
                                exs.Add(New Exception(oUser.Lang.Tradueix("producto desconocido", "producte desconegut", "unknown product") & " " & item.Sku.Guid.ToString))
                            Else
                                BLLProductSku.Load(oSku)
                                Dim oItem As New DTOPurchaseOrderItem
                                With oItem
                                    .PurchaseOrder = oOrder
                                    .Qty = item.Qty
                                    .Pending = item.Qty
                                    .Sku = oSku
                                    .Price = BLLProductSku.Price(oSku, oCustomer)
                                    .RepCom = BLLPurchaseOrderItem.SuggestedRepCom(oItem, oRepProducts, oRepCliComs, exs)
                                End With
                                .Items.Add(oItem)
                            End If
                        Next

                        If exs.Count > 0 Then
                        Else
                            If pruebas Then
                                oOrder.Num = 11111
                            Else
                                BLLPurchaseOrder.Update(oOrder, exs)
                            End If

                            With purchaseOrder
                                .Id = oOrder.Num
                                .Guid = oOrder.Guid
                                .Fch = oOrder.Fch
                                .Customer.Nom = oOrder.Customer.Nom
                                .user.Nom = oUser.EmailAddress
                                .items = New List(Of DUI.PurchaseOrderItem)
                                .Eur = BLLPurchaseOrder.Eur(oOrder)
                                For Each item As DTOPurchaseOrderItem In oOrder.Items
                                    Dim oItem As New DUI.PurchaseOrderItem
                                    With oItem
                                        .Guid = item.Guid
                                        .Qty = item.Qty
                                        .Sku = New DUI.Sku
                                        With .Sku
                                            .Id = item.Sku.Id
                                            .Guid = item.Sku.Guid
                                            .Nom = item.Sku.NomLlarg
                                            .Price = item.Price.Eur
                                            If item.Sku.RRPP IsNot Nothing Then
                                                .RRPP = item.Sku.RRPP.Eur
                                            End If
                                            .Stock = item.Sku.Stock
                                            .Moq = BLLProductSku.Moq(item.Sku)
                                        End With
                                        .Eur = item.Price.Eur
                                        If item.Dto <> 0 Then
                                            .Dto = item.Dto
                                        End If
                                    End With
                                    .items.Add(oItem)
                                Next
                            End With

                        End If
                    End With
                End If
            End If

        Catch ex As Exception
            BLL.BLLWinBug.Log("api/purchaseOrder exception: " & ex.Message)
        End Try

        If exs.Count = 0 Then
            BLLPurchaseOrder.MailConfirmation(oOrder, Nothing, exs)
        Else
            For Each ex As Exception In exs
                purchaseOrder.validationErrors.Add(ex.Message)
            Next
        End If

        Return purchaseOrder

    End Function

    <HttpPost>
    <Route("api/purchaseorder/delete")>
    Public Function Delete(purchaseOrder As DUI.PurchaseOrder) As DUI.TaskResult
        Dim retval As New DUI.TaskResult
        If purchaseOrder.Guid = Nothing Then
            retval.Message = "falta identificador Guid del pedido"
        Else
            Dim oPurchaseOrder As New DTOPurchaseOrder(purchaseOrder.Guid)
            Dim exs As New List(Of Exception)
            If BLLPurchaseOrder.Delete(oPurchaseOrder, exs) Then
                retval.Success = True
            Else
                retval.Message = BLLExceptions.ToFlatString(exs)
            End If
        End If
        Return retval
    End Function

    <HttpPost>
    <Route("api/contact/purchaseorders/pending")>
    Public Function Pending(customer As DUI.Contact) As List(Of DUI.PurchaseOrder)
        Dim retval As New List(Of DUI.PurchaseOrder)
        Dim oCustomer As New DTOCustomer(customer.Guid)
        Dim oOrders As List(Of DTOPurchaseOrder) = BLLPurchaseOrders.Pending(oCustomer)
        For Each oOrder As DTOPurchaseOrder In oOrders
            Dim dui As New DUI.PurchaseOrder
            With dui
                .Guid = oOrder.Guid
                .Nom = oOrder.Concept
                .Id = oOrder.Num
                .Eur = BLLPurchaseOrder.SumaDeImportes(oOrder).Eur
                .Fch = oOrder.Fch
                .Customer = New DUI.Guidnom
                .Customer.Guid = oOrder.Customer.Guid
                .Customer.Nom = oOrder.Customer.FullNom
                .TotJunt = oOrder.TotJunt
                If .FchMin <> Nothing Then
                    .FchMin = oOrder.FchDelivery
                End If
                If oOrder.Incentiu IsNot Nothing Then
                    .Promo = New DUI.Promo
                    .Promo.Guid = oOrder.Incentiu.Guid
                    .Promo.Nom = BLLIncentiu.Title(oOrder.Incentiu, DTOLang.ESP)
                End If

                If oOrder.DocFile IsNot Nothing Then
                    .FileUrl = BLLDocFile.DownloadUrl(oOrder.DocFile, True)
                    .ThumbnailUrl = BLLDocFile.ThumbnailUrl(oOrder.DocFile, True)
                End If
                dui.items = New List(Of DUI.PurchaseOrderItem)
            End With
            For Each item As DTOPurchaseOrderItem In oOrder.Items
                Dim duitem As New DUI.PurchaseOrderItem
                With duitem
                    .Guid = item.Guid
                    .Qty = item.Pending
                    .Eur = item.Price.Eur
                    .Dto = item.Dto
                    .Sku = New DUI.Sku
                    .Sku.Guid = item.Sku.Guid
                    .Sku.Nom = item.Sku.NomCurt
                    .Sku.Category = New DUI.Category
                    .Sku.Category.Guid = item.Sku.Category.Guid
                    .Sku.Category.Nom = item.Sku.Category.Nom
                    .Sku.Category.Brand = New DUI.Brand
                    .Sku.Category.Brand.Guid = item.Sku.Category.Brand.Guid
                    .Sku.Category.Brand.Nom = item.Sku.Category.Brand.Nom
                End With
                dui.items.Add(duitem)
            Next

            retval.Add(dui)
        Next
        Return retval
    End Function

    <HttpPost>
    <Route("api/contact/purchaseorders/pendingSkus")>
    Public Function PendingSkus(customer As DUI.Contact) As List(Of DUI.PurchaseOrderItem)
        Dim retval As New List(Of DUI.PurchaseOrderItem)
        Dim oCustomer As New DTOCustomer(customer.Guid)
        Dim items As List(Of DTOPurchaseOrderItem) = BLLPurchaseOrders.PendingSkus(oCustomer)
        For Each item As DTOPurchaseOrderItem In items
            Dim duitem As New DUI.PurchaseOrderItem
            With duitem
                .Guid = item.Guid
                .Qty = item.Pending
                .Eur = item.Price.Eur
                .Dto = item.Dto
                .Sku = New DUI.Sku
                .Sku.Guid = item.Sku.Guid
                .Sku.Nom = item.Sku.NomCurt
                .Sku.Category = New DUI.Category
                .Sku.Category.Guid = item.Sku.Category.Guid
                .Sku.Category.Nom = item.Sku.Category.Nom
                .Sku.Category.Brand = New DUI.Brand
                .Sku.Category.Brand.Guid = item.Sku.Category.Brand.Guid
                .Sku.Category.Brand.Nom = item.Sku.Category.Brand.Nom
            End With
            retval.Add(duitem)
        Next

        Return retval
    End Function

End Class
