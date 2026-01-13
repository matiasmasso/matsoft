
@Code
    Dim oUser = ContextHelper.FindUserSync()
    Dim oBrowserCaps As System.Web.HttpBrowserCapabilitiesBase = Request.Browser
    Dim isMobileDevice As Boolean
    If oBrowserCaps IsNot Nothing Then
        isMobileDevice = oBrowserCaps.IsMobileDevice()
    End If


End Code

<!DOCTYPE html>

<html lang="@Choose(ContextHelper.lang().id, "es", "ca", "en")">
<head>
    <title>@IIf(ViewBag.Title = "", "MATIAS MASSO, S.A.", ViewBag.Title)</title>

    @System.Web.Optimization.Styles.Render("~/Media/Css/bundle")
    @RenderSection("hreflang", required:=False)

    @RenderSection("AdditionalMetaTags", False)
    @Html.Partial("_Meta")
    @Html.Partial("_GoogleAnalytics")
    @Html.Partial("_Alexa")
    @Html.Partial("_Facebook_pixel")

    @RenderSection("Styles", False)

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

    @If isMobileDevice Then
        @<main class="Main JsEnabled">
            @RenderBody()
        </main>
    Else
        @<section class="BodyGrid JsEnabled">
            <div class="BodyRow">
                <aside class="LeftColumn">
                    <!--@@Html.Partial("_AsideLeft", oUser, New ViewDataDictionary())-->
                </aside>

                <main class="Main">
                    @RenderBody()
                </main>

                <aside class="RightColumn">
                    @Html.Partial("_AsideRight", oUser, New ViewDataDictionary())
                </aside>
            </div>
        </section>
    End If

    <section class="JsDisabled">
        @Html.Partial("_NoScript")
    </section>

    <footer id="footer">
        @Html.Partial("_Footer")
    </footer>


    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    @System.Web.Optimization.Scripts.Render("~/bundles/Scripts")

    @RenderSection("Scripts", False)

    <script src="~/Media/js/GlobalSearchBar.js"></script>
    <script src="~/Media/js/speech-input.js"></script>
    <script async defer src="~/Media/js/speech-button.js"></script>


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

    <script type="application/ld+json">
        {
        "@@context": "http://schema.org",
        "@@type": "LocalBusiness",
        "address": {
        "@@type": "PostalAddress",
        "addressCountry": "ES",
        "addressLocality": "Barcelona",
        "streetAddress": "Diagonal 403",
        "postalCode": "08008"
        },
        "description": "Seguridad Infantil para el automóvil",
        "name": "MATIAS MASSO, S.A.",
        "telephone": "+34932541522"
        }
    </script>

    <link href="~/Media/Css/Speech-input.css" rel="stylesheet" />

</body>

</html>
