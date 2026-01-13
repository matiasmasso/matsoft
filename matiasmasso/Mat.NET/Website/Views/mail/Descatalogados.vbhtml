@ModelType DTOUser
@Code
    Layout = "~/Views/Mail/_Layout2.vbhtml"
End Code


    <table style="font-family: Helvetica Neue, Helvetica, Arial, sans-serif; font-size: 1em; width:100%; padding-top: 20px;">

        <tr>
            <td width="100%" valign="top" style="padding:0;vertical-align:top;">
                @Model.lang.Tradueix("Adjuntamos Excel con los últimos productos que han sido descatalogados.",
                                                                      "Adjuntem Excel amb els darrers productes que han estat descatalogats",
                                                                      "Please find attached Excel file with last outdated products")
            </td>
        </tr>
        <tr>
            <td>
                @Model.lang.Tradueix("Agradeceremos actualicen sus bases de datos para reflejar los cambios.",
                                                                                    "Agrairem actualitzin les seves bases de dades per reflectir aquests canvis",
                                                                                    "Please update your system accordingly")
            </td>
        </tr>
        <tr>
            <td>
                @Model.lang.Tradueix("Pueden consultar el histórico de productos descatalogados en el siguiente enlace:",
                                                                          "Es pot consultar l'históric de productes descatalogats al següent enllaç:",
                                                                          "You may browse our outdated archive from next link:")
            </td>
        </tr>
        <tr>
            <td align="center" style="padding-top:20px;">
                <a href="@MmoUrl.Factory(True, "descatalogados")">@MmoUrl.Factory(True, "descatalogados")</a>
            </td>
        </tr>
    </table>


