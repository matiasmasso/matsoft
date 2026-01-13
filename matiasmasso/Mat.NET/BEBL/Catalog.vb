Public Class Catalog

    Shared Function Factory(oEmp As DTOEmp, Optional oContact As DTOContact = Nothing) As DTOProductCatalog
        Dim retval As DTOProductCatalog = ProductCatalogLoader.Factory(oEmp, oContact)
        Return retval
    End Function

    Shared Function Fetch(oUser As DTOUser) As Models.CatalogModel
        Dim retval = CatalogLoader.Fetch(oUser)
        Return retval
    End Function

End Class
