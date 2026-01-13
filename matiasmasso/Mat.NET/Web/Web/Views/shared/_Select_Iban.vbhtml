@Code
    
End Code

<style>
    #IbanEntering input{
        width:300px;
    }

    #BankDetails {
    }

    #IbanWarning {
        color:red;
        font-weight:600;
    }

    #IbanEntered {
        padding-top:1em;
        font-weight:600;
    }

</style>


<div id="IbanEntered" hidden="hidden">
    <img id="IconOk" src="~/Media/Img/Ico/ok.png" />
</div>

<div id="IbanEntering">
    <p>
        @Mvc.ContextHelper.Tradueix("Entre todos los dígitos del IBAN:", _
                                  "Entri tots els digits del IBAN:", _
                                  "Please enter all your IBAN digits:")
    </p>

    <input type="text" maxlength="30" />


    <span id="IbanWarning" hidden="hidden">
        <img src="~/Media/Img/Ico/warn.gif" />
        &nbsp;
        @Mvc.ContextHelper.Tradueix("Digitos no válidos", _
                                  "Digits no valids", _
                                  "No valid digits")
    </span>
</div>


<div id="BankDetails"></div>




@Section Scripts
<script>
    $(document).ready(function () {

        $('#IbanEntering input').on('input', function (e) {
            $('#IbanWarning').hide();
            var src = $(this).val();
            if (src.length >= 8) {
                $.getJSON(
                    '@Url.Action("Validate","Iban")',
                    { src: src },
                function (success) {
                    var text = success.formated;
                    if (success.isvalidated)
                        onValidation(text);
                    else {
                        $('#IbanEntering input').val(text);
                        /*$('#IbanEntering input').setSelectionRange(text.length, text.length);*/
                        $('#BankDetails').html(success.text);
                    }
                });
            } else {
                $('#BankDetails').html('');
            }
        });


        $('#IbanEntering input').on('change', function (e) {
            $.getJSON(
                '@Url.Action("Validate","Iban")',
                { src: $(this).val() },
            function (success) {
                if (!success.isvalidated)
                    $('#IbanWarning').show();
            });

        });

        function onValidation(digits) {
            $('#IbanEntering').hide();
            $('#IbanEntered').show();
            $('#IbanEntered').prepend(digits);
            $(document).trigger('iban_Entered', [digits])
        }

    });


</script>


End Section

