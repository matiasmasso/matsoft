@ModelType DTONoticia

@Code
    Layout = "~/Views/Mail/_Layout.vbhtml"
    Dim sUrl = FEB.Noticia.UrlFriendly(Model, True)
    MatHelperStd.UrlHelper.addParam(sUrl, "utm_source", "newsletter")
    MatHelperStd.UrlHelper.addParam(sUrl, "utm_medium", "email")
    MatHelperStd.UrlHelper.addParam(sUrl, "utm_campaign", "news")
End Code

<table>
    <tr>

        <td align="center" style="padding: 20px 0px 30px; vertical-align:top;" width="265px">
            <a id="HyperlinkThumbnail" href="@Html.Raw(sUrl)">
            <img id="Thumbnail" src="@FEB.Noticia.UrlThumbnail(Model, True)" style="border-width:0px;" /></a>
        </td>

        <td style="padding: 20px 10px 0px 10px; vertical-align: top;" width="100%">
            <table width="100%" style="font-family: Helvetica Neue, Helvetica, Arial, sans-serif; font-size: 19px; color:#666666; line-height: 1.4em; height: 100%; margin-top:0;padding-top:0;">
                <tr>
                    <td>
                        <span id="LabelExcerpt">
                            @Html.Raw(Model.Excerpt)
                        </span>

                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <a id="HyperlinkReadMore" href="@Html.Raw(sUrl)" style="text-decoration: none;">seguir leyendo...</a>
                    </td>
                </tr>
            </table>
        </td>

    </tr>
</table>

