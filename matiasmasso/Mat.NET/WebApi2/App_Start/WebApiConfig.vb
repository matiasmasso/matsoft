Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web.Http
Imports System.Web.Http.Cors

Public Module WebApiConfig
    Public Sub Register(ByVal config As HttpConfiguration)
        ' Web API configuration and services
        '44332 DES DE LAPTOP
        '7285 des de laptop per PWA
        Dim Cors As New EnableCorsAttribute("https://localhost:7285, http://localhost:44397, https://localhost:44397, http://www.matiasmasso.es, https://www.matiasmasso.es, http://www.matiasmasso.pt, https://www.matiasmasso.pt, http://localhost:44365, https://localhost:44365, http://localhost:44332, https://localhost:44332", "*", "*")
        config.EnableCors(Cors)

        ' Web API routes
        config.MapHttpAttributeRoutes()

        config.Routes.MapHttpRoute(
            name:="DefaultApi",
            routeTemplate:="api/{controller}/{id}",
            defaults:=New With {.id = RouteParameter.Optional}
        )

        config.Formatters.JsonFormatter.SupportedMediaTypes.Add(New Net.Http.Headers.MediaTypeHeaderValue("text/html"))
        GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    End Sub
End Module
