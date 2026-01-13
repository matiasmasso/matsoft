@ModelType DTOUser  
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, ContextHelper.Lang, ViewBag.Lang)
End Code



    <h1>@ContextHelper.Tradueix("Cambio de contraseña", "Canvi de clau de pass", "Password edit", "Mudar a palavra-passe")</h1>

    <div class="row">
        <div class="label">
            @ContextHelper.Tradueix("Entre su contraseña vigente", "entri la seva clau de pas vigent", "please enter your current password", "entre a sua palavra-passe atual")
        </div>
        <div class="control">
            <input type="password" id="textboxOldPassword" class="form-textbox" />
            <img id="PasswordVerified" />
            <img src="~/Media/Img/Ico/empty.gif" />
        </div>
    </div>


                
                <a href="#" class="IforgotInfo"><img src="~/Media/Img/Ico/info_16.jpg" />&nbsp;@ContextHelper.Tradueix("No la recuerdo", "No la recordo", "I forgot")</a>
                <div hidden="hidden" id="Iforgot">
                    <p>
                    @ContextHelper.Tradueix("Si no la sabe o no la recuerda pulse el siguiente enlace y se la enviaremos al momento por correo",
                                          "Si no la sap o no la recorda premi el següent enllaç i li farem arribar al moment per correu",
                                          "If you don't know it or you don't remember it, click on next link and we will send it to your mailbox",
                                          "Se não a sabe ou não a recorda pulse no seguinte vinculo e lhe enviaremos no momento por correio")
                    </p>
                    <p>
                        <a href="#" id="mailPassword">
                            @ContextHelper.Tradueix("Enviarme la contraseña a ", "Envieu-me la clau de pas a ", "Mail password to ", "Enviar-me a palavra-passe a ")
                            @Model.emailAddress()
                        </a>
                    </p>
                </div>


        <div class="row">
            <div class="label">
                @ContextHelper.Tradueix("entre su nueva contraseña (max 24 caracteres)",
                                         "entri la nova clau de pas (max 24 caracters)",
                                         "enter your new password (max 24 characters)",
                                         "entre a sua nova palavra-passe (max 24 caracteres)")
            </div>
            <div class="control">
                <input type="password" id="textboxNewPassword"  maxlength="24" />
            </div>
        </div>

        <div class="row">
            <div class="label">
                @ContextHelper.Tradueix("entre de nuevo su nueva contraseña",
                                          "torni a entrar la nova clau de pas",
                                          "enter your new password again",
                                          "entre de novo a sua nova palavra-passe")
            </div>
            <div class="control">
                <input type="password" id="textboxNewPasswordAgain" />
            </div>
        </div>

        <div id="submit" class="row">
            <input id="passwordEditSubmit" type="button" value='@ContextHelper.Tradueix("Aceptar", "Acceptar", "Submit", "Aceitar")' />
        </div>

        <div>
            <span id="PasswordEdit-warn" class="form-warn"></span>
        </div>



@Section Styles
    <style scoped>
 
        .ContentColumn {
            max-width: 400px;
            margin:auto;
        }

        .IforgotInfo {
            display: inline-block;
            margin-top: 20px;
            vertical-align:middle;
        }

        .row {
            margin-top: 20px;
        }

        #Iforgot {
            padding: 7px 15px;
            background-color: moccasin;
            border: 1px solid black;
            box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);
        }

        #submit {
            display: flex;
            justify-content: flex-end;
            margin-right: 0;
        }

            #submit input {
                padding: 7px 20px;
                margin-right: 0;
                border: 1px solid cornflowerblue;
                border-radius: 5px;
                background-color: cornflowerblue;
                color: white;
            }

                #submit input:hover {
                    background-color: aqua;
                    color: white;
                }
    </style>
End Section

@Section Scripts

<script type="text/javascript">

    $(document).on('click', '.IforgotInfo', function () {
        $('#Iforgot').toggle("fast");
    });

    $(document).on('change paste keyup', '#textboxOldPassword', function (e) {
        $.getJSON('@Url.Action("CheckPassword")',
            { password: $('#textboxOldPassword').val() },
            function (response) {
                if (response.success)
                    $('#PasswordVerified').attr('src', '/Media/Img/Ico/ok.png');
                    else
                    $('#PasswordVerified').attr('src', '/Media/Img/Ico/aspa.png');
            });
    });

    $("#mailPassword").click(function () {
        event.preventDefault();
        $.ajax({
            url: '@Url.Action("MailPassword")',
            type: 'POST',
            dataType: "json",
            success: function () {
                warnMailbox;
            }

        })
    })

    $("#passwordEditSubmit").click(function () {
        var deferred = jQuery.Deferred();
        $.ajax({
            url: '@Url.Action("PasswordEdit")',
type: 'POST',
data: {
                oldPassword: $('#textboxOldPassword').val(),
                newPassword: $('#textboxNewPassword').val(),
                newPasswordAgain: $('#textboxNewPasswordAgain').val()
            },
            dataType: "json",
            success: function (result) {
                onValidation(result);
                deferred.resolve();
            }

        })
        return deferred.promise();
    })


    function onValidation(result) {
        $('#PasswordEdit-warn').html(result.text)
        if (result.value == 1) {
            $('#PasswordEdit-warn').css('color', 'blue');
            $('.row').hide();
            }
        else
            $('#PasswordEdit-warn').css('color', 'red');

    }
</script>
End Section


