@ModelType DTO.DTORaffleParticipant
@Code
    Layout = "~/Views/shared/_Layout_Minimal.vbhtml"
    Dim oWebsession As DTO.DTOSession = BLL.BLLSession.Find(Session("SessionId"))
    ViewData("Title") = oWebsession.Tradueix("Inscripción en sorteo", "Inscripció en sorteig", "Raffle subscription", "Inscrição no sorteio") & " | MATIAS MASSO, S.A."

    Dim oFacebookWidget As DTO.DTOrrssWidget = BLL.BLLProduct.FacebookWidget()

    Dim oZonas As List(Of DTO.DTOZona) = BLL.BLLRaffle.Zonas(Model)
End Code


<noscript><img height="1" width="1" alt="" style="display:none" src="https://www.facebook.com/tr?ev=6016095764849&amp;cd[value]=0.01&amp;cd[currency]=EUR&amp;noscript=1" /></noscript>


<div id="fb-root"></div>





<div id="Raffle_Steps">
    <div><b>Sorteo de una Römer Trifix Black Edition
    <br/>
    en el estand de Britax Römer
    <br/>
    de la Feria Bebés y Mamás de Madrid</b></div>

    <div>

        @If BLL.BLLRaffle.IsOver(Model.Raffle) Then

            @<div class="GameOver">
                <p>
                    @oWebSession.Tradueix("Este sorteo caducó el ", "Aquest sorteig va caducar el ", "This raffle is over since ")
                    @Model.Raffle.FchTo.ToShortDateString
                    <br />
                    @oWebSession.Tradueix("Comprueba si hay alguno en marcha en ", "Comprova si n'hi ha algun en marxa a ", "Check for active raffles on ")
                    <a href="https://www.matiasmasso.es/sorteos">www.matiasmasso.es/sorteos</a>
                </p>
            </div>

        Else

            @<p>
                <b>1. @oWebsession.Tradueix("Datos del participante", "Dades del participant", "Details of the participant", "Dados do participante")</b>
            </p>


            @<div class="SISU"
                data-lang="@oWebSession.Lang.Tag"
                data-email="@BLL.BLLSession.UserEmailAddress(oWebsession)"
                data-isauthenticated="@oWebSession.IsAuthenticated.ToString.ToLower"
                data-src="@CInt(DTO.DTOUser.Sources.Raffle)"></div>

            @<div class="IsDuplicated" hidden>
                @Html.Raw(oWebSession.Tradueix("este usuario ya está registrado en este sorteo desde ", "aquest usuari ja está registrat en aquest sorteig des de ", "this user is already enrolled since "))
                <span></span>
            </div>

            @<div id="FbVerificationRequest" hidden>
                <p><b>2. @oWebsession.Tradueix("Síguenos en Facebook", "Segueix-nos a Facebook", "Follow us on Facebook", "Segue-nos em Facebook")</b></p>
                <p>
                    Este concurso es exclusivamente para seguidores de la marca en Facebook.<br/>
                    Si aun no eres seguidor nuestro puedes hacerlo pulsando el botón "me gusta" a continuación.<br />

                    <div class="fb-like-box"
                         data-href="@oFacebookWidget.Url"
                         data-width="100%"
                         data-colorscheme="light"
                         data-show-faces="false"
                         data-header="false"
                         data-stream="false"
                         data-show-border="false">
                    </div>

                    <input type="button" id="FbVerificationButton"  value='@oWebSession.Tradueix("clic aquí si ya eres seguidor de la marca", "clic aquí si ja ets seguidor de la marca", "click here if you already follow the brand")' />
                </p>
            </div>

            @<div id="QuizRequest" hidden>
                <p>
                    <b>3. Confirma tu asistencia</b>
                </p>
                <p>
                    @oWebSession.Tradueix("Introduce el código que encontrarás en la banderola del que anuncia este sorteo en el stand de Britax Römer de la Feria Bebés y Mamás", "Introdueix el codi que trobarás a la banderola que anuncia aquest sorteig al stand de Britax Römer a Feria Bebes y Mamas", "Enter the code you'll find at the standup announcing this raffle at Britax Römer booth on Bebes y Mamas Fair")
                </p>
                <input type="text" />
            </div>


            @<div id="SubmitRequest" hidden>
                <p>
                    <b>4. @oWebSession.Tradueix("Acepta las bases del sorteo y envía tu solicitud", "Accepta les bases del sorteig i envía la sol.licitut", "Please accept the raffle terms and submit the form")</b>
                </p>
                <div id="bases">
                    @Html.Raw(Model.Raffle.Bases)
                </div>
                <p>
                    <input type="checkbox" id="termsAcceptance" />&nbsp;
                    @oWebSession.Tradueix("He leido y acepto las bases del sorteo", "He llegit i accepto les bases del sorteig", "I've read and accept the raffle terms")
                </p>
                <input type="button" value='@oWebSession.Tradueix("Enviar", "Enviar", "Submit")' />
            </div>

        End If

    </div>

    <input type="hidden" id="raffleguid" value="@Model.Raffle.Guid.ToString" />
    <input type="hidden" id="token" />

</div>

<fieldset id="Raffle_Thanks" hidden>
    <legend>Inscripción en sorteo</legend>
    <div>
        <p>
            Enhorabuena, has completado correctamente el proceso de inscripción
        </p>
        <p>
           Acabamos de enviarte un correo electrónico confirmando tu número de participación.
        </p>
        <p>
            Una vez celebrado el sorteo, publicaremos el ganador en <a href="https://www.matiasmasso.es/sorteo-trifix">www.matiasmasso.es/sorteo-trifix</a>
        </p>
        <p>
            Si te toca el premio, debes presentarte en el estand de Britax Römer entre las 17 y las 19 horas del 15/11/15 para recoger la silla.
        </p>
        <p>
            Si el ganador no se presenta transcurrido este plazo, el premio pasará al primero de los nueve suplentes; si no se encuentra en el estand a las 19 horas, la silla pasará al segundo suplente, y así sucesivamente.
        </p>
        <p>
            Gracias por participar
        </p>
    </div>
</fieldset>



@Section Styles
    <style scoped>
        #Raffle_Steps {
            width:320px;
            padding:10px;
            margin:auto;
            max-width:320px;
            text-overflow:ellipsis;
        }
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
        js.src = "//connect.facebook.net/@Html.Raw(BLL.BLLLang.Locale(oWebSession.Lang))/all.js#xfbml=1&appId=@oFacebookWidget.widgetId&version=v2.0";
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
    <script src="~/Media/js/RaffleTrifix.Play.js"></script>

End Section
