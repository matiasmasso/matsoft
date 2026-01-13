Public Class BrandController
    Inherits _BaseController

    'TO DEPRECATE:
    <HttpPost>
    <Route("api/productbrands")>
    Public Function productbrands(user As DTOGuidNom) As List(Of DTOGuidNom)
        Dim retval As New List(Of DTOGuidNom)
        Dim oUser As DTOUser = BLLUser.Find(user.Guid)
        retval = BLL.BLLProductBrands.GuidNoms(oUser)
        Return retval
    End Function

    <HttpPost>
    <Route("api/brands")>
    Public Function brands(user As DUI.User) As List(Of DUI.Brand)
        Dim retval As New List(Of DUI.Brand)
        Dim oUser As DTOUser = BLLUser.Find(user.Guid)
        Dim oBrands As List(Of DTOProductBrand) = BLL.BLLProductBrands.All(oUser)

        Dim oVarios As DTOProductBrand = oBrands.FirstOrDefault(Function(x) x.Nom = "Varios")
        If oVarios IsNot Nothing Then
            oBrands.Remove(oVarios)
        End If

        For Each oBrand As DTOProductBrand In oBrands
            Dim item As New DUI.Brand
            With item
                .Guid = oBrand.Guid
                .Nom = oBrand.Nom
            End With
            retval.Add(item)
        Next
        Return retval
    End Function

    <HttpPost>
    <Route("api/contact/brands")>
    Public Function brands(contact As DUI.Contact) As List(Of DUI.Brand)
        Dim retval As New List(Of DUI.Brand)
        Dim oCustomer As DTOCustomer = BLLCustomer.Find(contact.Guid)
        BLLContact.Load(oCustomer)
        Dim oBrands As List(Of DTOProductBrand) = BLL.BLLProductBrands.FromCustomer(oCustomer)
        For Each oBrand As DTOProductBrand In oBrands
            Dim item As New DUI.Brand
            With item
                .Guid = oBrand.Guid
                .Nom = oBrand.Nom
            End With
            retval.Add(item)
        Next
        Return retval
    End Function

    <HttpPost>
    <Route("api/productbrand/skus")>
    Public Function productbrandskus(data As DUI.BrandUser) As List(Of DUI.Category)
        Dim retval As New List(Of DUI.Category)
        Dim oBrand As New DTOProductBrand(data.brand.Guid)
        Dim oUser As DTOUser = BLLUser.Find(data.user.Guid)
        Dim oCategories As List(Of DTOProductCategory) = BLLSkuStocks.ForApi(oBrand, oUser)
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
