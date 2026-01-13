@ModelType DTOUser
@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code


<fieldset class="formWrap">

    <legend>@Model.Lang.Tradueix("Activación de registro", "Activació de registre", "Activation confirmation")</legend>
    <br />
    @Model.Lang.Tradueix("Su registro ha sido activado satisfactoriamente, gracias por su paciencia.",
                                       "El seu compte ha estat activat satisfactoriament, gracies per la seva paciencia.",
                                       "Your account has been successfully activated, thanks for your patience.")
    <br /><br />
    @Model.Lang.Tradueix("Puede acceder y modificar sus datos en cualquier momento mediante el menú 'perfil' que encontrará en la esquina superior derecha de la página.",
                                           "Pot accedir i modificar les seves dades en qualsevol moment per el menú 'perfil' que trobará a dalt a la dreta de la página",
                                           "You may access and edit your data through your profile menu on the top right corner of the site")

</fieldset>
