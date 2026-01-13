Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Public Class DefaultController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Default/{cod}")>
    Public Function Find(cod As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Default.Find(cod)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el Default")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Default/{cod}/{emp}")>
    Public Function Find2(cod As Integer, emp As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim value = BEBL.Default.Find(cod, oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el Default")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Default")>
    Public Function Update(<FromBody> value As DTODefault) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Default.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Default")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Default")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/Default/delete")>
    Public Function Delete(<FromBody> value As DTODefault) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Default.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Default")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Default")
        End Try
        Return retval
    End Function

End Class

Public Class DefaultsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Defaults/{emp}")>
    Public Function All(emp As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.Defaults.All(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Defaults")
        End Try
        Return retval
    End Function

End Class

