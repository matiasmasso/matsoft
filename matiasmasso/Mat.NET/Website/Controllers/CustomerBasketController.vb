Imports MatHelperStd
Imports System.Linq
Public Class CustomerBasketController
    Inherits _MatController

    Async Function Index() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing

        Dim oUser = ContextHelper.GetUser()
        If oUser Is Nothing Then
            retval = LoginOrView("CustomerBasket")
        Else
            Select Case oUser.Rol.id
                Case DTORol.Ids.cliFull, DTORol.Ids.cliLite
                    Dim oCustomers = Await FEB.User.CustomersForBasket(exs, oUser)
                    If oCustomers.Count = 0 Then
                        Return MyBase.UnauthorizedView
                    Else
                        ViewBag.Title = ContextHelper.Tradueix("Formulario de pedido", "Formulari de Comanda", "Customer Order Form")
                        Return View("CustomerBasket", oCustomers)
                    End If
                Case DTORol.Ids.unregistered
                    retval = LoginOrView("CustomerBasket")
                Case Else
                    retval = MyBase.UnauthorizedView()
            End Select
        End If

        Return retval

    End Function

    Async Function UpdateRep() As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim retval As PartialViewResult = Nothing
        Dim oUser = ContextHelper.GetUser()

        Dim jsonMailConfirmationRequest = Request.Form("mailConfirmation")
        Dim jsonOrder As String = Request.Form("purchaseOrder")

        Try
            Dim oOrder = JsonHelper.DeSerialize(Of DTOPurchaseOrder)(jsonOrder, exs)
            With oOrder
                .Emp = .Customer.Emp
                .Cod = DTOPurchaseOrder.Codis.client
                .Cur = DTOCur.Eur
                .Fch = DTO.GlobalVariables.Today()
                .Source = DTOPurchaseOrder.Sources.representante_por_Web
                .UsrLog = DTOUsrLog.Factory(oUser)
            End With

            oOrder.Num = Await FEB.RepBasket.Update(exs, oOrder)

            If exs.Count = 0 Then
                Dim oOrders As New List(Of DTOPurchaseOrder)
                If exs.Count = 0 Then oOrders = Await FEB.PurchaseOrders.LastOrdersEntered(exs, oUser)
                If exs.Count = 0 Then
                    retval = PartialView("_LastOrdersEntered", oOrders)
                Else
                    retval = PartialView("_error", exs)
                End If

                Dim blCcRep = JsonHelper.DeSerialize(Of Boolean)(jsonMailConfirmationRequest, exs)
                'Dim blCcRep = jss.Deserialize(Of Boolean)(jsonMailConfirmationRequest)
                Dim oMailMessage = DTOPurchaseOrder.mailMessageRepConfirmation(oOrder, blCcRep)
                Await FEB.MailMessage.Send(exs, DTOUser.Wellknown(DTOUser.Wellknowns.info), oMailMessage)
            End If


        Catch ex As Exception
            retval = PartialView("_error", {ex}.ToList)
        End Try

        Return retval

    End Function

    Public Async Function Update() As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim retval As PartialViewResult = Nothing
        Dim oUser = ContextHelper.GetUser()
        FEB.User.Load(exs, oUser)

        Dim oOrder As DTOPurchaseOrder = Nothing

        Dim jsonMailConfirmationRequest = Request.Form("mailConfirmation")
        Dim jsonOrder As String = Request.Form("purchaseOrder")

        Try
            oOrder = JsonHelper.DeSerialize(Of DTOPurchaseOrder)(jsonOrder, exs)
            With oOrder
                .Emp = oUser.Emp
                .Cod = DTOPurchaseOrder.Codis.client
                .Cur = DTOCur.Eur
                .Fch = DTO.GlobalVariables.Today()
                .Source = DTOPurchaseOrder.Sources.cliente_por_Web
                .UsrLog = DTOUsrLog.Factory(oUser)
            End With

            Dim oFiles = MyBase.PostedFiles()
            If oFiles.Count > 0 Then
                oOrder.EtiquetesTransport = LegacyHelper.DocfileHelper.Factory(oFiles, exs).First()
            End If

            oOrder.Num = Await FEB.CustomerBasket.Update(exs, oOrder)

            If exs.Count = 0 Then
                Dim oMailMessage = DTOPurchaseOrder.mailMessageConfirmation(oOrder)
                Await FEB.MailMessage.Send(exs, DTOUser.Wellknown(DTOUser.Wellknowns.info), oMailMessage)
            End If

        Catch ex As Exception
            exs.Add(ex)
        End Try

        If exs.Count = 0 Then
            retval = PartialView("Thanks", oOrder)
        Else
            retval = PartialView("_error", exs)
        End If

        Return retval
    End Function

    Async Function DeliverableItems(basket As String, merge As Boolean) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim oBasket As DTOBasket = FEB.Basket.GetModelBasket(exs, basket)
        Dim oUser = ContextHelper.GetUser()
        Dim oPurchaseOrder = Await FEB.Basket.GetPurchaseOrder(oBasket, oUser, DTOPurchaseOrder.Sources.cliente_por_Web)
        Dim oCustomer As DTOCustomer = oPurchaseOrder.Contact
        FEB.Customer.Load(oCustomer, exs)
        Dim oCcx = FEB.Customer.CcxOrMe(exs, oCustomer)
        oCcx.ProductDtos = Await FEB.CliProductDtos.All(oCcx, exs)

        Dim oPendingItems = Await FEB.PurchaseOrderItems.Pending(exs, oCustomer, DTOPurchaseOrder.Codis.client, Website.GlobalVariables.Emp.Mgz)
        Dim oDeliverableItems As New List(Of DTOPurchaseOrderItem)
        For Each item As DTOPurchaseOrderItem In oPendingItems
            If item.Pending <= (item.Sku.Stock - item.Sku.Clients) Then
                oDeliverableItems.Add(item)
            End If
        Next

        'FEB.PurchaseOrder.SetIncentius(oPurchaseOrder)

        Dim retval As PartialViewResult = PartialView("DeliverableItems_", oDeliverableItems)
        Return retval

    End Function

    Async Function Thanks(data As Guid) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim retval As PartialViewResult = Nothing
        Dim oPurchaseOrder = Await FEB.PurchaseOrder.Find(data, exs)
        retval = PartialView("Thanks_PurchaseOrder_", oPurchaseOrder)
        Return retval
    End Function

    Overloads Async Function MailConfirmation(orderGuid As Guid) As Threading.Tasks.Task(Of JsonResult)
        Dim exs As New List(Of Exception)
        Dim oOrder = Await FEB.PurchaseOrder.Find(orderGuid, exs)
        FEB.User.Load(exs, oOrder.UsrLog.UsrCreated)
        Dim oMailMessage = DTOPurchaseOrder.mailMessageConfirmation(oOrder)

        Dim myData As Object
        Dim oUser = ContextHelper.GetUser()
        If Await FEB.MailMessage.Send(exs, oUser, oMailMessage) Then
            myData = New With {.result = "1"}
        Else
            myData = New With {.result = "0", .message = exs(0)}
        End If

        Dim retval As JsonResult = Json(myData, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

End Class