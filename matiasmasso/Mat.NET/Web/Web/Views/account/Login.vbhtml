@ModelType Mvc.LoginViewModel
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim oLang As DTOLang = Mvc.ContextHelper.Lang()
End Code

<div id="fb-root"></div>
<script>

    // Load the SDK Asynchronously
    (function (d) {
        var js, id = 'facebook-jssdk', ref = d.getElementsByTagName('script')[0];
        if (d.getElementById(id)) { return; }
        js = d.createElement('script'); js.id = id; js.async = true;
        js.src = "//connect.facebook.net/en_US/all.js";
        ref.parentNode.insertBefore(js, ref);
    }(document));

</script>



<h1>@Mvc.ContextHelper.Tradueix("Inicia sesión", "Inicia sessió", "Sign in", "Iniciar sessão")</h1>

<div class="Caption">@oLang.Tradueix("entra con tu contraseña", "entra amb la clau de pas", "login with your password", "Entra com o teu usuário e contrassenha")</div>
@Using Html.BeginForm()
    @<div class="ValidationSummary">
        @Html.ValidationSummary(False)
    </div>
    @<fieldset>
        <div>
            @Html.TextBoxFor(Function(m) m.EmailAddress, New With {.placeholder = oLang.Tradueix("correo electrónico", "correu electronic", "email address", "correio eletrónico")})
        </div>
        <div>
            @Html.PasswordFor(Function(m) m.Password, New With {.placeholder = oLang.Tradueix("contraseña", "clau de pas", "password", "contrassenha")})
        </div>
        <div class="remindMeSubmit">
            <div>
                @Html.CheckBoxFor(Function(m) m.Persist)
                @oLang.Tradueix("recuerdame", "recorda'm", "remind me", "recorda-me")
            </div>
            <input type="submit" />
        </div>
    </fieldset>
End Using


<div class="orDivider">
    <hr />
    &nbsp;@Mvc.ContextHelper.Tradueix("o", "o", "or", "o")&nbsp;
    <hr />
</div>

<!--
        <div class="loginWithFb">
            <a href="#" class="fb connect" onclick="onfb2()">
                @@oLang.Tradueix("Entrar por Facebook", "Entrar per Facebook", "Sign in with Facebook", "entrar com Facebook")
            </a>
        </div>
    -->
<div class="orDivider">
    <hr />
</div>

<div class="signupIforgot">
    <a href="/registro">@oLang.Tradueix("regístrame ahora", "registra'm ara", "register now", "Registrar-me agora")</a>
    <a href="#" onclick="toggleIForgot()">@oLang.Tradueix("no recuerdo la contraseña", "no recordo la clau de pas", "I forgot", "Não recordo a contrassenha")</a>
</div>

<div class="IforgotInstructions" hidden="hidden">
    @Html.Raw(oLang.Tradueix("Para recuperar su contraseña:<br/>1) entra tu email en la primera casilla<br/> 2) deja la contraseña en blanco<br/> 3) pulsa en Enviar.<br/> Recibirás un correo con instrucciones",
                                                           "Per recuperar la clau de pas:<br/>1) entra el teu email a la primera casella<br/>2) deixa la clau de pas en blanc<br/>3) prem Enviar.<br/>Rebràs un email amb instruccions",
                                                           "In order to recover your password:<br/>1) enter your email adress on first box<br/>2) leave password box blank<br/>3) accepty the form.<br/>You'll receive a message with instructions",
                      "Para recuperar su contraseña:<br/>1) introduz o teu email no priemiro espaço<br/> 2) deixa o espaço da contrassenha em branco<br/> 3) prime Enviar.<br/> Receberás uma mensagem com instruções"))
</div>


@Section Scripts
    <script>
        var hasInvokedLoginDialog = false;
        var accessToken;


        //code from http://facebooksdk.net/docs/web/getting-started
        window.fbAsyncInit = function () {
            FB.init({
                appId: '489736407757151', // App ID
                status: true, // check login status
                cookie: true, // enable cookies to allow the server to access the session
                xfbml: true  // parse XFBML
            });



            FB.Event.subscribe('auth.authResponseChange', function (response) {
                if (response.status === 'connected') {
                    // the user is logged in and has authenticated your
                    // app, and response.authResponse supplies
                    // the user's ID, a valid access token, a signed
                    // request, and the time the access token
                    // and signed request each expire
                    var uid = response.authResponse.userID;
                    accessToken = response.authResponse.accessToken;

                    if (hasInvokedLoginDialog == true)
                        onFbLogin();
                    else {
                        FB.api('/me', 'GET', { "fields": "id,name,email,picture" },
                            function (response) {
                                var continueAsLabel = '@(oLang.Tradueix("Continuar como ", "Continuar com ", "Continue as "))' + response.name;
                                //$('.loginWithFb a').html(continueAsLabel);
                                $('a.fb').html(continueAsLabel);
                                $('a.fb').css('padding-right','60px');
                                $('head').append('<style>.connect:after{background-image:url(' + response.picture.data.url + ');}</style>');
                            }
                        );
                    }

                } else if (response.status === 'not_authorized') {
                    // the user is logged in to Facebook,
                    // but has not authenticated your app
                    alert('not authorized');
                } else {
                    // the user isn't logged in to Facebook.
                    alert('user not logged into facebook');
                }
            });


        };





        //---------------------------------------------------------

        function toggleIForgot() {
            $(".IforgotInstructions").toggle();
        }

        function onfb2() {
            if (accessToken == null) {
                hasInvokedLoginDialog = true;
                FB.login(function (response) {
                }, { scope: 'email' });
            } else {
                onFbLogin();
            }
        }



    function onFbLogin() {

                // Handle the access token:
                // Do a post to the server to finish the logon
                // This is a form post since we don't want to use AJAX
                var form = document.createElement("form");
                form.setAttribute("method", 'post');
        //form.setAttribute("action", '/account/FbLoginConfirmation');
        form.setAttribute("action", '/FacebookLoginHandler.ashx');

                var field = document.createElement("input");
                field.setAttribute("type", "hidden");
                field.setAttribute("name", 'accessToken');
                field.setAttribute("value", accessToken);
                form.appendChild(field);

                var field = document.createElement("input");
                field.setAttribute("type", "hidden");
                field.setAttribute("name", 'returnUrl');
                field.setAttribute("value", '@Model.ReturnUrl');
                form.appendChild(field);

                var field = document.createElement("input");
                field.setAttribute("type", "hidden");
                field.setAttribute("name", 'persist');
                field.setAttribute("value", '@Model.Persist');
                form.appendChild(field);

                document.body.appendChild(form);
                form.submit();

    }

    </script>
End Section
@Section styles
    <style>

        .ContentColumn {
            max-width: 400px;
            margin: 0 auto;
        }

            .ContentColumn .Caption {
                margin: 30px 0 15px;
            }

        fieldset {
            padding: 0;
            margin-top: 15px;
            width: 100%;
            border-width: 0px;
        }

        .ValidationSummary {
            margin-top: 15px;
            color: crimson;
        }

        fieldset input[type=text], fieldset input[type=password] {
            width: 100%;
        }

        fieldset input[type=checkbox] {
            padding-left: 0;
            margin-left: 0;
        }

        .remindMeSubmit {
            display: flex;
            justify-content: space-between;
            margin-top: 15px;
        }

            .remindMeSubmit input[type=submit] {
                background-color: #3A5A97;
                font-size: 1em;
                color: white;
                margin: 10px 0 10px 10px;
                border: none;
                padding: 5px 12px;
                border-radius: 4px;
            }

        .signupIforgot {
            display: flex;
            justify-content: space-between;
            margin-top: 15px;
        }



        .IforgotInstructions {
            padding: 15px;
            margin-top: 15px;
            background-color: cornsilk;
            font-size: smaller;
        }

        .orDivider {
            display: flex;
            flex-direction: row;
            margin-top: 15px;
        }

        hr {
            width: 100%;
            height: 1px;
            color: black;
        }

        .loginWithFb {
            display: grid;
            grid-auto-flow: row;
            justify-content: center;
            margin-top: 15px;
        }

        a.fb {
            position: relative;
            /*font-family: Lucida Grande, Helvetica Neue, Helvetica, Arial, sans-serif;*/
            display: inline-block;
            /*font-size: 14px;*/
            padding: 13px 18px 15px 44px;
            background: #3A5A97;
            color: #fff;
            text-shadow: 0 -1px 0 rgba(0,0,20,.4);
            text-decoration: none;
            line-height: 1;
            border-radius: 5px;
        }

        .connect:before {
            display: inline-block;
            position: relative;
            background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABQAAAAUCAYAAACNiR0NAAAKzGlDQ1BJQ0MgUHJvZmlsZQAASA2tlndUU8kXx+e99EZLqFJCb9JbAOk19I5gIySBhBJjIIjYEFlcgbUgIgKKIEtVcC2ArAURxcKi2FBBF2RRUNfFgg2V3wOWuOd3fvvfb96Zmc+7c+fOnTkz53wBIPeyhMIUWAaAVEG6KMzHnb40JpaOewwgQADSQBVQWew0oVtISAD41/L+HuKNlNsms7H+1e1/D8hyuGlsAKAQZDiek8ZORfjkbGULRekAoHiIXXttunCWCxGmiZAEET40y4nzjPgDWvw8X5nziQjzQHyGAMCTWSxRIgCkccROz2AnInHIeITNBRy+AGEGws5sHouDcCbCi1NTV89yDcIG8f+Ik/gPZrHiJTFZrEQJz+8FmYks7MlPE6aw1s39/D+b1BQxcl5zRRNpyTyRbxjSKyFnVpG82l/Cgvig4AU7H9nRAvPEvpELzE7zQM5yfi6H5em/wOLkSLcFZokQ+tuHn86MWGDR6jBJfEFK0Oz9mMuBx2VKmJvmFb5gT+B7Mxc4ixcRvcAZ/KigBU5LDpfkkMXzkNhF4jBJzgkib8keU9OQmX+vy2Z9XyudF+G7YOdwPb0WmCuIlOQjTHeXxBGmzN3vufy5KT4Se1pGuGRuuihCYk9i+c3e1zl/YXqI5EyAJ/ACAchHB5bAGpgDBogG3iAknZuJ3DsAPFYL14n4ibx0uhvyUrh0poBtuphuaW5hDcDsu5v1AeDt/bn3BCngv9uqKgAIsEIGB7/bzHYAUO2EXP0d3226RwCQ3QXA2W62WJQxFw6gZzsMICLvmQaUgTrQBgbABMnQFjgCVyRjPxAMIkAMWAnYgAdSgQisBRvAFpAHCsAusBeUgUpwGNSDo+A4aANnwAVwGVwHN8FdMAiGwRh4ASbBezANQRAOokBUSBnSgHQhY8gSYkDOkBcUAIVBMVAclAgJIDG0AdoKFUBFUBlUBTVAv0CnoQvQVagfegCNQBPQG+gzjILJMA1Wg/VgM5gBu8H+cAS8Ak6E18BZcC68Ay6Fq+EjcCt8Ab4O34WH4RfwFAqgSCgFlCbKBMVAeaCCUbGoBJQItQmVjypBVaOaUR2oHtRt1DDqJeoTGoumouloE7Qj2hcdiWaj16A3oQvRZeh6dCu6G30bPYKeRH/DUDCqGGOMA4aJWYpJxKzF5GFKMLWYU5hLmLuYMcx7LBargNXH2mF9sTHYJOx6bCH2ALYF24ntx45ip3A4nDLOGOeEC8axcOm4PNx+3BHcedwt3BjuI56E18Bb4r3xsXgBPgdfgm/En8Pfwj/DTxNkCLoEB0IwgUNYR9hJqCF0EG4QxgjTRFmiPtGJGEFMIm4hlhKbiZeIQ8S3JBJJi2RPCiXxSdmkUtIx0hXSCOkTWY5sRPYgLyeLyTvIdeRO8gPyWwqFokdxpcRS0ik7KA2Ui5THlI9SVClTKaYUR2qzVLlUq9QtqVfSBGldaTfpldJZ0iXSJ6RvSL+UIcjoyXjIsGQ2yZTLnJYZkJmSpcpayAbLpsoWyjbKXpUdl8PJ6cl5yXHkcuUOy12UG6WiqNpUDyqbupVaQ71EHaNhafo0Ji2JVkA7SuujTcrLyVvLR8lnypfLn5UfVkAp6CkwFVIUdiocV7in8FlRTdFNkau4XbFZ8ZbiB6VFSq5KXKV8pRalu0qflenKXsrJyruV25QfqaBVjFRCVdaqHFS5pPJyEW2R4yL2ovxFxxc9VIVVjVTDVNerHlbtVZ1SU1fzUROq7Ve7qPZSXUHdVT1JvVj9nPqEBlXDWYOvUaxxXuM5XZ7uRk+hl9K76ZOaqpq+mmLNKs0+zWktfa1IrRytFq1H2kRthnaCdrF2l/akjoZOoM4GnSadh7oEXYYuT3efbo/uBz19vWi9bXpteuP6SvpM/Sz9Jv0hA4qBi8Eag2qDO4ZYQ4ZhsuEBw5tGsJGNEc+o3OiGMWxsa8w3PmDcvxiz2H6xYHH14gETsombSYZJk8mIqYJpgGmOaZvpKzMds1iz3WY9Zt/MbcxTzGvMBy3kLPwsciw6LN5YGlmyLcst71hRrLytNlu1W722NrbmWh+0vm9DtQm02WbTZfPV1s5WZNtsO2GnYxdnV2E3wKAxQhiFjCv2GHt3+832Z+w/Odg6pDscd/jL0cQx2bHRcXyJ/hLukpolo05aTiynKqdhZ7pznPMh52EXTReWS7XLE1dtV45rreszN0O3JLcjbq/czd1F7qfcP3g4eGz06PREefp45nv2ecl5RXqVeT321vJO9G7ynvSx8Vnv0+mL8fX33e07wFRjspkNzEk/O7+Nft3+ZP9w/zL/JwFGAaKAjkA40C9wT+BQkG6QIKgtGAQzg/cEPwrRD1kT8msoNjQktDz0aZhF2IawnnBq+KrwxvD3Ee4ROyMGIw0ixZFdUdJRy6Maoj5Ee0YXRQ8vNVu6cen1GJUYfkx7LC42KrY2dmqZ17K9y8aW2yzPW35vhf6KzBVXV6qsTFl5dpX0KtaqE3GYuOi4xrgvrGBWNWsqnhlfET/J9mDvY7/guHKKORNcJ24R91mCU0JRwniiU+KexAmeC6+E95LvwS/jv07yTapM+pAcnFyXPJMSndKSik+NSz0tkBMkC7pXq6/OXN0vNBbmCYfXOKzZu2ZS5C+qTYPSVqS1p9MQgdMrNhD/IB7JcM4oz/i4NmrtiUzZTEFm7zqjddvXPcvyzvp5PXo9e33XBs0NWzaMbHTbWLUJ2hS/qWuz9ubczWPZPtn1W4hbkrf8lmOeU5Tzbmv01o5ctdzs3NEffH5oypPKE+UNbHPcVvkj+kf+j33brbbv3/4tn5N/rcC8oKTgSyG78NpPFj+V/jSzI2FH307bnQd3YXcJdt3b7bK7vki2KKtodE/gntZienF+8bu9q/ZeLbEuqdxH3CfeN1waUNq+X2f/rv1fynhld8vdy1sqVCu2V3w4wDlw66DrweZKtcqCys+H+IfuV/lUtVbrVZccxh7OOPy0Jqqm52fGzw21KrUFtV/rBHXD9WH13Q12DQ2Nqo07m+AmcdPEkeVHbh71PNrebNJc1aLQUnAMHBMfe/5L3C/3jvsf7zrBONF8UvdkxSnqqfxWqHVd62Qbr224Paa9/7Tf6a4Ox45Tv5r+WndG80z5WfmzO88Rz+WemzmfdX6qU9j58kLihdGuVV2DF5devNMd2t13yf/Slcvely/2uPWcv+J05cxVh6unrzGutV23vd7aa9N76jeb30712fa13rC70X7T/mZH/5L+c7dcbl247Xn78h3mnet3g+7234u8d39g+cDwfc798QcpD14/zHg4PZg9hBnKfyTzqOSx6uPq3w1/bxm2HT474jnS+yT8yeAoe/TFH2l/fBnLfUp5WvJM41nDuOX4mQnviZvPlz0feyF8Mf0y70/ZPyteGbw6+ZfrX72TSyfHXotez7wpfKv8tu6d9buuqZCpx+9T309/yP+o/LH+E+NTz+foz8+m137BfSn9avi145v/t6GZ1JkZIUvEmtMCKKSFExIAeFMHACUGAOpNAIhS87p4zgOa1/IIQ3/XWfN/8bx2nh1ANAQ4kg1AaCciqZHfk0ivh/Qy2QCEuAIQ4QpgKytJRUZmS1qCleUcQKQ2RJqUzMy8RfQgzhCArwMzM9NtMzNfaxH9/hCAzvfzenzWWwbRNoeMrDw9w7sVjbPn5v+j+Q+WawDovrJFEQAAAAlwSFlzAAALEwAACxMBAJqcGAAAAdVpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IlhNUCBDb3JlIDUuNC4wIj4KICAgPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4KICAgICAgPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIKICAgICAgICAgICAgeG1sbnM6dGlmZj0iaHR0cDovL25zLmFkb2JlLmNvbS90aWZmLzEuMC8iPgogICAgICAgICA8dGlmZjpDb21wcmVzc2lvbj4xPC90aWZmOkNvbXByZXNzaW9uPgogICAgICAgICA8dGlmZjpQaG90b21ldHJpY0ludGVycHJldGF0aW9uPjI8L3RpZmY6UGhvdG9tZXRyaWNJbnRlcnByZXRhdGlvbj4KICAgICAgICAgPHRpZmY6T3JpZW50YXRpb24+MTwvdGlmZjpPcmllbnRhdGlvbj4KICAgICAgPC9yZGY6RGVzY3JpcHRpb24+CiAgIDwvcmRmOlJERj4KPC94OnhtcG1ldGE+Cjl0tmoAAAEMSURBVDgRY8hu3Pj/xevP/ykFIDNAZjE+ffnxv5QYHwM1wLNXnxgYQS4jx7C/f/8xMDMzYWhlwRDBI/DyzReGWatOMRw5+5Dh6/dfDOxsLAyiQtwMK/oi4LqINvDFm88MqTXrGd5/+g7X/PPXH4YnLz7C+SAG0QbOXnUaxTBhAS4GYUEuBl4udvIMPHnpMVxjY64Lg7OlMpyPzMAMVWRZJPaHTz/gPFyGgRQQbSDcNAIMvMnGJmomAe0MDAJ8HAxbZsTD1VHsQgVpQbhhIAbFBirKCKEYiNfLyCqRvX9kWTqyFAqbYheimAbkjBqIHiKk85lAhSK1AMgsprYZBxhevf1CsZnPX39mAJkFAN8bnc6Q9Jq4AAAAAElFTkSuQmCC);
            height: 23px;
            background-repeat: no-repeat;
            background-position: 0px 3px;
            text-indent: -9999px;
            text-align: center;
            width: 29px;
            line-height: 23px;
            margin: -8px 7px -7px -30px;
            padding: 2 25px 0 0;
            content: "f";
        }

        .connect:after {
            display: inline-block;
            background-repeat: no-repeat;
            top: 0px;
            right: 0px;
            content: ' ';
            border-top-right-radius: 5px;
            border-bottom-right-radius: 5px;
            position: absolute;
            height: 42px;
            width: 42px;
            z-index: 3;
            display: block;
            background-size: cover;
        }
    </style>
End Section
