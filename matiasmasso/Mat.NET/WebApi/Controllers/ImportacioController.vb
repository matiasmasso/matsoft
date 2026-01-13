Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class ImportacioController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Importacio/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Importacio.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Importacio")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Importacio/FromDelivery/{delivery}")>
    Public Function FromDelivery(delivery As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oDelivery As New DTODelivery(delivery)
            Dim value = BEBL.Importacio.FromDelivery(oDelivery)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Importacio")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Importacio")>
    Public Function Update(<FromBody> value As DTOImportacio) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            value.restoreObjects()
            If BEBL.Importacio.Update(value, exs) Then
                retval = Request.CreateResponse(Of Integer)(HttpStatusCode.OK, value.id)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Importacio")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Importacio")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Importacio/validateCamion")>
    Public Function validateCamion(<FromBody> value As DTOImportacio.Confirmation) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Importacio.validateCamion(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al validar la entrada")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al validar la entrada")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Importacio/Entrada")>
    Public Function Entrada(<FromBody> value As DTODelivery) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Importacio.Entrada(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la entrada")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la entrada")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Importacio/LogAvisTrp/{importacio}")>
    Public Function LogAvisTrp(importacio As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim value = BEBL.Importacio.Find(importacio)
            If BEBL.Importacio.LogAvisTrp(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al registrar avis al transport")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al registrar avis al transport")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Importacio/delete")>
    Public Function Delete(<FromBody> value As DTOImportacio) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Importacio.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Importacio")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Importacio")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Importacio/confirm")>
    Public Function Confirm(<FromBody> value As DTOImportacio.Confirmation) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Importacio.Confirm(exs, value) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al processar la confirmacio")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al processar la confirmacio")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Importacio/unconfirm/{importacio}")>
    Public Function UnConfirm(importacio As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oImportacio As New DTOImportacio(importacio)
            If BEBL.Importacio.UnConfirm(exs, oImportacio) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al retrocedir la confirmacio")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al retrocedir la confirmacio")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Importacio/previsions/revert/{importacio}")>
    Public Function RevertPrevisions(importacio As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oImportacio As New DTOImportacio(importacio)
            If BEBL.Importacio.RevertPrevisions(exs, oImportacio) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al retrocedir les previsions")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al retrocedir les previsions")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Importacio/invoicesReceived/{importacio}")>
    Public Function InvoicesReceived(importacio As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oImportacio As New DTOImportacio(importacio)
            Dim values = BEBL.InvoicesReceived.All(oImportacio:=oImportacio)
            retval = Request.CreateResponse(Of List(Of DTOInvoiceReceived))(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les factures de la importacio")
        End Try
        Return retval
    End Function

End Class

Public Class ImportacionsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Importacions/{emp}/{year}/{proveidor}")>
    Public Function All(emp As Integer, year As Integer, proveidor As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oProveidor = DTOBaseGuid.Opcional(Of DTOProveidor)(proveidor)
            Dim values = BEBL.Importacions.All(oEmp, year, oProveidor)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Importacions")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/Importacions/weeks/{emp}")>
    Public Function Weeks(emp As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.Importacions.Weeks(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Importacions")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Importacions/transits/{emp}")>
    Public Function Transits(emp As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.Importacions.Transits(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Importacions")
        End Try
        Return retval
    End Function


End Class