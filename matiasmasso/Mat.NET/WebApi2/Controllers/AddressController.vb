Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class AddressController
    Inherits _BaseController


    <HttpPost>
    <Route("api/Address")>
    Public Function Update(<FromBody> value As DTOAddress) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Address.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar l'adreça")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar l'adreça")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Address/updateGeo/{user}/{contact}/{codi}")>
    Public Function updateGeo(user As Guid, contact As Guid, codi As Integer, <FromBody> coordenadas As MatHelperStd.GeoHelper.Coordenadas) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Dim exs As New List(Of Exception)
        Try
            Dim oUser As New DTOUser(user)
            Dim oContact As New DTOContact(contact)
            If BEBL.Address.update(exs, oUser, oContact, codi, coordenadas) Then
                retval = Request.CreateResponse(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "Error al llegir les adreçes")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les adreçes")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Address/delete")>
    Public Function Delete(<FromBody> value As DTOAddress) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Address.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar l'adreça")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar l'adreça")
        End Try
        Return retval
    End Function

End Class

Public Class AddressesController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Addresses/{contact}/{codi}")>
    Public Function All(contact As Guid, codi As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oContact As New DTOContact(contact)
            Dim values = BEBL.Addresses.All(oContact, codi)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les adreçes")
        End Try
        Return retval
    End Function

End Class
