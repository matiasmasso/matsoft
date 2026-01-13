Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class RepliqController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Repliq/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Repliq.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Repliq")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Repliq/pdf/{guid}")>
    Public Function Pdf(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oRepLiq As New DTORepLiq(guid)
            Dim value = BEBL.Repliq.Pdf(oRepLiq)
            retval = MyBase.HttpPdfResponseMessage(value, "M+O-Liquidación de comisiones")
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Repliq")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Repliq/header/{emp}/{invoice}/{rep}")>
    Public Function header(emp As DTOEmp.Ids, invoice As Guid, rep As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oInvoice As New DTOInvoice(invoice)
            Dim oRep As New DTORep(rep)
            Dim value = BEBL.Repliq.Header(oEmp, oInvoice, oRep)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Repliq")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Repliq")>
    Public Function Update(<FromBody> value As DTORepLiq) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Repliq.Update(exs, value) Then
                retval = Request.CreateResponse(Of DTORepLiq)(HttpStatusCode.OK, value)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Repliq")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Repliq")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Repliq/delete")>
    Public Function Delete(<FromBody> value As DTORepLiq) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Repliq.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Repliq")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Repliq")
        End Try
        Return retval
    End Function

End Class

Public Class RepliqsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Repliqs/{user}")>
    Public Function Model(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim values = BEBL.RepLiqs.Model(oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les liquidacionss")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Repliqs/headers/{emp}")>
    Public Function Headers(emp As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.RepLiqs.Headers(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les liquidacionss")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Repliqs/headers/FromRep/{rep}")>
    Public Function FromRep(rep As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oRep As New DTORep(rep)
            Dim values = BEBL.RepLiqs.Headers(oRep)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les liquidacionss")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Repliqs/headers/FromUser/{user}")>
    Public Function FromUser(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser As New DTOUser(user)
            Dim values = BEBL.RepLiqs.Headers(oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les liquidacionss")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Repliqs/delete")>
    Public Function Delete(<FromBody> values As List(Of DTORepLiq)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.RepLiqs.Delete(exs, values) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar les Repliqs")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar les Repliqs")
        End Try
        Return retval
    End Function




End Class
