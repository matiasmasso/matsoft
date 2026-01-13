Public Class PriceListCustomer

    Shared Function Find(oGuid As Guid) As DTOPricelistCustomer
        Return PriceListCustomerLoader.Find(oGuid)
    End Function

    Shared Function Load(ByRef value As DTOPricelistCustomer, Optional ForceReload As Boolean = False) As Boolean
        Return PriceListCustomerLoader.Load(value, ForceReload)
    End Function

    Shared Function Update(value As DTOPricelistCustomer, ByRef exs As List(Of Exception)) As Boolean
        Return PriceListCustomerLoader.Update(value, exs)
    End Function

    Shared Function Delete(value As DTOPricelistCustomer, ByRef exs As List(Of Exception)) As Boolean
        Return PriceListCustomerLoader.Delete(value, exs)
    End Function

    Shared Function GetCustomerCost(oSku As DTOProductSku, oCustomCosts As List(Of DTOPricelistItemCustomer), oDtos As List(Of DTOCustomerTarifaDto)) As DTOAmt
        Dim retval As DTOAmt = Nothing

        'prova si existeix tarifa personalitzada d'aquest client per aquest article
        Dim oCustomCost As DTOPricelistItemCustomer = oCustomCosts.Find(Function(x) x.Sku.Equals(oSku))
        If oCustomCost Is Nothing Then
            'si no n'hi ha, busca a la tarifa general i dedueix el descompte que pugui tenir

            Dim DcCost As Decimal

            If oSku.RRPP IsNot Nothing Then
                Dim RRPP As Decimal = oSku.RRPP.Eur
                oSku.DtoSobreRRPP = Nothing
                Dim oDto As DTOCustomerTarifaDto = GetCustomerDto(oDtos, oSku)
                If oDto Is Nothing Then
                    DcCost = RRPP
                Else
                    oSku.DtoSobreRRPP = oDto
                    DcCost = Math.Round(RRPP * (100 - oSku.DtoSobreRRPP.Dto) / 100, 2, MidpointRounding.AwayFromZero)
                End If
            End If

            retval = DTOAmt.Factory(DcCost)
        Else
            retval = oCustomCost.Retail
        End If

        Return retval
    End Function

    Shared Function GetCustomerCost(oCustomer As DTOCustomer, oSku As DTOProductSku, oDtos As List(Of DTOCustomerTarifaDto), Optional DtFch As Date = Nothing) As DTOAmt
        If DtFch = Nothing Then DtFch = DTO.GlobalVariables.Today()
        'prova si existeix tarifa personalitzada d'aquest client per aquest article
        Dim retval As DTOAmt = PriceListItemCustomerLoader.GetCustomerCost(oCustomer, oSku, DtFch)

        'si no n'hi ha, busca a la tarifa general i dedueix el descompte que pugui tenir per marca
        If retval Is Nothing Then
            Dim RRPP As Decimal = oSku.RRPP.Eur
            Dim DtoSobreRRPP As Decimal = 0
            Dim oDto As DTOCustomerTarifaDto = oDtos.Find(Function(x) oSku.Equals(x.Product) Or oSku.Category.Equals(x.Product) Or oSku.Category.Brand.Equals(x.Product) Or x.Product Is Nothing)
            If oDto IsNot Nothing Then DtoSobreRRPP = oDto.Dto
            Dim DcCost As Decimal = Math.Round(RRPP * (100 - DtoSobreRRPP) / 100, 2, MidpointRounding.AwayFromZero)
            retval = DTOAmt.Factory(DcCost)
        End If
        Return retval
    End Function

    Shared Function GetCustomerCost(oSku As DTOProductSku, oDtos As List(Of DTOCustomerTarifaDto), Optional DtFch As Date = Nothing) As DTOAmt
        Dim retval As DTOAmt = Nothing

        If oSku.RRPP IsNot Nothing Then
            If DtFch = Nothing Then DtFch = DTO.GlobalVariables.Today()
            Dim DcDto As Decimal = DTOCustomerTarifaDto.ProductDto(oDtos, oSku) ' oSku.Category.Brand)
            Dim DcCost As Decimal = Math.Round(oSku.RRPP.Eur * (100 - DcDto) / 100, 2, MidpointRounding.AwayFromZero)
            retval = DTOAmt.Factory(DcCost)
        End If
        Return retval
    End Function

    Shared Function GetCustomerDto(oDtos As List(Of DTOCustomerTarifaDto), oSku As DTOProductSku) As DTOCustomerTarifaDto
        Dim retval As DTOCustomerTarifaDto = oDtos.Find(Function(x) oSku.Equals(x.Product))
        If retval Is Nothing Then
            retval = oDtos.Find(Function(x) oSku.Category.Equals(x.Product))
            If retval Is Nothing Then
                retval = oDtos.Find(Function(x) oSku.Category.Brand.Equals(x.Product))
                If retval Is Nothing Then
                    retval = oDtos.Find(Function(x) x.Product Is Nothing)
                End If
            End If
        End If
        Return retval
    End Function


End Class


Public Class PriceListsCustomer
    Shared Function All(Optional oCustomer As DTOCustomer = Nothing) As List(Of DTOPricelistCustomer)
        Dim retval As List(Of DTOPricelistCustomer) = PriceListsCustomerLoader.All(oCustomer)
        Return retval
    End Function


    Shared Function Delete(values As List(Of DTOPricelistCustomer), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = True
        For Each item As DTOPricelistCustomer In values
            If Not PriceListCustomerLoader.Delete(item, exs) Then retval = False
        Next
        Return retval
    End Function

End Class