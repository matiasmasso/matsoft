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
                        .FchMin = oOrder.FchMin
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
                    .FchMin = oOrder.FchMin
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
