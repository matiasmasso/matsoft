Imports System.Web.Optimization

Public Class MvcApplication
    Inherits System.Web.HttpApplication

    Protected Sub Application_Start()
        AreaRegistration.RegisterAllAreas()

        'afegit 27/10/2020 per evitar que ajax converteixi empty string values to null
        ModelMetadataProviders.Current = New EmptyStringDataAnnotationsModelMetadataProvider()

        Dim exs As New List(Of Exception)
        If InitApp(exs) Then
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
            RouteConfig.RegisterRoutes(RouteTable.Routes)
            BundleConfig.RegisterBundles(BundleTable.Bundles)
        Else
            FEB.Exceptions.LogError("Web application failed to start on Global.asax:", exs)
        End If

    End Sub

    Private Function InitApp(exs As List(Of Exception)) As Boolean
        DTOApp.Current = FEB.App.InitializeSync(exs, DTOApp.AppTypes.web, "https://api.matiasmasso.es", 55836, False)

        Website.GlobalVariables.Emp = FEB.Emp.FindSync(DTOEmp.Ids.MatiasMasso, exs)
        With Website.GlobalVariables.Emp
            .Org = FEB.Contact.FindSync(.Org.Guid, exs)
            .Mgz = DTOMgz.FromContact(FEB.Contact.FindSync(.Mgz.Guid, exs))
        End With
        FEB.Cache.FetchSync(exs, Website.GlobalVariables.Emp)

        Dim retval As Boolean = True
        Return retval
    End Function


    Sub Session_Start(sender As Object, e As System.EventArgs)
        Dim exs As New List(Of Exception)
        ContextHelper.RestoreSession(exs)
    End Sub
End Class
