Public Class DTOProductSkuQtyEur
    Inherits DTOProductSku

    Property Qty As Integer
    Property Amt As DTOAmt

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub
End Class
