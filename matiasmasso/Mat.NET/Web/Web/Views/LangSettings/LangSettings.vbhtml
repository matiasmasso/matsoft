@imports Mvc

@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim oLang As DTOLang = ContextHelper.Lang()

    Dim userCookie = ContextHelper.GetCookieValue(Mvc.ContextHelper.Cookies.Lang)
    Dim userCookieLang = DTOLang.Factory(userCookie)
    Dim userCookieNom = DTOLang.LangNom(userCookieLang).Text(oLang)

    Dim browserLanguages = ContextHelper.BrowserLanguages()

    Dim osLanguage = ContextHelper.OSLanguage()
End Code    

<body>
    <div class="page-wrapper">
        <div class="title">
            @oLang.Tradueix("Configuración de Idioma", "Configuració de idioma", "Language settings", "opções de linguagem")
        </div>
        <p>
            @oLang.Tradueix("En matiasmasso.pt el idioma es siempre Portugués.",
                                             "A matiasmasso.pt l'idioma es sempre Portugués.",
                                             "matiasmasso.pt language is always portuguese.",
                                             "A língua é sempre portuguesa em matiasmasso.pt.")
        </p>
        <p>
            @oLang.Tradueix("En matiasmasso.es el idioma viene determinado por la cookie de usuario.",
                                         "A matiasmasso.es l'idioma de la pàgina ve determinat per la cookie de usuari.",
                                         "matiasmasso.es language is set by user cookie.",
                                         "O idioma do matiasmasso.es é definido pelo cookie do usuário.")
        </p>
        <p>
            @oLang.Tradueix("Si no hay cookie lo determina la configuración del navegador.",
                                       "Si no es troba cap cookie ho determinarà la configuració del navegador.",
                                       "If user cookie is not available the language will match the browser settings.",
                                       "Se o cookie do usuário não estiver disponível, o idioma corresponderá às configurações do navegador.")
        </p>
        <p>
            @oLang.Tradueix("Si el navegador no está configurado se tomará el idioma del sistema operativo.",
                                      "Si el navegador no està configurat es prendrà l'idioma del sistema operatiu.",
                                      "If browser settings are empty it will match Operative System language.",
                                      "Se as configurações do navegador estiverem vazias, corresponderá ao idioma do sistema operacional.")

        </p>
        <div class="grid-container">
            <div class="caption">
                @oLang.Tradueix("Cookie de usuario", "Cookie de usuari", "User cookie", "cookie de usuário")
            </div>
            <div>
                cookie
            </div>
            <div>
                <select onchange="setLangCookie(this);">
                    <option value="NON" @IIf(userCookie = "", "selected", "")>@oLang.Tradueix("(seleccionar idioma)", "(seleccioneu idioma)", "(pick a language)", "(selecione um idioma)")</option>
                    <option value="ESP" @IIf(userCookie = "ESP", "selected", "")>@DTOLang.ESP.Nom(oLang)</option>
                    <option value="CAT" @IIf(userCookie = "CAT", "selected", "")>@DTOLang.CAT.Nom(oLang)</option>
                    <option value="ENG" @IIf(userCookie = "ENG", "selected", "")>@DTOLang.ENG.Nom(oLang)</option>
                    <option value="POR" @IIf(userCookie = "POR", "selected", "")>@DTOLang.POR.Nom(oLang)</option>
                </select>
            </div>

            <div class="caption">
               @oLang.Tradueix("interfaz del navegador", "interficie del navegador", "browser language")
            </div>
            <div id="BrowserLanguage">
            </div>
            <div id="BrowserLanguageResult">
            </div>

            <div class="caption">
                @oLang.Tradueix("Preferencias de idioma configuradas en el navegador", "Preferències de idioma configurades al navegador", "Browser language preferences", "Preferências de idioma do navegador")
                <span class="subtitle">
                    @ContextHelper.BrowserAgent()
                    @ContextHelper.BrowserVersion()
                </span>
            </div>

            @For Each browserlanguage In browserLanguages
                @<div>
                    @browserlanguage
                </div>

                @<div>
                    @DTOLang.Nom(browserlanguage, oLang)
                </div>
            Next

            <div class="caption">
                @oLang.Tradueix("Idioma del sistema operativo", "Idioma del sistema operatiu", "Operative System Language", "Linguagem do sistema operativo")
            </div>
            <div>
                @osLanguage
            </div>
            <div>
                @DTOLang.Nom(osLanguage, oLang)
            </div>

            <div class="caption">
                @oLang.Tradueix("idioma resultante", "idioma resultant", "Result langage", "idioma estimado")
            </div>
            <div>
                &nbsp;
            </div>
            <div>
                @oLang.Nom()
            </div>
        </div>
    </div>


    @Section Styles
        <style>
            .page-wrapper {
                max-width: 450px;
                margin: auto;
                font-family: Verdana;
            }

            .title {
                font-size: 1.4em;
                font-weight: 600;
                margin-top: 50px;
                margin-bottom: 30px;
            }

            .grid-container {
                display: grid;
                grid-template-columns: auto auto;
            }

                .grid-container div {
                    border: 1px solid #DDDDDD;
                    padding: 10px;
                }

                .grid-container .caption {
                    grid-column: 1 / 3; /* span from grid column line 1 to 3 (i.e., span 2 columns) */
                    font-size: 1.1em;
                    font-weight: 500;
                    background-color: lightgray;
                }
        </style>
    End Section

    @Section Scripts
        <script>
            $(document).ready(function () {
                var iso = window.navigator.language;
                $("#BrowserLanguage").html(iso);
                var retval = 'Español';
                if (iso.startsWith('ca')) {
                    retval = 'Català';
                } else if (iso.startsWith('en')) {
                    retval = 'English';
                } else if (iso.startsWith('pt')) {
                    retval = 'Portugues';
                }
                $("#BrowserLanguageResult").html(retval);
            })


        </script>
    End Section
</body>
