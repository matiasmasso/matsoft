Public Class CategoryController
    Inherits _BaseController


    <HttpPost>
    <Route("api/categories")>
    Public Function categoriesPerContact(contactProduct As DUI.ContactProduct) As List(Of DUI.Category)
        Dim retval As New List(Of DUI.Category)
        If contactProduct.Contact IsNot Nothing Then
            Dim oCustomer As DTOCustomer = BLLCustomer.Find(contactProduct.Contact.Guid)
            BLLContact.Load(oCustomer)
        End If

        Dim oBrand As New DTOProductBrand(contactProduct.Product.Guid)
        Dim oCategories As List(Of DTOProductCategory) = BLL.BLLProductCategories.All(oBrand, skipEmptyCategories:=True)

        'descarta recanvis i publicitat
        oCategories = oCategories.Where(Function(x) (x.Codi = DTOProductCategory.Codis.Standard Or x.Codi = DTOProductCategory.Codis.Accessories)).ToList

        For Each oCategory As DTOProductCategory In oCategories
            Dim item As New DUI.Category
            With item
                .Guid = oCategory.Guid
                .Nom = oCategory.Nom
            End With
            retval.Add(item)
        Next
        Return retval
    End Function

    <HttpPost>
    <Route("api/brand/skus")>
    Public Function skus(brand As DTOGuidNom, user As DTOGuidNom) As List(Of DUI.Category)
        Dim oUser As DTOUser = BLLUser.Find(user.Guid)
        Dim oBrand As New DTOProductBrand(brand.Guid)
        Dim oCategories As List(Of DTOProductCategory) = BLL.BLLSkuStocks.ForApi(oBrand, oUser)
        Dim retval As New List(Of DUI.Category)
        For Each oCategory As DTOProductCategory In oCategories
            Dim DuiCategory As New DUI.Category()
            With DuiCategory
                .Guid = oCategory.Guid
                .Nom = oCategory.Nom
                .Skus = New List(Of DUI.Sku)
            End With
            retval.Add(DuiCategory)
            For Each oSku As DTOProductSku In oCategory.Skus
                Dim DuiSku As New DUI.Sku
                With DuiSku
                    .Guid = oSku.Guid
                    .Nom = oSku.NomCurt
                    .Stock = oSku.Stock - (oSku.Clients - oSku.ClientsAlPot - oSku.ClientsEnProgramacio)
                End With
                DuiCategory.Skus.Add(DuiSku)
            Next
        Next

        Return retval
    End Function



End Class
