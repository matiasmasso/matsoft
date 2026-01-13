Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class LiniaTelefonController
    Inherits _BaseController

    <HttpGet>
    <Route("api/LiniaTelefon/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.LiniaTelefon.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la LiniaTelefon")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/LiniaTelefon")>
    Public Function Update(<FromBody> value As DTOLiniaTelefon) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.LiniaTelefon.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la LiniaTelefon")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la LiniaTelefon")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/LiniaTelefon/delete")>
    Public Function Delete(<FromBody> value As DTOLiniaTelefon) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.LiniaTelefon.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la LiniaTelefon")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la LiniaTelefon")
        End Try
        Return retval
    End Function

End Class

Public Class LiniaTelefonsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/LiniaTelefons")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.LiniaTelefons.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les LiniaTelefons")
        End Try
        Return retval
    End Function

End Class
