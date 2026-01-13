Public Class ProductCatalog

    Shared Function Brands(oEmp As DTOEmp) As List(Of DTOProductBrand)
        Dim retval As List(Of DTOProductBrand) = ProductCatalogLoader.Brands(oEmp)
        Return retval
    End Function

    Shared Function Factory(oEmp As DTOEmp, Optional oContact As DTOContact = Nothing) As DTOProductCatalog
        Dim retval As DTOProductCatalog = ProductCatalogLoader.Factory(oEmp, oContact)
        Return retval
    End Function

    Shared Function Factory(oEmp As DTOEmp, oIncidencia As DTOIncidencia) As DTOProductCatalog
        Dim retval As DTOProductCatalog = Nothing
        Select Case oIncidencia.Procedencia
            Case DTOIncidencia.Procedencias.MyShop, DTOIncidencia.Procedencias.Expo
                retval = ProductCatalogLoader.Factory(oIncidencia.Customer)
            Case Else
                retval = ProductCatalogLoader.Factory(oEmp)
        End Select
        Return retval
    End Function

    Shared Function CompactTree(oEmp As DTOEmp, Optional includeObsolets As Boolean = False) As List(Of DTOCompactBrand)
        Return ProductCatalogLoader.CompactTree(oEmp, includeObsolets)
    End Function

    Shared Function CustomerBasicTree(oCustomer As DTOCustomer, oLang As DTOLang) As DTOBasicCatalog
        Return ProductCatalogLoader.CustomerBasicTree(oCustomer, oLang)
    End Function

    Shared Function CompactBrandCategories(user As DTOUser) As List(Of DTOCompactNode)
        Return ProductCatalogLoader.BrandCategories(user)
    End Function

    Shared Function CompactSkus(oUser As DTOUser, oCategory As DTOProductCategory, oMgz As DTOMgz) As List(Of DTOCompactGuidNomQtyEur)
        Dim retval As List(Of DTOCompactGuidNomQtyEur) = ProductCatalogLoader.CompactSkus(oUser, oCategory, oMgz)
        Return retval
    End Function

    Shared Function Refs() As List(Of DTOProductSku)
        Dim retval = ProductCatalogLoader.Refs()
        Return retval
    End Function

End Class