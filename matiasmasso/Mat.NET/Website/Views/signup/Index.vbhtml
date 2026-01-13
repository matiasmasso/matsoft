@ModelType LeadViewModel
@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"
    
End Code


@Using (Html.BeginForm())

    @<div class="pagewrapper">

        <h1>@ContextHelper.Tradueix("Formulario de registro", "Formulari de registre", "Sign up form", "Formulário de inscrição")</h1>

        @Html.Partial("_ValidationSummary", ViewData.ModelState)

         <div class="row">
             <div class="label">
                 @ContextHelper.Tradueix("por favor introduzca su dirección de correo electrónico", "si us plau introdueixi la seva adreça email", "please enter your email address")
             </div>

             <div class="control">
                 @Html.TextBoxFor(Function(Model) Model.EmailAddress, New With {.autocomplete = "on"})
             </div>
         </div>

        <div id="submit" class="row">
            <input type="submit" value='@ContextHelper.Tradueix("Aceptar","Acceptar","Submit","Aceitar")' />
        </div>

    </div>
    
End Using