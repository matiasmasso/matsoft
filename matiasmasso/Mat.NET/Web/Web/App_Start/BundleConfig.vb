Imports System.Web.Optimization

Public Class BundleConfig

    Public Shared Sub RegisterBundles(bundles As BundleCollection)
        bundles.Add(New ScriptBundle("~/bundles/scripts").IncludeDirectory("~/Scripts", "*.js", True))
        bundles.Add(New StyleBundle("~/Media/Css/bundle").IncludeDirectory("~/Styles", "*.css", True))
        BundleTable.EnableOptimizations = True
    End Sub

End Class
