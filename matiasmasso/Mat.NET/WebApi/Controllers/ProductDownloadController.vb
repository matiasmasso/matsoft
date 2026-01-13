Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class ProductDownloadController
    Inherits _BaseController

    <HttpGet>
    <Route("api/ProductDownload/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.ProductDownload.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la ProductDownload")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/ProductDownload")>
    Public Function Upload() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value = ApiHelper.Client.DeSerialize(Of DTOProductDownload)(json, exs)
            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar la ProductDownload")
            Else
                If value.DocFile IsNot Nothing Then
                    value.DocFile.Thumbnail = oHelper.GetImage("docfile_thumbnail")
                    value.DocFile.Stream = oHelper.GetFileBytes("docfile_stream")
                End If

                If DAL.ProductDownloadLoader.Update(value, exs) Then
                    result = Request.CreateResponse(HttpStatusCode.OK)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar el docfile a DAL.ProductDownloadLoader")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error a DAL.ProductDownloadLoader")
        End Try

        Return result
    End Function


    <HttpPost>
    <Route("api/ProductDownload/delete")>
    Public Function Delete(<FromBody> value As DTOProductDownload) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.ProductDownload.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la ProductDownload")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la ProductDownload")
        End Try
        Return retval
    End Function

End Class

Public Class ProductDownloadsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/productdownloads/{target}")>
    Public Function All(target As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oTarget As New DTOBaseGuid(target)
            Dim values = BEBL.ProductDownloads.All(oTarget)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els documents")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/productdownloads/productmodels/{src}/{lang}")>
    Public Function ProductModels(src As DTOProductDownload.Srcs, lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oLang = DTOLang.Factory(lang)
            Dim values = BEBL.ProductDownloads.ProductModels(src, oLang)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els documents")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ProductDownloads/FromSrc/{src}")>
    Public Function FromSrc(src As DTOProductDownload.Srcs) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.ProductDownloads.All(src)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els documents")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ProductDownloads/FromProductOrChildren/{product}/{includeObsoletos}/{onlyConsumerEnabled}/{src}")>
    Public Function FromProductOrChildren(product As Guid, includeObsoletos As Integer, onlyConsumerEnabled As Integer, src As DTOProductDownload.Srcs) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProduct As New DTOProduct(product)
            Dim values = BEBL.ProductDownloads.FromProductOrChildren(oProduct, includeObsoletos, onlyConsumerEnabled, src)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els documents")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ProductDownloads/ExistsFromProductOrParent/{product}/{includeObsoletos}/{onlyConsumerEnabled}/{src}")>
    Public Function ExistsFromProductOrParent(product As Guid, includeObsoletos As Integer, onlyConsumerEnabled As Integer, src As DTOProductDownload.Srcs) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProduct As New DTOProduct(product)
            Dim value = BEBL.ProductDownloads.ExistsFromProductOrParent(oProduct, includeObsoletos, onlyConsumerEnabled, src)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els documents")
        End Try
        Return retval
    End Function






    <HttpGet>
    <Route("api/productdownloads/LastCompatibilityReport/{product}")>
    Public Function LastCompatibilityReport(product As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProduct As New DTOProduct(product)
            Dim values = BEBL.ProductDownloads.All(oProduct).Where(Function(x) x.Src = DTOProductDownload.Srcs.Compatibilidad And x.DocFile IsNot Nothing).ToList
            Select Case values.Count
                Case 0
                Case Else
                    Dim hash = values.OrderByDescending(Function(x) x.DocFile.Fch).First?.DocFile.Hash
                    Dim value As ImageMime = BEBL.DocFile.Stream(hash)
                    Dim filename = String.Format("M+O Listado de compatibilidad.{0}", value.Mime.ToString())
                    retval = MyBase.HttpFileStreamResponseMessage(value.ByteArray, filename)

                    'retval = MyBase.HttpPdfResponseMessage(value.DocFile.Stream, "Listado de compatibilidad.pdf")
            End Select

        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els documents")
        End Try
        Return retval
    End Function

End Class
