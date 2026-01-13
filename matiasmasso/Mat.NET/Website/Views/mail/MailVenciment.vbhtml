@ModelType DTOCsb

@Code
    ViewBag.Title = "Venciment"
    Layout = "~/Views/mail/_Layout.vbhtml"
    Dim oLang As DTOLang = Model.Contact.Lang
End Code

<table style="font-family: Helvetica Neue, Helvetica, Arial, sans-serif; font-size: 16px; line-height: 1.3em; padding: 0 10px 0 10px;">
    <tr>
        <td>
            <p>
                @oLang.Tradueix( _
                "Confirmamos que hemos puesto en circulación el siguiente efecto domiciliado en cuenta:", _
                "Confirmem la posta en circulació del següent efecte domiciliat en compte:", _
                "Please note we have just sent next draft to the bank:")
            </p>

            <table style="font-family: Helvetica Neue, Helvetica, Arial, sans-serif; font-size: 16px; line-height: 1.3em; width:100%">
                <tr><td style="padding: 0 10px 0 10px; border:1px solid gray;">
                    @oLang.Tradueix("Librado", "Lliurat", "Debitor")
                    </td><td style="padding: 0 10px 0 10px; border:1px solid gray;">@Model.Contact.Nom</td></tr>
                <tr><td style="padding: 0 10px 0 10px; border:1px solid gray;">
                    @oLang.Tradueix("Concepto","Concepte","Concept")
                    </td><td style="padding: 0 10px 0 10px; border:1px solid gray;">@Model.Txt</td></tr>
                <tr><td style="padding: 0 10px 0 10px; border:1px solid gray;">
                    @oLang.Tradueix("Importe", "Import", "Amount")
                    </td><td style="padding: 0 10px 0 10px; border:1px solid gray;">@DTOAmt.CurFormatted(Model.Amt)</td></tr>
                <tr><td style="padding: 0 10px 0 10px; border:1px solid gray;">
                    @oLang.Tradueix("Vencimiento", "Venciment", "Due date")</td><td style="padding: 0 10px 0 10px; border:1px solid gray;">@Model.vto.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"))</td></tr>
                <tr><td style="padding: 0 10px 0 10px; border:1px solid gray; vertical-align:top;">
                    @oLang.Tradueix("Cuenta", "Compte", "Account")</td><td style="padding: 0 10px 0 10px; border:1px solid gray;">@Html.Raw(FEB.Iban.ToHtml(Model.Iban))</td></tr>
            </table>

            <p>
                @oLang.Tradueix("Para cualquier aclaración sobre este tema puede dirigirse a:","Per qualsevol aclaració pot adreçar-se a:","On any doubt please contact:")
            </p>

            <p>
                Soledad Lizano<br />
                <a href="mailto:cuentas@matiasmasso.es">cuentas@matiasmasso.es</a><br />
                tel.: 932541520
            </p>

            <p>
                @oLang.Tradueix(
                  "Este correo se remite a los destinatarios de su organización suscritos a la recepción de facturas, que son los que figuran en el encabezado del mensaje.",
                  "Aquest missatge s'emet als destinataris de la seva organització suscrits a la recepció de factures, que son els enumerats a la capçalera.",
                  "This message is sent to all invoice reception subscribers of your organizations, as per destination recipients above.")
            </p>

            <p>
                @oLang.Tradueix(
                   "Es indispensable para el mantenimiento del crédito disponer de al menos una dirección de correo electrónico habilitada para este propósito.",
                   "Per el manteniment del crèdit es indispensable disposar al menys d'una adreça email habilitada per aquest propòsit.",
                   "At least one enabled recipient is required to keep an open credit account.")
            </p>

            <p>
                @oLang.Tradueix(
                   "Si cree que debiéramos añadir o suprimir algún destinatario por favor háganoslo saber.",
                   "Si creu que hauriem d'afegir o suprimir algun destinatari si us plau faci'ns-ho saber.",
                   "If you feel we should add or remove somebody please contact us.")
            </p>

            <p>
                @oLang.Tradueix( _
                "Reciba un cordial saludo,", _
                "Cordialment,", _
                "Regards,")
                
            </p>

            <p>
                MATIAS MASSO, S.A.<br />
                @oLang.Tradueix( _
                "Administración", _
                "Administració,", _
                "Accounts Dept,")
            </p>
        </td>
    </tr>
</table>