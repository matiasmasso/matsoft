Imports System.Web.Http
Imports System.Web.Optimization
Imports System.Web.Mvc
Imports System.Web.Routing

Public Class WebApiApplication
    Inherits System.Web.HttpApplication

    Sub Application_Start()
        AreaRegistration.RegisterAllAreas()
        GlobalConfiguration.Configure(AddressOf WebApiConfig.Register)
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)

        Dim exs As New List(Of Exception)
        If BLL.BLLApp.Initialize(DTOEmp.Ids.MatiasMasso, DTOSession.AppTypes.WebApi, DTOLang.Ids.ESP, DTOCur.Ids.EUR, exs) Then
            RouteConfig.RegisterRoutes(RouteTable.Routes)
            BundleConfig.RegisterBundles(BundleTable.Bundles)
        Else
            BLLExceptions.LogError("Web application failed to start on Global.asax:", exs)
        End If

    End Sub
End Class
