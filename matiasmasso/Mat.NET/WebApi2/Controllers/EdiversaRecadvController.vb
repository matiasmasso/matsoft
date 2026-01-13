Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class EdiversaRecadvController
    Inherits _BaseController

    <HttpGet>
    <Route("api/EdiversaRecadv/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.EdiversaRecadv.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la EdiversaRecadv")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/EdiversaRecadv/image/{guid}")>
    Public Function GetIcon(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.EdiversaRecadv.Find(guid)
            'retval = MyBase.HttpImageResponseMessage(value.Image)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el image del EdiversaRecadv")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/EdiversaRecadv")>
    Public Function Update(<FromBody> value As DTOEdiversaRecadv) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.EdiversaRecadv.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la EdiversaRecadv")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la EdiversaRecadv")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/EdiversaRecadv/delete")>
    Public Function Delete(<FromBody> value As DTOEdiversaRecadv) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.EdiversaRecadv.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la EdiversaRecadv")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la EdiversaRecadv")
        End Try
        Return retval
    End Function

End Class

Public Class EdiversaRecadvsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/EdiversaRecadvs")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.EdiversaRecadvs.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les EdiversaRecadvs")
        End Try
        Return retval
    End Function

End Class

