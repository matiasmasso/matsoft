@ModelType LeadViewModel
@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"

    Model.Fase = LeadViewModel.Fases.ExistingUser
End Code


@Using (Html.BeginForm("existingUsuari", "SignUp"))

    @<div class="pagewrapper">

    <h1>@ContextHelper.Tradueix("Formulario de registro", "Formulari de registre", "Sign up form", "Formulário de inscrição")</h1>

    @Html.Partial("_ValidationSummary", ViewData.ModelState)

    <div class="row">
        <div class="label">
            @ContextHelper.Tradueix("dirección de correo electrónico", "adreça email", "email address")
        </div>
        <div class="control">
            @Html.TextBoxFor(Function(Model) Model.EmailAddress, New With {.readonly = True})
        </div>
    </div>

    <div class="row">
        <div class="control">
            @ContextHelper.Tradueix("esta dirección de correo ya está registrada.", "aquesta adreça ja está registrada.", "this email address is already registered.")
            <br />
            @ContextHelper.Tradueix("Por favor entre la contraseña en la siguiente casilla.", "Si us plau entri la clau de pas a la següent casella.", "Please enter your password on next box.")
            <br />
            @ContextHelper.Tradueix("Si no la sabe o no la recuerda haga clic ", "Si no la sap o no la recorda faci clic ", "If you don't have one or you don't remember it please clic ")
            <a href="#" id="emailPassword">
                @ContextHelper.Tradueix("aquí", "aquí", "here")
            </a>
            @ContextHelper.Tradueix(" y se la enviaremos al momento a su buzón.", " i li enviarem al moment a la seva bustia.", " and we will send it to your mailbox.")
        </div>

        <div class="formLabelDiv" hidden="hidden">
            <label id="mailPasswordResult">@ContextHelper.Tradueix("le estamos enviando la contraseña por email...", "li estem enviant la clau de pas per email...", "we are sending your password via email...")</label>
        </div>
    </div>

         <div class="row">
             <div class="label">
                 @ContextHelper.Tradueix("contraseña", "clau de pas", "password", "palavra-passe")
             </div>

             <div class="control">
                 @Html.PasswordFor(Function(Model) Model.Password, New With {.autocomplete = "on"})
             </div>
         </div>

        <div id="submit" class="row">
            <input type="submit"  value='@ContextHelper.Tradueix("Aceptar", "Acceptar", "Submit", "Aceitar")' />
        </div>

    </div>

            @Html.HiddenFor(Function(Model) Model.Fase)

End Using


<script type="text/javascript">
    $('#emailPassword').click(function () {
        $('#mailPasswordResult').css('color', 'navy');
        $('.formLabelDiv').show();
        $.ajax({
            url: '@Url.Action("mailPassword")',
            data: {
                emailAddress: '@Model.EmailAddress'
            },
            dataType: "json",
            success: function (result) {
                onPasswordEmailed(result);
            }
        })
    })

    function onPasswordEmailed(result) {
        $('#mailPasswordResult').html(result.text)
        if (result.value == 0)
            $('#mailPasswordResult').css('color', 'blue');
        else
            $('#mailPasswordResult').css('color', 'red');

    }
</script>
