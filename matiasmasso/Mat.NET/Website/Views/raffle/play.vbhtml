@ModelType DTORafflePlayModel
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, ContextHelper.Lang, ViewBag.Lang)

    Dim hostName As String = HttpContext.Current.Request.Url.Host    '.Request.Url.Host
    Dim domain = ContextHelper.Domain()

    Dim oUser = If(Model.Src = DTORafflePlayModel.Srcs.app, Model.Participant.User, ContextHelper.FindUserSync())
    Dim oBrand = Model.Participant.Raffle.Brand
    Dim oFacebookWidget As DTOrrssWidget = FEB.Product.FacebookWidget(domain, oBrand)

    Dim exs As New List(Of Exception)
    Dim oZonas = FEB.Raffle.ZonasSync(exs, Website.GlobalVariables.Emp, Model.Participant.Raffle)
End Code

@Section AdditionalMetaTags
    <meta property="og:title" content="¡Participa en este fantástico sorteo!" />
    <meta property="og:site_name" content="M+O Sorteos y concursos" />
    <meta property="og:url" content="@FEB.Raffle.PlayOrZoomUrl(Model.Participant.Raffle, True)" />
    <meta property="og:description" content="@Model.Participant.Raffle.Title" />
    <meta Property="og:locale" content="@DTOLang.Locale(ContextHelper.Lang())" />
    <meta Property="fb:app_id" content="@FEB.Facebook.AppId(FEB.Facebook.AppIds.MatiasMasso)" />
    <meta Property="article:publisher" content="https://www.facebook.com/matiasmasso.sa" />
    <meta Property="og:image" content="@FEB.Raffle.ImgCallAction500(Model.Participant.Raffle, True)" />
End Section



<noscript><img height="1" width="1" alt="" style="display:none" src="https: //www.facebook.com/tr?ev=6016095764849&amp;cd[value]=0.01&amp;cd[currency]=EUR&amp;noscript=1" /></noscript>


<div id="fb-root"></div>


<h1 class="Title">@ViewBag.Title</h1>

<div class="banner">
    <img src="@FEB.Raffle.ImgBanner600Url(Model.Participant.Raffle)" width="600" height="200" />
</div>

<div id="Raffle_Steps">

    @If DTORaffle.IsOver(Model.Participant.Raffle) Then

        @<div class="GameOver">
            <p>
                @ContextHelper.Tradueix("Este sorteo caducó el ", "Aquest sorteig va caducar el ", "This raffle is over since ", "Este sorteio caducou a ")
                @Model.Participant.Raffle.FchTo.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"))
                <br />
                @ContextHelper.Tradueix("Comprueba si hay alguno en marcha en ", "Comprova si n'hi ha algun en marxa a ", "Check for active raffles on ", "Confirma se há algum ativo actualmente em ")
                <a href="https://www.matiasmasso.es/sorteos">www.matiasmasso.es/sorteos</a>
            </p>
        </div>

    Else
        @<div class="Epigraf">
            <b>1. @ContextHelper.Tradueix("Datos del participante", "Dades del participant", "Details of the participant", "Dados do participante")</b>
        </div>

        If Model.Src = DTORafflePlayModel.Srcs.app Then
        @<div>
            @oUser.FullNom()<br/>@oUser.EmailAddress
        </div>

        Else


            @<div class="SISU"
                  data-lang="@ContextHelper.Lang().Tag"
                  data-email="@DTOUser.GetEmailAddress(oUser)"
                  data-isauthenticated="@DTOUser.IsAuthenticated(oUser).ToString.ToLower"
                  data-src="@CInt(DTOUser.Sources.raffle)"></div>

            @<div class="Exception"><span style="color:red"></span></div>
        End If


        @<div id="QuizRequest" @IIf(Model.Src <> DTORafflePlayModel.Srcs.app, "hidden", "") Class="Contingut">
            <div class="Epigraf">
                <b>2. @ContextHelper.Tradueix("Danos tu respuesta", "Dona'ns la teva resposta", "Give us your answer", "Dá-nos a tua resposta")</b>
            </div>
            <p>
                <span id="QuizQuestion">@Html.Raw(Model.Participant.Raffle.Question)</span>
            </p>
            @If Model.Participant.Raffle.Answers IsNot Nothing AndAlso Model.Participant.Raffle.Answers.Count > 0 Then
                For i As Integer = 0 To Model.Participant.Raffle.Answers.Count - 1
                    @<input type="radio" name="QuizAnswer" value="@i" />
                    @Model.Participant.Raffle.Answers(i)
                    @<br>
                Next
            End If
        </div>

        @<div id="DistributorSelection" hidden>
            <div class="Epigraf">
                <b>3. @ContextHelper.Tradueix("Selecciona tu distribuidor favorito", "Sel.lecciona el teu distribuidor favorit", "Please select your choice of distributor", "Seleciona o teu distribuidor favorito")</b>
            </div>
            <p>
                @ContextHelper.Tradueix("El premio se recoge en el distribuidor que cada participante selecciona, donde se hará una foto de la entrega para su publicación en nuestros medios",
                                                                                                   "El premi es recull al distribuidor que cada participant selecciona, on es fará una fotografía de la entrega per publicar als nostres mitjans",
                                                                                                   "The participant will pick up the prize prize at the distributor store, where a picture of the delivery will be taken to be published on our media",
                                                                                                   "O prémio recolhe-se no distribuidor que cada participante seleciona, onde se fará uma fotografia da entrega para a sua publicação nas nossas redes sociais.")
            </p>

            <div class="StoreLocator">
                <div>
                    <select id="dropdownZona">
                        <option value="">@ContextHelper.Tradueix("(seleccionar zona)")</option>
                    </select>
                </div>
                <div>
                    <select id="dropdownLocation" hidden>
                        <option value="">@ContextHelper.Tradueix("(seleccionar población)")</option>
                    </select>
                </div>
                <div id="divDistributors" />
            </div>

            <a href="#" id="ChangeDistributorRequest" hidden>
                @ContextHelper.Tradueix("(seleccionar otro distribuidor)", "(sel.leccionar un altre distribuïdor)", "(select another distributor)", "(Selecionar outro distribuidor)")
            </a>
        </div>

        @<div id="SubmitRequest" hidden>
            <div class="Epigraf">
                <b>4. @ContextHelper.Tradueix("Acepta las bases del sorteo y envía tu solicitud", "Accepta les bases del sorteig i envía la sol.licitut", "Please accept the raffle terms and submit the form", "Aceita as bases do sorteio e envia a tua participação")</b>
            </div>
            <div id="bases" class="Contingut">
                @Html.Raw(Model.Participant.Raffle.Bases)
            </div>
            <div class="SubmitComplexRow">
                <input type="checkbox" id="termsAcceptance" />&nbsp;
                @ContextHelper.Tradueix("He leido y acepto las bases del sorteo", "He llegit i accepto les bases del sorteig", "I've read and accept the raffle terms", "Li e aceito as bases do sorteio")
                <input type="button" class="Submit" value='@ContextHelper.Tradueix("Enviar", "Enviar", "Submit")' />
            </div>
        </div>

    End If

</div>

<div id="Thanks"></div>


<input type="hidden" id="raffleguid" value="@Model.Participant.Raffle.Guid.ToString" />
<input type="hidden" id="token" />
<input type="hidden" id="ProductBrand" value="@Model.Participant.Raffle.Brand.Guid.ToString" />

@Section Styles
    <link href="~/Media/Css/SISU.css" rel="stylesheet" />
    <style scoped>
        .PageWrapperHorizontal {
            justify-content: start !important;
            height: 100%;
        }

        .ContentColumn {
            max-width: 600px;
            width: 100%;
        }


        .FbWarn {
            color: red;
        }

        .banner {
        }

        .Epigraf {
            padding:30px 0 10px;
        }
        .banner img {
            width: 100%;
            height: auto;
        }


        #QuizQuestion ul li {
            padding: 10px 0 5px;
        }
        /*
                .EmailVerificationRequest input[type="email"] {
                    width: 320px;
                }

                #FbVerificationButton {
                    width: 298px;
                    margin: 10px;
                }
        */
        .RaffleAreaDropdown {
            display: inline-block;
        }

        #dropdownZona, #dropdownLocation {
            width: 100%;
            max-width: 320px;
            font-size: 1em;
            padding: 4px 7px 2px 4px;
        }

        #divDistributors {
        }

        .Distributor {
            display: block;
            margin-top: 15px;
        }

            .Distributor:hover {
                color: blue;
            }

            .Distributor div:first-child {
                font-weight: 600;
            }

        #ChangeDistributorRequest {
            color: blue;
            margin-top: 15px;
        }

        #bases {
            max-width: 460px;
            height: 200px;
            overflow: scroll;
            margin: 20px auto 10px;
            border: 1px solid gray;
        }

            #bases.h2 {
                font-size: 1.0em;
            }

        .SubmitComplexRow {
            margin: 20px 0;
            align-items: center;
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
    <script src="~/Scripts/SignInOrSignUp.js"></script>
    <script src="~/Media/js/RafflePlay2.js"></script>

    <script>
        (function (d, s, id) {
            var js, fjs = d.getElementsByTagName(s)[0];
            if (d.getElementById(id)) return;
            js = d.createElement(s); js.id = id;
            js.src = "//connect.facebook.net/@Html.Raw(DTOLang.Locale(ContextHelper.Lang()))/all.js#xfbml=1&appId=@oFacebookWidget.widgetId&version=v3.0";
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

    <script>
        var storelocator = @Html.Raw(Model.StoreLocator.Offline.SerializedForRaffles());
    </script>


End Section


