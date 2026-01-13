Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class ExerciciController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Exercici/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Exercici.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir l'Exercici")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Exercici/saldos/{emp}/{year}/{skipTancament}")>
    Public Function saldos(emp As DTOEmp.Ids, year As Integer, skipTancament As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oExercici As New DTOExercici(oEmp, year)
            Dim values = BEBL.Exercici.Saldos(oExercici, (skipTancament = 1))
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els saldos de l'Exercici")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Exercici/RenumeraAssentaments/{emp}/{year}")>
    Public Function RenumeraAssentaments(emp As DTOEmp.Ids, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oExercici As New DTOExercici(oEmp, year)
            Dim value = BEBL.Exercici.RenumeraAssentaments(exs, oExercici)
            If exs.Count = 0 Then
                retval = Request.CreateResponse(HttpStatusCode.OK, value)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al renumerar els assentaments")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al renumerar els assentaments")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Exercici/RetrocedeixAssentamentsApertura/{emp}/{year}")>
    Public Function RetrocedeixAssentamentsApertura(emp As DTOEmp.Ids, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oExercici As New DTOExercici(oEmp, year)
            If BEBL.Exercici.RetrocedeixAssentamentsApertura(exs, oExercici) Then
                retval = Request.CreateResponse(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al retrocedir la apertura de l'Exercici")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al retrocedir la apertura de l'Exercici")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/Exercici/EliminaTancaments/{emp}/{year}")>
    Public Function EliminaTancaments(emp As DTOEmp.Ids, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oExercici As New DTOExercici(oEmp, year)
            If BEBL.Exercici.EliminaTancaments(exs, oExercici) Then
                retval = Request.CreateResponse(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al retrocedir el tancament de l'Exercici")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al retrocedir el tancament de l'Exercici")
        End Try
        Return retval
    End Function

End Class

Public Class ExercicisController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Exercicis/{emp}/{contact}/{cta}")>
    Public Function All(emp As DTOEmp.Ids, contact As Guid, cta As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oContact = DTOBaseGuid.Opcional(Of DTOContact)(contact)
            Dim oCta = DTOBaseGuid.Opcional(Of DTOPgcCta)(cta)
            Dim values = BEBL.Exercicis.All(oEmp, oContact, oCta)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els exercicis")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Exercicis/years/{emp}")>
    Public Function Years(emp As DTOEmp.Ids) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.Exercicis.Years(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els exercicis")
        End Try
        Return retval
    End Function

End Class