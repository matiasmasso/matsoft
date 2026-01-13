Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class WebAtlasController
    Inherits _BaseController

    <HttpGet>
    <Route("api/webatlas/proveidor/{proveidor}/distributors")>
    Public Function forProveidor(proveidor As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProveidor = BEBL.Proveidor.Find(proveidor)
            Dim values = BEBL.WebAtlas.distribuidors(oProveidor)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les RepCustomers")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/webatlas/distribuidors/{product}/{provinciaOrZona}/{location}/{proveidor}/{lang}/{includeItems}/{latestPdcFchFrom}")>
    Public Function distribuidors(product As Guid, provinciaOrZona As Guid, location As Guid, proveidor As Guid, lang As String, includeItems As Integer, latestPdcFchFrom As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProduct = DTOBaseGuid.Opcional(Of DTOProduct)(product)
            Dim oProvinciaOrZona = DTOBaseGuid.Opcional(Of DTOArea)(provinciaOrZona)
            Dim oLocation = DTOBaseGuid.Opcional(Of DTOLocation)(location)
            Dim oProveidor = DTOBaseGuid.Opcional(Of DTOProveidor)(proveidor)
            Dim oLang = DTOLang.Factory(lang)
            Dim values = BEBL.WebAtlas.distribuidors(oProduct, oProvinciaOrZona, oLocation, oProveidor, oLang)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les RepCustomers")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/webatlas/update/{empId}")>
    Public Function Update(empId As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = MyBase.GetEmp(empId)
            If BEBL.WebAtlas.Update(oEmp, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al actualitzar els punts de venda")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al actualitzar els punts de venda")
        End Try
        Return retval
    End Function
End Class
