<!DOCTYPE HTML PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title>
        Circular
    </title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
</head>
<body bgcolor="#F5F5F5" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" style="margin:0;padding:0;">

    <table cellpadding="0" cellspacing="0" width="100%" bgcolor="#F5F5F5" style="font-family: Helvetica Neue, Helvetica, Arial, sans-serif; font-size: 19px; line-height: 1.4em; height: 100%; ">
        <tr>
            <td style="vertical-align: top;">

                <table align="center" cellpadding="0" cellspacing="0" width="600" bgcolor="#FFFFFF" style="font-family: arial, sans-serif; font-size: 12pt; height: 100%;">

                    <!-- Section Header -->
                    <tr>
                        <td align="right" bgcolor="#DDDDDD" style="color: #777777; padding: 15px 10px 5px 10px; font-size: 11pt">
                            @If ViewBag.ViewInBrowser > "" Then
                                @<a id="HyperlinkBrowse" href="@Html.Raw(ViewBag.ViewInBrowser)" style="text-decoration: none;color: #777777">
                                    ver en el navegador
                                </a>
                            End If
                            @If ViewBag.BajaUrl > "" Then
                                @Html.Raw("&nbsp;|&nbsp;")
                                @<a id="HyperlinkCancelSubscription" href="@Html.Raw(ViewBag.BajaUrl)" style="text-decoration: none;color: #777777">
                                    dar de baja
                                </a>
                            End If
                        </td>
                    </tr>
                    <!-- End Section Header -->
                    <!-- Begin Section Content -->
                    <tr>
                        <td style="padding:10px;">
                            @RenderBody()
                        </td>
                    </tr>
                    <!-- End Section Content -->
                    <!-- Begin Section Footer -->
                    <tr>
                        <td bgcolor="#DDDDDD">
                            <table width="100%" style="color: #777777; font-family: arial, sans-serif; font-size: 11pt; line-height: 1.2em; ">
                                <tr>
                                    <td>&nbsp;</td>
                                    <td bgcolor="#DDDDDD" style="width:38px;">
                                        <a href="https://www.matiasmasso.es" title="MATIAS MASSO, S.A.">
                                            <img src="https://www.matiasmasso.es/Media/Img/Logos/logo.mmo.38x33.gif" width="38" height="33" alt="Logo de MATIAS MASSO, S.A." />
                                        </a>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td width="100%" align="left" style="padding: 10px 10px 10px 0;">
                                        <a href="https://www.matiasmasso.es" style="text-decoration: none; color: #777777; font-weight: bold;">MATIAS MASSO, S.A.</a><br />
                                        Diagonal 403, Barcelona
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <!-- End Section Footer -->
                </table>

            </td>
        </tr>
    </table>

</body>

</html>



