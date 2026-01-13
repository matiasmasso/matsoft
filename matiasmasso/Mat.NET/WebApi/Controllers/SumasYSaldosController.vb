Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class sumasysaldosController
    Inherits _BaseController

    <HttpGet>
    <Route("api/sumasysaldos/{emp}/{fch}")>
    Public Function All(emp As DTOEmp.Ids, fch As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.SumasYSaldos.All(oEmp, fch)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els sumasysaldos")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/sumasysaldos/{emp}/{year}/{HideEmptySaldo}/{range}/{contact}")>
    Public Function All(emp As DTOEmp.Ids, year As Integer, HideEmptySaldo As Boolean, range As Defaults.ContactRange, contact As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oExercici As New DTOExercici(oEmp, year)
            Dim oContact = DTOBaseGuid.Opcional(Of DTOContact)(contact)
            Dim values = BEBL.SumasYSaldos.All(oExercici, HideEmptySaldo, range, oContact)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els sumasysaldos")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/sumasysaldos/SummaryFromFch/{emp}/{fch}")>
    Public Function SummaryFromFch(emp As DTOEmp.Ids, fch As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.SumasYSaldos.Summary(oEmp, fch)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els sumasysaldos")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/sumasysaldos/SummaryFromYear/{emp}/{year}")>
    Public Function SummaryFromYear(emp As DTOEmp.Ids, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oExercici As New DTOExercici(oEmp, year)
            Dim values = BEBL.SumasYSaldos.Summary(oExercici)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els sumasysaldos")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/sumasysaldos/SummaryFromYear/{emp}/{year}/{fch}")>
    Public Function SummaryUpToFch(emp As DTOEmp.Ids, year As Integer, fch As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oExercici As New DTOExercici(oEmp, year)
            Dim values = BEBL.SumasYSaldos.Summary(oExercici, fch.Date)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els sumasysaldos")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/sumasysaldos/subcomptes/{emp}/{year}/{cta}")>
    Public Function SummaryFromCta(emp As DTOEmp.Ids, year As Integer, cta As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oExercici As New DTOExercici(oEmp, year)
            Dim oCta As New DTOPgcCta(cta)
            Dim values = BEBL.SumasYSaldos.SubComptes(oExercici, oCta)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els sumasysaldos")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/sumasysaldos/years/{emp}/{cta}/{contact}")>
    Public Function SummaryFromContact(emp As DTOEmp.Ids, cta As Guid, contact As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oContact = DTOBaseGuid.Opcional(Of DTOContact)(contact)
            Dim oCta = DTOBaseGuid.Opcional(Of DTOPgcCta)(cta)
            Dim values = BEBL.SumasYSaldos.Years(oEmp, oContact, oCta)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els sumasysaldos")
        End Try
        Return retval
    End Function

End Class
