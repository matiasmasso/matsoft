Public Class Invoice
    Inherits GuidNomEur
    Property Id As Integer
    Property Fch As String
    Property Customer As Guidnom
    Property FileUrl As String
    Property ThumbnailUrl As String
    Property items As List(Of DeliveryOrderItem)
End Class
