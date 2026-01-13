Public Class DTOImportValidacio
    Inherits DTOBaseGuid

    Property Lin As Integer
    Property Sku As DTOProductSku
    Property Qty As Integer
    Property Cfm As Integer

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub
End Class
