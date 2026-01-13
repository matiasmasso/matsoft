Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class BloggerController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Blogger/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Blogger.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el Blogger")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Blogger/logo/{guid}")>
    Public Function GetIcon(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Blogger.Find(guid)
            retval = MyBase.HttpImageResponseMessage(value.Logo)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el logo del Blogger")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Blogger")>
    Public Function Upload() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value = ApiHelper.Client.DeSerialize(Of DTOBlogger)(json, exs)
            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar la Blogger")
            Else
                value.Logo = oHelper.GetImage("logo")

                If DAL.BloggerLoader.Update(value, exs) Then
                    result = Request.CreateResponse(HttpStatusCode.OK)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar el logo a DAL.BloggerLoader")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error a DAL.BloggerLoader")
        End Try

        Return result
    End Function


    <HttpPost>
    <Route("api/Blogger/delete")>
    Public Function Delete(<FromBody> value As DTOBlogger) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Blogger.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar el Blogger")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar el Blogger")
        End Try
        Return retval
    End Function

End Class

Public Class BloggersController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Bloggers")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.Bloggers.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els Bloggers")
        End Try
        Return retval
    End Function

End Class
