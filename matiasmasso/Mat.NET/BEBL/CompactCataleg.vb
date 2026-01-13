Public Class CompactCataleg

    Shared Function Factory(oEmp As DTOEmp, oUser As DTOUser) As DTOCompactCataleg
        Dim oBrands As List(Of DTOProductBrand) = BEBL.ProductBrands.All(oEmp, oUser)
        Dim retval As New DTOCompactCataleg
        With retval
            .brands = DTOCompactCataleg.CompactBrands(oBrands)
        End With
        Return retval
    End Function

End Class
