Public Class CatalegController
    Inherits _BaseController

    <HttpPost>
    <Route("api/cataleg")>
    Public Function compactCataleg(user As DTOGuidNom) As DTOCompactCataleg
        Dim oUser As DTOUser = BLLUser.Find(user.Guid)

        Dim exs As New List(Of Exception)
        Dim retval As DTOCompactCataleg = BLLCompactCatalog.Cataleg(oUser, exs)
        Return retval
    End Function

    <HttpGet>
    <Route("api/cataleg")>
    Public Function compactCatalegGet() As DTOCompactCataleg
        Dim oUser As DTOUser = BLLUser.WellKnown(BLLUser.WellKnowns.ZabalaHoyos)
        BLLUser.Load(oUser)

        Dim exs As New List(Of Exception)
        Dim retval As DTOCompactCataleg = BLLCompactCatalog.Cataleg(oUser, exs)
        Return retval
    End Function

    <HttpPost>
    <Route("api/catalegBrands")>
    Public Function CatalegBrands(oUser As DTOGuidNom) As List(Of DTOProductBrand)
        Dim retval As List(Of DTOProductBrand) = BLLProductCatalog.Brands()
        Return retval
    End Function

End Class
