@ModelType List(Of DTOCsb)
@Code
    Layout = "~/Views/Mail/_Layout3.vbhtml"
End Code

<p>Sres.,</p>

<p>
    Debido a un error en la implantación del nuevo formato XML para la domiciliación de recibos SEPA, le han sido cargados los siguientes efectos con vencimiento 3 de Enero en lugar del vencimiento que le correspondía:
</p>
<table align="center" border="0">
    <tr>
        <td width="80px" align="right" style="padding-right:10px;">importe</td>
        <td width="200px">concepto</td>
        <td width="80px">vencimiento correcto</td>
        <td width="80px">cargado en fecha</td>
    </tr>
    @For Each oCsb As DTOCsb In Model
    @<tr>
         <td align="right" style="padding-right:10px;">@DTOAmt.CurFormatted(oCsb.Amt)</td>
         <td>@oCsb.Txt</td>
         <td align="center">@oCsb.vto.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"))</td>
         <td align="center">03/01/2016</td>
    </tr>
    Next
</table>
<p>
    Si desean devolverlo por favor confirmenoslo a cuentas@matiasmasso.es y se lo domiciliaremos de nuevo al vencimiento que le corresponde.
</p>
<p>
    Rogamos disculpen las molestias ocasionadas.
</p>
Matias Masso<br/>
MATIAS MASSO, S.A.
