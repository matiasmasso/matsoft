Imports System.IO
Imports System.Net.Http
Imports System.Web.Http.Filters


Public Class CustomCompressionAttribute
    Inherits ActionFilterAttribute


    Public Overrides Sub OnActionExecuted(ByVal actionContext As HttpActionExecutedContext)
        Dim isCompressionSupported As Boolean = CompressionHelper.IsCompressionSupported()
        Dim acceptEncoding As String = HttpContext.Current.Request.Headers("Accept-Encoding")

        If isCompressionSupported Then
            Dim content = actionContext.Response.Content
            Dim byteArray = If(content Is Nothing, Nothing, content.ReadAsByteArrayAsync().Result)
            Dim memoryStream As MemoryStream = New MemoryStream(byteArray)

            If acceptEncoding.Contains("gzip") Then
                actionContext.Response.Content = New ByteArrayContent(CompressionHelper.Compress(memoryStream.ToArray(), False))
                actionContext.Response.Content.Headers.Remove("Content-Type")
                actionContext.Response.Content.Headers.Add("Content-encoding", "gzip")
                actionContext.Response.Content.Headers.Add("Content-Type", "application/json")
            Else
                actionContext.Response.Content = New ByteArrayContent(CompressionHelper.Compress(memoryStream.ToArray()))
                actionContext.Response.Content.Headers.Remove("Content-Type")
                actionContext.Response.Content.Headers.Add("Content-encoding", "deflate")
                actionContext.Response.Content.Headers.Add("Content-Type", "application/json")
            End If
        End If

        MyBase.OnActionExecuted(actionContext)
    End Sub


End Class



