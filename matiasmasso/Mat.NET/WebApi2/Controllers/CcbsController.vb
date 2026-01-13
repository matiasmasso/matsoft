Imports System.Net
Imports System.Net.Http
Imports System.Web.Http


Public Class CcbsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Ccbs/{emp}/{year}/{cta}/{contact}/{fch}")>
    Public Function AllWithContact(emp As DTOEmp.Ids, year As Integer, cta As Guid, contact As Guid, fch As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oExercici = MyBase.GetExercici(emp, year)
            Dim oCta = BEBL.PgcCta.Find(cta)
            Dim oContact As DTOContact = Nothing
            If contact <> Nothing Then oContact = New DTOContact(contact)
            Dim values = BEBL.Ccbs.AllWithContact(oContact, oExercici, oCta, fch)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Ccbs")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Ccbs/fromCta/{emp}/{year}/{cta}")>
    Public Function FromCta(emp As DTOEmp.Ids, year As Integer, cta As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oExercici As New DTOExercici(New DTOEmp(emp), year)
            Dim oCta = BEBL.PgcCta.Find(cta)
            Dim values = BEBL.Ccbs.All(oExercici, oCta)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Ccbs")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Ccbs/{emp}/{year}/{month}")>
    Public Function All2(emp As DTOEmp.Ids, year As Integer, month As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oYearMonth As New DTOYearMonth(year, month)
            Dim values = BEBL.Ccbs.All2(oEmp, oYearMonth)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Ccbs")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Ccbs/LlibreMajor/{emp}/{year}")>
    Public Function LlibreMajor(emp As DTOEmp.Ids, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oExercici = MyBase.GetExercici(emp, year)
            Dim values = BEBL.Ccbs.LlibreMajor(oExercici)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Ccbs")
        End Try
        Return retval
    End Function





End Class

