Public Class FourMoms
    Shared Function SalePoints(DtFch As Date) As List(Of DTOProductAreaQty)
        Dim oBrand As DTOProductBrand = DTOProductBrand.Wellknown(DTOProductBrand.Wellknowns.FourMoms)
        Dim retval As List(Of DTOProductAreaQty) = FourMomsLoader.SalePoints(oBrand, DtFch)
        Return retval
    End Function

    Shared Function Sales(DtFch As Date) As List(Of DTOProductAreaQty)
        Dim oBrand As DTOProductBrand = DTOProductBrand.Wellknown(DTOProductBrand.Wellknowns.FourMoms)
        Dim retval As List(Of DTOProductAreaQty) = FourMomsLoader.Sales(oBrand, DtFch)
        Return retval
    End Function

    Shared Function Stocks(oMgz As DTOMgz, DtFch As Date) As List(Of DTOProductAreaQty)
        Dim oBrand As DTOProductBrand = DTOProductBrand.Wellknown(DTOProductBrand.Wellknowns.FourMoms)
        Dim retval As List(Of DTOProductAreaQty) = FourMomsLoader.Stocks(oBrand, oMgz, DtFch)
        Return retval
    End Function
End Class
