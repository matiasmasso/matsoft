Public Class PurchaseOrder
    Inherits GuidNomEur
    Property Id As Integer
    Property Fch As String
    Property Customer As Guidnom
    Property Obs As String
    Property TotJunt As Boolean
    Property FchMin As Date

    Property Promo As Promo

    Property ThumbnailUrl As String
    Property FileUrl As String

    Property items As List(Of PurchaseOrderItem)

    Property user As Guidnom
    Property validationErrors As List(Of String)



End Class

Public Class PurchaseOrderItem
    Inherits GuidNomEur
    Property Qty As Integer
    Property Dto As Integer
    Property Sku As Sku
End Class