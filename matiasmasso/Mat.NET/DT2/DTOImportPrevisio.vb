Public Class DTOImportPrevisio
    Inherits DTOBaseGuid
    Property Importacio As DTOImportacio
    Property NumComandaProveidor As String
    Property Lin As Integer
    Property SkuRef As String
    Property Sku As DTOProductSku
    Property SkuNom As String
    Property Qty As Integer
    Property PurchaseOrderItem As DTOPurchaseOrderItem

    Property Errors As List(Of ValidationErrors)

    Public Enum ValidationErrors
        SkuNotFound
        OrderNotFound
    End Enum

    Public Enum ValidationSolutions
        SelectSku
        SelectOrder
    End Enum

    Public Sub New()
        MyBase.New()
        _Errors = New List(Of ValidationErrors)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _Errors = New List(Of ValidationErrors)
    End Sub

End Class
