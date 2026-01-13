Imports System.Web.Routing

Public Class MvcApplication
    Inherits System.Web.HttpApplication

    Protected Sub Application_Start()
        AreaRegistration.RegisterAllAreas()

        'afegit 27/10/2020 per evitar que ajax converteixi empty string values to null
        ModelMetadataProviders.Current = New EmptyStringDataAnnotationsModelMetadataProvider()

        Dim exs As New List(Of Exception)
        If InitApp(exs) Then
            RouteConfig.RegisterRoutes(RouteTable.Routes)
            BundleConfig.RegisterBundles(Optimization.BundleTable.Bundles)
        Else
            FEB2.Exceptions.LogError("Web application failed to start on Global.asax:", exs)
        End If

    End Sub

    Private Function InitApp(exs As List(Of Exception)) As Boolean
        DTOApp.Current = FEB2.App.InitializeSync(exs, DTOApp.AppTypes.Web, "https://matiasmasso-api.azurewebsites.net", 55836, False)

        GlobalVariables.Emp = FEB2.Emp.FindSync(DTOEmp.Ids.MatiasMasso, exs)
        With GlobalVariables.Emp
            .Org = FEB2.Contact.FindSync(.Org.Guid, exs)
            .Mgz = DTOMgz.FromContact(FEB2.Contact.FindSync(.Mgz.Guid, exs))
        End With

        Dim retval As Boolean = True
        Return retval
    End Function


    Sub Session_Start(sender As Object, e As System.EventArgs)
        Dim exs As New List(Of Exception)
        ContextHelper.RestoreSession(exs)
    End Sub

End Class


