<!DOCTYPE html>

<html>
<head>
    <title>@ViewBag.Title</title>
    @Html.Partial("_Meta")
    <link href="~/Media/Css/4moms.css" rel="stylesheet" />
    @RenderSection("Styles", False)

</head>
<body>
    <nav>
        @Html.Partial("Nav_", ViewData("eComPage"))
    </nav>
    <main>
        @RenderBody()
    </main>

    @System.Web.Optimization.Scripts.Render("~/bundles/Scripts")
    <script src="~/Media/js/iFrameResizer.ContentWindow.min.js"></script>
    <script>
    $(document).ready(function () {
        $(document).on('addItem', function (e, argument) {
            var url = '@url.action("addItem")';
            $('nav').load(url, argument);
        });
    });
    </script>    
    @RenderSection("Scripts", False)
</body>
</html>

