Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class YouTubeMovieController
    Inherits _BaseController

    <HttpGet>
    <Route("api/YouTubeMovie/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.YouTubeMovie.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la YouTubeMovie")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/YouTubeMovie/thumbnail/{guid}")>
    Public Function Thumbnail(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.YouTubeMovie.Thumbnail(guid)
            retval = MyBase.HttpImageMimeResponseMessage(value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el thumbnail del video")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/YouTubeMovie/DEPRECATED")>
    Public Function Update(<FromBody> value As DTOYouTubeMovie) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.YouTubeMovie.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la YouTubeMovie")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la YouTubeMovie")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/YouTubeMovie")>
    Public Function Upload() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value = ApiHelper.Client.DeSerialize(Of DTOYouTubeMovie)(json, exs)
            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar el video")
            Else
                value.Thumbnail = oHelper.GetFileBytes("image")
                If BEBL.YouTubeMovie.Update(value, exs) Then
                    GlobalVariables.CachedImages.Reset(value.Guid)
                    result = Request.CreateResponse(HttpStatusCode.OK)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar el video")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error al pujar el video")
        End Try

        Return result
    End Function



    <HttpPost>
    <Route("api/YouTubeMovie/delete")>
    Public Function Delete(<FromBody> value As DTOYouTubeMovie) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.YouTubeMovie.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la YouTubeMovie")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la YouTubeMovie")
        End Try
        Return retval
    End Function

End Class

Public Class YouTubeMoviesController
    Inherits _BaseController

    <HttpGet>
    <Route("api/YouTubeMovies/productModel/{emp}/{langId}/{user}")>
    Public Function Model(emp As DTOEmp.Ids, langId As DTOLang.Ids, user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp As New DTOEmp(emp)
            Dim oLang = DTOLang.Factory(langId)
            Dim oUser As DTOUser = Nothing
            If Not user = Nothing Then
                oUser = BEBL.User.Find(user)
            End If
            Dim value = BEBL.YouTubeMovies.ProductModel(oEmp, oLang, oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les YouTubeMovies")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/YouTubeMovies/{product}/{user}")>
    Public Function AllLangs(product As Guid, user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProduct As DTOProduct = Nothing
            If product <> Nothing Then oProduct = New DTOProduct(product)
            Dim oUser = BEBL.User.Find(user)
            Dim values = BEBL.YouTubeMovies.All(oProduct, oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les YouTubeMovies")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/YouTubeMovies/{product}/{user}/{lang}")>
    Public Function FromProductUserLang(product As Guid, user As Guid, lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProduct As DTOProduct = Nothing
            If product <> Nothing Then oProduct = New DTOProduct(product)
            Dim oUser = BEBL.User.Find(user)
            Dim oLang = DTOLang.Factory(lang)
            Dim values = BEBL.YouTubeMovies.All(oProduct, oUser, oLang)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les YouTubeMovies")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/YouTubeMovies/last/{product}/{user}/{lang}")>
    Public Function Last(product As Guid, user As Guid, lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProduct As DTOProduct = Nothing
            If product <> Nothing Then oProduct = New DTOProduct(product)
            Dim oUser As DTOUser = Nothing
            If user <> Nothing Then oUser = BEBL.User.Find(user)
            Dim oLang = DTOLang.Factory(lang)
            Dim values = BEBL.YouTubeMovies.Last(oProduct, oUser, oLang)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les YouTubeMovies")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/YouTubeMovies/FromProductGuid/{product}/{lang}")> 'Obsoleto??
    Public Function FromChildrenOrSelf(product As Guid, lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oLang = DTOLang.Factory(lang)
            Dim oProduct As New DTOProduct(product)
            Dim values = BEBL.YouTubeMovies.FromChildrenOrSelf(oProduct, oLang)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les YouTubeMovies")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/YouTubeMovies/FromSkuOrElseCategory/{sku}/{lang}")> 'iMat 3.1
    Public Function FromSkuOrElseCategory(sku As Guid, lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oLang = DTOLang.Factory(lang)
            Dim oSku As New DTOProductSku(sku)
            Dim values = BEBL.YouTubeMovies.FromProduct(oSku, oLang)
            If values.Count = 0 Then
                BEBL.ProductSku.Load(oSku)
                values = BEBL.YouTubeMovies.FromProduct(oSku.Category, oLang)
            End If
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les YouTubeMovies")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/YouTubeMovies/ExistFromProduct/{product}/{langId}")>
    Public Function ExistFromProduct(product As Guid, langId As DTOLang.Ids) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oLang = DTOLang.Factory(langId)
            Dim value = BEBL.YouTubeMovies.ExistFromProduct(product, oLang)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les YouTubeMovies")
        End Try
        Return retval
    End Function



End Class
