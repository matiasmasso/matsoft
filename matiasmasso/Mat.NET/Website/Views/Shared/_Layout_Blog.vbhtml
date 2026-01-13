@Code
    Dim lang = If(ViewBag.Lang Is Nothing, ContextHelper.Lang, ViewBag.Lang)
End Code

<!DOCTYPE html>

<html lang="@lang.ISO6391()">

<head>
    <title>@IIf(ViewBag.Title = "", "El Blog de Matias Massó", "M+O Blog | " & ViewBag.Title)</title>
    @RenderSection("AdditionalMetaTags", False)
    @Html.Partial("_Meta")
    @Html.Partial("_GoogleAnalytics")
    @Html.Partial("_Alexa")
    <!--
       @@Html.Partial("_Facebook_pixel")
    -->

    @RenderSection("Styles", False)
    <!--
    @@System.Web.Optimization.Styles.Render("~/Media/Css/bundle")
        -->
    <link href="~/Styles/_Layout_blog.css" rel="stylesheet" />
    <link href="~/Styles/Site.css" rel="stylesheet" />
    <link href="~/Media/Css/PopSideNav.css" rel="stylesheet" />
    <style>
    </style>
</head>

<body>
    <div class="body-content">
        @If Not ContextHelper.isTrimmed() Then
            @<header>

                <div Class="banner">
                    <a href="/blog" title='@ContextHelper.Tradueix("El blog de Matías Massó", "El blog de Matías Massó", "Matías Massó blog", "O blog de Matias Massó")'>
                        <img src="~/Media/Img/Portada/BlogBanner-950.jpg" width="950" height="338" alt='@ContextHelper.Tradueix("El blog de Matías Massó", "El blog de Matías Massó", "Matías Massó blog", "O blog de Matias Massó")' />
                        <div class="BlogTitle">@ContextHelper.Tradueix("El blog de Matías Massó", "El blog de Matías Massó", "Matías Massó blog", "O blog de Matias Massó")</div>
                    </a>
                    <a href="#" id="Hamburger" title="menu">
                        <img src="~/Media/Img/Ico/hamburger32.png" width="32" height="32" alt="menu" />
                    </a>
                </div>

                <nav>
                    @Html.Partial("_NavBar-Blog")
                </nav>

            </header>
        End If

        <main>
            @RenderBody()
        </main>

        @If Not ContextHelper.isTrimmed() Then
            @<div class="popSidenav popdown">
                <div class="popButtonsBar">
                    <form action='/search/index' name="SearchForm" method="post">
                        <input type="text" class="SearchBox" name="SearchKey" />
                    </form>

                    <a href="#" class="searchbtn">
                        <img src="~/Media/Img/Ico/magnifying-glass.jpg" alt='@ContextHelper.Tradueix("Buscar", "Cercar", "Search")' width="20" height="20" />
                    </a>

                    <a href="#" class="closebtn">
                        &times;
                    </a>
                </div>
                @Html.Partial("_SideNav-Blog", ContextHelper.NavViewModel().SideNav(lang))
            </div>

            @<footer>

                <!-- Bottom footer -->
                <div class="bottomFooter">
                    <div>
                        &copy; @DTO.GlobalVariables.Now().Year - MATIAS MASSO, S.A.
                        <span class="AllRightsReserved">
                            @ContextHelper.Tradueix("Todos los derechos reservados", "Tots els drets reservats", "All rights reserved", "Todos os direitos reservados")
                        </span>
                    </div>

                    <div class="Breadcrumbs">
                        <a href="/avisolegal">@ContextHelper.Tradueix("Aviso Legal", "Avís Legal", "Legal Notice", "Aviso Legal")</a>
                        <a href="/privacidad">@ContextHelper.Tradueix("Privacidad", "Privacitat", "Privacity", "Privacidade")</a>
                        <a href="/cookies">@ContextHelper.Tradueix("Cookies")</a>
                    </div>
                </div>
            </footer>
        End If
    </div>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="~/Media/js/Site.js"></script>
    @RenderSection("Scripts", False)

    <script>

                                                                                                    $(document).ready(function () {
                                                                                                        loadStoreLocatorCalltoAction()
                                                                                                    });

                                                                                                    $(document).on('click', '#Hamburger', function () {
                                                                                                        $('.popSidenav').toggleClass('popup popdown');
                                                                                                    });

                                                                                                    $(document).on('click', '.closebtn', function () {
                                                                                                        $('.popSidenav').toggleClass('popup popdown');
                                                                                                        $('.popSidenav').removeClass('PopupSearch');
                                                                                                        $('.popSidenav .SearchBox').removeClass('PopupSearch');
                                                                                                        $('.popSidenav .searchbtn').show();
                                                                                                    });

                                                                                                    $(document).on('click', '.searchbtn', function () {
                                                                                                        $('.popSidenav').toggleClass('PopupSearch');
                                                                                                        $('.popSidenav .SearchBox').toggleClass('PopupSearch');
                                                                                                        $(this).hide();
                                                                                                    });

                                                                                                    $(document).on('click', '.dropdown', function () {
                                                                                                        if ($(this).children(".dropdown-content").is(":visible")) {
                                                                                                            $(this).children(".dropdown - content").hide();
                                                                                                        } else {
                                                                                                            $('.dropdown .dropdown-content:visible').hide();
                                                                                                            $(this).children(".dropdown-content").slideToggle(500);
                                                                                                        }
                                                                                                    });

                                                                                                    $(document).on('click', '.StoreLocatorCallToAction a', function () {
                                                                                                        gtag_report_conversion(window.location.href);
                                                                                                    });


                                                                                                    function gtag_report_conversion(url) {
                                                                                                        gtag('event', 'conversion', { 'send_to': 'AW-965897101/TpTICN7kn8kBEI3XycwD', 'value': 100.0, 'currency': 'EUR' }); return false;
                                                                                                    }

                                                                                                    function loadStoreLocatorCalltoAction() {
                                                                                                        $('.StoreLocatorCallToAction a').html('@ContextHelper.Tradueix("¿Dónde comprar?", "On comprar?", "Where to buy?", "Onde comprar")')
                                                                                                    }



    </script>

</body>

</html>
