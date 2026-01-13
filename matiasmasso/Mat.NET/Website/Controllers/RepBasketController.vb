Imports MatHelperStd

Public Class RepBasketController
    Inherits _MatController


    Function Index() As ActionResult
        Dim oUser = ContextHelper.GetUser
        If oUser Is Nothing Then
            Return MyBase.UnauthorizedView
        Else
            ViewBag.Title = ContextHelper.Tradueix("Pedido de Representante", "Comanda de Representant", "Sales Order")
            Return View("NewRepBasket")
        End If
    End Function

    Async Function ForCustomer(customer As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oUser = ContextHelper.GetUser
        If oUser Is Nothing Then
            Return MyBase.UnauthorizedView
        Else
            Dim oCustomer As New DTOCustomer(customer)
            Dim oTarifa = Await FEB.CustomerBasket.Tarifa(exs, oUser, oCustomer)
            ViewBag.Title = ContextHelper.Tradueix("Pedido de Representante", "Comanda de Representant", "Sales Order")
            Return View("NewRepBasket", oTarifa)
        End If
    End Function


    Async Function Update() As Threading.Tasks.Task(Of PartialViewResult)
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
                Dim oMailMessage = DTOPurchaseOrder.mailMessageRepConfirmation(oOrder, blCcRep)
                Await FEB.MailMessage.Send(exs, DTOUser.Wellknown(DTOUser.Wellknowns.info), oMailMessage)
            End If


        Catch ex As Exception
            retval = PartialView("_error", {ex}.ToList)
        End Try

        Return retval

    End Function


End Class