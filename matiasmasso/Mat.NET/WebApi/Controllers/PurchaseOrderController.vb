Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class PurchaseOrderController
    Inherits _BaseController


    <HttpGet>
    <Route("api/purchaseorder/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage
        Try
            Dim value = BEBL.PurchaseOrder.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la purchaseorder")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/purchaseorder/{guid}/{mgz}")>
    Public Function FindWithMgz(guid As Guid, mgz As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage
        Try
            Dim oMgz = DTOBaseGuid.Opcional(Of DTOMgz)(mgz)
            Dim value = BEBL.PurchaseOrder.Find(guid, oMgz)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la comanda")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/purchaseorder/fromNum/{emp}/{year}/{id}")>
    Public Function FromNum(emp As Integer, year As Integer, id As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim value = BEBL.PurchaseOrder.FromNum(oEmp, year, id)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la comanda")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/purchaseorder/fromEdi/{emp}")>
    Public Function FromEdi(emp As Integer, <FromBody> src As String) As HttpResponseMessage
        Dim exs As New List(Of Exception)
        Dim retval As HttpResponseMessage
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim value = BEBL.PurchaseOrder.FromEdi(exs, oEmp, src)
            If exs.Count = 0 Then
                retval = Request.CreateResponse(HttpStatusCode.OK, value)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al llegir la comanda des de Edi")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la comanda des de Edi")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/PurchaseOrder/WithDeliveryItems/{order}")>
    Public Function WithDeliveryItems(order As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage
        Try
            Dim value = BEBL.PurchaseOrder.FindWithDeliveries(order)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la purchaseorder")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/PurchaseOrder/Update")>
    Public Function Upload() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value = ApiHelper.Client.DeSerialize(Of DTOPurchaseOrder)(json, exs)
            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar la PurchaseOrder")
            Else
                For Each item In value.items
                    item.purchaseOrder = value
                    For Each oBundle In item.bundle
                        oBundle.purchaseOrder = value
                    Next
                Next

                If value.DocFile IsNot Nothing Then
                    value.DocFile.Thumbnail = oHelper.GetImage("docfile_thumbnail")
                    value.DocFile.Stream = oHelper.GetFileBytes("docfile_stream")
                End If
                If value.EtiquetesTransport IsNot Nothing Then
                    value.EtiquetesTransport.Thumbnail = oHelper.GetImage("EtiquetesTransport_thumbnail")
                    value.EtiquetesTransport.Stream = oHelper.GetFileBytes("EtiquetesTransport_stream")
                End If

                If BEBL.PurchaseOrder.Update(value, exs) Then
                    result = Request.CreateResponse(HttpStatusCode.OK, value)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar la comanda")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error a DAL.PurchaseOrderLoader")
        End Try

        Return result
    End Function



    <HttpPost>
    <Route("api/PurchaseOrder/delete")>
    Public Function Delete(<FromBody> value As DTOPurchaseOrder) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.PurchaseOrder.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la comanda")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la comanda")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/PurchaseOrder/cobraPerVisa")>
    Public Function cobraPerVisa(<FromBody> value As DTOTpvLog) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.PurchaseOrder.CobraPerVisa(value, exs) Then
                retval = Request.CreateResponse(Of DTOTpvLog)(HttpStatusCode.OK, value)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al cobrar la comanda per Visa")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al cobrar la comanda per Visa")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/PurchaseOrder/ResetPendingQty/{purchaseorder}")>
    Public Function ResetPendingQty(purchaseorder As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oPurchaseOrder As New DTOPurchaseOrder(purchaseorder)
            If BEBL.PurchaseOrder.ResetPendingQty(oPurchaseOrder, exs) Then
                retval = Request.CreateResponse(HttpStatusCode.OK)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al ResetPendingQty")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al ResetPendingQty")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/PurchaseOrder/SearchConcepte")>
    Public Function SearchConcepte(<FromBody> sKey As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.PurchaseOrder.SearchConcepte(sKey)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al cercar el concepte")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/PurchaseOrder/RecalculaPendents/{purchaseorder}")>
    Public Function RecalculaPendents(purchaseorder As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oPurchaseOrder As New DTOPurchaseOrder(purchaseorder)
            Dim value = BEBL.PurchaseOrder.RecalculaPendents(exs, oPurchaseOrder)
            If exs.Count = 0 Then
                retval = Request.CreateResponse(HttpStatusCode.OK, value)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al recalcular les partides pendents")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al recalcular les partides pendents")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/PurchaseOrder/RemovePromo/{purchaseorder}")>
    Public Function RemovePromo(purchaseorder As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oPurchaseOrder As New DTOPurchaseOrder(purchaseorder)
            If BEBL.PurchaseOrder.RemovePromo(exs, oPurchaseOrder) Then
                retval = Request.CreateResponse(HttpStatusCode.OK)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al retirar la promo")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al retirar la promo")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/purchaseorder/submit")>
    Public Async Function Submit(<FromBody> purchaseOrder As DTOCompactPO) As Threading.Tasks.Task(Of HttpResponseMessage)
        Dim retval As HttpResponseMessage
        Try
            Dim value As DTOCompactPO = Await SubmitProcess(purchaseOrder, pruebas:=False)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la comanda")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/purchaseorder/submit/pruebas")>
    Public Async Function SubmitPruebas(<FromBody> purchaseOrder As DTOCompactPO) As Threading.Tasks.Task(Of HttpResponseMessage)
        Dim retval As HttpResponseMessage
        Try
            Dim value As DTOCompactPO = Await SubmitProcess(purchaseOrder, pruebas:=True)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la comanda en proves")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/purchaseorder/upload")>
    Public Function PurchaseorderUpload(sJson As String) As DTOTaskResult
        Dim exs As New List(Of Exception)
        Dim retval As DTOTaskResult
        Dim msg As String = ""

        BEBL.JsonSchema.Validate(DTOJsonSchema.Wellknowns.PurchaseOrder, sJson, exs)

        If exs.Count = 0 Then
            retval = DTOTaskResult.Factory(DTOTask.ResultCods.success, msg)
        Else
            retval = DTOTaskResult.FailResult(exs, msg)
        End If
        Return retval

    End Function

    Private Async Function SubmitProcess(purchaseOrder As DTOCompactPO, Optional pruebas As Boolean = False) As Threading.Tasks.Task(Of DTOCompactPO)
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
                    exs.Add(New Exception(oUser.lang.Tradueix("Usuario no autorizado para este cliente", "Usuari no autoritzat per aquest client", "User not authorized for this customer")))
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
                            .concept = purchaseOrder.Nom
                        ElseIf Not String.IsNullOrEmpty(purchaseOrder.Obs) Then
                            .concept = purchaseOrder.Obs
                        Else
                            .concept = "via Api"
                        End If

                        Dim lin As Integer

                        For Each item In purchaseOrder.Items
                            lin += 1
                            Dim oItem As New DTOPurchaseOrderItem
                            oItem.sku = DTOProductSku.Wellknown(DTOProductSku.Wellknowns.UnknownSku)

                            If item.Sku Is Nothing Then
                                oItem.ErrCod = DTOPurchaseOrderItem.ErrCods.UnknownProduct
                                oItem.ErrDsc = oUser.lang.Tradueix("Producto no especificado", "Producte no especificat", "Missing product")
                            ElseIf item.Sku.Ean Is Nothing Then
                                oItem.ErrCod = DTOPurchaseOrderItem.ErrCods.UnknownProduct
                                oItem.ErrDsc = oUser.lang.Tradueix("Falta código Ean", "Falta codi Ean", "Missing Ean code")
                            ElseIf String.IsNullOrEmpty(item.Sku.Ean) Then
                                oItem.ErrCod = DTOPurchaseOrderItem.ErrCods.UnknownProduct
                                oItem.ErrDsc = oUser.lang.Tradueix("Código Ean vacío", "Codi Ean buit", "Empty Ean code")
                            Else
                                Dim sEan = item.Sku.Ean
                                If oSkus.Any(Function(x) x.ean13.value = sEan) Then
                                    oItem.sku = oSkus.FirstOrDefault(Function(x) x.ean13.value = sEan)
                                    Select Case oItem.sku.codExclusio
                                        Case DTOProductSku.CodisExclusio.Inclos
                                        Case DTOProductSku.CodisExclusio.Obsolet, DTOProductSku.CodisExclusio.OutOfCatalog
                                            If oItem.sku.stockAvailable = 0 Then
                                                oItem.ErrCod = DTOPurchaseOrderItem.ErrCods.Obsolet
                                                oItem.ErrDsc = oUser.lang.Tradueix("Producto obsoleto", "Producte obsolet", "outdated product")
                                            End If
                                        Case DTOProductSku.CodisExclusio.Canal
                                            oItem.ErrCod = DTOPurchaseOrderItem.ErrCods.ChannelExcluded
                                            oItem.ErrDsc = oUser.lang.Tradueix("Distribución no autorizada en este canal", "Distribució no autoritzada en aquest canal", "Unallowed distribution channel")
                                        Case DTOProductSku.CodisExclusio.Exclusives
                                            oItem.ErrCod = DTOPurchaseOrderItem.ErrCods.CustomerExcluded
                                            oItem.ErrDsc = oUser.lang.Tradueix("Producto de distribución selectiva", "Producte de distribució selectiva", "Restricted distribution")
                                        Case DTOProductSku.CodisExclusio.PremiumLine
                                            oItem.ErrCod = DTOPurchaseOrderItem.ErrCods.PremiumLineExcluded
                                            oItem.ErrDsc = oUser.lang.Tradueix("Producto exclusivo de distribución con contrato", "Producte exclusiu de distribució sota contracte", "Distribution contract required for this product")
                                    End Select

                                Else
                                    oItem.ErrCod = DTOPurchaseOrderItem.ErrCods.UnknownProduct
                                    oItem.ErrDsc = String.Format(oUser.lang.Tradueix("Código Ean {0} desconocido", "Codi Ean {0} desconegut", "Ean code {0} not found"), sEan)
                                End If

                                With item
                                    With .Sku
                                        .Guid = oItem.sku.Guid
                                        .Nom = oItem.sku.NomLlarg.Esp
                                        .Moq = oItem.sku.innerPackOrInherited()
                                        .Stock = oItem.sku.stockAvailable()
                                    End With
                                    If oItem.sku.price IsNot Nothing Then .Price = oItem.sku.price.eur
                                End With

                                With oItem
                                    .purchaseOrder = oOrder
                                    .qty = item.Qty
                                    .pending = item.Qty
                                    .price = DTOAmt.Factory(item.Price)
                                    If .ErrCod = DTOPurchaseOrderItem.ErrCods.Success Then
                                        .repCom = BEBL.PurchaseOrderItem.SuggestedRepCom(oUser, oItem, oRepProducts, oRepCliComs, exs)
                                    End If
                                End With
                                .items.Add(oItem)
                            End If
                        Next

                        With purchaseOrder
                            .Fch = oOrder.fch
                            .Eur = oOrder.SumaDeImportes().eur

                            If oOrder IsNot Nothing Then
                                If pruebas Then
                                    .Guid = oOrder.Guid
                                    .Id = 11111
                                Else
                                    oOrder.docFile = BEBL.DocFile.LoadFromString(src)
                                    If BEBL.PurchaseOrder.Update(oOrder, exs) Then
                                        .Guid = oOrder.Guid
                                        .Id = oOrder.num
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

    <HttpGet>
    <Route("api/purchaseOrder/mailMe/{order}/{user}")> 'for SwiftUI
    Public Async Function mailme(order As Guid, user As Guid) As Threading.Tasks.Task(Of HttpResponseMessage)
        Dim exs As New List(Of Exception)
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim oOrder = BEBL.PurchaseOrder.Find(order)
            Dim oMailMessage = DTOPurchaseOrder.mailMessageConfirmation(oOrder)
            Await BEBL.MailMessageHelper.Send(oUser.Emp, oMailMessage, exs)

            If exs.Count = 0 Then
                retval = Request.CreateResponse(Of Integer)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al enviar la comanda")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al enviar la comanda")
        End Try
        Return retval
    End Function




End Class

Public Class PurchaseOrdersController
    Inherits _BaseController

    <HttpGet>
    <Route("api/purchaseorders/years/{emp}/{cod}/{contact}/{includeGroupSalePoints}")>
    Public Function Years(emp As DTOEmp.Ids, cod As DTOPurchaseOrder.Codis, contact As Guid, includeGroupSalePoints As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oContact = DTOBaseGuid.Opcional(Of DTOContact)(contact)
            Dim value = BEBL.PurchaseOrders.Years(oEmp, cod, oContact, includeGroupSalePoints)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els anys amb comanda")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/purchaseorders/{emp}/{cod}/{year}/{contact}/{includeGroupSalePoints}")>
    Public Function All(emp As DTOEmp.Ids, cod As DTOPurchaseOrder.Codis, year As Integer, contact As Guid, includeGroupSalePoints As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oContact = DTOBaseGuid.Opcional(Of DTOContact)(contact)
            Dim value = BEBL.PurchaseOrders.All(oEmp, cod, oContact, year, includeGroupSalePoints)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les comandes")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/purchaseorders/fromUser/{user}")> ' per iMat
    Public Function FromUser(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim values = BEBL.PurchaseOrders.All(oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les comandes")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/purchaseorders/Headers/fromCustomer/{customer}")>
    Public Function HeadersFromCustomer(customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage
        Try
            Dim oCustomer = New DTOCustomer(customer)
            Dim values = BEBL.PurchaseOrders.Headers(Cod:=DTOPurchaseOrder.Codis.client, Contact:=oCustomer)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les comandes")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/purchaseorders/Headers/{emp}/{cod}/{contact}/{ccx}/{rep}/{year}/{fchcreatedfrom}/{fchcreatedTo}")>
    Public Function All(emp As DTOEmp.Ids, cod As DTOPurchaseOrder.Codis, contact As Guid, ccx As Guid, rep As Guid, year As Integer, fchcreatedfrom As Date, fchcreatedto As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oContact = DTOBaseGuid.Opcional(Of DTOContact)(contact)
            Dim oCcx = DTOBaseGuid.Opcional(Of DTOCustomer)(ccx)
            Dim oRep = DTOBaseGuid.Opcional(Of DTORep)(rep)
            Dim values = BEBL.PurchaseOrders.Headers(oEmp, cod, oContact, oCcx, oRep, year, fchcreatedfrom, fchcreatedto)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les comandes")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/purchaseorders/Exists/{user}/{fchfrom}/{fchto}")>
    Public Function Exists(user As Guid, fchfrom As Date, fchto As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage
        Try
            Dim oUser As New DTOUser(user)
            Dim value = BEBL.PurchaseOrders.Exists(oUser, fchfrom, fchto)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les comandes")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/purchaseorders/LastOrdersEntered/{user}")>
    Public Function LastOrdersEntered(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage
        Try
            Dim oUser As New DTOUser(user)
            Dim values = BEBL.PurchaseOrders.LastOrdersEntered(oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les ultimes comandes")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/purchaseorders/Pending/{emp}/{cod}/{contact}")>
    Public Function Pending(emp As DTOEmp.Ids, cod As DTOPurchaseOrder.Codis, contact As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oContact = DTOBaseGuid.opcional(Of DTOContact)(contact)
            Dim values = BEBL.PurchaseOrders.Pending(oEmp, cod, oContact)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les comandes pendents")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/purchaseorders/Pending/{user}/{cod}")>
    Public Function Pending(user As Guid, cod As DTOPurchaseOrder.Codis) As HttpResponseMessage
        Dim retval As HttpResponseMessage
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim values = BEBL.PurchaseOrders.Pending(oUser.Emp, cod, Nothing, oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les comandes pendents")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/purchaseorders/PendingForPlatforms/{customer}")>
    Public Function PendingForPlatforms(customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage
        Try
            Dim oCustomer As New DTOCustomer(customer)
            Dim values = BEBL.PurchaseOrders.PendingForPlatforms(oCustomer)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les comandes pendents")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/purchaseorders/PendingForHoldingPlatforms/{holding}")>
    Public Function PendingForHoldingPlatforms(holding As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage
        Try
            Dim oHolding As New DTOHolding(holding)
            Dim values = BEBL.PurchaseOrders.PendingForPlatforms(oHolding)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les comandes pendents")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/purchaseorders/StocksAvailableForPlatforms/{emp}/{customer}")>
    Public Function StocksAvailableForPlatforms(emp As DTOEmp.Ids, customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oCustomer As New DTOCustomer(customer)
            Dim values = BEBL.PurchaseOrders.StocksAvailableForPlatforms(oEmp, oCustomer)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els stocks disponibles")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/purchaseorders/StocksAvailableForHoldingPlatforms/{emp}/{holding}")>
    Public Function StocksAvailableForHoldingPlatforms(emp As DTOEmp.Ids, holding As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oHolding As New DTOHolding(holding)
            Dim values = BEBL.PurchaseOrders.StocksAvailableForPlatforms(oEmp, oHolding)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els stocks disponibles")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/PurchaseOrders/delete")>
    Public Function Delete(<FromBody> values As List(Of Guid)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.PurchaseOrders.Delete(exs, values) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar les comandes")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar les comandes")
        End Try
        Return retval
    End Function

End Class