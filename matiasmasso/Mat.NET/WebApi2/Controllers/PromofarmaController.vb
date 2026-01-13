Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class PromofarmaController
    Inherits _BaseController


    <HttpGet>
    <Route("api/promofarma/feed")>
    Public Function Fetch() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(DTOEmp.Ids.MatiasMasso)
            Dim value = BEBL.Promofarma.Feed(oEmp.Mgz)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error on reading sale points")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("promofarma/feed/csv")>
    Public Function CsvNoApi() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(DTOEmp.Ids.MatiasMasso)
            Dim value = BEBL.Promofarma.Feed(oEmp.Mgz)

            Dim result As New HttpResponseMessage(HttpStatusCode.OK)
            result.Content = New StringContent(value.Csv.ToString(), Encoding.UTF8)
            result.Content.Headers.ContentType = New Headers.MediaTypeHeaderValue("text/csv")
            result.Content.Headers.ContentDisposition = New Headers.ContentDispositionHeaderValue("attachment") With {.FileName = "Export.csv"}
            Return result
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error on reading sale points")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/promofarma/feed/DisableCheaperThan")>
    Public Function DisableCheaperThan(<FromBody> min As Decimal) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(DTOEmp.Ids.MatiasMasso)
            Dim value = BEBL.Promofarma.DisableCheaperThan(min)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error on disabling")
        End Try
        Return retval
    End Function



    <HttpGet>
    <Route("api/promofarma/feed/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Promofarma.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Template")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/promofarma/feed")>
    Public Function Update(<FromBody> value As DTO.Integracions.Promofarma.Feed.Item) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Promofarma.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Template")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Template")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/promofarma/feed/enable/{enabled}")>
    Public Function Enable(enabled As Integer, <FromBody> values As List(Of DTO.Integracions.Promofarma.Feed.Item)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.PromofarmaFeeds.Enable(values, enabled = 1, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Template")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Template")
        End Try
        Return retval
    End Function


End Class

