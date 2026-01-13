Public Class ProductCategory

    Shared Function Find(oGuid As Guid) As DTOProductCategory
        Dim retval As DTOProductCategory = ProductCategoryLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function FromNom(oBrand As DTOProductBrand, sNom As String) As DTOProductCategory
        Dim retval As DTOProductCategory = ProductCategoryLoader.FromNom(oBrand, sNom)
        Return retval
    End Function

    Shared Function Thumbnail(oCategory As DTOProductCategory) As Byte() 'To Deprecate
        Dim retval As Byte() = ProductCategoryLoader.Thumbnail(oCategory)
        Return retval
    End Function

    Shared Function Image(oGuid) As ImageMime
        Return ProductCategoryLoader.Image(oGuid)
    End Function

    Shared Function Load(ByRef oCategory As DTOProductCategory) As Boolean
        Dim retval As Boolean = ProductCategoryLoader.Load(oCategory)
        Return retval
    End Function

    Shared Function Update(oProductCategory As DTOProductCategory, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ProductCategoryLoader.Update(oProductCategory, exs)
        BEBL.ServerCache.NotifyUpdate(Models.ServerCache.Tables.Categories)
        BEBL.ServerCache.NotifyUpdate(Models.ServerCache.Tables.ProductUrls)
        Return retval
    End Function

    Shared Function Delete(oProductCategory As DTOProductCategory, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ProductCategoryLoader.Delete(oProductCategory, exs)
        BEBL.ServerCache.NotifyUpdate(Models.ServerCache.Tables.Categories)
        BEBL.ServerCache.NotifyUpdate(Models.ServerCache.Tables.ProductUrls)
        Return retval
    End Function


    Shared Function SkuColorsSprite(oCategory As DTOProductCategory, width As Integer, height As Integer) As Byte()
        'used on SkuColors website plugin
        Dim retval = ServerCache.Image(Models.ImageCache.Cods.CategorySkuColorsSprite, oCategory.Guid)
        If retval Is Nothing Then
            Dim oSkuColorImages = ProductSkusLoader.SkuColorImages(oCategory, IncludeObsoletos:=False)
            retval = LegacyHelper.SpriteBuilder.Factory(oSkuColorImages, width, height)
            ServerCache.AddImage(Models.ImageCache.Cods.CategorySkuColorsSprite, oCategory.Guid, retval)
        End If
        Return retval
    End Function

    Shared Function SortSkus(exs As List(Of Exception), oCategoryGuid As Guid, oDict As Dictionary(Of Guid, Integer))
        Dim retval As Boolean = ProductCategoryLoader.SortSkus(exs, oCategoryGuid, oDict)
        If retval Then BEBL.ServerCache.NotifyUpdate(Models.ServerCache.Tables.Skus)
        Return retval
    End Function
End Class

Public Class ProductCategories
    Shared Function All(oEmp As DTOEmp) As List(Of DTOProductCategory)
        Return ProductCategoriesLoader.All(oEmp)
    End Function

    Shared Function All(oBrand As DTOProductBrand) As List(Of DTOProductCategory)
        Return ProductCategoriesLoader.All(oBrand)
    End Function

    Shared Function All(oDept As DTODept) As List(Of DTOProductCategory)
        Dim retval As New List(Of DTOProductCategory)
        Dim oCategories = ProductCategoriesLoader.All(oDept.Brand)

        Dim oCnaps = BEBL.Cnaps.All()
        For Each oCnap In oCnaps.Where(Function(x) x.Parent IsNot Nothing)
            Dim oParentGuid = oCnap.Parent.Guid
            oCnap.Parent = oCnaps.FirstOrDefault(Function(x) x.Guid.Equals(oParentGuid))
        Next

        For Each oCategory In oCategories.Where(Function(x) x.Cnap IsNot Nothing).ToList()
            oCategory.Cnap = oCnaps.FirstOrDefault(Function(x) x.Equals(oCategory.Cnap))
            If oDept.cnaps.Any(Function(x) oCategory.Cnap.IsChildOf(x)) Then
                retval.Add(oCategory)
            End If
        Next

        Return retval
    End Function

    Shared Function AllWithFilterItems(oBrand As DTOProductBrand) As List(Of DTOProductCategory)
        Return ProductCategoriesLoader.AllWithFilterItems(oBrand)
    End Function

    Shared Function All(oBrand As DTOProductBrand, Optional oMgz As DTOMgz = Nothing, Optional IncludeObsolets As Boolean = False, Optional oSortOrder As DTOProductCategory.SortOrders = DTOProductCategory.SortOrders.Alfabetic, Optional skipEmptyCategories As Boolean = False) As List(Of DTOProductCategory)
        Dim retval As List(Of DTOProductCategory) = ProductCategoriesLoader.All(oBrand, oMgz, IncludeObsolets, oSortOrder, stockOnly:=False, skipEmptyCategories:=skipEmptyCategories)
        Return retval
    End Function


    Shared Function GuidNoms(oBrand As DTOProductBrand, oCustomer As DTOCustomer, stockOnly As Boolean, Optional oMgz As DTOMgz = Nothing, Optional oSortOrder As DTOProductCategory.SortOrders = DTOProductCategory.SortOrders.Alfabetic) As List(Of DTOGuidNom)
        Dim oCategories As List(Of DTOProductCategory) = ProductCategoriesLoader.All(oBrand, oMgz, False, oSortOrder, stockOnly)
        Dim retval As New List(Of DTOGuidNom)
        For Each item In oCategories
            Select Case item.Codi
                Case DTOProductCategory.Codis.Standard, DTOProductCategory.Codis.accessories
                    retval.Add(New DTOGuidNom(item.Guid, item.nom.Esp))
            End Select
        Next
        Return retval
    End Function

    Shared Function All(oCustomer As DTOCustomer) As List(Of DTOProductCategory)
        Dim oSkus As List(Of DTOProductSku) = ProductSkusLoader.All(oCustomer)
        Dim retval As List(Of DTOProductCategory) = oSkus.Select(Function(x) x.category).Distinct.ToList
        Return retval
    End Function

    Shared Function GuidNoms(oBrand As DTOProductBrand, oUser As DTOUser, stockOnly As Boolean, Optional oMgz As DTOMgz = Nothing, Optional oSortOrder As DTOProductCategory.SortOrders = DTOProductCategory.SortOrders.Alfabetic) As List(Of DTOGuidNom)
        Dim oCategories As List(Of DTOProductCategory) = ProductCategoriesLoader.All(oBrand, oMgz, False, oSortOrder, stockOnly)
        Dim retval As New List(Of DTOGuidNom)
        For Each item In oCategories
            Select Case item.Codi
                Case DTOProductCategory.Codis.Standard, DTOProductCategory.Codis.accessories
                    retval.Add(New DTOGuidNom(item.Guid, item.nom.Esp))
            End Select
        Next
        Return retval
    End Function

    Shared Function CompactTree(oEmp As DTOEmp, oLang As DTOLang) As DTOCatalog
        Return ProductCategoriesLoader.CompactTree(oEmp, oLang)
    End Function

    Shared Function FromCustomerOrders(oContact As DTOContact, stockOnly As Boolean, oBrand As DTOProductBrand) As List(Of DTOProductCategory)
        Dim retval As List(Of DTOProductCategory) = ProductCategoriesLoader.FromCustomerOrders(oContact, oBrand)
        Return retval
    End Function

    Shared Function Move(oValues As List(Of DTOProductCategory), exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ProductCategoriesLoader.Move(oValues, exs)
        Return retval
    End Function
End Class
