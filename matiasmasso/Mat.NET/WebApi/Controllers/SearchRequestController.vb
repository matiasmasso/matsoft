Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class SearchRequestController
    Inherits _BaseController

    <HttpPost>
    <Route("api/SearchRequest")>
    Public Function Load(<FromBody> value As DTOSearchRequest) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.SearchRequest.Load(value, exs) Then
                retval = Request.CreateResponse(Of DTOSearchRequest)(HttpStatusCode.OK, value)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la SearchRequest")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la SearchRequest")
        End Try
        Return retval
    End Function
End Class

Public Class SearchRequestsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/SearchRequests/{emp}")>
    Public Function All(emp As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.SearchRequests.All(oEmp)
            If exs.Count = 0 Then
                retval = Request.CreateResponse(Of List(Of DTOSearchRequest))(HttpStatusCode.OK, values)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al llegir les SearchRequest")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les SearchRequest")
        End Try
        Return retval
    End Function
End Class
