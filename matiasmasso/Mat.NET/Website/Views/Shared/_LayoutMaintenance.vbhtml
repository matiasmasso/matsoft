<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>M+O | @ViewBag.Title</title>
    @Styles.Render("~/Content/css")
    <style>
        body {
            max-width:600px;
            margin:auto;
            min-height:100%;
            font-family: Arial, Helvetica, sans-serif;
        }
    </style>
</head>
<body>
    <div class="container body-content">
        @RenderBody()
        <hr />
        <footer>
            <p>&copy; @DTO.GlobalVariables.Now().Year - MATIAS MASSO, S.A.</p>
        </footer>
    </div>

    @Scripts.Render("~/bundles/jquery")
    @RenderSection("scripts", required:=False)
</body>
</html>
