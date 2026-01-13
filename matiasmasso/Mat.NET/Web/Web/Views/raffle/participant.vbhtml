@ModelType DTORaffleParticipant
@Code
    Layout = "~/Views/shared/_Layout.vbhtml"

    ViewBag.Title = Mvc.ContextHelper.Tradueix("Inscripción en sorteo", "Inscripció en sorteig", "Raffle subscription", "Inscrição no sorteio") & " | MATIAS MASSO, S.A."

    Dim hostName As String = HttpContext.Current.Request.Url.Host    '.Request.Url.Host
    Dim domain = Mvc.ContextHelper.Domain()


    Dim oBrand = Model.Raffle.Brand
    Dim oFacebookWidget As DTOrrssWidget = FEB2.Product.FacebookWidget(domain, oBrand)
End Code

<noscript><img height="1" width="1" alt="" style="display:none" src="https: //www.facebook.com/tr?ev=6016095764849&amp;cd[value]=0.01&amp;cd[currency]=EUR&amp;noscript=1" /></noscript>


<div id="fb-root"></div>

<fieldset id="Raffle_Duplicated">
    <legend>@Mvc.ContextHelper.Tradueix("Inscripción en sorteo", "Inscripció en sorteig", "Raffle subscription", "Inscrição no sorteio")</legend>
    <div>
        <p>
            @Mvc.ContextHelper.Tradueix("Confirmamos que ya estas participando en este sorteo desde el " & Format(Model.Fch, "dd/MM/yy") & " a las " & Format(Model.Fch, "HH:mm") & " con el número " & Model.Num, "Confirmem que ja estas participant en aquest sorteig desde el " & Format(Model.Fch, "dd/MM/yy") & " a les " & Format(Model.Fch, "HH:mm") & " amb el número " & Model.Num, "We are glad to confirm you are already participating on this raffle since " & Format(Model.Fch, "dd/MM/yy") & " at " & Format(Model.Fch, "HH:mm") & " with ticket number " & Model.Num)
        </p>
        <p>
            @Mvc.ContextHelper.Tradueix("Una vez celebrado el sorteo, publicaremos el ganador en", "Un cop celebrat el sorteig, publicarem el guanyador a", "Once the raffle is over, we will publish the winner at", "Uma vez se celebre o sorteio, publicaremos o nome do ganhador em")
            &nbsp;<a href='@FEB2.Raffles.Url(Mvc.ContextHelper.lang())'>@FEB2.Raffles.Url(Mvc.ContextHelper.lang(), True)</a>
        </p>
        <p>
            @Mvc.ContextHelper.Tradueix("Si te toca el premio, tienes 30 días para ponerte en contacto con nosotros en ", "Si et toca el premi, tens 30 dies per posar-te en contacte amb nosaltres a ", "If your name becomes published, you have 30 days to confirm your acceptance at ", "Se te toca o prémio, tens 30 dias para pôr-te em contacto connosco em ")
            <a href="mailto:marketing@matiasmasso.es">marketing@matiasmasso.es</a>
        </p>
        <p class="FbWarn">
                <span>
                    @Html.Raw(Mvc.ContextHelper.Tradueix("Recuerda que este es un sorteo exclusivo para seguidores de nuestros perfiles en Facebook y/o Instagram. Si aun no lo eres puedes seguirnos haciendo clic en los botones <b>Me gusta</b> (Facebook) o <b>Seguir</b> (Instagram)  en las siguientes páginas:",
                                                                        "Recorda que aquest és un sorteig exclusiu per seguidors dels nostres perfils a Facebook i/o Instagram. Si encara no ens segueixes, fes clic als botons <b>M'agrada</b> (Facebook) o <b>Seguir</b>(Instagram) a les següents pàgines:",
                                                                        "Please note this is a raffle restricted to our Facebook/Instagram followers. If you are not following us yet, you may clic <b>Like button</b> (Facebook) or <b>Follow</b> (Instagram) on next pages:",
                                                                        "Recorda que este é um sorteio exclusivo para os seguidores dos nossos perfis em Facebook/Instagram. Se ainda não o és podes seguir-nos fazendo clic no botão <b>Gosto</b> (Facebook) ou <b>Seguir</b> (Instagram) nas seguientes páginas:"))

                </span>
        </p>
        <p>
            @If Mvc.ContextHelper.Lang.IsPor() Then
                @<a href="https://www.facebook.com/matiasmasso.sa.pt" target="_blank">www.facebook.com/matiasmasso.sa.pt</a>
                @<br />
                @<a href="https://www.instagram.com/matiasmasso.sa_pt" target="_blank">www.instagram.com/matiasmasso.sa_pt</a>
            Else
                @<a href="https://www.facebook.com/matiasmasso.sa" target="_blank">www.facebook.com/matiasmasso.sa</a>
                @<br />
                @<a href="https://www.instagram.com/matiasmasso.sa" target="_blank">www.instagram.com/matiasmasso.sa</a>
            End If
            <br /><a href="@FEB2.Raffle.FacebookPage(Model.Raffle, Mvc.ContextHelper.Lang())" target="_blank">@FEB2.Raffle.FacebookPageLabel(Model.Raffle, Mvc.ContextHelper.Lang())</a>
            @If (oBrand.Is4moms() Or oBrand.IsBritaxRoemer) Then
                If Mvc.ContextHelper.Lang.IsPor() Then
                    If oBrand.IsBritaxRoemer Then
                        @<br />
                        @<a href="https://www.instagram.com/britaxroemer_pt" target="_blank">www.instagram.com/britaxroemer_pt</a>
                    ElseIf oBrand.Is4moms Then
                        @<br />
                        @<a href="https://www.instagram.com/4moms_pt" target="_blank">www.instagram.com/4moms_pt</a>
                    End If
                Else
                    If oBrand.IsBritaxRoemer Then
                        @<br />
                        @<a href="https://www.instagram.com/britaxroemer_es" target="_blank">www.instagram.com/britaxroemer_es</a>
                    ElseIf oBrand.Is4moms Then
                        @<br />
                        @<a href="https://www.instagram.com/4moms_es" target="_blank">www.instagram.com/4moms_es</a>
                    End If
                End If
            End If
        </p>

        <p>
            @Mvc.ContextHelper.Tradueix("Gracias por participar", "Gracies per participar", "Thanks for participating", "Obrigada por participar.")
        </p>
    </div>

</fieldset>


@Section Styles
    <style scoped>
        fieldset {
            min-height: 400px;
            margin-bottom: 20px;
            padding-bottom: 20px;
        }

        .FbWarn {
            color: red;
        }

        .banner {
            padding: 10px;
        }

            .banner img {
                width: 100%;
            }

        .EmailVerificationRequest input[type="email"] {
            width: 320px;
        }

        #FbVerificationButton {
            width: 298px;
            margin: 10px 0 10px 0;
        }

        .RaffleAreaDropdown {
            display: inline-block;
        }

        #bases {
            max-width: 460px;
            height: 200px;
            overflow: scroll;
            margin: auto;
            border: 1px solid gray;
        }

            #bases.h2 {
                font-size: 1.0em;
            }

        .GameOver {
            color: red;
        }

        .IsDuplicated {
            color: red;
        }

        .DistXX {
            display: block;
            margin: 10px 0 10px 0;
        }

            .DistXX:hover {
                color: red;
            }

            .DistXX::first-line {
                font-weight: 600;
            }

        /*
        Make the Facebook Like box responsive (fluid width)
        https://developers.facebook.com/docs/reference/plugins/like-box/

        This element holds injected scripts inside iframes that in
        some cases may stretch layouts. So, we're just hiding it.
        */

        #fb-root {
            display: none;
        }

        /* To fill the container and nothing else */

        .fb_iframe_widget, .fb_iframe_widget span, .fb_iframe_widget span iframe[style] {
            width: 100% !important;
        }
    </style>

End Section


@Section Scripts


    <script>

    (function (d, s, id) {
        var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) return;
        js = d.createElement(s); js.id = id;
        js.src = "//connect.facebook.net/@Html.Raw(DTOLang.Locale(Mvc.ContextHelper.lang()))/all.js#xfbml=1&appId=@oFacebookWidget.widgetId&version=v3.0";
        fjs.parentNode.insertBefore(js, fjs);
    }(document, 'script', 'facebook-jssdk'));


    </script>


    <!-- Facebook Conversion Code for Sorteo Advansafix -->
    <script>
        (function () {
            var _fbq = window._fbq || (window._fbq = []);
            if (!_fbq.loaded) {
                var fbds = document.createElement('script');
                fbds.async = true;
                fbds.src = '//connect.facebook.net/en_US/fbds.js';
                var s = document.getElementsByTagName('script')[0];
                s.parentNode.insertBefore(fbds, s);
                _fbq.loaded = true;
            }
        })();
        window._fbq = window._fbq || [];
        window._fbq.push(['track', '6016095764849', { 'value': '0.01', 'currency': 'EUR' }]);
    </script>

    <script src="~/Media/js/Raffle.Play.js"></script>

End Section


