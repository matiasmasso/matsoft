Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class CatalegController
    Inherits _BaseController

    <HttpGet>
    <Route("api/cataleg/fromEmp/{emp}")>
    Public Function FromEmp(emp As DTOEmp.Ids) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp As New DTOEmp(emp)
            Dim value = BEBL.Catalog.Factory(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el catàleg")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/cataleg/fromContact/{contact}")>
    Public Function fromContact(contact As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oContact = BEBL.Contact.Find(contact)
            If oContact Is Nothing Then
                retval = MyBase.HttpErrorResponseMessage("contacte no trobat")
            Else
                Dim value = BEBL.Catalog.Factory(oContact.Emp, oContact)
                retval = Request.CreateResponse(HttpStatusCode.OK, value)
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el catàleg")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/cataleg")>
    Public Function compactCataleg(user As DTOGuidNom) As DTOCompactCataleg
        Dim oUser As DTOUser = BEBL.User.Find(user.Guid)

        Dim exs As New List(Of Exception)
        Dim retval = BEBL.CompactCataleg.Factory(oUser.Emp, oUser)
        Return retval
    End Function

    <HttpGet>
    <Route("api/cataleg")>
    Public Function compactCatalegGet() As DTOCompactCataleg
        Dim oUser As DTOUser = DTOUser.Wellknown(DTOUser.Wellknowns.ZabalaHoyos)
        BEBL.User.Load(oUser)

        Dim exs As New List(Of Exception)
        Dim retval = BEBL.CompactCataleg.Factory(oUser.Emp, oUser)
        Return retval
    End Function

    <HttpGet>
    <Route("api/cataleg2_ToDeprecate")>
    Public Function compactCatalegGet2() As DTOCompactCataleg
        Dim oUser As DTOUser = DTOUser.Wellknown(DTOUser.Wellknowns.ZabalaHoyos)
        BEBL.User.Load(oUser)

        Dim exs As New List(Of Exception)
        Dim retval = BEBL.CompactCataleg.Factory(oUser.Emp, oUser)
        Return retval
    End Function

    <HttpPost>
    <Route("api/catalegBrands")>
    Public Function CatalegBrands(oUser As DTOGuidNom) As List(Of DTOProductBrand)
        Dim oEmp As New DTOEmp(DTOEmp.Ids.MatiasMasso)
        Dim retval As List(Of DTOProductBrand) = BEBL.ProductCatalog.Brands(oEmp)
        Return retval
    End Function

    <HttpPost>
    <Route("api/BrandCategories/compact")>
    Public Function CatalegBrands(user As DTOUser) As List(Of DTOCompactNode)
        Dim retval As List(Of DTOCompactNode) = BEBL.ProductCatalog.CompactBrandCategories(user)
        Return retval
    End Function

    <HttpPost>
    <Route("api/skus/compact")>
    Public Function Skus(userCustom As DTOCompactUserCustom) As List(Of DTOCompactGuidNomQtyEur)
        Dim retval As New List(Of DTOCompactGuidNomQtyEur)
        If userCustom IsNot Nothing Then
            Dim oUser As DTOUser = BEBL.User.Find(userCustom.user.guid)
            Dim oCategory As New DTOProductCategory(userCustom.custom.guid)
            Dim oEmp = MyBase.GetEmp(DTOEmp.Ids.MatiasMasso)
            retval = BEBL.ProductCatalog.CompactSkus(oUser, oCategory, oEmp.Mgz)
        End If
        Return retval
    End Function

End Class
