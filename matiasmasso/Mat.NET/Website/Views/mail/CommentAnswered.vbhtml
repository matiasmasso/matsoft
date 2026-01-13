@ModelType DTOPostComment
@Code
    Layout = "~/Views/Mail/_Layout.vbhtml"
End Code
<p>
    Su siguiente comentario en nuestra página web ha recibido respuesta:
</p>
<table>
    <tr>
        <td style="padding:4px 15px 2px 4px;">Autor</td>
        <td style="padding:4px 7px 2px 4px;">@Model.User.NicknameForComments</td>
    </tr>
    <tr>
        <td>Fecha</td>
        <td>@Model.Fch.ToString("dd/MM/yy") @Model.Fch.ToString("HH:mm")</td>
    </tr>
    <tr>
        <td>Página</td>
        <td>@Model.ParentTitle.Tradueix(ViewBag.Lang)</td>
    </tr>
</table>

<p>
    Puede consultarla y responder si lo desea en el siguiente enlace:
</p>
<p style="text-align:center;">
    <a href="@Model.Url(ViewBag.Lang, True)">Clic aquí para ver la respuesta al comentario</a>

</p>
