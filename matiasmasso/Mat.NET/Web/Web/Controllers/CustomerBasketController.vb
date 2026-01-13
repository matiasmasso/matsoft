Public Class CustomerBasketController
    Inherits _MatController

    Async Function Index() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing

        Dim oUser = ContextHelper.GetUser()
        If oUser Is Nothing Then
            retval = LoginOrView("CustomerBasket")
        Else
            Select Case oUser.Rol.Id
                Case DTORol.Ids.CliFull, DTORol.Ids.CliLite
                    Dim oCustomers = Await FEB2.User.CustomersForBasket(exs, oUser)
                    If oCustomers.Count = 0 Then
                        Return MyBase.UnauthorizedView
                    Else
                        ViewBag.Title = Mvc.ContextHelper.Tradueix("Formulario de pedido", "Formulari de Comanda", "Customer Order Form")
                        Return View("CustomerBasket", oCustomers)
                    End If
                Case DTORol.Ids.Unregistered
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
                .emp = .customer.emp
                .cod = DTOPurchaseOrder.Codis.client
                .Cur = DTOCur.Eur
                .fch = Today
                .source = DTOPurchaseOrder.Sources.representante_por_Web
                .UsrLog = DTOUsrLog.Factory(oUser)
            End With

            oOrder.num = Await FEB2.RepBasket.Update(exs, oOrder)

            If exs.Count = 0 Then
                Dim oOrders As New List(Of DTOPurchaseOrder)
                If exs.Count = 0 Then oOrders = Await FEB2.PurchaseOrders.LastOrdersEntered(exs, oUser)
                If exs.Count = 0 Then
                    retval = PartialView("_LastOrdersEntered", oOrders)
                Else
                    retval = PartialView("_error", exs)
                End If

                Dim blCcRep = JsonHelper.DeSerialize(Of Boolean)(jsonMailConfirmationRequest, exs)
                'Dim blCcRep = jss.Deserialize(Of Boolean)(jsonMailConfirmationRequest)
                Dim oMailMessage = DTOPurchaseOrder.MailMessageRepConfirmation(oOrder, blCcRep)
                Await FEB2.MailMessage.Send(exs, DTOUser.Wellknown(DTOUser.Wellknowns.info), oMailMessage)
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
        FEB2.User.Load(exs, oUser)

        Dim oOrder As DTOPurchaseOrder = Nothing

        Dim jsonMailConfirmationRequest = Request.Form("mailConfirmation")
        Dim jsonOrder As String = Request.Form("purchaseOrder")

        Try
            oOrder = JsonHelper.DeSerialize(Of DTOPurchaseOrder)(jsonOrder, exs)
            With oOrder
                .emp = oUser.Emp
                .cod = DTOPurchaseOrder.Codis.client
                .Cur = DTOCur.Eur
                .fch = Today
                .source = DTOPurchaseOrder.Sources.cliente_por_Web
                .UsrLog = DTOUsrLog.Factory(oUser)
            End With

            Dim oFiles = MyBase.PostedFiles()
            If oFiles.Count > 0 Then
                oOrder.etiquetesTransport = LegacyHelper.DocfileHelper.Factory(oFiles, exs).First
            End If

            oOrder.Num = Await FEB2.CustomerBasket.Update(exs, oOrder)

            If exs.Count = 0 Then
                Dim oMailMessage = DTOPurchaseOrder.mailMessageConfirmation(oOrder)
                Await FEB2.MailMessage.Send(exs, DTOUser.Wellknown(DTOUser.Wellknowns.info), oMailMessage)
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
        Dim oBasket As DTOBasket = FEB2.Basket.GetModelBasket(exs, basket)
        Dim oUser = ContextHelper.GetUser()
        Dim oPurchaseOrder = Await FEB2.Basket.GetPurchaseOrder(oBasket, oUser, DTOPurchaseOrder.Sources.cliente_por_Web)
        Dim oCustomer As DTOCustomer = oPurchaseOrder.Contact
        FEB2.Customer.Load(oCustomer, exs)
        Dim oCcx = FEB2.Customer.CcxOrMe(exs, oCustomer)
        oCcx.ProductDtos = Await FEB2.CliProductDtos.All(oCcx, exs)

        Dim oPendingItems = Await FEB2.PurchaseOrderItems.Pending(exs, oCustomer, DTOPurchaseOrder.Codis.Client, GlobalVariables.Emp.Mgz)
        Dim oDeliverableItems As New List(Of DTOPurchaseOrderItem)
        For Each item As DTOPurchaseOrderItem In oPendingItems
            If item.Pending <= (item.Sku.Stock - item.Sku.Clients) Then
                oDeliverableItems.Add(item)
            End If
        Next

        'FEB2.PurchaseOrder.SetIncentius(oPurchaseOrder)

        Dim retval As PartialViewResult = PartialView("DeliverableItems_", oDeliverableItems)
        Return retval

    End Function

    Async Function Thanks(data As Guid) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim retval As PartialViewResult = Nothing
        Dim oPurchaseOrder = Await FEB2.PurchaseOrder.Find(data, exs)
        retval = PartialView("Thanks_PurchaseOrder_", oPurchaseOrder)
        Return retval
    End Function

    Overloads Async Function MailConfirmation(orderGuid As Guid) As Threading.Tasks.Task(Of JsonResult)
        Dim exs As New List(Of Exception)
        Dim oOrder = Await FEB2.PurchaseOrder.Find(orderGuid, exs)
        FEB2.User.Load(exs, oOrder.UsrLog.UsrCreated)
        Dim oMailMessage = DTOPurchaseOrder.MailMessageConfirmation(oOrder)

        Dim myData As Object
        Dim oUser = ContextHelper.GetUser()
        If Await FEB2.MailMessage.Send(exs, oUser, oMailMessage) Then
            myData = New With {.result = "1"}
        Else
            myData = New With {.result = "0", .message = exs(0)}
        End If

        Dim retval As JsonResult = Json(myData, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

End Class