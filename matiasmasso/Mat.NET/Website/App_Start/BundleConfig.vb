Imports System.Web.Optimization

Public Module BundleConfig
    ' For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
    Public Sub RegisterBundles(ByVal bundles As BundleCollection)

        bundles.Add(New ScriptBundle("~/bundles/jquery").Include(
                    "~/Scripts/jquery-{version}.js"))

        bundles.Add(New ScriptBundle("~/bundles/jqueryval").Include(
                    "~/Scripts/jquery.validate*"))

        bundles.Add(New ScriptBundle("~/bundles/scripts").IncludeDirectory("~/Scripts", "*.js", True))
        bundles.Add(New StyleBundle("~/Media/Css/bundle").IncludeDirectory("~/Styles", "*.css", True))
        BundleTable.EnableOptimizations = True

    End Sub
End Module

