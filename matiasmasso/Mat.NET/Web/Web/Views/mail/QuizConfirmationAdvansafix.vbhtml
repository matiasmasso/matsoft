@ModelType DTOQuizAdvansafix
@Code
    ViewBag.Title = "QuizConfirmationConsumerFair"
    Layout = "~/Views/mail/_Layout.vbhtml"
    Dim exs As New List(Of Exception)
    FEB2.Contact.Load(Model.Customer, exs)
End Code

    <p>
        Gracias por declarar sus existencias de producto.
    <p>
        Confirmamos sus datos, que puede modificar en cualquier momento hasta el 10 de Noviembre desde el mismo enlace.
    </p>

<table style="font-family: Helvetica Neue, Helvetica, Arial, sans-serif; font-size: 19px;width:80%;margin:auto;color:navy;">

    <tr>
        <td colspan="3">
            @Html.Raw(FEB2.Contact.HtmlNameAndAddress(Model.Customer))
        </td>
    </tr>
    <tr><td colspan="3">&nbsp;</td></tr>

    <tr>
        <td style="text-wrap:none">
            Römer Advansafix II SICT
        </td>
        <td style="text-align:right;">
            @Model.QtySICT
        </td>
        <td style="text-align:left;">
            @IIf(Model.QtySICT = 1, "unidad", "unidades")
        </td>
    </tr>

    <tr>
        <td style="text-wrap:none">
            Römer Advansafix II
        </td>
        <td style="text-align:right;">
            @Model.QtyNoSICT
        </td>
        <td style="text-align:left;">
            @IIf(Model.QtyNoSICT = 1, "unidad", "unidades")
        </td>
    </tr>
</table>

<p>
    Le recordamos que puede consultar las condiciones de esta acción en <a href="https://www.matiasmasso.es/news/advansafix/precio">nuestra sección de noticias</a> de acceso restringido al profesional.
</p>

