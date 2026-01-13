Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class SatRecallController
    Inherits _BaseController

    <HttpGet>
    <Route("api/SatRecall/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.SatRecall.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la SatRecall")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/SatRecall/FromIncidencia/{incidencia}")>
    Public Function FromIncidencia(incidencia As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oIncidencia As New DTOIncidencia(incidencia)
            Dim value = BEBL.SatRecall.fromIncidencia(oIncidencia)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la SatRecall")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/SatRecall")>
    Public Function Update(<FromBody> value As DTOSatRecall) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.SatRecall.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la SatRecall")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la SatRecall")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/SatRecall/delete")>
    Public Function Delete(<FromBody> value As DTOSatRecall) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.SatRecall.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la SatRecall")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la SatRecall")
        End Try
        Return retval
    End Function

End Class

Public Class SatRecallsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/SatRecalls/{emp}/{mode}")>
    Public Function All(emp As Integer, mode As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.SatRecalls.All(oEmp, mode)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les SatRecalls")
        End Try
        Return retval
    End Function

End Class
