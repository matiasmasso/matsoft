@ModelType LoginViewModel
@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"

End Code


<div class="pagewrapper">
    @Using (Html.BeginForm(Nothing, Nothing, FormMethod.Post, New With {.id = "LoginForm", .name = "LoginForm"}))


        @<h1>@Html.Raw(ContextHelper.Tradueix("Identificación", "Identificació", "Login", "Identificação"))</h1>

        @Html.Partial("_ValidationSummary", ViewData.ModelState)

        @<div class="row">
            <div class="label">
                email:
            </div>
            <div class="control">
                @Html.TextBoxFor(Function(Model) Model.EmailAddress, New With {.maxLength = "100", .autocomplete = "on", .class = "EmailAddress"})
            </div>
        </div>

        @<div class="row">
            <div class="label">
                @ContextHelper.Tradueix("contraseña", "clau de pas", "password", "palavra-passe")
            </div>
            <div class="control">
                @Html.TextBoxFor(Function(Model) Model.Password, New With {.type = "password", .placeholder = "******", .autocomplete = "on", .class = "Password"})
            </div>
        </div>

        @<div class="row">
            <div class="label">
                &nbsp;
            </div>
            <div class="control">
                @Html.CheckBoxFor(Function(Model) Model.Persist)
                @ContextHelper.Tradueix("recordarme la próxima vez", "recordar-me la propera vegada", "remember me next time", "recorda-me da próxima vez")
            </div>
        </div>


        @<div class="row">
            <div class="label">
                &nbsp;
            </div>
            <div class="control">
                <div class="Literal">
                    @ContextHelper.Tradueix("¿no recuerda la contraseña?", "no recorda la clau de pas?", "forgot your password?", "Não se lembra da sua palabra-chave?")<br />
                    @ContextHelper.Tradueix("déjela en blanco y se la enviaremos a su correo", "deixi-la en blanc i la enviarem a la seva bustia", "leave it blank and we will send it to your mailbox", "deixe em branco, vamos enviá-la ao seu correio eletrónico")
                </div>
            </div>
        </div>


        @<div id="submit" class="row">
            <input id="LoginSubmit" type="submit" value='@ContextHelper.Tradueix("Aceptar", "Acceptar", "Submit", "Aceitar")' />
        </div>



        @Html.HiddenFor(Function(Model) Model.ReturnUrl, New With {.id = "ReturnUrl"})

    End Using
</div>

@Section Styles
    <style>
        .pagewrapper {
            width: 320px;
            max-width: 320px;
            margin: auto;
        }

            .pagewrapper .EmailAddress {
                width: 278px;
                margin-right: 10px;
            }

            .pagewrapper .Password {
                width: 139px;
            }

            .pagewrapper .Literal {
                width: 260px;
            }

        #submit {
            text-align: right;
        }
    </style>
End Section

@Section Scripts
    <script>
        $('#LoginForm').submit(function () {
            $('.loading').show();
            var returnUrl = $('#ReturnUrl').val();
            if (returnUrl.indexOf('/doc/') == 0) {
                window.location.href = '/pro';
                $('.loading').hide();
            }
        });
    </script>
End Section
