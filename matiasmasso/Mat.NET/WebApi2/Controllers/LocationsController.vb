Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class LocationController
    Inherits _BaseController


    <HttpGet>
    <Route("api/Location/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Location.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la població")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Location/FromNom/{ISOpais}")>
    Public Function FromNom(ISOpais As String, <FromBody> LocationNom As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Location.FromNom(LocationNom, ISOpais)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la població")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Location/FromNom")>
    Public Function FromNom(<FromBody> LocationNom As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Location.FromNom(LocationNom)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la població")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Location/FromZip/{country}/{ZipCod}")> 'TO DEPRECATE (es en plural)
    Public Function FromZip(country As Guid, ZipCod As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCountry As New DTOCountry(country)
            Dim value = BEBL.Location.FromZip(oCountry, ZipCod)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la població")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Location")>
    Public Function Update(<FromBody> value As DTOLocation) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Location.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la població")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la població")
        End Try
        Return retval
    End Function



    <HttpGet>
    <Route("api/Location/delete/{location}")>
    Public Function Delete(location As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oLocation As New DTOLocation(location)
            If BEBL.Location.Delete(exs, oLocation) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al intentar eliminar la població")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al intentar eliminar la població")
        End Try
        Return retval
    End Function

End Class


Public Class LocationsController
    Inherits _BaseController


    <HttpGet>
    <Route("api/Locations/fromZona/{zona}")>
    Public Function fromZona(zona As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oZona As New DTOZona(zona)
            Dim values = BEBL.Locations.FromZona(oZona)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la població")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Locations/relocate/{zonaTo}")>
    Public Function reLocate(zonaTo As Guid, <FromBody> locations() As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oZona As New DTOZona(zonaTo)
            Dim oLocations As New List(Of DTOLocation)
            For Each guid In locations
                oLocations.Add(New DTOLocation(guid))
            Next
            Dim value = BEBL.Locations.reLocate(exs, oZona, oLocations)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al reasignar la zona de les poblacions")
        End Try
        Return retval
    End Function

End Class
