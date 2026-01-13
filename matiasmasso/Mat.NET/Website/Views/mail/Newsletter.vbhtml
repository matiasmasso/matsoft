@ModelType DTONewsletter
@Code
    Layout = "~/Views/Mail/_Layout2.vbhtml"
    Dim sUrl As String = ""
    MatHelperStd.UrlHelper.addParam(sUrl, "utm_source", "newsletter")
    MatHelperStd.UrlHelper.addParam(sUrl, "utm_medium", "email")
    MatHelperStd.UrlHelper.addParam(sUrl, "utm_campaign", "news")
    
    Dim oLang As DTOLang = Model.Lang
End Code

<table>

    @For Each item In Model.Sources

                @<tr>

                    <td align="center" style="padding: 20px 0px 30px; vertical-align:top;" width="100px">
                        <a id="HyperlinkThumbnail" href="@Html.Raw(FEB.Newsletter.ItemUrl(item, True))">
                            <img id="Thumbnail" src="@Html.Raw(FEB.Newsletter.ImageUrl(item, True))" width="100" style="width:100px;border-width:0px;" />
                        </a>
                    </td>

                    <td style="padding: 20px 10px 0px 10px; vertical-align: top;" width="100%">
                        <table width="100%" style="font-family: Helvetica Neue, Helvetica, Arial, sans-serif; color:#666666; line-height: 1.4em; height: 100%; margin-top:0;padding-top:0;">
                            <tr>
                                <td>
                                    <a id="HyperlinkThumbnail" href="@Html.Raw(FEB.Newsletter.ItemUrl(item, True))" style="font-weight:700;margin-bottom:10px;">
                                        @Html.Raw(item.Title.Tradueix(oLang))
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span id="LabelExcerpt">
                                        @Html.Raw(item.Excerpt.Tradueix(oLang))
                                    </span>

                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <a id="HyperlinkReadMore" href="@Html.Raw(FEB.Newsletter.ItemUrl(item, True))" style="text-decoration: none;">
                                    @Html.Raw(oLang.Tradueix("seguir leyendo", "seguir llegint", "read more", "continuar a lêr"))...
                                    </a>
                                </td>
                            </tr>
                        </table>
                    </td>

                </tr>
    Next

</table>

