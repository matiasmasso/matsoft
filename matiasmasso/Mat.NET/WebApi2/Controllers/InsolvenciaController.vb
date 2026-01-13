Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class InsolvenciaController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Insolvencia/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Insolvencia.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Insolvencia")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Insolvencia")>
    Public Function Update(<FromBody> value As DTOInsolvencia) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Insolvencia.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Insolvencia")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Insolvencia")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Insolvencia/delete")>
    Public Function Delete(<FromBody> value As DTOInsolvencia) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Insolvencia.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Insolvencia")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Insolvencia")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/insolvencia/IsInsolvent/{contact}")>
    Public Function IsInsolvent(contact As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oContact As New DTOContact(contact)
            Dim value = BEBL.Insolvencia.IsInsolvent(oContact)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Insolvencia")
        End Try
        Return retval
    End Function

End Class

Public Class InsolvenciasController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Insolvencias")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.Insolvencias.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Insolvencias")
        End Try
        Return retval
    End Function

End Class
