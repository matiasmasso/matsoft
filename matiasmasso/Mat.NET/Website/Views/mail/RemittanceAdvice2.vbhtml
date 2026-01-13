@ModelType DTOBancTransferBeneficiari
@Code
    Layout = "~/Views/Mail/_Layout3.vbhtml"
    Dim oLang As DTOLang = Model.Contact.Lang
    Dim ShowIban As Boolean = Model.BankBranch IsNot Nothing
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
    @For Each item As DTOCcb In DTOBancTransferBeneficiari.Ccbs(Model)
    @<tr>
        <td nowrap="nowrap" width="80%">
            @if FEB.Ccb.LinkToFactura(item) > "" Then
            @<a href = "@FEB.Ccb.LinkToFactura(item, True)" >
                    @FEB.Ccb.FacturaText(item, oLang)
            </a>
            Else
                @<span>
                    @FEB.Ccb.FacturaText(item, oLang)
                </span>
            End If
        </td>
        <td align="right" nowrap="nowrap">
            @Select Case item.Dh
                Case DTOCcb.DhEnum.Debe
                    @<span>@DTOAmt.CurFormatted(item.Amt)</span>
                Case DTOCcb.DhEnum.Haber
                    @<span>-@DTOAmt.CurFormatted(item.Amt)</span>
            End Select
        </td>
    </tr>
    Next

    <tr style="margin:0;padding:0;height:2px;">
        <td></td>
        <td style="margin:0;padding:0;height:2px;">
            <hr />
        </td>
    </tr>

    <tr>
        <td>
            total
        </td>
        <td align="right" nowrap="nowrap">
            @DTOAmt.CurFormatted((Model.Parent.Total))
        </td>
    </tr>
</table>

<br/>

<table style = "font-family: Helvetica Neue, Helvetica, Arial, sans-serif; font-size: 1em;width:100%" >

    <tr>
        <td width = "100%" valign="top" style="padding:0;vertical-align:top;">
            @oLang.Tradueix("Cuenta beneficiaria:", "Compte beneficiari:", "Destination account:")
            <p>
                @Html.Raw(DTOIban.Formated(Model.Account))
            <br/>
                @Html.Raw(DTOBankBranch.FullNomAndAddressHtml(Model.BankBranch))
            </p>
        </td>
    </tr>

</table>


