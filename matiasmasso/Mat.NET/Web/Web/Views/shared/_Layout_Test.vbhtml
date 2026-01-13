<!DOCTYPE html>

<html>
<head>
    <!--
    <title>@@IIf(ViewBag.Title = "", "MATIAS MASSO, S.A.", ViewBag.Title)</title>

    @@System.Web.Optimization.Styles.Render("~/Media/Css/bundle")
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width" />
        -->
    @Html.Partial("_Meta")
    @RenderSection("Styles", False)

</head>
<body>
    <div>
        @RenderBody()
    </div>

    <!--
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    @@System.Web.Optimization.Scripts.Render("~/bundles/Scripts")
    @@RenderSection("Scripts", False)
        -->
</body>
</html>
