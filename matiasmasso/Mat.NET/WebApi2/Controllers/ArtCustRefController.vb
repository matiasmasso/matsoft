Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class ArtCustRefsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/ArtCustRef/fromUsr/{user}")>
    Public Function Find(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser As New DTOUser(user)
            Dim value = BEBL.ArtCustRefs.FromUser(oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Template")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ArtCustRef/ElCorteIngles")>
    Public Function ElCorteIngles() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.ArtCustRefs.ElCorteIngles()
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el cataleg de El Corte Ingles")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/ArtCustRefs/ElCorteIngles")>
    Public Function ElCorteIngles(<FromBody> items As List(Of DTO.Integracions.ElCorteIngles.Cataleg)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If items.Count = 0 Then
                retval = MyBase.HttpErrorResponseMessage(exs, "no hi ha cap producte per afegir")
            Else
                Dim value = BEBL.ArtCustRefs.Append(exs, items)
                If exs.Count = 0 Then
                    retval = Request.CreateResponse(HttpStatusCode.OK, value)
                Else
                    retval = MyBase.HttpErrorResponseMessage(exs, "error al llegir el cataleg de El Corte Ingles")
                End If
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el cataleg de El Corte Ingles")
        End Try
        Return retval
    End Function

End Class

