Public Class DTOBasket
    Property customer As Guid
    Property customerUrl As String
    Property addressLines As List(Of String)
    Property concept As String
    Property totjunt As Boolean
    Property fchmin As String
    Property obs As String
    Property includePendingOrders As Boolean
    Property lines As List(Of DTOBasketLine)
    Property suma As Decimal
    Property sumaFormatted As String
    Property Id As Integer
    Property mailConfirmation As Boolean
    Property Catalog As DTOBasketCatalog
End Class

Public Class DTOBasketLine
    Property qty As Integer
    Property availableStock As Integer
    Property pending As Integer
    Property sku As Guid
    Property category As Guid
    Property brand As Guid
    Property url As String
    Property nom As String
    Property price As Decimal
    Property dto As Decimal
    Property Amount As Decimal
    Property priceFormatted As String
    Property amountFormatted As String

    Shared Function Factory(oItem As DTOPurchaseOrderItem) As DTOBasketLine
        Dim retval As New DTOBasketLine
        With retval
            .sku = oItem.Sku.Guid
            .category = oItem.Sku.Category.Guid
            .brand = oItem.Sku.Category.Brand.Guid
            .qty = oItem.Qty
            .nom = oItem.Sku.NomLlarg
            .price = oItem.Price.Eur
            .priceFormatted = DTOAmt.CurFormatted(oItem.Price)
            .dto = oItem.Dto
            .availableStock = Math.Min(oItem.Qty, oItem.Sku.Stock)
            .pending = oItem.Qty - .availableStock
            Dim oAmt = oItem.Amount
            .Amount = oAmt.Eur
            .amountFormatted = DTOAmt.CurFormatted(oAmt)
        End With
        Return retval
    End Function
End Class
