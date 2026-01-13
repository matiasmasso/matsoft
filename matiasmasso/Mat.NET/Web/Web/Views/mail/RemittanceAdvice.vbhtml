@ModelType DTORemittanceAdvice
@Code
    Layout = "~/Views/Mail/_Layout3.vbhtml"
    Dim exs As New List(Of Exception)
    FEB2.Contact.Load(Model.Proveidor, exs)
    Dim oLang As DTOLang = Model.Proveidor.Lang
    Dim ShowIban As Boolean = Model.Iban IsNot Nothing
End Code





<p style="font-family: Helvetica Neue, Helvetica, Arial, sans-serif; font-size: 1.5em;">
    @oLang.Tradueix("Aviso de transferencia bancaria", "Avis de transferència bancària", "Remittance Advice")
</p>
<p style="font-family: Helvetica Neue, Helvetica, Arial, sans-serif; font-size: 1.0em;">
    @oLang.Tradueix("Nos complace confirmarles que hemos cursado transferencia bancaria a su favor según el siguiente detalle:", _
                  "Ens complau confirmar-li'ls que hem cursat transferència bancaria al seu favor segons el següent detall:", _
                  "We are glad to confirm our bank transfer as per following detail:")

</p>
<table style="width: 100%;font-family: Helvetica Neue, Helvetica, Arial, sans-serif; font-size: 1em;">
    @For Each item As DTORemittanceAdviceItem In Model.Items
        @<tr>
            <td width="30px">
                @If item.DocFile IsNot Nothing Then
                @<a href="@FEB2.DocFile.DownloadUrl(item.DocFile, True)" title='@oLang.Tradueix("descargar copia de factura", "descarregar copia de la factura", "download invoice copy")'>
                    <img src="https://www.matiasmasso.es/Media/Img/Ico/pdf.gif" alt= '@oLang.Tradueix("descargar copia de factura", "descarregar copia de la factura", "download invoice copy")' />
                </a>
                End If
            </td>
            <td nowrap="nowrap" width="80%">
                @item.Text(oLang)
            </td>
            <td align="right" nowrap="nowrap">
                @DTOAmt.CurFormatted(item.Amt)
            </td>
        </tr>

    Next

    <tr style="margin:0;padding:0;height:2px;">
        <td></td>
        <td></td>
        <td style="margin:0;padding:0;height:2px;">
            <hr />
        </td>
    </tr>

    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            total
        </td>
        <td align="right" nowrap="nowrap">
            @DTOAmt.CurFormatted(DTORemittanceAdvice.Total(Model))
        </td>
    </tr>
</table>

<br/>

<table style="font-family: Helvetica Neue, Helvetica, Arial, sans-serif; font-size: 1em;width:100%">

    <tr>
        <td width="100%" valign="top" style="padding:0;vertical-align:top;">
            @oLang.Tradueix("Cuenta beneficiaria:", "Compte beneficiari:", "Destination account:")
            <p>
                @Html.Raw(FEB2.Iban.ToHtml(Model.Iban, True))
            </p>
        </td>
    </tr>

    <tr>
        <td align="center">
            @If Model.DocFile IsNot Nothing Then
                @<a href="@FEB2.DocFile.DownloadUrl(Model.DocFile, False)" title='@oLang.Tradueix("descargar justificante bancario", "descarregar justificant bancari", "download bank receipt")'>
                    <img src="@FEB2.DocFile.ThumbnailUrl(Model.DocFile)" width='150px' height='auto' alt='@oLang.Tradueix("descargar justificante bancario", "descarregar justificant bancari", "download bank receipt")' />
                </a>
            End If
        </td>
    </tr>

    @If Model.DocFile IsNot Nothing Then
        @<tr>
            <td style="background-color: #cc0000; text-align: center; vertical-align: middle; height: 20px;font-family: Helvetica Neue, Helvetica, Arial, sans-serif;font-size:14px;">
                <a href="@FEB2.DocFile.DownloadUrl(Model.DocFile, False)" title='@oLang.Tradueix("descargar justificante bancario", "descarregar justificant bancari", "download bank receipt")' style="color: white;text-decoration:none;">
                    @oLang.Tradueix("descargar justificante", "descarregar justificant", "download receipt")
                </a>
            </td>
        </tr>

    End If
</table>


