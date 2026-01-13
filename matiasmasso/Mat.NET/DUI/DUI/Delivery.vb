Public Class Delivery
    Inherits GuidNomEur
    Property Id As Integer
    Property Fch As String
    Property Customer As Guidnom
    Property FileUrl As String
    Property items As List(Of DeliveryOrderItem)
End Class

Public Class DeliveryOrderItem
    Inherits GuidNomEur
    Property Qty As Integer
    Property Dto As Integer
    Property Sku As Guidnom
End Class