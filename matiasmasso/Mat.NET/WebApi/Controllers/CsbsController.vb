Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class CsbController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Csb/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Csb.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Csb")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Csb/saveVto/{csb}/{user}")>
    Public Function saveVto(csb As Guid, user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCsb = BEBL.Csb.Find(csb)
            If oCsb Is Nothing Then
                retval = MyBase.HttpErrorResponseMessage("No s'ha trobat l'efecte original")
            Else
                Dim exs As New List(Of Exception)
                Dim oUser As New DTOUser(user)
                Dim oCtas = BEBL.PgcCtas.Current()
                Dim value = BEBL.Csb.SaveVto(oCsb, oUser, oCtas, exs)
                If exs.Count = 0 Then
                    retval = Request.CreateResponse(HttpStatusCode.OK, True)
                Else
                    retval = MyBase.HttpErrorResponseMessage(exs, "error al registrar el venciment")
                End If
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al registrar el venciment")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/Csb/revertVto/{cca}")>
    Public Function revertVto(cca As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCca = BEBL.Cca.Find(cca)
            If oCca Is Nothing Then
                retval = MyBase.HttpErrorResponseMessage("No s'ha trobat l'assentament original del venciment")
            Else
                Dim exs As New List(Of Exception)
                Dim value = BEBL.Csb.RevertVto(oCca, exs)
                If exs.Count = 0 Then
                    retval = Request.CreateResponse(HttpStatusCode.OK, True)
                Else
                    retval = MyBase.HttpErrorResponseMessage(exs, "error al revertir el venciment")
                End If
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al revertir el venciment")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/Csb/reclama/{user}/{csb}")>
    Public Function reclama(user As Guid, csb As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCsb = BEBL.Csb.Find(csb)
            If oCsb Is Nothing Then
                retval = MyBase.HttpErrorResponseMessage("No s'ha trobat l'efecte a reclamar")
            Else
                Dim exs As New List(Of Exception)
                Dim oUser = BEBL.User.Find(user)
                Dim value As DTOCca = BEBL.Csb.Reclama(exs, oUser, oCsb)
                If exs.Count = 0 Then
                    retval = Request.CreateResponse(HttpStatusCode.OK, value)
                Else
                    retval = MyBase.HttpErrorResponseMessage(exs, "error al reclamar l'efecte")
                End If
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al reclamar l'efecte")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Csb/RetrocedeixReclamacio/{user}/{csb}")>
    Public Function RetrocedeixReclamacio(user As Guid, csb As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCsb = BEBL.Csb.Find(csb)
            If oCsb Is Nothing Then
                retval = MyBase.HttpErrorResponseMessage("No s'ha trobat l'efecte original")
            Else
                Dim exs As New List(Of Exception)
                Dim oUser = New DTOUser(user)
                Dim value = BEBL.Csb.RetrocedeixReclamacio(oUser, oCsb, exs)
                If exs.Count = 0 Then
                    retval = Request.CreateResponse(HttpStatusCode.OK, True)
                Else
                    retval = MyBase.HttpErrorResponseMessage(exs, "error al retrocedir reclamacio de l'efecte")
                End If
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al retrocedir reclamacio de l'efecte")
        End Try
        Return retval
    End Function

End Class


Public Class CsbsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Csbs/{emp}/{banc}/{year}")>
    Public Function All(emp As Integer, banc As Guid, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oBanc = New DTOBanc(banc)
            Dim values = BEBL.Csbs.All(oEmp, oBanc, year)
            If exs.Count = 0 Then
                retval = Request.CreateResponse(HttpStatusCode.OK, values)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "Error al llegir els efectes presentats a un banc")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els efectes presentats a un banc")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Csbs/CsbResults/{emp}")>
    Public Function CsbResults(emp As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = New DTOEmp(emp)
            Dim values = BEBL.Csbs.CsbResults(oEmp)
            If exs.Count = 0 Then
                retval = Request.CreateResponse(HttpStatusCode.OK, values)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "Error al llegir els resultats dels efectes per venciment")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els resultats dels efectes per venciment")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Csbs/FromCustomer/{emp}/{customer}")>
    Public Function All(emp As Integer, customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oCustomer = New DTOCustomer(customer)
            Dim values = BEBL.Csbs.All(oEmp, customer:=oCustomer)
            If exs.Count = 0 Then
                retval = Request.CreateResponse(HttpStatusCode.OK, values)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "Error al llegir els efectes lliurats a un client")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els efectes lliurats a un client")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/Csbs/FromIban/{iban}")>
    Public Function All(iban As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oIban = BEBL.Iban.Find(iban)
            Dim values = BEBL.Csbs.All(oIban)
            If exs.Count = 0 Then
                retval = Request.CreateResponse(HttpStatusCode.OK, values)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "Error al llegir els efectes lliurats a un compte corrent")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els efectes lliurats a un compte corrent")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/Csbs/PendentsDeGirar/{emp}/{country}/{sepa}")>
    Public Function PendentsDeGirar(emp As Integer, country As Guid, sepa As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oCountry As DTOCountry = Nothing
            If Not country.Equals(Guid.Empty) Then oCountry = New DTOCountry(country)
            Dim blSepa = (sepa = 1)
            Dim values = BEBL.Csbs.PendentsDeGirar(oEmp, exs, oCountry, blSepa)
            If exs.Count = 0 Then
                retval = Request.CreateResponse(HttpStatusCode.OK, values)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "Error al llegir les partides pendents de girar")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les partides pendents de girar")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/Csbs/PendentsDeVto/{emp}/{fch}")>
    Public Function PendentsDeVto(emp As Integer, fch As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.Csbs.PendentsDeVto(oEmp, fch)
            If exs.Count = 0 Then
                retval = Request.CreateResponse(HttpStatusCode.OK, values)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "Error al llegir les partides pendents de vencer")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les partides pendents de vencer")
        End Try
        Return retval
    End Function




    <HttpGet>
    <Route("api/csbs/NotifyVtos/{emp}")> 'Matsched
    Public Function NotifyVtos(emp As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = MyBase.GetEmp(emp)
            If BEBL.Csbs.NotifyVtos(exs, oEmp) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Csb")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Csb")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/csbs/mailingLogs/{emp}/{year}")> 'Matsched
    Public Function mailingLogs(emp As DTOEmp.Ids, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp As New DTOEmp(emp)
            Dim values = BEBL.Csbs.mailingLogs(oEmp, year)
            retval = Request.CreateResponse(Of List(Of DTOCsb))(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Csb")
        End Try
        Return retval
    End Function


End Class
