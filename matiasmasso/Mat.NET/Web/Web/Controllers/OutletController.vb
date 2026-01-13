Public Class OutletController
    Inherits _MatController

    Async Function Index() As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Dim exs = New List(Of Exception)
        Dim oUser = ContextHelper.GetUser()
        Select Case MyBase.Authorize(oUser, {DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.Auditor, DTORol.Ids.Manufacturer, DTORol.Ids.SalesManager, DTORol.Ids.Rep, DTORol.Ids.Comercial, DTORol.Ids.CliFull, DTORol.Ids.CliLite})
            Case AuthResults.success
                Dim Model = Await FEB2.Outlet.All(exs, oUser)
                retval = View("Outlet", Model)
            Case AuthResults.login
                retval = LoginOrView("Outlet")
            Case AuthResults.denied
                retval = MyBase.UnauthorizedView()
        End Select

        Return retval
    End Function

    Async Function Update(customer As Guid, lines As String) As Threading.Tasks.Task(Of JsonResult)
        Dim exs As New List(Of Exception)
        Dim oUser = ContextHelper.GetUser()
        Dim oCustomer = Await FEB2.Customer.Find(exs, customer)
        Dim jss As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim oLines As List(Of DTOBasketLine) = jss.Deserialize(Of List(Of DTOBasketLine))(lines)
        Dim oSkus = Await FEB2.Outlet.All(exs, oUser)
        Dim oPurchaseOrder As DTOPurchaseOrder = DTOPurchaseOrder.Factory(oCustomer, oUser, Today, DTOPurchaseOrder.Sources.cliente_por_Web, "Outlet profesional online")
        For Each oLine As DTOBasketLine In oLines
            Dim oItem As New DTOPurchaseOrderItem()
            With oItem
                .PurchaseOrder = oPurchaseOrder
                .Qty = oLine.qty
                .Pending = .Qty
                .Sku = oSkus.First(Function(x) x.Guid.Equals(oLine.sku))
                .Price = .Sku.Price
                .Dto = .Sku.OutletDto
            End With
            oPurchaseOrder.Items.Add(oItem)
        Next

        Dim myData As Object = Nothing
        Dim oBasket As DTOBasket = FEB2.Basket.Factory(oPurchaseOrder)
        Dim pOrder = Await FEB2.PurchaseOrder.Update(exs, oPurchaseOrder)
        If exs.Count = 0 Then
            oPurchaseOrder = pOrder
            myData = New With {.mode = 1, .basket = oBasket, .errors = 0, .guid = oPurchaseOrder.Guid.ToString}
        Else
            myData = New With {.mode = 1, .basket = oBasket, .errors = 1, .message = "se ha producido un error al grabar el pedido"}
        End If
        Dim retval As JsonResult = Json(myData, JsonRequestBehavior.AllowGet)
        Return retval
    End Function


End Class
