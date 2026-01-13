<!DOCTYPE html>
@Code
    Dim exs As New List(Of Exception)
    Dim lang = If(ViewBag.Lang Is Nothing, ContextHelper.Lang, ViewBag.Lang)
End Code

<html lang="@lang.ISO6391()">
<head>
    <title>@IIf(ViewBag.Title = "", "MATIAS MASSO, S.A.", "M+O | " & ViewBag.Title)</title>
    @RenderSection("hreflang", required:=False)
    @RenderSection("AdditionalMetaTags", False)
    @Html.Partial("_Meta")
    @Html.Partial("_GoogleAnalytics")
    @Html.Partial("_Alexa")
    @Html.Partial("_Facebook_pixel")

    @RenderSection("Styles", False)

    <link href="~/Styles/Site.css" rel="stylesheet" />
    <link href="~/Styles/_Layout_2.css" rel="stylesheet" />
    <link href="~/Styles/CookieConsent.css" rel="stylesheet" />
    <link href="~/Styles/TopNavBar2.css" rel="stylesheet" />
    <link href="~/Media/Css/PopSideNav.css" rel="stylesheet" />

    <!-- Google Tag Manager -->
    <script>
        (function (w, d, s, l, i) {
            w[l] = w[l] || []; w[l].push({
                'gtm.start':
                    new Date().getTime(), event: 'gtm.js'
            }); var f = d.getElementsByTagName(s)[0],
                j = d.createElement(s), dl = l != 'dataLayer' ? '&l=' + l : ''; j.async = true; j.src =
                    'https://www.googletagmanager.com/gtm.js?id=' + i + dl; f.parentNode.insertBefore(j, f);
        })(window, document, 'script', 'dataLayer', 'GTM-M4SNQC6');</script>
    <!-- End Google Tag Manager -->

</head>

<body>
    <!-- Google Tag Manager (noscript) -->
    <noscript>
        <iframe src="https://www.googletagmanager.com/ns.html?id=GTM-M4SNQC6"
                height="0" width="0" style="display:none;visibility:hidden"></iframe>
    </noscript>
    <!-- End Google Tag Manager (noscript) -->

    <div class="body-content">

        @If Not ContextHelper.isTrimmed() Then

            @<header>
                <div Class="headerTopRow">

                    <!--logo-->
                    <div Class="logo">
                        <a href="/">
                            <img src="~/Media/Img/Logos/Logo60.jpg" width="20" height="20" alt="Logo de MATIAS MASSO, S.A." />
                            <Span Class="razonsocial">MATIAS MASSO, S.A.</Span>
                        </a>
                    </div>

                    <div Class="Hello">
                        @If Session("User") IsNot Nothing Then
                            @<a href="#" title="@CType(Session("User"), DTOUser).EmailAddress">@Html.Raw(String.Format("{0} {1}", lang.Tradueix("Hola", "Hola", "Hi", "Olá"), CType(Session("User"), DTOUser).NicknameOrElse))</a>
                        End If
                    </div>

                    <div Class="SearchBoxRow">
                        <!--search-->
                        <!--<form action='/search/index' name="SearchForm" method="post">
                            <input type="text" class="SearchBox hideOnMobile" name="SearchKey" />
                        </form>-->

                        <!--profile-->
                        @If ContextHelper.GetUser() Is Nothing Then
                            @<a href="/account/login" Class="avatar" title='@lang.Tradueix("iniciar sesión", "iniciar sessió", "log in")'>
                                <img src="/Media/Img/Misc/User.png" width="12" height="12" />
                            </a>
                        End If

                        <a href="#" id="Hamburger" title="menu">
                            <img Class="showOnMobile" src="~/Media/Img/Ico/hamburger32.png" width="32" height="32" alt="menu" />
                        </a>
                    </div>
                </div>

                <!--nav-->
                <nav>
                    @Html.Partial("_NavBar", ContextHelper.NavViewModel().TopNavBar(lang))
                </nav>
            </header>

        End If

        <main>
            @RenderBody()
        </main>

        @If Not ContextHelper.isTrimmed() Then

            @<div Class="popSidenav popdown">
                <div Class="popButtonsBar">
                    @*<form action='/search/index' name="SearchForm" method="post">
                        <input type="text" class="SearchBox" name="SearchKey" />
                    </form>

                    <a href="#" Class="searchbtn">
                        <img src="/Media/Img/Ico/magnifying-glass.jpg" alt='@ContextHelper.Tradueix("Buscar", "Cercar", "Search")' width="20" height="20" />
                    </a>

                    <a href="#" Class="closebtn">
                        &times;
                    </a>*@
                </div>
                @Html.Partial("_SideNav", ContextHelper.NavViewModel().SideNav(lang))
            </div>

            @<footer>
    <!-- Top footer -->

    <!--<div Class="topFooter hideOnMobile">-->
        <!--about-->
        <!--<div>
            <h3>@lang.Tradueix("Acerca de", "Qui som", "About", "Sobre nós").ToUpper()</h3>
            <div>
                @lang.Tradueix("MATIAS MASSO, S.A. importa marcas líderes en puericultura para su distribución en los mercados Español, Portugués y de Andorra.",
     "MATIAS MASSO, S.A. importa marques líders en puericultura per la seva distribució als mercats Espanyol, Portuguès y d'Andorra.",
     "MATIAS MASSO, S.A. builds child care leader brand local markets for Spain, Portugal and Andorra.",
     "MATIAS MASSO, S.A. importa marcas líderes em puericultura para a sua distribuição nos mercados de Espanha, Portugal e de Andorra")
                <a href="/about">
                    @lang.Tradueix("Leer más...", "Continuar llegint...", "ReadMore...", "Lêr mais...")
                </a>
            </div>
        </div>-->

        <!--help-->
        <!--<div>
            <h3>@lang.Tradueix("Ayuda", "Ajuda", "Help", "Ajuda").ToUpper()</h3>
            <div>
                <a href="/distribuidores">
                    @lang.Tradueix("¿Dónde comprar?", "On comprar?", "Where to buy?", "Onde comprar")
                </a>
            </div>
            <div>
                <a href="/instrucciones">
                    @lang.Tradueix("Manuales de usuario", "Manuals d'usuari", "User manuals", "Manuais de usuário")
                </a>
            </div>
            <div>
                <a href="/videos">
                    @lang.Tradueix("Vídeos")
                </a>
            </div>
            <div>
                <a href="/compatibilidad">
                    @lang.Tradueix("Listas de compatibilidad", "Llistes de compatibilitat", "Compatibility lists", "Listas de compatibilidade")
                </a>
            </div>
            <div>
                <a href="/blog/consultas">
                    @lang.Tradueix("Consultas", "Consultes", "Any questions?")
                </a>
            </div>
        </div>-->

        <!--contact-->
        <!--<div>
            <h3>@lang.Tradueix("Contacto", "Contacte", "Contact")</h3>
            <div>
                <a href='tel:@lang.Tradueix("+34932541522", "+34932541522", "+34932541522", "+351308806304")'>
                    <img src="~/Media/Img/Ico/phone.png" width="18" height="13" class="contact" />
                    @lang.Tradueix("932541522", "932541522", "(+34) 932541522", "(+351) 308.806.304")
                </a>
            </div>
            <div>
                <a href='mailto:@lang.Tradueix("info@matiasmasso.es", "info@matiasmasso.es", "info@matiasmasso.es", "info@matiasmasso.pt")'>
                    <img src="~/Media/Img/Ico/email.png" width="18" height="13" class="contact" />
                    @lang.Tradueix("info@matiasmasso.es", "info@matiasmasso.es", "info@matiasmasso.es", "info@matiasmasso.pt")
                </a>
            </div>
            <h3>@lang.Tradueix("Síguenos en: ", "Segueix-nos a:", "Follow us on:", "Segue-nos em:").ToUpper()</h3>
            <div Class="socialMedia">
                @Html.Partial("_SocialMedia")
            </div>
        </div>
    </div>

    <div Class="socialMediaMobile showOnMobile">
        @lang.Tradueix("Síguenos en:", "Segueix-nos a:", "Follow us on:", "Segue-nos em:")
    </div>
    <div Class="socialMediaMobile showOnMobile">
        @Html.Partial("_SocialMedia")
    </div>-->

    <!-- Bottom footer -->
    <!--<div Class="bottomFooter hideOnMobile"> -->
    <div Class="bottomFooter">
        <div Class="hideOnMobile">
            &copy; @DTO.GlobalVariables.Now().Year - MATIAS MASSO, S.A.
            <Span>
                @lang.Tradueix("Todos los derechos reservados", "Tots els drets reservats", "All rights reserved", "Todos os direitos reservados")
            </Span>
        </div>



        <div Class="legal">
            <a href="/avisolegal">@lang.Tradueix("Aviso Legal", "Avís Legal", "Legal Notice", "Aviso Legal")</a>
            <a href="/privacidad">@lang.Tradueix("Privacidad", "Privacitat", "Privacity", "Privacidade")</a>
            <a href="/cookies">@lang.Tradueix("Cookies")</a>
        </div>
    </div>

        <!-- extra bottom Feder ICEX for Portugal-->
    <!--@If ContextHelper.Domain.Id = DTOWebDomain.Ids.matiasmasso_pt Then
        @<div class="LogoFeder">
            <div class="LogoFederImg">
                <img src="~/Media/Img/Misc/logo_feder.png" />
            </div>
            <div>MATIAS MASSO, S.A., en el marco del Programa ICEX Next, ha contado con el apoyo de ICEX y con la cofinanciación del fondo europeo FEDER. La finalidad de este apoyo es contribuir al desarrollo internacional de la empresa y de su entorno</div>
        </div>

    End If-->
</footer>

        End If

    </div>

    @Html.Partial("_CookieConsentBanner")


    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script Async src="https://www.googletagmanager.com/gtag/js?id=AW-965897101"></script>
    <script src="/Scripts/GoogleAnalytics.js"></script>
    <script src="/Media/js/Site.js"></script>
    <script src="/Media/js/CookieConsent.js"></script>

    @RenderSection("Scripts", False)

    <Script>
            $(document).ready(function(){
                loadStoreLocatorCalltoAction();
            });

            $(document).on('click', '#Hamburger, .topnavbar .VerticalEllipsis', function () {
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

            $(document).on('click', '.dropdown2', function () {
                if ($(this).children(".dropdown-content2").is(":visible")) {
                    $(this).children(".dropdown-content2").hide();
                } else {
                    $('.dropdown2 .dropdown-content2:visible').hide();
                    $(this).children(".dropdown-content2").slideToggle(500);
                }
            });

            $(document).on('click', '.StoreLocatorCallToAction a', function () {
                gtag_report_conversion(window.location.href);
            });


            function gtag_report_conversion(url) {
                gtag('event', 'conversion', { 'send_to': 'AW-965897101/TpTICN7kn8kBEI3XycwD', 'value': 100.0, 'currency': 'EUR' }); return false;
            }

            function loadStoreLocatorCalltoAction() {
                $('.StoreLocatorCallToAction a').html('@lang.Tradueix("¿Dónde comprar?", "On comprar?", "Where to buy?", "Onde comprar")')
            }

            var resizeId;
            $(window).resize(function () {
                clearTimeout(resizeId);
                resizeId = setTimeout(doneResizing, 500);
            });


            function doneResizing() {
            }

            function setLangCookie(obj) {
                var cookieId = '@ContextHelper.Cookies.Lang';
                var tag = obj.value;

                if (tag == 'NON') {
                    deleteCookie(cookieId);
                    } else {
                    setCookie(cookieId, tag, 50 * 365);
                    }
                window.location.href = '/' + tag;
            }

        function setCookie(cname, cvalue, exdays) {
            var d = new Date();
            d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
            var expires = "expires=" + d.toUTCString();
            document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
        }

        function getCookie(cname) {
            var name = cname + "=";
            var ca = document.cookie.split(';');
            for (var i = 0; i < ca.length; i++) {
                var c = ca[i];
                while (c.charAt(0) == ' ') {
                    c = c.substring(1);
                }
                if (c.indexOf(name) == 0) {
                    return c.substring(name.length, c.length);
                }
            }
            return "";
        }

        function deleteCookie(cname) {
            document.cookie = cname + "=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
        }

    </Script>

</body>

</html>
