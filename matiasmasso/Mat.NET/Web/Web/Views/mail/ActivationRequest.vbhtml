@ModelType DTOUser
@Code
    Layout = "~/Views/Mail/_Layout.vbhtml"
    Dim activationUrl As String = ""
End Code

<table cellpadding="20">
    <tr>
        <td colspan="2">
            @Html.Raw(Model.Lang.Tradueix("Gracias por registrarse en nuestra página web www.matiasmasso.es", _
                                          "Gracies per registrar-se a la nostre página web www.matiasmasso.es", _
                                          "Thanks for signing up on our website www.matiasmasso.es"))
            <br />
            @Html.Raw(Model.Lang.Tradueix("Si no ha sido Vd quien se ha registrado puede eliminar este mensaje de su buzón.", _
                                          "Si no ha estat vosté qui s'ha registrat pot esborrar tranquilament aquest missatge", _
                                          "If you have not signed up in our website please remove this message from your mailbox"))
            <br />
            @Html.Raw(Model.Lang.Tradueix("Por favor haga clic en el siguiente enlace para activar su cuenta", _
                                          "Si us plau faci clic al següent enllaç per activar el seu compte", _
                                          "Please clic on next link to activate your account"))
            <br />
            <a href="@FEB2.User.ActivationUrl(Model)">
                @Model.Lang.Tradueix("validar mi cuenta de correo", "validar el meu email", "validate my email address")
            </a>
        <td>
    </tr>

</table>