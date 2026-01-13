Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class FtpserverController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Ftpserver/{owner}")>
    Public Function Find(owner As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oOwner As New DTOGuidNom(owner)
            Dim value = BEBL.Ftpserver.Find(oOwner)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el servidor Ftp")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Ftpserver")>
    Public Function Update(<FromBody> value As DTOFtpserver) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Ftpserver.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar el servidor Ftp")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar el servidor Ftp")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Ftpserver/delete")>
    Public Function Delete(<FromBody> value As DTOFtpserver) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Ftpserver.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar el servidor Ftp")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar el servidor Ftp")
        End Try
        Return retval
    End Function

End Class

Public Class FtpserversController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Ftpservers/{emp}")>
    Public Function All(emp As DTOEmp.Ids) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp As New DTOEmp(emp)
            Dim values = BEBL.Ftpservers.All(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els servidors Ftp")
        End Try
        Return retval
    End Function

End Class
