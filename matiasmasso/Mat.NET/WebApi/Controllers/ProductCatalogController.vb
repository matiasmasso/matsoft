Imports System.Net
Imports System.Net.Http
Imports System.Web.Http


Public Class ProductCatalogsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/ProductCatalog/Brands/{emp}")>
    Public Function All(emp As DTOEmp.Ids) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.ProductCatalog.Brands(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les ProductCatalogs")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ProductCatalog/Factory/{emp}/{contact}")>
    Public Function Factory(emp As DTOEmp.Ids, contact As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oContact = DTOBaseGuid.Opcional(Of DTOContact)(contact)
            Dim values = BEBL.ProductCatalog.Factory(oEmp, oContact)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les ProductCatalogs")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ProductCatalog/FromIncidencia/{emp}/{incidencia}")>
    Public Function FromIncidencia(emp As DTOEmp.Ids, incidencia As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oIncidencia = BEBL.Incidencia.Find(incidencia)
            Dim values = BEBL.ProductCatalog.Factory(oEmp, oIncidencia)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les ProductCatalogs")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ProductCatalog/CompactTree/{emp}")>
    Public Function CompactTree(emp As DTOEmp.Ids) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.ProductCatalog.CompactTree(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir l'arbre de productes")
        End Try
        Return retval
    End Function
    <HttpGet>
    <Route("api/ProductCatalog/CompactTree/{emp}/{includeObsoletos}")>
    Public Function CompactTreeWithObsoletos(emp As DTOEmp.Ids, includeObsoletos As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.ProductCatalog.CompactTree(oEmp, (includeObsoletos = 1))
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir l'arbre de productes")
        End Try
        Return retval
    End Function
    <HttpGet>
    <Route("api/ProductCatalog/CustomerBasicTree/{customer}/{lang}")>
    Public Function CustomerBasicTree(customer As Guid, lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer As New DTOCustomer(customer)
            Dim oLang = DTOLang.Factory(lang)
            Dim values = BEBL.ProductCatalog.CustomerBasicTree(oCustomer, oLang)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir l'arbre de productes")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/ProductCatalog/CompactBrandCategories/{user}")>
    Public Function CompactBrandCategories(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim values = BEBL.ProductCatalog.CompactBrandCategories(oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les ProductCatalogs")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ProductCatalog/CompactSkus/{user}/{category}")>
    Public Function CompactSkus(user As Guid, category As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim oCategory As New DTOProductCategory(category)
            Dim values = BEBL.ProductCatalog.CompactSkus(oUser, oCategory, oUser.Emp.Mgz)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les ProductCatalogs")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ProductCatalog/refs")>
    Public Function refs() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.ProductCatalog.Refs()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les referencies")
        End Try
        Return retval
    End Function

End Class
