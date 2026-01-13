Public Class Product

    Shared Function Find(oGuid As Guid) As DTOProduct
        Dim retval As DTOProduct = ProductLoader.Find(oGuid)
        Return retval
    End Function


    Shared Function Load(ByRef src As DTOProduct) As Boolean
        Dim retval As Boolean
        If TypeOf src Is DTOProductBrand Then
            Dim oBrand As DTOProductBrand = DirectCast(src, DTOProductBrand)
            retval = ProductBrandLoader.Load(oBrand)
        ElseIf TypeOf src Is DTOProductCategory Then
            Dim oCategory As DTOProductCategory = DirectCast(src, DTOProductCategory)
            retval = ProductCategoryLoader.Load(oCategory)
        ElseIf TypeOf src Is DTOProductSku Then
            Dim oSku As DTOProductSku = DirectCast(src, DTOProductSku)
            retval = ProductSkuLoader.Load(oSku)
        Else
            Select Case src.SourceCod
                Case DTOProduct.SourceCods.Brand
                    src = BEBL.ProductBrand.Find(src.Guid)
                Case DTOProduct.SourceCods.Category
                    src = BEBL.ProductCategory.Find(src.Guid)
                Case DTOProduct.SourceCods.Sku
                    src = BEBL.ProductSku.Find(src.Guid)
            End Select
            retval = True
        End If
        Return retval
    End Function

    Shared Function Nom(src As DTOProduct) As String
        Dim retval As String = ""
        If TypeOf src Is DTOProductBrand Then
            retval = DirectCast(src, DTOProductBrand).nom.Esp
        ElseIf TypeOf src Is DTOProductCategory Then
            retval = DirectCast(src, DTOProductCategory).nom.Esp
        ElseIf TypeOf src Is DTOProductSku Then
            retval = DirectCast(src, DTOProductSku).nom.Esp
        End If
        Return retval
    End Function

    Shared Function BrandCodDist(oProduct As DTOProduct) As DTOProductBrand.CodDists
        Dim retval As DTOProductBrand.CodDists
        Dim oBrand As DTOProductBrand = BEBL.Product.Brand(oProduct)
        If oBrand IsNot Nothing Then
            BEBL.ProductBrand.Load(oBrand)
            retval = oBrand.CodDist
        End If
        Return retval
    End Function

    Shared Function Brand(src As DTOProduct) As DTOProductBrand
        Dim retval As DTOProductBrand = Nothing
        If src IsNot Nothing Then
            If TypeOf src Is DTOProductBrand Then
                retval = src
            ElseIf TypeOf src Is DTOProductCategory Then
                Dim oCategory As DTOProductCategory = src
                If oCategory.Brand Is Nothing Then BEBL.ProductCategory.Load(oCategory)
                retval = oCategory.Brand
            ElseIf TypeOf src Is DTOProductSku Then
                Dim oSku As DTOProductSku = src
                If oSku.Category Is Nothing Then BEBL.ProductSku.Load(oSku)
                If oSku.Category.Brand Is Nothing Then BEBL.ProductCategory.Load(oSku.Category)
                retval = oSku.Category.Brand
            End If
        End If
        Return retval
    End Function

    Shared Function AllowUserToFraccionarInnerPack(oProduct As DTOProduct, oUser As DTOUser) As Boolean
        Dim retVal As Boolean = ProductLoader.AllowUserToFraccionarInnerPack(oProduct, oUser)
        Return retVal
    End Function

    Shared Function FraccionarTemporalment(exs As List(Of Exception), oProduct As DTOProduct, oUser As DTOUser) As Boolean
        Dim retVal As Boolean = ProductLoader.FraccionarTemporalment(oProduct, oUser, exs)
        Return retVal
    End Function

    Shared Function RelatedsExist(oCod As DTOProduct.Relateds, oProduct As DTOProduct) As Boolean
        Return ProductLoader.RelatedsExist(oCod, oProduct)
    End Function

    Shared Function Relateds(cod As DTOProduct.Relateds, oTarget As DTOProduct, Optional oMgz As DTOMgz = Nothing, Optional IncludeObsoletos As Boolean = False, Optional AllowInheritance As Boolean = True) As List(Of DTOProductSku)
        If oMgz Is Nothing Then
            'per la web amb excerpts
            Return ProductLoader.Relateds(oTarget, cod, IncludeObsoletos, AllowInheritance)
        Else
            'amb stocks per el Mat.Net
            Return BEBL.ProductSkus.Relateds(oTarget, cod, oMgz)
        End If
    End Function

    Shared Function UpdateRelateds(exs As List(Of Exception), cod As DTOProduct.Relateds, oTarget As DTOProduct, oSkus As List(Of DTOProductSku)) As Boolean
        Dim retval As Boolean
        If ProductLoader.UpdateRelateds(oTarget, oSkus, cod, exs) Then
            BEBL.ServerCache.NotifyUpdate(Models.ServerCache.Tables.ProductSpares)
            retval = True
        End If
        Return retval
    End Function
End Class


Public Class Products

    Shared Function FromNom(oBrand As DTOProductBrand, sNom As String) As List(Of DTOProduct)
        Return ProductsLoader.FromNom(oBrand, sNom)
    End Function

    Shared Function ForSitemap(oEmp As DTOEmp) As DTOProductBrand.Collection
        Return ProductsLoader.ForSitemap(oEmp)
    End Function

    Shared Function FromCnap(sKeyword As String) As List(Of DTOProduct)
        Dim retval As List(Of DTOProduct) = ProductsLoader.FromCnap(sKeyword)
        Return retval
    End Function
End Class