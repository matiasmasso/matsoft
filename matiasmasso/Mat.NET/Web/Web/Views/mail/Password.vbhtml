@ModelType DTOUser

@Code
    Layout = "~/Views/Mail/_Layout.vbhtml"
End Code

<table cellpadding="20" style="font-family: arial, sans-serif; font-size: 12pt;">
    <tr>
        <td colspan=" 2">
            <font face="arial, sans-serif">
                @Html.Raw(Model.Lang.Tradueix("Este correo se envía en respuesta a una solicitud de contraseña"))</font>
                <br />
                @Html.Raw(Model.Lang.Tradueix("Si no la ha solicitado, puede eliminar este mensaje de su buzón."))
                <br />
                @Html.Raw(Model.Lang.Tradueix("Las contraseñas se envían tan solo al correo de sus destinatarios."))
                <br />
<td>
        </tr>
    <tr>
        <td>
            <table cellpadding="0" style="font-family: arial, sans-serif; font-size: 12pt;">
                <tr>
                    <td style="width:120px">usuario</td>
                    <td><b>@Html.Raw(Model.EmailAddress)</b></td>
                </tr>
                <tr>
                    <td>password</td>
                    <td><b>@Html.Raw(Model.Password)</b></td>
                </tr>
            </table>
        </td>
    </tr>
</table>