var SISU_Lang;
var SISU_IsAuthenticated;
var SISU_HideAfterAuthenticate;
var SISU_Src;
var SISU_Argument;
var SISU_Email;

//$(document).ready(function () {

var spinner = SISU_Spinner();

    function SISU_SignInOrSignUp(div, argument) {
        SISU_Lang = div.data('lang');
        SISU_IsAuthenticated = div.data('isauthenticated');
        SISU_HideAfterAuthenticate = div.data('hideafterauthenticate');
        SISU_Src = div.data('src');
        SISU_Argument = argument;

        SISU_Email = div.data('email');

        //div.attr('style', 'max-width:500px;');

        div.append(SISU_DivRowMsg());
        div.append(SISU_DivRowEmail(SISU_Email));
        div.append(SISU_DivRowPwd());
        div.append(SISU_DivRowRemindMyPwd());
        div.append(SISU_FormRowSet());

        $('#SISU_RowPwd').hide();
        $('#SISU_RowRemindMyPwd').hide();
        $('#SISU_Msg').html(Tradueix("Por favor entre su correo electrónico y pulse el botón 'verificar'", "Si us plau entri la seva adreça email i premi el botó 'verificar'", "Please enter you email address and press the 'verify' button","Por favor entre o seu correio electrónico e prima o botão de 'verificar'"));
        $('#SISU_Msg').show();
    }

    function SISU_EmailChanged() {
        $('#SISU_EmailOk').hide();
        $('#SISU_ButtonEmail').show();
        $('#SISU_RowPwd').hide();
        $('#SISU_RowRemindMyPwd').hide();
        $('#SISU_Msg').html(Tradueix("Por favor entre su correo electrónico y pulse el botón 'verificar'", "Si us plau entri la seva adreça email i premi el botó 'verificar'", "Please enter you email address and press the 'verify' button","Por favor entre o seu correio electrónico e prima o botão de 'verificar'"));
        $('#SISU_Msg').show();
    }



$(document).on('input', '#SISU_textboxZip', function (event) {
    //display warning if zipcod exceeds length
    var zipcod = $('#SISU_textboxZip').val();
    var countryguid = $('#SISU_selectCountry').val();
    var spain = 'aeea6300-de1d-4983-9aa2-61b433ee4635';
    var portugal = '631b1258-9761-4254-8ed9-25b9e42fd6d1';
    if (countryguid == spain) {
        zipcod = zipcod.replace(/\s/g, ''); //remove white spaces
        if (zipcod.length > 5) {
            $('#SISU_RowZip .warning16').show();
        } else {
            $('#SISU_RowZip .warning16').hide();
        }
    } else if (countryguid == portugal) {
        zipcod = zipcod.replace(/\D/g, ''); //remove any non-digit
        if (zipcod.length > 7) {
            $('#SISU_RowZip .warning16').show();
        } else {
            $('#SISU_RowZip .warning16').hide();
        }
    }
});

$(document).on('change', '#SISU_textboxZip', function (event) {
    var retval = false;
    var zipcod = $('#SISU_textboxZip').val();
    var countryguid = $('#SISU_selectCountry').val();
    var spain = 'aeea6300-de1d-4983-9aa2-61b433ee4635';
    var portugal = '631b1258-9761-4254-8ed9-25b9e42fd6d1';
    if (countryguid == spain) {
        zipcod = zipcod.replace(/\s/g, ''); //remove white spaces
        retval = zipcod == '' || (zipcod.match(/^[0-9]{5}$/) ? true : false) && parseInt(zipcod, 10) < 53000 && parseInt(zipcod, 10) > 1000 && parseInt(zipcod, 10) % 1000;
    } else if (countryguid == portugal) {
        zipcod = zipcod.replace(/\D/g, ''); //remove any non-digit
        if (zipcod.length == 7) {
            zipcod = zipcod.replace(/(\d{4})/gi, "$1-"); //adds a dash after the first 4 digits
            retval = true;
        }
        retval = zipcod == '' || (zipcod.match(/^[1-9][0-9]{3}-[0-9]{3}$/) ? true : false);
    } else {
        retval = true;
    }

    if (retval) {
        $('#SISU_RowZip .warning16').hide();
        $('#SISU_textboxZip').val(zipcod);
        $('#SISU_spanLocation').text('');
        $.getJSON('/Utilities/ZipcodLocation', { Countryguid: countryguid, Zipcod: zipcod }, function (result) {
            $('#SISU_spanLocation').text(result);
        });
    } else {
        $('#SISU_spanLocation').text('');
        $('#SISU_RowZip .warning16').show();
    }

});


    function SISU_verifyEmail() {
        if (SISU_IsAuthenticated === true && $('#SISU_Email').val() === SISU_Email) {
            $('#SISU_Email').attr('disabled', 'disabled');
            $('#SISU_ButtonEmail').hide();
            $('#SISU_RowEmail').append(spinner);
            $.getJSON('/Account/GetUsuari', { email: SISU_Email }, function (success) {
                spinner.remove();
                $('#SISU_EmailOk').show();
                if (missingValues(success)) {
                    requestMissingValues(success);
                } else {
                    $('#SISU_Msg').hide();
                    triggerAuthentication();
                };
            });

        }
        else {
            //$("div.loading").show();
            $('#SISU_ButtonEmail').hide();
            $('#SISU_RowEmail').append(spinner);

            $.getJSON('/Account/VerifyEmail', { email: $('#SISU_Email').val() }, function (success) {
               // $('#SISU_RowEmail').remove(spinner);
                spinner.remove();
                $('.SISU_LoginVerificationResponse span').html(success.text);
                $('.SISU_LoginVerificationResponse span').css('color', success.color);
                switch (success.result) {
                    case 1:
                        /* --- email registrat pero no validat encara --- */
                        //$("div.loading").hide();
                        //$('#SISU_ButtonEmail').hide();
                        $('#SISU_EmailOk').show();
                        $('#SISU_RowPwd').show('fast');
                        $('#SISU_RowRemindMyPwd').show('fast');
                        $('#SISU_Msg').hide();
                        break;
                    case 2:
                        $('#SISU_Msg').html(Tradueix('email vacío', 'email buit', 'empty email', 'correio electrónico vazio'));
                        $('#SISU_Msg').show();
                        $('#SISU_Msg').css('color', 'red');
                        break;
                    case 3:
                        $('#SISU_Msg').html(Tradueix('email no válido', 'email no valid', 'wrong email', 'correio electrónico inválido'));
                        $('#SISU_Msg').show();
                        $('#SISU_Msg').css('color', 'red');
                        break;
                    case 4:
                        $('#SISU_Msg').append(spinner);
                        $('#SISU_Msg').html(Tradueix('email no registrado. Le estamos enviando la contraseña', 'email no registrat. Li estem enviant la contrasenya', 'unregistered email. We are sending your password', 'Correio eletrónico não registado. Em breve receberá a sua palavra-passe'));
                        $('#SISU_Msg').css('color', 'red');
                        $('#SISU_Msg').show();
                        SISU_mailVerificationCode();
                        break;
                    default:
                };
            });
        }

    }

    function SISU_RemindMyPwd() {
        $("div.loading").show();
        $('#SISU_RowRemindMyPwd').hide();
        $('#SISU_Msg').html(Tradueix('estamos enviando un correo con la contraseña...', 'estem enviant un correu amb la clau de pas...', 'We are mailing you your password...', 'Estamos enviando sua senha...'));
        $('#SISU_Msg').show();
        SISU_mailPwd();
    }

    function SISU_mailPwd() {
        $.getJSON('/Account/EmailPassword', { email: $('#SISU_Email').val() }, function (success) {
            $("div.loading").hide();
            switch (success.result) {
                case 1:
                    $('#SISU_RowPwd').show('fast');
                    $('#SISU_RowRemindMyPwd').show('fast');
                    $('#SISU_Msg').html(Tradueix('Correo enviado. Por favor entra la contraseña.<br/>Revisa la bandeja de correo no deseado si no la recibes.', 'Correu enviat. Si us plau entra la clau de pas.<br/>Revisa la safata de correu no sol.licitat si no la reps.', 'Password sent. Please browse your inbox.<br/>Browse your spam box if you don´t find it.', 'Receberá uma mensagem com a palavra-passe na sua caixa de correio electrónico. Se não a receber nos próximos minutos por favor, verifique a sua caixa de spam'));
                    break;
                default:
                    $('#SISU_Msg').html(Tradueix('error SYSERR_53, por favor consulta con oficinas', 'error SYSERR_53, si us plau consulta amb oficines', 'system error SYSERR_53. Please contact our offices', 'system error SYSERR_53. Por favor, contacte com os nossos escritórios'));
            };
        });
    }

    function SISU_mailVerificationCode() {
        $.getJSON('/Account/EmailVerificationCode', { email: $('#SISU_Email').val() }, function (success) {
            spinner.remove();
            switch (success.result) {
                case 1:
                    $('#SISU_ButtonEmail').hide();
                    $('#SISU_RowPwd').show('fast');
                    $('#SISU_RowRemindMyPwd').show('fast');
                    $('#SISU_Msg').html(Tradueix('Correo enviado. Por favor entra la contraseña que te acabamos de enviar.', 'Correu enviat. Si us plau entra la clau de pas que acabem de enviar-te.', 'Password sent. Please browse your inbox.', 'procure a sua palavra-passe na sua caixa de correio electrónico. Se não o receber nos próximos minutos por favor, verifique a sua caixa de spam'));
                    $('#SISU_Msg').css('color', 'green');
                    break;
                default:
                    $('#SISU_Msg').html(Tradueix('error SYSERR_52, por favor consulta con oficinas', 'error SYSERR_52, si us plau consulta amb oficines', 'system error SYSERR_52. Please contact our offices', 'system error SYSERR_52. Por favor, contacte com os nossos escritórios'));
                    $('#SISU_Msg').css('color', 'red');
            };
        });
    }

function SISU_verifyPwd() {
        $('#SISU_ButtonPwd').hide();
        $('#SISU_RowRemindMyPwd').hide();
        $('#SISU_RowPwd').append(spinner);
        $.getJSON('/Account/VerifyPwd', { email: $('#SISU_Email').val(), pwd: $('#SISU_textboxPwd').val() }, function (success) {
            spinner.remove();
            switch (success.result) {
                case 1:
                    /* --- existing user with validated password --- */
                    $('#SISU_Email').attr('disabled', 'disabled');
                    $('#SISU_RowPwd').hide();
                    $('#SISU_RowRemindMyPwd').hide();

                    if (missingValues(success)) {
                        requestMissingValues(success);
                    } else {
                        $('#SISU_Msg').hide();
                        triggerAuthentication();
                    };

                    break;
                case 5:
                    $('#SISU_RowRemindMyPwd').show();
                    $('#SISU_Msg').html(Tradueix('contraseña está vacía', 'clau de pas buida', 'empty password', 'palavra-passe vazia'));
                    $('#SISU_Msg').css('color', 'red');
                    $('#SISU_Msg').show();
                    $('#SISU_ButtonPwd').show();
                    break;
                case 6:
                    $('#SISU_RowRemindMyPwd').show();
                    $('#SISU_Msg').html(Tradueix('contraseña incorrecta', 'clau de pas no válida', 'invalid password', 'palavra-passe não válida'));
                    $('#SISU_Msg').css('color', 'red');
                    $('#SISU_Msg').show();
                    $('#SISU_ButtonPwd').show();
                    break;
                case 7:
                    /* --- new validated user. Proceed to complete user data --- */
                    $('#SISU_Email').attr('disabled', 'disabled');
                    $('#SISU_ButtonEmail').hide();
                    $('#SISU_EmailOk').show();
                    $('#SISU_RowPwd').hide();
                    $('#SISU_RowRemindMyPwd').hide();
                    $('#SISU_FormRowSet').show();
                    $('#SISU_Msg').hide();
                    break;
                case 9:
                    /* user deleted */
                    $('#SISU_RowRemindMyPwd').hide();
                    $('#SISU_Msg').html(Tradueix('usuario dado de baja', 'usuari donat de baixa', 'user deleted', 'usuário excluído'));
                    $('#SISU_Msg').css('color', 'red');
                    $('#SISU_Msg').show();
                    break;
                default:
                    $('#SISU_RowRemindMyPwd').hide();
                    $('#SISU_Msg').html(Tradueix('error SYSERR_50, por favor consulta con oficinas', 'error SYSERR_50, si us plau consulta amb oficines', 'system error SYSERR_50. Please contact our offices', 'system error SYSERR_50. Por favor, contacte com os nossos escritórios'));
                    $('#SISU_Msg').css('color', 'red');
            };
        });
    }

    function missingValues(data) {
        var retval = (data.nom === ''
            || data.cognoms === ''
            || data.Country === ''
            || data.Zipcod === ''
            || data.BirthYear === 0);
        return (retval);
    }

    function requestMissingValues(data) {
        $('#SISU_Msg').html(Tradueix('por favor complete los datos en blanco', 'si us plau completi les dades en blanc', 'please fill up blank boxes','Por favor complete todos os dados'));
        $('#SISU_Msg').show();

        $('#SISU_textboxFirstname').val(data.nom);
        $('#SISU_textboxSurname').val(data.cognoms);
        $('#SISU_textboxNickname').val(data.nickname);
        if (data.Country > '') {
            $('#SISU_selectCountry').val(data.Country);
        }
        $('#SISU_textboxZip').val(data.Zipcod);
        $('#SISU_textboxTel').val(data.tel);

        if (data.birthyear != 0) {
            $('#SISU_textboxBirth').val(data.birthyear);
        }

        $('#SISU_FormRowSet').show();
    }

    function SISU_update() {
        $('#SISU_Msg').hide();
        $('#SISU_ButtonSubmit').hide();
        $("#SISU_RowSubmit").append(spinner);

        $.getJSON('/Account/UpdateUsuari',
            {
                src: SISU_Src,
                email: $('#SISU_Email').val(),
                Firstname: $('#SISU_textboxFirstname').val(),
                Surname: $('#SISU_textboxSurname').val(),
                Nickname: $('#SISU_textboxNickname').val(),
                Country: $('#SISU_selectCountry').val(),
                Zipcod: $('#SISU_textboxZip').val(),
                Tel: $('#SISU_textboxTel').val(),
                BirthYear: $('#SISU_textboxBirth').val()
            },
            function (success) {
                spinner.remove();
                switch (success.result) {
                    case 1:
                        $('#SISU_FormRowSet').hide();
                        triggerAuthentication();
                        break;
                    case 11:
                        $('#SISU_Msg').html(Tradueix('código postal incorrecto', 'codi postal incorrecte', 'invalid Zip', 'código postal inválido ou incompleto'));
                        $('#SISU_Msg').css('color', 'red');
                        $('#SISU_Msg').show();
                        $('#SISU_ButtonSubmit').show();
                        break;
                    default:
                        $('#SISU_Msg').html(Tradueix('error SYSERR_51, por favor consulta con oficinas', 'error SYSERR_51, si us plau consulta amb oficines', 'system error SYSERR_51. Please contact our offices', 'system error SYSERR_51. Por favor, contacte com os nossos escritórios'));
                        $('#SISU_Msg').css('color', 'red');
                        $('#SISU_Msg').show();
                        $('#SISU_ButtonSubmit').show();
                };
            });

    }

    function triggerAuthentication() {
        $("div.loading").show();

        $.getJSON('/Account/onEmailAuthentication', { email: $('#SISU_Email').val() },
        function (success) {
            $("div.loading").hide();
            if (SISU_HideAfterAuthenticate === true) {
                $('#SISU_RowEmail').hide();
            }

            /*si no s'ha especificat un valor de argument, retorna la adreça email*/
            if (SISU_Argument === '') {
                var dirty = $('#SISU_Email').val();
                SISU_Argument = dirty.replace(/ /g, "");
                $('#SISU_Email').val(SISU_Argument);
            }

            $(document).trigger('SISU_Authenticated', SISU_Argument);
        });

    }

    //-----------------------------------------------------DOM elements factory---------------------------------------------------------

    function SISU_ButtonSubmit() {
        return ($('<input />', {
            'id': 'SISU_ButtonSubmit',
            'type': 'button',
            'value': 'enviar',
            'onclick': 'SISU_update();'
        }))
    }

    function SISU_DivRowEmail(email) {
        var $div = $('<div/>', {
            'id': 'SISU_RowEmail'
        });
        $div.append(SISU_Label('email', 'email', 'email', 'email'));
        $div.append(SISU_TextBoxEmail(email));
        $div.append(SISU_ButtonEmail());
        //$div.append(SISU_Spinner("SISU_Spinner_Email"));
        $div.append(SISU_EmailOk());
        return ($div);
    }

    function SISU_DivRowPwd() {
        var $div = $('<div/>', {
            'id': 'SISU_RowPwd'
        });
        $div.append(SISU_Label('contraseña', 'clau de pas', 'password', 'palavra-passe'));
        $div.append(SISU_TextBoxPwd());
        $div.append(SISU_ButtonPwd());
        return ($div);
    }

    function SISU_FormRowSet() {
        var $div = $('<div/>', {
            'id': 'SISU_FormRowSet'
        });
        $div.append(SISU_DivRowFirstname());
        $div.append(SISU_DivRowSurname());
        $div.append(SISU_DivRowNickname());
        $div.append(SISU_DivRowCountry());
        $div.append(SISU_DivRowZip());
        $div.append(SISU_DivRowTel());
        $div.append(SISU_DivRowBirth());
        $div.append(SISU_DivRowSubmit());
        return ($div);
    }

    function SISU_DivRowMsg() {
        var $div = $('<div/>');
        $div.append(SISU_Label('', '', '', ''));
        $div.append(SISU_Msg());
        return ($div);
    }

    function SISU_Msg() {
        return (
            $('<div/>', {
                //'style': 'display:inline-block;width:320px;'
            }).append(
            $('<label />', {
                'id': 'SISU_Msg'
            }))
        )
    }

    function SISU_DivRowRemindMyPwd() {
        var $div = $('<div/>', {
            'id': 'SISU_RowRemindMyPwd'
        });

        $div.append(SISU_Label('', '', '', ''));
        $div.append(SISU_LinkRemindMyPwd());
        return ($div);
    }



function SISU_DivRowFirstname() {
        var $div = $('<div/>', {
            'id': 'SISU_RowFirstName'
        });
        $div.append(SISU_Label('Nombre', 'Nom', 'First name', 'Nome'));
        $div.append(SISU_TextBoxFirstname());
        return ($div);
    }

    function SISU_DivRowSurname() {
        var $div = $('<div/>', {
            'id': 'SISU_RowSurName'
        });
        $div.append(SISU_Label('Apellidos', 'Cognoms', 'Surname','Apelidos'));
        $div.append(SISU_TextBoxSurname());
        return ($div);
    }

    function SISU_DivRowNickname() {
        var $div = $('<div/>', {
            'id': 'SISU_RowNickname'
        });
        $div.append(SISU_Label('Alias', 'Alies', 'Nickname', 'Nome de usuário'));
        $div.append(SISU_TextBoxNickname());
        return ($div);
    }

    function SISU_DivRowCountry() {
        var $div = $('<div/>', {
            'id': 'SISU_RowCountry'
        });
        $div.append(SISU_Label('Pais', 'Pais', 'Country', 'País'));
        $div.append(SISU_SelectCountry());
        return ($div);
    }

    function SISU_DivRowZip() {
        var $div = $('<div/>', {
            'id': 'SISU_RowZip'
        });
        $div.append(SISU_Label('Cód.postal', 'Codi postal', 'Zip code','Cód.Postal'));
        $div.append(SISU_TextBoxZip());
        $div.append(SISU_Warning());
        $div.append(SISU_SpanLocation());
        return ($div);
    }

    function SISU_DivRowTel() {
        var $div = $('<div/>', {
            'id': 'SISU_RowTel'
        });
        $div.append(SISU_Label('Teléfono', 'Teléfon', 'Phone','Telefone'));
        $div.append(SISU_TextboxTel());
        $div.append(SISU_Comment('(opcional)', '(opcional)', '(optional)', '(opcional)'));
        return ($div);
    }

    function SISU_DivRowBirth() {
        var $div = $('<div/>', {
            'id': 'SISU_RowBirth'
        });
        $div.append(SISU_Label('nacimiento', 'neixament', 'Birth', 'Ano nascimento'));
        $div.append(SISU_TextBoxBirth());
        $div.append(SISU_Comment('solo el año, en 4 dígitos', "tant sols l'any, en 4 digits", 'year only, 4 digits', 'só 4 dígitos'));
        return ($div);
    }

    function SISU_DivRowSubmit() {
        var $div = $('<div id = "SISU_RowSubmit"/>', {
            //'style': 'text-align:right;'
        });
        $div.append(SISU_ButtonSubmit());
        return ($div);
    }

    function SISU_Label(Esp, Cat, Eng, Por) {
        return (
            $('<div class="SISU_Label"/>', {
                //'style': 'display:inline-block;width:100px;'
            }).append(
            $('<label />', {
                'text': Tradueix(Esp, Cat, Eng, Por)
            }))
        )
    }

    function SISU_Comment(Esp, Cat, Eng) {
        return (
            $('<div/>', {
                'class': 'SISU_Comment'
                //'style': 'display:inline-block;margin-left:10px;'
            }).append(
            $('<label />', {
                'text': Tradueix(Esp, Cat, Eng)
            }))
        )
    }

    function SISU_TextBoxEmail(email) {
        return ($('<input />', {
            'id': 'SISU_Email',
            'type': 'text',
            'oninput': 'SISU_EmailChanged()',
            'onchange': 'SISU_EmailChanged()',
            'onpaste': 'SISU_EmailChanged()',
            'value': email
        }))
    }

    $(document).on('keypress', '#SISU_Email', function (key) {
        if (key.charCode === 32)
            return false;
        else
            return true;
    });


function SISU_ButtonEmail() {
    return ($('<input />', {
        'id': 'SISU_ButtonEmail',
        'type': 'button',
        'value': 'verificar',
        'onclick': 'SISU_verifyEmail();'
    }))
}

function SISU_Spinner() {
    return ($('<div />', {
        'class': 'Spinner64'
    }))
}

    function SISU_EmailOk() {
        return ($('<img />', {
            'id': 'SISU_EmailOk',
            'src': '/Media/Img/Ico/Ok.Png',
            'hidden': 'hidden'
        }))
    }

    function SISU_TextBoxPwd() {
        return ($('<input />', {
            'id': 'SISU_textboxPwd',
            'type': 'password',
            'maxlength': '24'
        }))
    }

    function SISU_ButtonPwd() {
        return ($('<input />', {
            'id': 'SISU_ButtonPwd',
            'type': 'button',
            'value': 'verificar',
            'onclick': 'SISU_verifyPwd();'
        }))
    }

    function SISU_LinkRemindMyPwd() {
        return ($('<a />', {
            'id': 'SISU_LinkRemindMyPwd',
            'href': 'javascript:;',
            'onclick': 'SISU_RemindMyPwd();',
            'text': Tradueix('Recuérdame la contraseña', 'Recorda\'m la clau de pas', 'Remind my password', 'Recordar a palavra-passe')
        }))
    }

    function SISU_TextBoxFirstname() {
        return ($('<input />', {
            'id': 'SISU_textboxFirstname',
            'type': 'text',
            'maxlength': '20'
        }))
    }

    function SISU_TextBoxSurname() {
        return ($('<input />', {
            'id': 'SISU_textboxSurname',
            'type': 'text',
            'maxlength': '60'
        }))
    }

    function SISU_TextBoxNickname() {
        return ($('<input />', {
            'id': 'SISU_textboxNickname',
            'type': 'text',
            'maxlength': '20'
        }))
    }

    function SISU_SelectCountry() {
        var $select = $('<select />', {
            'id': 'SISU_selectCountry'
        });


        $.getJSON('/Utilities/Countries', function (result) {
            for (var i = 0; i < result.length; i++) {
                if (result[i].Guid === 'aeea6300-de1d-4983-9aa2-61b433ee4635') {
                    $select.append('<option value="' + result[i].Guid + '" selected>' + result[i].Nom + '</option>');
                }
                else {
                    $select.append('<option value="' + result[i].Guid + '">' + result[i].Nom + '</option>');
                }
            }

        });

        return $select;

    }

    function SISU_TextBoxZip() {
        return ($('<input />', {
            'id': 'SISU_textboxZip',
            'type': 'text',
            'width': '70',
            'maxlength': '10'
        }))
}

function SISU_Warning() {
    return ($('<img />', {
        'src': 'https://www.matiasmasso.es/media/img/ico/warn.gif',
        'alt': 'error',
        'class': 'warning16',
        'width': '16',
        'height': '16',
        'hidden':'hidden'
    }))
}

    function SISU_SpanLocation() {
        return ($('<span />', {
            'id': 'SISU_spanLocation'
        }))
    }

    function SISU_TextboxTel() {
        return ($('<input />', {
            'id': 'SISU_textboxTel',
            'type': 'text',
            'width': '100',
            'maxlength': '15',
        }))
    }

    function SISU_TextBoxBirth() {
        return ($('<input />', {
            'id': 'SISU_textboxBirth',
            'type': 'number',
            'width': '70',
            'min': '1900',
            'max': '2020'
        }))
    }


//});




//-----------------------------------------------------utilities---------------------------------------------------------

function Tradueix(Esp, Cat, Eng, Por) {
    var retval = Esp;
    switch (SISU_Lang) {
        case 'ENG':
            if (Eng > '') retval = Eng;
            break;
        case 'CAT':
            if (Cat > '') retval = Cat;
            break;
        case 'POR':
            if (Por > '') retval = Por;
            break;
        default:
    };
    return (retval);
}


//------------------------------------------------------events------------------------------------------------------------

