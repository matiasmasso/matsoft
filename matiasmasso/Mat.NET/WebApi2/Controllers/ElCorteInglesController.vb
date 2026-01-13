Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class ElCorteInglesController
    Inherits _BaseController

    <HttpPost>
    <Route("api/elCorteIngles/ComandesDeTransmisions")>
    Public Function ComandesDeTransmisions(<FromBody> oTransmisions As List(Of DTOTransmisio)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.ElCorteIngles.ComandesDeTransmisions(oTransmisions)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Template")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ElCorteIngles/orders/{year}")>
    Public Function Orders(year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.ElCorteIngles.OrdersModel(year)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les comandes de El Corte Ingles")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/ElCorteIngles/plantillas/descatalogats")>
    Public Function PlantillaDescatalogats() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.ElCorteIngles.PlantillaDescatalogats()
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al redactar la plantilla e descatalogats per El Corte Ingles")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ElCorteIngles/plantillas/exhaurits")>
    Public Function PlantillaExhaurits() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.ElCorteIngles.PlantillaExhaurits()
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al redactar la plantilla e descatalogats per El Corte Ingles")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/ElCorteIngles/descataloga")>
    Public Function Descataloga(<FromBody> oGuids As List(Of Guid)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.ElCorteIngles.Descataloga(exs, oGuids) Then
                retval = Request.CreateResponse(HttpStatusCode.OK)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al descatalogar articles de El Corte Ingles")
            End If

        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al descatalogar articles de El Corte Ingles")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/ElCorteIngles/recataloga")>
    Public Function Recataloga(<FromBody> oGuids As List(Of Guid)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.ElCorteIngles.Recataloga(exs, oGuids) Then
                retval = Request.CreateResponse(HttpStatusCode.OK)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al recatalogar articles de El Corte Ingles")
            End If

        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al recatalogar articles de El Corte Ingles")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/elcorteingles/AlineamientoDeDisponibilidad")>
    Public Function Factory() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Integracions.ElCorteIngles.AlineamientoDeDisponibilidad.Factory()
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al crear el fitxer d'alineamiento de disponibilitat")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/elcorteingles/AlineamientoDeDisponibilidad/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Integracions.ElCorteIngles.AlineamientoDeDisponibilidad.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el log")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/elcorteingles/AlineamientosDeDisponibilidad")>
    Public Function AlineamientosDeDisponibilidad() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.Integracions.ElCorteIngles.AlineamientosDeDisponibilidad.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els Alineamientos de Disponibilidad")
        End Try
        Return retval
    End Function
End Class
