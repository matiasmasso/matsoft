
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class ProductPluginController
    Inherits _BaseController

    <HttpGet>
    <Route("api/ProductPlugin/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.ProductPlugin.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la ProductPlugin")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ProductPlugin/sprite/{guid}")>
    Public Function Sprite(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.ProductPlugin.Sprite(guid)
            retval = MyBase.HttpImageResponseMessage(value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir l'sprite del Plugin")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/ProductPlugin")>
    Public Function Update(<FromBody> value As DTOProductPlugin) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.ProductPlugin.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la ProductPlugin")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la ProductPlugin")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/ProductPlugin/delete")>
    Public Function Delete(<FromBody> value As DTOProductPlugin) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.ProductPlugin.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la ProductPlugin")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la ProductPlugin")
        End Try
        Return retval
    End Function

End Class

Public Class ProductPluginsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/ProductPlugins/{product}")>
    Public Function All(product As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProduct As New DTOProduct(product)
            Dim values = BEBL.ProductPlugins.All(oProduct)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els ProductPlugins")
        End Try
        Return retval
    End Function

End Class
