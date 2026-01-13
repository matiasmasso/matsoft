Imports System.Net
Imports System.Net.Http
Imports System.Web.Http


Public Class ImportPrevisionsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/ImportPrevisions/{sku}")>
    Public Function All(sku As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oSku = BEBL.ProductSku.Find(sku)
            Dim values = BEBL.ImportPrevisions.All(oSku)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les ImportPrevisions")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ImportPrevisions/load/{importacio}")>
    Public Function Load(importacio As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value As New DTOImportacio(importacio)
            BEBL.ImportPrevisions.Load(value)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Previsions")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/ImportPrevisions")>
    Public Function Update(<FromBody> value As DTOImportacio) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.ImportPrevisions.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la ImportPrevisions")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la ImportPrevisions")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/ImportPrevisions/UploadExcel/{importacio}")>
    Public Function UploadExcel(importacio As Guid, <FromBody> oSheet As MatHelper.Excel.Sheet) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oImportacio As New DTOImportacio(importacio)
            If BEBL.ImportPrevisions.UploadExcel(exs, oImportacio, oSheet) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al importar les previsions")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al importar les previsions")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/ImportPrevisions/delete")>
    Public Function Delete(<FromBody> values As List(Of DTOImportPrevisio)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.ImportPrevisions.Delete(values, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar les previsions")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar les previsions")
        End Try
        Return retval
    End Function

End Class
