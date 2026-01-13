Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class IncotermController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Incoterm/{id}")>
    Public Function Find(id As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Incoterm.Find(id)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir l'Incoterm")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Incoterm")>
    Public Function Update(<FromBody> value As DTOIncoterm) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Incoterm.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar l'Incoterm")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar l'Incoterm")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Incoterm/delete")>
    Public Function Delete(<FromBody> value As DTOIncoterm) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Incoterm.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar l'Incoterm")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar l'Incoterm")
        End Try
        Return retval
    End Function

End Class

Public Class IncotermsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Incoterms")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.Incoterms.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Incoterms")
        End Try
        Return retval
    End Function

End Class
