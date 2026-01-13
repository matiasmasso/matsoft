Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class SitemapController
    Inherits _BaseController

    <HttpGet>
    <Route("api/sitemap/product_accessories/{emp}/{domain}")>
    Public Function product_accessories(emp As DTOEmp.Ids, domain As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oDomain = DTOWebDomain.Factory(domain)
            Dim values = BEBL.SiteMap.product_accessories(oEmp, oDomain)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Sitemaps")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/sitemap/product_descargas/{emp}/{domain}")>
    Public Function product_descargas(emp As DTOEmp.Ids, domain As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oDomain = DTOWebDomain.Factory(domain)
            Dim values = BEBL.SiteMap.product_descargas(oEmp, oDomain)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Sitemaps")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/sitemap/product_downloads/{emp}/{domain}")>
    Public Function product_downloads(emp As DTOEmp.Ids, domain As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oDomain = DTOWebDomain.Factory(domain)
            Dim values = BEBL.SiteMap.product_downloads(oEmp, oDomain)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Sitemaps")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/sitemap/product_videos/{emp}/{domain}")>
    Public Function product_videos(emp As DTOEmp.Ids, domain As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oDomain = DTOWebDomain.Factory(domain)
            Dim values = BEBL.SiteMap.product_videos(oEmp, oDomain)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Sitemaps")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/sitemap/Distributors/{emp}/{domain}")>
    Public Function Distributors(emp As DTOEmp.Ids, domain As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oDomain = DTOWebDomain.Factory(domain)
            Dim values = BEBL.SiteMap.Distributors(oEmp, oDomain)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Sitemaps")
        End Try
        Return retval
    End Function

End Class
