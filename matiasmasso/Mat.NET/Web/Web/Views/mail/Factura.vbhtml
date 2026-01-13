@ModelType DTOInvoice
@Code
    Layout = "~/Views/mail/_Layout2.vbhtml"
End Code

<div>
    <table>
        <tr>
            <td>
                Apreciado cliente,
            </td>
        </tr>
        <tr><td>&nbsp;</td></tr>
        <tr>
            <td>
                Le informamos que tiene a su disposición su nueva Factura Electrónica numero @Model.Num del @Model.fch.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES")), que esperamos sea de su conformidad.
            </td>
        </tr>

        <tr><td>&nbsp;</td></tr>
        <tr>
            <td>
                <a href="@FEB2.Invoice.PdfUrl(Model, True)">Descarguela aquí</a>
                &nbsp;
                <a href="@FEB2.Invoice.PdfUrl(Model, True)">
                    <img src="https://www.matiasmasso.es/Media/Img/Ico/pdf.gif" />
                       
                </a>
            </td>
        </tr>
        <tr><td>&nbsp;</td></tr>

        <tr>
            <td>
                Le recordamos que la Factura Electronica con firma digital sustituye a la de papel y tiene la misma validez legal.
                <br/>No necesita imprimirla.
                <br/>Una oficina sin papeles es más respetuosa con el medio ambiente.
            </td>
        </tr>
        <tr>
            <td>
                Puede recuperar cualquier factura anterior desde
                &nbsp;
                <a href="https://www.matiasmasso.es/facturas" >www.matiasmasso.es/facturas</a>
            </td>
        </tr>
        <tr>
            <td>
                Si tiene cualquier duda puede ponerse en contacto con nuestro departamento de administración (tel: 932.541.520 cuentas@matiasmasso.es)
            </td>
        </tr>
        <tr><td>&nbsp;</td></tr>
        <tr>
            <td>
                Atentamente,
            </td>
        </tr>

    </table>
</div>