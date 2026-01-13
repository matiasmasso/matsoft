Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class DownloadController
    Inherits _BaseController


    <HttpGet>
    <Route("api/download/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oDownload = BEBL.Download.Find(guid)
            If oDownload Is Nothing Then
                retval = MyBase.HttpErrorResponseMessage("no s'ha trobat el document")
            Else
                retval = Request.CreateResponse(Of DTOProductDownload)(HttpStatusCode.OK, oDownload)
            End If

        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el document")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/download")>
    Public Function Upload() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)

        Dim resultHash As String = ""
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value = ApiHelper.Client.DeSerialize(Of DTOProductDownload)(json, exs)
            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar la documentació")
            Else
                If value.DocFile IsNot Nothing Then
                    value.DocFile.Thumbnail = oHelper.GetImage("docfile_thumbnail")
                    value.DocFile.Stream = oHelper.GetFileBytes("docfile_stream")
                    If DAL.ProductDownloadLoader.Update(value, exs) Then
                        result = Request.CreateResponse(HttpStatusCode.OK)
                    Else
                        result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar el docfile a DAL.ProductDownloadLoader")
                    End If
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error a DAL.ProductDownloadLoader")
        End Try

        Return result

    End Function


    <HttpPost>
    <Route("api/download/delete")>
    Public Function Delete(<FromBody()> ByVal value As DTOProductDownload) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Download.Delete(value, exs) Then
                retval = Request.CreateResponse(Of DTOProductDownload)(HttpStatusCode.OK, value)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "Error al eliminar el document")
            End If

        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar el document")
        End Try
        Return retval
    End Function



End Class


Public Class DownloadsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/downloads/{target}")>
    Public Function targetDownloads(target As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oTarget As New DTOBaseGuidCodNom(target)
            Dim values = BEBL.Downloads.All(oTarget)
            retval = Request.CreateResponse(Of List(Of DTOProductDownload))(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els documents")
        End Try
        Return retval
    End Function



    <HttpGet>
    <Route("api/downloads/fromProductOrParent/{product}")>
    Public Function FromProductOrParent(product As Guid) As List(Of DTOProductDownload)
        Dim oProduct As New DTOProduct(product)
        Dim retval As List(Of DTOProductDownload) = BEBL.Downloads.FromProductOrParent(oProduct)
        Return retval
    End Function
End Class