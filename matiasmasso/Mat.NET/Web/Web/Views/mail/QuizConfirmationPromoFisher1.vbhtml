@ModelType DTO.DTOPurchaseOrder
@Code
    Layout = "~/Views/mail/_Layout.vbhtml"

End Code

<div>

    <p>
        Gracias por participar en la promoción de Fisher-Price.
    </p>

    <p>
        Confirmamos su pedido:
    </p>

    <table style="width:100%;font-family: Helvetica Neue, Helvetica, Arial, sans-serif; font-size: 16px;" cellpadding="0" cellspacing="0">
        @For Each s As String In BLL.BLLAddress.Lines(Model)
            @<tr>
                <td colspan="4">
                    @Html.Raw(s)
                </td>
            </tr>
        Next
            
        <tr><td colspan="4">&nbsp;</td></tr>

        <tr>
            <td style="text-align:left;">producto</td>
            <td style="text-align:right;">unidades</td>
            <td style="text-align:right;padding-right:10px;">precio</td>
            <td style="text-align:right;padding-right:10px;">descto</td>
            <td style="text-align:right;">importe</td>
        </tr>
        @For Each item As DTO.DTOPurchaseOrderItem In Model.Items
            @<tr>
                <td style="text-align:left;">
                    <a href="@BLL.BLLProductSku.Url(item.Sku)">
                        @item.Sku.Nom
                    </a>
                </td>
                <td style="text-align:right;padding-right:20px;">@item.Qty</td>
                @If item.ChargeCod = DTO.DTOPurchaseOrderItem.ChargeCods.FOC Then
                    @<td style="text-align:right;white-space: nowrap;" colspan="3">sin cargo</td>
            Else
                    @<td style="text-align:right;white-space: nowrap;padding-right:10px;">@DTO.DTOAmt.CurFormatted(item.Price)</td>
                    @<td style="text-align:right;white-space: nowrap;padding-right:10px;">@(item.dto & "%") </td>
                    @<td style="text-align:right;white-space: nowrap;">@DTO.DTOAmt.CurFormatted(item.Amt)</td>
                End If
            </tr>
        Next
        <tr>
            <td style="text-align:left;" colspan="4">total (IVA no incluido)</td>
            <td style="text-align:right;">@DTO.DTOAmt.CurFormatted(BLL.BLLPurchaseOrder.SumaDeImportes(Model))</td>
        </tr>
    </table>

    <p>
        Reciba un cordial saludo.<br />
        MATIAS MASSO, S.A.
    </p>

</div>
