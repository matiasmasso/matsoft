Public Class ProductCatalog
    Inherits _FeblBase


    Shared Async Function Brands(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of List(Of DTOProductBrand))
        Return Await Api.Fetch(Of List(Of DTOProductBrand))(exs, "ProductCatalog/Brands", oEmp.Id)
    End Function

    Shared Async Function Factory(exs As List(Of Exception), oEmp As DTOEmp, Optional oContact As DTOContact = Nothing) As Task(Of DTOProductCatalog)
        Return Await Api.Fetch(Of DTOProductCatalog)(exs, "ProductCatalog/Factory", oEmp.Id, OpcionalGuid(oContact))
    End Function

    Shared Async Function Factory(exs As List(Of Exception), oEmp As DTOEmp, oIncidencia As DTOIncidencia) As Task(Of DTOProductCatalog)
        Return Await Api.Fetch(Of DTOProductCatalog)(exs, "ProductCatalog/FromIncidencia", oEmp.Id, oIncidencia.Guid.ToString())
    End Function

    Shared Async Function CompactTree(exs As List(Of Exception), oEmp As DTOEmp, Optional IncludeObsoletos As Boolean = False) As Task(Of List(Of DTOProductBrand))
        Dim retval As New List(Of DTOProductBrand)
        Dim oCompactBrands = Await Api.Fetch(Of DTOCompactBrand.Collection)(exs, "ProductCatalog/CompactTree", oEmp.Id, If(IncludeObsoletos, 1, 0))
        If oCompactBrands IsNot Nothing Then
            retval = oCompactBrands.ProductBrands()
            For Each oBrand In retval
                For Each oCategory In oBrand.Categories
                    oCategory.Brand = oBrand
                    For Each oSku In oCategory.Skus
                        oSku.Category = oCategory
                    Next
                Next
            Next

        End If
        Return retval
    End Function

    Shared Async Function CompactBrandCategories(exs As List(Of Exception), user As DTOUser) As Task(Of List(Of DTOCompactNode))
        Return Await Api.Fetch(Of List(Of DTOCompactNode))(exs, "ProductCatalog/CompactBrandCategories", user.Guid.ToString())
    End Function

    Shared Async Function CompactSkus(exs As List(Of Exception), oUser As DTOUser, oCategory As DTOProductCategory) As Task(Of List(Of DTOCompactGuidNomQtyEur))
        Return Await Api.Fetch(Of List(Of DTOCompactGuidNomQtyEur))(exs, "ProductCatalog/CompactSkus", oUser.Guid.ToString, oCategory.Guid.ToString())
    End Function



    Shared Async Function Refs(exs As List(Of Exception)) As Task(Of List(Of DTOProductSku))
        Return Await Api.Fetch(Of List(Of DTOProductSku))(exs, "ProductCatalog/refs")
    End Function


End Class
