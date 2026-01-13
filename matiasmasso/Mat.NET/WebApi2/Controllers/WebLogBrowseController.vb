Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class WebLogBrowseController
    Inherits _BaseController

    <HttpGet>
    <Route("api/WebLogBrowse/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.WebLogBrowse.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la WebLogBrowse")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/WebLogBrowse")>
    Public Function Update(<FromBody> value As DTOWebLogBrowse) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.WebLogBrowse.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la WebLogBrowse")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la WebLogBrowse")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/WebLogBrowse/delete")>
    Public Function Delete(<FromBody> value As DTOWebLogBrowse) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.WebLogBrowse.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la WebLogBrowse")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la WebLogBrowse")
        End Try
        Return retval
    End Function

End Class

Public Class WebLogBrowsesController
    Inherits _BaseController

    <HttpGet>
    <Route("api/WebLogBrowses/{doc}")>
    Public Function All(doc As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oDoc As New DTOBaseGuid(doc)
            Dim values = BEBL.WebLogBrowses.All(oDoc)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les WebLogBrowses")
        End Try
        Return retval
    End Function

End Class
