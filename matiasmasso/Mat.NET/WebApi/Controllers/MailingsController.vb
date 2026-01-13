Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Public Class MailingsController
    Inherits _BaseController

    <HttpPost>
    <Route("api/Mailing/{emp}")>
    Public Function Load(emp As DTOEmp.Ids, <FromBody()> ByVal oMailing As DTOMailing) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = MyBase.GetEmp(emp)
            BEBL.Mailing.Load(oEmp, oMailing)
            retval = Request.CreateResponse(Of DTOMailing)(HttpStatusCode.OK, oMailing)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al carregar el mailing")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Mailing/XarxaDistribuidors/{fch}")>
    Public Function XarxaDistribuidors(fch As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim values = BEBL.Mailing.XarxaDistribuidors(fch)
            retval = Request.CreateResponse(Of List(Of DTOLeadChecked))(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la xarxa de distribuidors")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Mailing/Reps")>
    Public Function Reps(<FromBody> values As DTOChannelsBrands) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oChannels = values.Channels
            Dim oBrands = values.Brands
            Dim oReps = BEBL.Mailing.Reps(oChannels, oBrands)
            retval = Request.CreateResponse(Of List(Of DTOLeadChecked))(HttpStatusCode.OK, oReps)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els reps")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Mailing/log")> 'DEPRECATED -> Passat a api/MailingLog
    Public Function MailingLog(<FromBody()> ByVal guid As Guid, users As IEnumerable(Of DTOUser)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Mailings.Log(guid, users, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al loguejar el mailing")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al loguejar el mailing")
        End Try
        Return retval
    End Function




End Class
