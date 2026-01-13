@ModelType DTO.DTORaffleParticipant
@Code
    Layout = "~/Views/shared/_Layout.vbhtml"
    Dim oWebsession As DTO.DTOSession = BLL.BLLSession.Find(Session("SessionId"))
    ViewData("Title") = oWebsession.Tradueix("Inscripción en sorteo", "Inscripció en sorteig", "Raffle subscription", "Inscrição no sorteio") & " | MATIAS MASSO, S.A."

    Dim hostName As String = HttpContext.Current.Request.Url.Host    '.Request.Url.Host
    Dim domain As DTO.DTOWebPageAlias.domains = BLL.BLLWebPageAlias.domain(hostName)


    Dim oBrand As DTO.DTOProduct = BLL.BLLRaffle.Brand(Model.Raffle)
    Dim oFacebookWidget As DTO.DTOrrssWidget = BLL.BLLProduct.FacebookWidget(domain, oBrand)

    Dim oZonas = BLL.BLLRaffle.Zonas(Mvc.GlobalVariables.Emp, Model.Raffle)
End Code

@Section AdditionalMetaTags
    <meta property="og:title" content="¡Participa en este fantástico sorteo!" />
    <meta property="og:site_name" content="M+O Sorteos y concursos" />
    <meta property="og:url" content="@FEBL.Raffle.PlayOrZoomUrl(Model.Raffle, True)" />
    <meta property="og:description" content="@Model.Raffle.Title" />
    <meta Property="og:locale" content="@DTO.DTOLang.Locale(oWebsession.Lang)" />
    <meta Property="fb:app_id" content="@FEBL.Facebook.AppId(FEBL.Facebook.AppIds.MatiasMasso)" />
    <meta Property="article:publisher" content="https://www.facebook.com/matiasmasso.sa" />
    <meta Property="og:Image" content="@BLL.BLLRaffle.ImgCallAction500(Model.Raffle, True)" />
End Section



<noscript><img height="1" width="1" alt="" style="display:none" src="https: //www.facebook.com/tr?ev=6016095764849&amp;cd[value]=0.01&amp;cd[currency]=EUR&amp;noscript=1" /></noscript>


<div id="fb-root"></div>





<fieldset id="Raffle_Steps">
    <legend>@oWebsession.Tradueix("Inscripción en sorteo", "Inscripció en sorteig", "Raffle subscription", "Inscrição no sorteio")</legend>

    <div>
        <div class="banner">
            <img src="@FEBL.Raffle.ImgBanner600Url(Model.Raffle)" />
        </div>

        @If DTO.DTORaffle.IsOver(Model.Raffle) Then

            @<div class="GameOver">
                <p>
                    @oWebsession.Tradueix("Este sorteo caducó el ", "Aquest sorteig va caducar el ", "This raffle is over since ", "Este sorteio caducou a ")
                    @Model.Raffle.FchTo.ToShortDateString
                    <br />
                    @oWebsession.Tradueix("Comprueba si hay alguno en marcha en ", "Comprova si n'hi ha algun en marxa a ", "Check for active raffles on ", "Confirma se há algum ativo actualmente em ")
                    <a href="https://www.matiasmasso.es/sorteos">www.matiasmasso.es/sorteos</a>
                </p>
            </div>

        Else

            @<p>
                <b>1. @oWebsession.Tradueix("Datos del participante", "Dades del participant", "Details of the participant", "Dados do participante")</b>
            </p>


            @<div class="SISU"
                  data-lang="@oWebsession.Lang.Tag"
                  data-email="@BLL.BLLSession.UserEmailAddress(oWebsession)"
                  data-isauthenticated="@oWebsession.IsAuthenticated.ToString.ToLower"
                  data-src="@CInt(DTO.DTOUser.Sources.Raffle)"></div>

            @<div class="Exception"><span style="color:red"></span></div>

            @<div id="FbVerificationRequest" hidden>
                <p><b>2. @oWebsession.Tradueix("Síguenos en Facebook", "Segueix-nos a Facebook", "Follow us on Facebook", "Segue-nos em Facebook")</b></p>
                <p>
                    @oWebsession.Tradueix("Este concurso es exclusivamente para seguidores de la marca en Facebook.", "Aquest concurs es exclusivament per seguidors de la marca a Facebook.", "This raffle is restricted to our followers.", "Este concurso é exclusivamente para seguidores da marca em Facebook.")
                    <br />
                    @oWebsession.Tradueix("Si aun no eres seguidor nuestro puedes hacerlo pulsando el botón 'Me gusta' a continuación.",
                                                                                              "Si encara no ets seguidor de la marca t'hi pots fer prement el botó 'm'agrada' a continuació.",
                                                                                              "If you are not following us yet, you may join us clicking on next 'like' button.",
                                                                                              "Se ainda não nos segues podes pulsar o botão 'gosto' à continuação.")
                    <br />

                    <div class="fb-like"
                         data-href="@oFacebookWidget.Url"
                         data-width="120"
                         data-layout="standard"
                         data-action="like"
                         data-show-faces="true"
                         data-share="true">
                    </div>

                    <input type="button" id="FbVerificationButton" value='@oWebsession.Tradueix("clic aquí si ya eres seguidor de la marca", "clic aquí si ja ets seguidor de la marca", "click here if you already follow the brand", "pulsa aqui se já és seguidor da marca")' />
                </p>
            </div>

            @<div id="QuizRequest" hidden>
                <p>
                    <b>3. @oWebsession.Tradueix("Danos tu respuesta", "Dona'ns la teva resposta", "Give us your answer", "Dá-nos a tua resposta")</b>
                </p>
                <p>
                    <span id="QuizQuestion">@Html.Raw(Model.Raffle.Question)</span>
                </p>
                @For i As Integer = 0 To Model.Raffle.Answers.Count - 1
                    @<input type="radio" name="QuizAnswer" value="@i" />@Model.Raffle.Answers(i)
                    @<br>
                Next
            </div>

            @<div id="DistributorSelection" hidden>
                <p>
                    <b>4. @oWebsession.Tradueix("Selecciona tu distribuidor favorito", "Sel.lecciona el teu distribuidor favorit", "Please select your choice of distributor", "Seleciona o teu distribuidor favorito")</b>
                </p>
                <p>
                    @oWebsession.Tradueix("El premio se recoge en el distribuidor que cada participante selecciona, donde se hará una foto de la entrega para su publicación en nuestros medios",
                                                                                          "El premi es recull al distribuidor que cada participant selecciona, on es fará una fotografía de la entrega per publicar als nostres mitjans",
                                                                                          "The participant will pick up the prize prize at the distributor store, where a picture of the delivery will be taken to be published on our media",
                                                                                          "O prémio recolhe-se no distribuidor que cada participante seleciona, onde se fará uma fotografia da entrega para a sua publicação nas nossas redes sociais.")
                </p>

                <div class="RaffleAreaDropdown" id="dropdownZona">
                    <select>
                        <option value="@System.Guid.Empty.ToString" selected>@oWebsession.Tradueix("(selecciona una zona)", "(sel.lecciona una zona)", "(select your area)")</option>
                        @For Each oZona As DTO.DTOGuidNom In oZonas
                            @<option value="@oZona.Guid.ToString">@oZona.Nom</option>
                        Next
                    </select>
                </div>

                <div id="dropdownLocation" hidden>
                    <select>
                        <option value="@System.Guid.Empty.ToString" selected>@oWebsession.Tradueix("(selecciona una poblacion)", "(sel.lecciona una poblacio)", "(select your location)")</option>
                    </select>
                </div>

                <div id="dropdownDistributor" hidden>
                    <!--<input type="radio" name="distributorgroup" value="" />-->
                </div>

                <a href="#" id="ChangeDistributorRequest" hidden>
                    @oWebsession.Tradueix("(seleccionar otro distribuidor)", "(sel.leccionar un altre distribuïdor)", "(select another distributor)", "(Selecionar outro distribuidor)")
                </a>
            </div>

            @<div id="SubmitRequest" hidden>
                <p>
                    <b>5. @oWebsession.Tradueix("Acepta las bases del sorteo y envía tu solicitud", "Accepta les bases del sorteig i envía la sol.licitut", "Please accept the raffle terms and submit the form", "Aceita as bases do sorteio e envia a tua participação")</b>
                </p>
                <div id="bases">
                    @Html.Raw(Model.Raffle.Bases)
                </div>
                <p>
                    <input type="checkbox" id="termsAcceptance" />&nbsp;
                    @oWebsession.Tradueix("He leido y acepto las bases del sorteo", "He llegit i accepto les bases del sorteig", "I've read and accept the raffle terms", "Li e aceito as bases do sorteio")
                </p>
                <input type="button" value='@oWebsession.Tradueix("Enviar", "Enviar", "Submit")' />
            </div>

        End If

    </div>

    <input type="hidden" id="raffleguid" value="@Model.Raffle.Guid.ToString" />
    <input type="hidden" id="token" />
    <input type="hidden" id="ProductBrand" value="@BLL.BLLRaffle.Brand(Model.Raffle).Guid.ToString" />

</fieldset>

<fieldset id="Raffle_Thanks" hidden>
    <legend>@oWebsession.Tradueix("Inscripción en sorteo", "Inscripció en sorteig", "Raffle subscription", "Inscrição no sorteio")</legend>
    <div>
        <p>
            @oWebsession.Tradueix("Enhorabuena, has completado correctamente el proceso de inscripción", "Enhorabona, has completat correctament el procés de inscripció", "Congratulations, you successfully completed the subscription process", "Estás de parabéns, terminaste corretamente o processo de inscrição.")
        </p>
        <p>
            @oWebsession.Tradueix("Acabamos de enviarte un correo electrónico confirmando tu participación.", "Acabem de enviar-te un email confirmant la teva participació.", "We've just sent you an email confirming your enrollment.", "Acabamos de enviar-te um correio eletrónico a confirmar a tua participação.")
        </p>
        <p>
            @oWebsession.Tradueix("Una vez celebrado el sorteo, publicaremos el ganador en", "Un cop celebrat el sorteig, publicarem el guanyador a", "Once the raffle is over, we will publish the winner at", "Uma vez se celebre o sorteio, publicaremos o nome do ganhador em")
            &nbsp;<a href='@BLL.BLLRaffles.Url(oWebsession.Lang)'>@BLL.BLLRaffles.Url(oWebsession.Lang, True)</a>
        </p>
        <p>
            @oWebsession.Tradueix("Recuerda que si te toca el premio, tienes 30 días para ponerte en contacto con nosotros en ", "Recorda que si et toca el premi, tens 30 dies per posar-te en contacte amb nosaltres a ", "Please remind If your name becomes published, you have 30 days to confirm your acceptance at ", "Recorda que se te toca o prémio, tens 30 dias para pôr-te em contacto connosco em ")
            <a href="mailto:marketing@matiasmasso.es">marketing@matiasmasso.es</a>
        </p>
        <p>
            @oWebsession.Tradueix("Gracias por participar", "Gracies per participar", "Thanks for participating", "Obrigada por participar.")
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
        js.src = "//connect.facebook.net/@Html.Raw(DTO.DTOLang.Locale(oWebsession.Lang))/all.js#xfbml=1&appId=@oFacebookWidget.widgetId&version=v3.0";
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


