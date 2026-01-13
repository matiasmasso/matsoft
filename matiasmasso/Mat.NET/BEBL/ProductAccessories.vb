Public Class ProductAccessories

    Shared Function Exist(oProduct As DTOProduct, Optional IncludeObsoletos As Boolean = False) As Boolean
        Return ProductAccessoriesLoader.Exist(oProduct, IncludeObsoletos)
    End Function

    Shared Function Accessories(oSrc As DTOProduct, Optional IncludeObsoletos As Boolean = False, Optional AllowInheritance As Boolean = True) As List(Of DTOProductSku)
        Dim retval As List(Of DTOProductSku) = ProductLoader.Relateds(oSrc, DTOProductSku.Relateds.Accessories, IncludeObsoletos, AllowInheritance)
        Return retval
    End Function

    Shared Function Spares(oSrc As DTOProduct, Optional IncludeObsoletos As Boolean = False, Optional AllowInheritance As Boolean = True) As List(Of DTOProductSku)
        Dim retval As List(Of DTOProductSku) = ProductLoader.Relateds(oSrc, DTOProductSku.Relateds.Spares, IncludeObsoletos, AllowInheritance)
        Return retval
    End Function

    Shared Function UpdateAccessories(oSrc As DTOProduct, oAccessories As List(Of DTOProductSku), exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ProductLoader.UpdateRelateds(oSrc, oAccessories, DTOProductSku.Relateds.Accessories, exs)
        Return retval
    End Function

    Shared Function UpdateSpares(oSrc As DTOProduct, oSpares As List(Of DTOProductSku), exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ProductLoader.UpdateRelateds(oSrc, oSpares, DTOProductSku.Relateds.Spares, exs)
        Return retval
    End Function



End Class
