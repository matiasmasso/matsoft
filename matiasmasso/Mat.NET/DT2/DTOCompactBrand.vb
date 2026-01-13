Public Class DTOCompactCataleg
    Property brands As List(Of DTOCompactBrand)
    Property BrandSpriteHash As String
    Property skus As List(Of DTOCompactSku)

    Shared Function CompactBrands(oBrands As List(Of DTOProductBrand)) As List(Of DTOCompactBrand)
        Dim retval As New List(Of DTOCompactBrand)
        For Each oBrand As DTOProductBrand In oBrands

            Dim oCompactBrand = DTOCompactBrand.Factory(oBrand)
            retval.Add(oCompactBrand)

            For Each oCategory As DTOProductCategory In oBrand.Categories
                Dim oCompactCategory = DTOCompactCategory.Factory(oCategory)
                oCompactBrand.categories.Add(oCompactCategory)

                For Each oSku As DTOProductSku In oCategory.Skus
                    Dim oCompactSku = DTOCompactSku.Factory(oSku)
                    oCompactCategory.skus.Add(oCompactSku)
                Next
            Next

        Next
        Return retval
    End Function
End Class

Public Class DTOCompactBrand
    Property guid As Guid
    Property nom As String
    Property categories As List(Of DTOCompactCategory)

    Shared Function Factory(oBrand As DTOProductBrand) As DTOCompactBrand
        Return DTOCompactBrand.Factory(oBrand.Guid, oBrand.Nom)
    End Function

    Shared Function Factory(oGuid As Guid, Optional sNom As String = "") As DTOCompactBrand
        Dim retval As New DTOCompactBrand
        With retval
            .guid = oGuid
            .nom = sNom
            .categories = New List(Of DTOCompactCategory)
        End With
        Return retval
    End Function
End Class

Public Class DTOCompactCategory
    Property guid As Guid
    Property brand As DTOCompactBrand
    Property nom As String
    Property skus As List(Of DTOCompactSku)

    Shared Function Factory(oCategory As DTOProductCategory) As DTOCompactCategory
        Return DTOCompactCategory.Factory(oCategory.Guid, oCategory.Nom)
    End Function

    Shared Function Factory(oGuid As Guid, Optional sNom As String = "") As DTOCompactCategory
        Dim retval As New DTOCompactCategory
        With retval
            .guid = oGuid
            .nom = sNom
            .skus = New List(Of DTOCompactSku)
        End With
        Return retval
    End Function
End Class

Public Class DTOCompactSku
    Property guid As Guid
    Property category As DTOCompactCategory
    Property nom As String
    Property retail As Decimal
    Property obsoleto As Boolean
    Property thumbnailUrl As String

    Shared Function Factory(oSku As DTOProductSku) As DTOCompactSku
        Return DTOCompactSku.Factory(oSku.Guid, oSku.Nom)
    End Function

    Shared Function Factory(oGuid As Guid, Optional sNom As String = "") As DTOCompactSku
        Dim retval As New DTOCompactSku
        With retval
            .guid = oGuid
            .nom = sNom
        End With
        Return retval
    End Function
End Class
