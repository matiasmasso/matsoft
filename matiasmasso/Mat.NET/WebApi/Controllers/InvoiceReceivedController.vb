Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class InvoiceReceivedController
    Inherits _BaseController

    <HttpGet>
    <Route("api/InvoiceReceived/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.InvoiceReceived.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la factura rebuda")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/InvoiceReceived")>
    Public Function Update(<FromBody> value As DTOInvoiceReceived) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.InvoiceReceived.Update(exs, value) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la factura rebuda")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la factura rebuda")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/InvoiceReceived/delete")>
    Public Function Delete(<FromBody> value As DTOInvoiceReceived) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.InvoiceReceived.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la factura rebuda")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la factura rebuda")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/InvoiceReceived/delivery/{invoice}/{user}")>
    Public Function Delivery(invoice As Guid, user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oInvoiceReceived = BEBL.InvoiceReceived.Find(invoice)
            Dim oUser = BEBL.User.Find(user)
            Dim value = BEBL.InvoiceReceived.Delivery(oInvoiceReceived, oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la factura rebuda")
        End Try
        Return retval
    End Function
End Class

Public Class InvoicesReceivedController
    Inherits _BaseController

    <HttpGet>
    <Route("api/InvoicesReceived/{year}")>
    Public Function All(year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.InvoicesReceived.All(year)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les factures rebudes")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/InvoicesReceived/FromConfirmation")>
    Public Function FromConfirmation(<FromBody> oConfirmation As DTOImportacio.Confirmation) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.InvoicesReceived.All(oConfirmation)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les factures confirmades")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/InvoicesReceived/delete")>
    Public Function Delete(<FromBody> values As List(Of DTOInvoiceReceived)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.InvoicesReceived.Delete(values, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar les factures")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar les factures")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/InvoicesReceived/SetPrevisions/{importacio}")>
    Public Function SetPrevisions(importacio As Guid, <FromBody> values As List(Of DTOInvoiceReceived)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oImportacio As New DTOImportacio(importacio)
            If BEBL.InvoicesReceived.SetPrevisions(exs, values, oImportacio) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al assignar la importacio a les factures")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al assignar la importacio a les factures")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/InvoicesReceived/ClearImportacio")>
    Public Function ClearImportacio(<FromBody> values As List(Of DTOInvoiceReceived)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.InvoicesReceived.ClearImportacio(exs, values) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al assignar la importacio a les factures")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al assignar la importacio a les factures")
        End Try
        Return retval
    End Function


End Class

