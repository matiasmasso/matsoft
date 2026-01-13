Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Public Class AtlasController
    Inherits _BaseController


    <HttpGet>
    <Route("api/atlas/compact/{user}")>
    Public Function CompactAtlas(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim values = BEBL.Atlas.Compact(oUser, onlysales:=False)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir l'Atlas")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/atlas/compact/withSales/{user}")>
    Public Function CompactAtlasWithSales(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim values = BEBL.Atlas.Compact(oUser, onlysales:=True)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir l'Atlas")
        End Try
        Return retval
    End Function


End Class