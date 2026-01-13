<!DOCTYPE html>

<html>
<head>
    <title>@iif(ViewBag.Title = "", "MATIAS MASSO, S.A.", ViewBag.Title)</title>

    @System.Web.Optimization.Styles.Render("~/Media/Css/bundle")
    @Html.Partial("_Meta")
    @RenderSection("Styles", False)

</head>
<body>
    <div>
        @RenderBody()
    </div>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <!--@@System.Web.Optimization.Scripts.Render("~/bundles/Scripts")-->
    @RenderSection("Scripts", False)

</body>
</html>
