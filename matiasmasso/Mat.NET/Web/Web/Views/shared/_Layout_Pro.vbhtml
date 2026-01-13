@Code
    Dim lang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)
End Code

<!DOCTYPE html>

<html lang="@Choose(Mvc.ContextHelper.Lang().id, "es", "ca", "en")">
<head>
    <title>@IIf(ViewBag.Title = "", "MATIAS MASSO, S.A.", "M+O | " & ViewBag.Title)</title>
    @RenderSection("hreflang", required:=False)
    @RenderSection("AdditionalMetaTags", False)
    @Html.Partial("_Meta")
    @Html.Partial("_GoogleAnalytics")
    @Html.Partial("_Alexa")
    @Html.Partial("_Facebook_pixel")

    @RenderSection("Styles", False)
    <!--
    @@System.Web.Optimization.Styles.Render("~/Media/Css/bundle")
        -->
    <style>
        .PageWrapperHorizontal {
            width: 100%;
            justify-content: space-between;
        }

        .Aside {
            display: flex;
            flex-direction: column;
            justify-content: start;
            width: 200px;
        }

            .Aside a {
                display: block;
                padding: 4px 7px 2px 4px;
            }

                .Aside a:hover {
                    color: #167ac6;
                }

        .MainContent {
            width: 100%;
        }


        .Grid {
            display: grid;
            margin-top: 20px;
        }

            .Grid .Row, .Grid .HeaderRow {
                display: contents;
            }

                .Grid .HeaderRow div {
                    color: gray;
                    font-weight: 600;
                    margin-bottom: 15px;
                }

                .Grid .Row:hover div {
                    background-color: aliceblue;
                    border-top: 1px solid lightblue;
                    border-bottom: 1px solid lightblue;
                    cursor: pointer;
                }

                .Grid .Row div {
                    border-top: 1px solid white;
                    border-bottom: 1px solid white;
                }

                .Grid .Row.Active div {
                    background-color: aliceblue;
                    border-top: 1px solid lightblue;
                    border-bottom: 1px solid lightblue;
                    cursor: pointer;
                }

        .ContextMenu {
            display: none;
            position: absolute;
            background-color: #f9f9f9;
            min-width: 160px;
            box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);
            padding: 12px 16px 12px 20px;
            z-index: 1;
        }

            .ContextMenu a {
                display: block;
                padding: 4px 7px 2px 4px;
            }

            .ContextMenu .Disabled {
                color: lightgray;
            }

                .ContextMenu .Disabled:hover {
                    color: lightgray;
                }


        @@media screen and (max-width: 900px) {
            .Hello {
                display: none;
            }
        }
    </style>

    <link href="~/Styles/_Layout_2.css" rel="stylesheet" />
    <link href="~/Styles/Site.css" rel="stylesheet" />
    <link href="~/Styles/TopNavBar2.css" rel="stylesheet" />

</head>

<body>
    <div class="body-content">
        <header>
            <div class="headerTopRow">

                <!--logo-->
                <div class="logo">
                    <a href="/">
                        <img src="~/Media/Img/Logos/Logo60.jpg" />
                        <span class="razonsocial">MATIAS MASSO, S.A.</span>
                    </a>
                </div>

                <div class="Hello">
                    @if Session("User") IsNot Nothing Then
                        @<a href="#" title="@CType(Session("User"), DTOUser).emailAddress">@Html.Raw(String.Format("{0} {1}", Mvc.ContextHelper.Tradueix("Hola", "Hola", "Hi", "Olá"), CType(Session("User"), DTOUser).NicknameOrElse))</a>
                    End If
                </div>


                <div class="searchBox">
                    <!--search-->
                    <form action='/search/index' name="searchForm" method="post">
                        <input type="text" class="hideOnMobile" name="SearchKey" onfocusout="document.searchForm.submit()" />
                    </form>

                    <!--profile-->
                    @If Mvc.ContextHelper.GetUser() Is Nothing Then
                        @<a href="/pro" Class="avatar" title='@Mvc.ContextHelper.Lang.Tradueix("iniciar sesión", "iniciar sessió", "log in")'>
                            <img src="/Media/Img/Misc/User.png" width="12" height="12" />
                        </a>
                    End If

                    <a href="#" id="hamburger">
                        <img class="showOnMobile" src="~/Media/Img/Ico/hamburger32.png" width="32" height="32" />
                    </a>
                </div>
            </div>

            <!--nav-->
            <nav>
                @Html.Partial("_NavBar", Mvc.ContextHelper.NavViewModel().TopNavBar(lang))
            </nav>
        </header>


        <main>

            <div class="PageWrapperHorizontal">

                @If ViewBag.SideMenuItems IsNot Nothing Then
                    @<div Class="Aside">
                        @For Each menuItem As DTOMenuItem In ViewBag.SideMenuItems
                            @<a href="@menuItem.Action">@menuItem.Caption</a>
                        Next
                    </div>
                End If

                <div Class="MainContent">
                    <h3 class="PageTitle">@ViewBag.Title</h3>

                    @RenderBody()
                </div>
            </div>
        </main>

        <footer>
            <!-- Top footer -->

            <div class="topFooter hideOnMobile">
                <!--about-->
                <div>
                    <h3>@Mvc.ContextHelper.Tradueix("Acerca de", "Qui som", "About", "Sobre nós").ToUpper()</h3>
                    <div>
                        @Mvc.ContextHelper.Tradueix("MATIAS MASSO, S.A. importa marcas líderes en puericultura para su distribución en los mercados Español, Portugués y de Andorra.",
          "MATIAS MASSO, S.A. importa marques líders en puericultura per la seva distribució als mercats Espanyol, Portuguès y d'Andorra.",
          "MATIAS MASSO, S.A. builds child care leader brand local markets for Spain, Portugal and Andorra.",
          "MATIAS MASSO, S.A. importa marcas líderes em puericultura para a sua distribuição nos mercados de Espanha, Portugal e de Andorra")
                        <a href="/about">
                            @Mvc.ContextHelper.Tradueix("Leer más...", "Continuar llegint...", "ReadMore...", "Lêr mais...")
                        </a>
                    </div>
                </div>

                <!--help-->
                <div>
                    <h3>@Mvc.ContextHelper.Tradueix("Ayuda", "Ajuda", "Help").ToUpper()</h3>
                    <div>
                        <a href="/distribuidores">
                            @Mvc.ContextHelper.Tradueix("¿Dónde comprar?", "On comprar?", "Where to buy?", "Onde comprar")
                        </a>
                    </div>
                    <div>
                        <a href="/instrucciones">
                            @Mvc.ContextHelper.Tradueix("Manuales de usuario", "Manuals d'usuari", "User manuals", "Manuais de usuário")
                        </a>
                    </div>
                    <div>
                        <a href="/videos">
                            @Mvc.ContextHelper.Tradueix("Vídeos")
                        </a>
                    </div>
                    <div>
                        <a href="/compatibilidad">
                            @Mvc.ContextHelper.Tradueix("Listas de compatibilidad", "Llistes de compatibilitat", "Compatibility lists", "Listas de compatibilidade")
                        </a>
                    </div>
                    <div>
                        <a href="/blog/consultas">
                            @Mvc.ContextHelper.Tradueix("Consultas", "Consultes", "Any questions?")
                        </a>
                    </div>
                </div>

                <!--contact-->
                <div>
                    <h3>@Mvc.ContextHelper.Tradueix("Contacto", "Contacte", "Contact")</h3>
                    <div>
                        <a href='tel:@Mvc.ContextHelper.Tradueix("+34932541522", "+34932541522", "+34932541522", "+351308806304")'>
                            <img src="~/Media/Img/Ico/phone.png" class="contact" />
                            @Mvc.ContextHelper.Tradueix("932541522", "932541522", "(+34) 932541522", "(+351) 308.806.304")
                        </a>
                    </div>
                    <div>
                        <a href='mailto:@Mvc.ContextHelper.Tradueix("info@matiasmasso.es", "info@matiasmasso.es", "info@matiasmasso.es", "info@matiasmasso.pt")'>
                            <img src="~/Media/Img/Ico/email.png" class="contact" />
                            @Mvc.ContextHelper.Tradueix("info@matiasmasso.es", "info@matiasmasso.es", "info@matiasmasso.es", "info@matiasmasso.pt")
                        </a>
                    </div>
                    <h3>@Mvc.ContextHelper.Tradueix("Síguenos en: ", "Segueix-nos a:", "Follow us on:", "Segue-nos em:").ToUpper()</h3>
                    <div class="socialMedia">
                        @Html.Partial("_SocialMedia")
                    </div>
                </div>
            </div>

            <div class="socialMediaMobile showOnMobile">
                @Mvc.ContextHelper.Tradueix("Síguenos en:", "Segueix-nos a:", "Follow us on:", "Segue-nos em:")
            </div>
            <div class="socialMediaMobile showOnMobile">
                @Html.Partial("_SocialMedia")
            </div>

            <!-- Bottom footer -->
            <div class="bottomFooter hideOnMobile">
                <div>
                    &copy; @DateTime.Now.Year - MATIAS MASSO, S.A.
                    <span class="hideOnMobile">
                        @Mvc.ContextHelper.Tradueix("Todos los derechos reservados", "Tots els drets reservats", "All rights reserved", "Todos os direitos reservados")
                    </span>
                </div>

                <div class="legal">
                    <a href="/avisolegal">@Mvc.ContextHelper.Tradueix("Aviso Legal", "Avís Legal", "Legal Notice", "Aviso Legal")</a>
                    <a href="/privacidad">@Mvc.ContextHelper.Tradueix("Privacidad", "Privacitat", "Privacity", "Privacidade")</a>
                    <a href="/cookies">@Mvc.ContextHelper.Tradueix("Cookies")</a>
                </div>
            </div>
        </footer>
    </div>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="~/Media/js/Site.js"></script>
    @RenderSection("Scripts", False)

    <script>


        $(document).on('click', '#hamburger', function () {
            $('nav').slideToggle(1000);
            var img = $(this).children().first();
            var src = img.attr("src");
            var isMenu = src.indexOf('hamburger') != -1;
            img.attr("src", isMenu ? src.replace('hamburger32.png', 'cross24.png') : src.replace('cross24.png', 'hamburger32.png'));

        });

        $(document).on('click', '.dropdown', function () {
            if ($(this).children(".dropdown-content").is(":visible")) {
                $(this).children(".dropdown - content").hide();
            } else {
                $('.dropdown .dropdown-content:visible').hide();
                $(this).children(".dropdown-content").slideToggle(500);
            }
        });

    </script>

</body>

</html>
