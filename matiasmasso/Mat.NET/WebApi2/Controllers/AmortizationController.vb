Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class AmortizationController
    Inherits _BaseController


    <HttpGet>
    <Route("api/Amortization/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Amortization.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Amortization")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Amortization/FromAlta{cca}")>
    Public Function FromAlta(cca As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCca As New DTOCca(cca)
            Dim value = BEBL.Amortization.FromAlta(oCca)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Amortization")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Amortization/FromBaixa{cca}")>
    Public Function FromBaixa(cca As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCca As New DTOCca(cca)
            Dim value = BEBL.Amortization.FromBaixa(oCca)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Amortization")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Amortization")>
    Public Function Update(<FromBody> value As DTOAmortization) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Amortization.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Amortization")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Amortization")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Amortization/delete")>
    Public Function Delete(<FromBody> value As DTOAmortization) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Amortization.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Amortization")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Amortization")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Amortization/Amortiza/{user}/{emp}/{year}")>
    Public Function Update(user As Guid, emp As DTOEmp.Ids, year As Integer, <FromBody> value As DTOAmortization) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oUser As New DTOUser(user)
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oExercici As New DTOExercici(oEmp, year)
            If BEBL.Amortization.Amortitza(oUser, value, oExercici, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Amortization")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Amortization")
        End Try
        Return retval
    End Function

End Class
Public Class AmortizationsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Amortizations/{emp}")>
    Public Function All(emp As DTOEmp.Ids) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.Amortizations.All(oEmp)
            retval = Request.CreateResponse(Of List(Of DTOAmortization))(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les amortitzacions pendents")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/Amortizations/Pendents/{emp}/{year}")>
    Public Function Pendents(emp As DTOEmp.Ids, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oExercici As New DTOExercici(oEmp, year)
            Dim values = BEBL.Amortizations.PendentsDeAmortitzar(oExercici)
            retval = Request.CreateResponse(Of List(Of DTOAmortization))(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les amortitzacions pendents")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Amortizations/Amortiza/{user}/{emp}/{year}")>
    Public Function Amortitza(user As Guid, emp As DTOEmp.Ids, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oUser = BEBL.User.Find(user)
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oExercici As New DTOExercici(oEmp, year)
            If BEBL.Amortizations.Amortitza(oUser, oExercici, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Amortization")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Amortization")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Amortizations/RevertAmortizations/{emp}/{year}")>
    Public Function RevertAmortizations(emp As DTOEmp.Ids, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oExercici As New DTOExercici(oEmp, year)
            If BEBL.Amortizations.RevertAmortizations(oExercici, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al revertir la Amortization")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al revertir la Amortization")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/Amortizations/DefaultTipus")>
    Public Function DefaultTipus() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.Amortizations.DefaultTipus()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els tipus per defecte de les amortitzacions")
        End Try
        Return retval
    End Function

End Class
