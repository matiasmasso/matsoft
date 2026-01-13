Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class CategoriaDeNoticiaController
    Inherits _BaseController

    <HttpGet>
    <Route("api/CategoriaDeNoticia/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.CategoriaDeNoticia.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la CategoriaDeNoticia")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/CategoriaDeNoticia/fromNom{nom}")>
    Public Function fromNom(nom As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.CategoriaDeNoticia.FromNom(nom)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la CategoriaDeNoticia")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/CategoriaDeNoticia")>
    Public Function Update(<FromBody> value As DTOCategoriaDeNoticia) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.CategoriaDeNoticia.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la CategoriaDeNoticia")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la CategoriaDeNoticia")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/CategoriaDeNoticia/delete")>
    Public Function Delete(<FromBody> value As DTOCategoriaDeNoticia) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.CategoriaDeNoticia.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la CategoriaDeNoticia")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la CategoriaDeNoticia")
        End Try
        Return retval
    End Function

End Class

Public Class CategoriasDeNoticiaController
    Inherits _BaseController

    <HttpGet>
    <Route("api/CategoriasDeNoticia")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.CategoriasDeNoticia.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les CategoriasDeNoticia")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/CategoriasDeNoticia/ForSitemap")>
    Public Function ForSitemap() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.CategoriasDeNoticia.ForSitemap()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les CategoriasDeNoticia")
        End Try
        Return retval
    End Function

End Class
