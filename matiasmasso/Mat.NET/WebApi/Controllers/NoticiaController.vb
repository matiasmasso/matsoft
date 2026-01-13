Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class NoticiaController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Noticia/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Noticia.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Noticia")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/Noticia/Image265x150/{guid}")>
    Public Function GetIcon(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oImageMime = GlobalVariables.CachedImages.ImageMime(guid, Defaults.ImgTypes.news265x150)
            If oImageMime Is Nothing Then
                oImageMime = BEBL.Noticia.Image265x150(guid)
                GlobalVariables.CachedImages.Add(guid, Defaults.ImgTypes.news265x150, oImageMime)
            End If
            retval = MyBase.HttpImageMimeResponseMessage(oImageMime)

            retval.Headers.CacheControl = New Headers.CacheControlHeaderValue()
            retval.Headers.CacheControl.Public = True
            retval.Headers.CacheControl.MaxAge = New TimeSpan(30, 0, 0, 0)

        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la image del Noticia")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Noticia")>
    Public Function Upload() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value = ApiHelper.Client.DeSerialize(Of DTONoticia)(json, exs)
            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar la Noticia")
            Else
                value.image265x150 = oHelper.GetImage("Image265x150")

                If DAL.NoticiaLoader.Update(value, exs) Then
                    result = Request.CreateResponse(HttpStatusCode.OK)
                    GlobalVariables.CachedImages.Reset(value.Guid)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar el docfile a DAL.NoticiaLoader")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error a DAL.NoticiaLoader")
        End Try

        Return result
    End Function


    <HttpPost>
    <Route("api/Noticia/delete")>
    Public Function Delete(<FromBody> value As DTONoticia) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Noticia.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
                GlobalVariables.CachedImages.Reset(value.Guid)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Noticia")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Noticia")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Noticia/SearchByUrl")>
    Public Function SearchByUrl(<FromBody> urlFriendlySegment As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Noticia.SearchByUrl(urlFriendlySegment)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Noticia")
        End Try
        Return retval
    End Function

End Class

Public Class NoticiasController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Noticias/HeadersForSitemap/{emp}")>
    Public Function HeadersForSitemap(emp As DTOEmp.Ids) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim value = BEBL.Noticias.HeadersForSitemap(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les Noticies")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/Noticias/Headers/{src}/{HidePro}/{OnlyVisible}")>
    Public Function Headers(src As DTONoticia.Srcs, HidePro As Integer, OnlyVisible As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Noticias.Headers(src, (HidePro = 1), (OnlyVisible = 1))
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les Noticies")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Noticias/LastNoticia/{user}/{lang}/{product}")>
    Public Function LastNoticiaForProduct(user As Guid, lang As String, product As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser As DTOUser = Nothing
            If user <> Nothing Then
                oUser = BEBL.User.Find(user)
            End If
            Dim oLang = DTOLang.Factory(lang)
            Dim oProduct = DTOBaseGuid.Opcional(Of DTOProduct)(product)
            Dim value = BEBL.Noticias.LastNoticia(oUser, oLang, oProduct)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les Noticies")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Noticias/LastPost/{user}/{lang}")> '-----------------iMat SwiftUI
    Public Function LastNoticia(user As Guid, lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser As DTOUser = BEBL.User.Find(user)
            Dim oLang = DTOLang.Factory(lang)
            Dim value = BEBL.Noticias.LastPost(oUser, oLang)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les Noticies")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Noticias/LastPost/{lang}")> '-----------------iMat SwiftUI
    Public Function LastNoticia(lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oLang = DTOLang.Factory(lang)
            Dim value = BEBL.Noticias.LastPost(Nothing, oLang)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les Noticies")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/Noticias/FromCategoria/{user}/{categoriaDeNoticia}")>
    Public Function FromCategoria(user As Guid, categoriaDeNoticia As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = DTOBaseGuid.Opcional(Of DTOUser)(user)
            Dim oCategoriaDeNoticia As New DTOCategoriaDeNoticia(categoriaDeNoticia)
            Dim value = BEBL.Noticias.FromCategoria(oUser.Emp, oUser, oCategoriaDeNoticia)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les Noticies")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Noticias/NextEvents/{user}/{onlyVisible}/{iTop}")>
    Public Function NextEvents(user As Guid, onlyVisible As Integer, iTop As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = DTOBaseGuid.Opcional(Of DTOUser)(user)
            Dim value = BEBL.Noticias.NextEvents(oUser, (onlyVisible = 1), iTop)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les Noticies")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Noticias/Destacats/{src}/{user}")>
    Public Function Destacats(src As DTONoticiaBase.Srcs, user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser As DTOUser = Nothing
            If user <> Nothing Then
                oUser = BEBL.User.Find(user)
            End If

            Dim value = BEBL.Noticias.Destacats(src, oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les Noticies Destacades")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/Noticias/compact/{lang}/{user?}")> 'iMat 3.0
    Public Function Compact(lang As String, Optional user As Guid? = Nothing) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oLang = DTOLang.Factory(lang)
            Dim oUser As DTOUser = Nothing
            If user IsNot Nothing Then oUser = BEBL.User.Find(user)
            Dim HidePro = oUser Is Nothing OrElse oUser.Rol.isProfesional = False
            Dim value = BEBL.Noticias.Compact(oLang, HidePro)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les Noticies")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Noticias/LastNoticias/{user}/{lang}/{product}")>
    Public Function LastNoticias(user As Guid, lang As String, product As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim oLang = DTOLang.Factory(lang)
            Dim oProduct = DTOBaseGuid.Opcional(Of DTOProduct)(product)
            Dim value = BEBL.Noticias.LastNoticias(oUser, oLang, oProduct)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les Noticies")
        End Try
        Return retval
    End Function



End Class