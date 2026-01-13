Public Class PriceListItemCustomer
    Shared Function Search(oSku As DTOProductSku, Optional DtFch As Date = Nothing) As DTOPricelistItemCustomer
        Dim retval As DTOPricelistItemCustomer = PriceListItemCustomerLoader.Search(oSku, DtFch)
        Return retval
    End Function

    Shared Function Find(oPriceList As DTOPricelistCustomer, oSku As DTOProductSku) As DTOPricelistItemCustomer
        Dim retval As DTOPricelistItemCustomer = PriceListItemCustomerLoader.Find(oPriceList, oSku)
        Return retval
    End Function

    Shared Function Update(oTemplate As DTOPricelistItemCustomer, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = PriceListItemCustomerLoader.Update(oTemplate, exs)
        Return retval
    End Function

    Shared Function Delete(oTemplate As DTOPricelistItemCustomer, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = PriceListItemCustomerLoader.Delete(oTemplate, exs)
        Return retval
    End Function

    Shared Function GetCustomerCost(oSku As DTOProductSku, oDtos As List(Of DTOCustomerTarifaDto), Optional DtFch As Date = Nothing) As DTOAmt
        Dim retval As DTOAmt = Nothing

        If oSku.RRPP IsNot Nothing Then
            If DtFch = Nothing Then DtFch = DTO.GlobalVariables.Today()
            Dim DcDto As Decimal = DTOCustomerTarifaDto.ProductDto(oDtos, oSku)
            retval = oSku.rrpp.deductPercent(DcDto)
        End If
        Return retval
    End Function

End Class
Public Class PriceListItemsCustomer

    Shared Function All(oProductSku As DTOProductSku) As List(Of DTOPricelistItemCustomer)
        Return PriceListItemsCustomerLoader.All(oProductSku)
    End Function

    Shared Function Active(oCustomer As DTOCustomer, Optional DtFch As Date = Nothing) As List(Of DTOPricelistItemCustomer)
        If DtFch = Nothing Then DtFch = DTO.GlobalVariables.Today()
        Return PriceListItemsCustomerLoader.Active(oCustomer, DtFch)
    End Function

    Shared Function Vigent(Optional DtFch As Date = Nothing) As List(Of DTOPricelistItemCustomer)
        Return PriceListItemsCustomerLoader.Vigent(DtFch)
    End Function

End Class


