Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class TransportistaController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Transportista/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Transportista.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el Transportista")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Transportista/exists/{guid}")>
    Public Function Exists(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oContact As New DTOContact(guid)
            Dim value = BEBL.Transportista.Exists(oContact)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Transportista")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Transportista")>
    Public Function Update(<FromBody> value As DTOTransportista) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Transportista.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Transportista")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Transportista")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Transportista/delete")>
    Public Function Delete(<FromBody> value As DTOTransportista) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Transportista.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Transportista")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Transportista")
        End Try
        Return retval
    End Function

End Class

Public Class TransportistasController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Transportistas/{emp}/{onlyActive}")>
    Public Function All(emp As DTOEmp.Ids, onlyActive As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.Transportistas.All(oEmp, (onlyActive = 1))
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Transportistas")
        End Try
        Return retval
    End Function

End Class
