Public Class Brand
    Property Guid As String
    Property Nom As String

    Property Categories As List(Of Category)
End Class

Public Class Category
    Property Guid As String
    Property Nom As String

    Property Skus As List(Of Sku)
End Class
Public Class Sku
    Property Guid As String
    Property Nom As String

    Property Stock As Integer
    Property Moq As Integer
    Property Cost As Decimal
    Property RRPP As Decimal
End Class
