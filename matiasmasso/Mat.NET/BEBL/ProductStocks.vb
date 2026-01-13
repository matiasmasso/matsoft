Public Class ProductStocks

    Shared Function FromUserOrCustomer(oEmp As DTOEmp, oValue As DTOBaseGuid, oMgz As DTOMgz) As List(Of DTOProductSku)
        Return ProductStocksLoader.FromUserOrCustomer(oEmp, oValue, oMgz)
    End Function

    Shared Function Custom(oEmp As DTOEmp, oCustomer As DTOCustomer) As List(Of DTOCustomerProduct)
        Dim retval As List(Of DTOCustomerProduct) = ProductStocksLoader.Custom(oCustomer, oEmp.Mgz)
        Return retval
    End Function

    Shared Function Skus(oMgz As DTOMgz) As List(Of DTOProductSku)
        Dim retval As List(Of DTOProductSku) = ProductStocksLoader.Skus(oMgz)
        Return retval
    End Function

    Shared Function CostAndInventory(oEmp As DTOEmp, Optional oCategory As DTOProductCategory = Nothing) As List(Of DTOProductSku)
        Dim retval As List(Of DTOProductSku) = ProductStocksLoader.CostAndInventory(oEmp, oEmp.Mgz, oCategory)
        Return retval
    End Function

End Class
