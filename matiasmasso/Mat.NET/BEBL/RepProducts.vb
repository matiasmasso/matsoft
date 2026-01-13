Public Class RepProduct
    Shared Function Find(oGuid As Guid) As DTORepProduct
        Dim retval As DTORepProduct = RepProductLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function GetRepProduct(oArea As DTOArea, oProduct As DTOProduct, oChannel As DTODistributionChannel, DtFch As Date) As DTORepProduct
        Dim retval As DTORepProduct = RepProductLoader.GetRepProduct(oArea, oProduct, oChannel, DtFch)
        Return retval
    End Function

    Shared Function Load(ByRef oRepProduct As DTORepProduct) As Boolean
        Dim retval As Boolean = RepProductLoader.Load(oRepProduct)
        Return retval
    End Function

    Shared Function Update(oRepProduct As DTORepProduct, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = RepProductLoader.Update(oRepProduct, exs)
        Return retval
    End Function

    Shared Function Delete(oRepProduct As DTORepProduct, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = RepProductLoader.Delete(oRepProduct, exs)
        Return retval
    End Function

End Class

Public Class RepProducts

    Shared Function All(oEmp As DTOEmp, oRep As DTORep, Optional IncludeObsoletos As Boolean = False) As List(Of DTORepProduct)
        Dim retval As List(Of DTORepProduct) = RepProductsLoader.All(oEmp, oRep, IncludeObsoletos)
        Return retval
    End Function

    Shared Function All(oEmp As DTOEmp, Optional IncludeObsoletos As Boolean = False) As List(Of DTORepProduct)
        Dim retval As List(Of DTORepProduct) = RepProductsLoader.All(oEmp, , IncludeObsoletos)
        Return retval
    End Function

    Shared Function All(oRep As DTORep, Optional IncludeObsoletos As Boolean = False) As List(Of DTORepProduct)
        Dim retval As List(Of DTORepProduct) = RepProductsLoader.All(, oRep, IncludeObsoletos)
        Return retval
    End Function

    Shared Function All(oProduct As DTOProduct, Optional IncludeObsoletos As Boolean = False) As List(Of DTORepProduct)
        Dim retval As List(Of DTORepProduct) = RepProductsLoader.All(oProduct, IncludeObsoletos)
        Return retval
    End Function

    Shared Function All(oChannel As DTODistributionChannel, oArea As DTOArea, oProduct As DTOProduct, DtFch As Date) As List(Of DTORepProduct)
        Dim retval As List(Of DTORepProduct) = RepProductsLoader.All(oChannel, oArea, oProduct, DtFch)
        Return retval
    End Function

    Shared Function All(oArea As DTOArea, Optional IncludeObsoletos As Boolean = False) As List(Of DTORepProduct)
        Dim retval As List(Of DTORepProduct) = RepProductsLoader.All(oArea, IncludeObsoletos)
        Return retval
    End Function

    Shared Function Catalogue(oRep As DTORep) As List(Of DTOProductSku)
        Dim retval As List(Of DTOProductSku) = RepProductsLoader.Catalogue(oRep)
        Return retval
    End Function


    Shared Function CatalogueTree(oRep As DTORep) As List(Of Object)
        Dim exs As New List(Of Exception)
        Dim oCatalogue As List(Of DTOProductSku) = BEBL.RepProducts.Catalogue(oRep)
        Dim oBrands As List(Of DTOProductBrand) = oCatalogue.Select(Function(x) x.Category.Brand).Distinct.ToList

        Dim items As New List(Of Object)
        For Each oBrand As DTOProductBrand In oBrands
            Dim duiBrand As Object = New With {.guid = oBrand.Guid, .nom = oBrand.Nom, .categories = New List(Of Object)}
            items.Add(duiBrand)
            For Each oCategory As DTOProductCategory In oBrand.Categories
                Dim duiCategory As Object = New With {.guid = oCategory.Guid, .nom = oCategory.Nom, .skus = New List(Of Object)}
                duiBrand.categories.add(duiCategory)
                For Each oSku As DTOProductSku In oCategory.Skus
                    Dim duiSku As Object = New With {.guid = oSku.Guid, .nom = oSku.Nom}
                    duiCategory.skus.Add(duiSku)
                Next
            Next
        Next

        Return items
    End Function

    Shared Function Customers(oRepUser As DTOUser) As List(Of DTOGuidNode)
        Dim retval As List(Of DTOGuidNode) = RepProductsLoader.Customers(oRepUser)
        Return retval
    End Function



    Shared Function Update(oRepProducts As List(Of DTORepProduct), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = RepProductsLoader.Update(oRepProducts, exs)
        Return retval
    End Function

    Shared Function Delete(oRepProducts As List(Of DTORepProduct), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = RepProductsLoader.Delete(oRepProducts, exs)
        Return retval
    End Function

    Shared Function RepsxAreaWithMobiles(Optional BlIncludeObsolets As Boolean = False) As List(Of DTORepProduct)
        Dim retval As List(Of DTORepProduct) = RepProductsLoader.RepsxAreaWithMobiles(BlIncludeObsolets)
        Return retval
    End Function



    Shared Function Match(oEmp As DTOEmp, oRepProducts As List(Of DTORepProduct), oChannel As DTODistributionChannel, oZip As DTOZip, oSku As DTOProductSku, DtFch As Date) As List(Of DTORepProduct)
        Dim retval As New List(Of DTORepProduct)

        'filtra per data
        Dim Step1 As List(Of DTORepProduct) = oRepProducts.FindAll(Function(x) x.FchFrom <= DtFch And (x.FchTo = Nothing Or x.FchTo >= DtFch)).ToList
        'filtra per zona
        Dim Step2 As List(Of DTORepProduct) = Step1.FindAll(Function(x) (x.Area.Guid.Equals(oZip.Guid) Or x.Area.Guid.Equals(oZip.Location.Guid) Or x.Area.Guid.Equals(oZip.Location.Zona.Guid) Or x.Area.Guid.Equals(oZip.Location.Zona.Country.Guid))).ToList
        'filtra per producte
        Dim Step3 As List(Of DTORepProduct) = Step2.FindAll(Function(x) (x.Product.Guid.Equals(oSku.Guid) Or x.Product.Guid.Equals(oSku.Category.Guid) Or x.Product.Guid.Equals(oSku.Category.Brand.Guid))).ToList

        'filtra per canal de distribució o surt si el destinatari no está assignat a cap canal
        Dim Step4 As New List(Of DTORepProduct)
        If oChannel IsNot Nothing Then
            Step4 = Step3.FindAll(Function(x) x.DistributionChannel.Guid.Equals(oChannel.Guid)).ToList

            'identifica els exclosos
            Dim oRepsToRemove As New List(Of DTORep)
            For Each item As DTORepProduct In Step4.FindAll(Function(x) x.Cod = DTORepProduct.Cods.Excluded)
                oRepsToRemove.Add(item.Rep)
            Next

            'llista els inclosos que el seu rep no ha estat exclos
            For Each item As DTORepProduct In Step4.FindAll(Function(x) x.Cod = DTORepProduct.Cods.Included)
                If Not oRepsToRemove.Exists(Function(x) x.Guid.Equals(item.Rep.Guid)) Then
                    retval.Add(item)
                End If
            Next
        End If



        Return retval
    End Function

End Class
