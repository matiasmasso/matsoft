@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim oIfemaGuest As Mvc.IfemaGuestViewModel = Model
    
End Code

<fieldset class="mat_form" id="mat_form">

    <legend>@Mvc.ContextHelper.Tradueix("Registro de Visitante", "Registre de Visitant", "Sign up form")</legend>

    @Html.Label("LabelThanks",Mvc.ContextHelper.Tradueix("Gracias por registrarse", "Gracies per registrar-se", "Thanks for signing up"))
</fieldset>