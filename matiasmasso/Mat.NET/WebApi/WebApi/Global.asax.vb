Imports System.Web.Http
Imports System.Web.Mvc
Imports System.Web.Optimization

Public Class WebApiApplication
    Inherits System.Web.HttpApplication

    Sub Application_Start()

        Dim exs As New List(Of Exception)
        If BLL.BLLApp.Initialize(DTOEmp.Ids.MatiasMasso, DTOSession.AppTypes.Wcf, DTOLang.Ids.ESP, DTOCur.Ids.EUR, exs) Then
            AreaRegistration.RegisterAllAreas()
            GlobalConfiguration.Configure(AddressOf WebApiConfig.Register)
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
            RouteConfig.RegisterRoutes(RouteTable.Routes)
            BundleConfig.RegisterBundles(BundleTable.Bundles)

            'SqlServerTypes.Utilities.LoadNativeAssemblies(Server.MapPath("~/bin"))
        Else
            'TODO: Log Event
        End If

    End Sub
End Class
