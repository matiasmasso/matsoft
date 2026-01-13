@ModelType  DTOBrowser
@Code
    ViewBag.Title = "Probando navegador"
End Code

<!DOCTYPE html>

<html lang="@Choose(ContextHelper.Lang().id, "es", "ca", "en")">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>M+O</title>

    <style>
        body {
            font-size: 1em;
            font-family: Arial, sans-serif;
            box-sizing: border-box;
        }

        a {
            text-decoration: none;
        }

        .PageWrapper {
            max-width: 600px;
            margin: auto;
        }

        .email {
            text-align: right;
            padding: 20px 7px 2px 4px;
        }

        .Grid {
            display: grid;
            grid-template-columns: 1fr 1fr;
            border-top: 1px solid grey;
            border-left: 1px solid grey;
            width: 100%;
        }

            .Grid div {
                border-bottom: 1px solid grey;
                border-right: 1px solid grey;
                max-width:300px;
                overflow-wrap: break-word;
            }

        .Submit {
            font-size: 1em;
            color: white;
            background-color: #167ac6;
            margin: 10px 0 10px 10px;
            border: none;
            padding: 5px 20px;
            border-radius: 4px;
        }

            .Submit:hover {
                background-color: blue;
            }
    </style>
    </head>
<body>

    <div class="PageWrapper">
        <p>Esta es una página de prueba para verificar las características de su navegador.</p>
        <p>Al final de la página encontrará un botón para enviarla por correo al soporte informático</p>

        <div class="Grid">
            <div>IP</div>
            <div>@Model.IP</div>
            <div>navegador</div>
            <div>@Model.Browser</div>
            <div>javascript</div>
            <div>
                <noscript>deshabilitado</noscript>
                <script>document.write("habilitado")</script>
            </div>
            <div>usuario</div>
            <div>@Model.UserName</div>
            <div>sesión</div>
            <div>@Model.SessionId</div>
            <div>rol</div>
            <div>@Model.Rol</div>
        </div>
        <div class="email" id="ScriptsEnabled" style="display:none">
            <a href="@DTOBrowser.EmailLink(Model, True)">enviar por correo</a>
        </div>

        <h3>Cookies</h3>
        <div class="Grid">
            @For Each oCookie In Model.Cookies
                @<div>
                    @oCookie.Key
                </div>
                @<div>
                    @oCookie.Value
                </div>
            Next
        </div>

        <h3>Headers</h3>
        <div class="Grid">
            @For Each oHeader In Model.Headers
                @<div>
                    @oHeader.Key
                </div>
                @<div>
                    @oHeader.Value
                </div>
            Next
        </div>
        <div class="email">
            <a href="@DTOBrowser.EmailLink(Model, False)" Class="Submit">enviar por correo</a>
        </div>
    </div>

</body>
</html>

