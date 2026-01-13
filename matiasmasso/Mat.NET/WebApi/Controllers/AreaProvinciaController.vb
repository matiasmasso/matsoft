Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Public Class AreaProvinciaController

    Inherits _BaseController

    <HttpGet>
    <Route("api/AreaProvincia/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.AreaProvincia.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la AreaProvincia")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/AreaProvincia/FromSpanishZipCod/{zipcod}")>
    Public Function FromSpanishZipCod(zipcod As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.AreaProvincia.FromSpanishZipCod(zipcod)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la AreaProvincia")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/AreaProvincia")>
    Public Function Update(<FromBody> value As DTOAreaProvincia) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.AreaProvincia.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la AreaProvincia")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la AreaProvincia")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/AreaProvincia/delete")>
    Public Function Delete(<FromBody> value As DTOAreaProvincia) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.AreaProvincia.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la AreaProvincia")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la AreaProvincia")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/AreaProvincia/zonas/{provincia}")>
    Public Function Zonas(provincia As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProvincia As New DTOAreaProvincia(provincia)
            Dim value = BEBL.AreaProvincia.zonas(oProvincia)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la AreaProvincia")
        End Try
        Return retval
    End Function

End Class

Public Class AreaProvinciasController
    Inherits _BaseController

    <HttpGet>
    <Route("api/AreaProvincias/{country}")>
    Public Function All(country As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCountry As New DTOCountry(country)
            Dim values = BEBL.AreaProvincias.All(oCountry)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Provincias")
        End Try
        Return retval
    End Function

End Class
