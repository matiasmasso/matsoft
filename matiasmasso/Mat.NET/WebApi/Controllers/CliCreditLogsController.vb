Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class CliCreditLogController
    Inherits _BaseController


    <HttpGet>
    <Route("api/CliCreditLog/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.CliCreditLog.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el CliCreditLog")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/CliCreditLog/CurrentLog/{customer}")>
    Public Function CurrentLog(customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCcx As New DTOCustomer(customer)
            Dim value = BEBL.CliCreditLog.CurrentLog(oCcx)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el CliCreditLog")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/CliCreditLog")>
    Public Function Update(<FromBody> value As DTOCliCreditLog) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.CliCreditLog.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar el crèdit")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar el crèdit")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/CliCreditLog/delete")>
    Public Function Delete(<FromBody> value As DTOCliCreditLog) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.CliCreditLog.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar el crèdit")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar el crèdit")
        End Try
        Return retval
    End Function

End Class

Public Class CliCreditLogsController
    Inherits _BaseController



    <HttpGet>
    <Route("api/CliCreditLogs/{customer}")>
    Public Function All(customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer As New DTOCustomer(customer)
            Dim values = BEBL.CliCreditLogs.All(oCustomer)
            retval = Request.CreateResponse(Of List(Of DTOCliCreditLog))(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els log dels limits de credit a clients")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/CliCreditLogs/CaducaCredits")> 'Matsched
    Public Async Function CaducaCredits() As Threading.Tasks.Task(Of HttpResponseMessage)
        Dim retval As HttpResponseMessage = Nothing
        Try
            'TODO: ------------- Gestionar multiempresa
            Dim exs As New List(Of Exception)
            Dim oUser As DTOUser = BEBL.User.Find(DTOUser.Wellknowns.info)
            If Await BEBL.CliCreditLogs.CaducaCredits(oUser, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la CliCreditLog")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la CliCreditLog")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/CliCreditLogs/CreditLastAlbs/{emp}")>
    Public Function CreditLastAlbs(emp As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.CliCreditLogs.CreditLastAlbs(oEmp)
            retval = Request.CreateResponse(Of List(Of DTOCreditLastAlb))(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els DTOCreditLastAlb")
        End Try
        Return retval
    End Function


End Class
