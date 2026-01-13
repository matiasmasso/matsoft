Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Public Class PgcCtaController
    Inherits _BaseController


    <HttpGet>
    <Route("api/PgcCta/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.PgcCta.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la PgcCta")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/PgcCta")>
    Public Function Update(<FromBody> value As DTOPgcCta) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.PgcCta.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la PgcCta")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la PgcCta")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/PgcCta/delete")>
    Public Function Delete(<FromBody> value As DTOPgcCta) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.PgcCta.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la PgcCta")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la PgcCta")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/PgcCta/saldo/{emp}/{cta}/{contact}/{fch}")>
    Public Function Saldo(emp As DTOEmp.Ids, cta As Guid, contact As Guid, fch As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oCta = BEBL.PgcCta.Find(cta)
            Dim oContact As DTOContact = Nothing
            If contact <> Nothing Then oContact = New DTOContact(contact)
            Dim value = BEBL.PgcCta.Saldo(oEmp, oCta, oContact, fch)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el saldo")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/PgcCta/FromCod/{cod}/{emp}/{year}")>
    Public Function FromCod(cod As DTOPgcPlan.Ctas, emp As DTOEmp.Ids, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oExercici = DTOExercici.Current(oEmp)
            If year <> 0 Then oExercici = DTOExercici.FromYear(oEmp, year)
            Dim value = BEBL.PgcCta.FromCod(cod, oExercici)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el saldo")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/PgcCta/FromId/{plan}/{id}")>
    Public Function FromId(plan As Guid, Id As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oPlan = New DTOPgcPlan(plan)
            Dim value = BEBL.PgcCta.FromId(oPlan, Id)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el saldo")
        End Try
        Return retval
    End Function

End Class
Public Class PgcCtasController
    Inherits _BaseController

    <HttpGet>
    <Route("api/pgcctas/{year}")>
    Public Function Current(year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.PgcCtas.Current(year)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els comptes")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/pgcctas/search/{plan}/{searchkey}")>
    Public Function Search(plan As Guid, searchkey As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oPlan As New DTOPgcPlan(plan)
            Dim value = BEBL.PgcCtas.All(oPlan, searchkey)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al cercar el compte")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/pgcctas/{plan}/{pgcClass}")>
    Public Function All(plan As Guid, pgcClass As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oPlan As New DTOPgcPlan(plan)
            Dim oPgcClass As New DTOPgcClass(pgcClass)
            Dim value = BEBL.PgcCtas.All(oPlan, oPgcClass)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els comptes")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/pgcctas/{emp}/{year}/{contact}")>
    Public Function All(emp As DTOEmp.Ids, year As Integer, contact As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oExercici = DTOExercici.Current(oEmp)
            If year <> 0 Then oExercici = DTOExercici.FromYear(oEmp, year)
            Dim oContact As DTOContact = Nothing
            If contact <> Nothing Then oContact = New DTOContact(contact)
            Dim value = BEBL.PgcCtas.All(oExercici, oContact)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els comptes")
        End Try
        Return retval
    End Function


End Class
