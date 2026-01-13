Imports System.Net
Imports System.Net.Http
Imports System.Web.Http


Public Class ExtracteController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Extracte/Years/{emp}/{cta}/{contact}")>
    Public Function Years(emp As DTOEmp.Ids, cta As Guid, contact As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oCta As DTOPgcCta = Nothing
            If cta <> Nothing Then oCta = BEBL.PgcCta.Find(cta)
            Dim oContact As DTOContact = Nothing
            If contact <> Nothing Then oContact = BEBL.Contact.Find(contact)
            Dim values = BEBL.Extracte.Years(oEmp, oCta, oContact)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Extractes")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Extracte/Ccbs/{emp}/{year}/{cta}/{contact}")>
    Public Function Ccbs(emp As DTOEmp.Ids, year As Integer, cta As Guid, contact As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oCta As DTOPgcCta = Nothing
            If cta <> Nothing Then oCta = BEBL.PgcCta.Find(cta)
            Dim oContact As DTOContact = Nothing
            If contact <> Nothing Then oContact = BEBL.Contact.Find(contact)
            Dim oExercici As New DTOExercici(oEmp, year)
            Dim oExtracte = DTOExtracte.Factory(oExercici, oCta, oContact)
            Dim values = BEBL.Extracte.Ccbs(oExtracte)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Extractes")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Extracte/Ctas/{emp}/{year}/{contact}")>
    Public Function Years(emp As DTOEmp.Ids, year As Integer, contact As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oContact As DTOContact = Nothing
            If contact <> Nothing Then oContact = BEBL.Contact.Find(contact)
            Dim values = BEBL.Extracte.Ctas(oEmp, year, oContact)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Extractes")
        End Try
        Return retval
    End Function

End Class
