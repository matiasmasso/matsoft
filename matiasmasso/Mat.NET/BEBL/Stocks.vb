Public Class Stock

    Shared Function Available(oSku As DTOProductSku, oMgz As DTOMgz) As Integer
        Dim oStock As DTOStock = StockLoader.Find(oSku, oMgz)
        Dim retval As Integer = oStock.UnitsInStock - oStock.UnitsInClients
        If retval < 0 Then retval = 0
        Return retval
    End Function

End Class

Public Class Stocks

    Shared Function All(oMgz As DTOMgz, Optional oCategory As DTOProductCategory = Nothing) As List(Of DTOStock)
        Return StocksLoader.All(oMgz, oCategory)
    End Function

    Shared Function All(oUser As DTOUser) As List(Of DTOCompactStockOnly) '---------------------------- integracio clients externs
        Dim exs As New List(Of Exception)
        Dim retval As New List(Of DTOCompactStockOnly)
        BEBL.Emp.Load(oUser.Emp)
        Dim oSkus As List(Of DTOProductSku) = StocksLoader.All(oUser, oUser.Emp.Mgz)
        For Each oSku As DTOProductSku In oSkus
            Dim item As New DTOCompactStockOnly
            With item
                .Ref = oSku.Id
                If oSku.Ean13 IsNot Nothing Then
                    .Ean = oSku.Ean13.Value
                Else
                    .Ean = ""
                End If
                .Stock = oSku.stockAvailable()
            End With
            retval.Add(item)
        Next
        Return retval
    End Function
End Class
