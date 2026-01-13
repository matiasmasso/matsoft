<!DOCTYPE HTML PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
@code
    Dim oLang As DTOLang = ContextHelper.Lang()
End Code
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>
        Circular
    </title>

    <style type="text/css">
        body {
            margin: 10px 0;
            padding: 0 10px;
            font-size: 11pt; 
            /*background: #F9F2E7;*/
        }

        table {
            border-collapse: collapse;
        }

        td {
            font-family: arial, sans-serif;
            color: #333333;
        }

        .Main {
            padding:0 0 20px 0;
            margin:auto;

        }
        .Footer {
            color: #777777; 
        }

        .CorporateName {
            font-weight: bold;
            text-decoration:none;
        }

        .FooterMenu {
            color: #777777; 
            padding: 15px 10px 5px 10px; 
        }
        .FooterMenu a {
            vertical-align:bottom;
            text-decoration: none;
        }


        @@media only screen and (max-width: 640px) {
            body, table, td, p, a, li, blockquote {
            font-size: 13px;
                -webkit-text-size-adjust: none !important;
            }

            table {
                width: 100% !important;
            }

            .responsive-image img {
                height: auto !important;
                max-width: 100% !important;
                width: 100% !important;
            }
        .CorporateName {
            font-size: 13px;
        }
        .CorporateAddress {
            font-size: 11px;
        }
        .FooterMenu a {
            font-size: 11px;
            white-space: nowrap;
        }
        }
    </style>
</head>



<body>
    <table border="0" cellpadding="0" cellspacing="0" align="center" width="640">
        <!--header
        <tr>
            <td style="padding: 15px 10px 5px 10px; ">
                &nbsp; 
            </td>
        </tr>
        -->
        <tr>
            <td class="Main">
                @RenderBody()
            </td>
        </tr>
        <tr>
            <td class="Footer" >
                <table width="100%" cellpadding="0" cellspacing="0" class="Footer" border="0" style="margin-top:20px;">
                    <tr><td style="font-size: 0; line-height: 0;" height="1" bgcolor="#DDDDDD">&nbsp;</td></tr>
                    <tr>
                        <td style="padding:0;margin:0;">
                            <table cellpadding="0" cellspacing="0" border="0" style="border-collapse:collapse">
                                <tr >
                                    <td style="width:38px;padding:0;margin:0;" align="left" >
                                        <a href="https://www.matiasmasso.es" title="MATIAS MASSO, S.A.">
                                            <img src="https://www.matiasmasso.es/Media/Img/Logos/logo.mmo.38x33.gif" width="38" height="33" alt="Logo de MATIAS MASSO, S.A." />
                                        </a>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td align="left" class="Footer" style="padding: 10px 10px 10px 0; white-space: nowrap;">
                                        <a href="https://www.matiasmasso.es" class="CorporateName">
                                            MATIAS MASSO, S.A.
                                        </a>
                                        <br />
                                        <span class="CorporateAddress">Diagonal 403, Barcelona</span>
                                    </td>
                                    <td width="100%"></td>
                                    <td align="right" class="FooterMenu" nowrap="nowrap">
                                        @If ViewBag.ViewInBrowser > "" Then
                                            @<a id="HyperlinkBrowse" href="@Html.Raw(ViewBag.ViewInBrowser)" class="FooterMenu">
                                                ver en el navegador
                                            </a>
                                        End If
                                        @If ViewBag.BajaUrl > "" Then
                                            @Html.Raw("&nbsp;&nbsp;")
                                            @<a id="HyperlinkCancelSubscription" href="@Html.Raw(ViewBag.BajaUrl)" class="FooterMenu">
                                                @Html.Raw(oLang.Tradueix("dar de baja", "donar de baixa", "unsubscribe", "eliminar perfil"))
                                            </a>
                                        End If
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>



