Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class PortsCondicioController
    Inherits _BaseController

    <HttpGet>
    <Route("api/PortsCondicio/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.PortsCondicio.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les condicions de transport")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/PortsCondicio")>
    Public Function Update(<FromBody> value As DTOPortsCondicio) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.PortsCondicio.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar les condicions de transport")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar les condicions de transport")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/PortsCondicio/delete")>
    Public Function Delete(<FromBody> value As DTOPortsCondicio) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.PortsCondicio.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar les condicions de transport")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar les condicions de transport")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/PortsCondicio/customers/{guid}")>
    Public Function Customers(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oPortsCondicio As New DTOPortsCondicio(guid)
            Dim values = BEBL.PortsCondicio.Customers(oPortsCondicio)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les condicions de transport")
        End Try
        Return retval
    End Function

End Class

Public Class PortsCondicionsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/PortsCondicions")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.PortsCondicions.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les condicions de transport")
        End Try
        Return retval
    End Function

End Class
