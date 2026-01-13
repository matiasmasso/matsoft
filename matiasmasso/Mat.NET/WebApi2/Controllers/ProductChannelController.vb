Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class ProductChannelController
    Inherits _BaseController

    <HttpGet>
    <Route("api/ProductChannel/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.ProductChannel.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la ProductChannel")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/ProductChannel")>
    Public Function Update(<FromBody> value As DTOProductChannel) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.ProductChannel.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la ProductChannel")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la ProductChannel")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/ProductChannel/delete")>
    Public Function Delete(<FromBody> value As DTOProductChannel) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.ProductChannel.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la ProductChannel")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la ProductChannel")
        End Try
        Return retval
    End Function

End Class

Public Class ProductChannelsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/ProductChannels/FromProduct/{product}")>
    Public Function FromProduct(product As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProduct As New DTOProduct(product)
            Dim values = BEBL.ProductChannels.All(oProduct)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les ProductChannels")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ProductChannels/FromChannel/{channel}")>
    Public Function FromChannel(channel As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oChannel As New DTODistributionChannel(channel)
            Dim values = BEBL.ProductChannels.All(oChannel)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les ProductChannels")
        End Try
        Return retval
    End Function

End Class
