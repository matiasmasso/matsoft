Public Class Product
    Inherits Guidnom3
End Class

Public Class Brand
    Inherits Product
    Property Categories As List(Of Category)

End Class

Public Class Category
    Inherits Product
    Property Skus As List(Of Sku)
    Property Brand As Brand
End Class

Public Class Sku
    Inherits Product
    Property Id As Integer
    Property Dsc As String
    Property Price As Decimal
    Property RRPP As Decimal
    Property Stock As Integer
    Property Dto As Decimal
    Property Moq As Integer
    Property Category As Category

End Class


Public Class VwProductNom
    ' Inherits Guidnom
    Inherits Guidnom3
End Class

Public Class Brand2
    Inherits VwProductNom
    Property categories As List(Of Category2)

End Class

Public Class Category2
    Inherits VwProductNom
    Property skus As List(Of Sku2)
    Property brand As Brand2
End Class

Public Class Sku2
    Inherits VwProductNom
    Property price As Decimal
    Property retail As Decimal
    Property stock As Integer
    Property dto As Decimal
    Property moq As Integer
    Property category As Category2

End Class
