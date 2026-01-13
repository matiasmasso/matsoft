Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class BlogPostController
    Inherits _BaseController

    <HttpGet>
    <Route("api/BlogPost/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.BlogPost.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la BlogPost")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/BlogPost/model/{guid}/{lang}")>
    Public Function Model(guid As Guid, lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oLang = DTOLang.Factory(lang)
            Dim value = BEBL.BlogPost.Model(guid, oLang)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la BlogPost")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/BlogPost/model/{guid}/{lang}/{includeComment}")>
    Public Function ModelWithComment(guid As Guid, lang As String, includeComment As Nullable(Of Guid)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oLang = DTOLang.Factory(lang)
            Dim oIncludeComment As DTOPostComment = If(includeComment Is Nothing, Nothing, New DTOPostComment(includeComment))
            Dim value = BEBL.BlogPost.Model(guid, oLang, oIncludeComment)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la BlogPost")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/BlogPost/model/FromFriendlyUrl/{lang}")>
    Public Function FromFriendlyUrl(lang As String, <FromBody> friendlyUrl As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oLang = DTOLang.Factory(lang)
            Dim value = BEBL.BlogPost.ModelFromFriendlyUrl(friendlyUrl, oLang)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la BlogPost")
        End Try
        Return retval
    End Function

    'FromFriendlyUrl

    <HttpGet>
    <Route("api/BlogPost/model/last/{lang}")>
    Public Function LastModel(lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oLang = DTOLang.Factory(lang)
            Dim value = BEBL.BlogPost.LastModel(oLang)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la BlogPost")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/BlogPost/thumbnail/{guid}")>
    Public Function GetIcon(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = ImageMime.Factory(BEBL.BlogPost.Thumbnail(guid))
            retval = MyBase.HttpImageMimeResponseMessage(value)

            retval.Headers.CacheControl = New Headers.CacheControlHeaderValue()
            retval.Headers.CacheControl.Public = True
            retval.Headers.CacheControl.MaxAge = New TimeSpan(30, 0, 0, 0)

        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el image del BlogPost")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/BlogPost")>
    Public Function Upload() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value = ApiHelper.Client.DeSerialize(Of DTOBlogPost)(json, exs)
            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar la BlogPost")
            Else
                value.Thumbnail = oHelper.GetFileBytes("Thumbnail")

                If BEBL.BlogPost.Update(value, exs) Then
                    result = Request.CreateResponse(HttpStatusCode.OK)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar el BlogPost")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error a DAL.BlogPostLoader")
        End Try

        Return result
    End Function


    <HttpPost>
    <Route("api/BlogPost/delete")>
    Public Function Delete(<FromBody> value As DTOBlogPost) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.BlogPost.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la BlogPost")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la BlogPost")
        End Try
        Return retval
    End Function

End Class

Public Class BlogPostsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/BlogPosts/compact/{lang}")> 'iMat 3.0
    Public Function Compact(lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oLang = DTOLang.Factory(lang)
            Dim value = BEBL.BlogPosts.Compact(oLang)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les Noticies")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/BlogPosts")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.BlogPosts.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les BlogPosts")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/BlogPosts/{lang}")>
    Public Function AllFromLang(lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oLang = DTOLang.Factory(lang)
            Dim values = BEBL.BlogPosts.All(oLang)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les BlogPosts")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/BlogPosts/models/{lang}")>
    Public Function Models(lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oLang = DTOLang.Factory(lang)
            Dim values = BEBL.BlogPosts.Models(oLang)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les BlogPosts")
        End Try
        Return retval
    End Function
End Class
