Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class PgcSaldoController
    Inherits _BaseController

    <HttpGet>
    <Route("api/PgcSaldo/FromCtaCod/{emp}/{ctacod}/{contact}/{fch}")>
    Public Function FromCtaCod(emp As Integer, ctacod As DTOPgcPlan.Ctas, contact As Guid, fch As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oContact As DTOContact = Nothing
            Dim value As DTOAmt = Nothing
            If contact.Equals(Guid.Empty) Then
                Dim oEmp = GetEmp(emp)
                value = DTOAmt.Factory(BEBL.PgcSaldo.FromCtaCod(oEmp, ctacod, fch)).Trimmed
            Else
                oContact = New DTOContact(contact)
                value = DTOAmt.Factory(BEBL.PgcSaldo.FromCtaCod(ctacod, oContact, fch)).Trimmed
            End If

            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el saldo")
        End Try
        Return retval
    End Function
End Class

Public Class PgcSaldosController
    Inherits _BaseController

    <HttpGet>
    <Route("api/PgcSaldos/{emp}/{year}/{HideEmptySaldo}")>
    Public Function All(emp As Integer, year As Integer, HideEmptySaldo As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = GetEmp(emp)
            Dim oExercici As New DTOExercici(oEmp, year)
            Dim value = BEBL.PgcSaldos.All(oExercici, HideEmptySaldo = 1)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els saldos")
        End Try
        Return retval
    End Function
End Class

