@Code
    ' Dim oUser = Mvc.ContextHelper.FindUserSync()
    Dim oUser = Mvc.ContextHelper.GetUser()
End Code

<!DOCTYPE html>

<html lang="@Choose(Mvc.ContextHelper.lang().Id, "es", "ca", "en")">
<head>
    <title>@IIf(ViewBag.Title = "", "MATIAS MASSO, S.A.", ViewBag.Title)</title>

    @System.Web.Optimization.Styles.Render("~/Media/Css/bundle")
    @RenderSection("AdditionalMetaTags", False)
    @RenderSection("hreflang", required:=False)
    @Html.Partial("_Meta")
    @Html.Partial("_GoogleAnalytics")
    @Html.Partial("_Alexa")
    @RenderSection("Styles", False)

    <style>
                   body {
    max-width: 1200px;
    min-width: 320px;
	margin:auto;
    padding: 0;
    font-family: arial;
}
        main {
	display: block;
	height: auto;
	width:auto;
    min-height:400px;	
	padding:0 10px 10px;
    margin:auto;
}
    </style>

    <noscript>
        <style scoped>

            .JsEnabled {
                display: none;
            }

            .JsDisabled {
                display: block;
            }
        </style>
    </noscript>

</head>
<body>
    <header>
        @Html.Partial("_Header", oUser, New ViewDataDictionary())
    </header>

    <main class="Main JsEnabled">
        @RenderBody()
    </main>

    <section class="JsDisabled">
        @Html.Partial("_NoScript")
    </section>

    <footer id="footer">
        @Html.Partial("_Footer")
    </footer>

    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <!--
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    @@System.Web.Optimization.Scripts.Render("~/bundles/Scripts")
        -->


    @RenderSection("Scripts", False)

    <script type="text/jscript">
        $(document).ready(function () {

            $('#LangOption_ESP').click(function () {
                event.preventDefault();
                $.ajax({
                    url: "/home/ChangeLang",
                    data: {
                        langId: "ESP"
                    }
                });
                window.location.reload(false);
            })

            $('#LangOption_CAT').click(function () {
                event.preventDefault();
                $.ajax({
                    url: "/home/ChangeLang",
                    data: {
                        langId: "CAT"
                    }
                });
                window.location.reload(false);
            })

            $('#LangOption_ENG').click(function () {
                event.preventDefault();
                $.ajax({
                    url: "/home/ChangeLang",
                    data: {
                        langId: "ENG"
                    }
                });
                window.location.reload(false);
            })

            $('#logoff').click(function () {
                event.preventDefault();
                var deferred = jQuery.Deferred();
                $.ajax({
                    url: "/Account/Logoff",
                    success: function () {
                        window.location.href = '/';
                        deferred.resolve(result);
                    }
                })
                return deferred.promise();
            })


        });

    </script>
</body>

</html>
