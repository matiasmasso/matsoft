Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class PgcClassController
    Inherits _BaseController

    <HttpGet>
    <Route("api/PgcClass/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.PgcClass.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la PgcClass")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/PgcClass")>
    Public Function Update(<FromBody> value As DTOPgcClass) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.PgcClass.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la PgcClass")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la PgcClass")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/PgcClass/delete")>
    Public Function Delete(<FromBody> value As DTOPgcClass) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.PgcClass.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la PgcClass")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la PgcClass")
        End Try
        Return retval
    End Function

End Class

Public Class PgcClassesController
    Inherits _BaseController

    <HttpGet>
    <Route("api/PgcClasses/{plan}")>
    Public Function All(plan As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oPlan As New DTOPgcPlan(plan)
            Dim values = BEBL.PgcClasses.All(oPlan)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les PgcClasses")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/PgcClasses/tree/{emp}/{fromyear}/{plan}")>
    Public Function Tree(emp As Integer, fromyear As Integer, plan As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oPlan As New DTOPgcPlan(plan)
            Dim values = BEBL.PgcClasses.Tree(oEmp, fromyear, oPlan)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les PgcClasses")
        End Try
        Return retval
    End Function

End Class
