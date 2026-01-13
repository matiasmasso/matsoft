@ModelType DTO.DTOPurchaseOrder
@Code
    Layout = "~/Views/mail/_Layout.vbhtml"
    
End Code

<div>


        <p>
            Gracias por participar en la promoción Romer Dual-Fix
        </p>

    <p>
        Le recordamos que la promoción al consumidor consiste en el obsequio de un <a href="https://www.matiasmasso.es/britax/accesorios/pack_espaldas_marcha" target="_blank">pack de accesorios para sillas auto a contramarcha</a> valorado en 49,00 € por la compra de una silla Römer Dual-Fix entre el 15 de Marzo y el 30 de Abril de 2015.
    </p>
    <p>
        Su participación le da derecho a recibir un kit de accesorios sin cargo por cada Römer Dual-Fix que adquiera hasta el 30 de Abril, así como a la publicación de su establecimiento en la lista de distribuidores donde esta promoción es válida.
    </p>
    <p>
        A partir de este momento puede aplicar la promoción si lo desea, pero por favor no la publicite hasta el 15 de Marzo, fecha en que la anunciaremos a los consumidores.
    </p>
    <p>
        Confirmamos su pedido de implantación:
    </p>
    <table style="width:100%;font-family: Helvetica Neue, Helvetica, Arial, sans-serif; font-size: 16px;" cellpadding="0" cellspacing="0">
 @For Each s As String In BLL.BLLAddress.Lines(Model)
    @<tr>
        <td colspan="4">
            @Html.Raw(s)
        </td>
        </tr>
 Next
     
        <tr>
            <td style="text-align:left;">producto</td>
            <td style="text-align:right;">unidades</td>
            <td style="text-align:right;">precio</td>
            <td style="text-align:right;">importe</td>
        </tr>
        @For Each item As DTO.DTOPurchaseOrderItem In Model.Items
            @<tr>
                 <td style="text-align:left;">@item.Sku.Nom</td>
                 <td style="text-align:right;">@item.Qty</td>
        @If item.ChargeCod = DTO.DTOPurchaseOrderItem.ChargeCods.FOC Then
                @<td style="text-align:right;white-space: nowrap;" colspan="2">sin cargo</td>
            Else
                 @<td style="text-align:right;white-space: nowrap;">@DTO.DTOAmt.CurFormatted(item.Price)</td>
                 @<td style="text-align:right;white-space: nowrap;">@DTO.DTOAmt.CurFormatted(item.Amount)</td>
        End If
            </tr>
        Next
    <tr>
        <td style="text-align:left;" colspan="3">total (IVA no incluido)</td>
        <td style="text-align:right;">@DTO.DTOAmt.CurFormatted(BLL.BLLPurchaseOrder.SumaDeImportes(Model))</td>
    </tr>
    </table>

    <p>
        Reciba un cordial saludo.<br/>
        MATIAS MASSO, S.A.
    </p>
 
</div>


