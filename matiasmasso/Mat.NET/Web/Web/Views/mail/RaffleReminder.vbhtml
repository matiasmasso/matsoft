@ModelType DTORaffle

@Code
    Layout = "~/Views/Mail/_Layout.vbhtml"
    Dim sUrl As String = FEB2.Raffles.Url(DTOLang.ESP, True)
    MatHelperStd.UrlHelper.addParam(sUrl, "utm_source", "newsletter")
    MatHelperStd.UrlHelper.addParam(sUrl, "utm_medium", "email")
    MatHelperStd.UrlHelper.addParam(sUrl, "utm_campaign", "raffle reminder")
End Code

<table>
    <tr>

        <td align="center" style="padding: 20px 0px 30px; vertical-align:top;" width="265px">
            <a id="HyperlinkThumbnail" href="@Html.Raw(sUrl)">
                <img id="Thumbnail" src="@FEB2.Raffle.ThumbnailUrl(Model)" style="border-width:0px;" />
            </a>
        </td>

        <td style="padding: 20px 10px 0px 10px; vertical-align: top;" width="100%">
            <table style="font-family: Helvetica Neue, Helvetica, Arial, sans-serif; font-size: 19px; color:#666666; line-height: 1.4em; height: 100%; margin-top:0;padding-top:0; width:100%">
                <tr>
                    <td>
                        <span id="LabelExcerpt">
                            ¿aún no te has inscrito?<br/>
                            ¡aprovecha el último día, es gratis!<br/>
                             @Html.Raw(DTORaffle.RaffleTime(Model, DTOLang.ESP)) <!--el ganador se publicará..-->
                        </span>

                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <a id="HyperlinkReadMore" href="@Html.Raw(sUrl)" style="text-decoration: none;">pincha aquí para participar</a>
                    </td>
                </tr>
            </table>
        </td>

    </tr>
</table>