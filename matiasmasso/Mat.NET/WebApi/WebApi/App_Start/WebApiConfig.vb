Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web.Http

Public Module WebApiConfig
    Public Sub Register(ByVal config As HttpConfiguration)
        ' Web API configuration and services

        ' Web API routes
        config.MapHttpAttributeRoutes()

        'config.Routes.MapHttpRoute(
        'name:="DefaultApi",
        'routeTemplate:="api/{controller}/{id}",
        'defaults:=New With {.id = RouteParameter.Optional}
        ')

        'to ensure Json output
        config.Formatters.Remove(config.Formatters.XmlFormatter)
        config.Formatters.JsonFormatter.SupportedMediaTypes.Add(New System.Net.Http.Headers.MediaTypeHeaderValue("text/html"))
    End Sub
End Module
