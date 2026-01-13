Public Class DeliveryController
    Inherits _BaseController

    <HttpPost>
    <Route("api/contact/deliveries")>
    Public Function ContactDeliveries(contact As DUI.Guidnom) As List(Of DUI.Delivery)
        Dim retval As New List(Of DUI.Delivery)
        Dim oCustomer As New DTOCustomer(contact.Guid)
        Dim items As List(Of DTODelivery) = BLLDeliveries.Headers(oCustomer, DTOPurchaseOrder.Codis.Client)
        For Each item As DTODelivery In items
            Dim dui As New DUI.Delivery
            With dui
                .Guid = item.Guid
                .Id = item.Id
                .Fch = item.Fch
                .Eur = item.Import.Eur
                .FileUrl = BLLDelivery.Url(item)
            End With
            retval.Add(dui)
        Next
        Return retval
    End Function

End Class
