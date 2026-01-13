@ModelType LeadViewModel
@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"

End Code


    <fieldset class="formWrap">

        <legend>@ContextHelper.Tradueix("Formulario de registro", "Formulari de registre", "Sign up form", "Formulário de inscrição")</legend>
        <br/>
        @ContextHelper.Tradueix("Gracias por registrarse","Gracies per registrar-se","Thanks for signing up")
        <br /><br />
        @ContextHelper.Tradueix("Acabamos de enviarle un correo para validar su dirección email.", _
                               "Acabem de enviar-li un correu per validar la seva adreça email", _
                               "We have just sent you a message to confirm your email address")
        <br /><br/>
        @ContextHelper.Tradueix("Por favor ábralo y siga las instrucciones para activar su cuenta", _
                              "Si us plau obri'l i segueixi les seves instruccions per activar el compte", _
                              "please open it and follow the instructions to activate your account")
    </fieldset>

