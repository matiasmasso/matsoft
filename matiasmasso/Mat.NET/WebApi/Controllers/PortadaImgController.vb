Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Public Class PortadaImgController
    Inherits _BaseController


    <HttpGet>
    <Route("api/PortadaImg/img/{id}")>
    Public Function ImageMime(id As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.PortadaImg.ImageMime(id)
            retval = MyBase.HttpImageMimeResponseMessage(value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la imatge de portada")
        End Try
        Return retval
    End Function

End Class

Public Class PortadaImgsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/PortadaImgs")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.PortadaImgs.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les imatges de portada")
        End Try
        Return retval
    End Function

End Class
