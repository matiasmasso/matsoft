Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class CsaController
    Inherits _BaseController


    <HttpGet>
    <Route("api/Csa/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Csa.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la remesa d'efectes")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Csa")>
    Public Function Update(<FromBody> value As DTOCsa) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Csa.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la remesa d'efectes")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la remesa d'efectes")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/Csa/delete")>
    Public Function Delete(<FromBody> value As DTOCsa) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Csa.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la remesa d'efectes")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la remesa d'efectes")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Csa/LaCaixaRemesaExportacio/{guid}")>
    Public Function LaCaixaRemesaExportacio(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oCsa = BEBL.Csa.Find(guid)
            Dim value = BEBL.LaCaixaRemesaExportacio.Text(oCsa, exs)
            If exs.Count = 0 Then
                retval = Request.CreateResponse(Of String)(HttpStatusCode.OK, value)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al generar fitxer remesa")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al generar fitxer remesa")
        End Try
        Return retval

    End Function
End Class


Public Class CsasController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Csas/emp/years/{emp}")>
    Public Function EmpYears(emp As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = GlobalVariables.Emp(emp)
            Dim iYears = BEBL.Csas.Years(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, iYears)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els anys de les remeses")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Csas/banc/years/{banc}")>
    Public Function BancYears(banc As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oBanc As New DTOBanc(banc)
            Dim iYears = BEBL.Csas.Years(oBanc)
            retval = Request.CreateResponse(HttpStatusCode.OK, iYears)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els anys de les remeses")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Csas/emp/{emp}/{year}")>
    Public Function EmpAll(emp As Integer, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = GlobalVariables.Emp(emp)
            Dim oCsas = BEBL.Csas.All(year, oEmp:=oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, oCsas)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les remeses bancaries")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Csas/banc/{banc}/{year}")>
    Public Function bancAll(banc As Guid, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oBanc As New DTOBanc(banc)
            Dim oCsas = BEBL.Csas.All(year, oBanc:=oBanc)
            retval = Request.CreateResponse(HttpStatusCode.OK, oCsas)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els anys de les remeses")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/Csas/SaveRemesaCobrament/{user}")>
    Public Function SaveRemesaCobrament(user As Guid, <FromBody> oCsa As DTOCsa) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oUser As DTOUser = BEBL.User.Find(user)
            Dim value = BEBL.Csa.SaveRemesaCobrament(oCsa, oUser, exs)
            If exs.Count = 0 Then
                retval = Request.CreateResponse(Of DTOCsa)(HttpStatusCode.OK, value)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la remesa d'efectes")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la remesa d'efectes")
        End Try
        Return retval

    End Function

    <HttpPost>
    <Route("api/Csas")>
    Public Function Update(<FromBody> oCsas As List(Of DTOCsa)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim value = BEBL.Csas.Update(oCsas, exs)
            If exs.Count = 0 Then
                Dim ids As List(Of Integer) = oCsas.Select(Function(x) x.Id).ToList
                retval = Request.CreateResponse(Of List(Of Integer))(HttpStatusCode.OK, ids)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar les remeses")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar les remeses")
        End Try
        Return retval

    End Function

    <HttpPost>
    <Route("api/Csas/Delete")>
    Public Function Delete(<FromBody> oCsas As List(Of DTOCsa)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim value = BEBL.Csas.Delete(oCsas, exs)
            If exs.Count = 0 Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar les remeses")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar les remeses")
        End Try
        Return retval

    End Function






End Class
