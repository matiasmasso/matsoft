
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class ProductPluginItemController
    Inherits _BaseController

    <HttpGet>
    <Route("api/ProductPluginItem/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.ProductPluginItem.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la ProductPluginItem")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/ProductPluginItem")>
    Public Function Update(<FromBody> value As DTOProductPlugin.Item) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.ProductPluginItem.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la ProductPluginItem")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la ProductPluginItem")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/ProductPluginItem/delete")>
    Public Function Delete(<FromBody> value As DTOProductPlugin.Item) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.ProductPluginItem.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la ProductPluginItem")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la ProductPluginItem")
        End Try
        Return retval
    End Function

End Class

