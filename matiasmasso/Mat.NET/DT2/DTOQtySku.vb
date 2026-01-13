Public Class DTOQtySku
    Property Qty As Integer
    Property Sku As DTOProductSku = Nothing

    Public Sub New(iQty As Integer, oSku As DTOProductSku)
        MyBase.New()
        _Qty = iQty
        _Sku = oSku
    End Sub

End Class
