@ModelType BankTransferReminderModel
@Code
    Layout = "~/Views/mail/_Layout2.vbhtml"
    Dim oLang = Model.Lang()
End Code

<div>
    <table>
        <tr>
            <td>
                <p>
                    @oLang.Tradueix(
                        "Apreciado cliente,",
                        "Benvolgut client,",
                        "Dear customer",
                        "Apreciado cliente,"
                  )
                </p>
            </td>
        </tr>
        <tr>
            <td style="padding-top: 20px;">
                <p>
                    @oLang.Tradueix(
                           "Le recordamos el vencimiento inminente de las siguientes facturas, para las que agradeceremos curse transferencia a la cuentra que detallamos a continuación.",
                           "Li recordem el venciment inminent de les següents facures, per les que agrairem cursi transferència al compte que detallem a continuació.",
                           "We remind you of the imminent expiration of the following invoices, for which we would appreciate transferring to the account that we detail below.",
                           "Recordamos-lhe o vencimento iminente das seguintes faturas, para as que lhe agradecemos realize a transferência à conta que detalhamos abaixo"
                          )
                </p>
            </td>
        </tr>
        <tr>
            <td style="padding-top: 20px;">
                <table width="100%" boder="1" style="border: 1px solid gray; ">
                    <tr style="border: 1px solid gray;">
                        <th align="center" style="border: 1px solid gray; padding: 5px 10px;">
                            @oLang.Tradueix("factura", "factura", "invoice", "factura")
                        </th>
                        <th align="center" style="border: 1px solid gray; padding: 5px 10px;">
                            @oLang.Tradueix("fecha", "data", "date", "data")
                        </th>
                        <th align="right" style="border:  1px solid gray; padding: 5px 10px;">
                            @oLang.Tradueix("importe", "import", "amount", "importe")
                        </th>
                        <th align="center" style="border: 1px solid gray; padding: 5px 10px;">
                            @oLang.Tradueix("vencimiento", "vencimient", "due date", "vencimento")
                        </th>
                    </tr>

                    @For Each oPnd As DTOPnd In Model.Pnds
                        @<tr>
                            <td align="center" style="border: 1px solid gray; padding: 5px 10px;">
                                @oPnd.fraNum
                            </td>
                            <td align="center" style="border: 1px solid gray; padding: 5px 10px;">
                                @oPnd.fch.ToString("dd-MM-yyyy")
                            </td>
                            <td align="right" style="border: 1px solid gray; padding: 5px 10px;">
                                @oPnd.amt.CurFormatted()
                            </td>
                            <td align="center" style="border: 1px solid gray; padding: 5px 10px;">
                                @oPnd.vto.ToString("dd-MM-yyyy")
                            </td>
                        </tr>
                    Next
                    <tr>
                        <td align="center" style="padding: 5px 10px;">
                            total
                        </td>
                        <td></td>
                        <td align="right" style="padding: 5px 10px;">
                            @Model.Total().CurFormatted()
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td colspan="2" style="padding-top: 20px;"><p>
                            @oLang.Tradueix("Cuenta beneficiaria", "Compte beneficiari", "Destination account", "Conta beneficiária")
                            :</p></td>
                    </tr>
                    <tr>
                        <td>@oLang.Tradueix("Titular", "Titular", "Owner", "Titular"): </td>
                        <td>@Model.Beneficiario</td>
                    </tr>
                    <tr>
                        <td>Iban : </td>
                        <td><b>@DTOIban.Formated(Model.Iban)</b></td>
                    </tr>
                    <tr>
                        <td>Swift : </td>
                        <td> @DTOIban.Swift(Model.Iban)</td>
                    </tr>
                    <tr>
                        <td>@oLang.Tradueix("Importe", "Import", "Amount", "Importe") : </td>
                        <td><b>@Model.Total().CurFormatted()</b></td>
                    </tr>
                    <tr>
                        <td>@oLang.Tradueix("Concepto", "Concepte", "Concept", "Conceito") : </td>
                        <td>@oLang.Tradueix("Cliente num", "Client num.", "Customer #", "Cliente num."). @Model.CliNum</td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td style="padding-top: 20px;"><p>@oLang.Tradueix("Atentamente", "Atentament", "Sincerely", "Atentamente"),</p></td>
        </tr>
        <tr>
            <td style="padding-top: 20px;">
                <p>
                    Soledad Lizano<br />
                    @oLang.Tradueix("Contabilidad", "Comptabilitat", "Accounts", "Contabilidade")<br />
                    tel.: @oLang.Tradueix("932541520", "932541520", "(+34) 932541520", "(+351) 308.806.304")<br />
                    cuentas@matiasmasso.es
                </p>
            </td>
        </tr>
    </table>
</div>