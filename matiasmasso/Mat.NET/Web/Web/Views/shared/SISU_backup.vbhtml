@Code
    Dim oUser = Mvc.ContextHelper.FindUserSync()
    Dim IsAuthenticated As Boolean = DTOUser.IsAuthenticated(oUser)
    Dim sEmailAddress As String = DTOUser.GetEmailAddress(oUser)
End Code

<style scoped>
    .SISU_EmailVerificationRequest input[type="email"] {
        min-width: 320px;
    }

    .SISU_FirstName div, .SISU_Surname div {
        width: 150px;
    }

    .SISU_FirstName input, .SISU_Surname input {
        width: 320px;
    }

    .SISU_UserDataSubmit {
        text-align: right;
    }
</style>

@Mvc.ContextHelper.Tradueix("Por favor entre su correo electrónico y pulse el botón 'verificar'",
                                              "Si us plau entri la seva adreça email i premi el botó 'verificar'",
                                              "Please enter you email address and press the 'verify' button",
                                              "Por favor entre o seu correio electrónico e prima o botão de 'verificar'")
<br />
<div class="SISU_EmailVerificationRequest">
    <input type="email" value='@IIf(IsAuthenticated, sEmailAddress, "")' />
    <input type="button" onclick="verifyEmail()" value="@Mvc.ContextHelper.Tradueix("verificar","verificar","verify")" />
    <img src="~/Media/Img/Ico/ok.png" class="SISU_EmailVerified" hidden />
</div>

<div class='SISU_LoginVerificationResponse'>
    <span></span>
</div>

<div class="SISU_PasswordVerificationRequest" hidden>
    <input type="password" />
    <input type="button" onclick="verifyPwd()" value="validar" />
</div>

<div class="SISU_UserData" hidden>
    <div class="SISU_FirstName">
        <div>
            <label>@Mvc.ContextHelper.Tradueix("Nombre", "Nom", "First name", "Nome")</label>
        </div>
        <input type="text" />
    </div>
    <div class="SISU_Surname">
        <div>
            <label>@Mvc.ContextHelper.Tradueix("Apellidos", "Conoms", "Surname", "Apelidos")</label>
        </div>
        <input type="text" />
    </div>
    <div class="SISU_UserDataSubmit">
        <input type="button" value='@Mvc.ContextHelper.Tradueix("Enviar", "Enviar", "Submit", "Aceitar")' />
    </div>
</div>

@Section Scripts
<script>
    /*
    $('.SISU_EmailVerificationRequest input[type="email"]').on('change', function () {
        /*no va
        alert('Changed!');
    })
    */


    var AuthenticatedEmail = '@IIf(IsAuthenticated, sEmailAddress, "NotAuthenticated")';


    $(document).ready(function () {
        $('.SISU_UserDataSubmit input').click(function (event) {
            alert('SISU_Submit');
        })

        function SISU_Submit2() {
            alert('SISU_Submit');

            $("div.loading").show();

            var url = '@Url.Action("UpdateUsuari", "Account")';
            $.getJSON(
                url,
                {
                    email: SISU_email(),
                    firstname: $('.SISU_FirstName input').val(),
                    surname: $('.SISU_SurName input').val()
                },
                function (success) {
                    $("div.loading").hide();
                    $('.SISU_LoginVerificationResponse span').html(success.text);
                    $('.SISU_LoginVerificationResponse span').css('color', success.color);
                    switch (success.result) {
                        case 1: /*Success*/
                            $('.SISU_UserData').hide();
                            $(document).trigger('SISU_Authenticated', [1011]);
                            break;
                        case 2:
                            alert("error 2");
                            break;
                        case 10:
                            alert("error 10");
                            break;
                        default:
                            alert("error default");
                    };
                });

        }




        function verifyEmail() {
            if (SISU_email() == AuthenticatedEmail) {
                $('.SISU_EmailVerificationRequest input[type="button"]').hide();
                $('.SISU_EmailVerified').show();
                $('.SISU_PasswordVerificationRequest').hide();
                $(document).trigger('SISU_Authenticated', [1011]);
            }
            else {
                $("div.loading").show();
                var url = '@Url.Action("VerifyEmail", "Account")';

                $.getJSON(url, { email: SISU_email }, function (success) {
                    $("div.loading").hide();
                    $('.SISU_LoginVerificationResponse span').html(success.text);
                    $('.SISU_LoginVerificationResponse span').css('color', success.color);
                    switch (success.result) {
                        case 1: /*DTOUser.ValidationResults.Success*/
                            $('.SISU_PasswordVerificationRequest').show();
                            break;
                        case 4: /*CInt(DTOUser.ValidationResults.EmailNotRegistered)*/
                            $("div.loading").show();
                            $('.SISU_PasswordVerificationRequest').show();
                            url = '@Url.Action("EmailPassword", "Account")';
                            $.getJSON(url, { email: SISU_email }, function (success) {
                                $('.SISU_LoginVerificationResponse span').html(success.text);
                                $('.SISU_LoginVerificationResponse span').css('color', success.color);
                                $("div.loading").hide();
                            });
                            break;
                        default:
                            alert('success.result: default');
                            $('.SISU_PasswordVerificationRequest').hide();
                    };
                });
            }
        }

    })

    function verifyPwd() {
        var url = '@Url.Action("VerifyPwd", "Account")';
        $.getJSON(url, { email: SISU_email(), pwd: SISU_pwd() }, function (success) {
            pwdResult(success);
        });
    }

    function pwdResult(success) {
        switch (success.result) {
            case 1: /*DTOUser.ValidationResults.Success*/
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 7: /*DTOUser.ValidationResults.NewValidatedUser*/
                $('.SISU_EmailVerificationRequest input[type="email"]').attr('disabled', 'disabled');
                $('.SISU_EmailVerificationRequest input[type="button"]').hide();
                $('.SISU_EmailVerified').show();
                $('.SISU_LoginVerificationResponse').hide();
                $('.SISU_PasswordVerificationRequest').hide();
                $('.SISU_UserData').show();
                break;
            default:
        };
    }


    function SISU_email() {
        return $('.SISU_EmailVerificationRequest input[type="email"]').val();
    }

    function SISU_pwd() {
        return $('.SISU_PasswordVerificationRequest input[type="password"]').val();
    }





</script>


End Section


