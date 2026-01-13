@ModelType DTORaffleParticipant
@Code
    Layout = "~/Views/shared/_Layout.vbhtml"

    ViewData("Title") = Mvc.ContextHelper.tradueix("Inscripción en sorteo", "Inscripció en sorteig", "Raffle subscription", "Inscrição no sorteio") & " | MATIAS MASSO, S.A."

    Dim hostName As String = HttpContext.Current.Request.Url.Host    '.Request.Url.Host
    Dim domain As DTOWebPageAlias.domains = FEB2.UrlHelper.domain(hostName)


    Dim oUser = Mvc.ContextHelper.FindUserSync()
    Dim oBrand = Model.Raffle.brand
    Dim oFacebookWidget As DTOrrssWidget = FEB2.Product.FacebookWidget(domain, oBrand)

    Dim exs As New List(Of Exception)
    Dim oZonas = FEB2.Raffle.ZonasSync(exs, Mvc.GlobalVariables.Emp, Model.Raffle)
End Code

@Section AdditionalMetaTags
    <meta property="og:title" content="¡Participa en este fantástico sorteo!" />
    <meta property="og:site_name" content="M+O Sorteos y concursos" />
    <meta property="og:url" content="@FEB2.Raffle.PlayOrZoomUrl(Model.Raffle, True)" />
    <meta property="og:description" content="@Model.Raffle.Title" />
    <meta Property="og:locale" content="@DTOLang.Locale(Mvc.ContextHelper.lang())" />
    <meta Property="fb:app_id" content="@FEB2.Facebook.AppId(FEB2.Facebook.AppIds.MatiasMasso)" />
    <meta Property="article:publisher" content="https://www.facebook.com/matiasmasso.sa" />
    <meta Property="og:image" content="@FEB2.Raffle.ImgCallAction500(Model.Raffle, True)" />
End Section



<noscript><img height="1" width="1" alt="" style="display:none" src="https: //www.facebook.com/tr?ev=6016095764849&amp;cd[value]=0.01&amp;cd[currency]=EUR&amp;noscript=1" /></noscript>


<div id="fb-root"></div>





<fieldset id="Raffle_Steps">
    <legend>@Mvc.ContextHelper.tradueix("Inscripción en sorteo", "Inscripció en sorteig", "Raffle subscription", "Inscrição no sorteio")</legend>

    <div>
        <div class="banner">
            <img src="@FEB2.Raffle.ImgBanner600Url(Model.Raffle)" />
        </div>

        @If DTORaffle.IsOver(Model.Raffle) Then

            @<div class="GameOver">
                <p>
                    @Mvc.ContextHelper.tradueix("Este sorteo caducó el ", "Aquest sorteig va caducar el ", "This raffle is over since ", "Este sorteio caducou a ")
                    @Model.Raffle.fchTo.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"))
                    <br />
                    @Mvc.ContextHelper.tradueix("Comprueba si hay alguno en marcha en ", "Comprova si n'hi ha algun en marxa a ", "Check for active raffles on ", "Confirma se há algum ativo actualmente em ")
                    <a href="https://www.matiasmasso.es/sorteos">www.matiasmasso.es/sorteos</a>
                </p>
            </div>

        Else

            @<p>
                <b>1. @Mvc.ContextHelper.tradueix("Datos del participante", "Dades del participant", "Details of the participant", "Dados do participante")</b>
            </p>


            @<div class="SISU"
                  data-lang="@Mvc.ContextHelper.lang().Tag"
                  data-email="@DTOUser.GetEmailAddress(oUser)"
                  data-isauthenticated="@DTOUser.IsAuthenticated(oUser).ToString.ToLower"
                  data-src="@CInt(DTOUser.Sources.Raffle)"></div>

            @<div class="Exception"><span style="color:red"></span></div>

            @<div id="QuizRequest" hidden class="Contingut">
                <p>
                    <b>2. @Mvc.ContextHelper.tradueix("Danos tu respuesta", "Dona'ns la teva resposta", "Give us your answer", "Dá-nos a tua resposta")</b>
                </p>
                <p>
                    <span id="QuizQuestion">@Html.Raw(Model.Raffle.question)</span>
                </p>
                @If Model.Raffle.answers IsNot Nothing AndAlso Model.Raffle.answers.Count > 0 Then
                    For i As Integer = 0 To Model.Raffle.answers.Count - 1
                        @<input type="radio" name="QuizAnswer" value="@i" />
                            @Model.Raffle.answers(i)
                        @<br>
                    Next
                End if
            </div>

            @<div id="DistributorSelection" hidden>
                <p>
                    <b>3. @Mvc.ContextHelper.tradueix("Selecciona tu distribuidor favorito", "Sel.lecciona el teu distribuidor favorit", "Please select your choice of distributor", "Seleciona o teu distribuidor favorito")</b>
                </p>
                <p>
                    @Mvc.ContextHelper.tradueix("El premio se recoge en el distribuidor que cada participante selecciona, donde se hará una foto de la entrega para su publicación en nuestros medios",
                                                                                              "El premi es recull al distribuidor que cada participant selecciona, on es fará una fotografía de la entrega per publicar als nostres mitjans",
                                                                                              "The participant will pick up the prize prize at the distributor store, where a picture of the delivery will be taken to be published on our media",
                                                                                              "O prémio recolhe-se no distribuidor que cada participante seleciona, onde se fará uma fotografia da entrega para a sua publicação nas nossas redes sociais.")
                </p>

                <div class="RaffleAreaDropdown" id="dropdownZona">
                    <select>
                        <option value="@System.Guid.Empty.ToString" selected>@Mvc.ContextHelper.tradueix("(selecciona una zona)", "(sel.lecciona una zona)", "(select your area)")</option>
                        @For Each oZona In oZonas
                            @<option value="@oZona.Guid.ToString">@oZona.nom</option>
                        Next
                    </select>
                </div>

                <div id="dropdownLocation" hidden>
                    <select>
                        <option value="@System.Guid.Empty.ToString" selected>@Mvc.ContextHelper.tradueix("(selecciona una poblacion)", "(sel.lecciona una poblacio)", "(select your location)")</option>
                    </select>
                </div>

                <div id="dropdownDistributor" hidden>
                    <!--<input type="radio" name="distributorgroup" value="" />-->
                </div>

                <a href="#" id="ChangeDistributorRequest" hidden>
                    @Mvc.ContextHelper.tradueix("(seleccionar otro distribuidor)", "(sel.leccionar un altre distribuïdor)", "(select another distributor)", "(Selecionar outro distribuidor)")
                </a>
            </div>

            @<div id="SubmitRequest" hidden>
                <p>
                    <b>4. @Mvc.ContextHelper.tradueix("Acepta las bases del sorteo y envía tu solicitud", "Accepta les bases del sorteig i envía la sol.licitut", "Please accept the raffle terms and submit the form", "Aceita as bases do sorteio e envia a tua participação")</b>
                </p>
                <div id="bases">
                    @Html.Raw(Model.Raffle.Bases)
                </div>
                <p>
                    <input type="checkbox" id="termsAcceptance" />&nbsp;
                    @Mvc.ContextHelper.tradueix("He leido y acepto las bases del sorteo", "He llegit i accepto les bases del sorteig", "I've read and accept the raffle terms", "Li e aceito as bases do sorteio")
                </p>
                <input type="button" value='@Mvc.ContextHelper.tradueix("Enviar", "Enviar", "Submit")' />
            </div>

        End If

    </div>

    <input type="hidden" id="raffleguid" value="@Model.Raffle.Guid.ToString" />
    <input type="hidden" id="token" />
    <input type="hidden" id="ProductBrand" value="@Model.Raffle.Brand.Guid.ToString" />

</fieldset>

<fieldset id="Raffle_Thanks" hidden>
    <legend>@Mvc.ContextHelper.tradueix("Inscripción en sorteo", "Inscripció en sorteig", "Raffle subscription", "Inscrição no sorteio")</legend>
    <div>
        <p>
            @Mvc.ContextHelper.tradueix("Enhorabuena, has completado correctamente el proceso de inscripción", "Enhorabona, has completat correctament el procés de inscripció", "Congratulations, you successfully completed the subscription process", "Parabéns, completas-te corretamente o processo de inscrição.")
        </p>
        <p>
            @Mvc.ContextHelper.tradueix("Acabamos de enviarte un correo electrónico confirmando tu participación.", "Acabem de enviar-te un email confirmant la teva participació.", "We've just sent you an email confirming your enrollment.", "Acabamos de te enviar um correio eletrónico a confirmar a tua participação.")
        </p>
        <p>
            @Mvc.ContextHelper.tradueix("Una vez celebrado el sorteo, publicaremos el ganador en", "Un cop celebrat el sorteig, publicarem el guanyador a", "Once the raffle is over, we will publish the winner at", "Uma vez se celebre o sorteio, publicaremos o nome do ganhador/a em ")
            &nbsp;<a href='@FEB2.Raffles.Url(Mvc.ContextHelper.lang())'>@FEB2.Raffles.Url(Mvc.ContextHelper.lang(), True)</a>
        </p>
        <p>
            @Mvc.ContextHelper.tradueix("Si te toca el premio, tienes 30 días para ponerte en contacto con nosotros en ", "Si et toca el premi, tens 30 dies per posar-te en contacte amb nosaltres a ", "If your name becomes published, you have 30 days to confirm your acceptance at ", "Se te toca o prémio, tens 30 dias para nos contactar em ")
            <a href='mailto:marketing@matiasmasso.@Mvc.ContextHelper.tradueix("es", "es", "es", "pt")'>marketing@matiasmasso.@Mvc.ContextHelper.tradueix("es", "es", "es", "pt")</a>
        </p>
        <p class="FbWarn">
            <span>
                    @Html.Raw(Mvc.ContextHelper.Lang().tradueix("Recuerda que este es un sorteo exclusivo para seguidores de nuestros perfiles en Facebook y/o Instagram. Si aún no lo eres puedes seguirnos haciendo clic en los botones <b>Me gusta</b> (Facebook) o <b>Seguir</b> (Instagram)  en las siguientes páginas:",
                                                                              "Recorda que aquest és un sorteig exclusiu per seguidors dels nostres perfils a Facebook i/o Instagram. Si encara no ens segueixes, fes clic als botons <b>M'agrada</b> (Facebook) o <b>Seguir</b>(Instagram) a les següents pàgines:",
                                                                              "Please note this is a raffle restricted to our Facebook/Instagram followers. If you are not following us yet, you may clic <b>Like button</b> (Facebook) or <b>Follow</b> (Instagram) on next pages:",
                                                                              "Recorda que este sorteio é exclusivo para os seguidores dos nossos perfis em Facebook/Instagram. Se ainda não o és, podes seguir-nos clicando no botão <b>Gosto</b> (Facebook) ou <b>Seguir</b> (Instagram) nas seguintes páginas:"))

                </span>
        </p>
        <p>
            @If Mvc.ContextHelper.Lang().Equals(DTOLang.POR) Then
                @<a href="https://www.facebook.com/matiasmasso.sa.pt" target="_blank">www.facebook.com/matiasmasso.sa.pt</a>
                @<br />
                @<a href="https://www.instagram.com/matiasmasso.sa_pt" target="_blank">www.instagram.com/matiasmasso.sa_pt</a>
            Else
                @<a href="https://www.facebook.com/matiasmasso.sa" target="_blank">www.facebook.com/matiasmasso.sa</a>
                @<br />
                @<a href="https://www.instagram.com/matiasmasso.sa" target="_blank">www.instagram.com/matiasmasso.sa</a>
            End If


            @If (oBrand.Is4moms() Or oBrand.IsBritaxRoemer) Then
                @<br />
                @<a href = "@FEB2.Raffle.FacebookPage(Model.Raffle, Mvc.ContextHelper.Lang())" target="_blank">
                    @FEB2.Raffle.FacebookPageLabel(Model.Raffle, Mvc.ContextHelper.Lang())
                </a>
                @If Mvc.ContextHelper.Lang().Equals(DTOLang.POR) Then
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
            @Mvc.ContextHelper.tradueix("Gracias por participar", "Gracies per participar", "Thanks for participating", "Agradecemos a tua participação.")
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
            color:red;
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

        #QuizRequest {
            margin-top: 20px;
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

            .DistXX : hover {
                color: red;
            }

            .DistXX :: first-line {
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


