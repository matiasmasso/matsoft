Public Class StocksController

    Inherits _BaseController

    <HttpPost>
    <Route("api/stocks")>
    Public Function Stocks(user As DUI.User) As List(Of DUI.Stock)
        Dim retval As New List(Of DUI.Stock)
        Dim oUser As DTOUser = BLLUser.Find(user.Guid)
        If oUser IsNot Nothing Then
            Dim exs As New List(Of Exception)
            BLLWebAppLog.Log(oUser, DTOWebAppLog.OpIds.Stocks, exs)

            Dim oSkus As List(Of DTOProductSku) = BLLStocks.All(oUser)
            For Each oSku As DTOProductSku In oSkus
                Dim dui As New DUI.Stock
                With dui
                    .Ref = oSku.Id
                    .Ean = oSku.Ean13.Value
                    .Stock = BLLProductSku.StockAvailable(oSku)
                End With
                retval.Add(dui)
            Next
        End If

        Return retval
    End Function
End Class
