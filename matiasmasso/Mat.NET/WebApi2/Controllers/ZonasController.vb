Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class ZonaController
    Inherits _BaseController


    <HttpGet>
    <Route("api/Zona/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Zona.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Zona")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Zona/FromNom/{countryISO}/{nom}")>
    Public Function Find(countryISO As String, nom As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Zona.FromNom(nom.Replace("_", " "), countryISO)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Zona")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Zona/FromZip/{country}/{zip}")>
    Public Function Find(country As Guid, zip As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCountry As New DTOCountry(country)
            Dim value = BEBL.Zona.FromZip(oCountry, zip)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Zona")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Zona")>
    Public Function Update(<FromBody> value As DTOZona) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Zona.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Zona")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Zona")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/Zona/delete/{zonaFrom}/{zonaTo}")>
    Public Function Delete(zonaFrom As Guid, zonaTo As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oZonaFrom As New DTOZona(zonaFrom)
            Dim oZonaTo As New DTOZona(zonaTo)
            If BEBL.Zona.Delete(exs, oZonaFrom, oZonaTo) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Zona")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Zona")
        End Try
        Return retval
    End Function

End Class


Public Class ZonasController
    Inherits _BaseController


    <HttpGet>
    <Route("api/Zonas/{country}")>
    Public Function All(country As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCountry As New DTOCountry(country)
            Dim values = BEBL.Zonas.All(oCountry)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Zonas")
        End Try
        Return retval
    End Function

    '
    <HttpPost>
    <Route("api/Zonas/fromGeoNamePostalCode")>
    Public Function fromGeoNamePostalCode(<FromBody> oPostalCode As Google.Geonames.postalCodeClass) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.Zonas.All(oPostalCode)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Zonas")
        End Try
        Return retval
    End Function

End Class