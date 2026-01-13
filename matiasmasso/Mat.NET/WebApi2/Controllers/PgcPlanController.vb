Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class PgcPlanController
    Inherits _BaseController

    <HttpGet>
    <Route("api/PgcPlan/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.PgcPlan.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la PgcPlan")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/PgcPlan")>
    Public Function Update(<FromBody> value As DTOPgcPlan) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.PgcPlan.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la PgcPlan")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la PgcPlan")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/PgcPlan/delete")>
    Public Function Delete(<FromBody> value As DTOPgcPlan) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.PgcPlan.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la PgcPlan")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la PgcPlan")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/PgcPlan/FromYear/{year}")>
    Public Function FromYear(year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.PgcPlan.FromYear(year)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el PgcPlan")
        End Try
        Return retval
    End Function


End Class

Public Class PgcPlansController
    Inherits _BaseController

    <HttpGet>
    <Route("api/PgcPlans")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.PgcPlans.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les PgcPlans")
        End Try
        Return retval
    End Function

End Class

