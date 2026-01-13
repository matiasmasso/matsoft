@ModelType DTOTransmisio
@Code
    Layout = "~/Views/mail/_Layout2.vbhtml"
End Code

<style>
    td {
        padding: 4px 7px 2px 4px;
    }
</style>

<p>
    Adjuntem fitxers en format XML amb les dades del picking corresponent a la transmisió num.
    @Model.Id
    del
    @Model.fch.LocalDateTime.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"))
</p>

<p>
    Podeu descarregar els albarans en Pdf de <a href="@FEB2.Transmisio.UrlPdfDeliveries(Model)">aquest enllaç</a>
</p>

@If Model.deliveries.Exists(Function(x) x.CustomerDocURL.isNotEmpty()) Then

    @<div>
        <p>
            Els següents albarans viatjen directament a consumidor, per lo qual cal substituir la documentació per els originals del client que podeu descarregar-vos dels següents enllaços:
        </p>
        <table>


            @For Each item As DTODelivery In Model.deliveries.FindAll(Function(x) x.CustomerDocURL.isNotEmpty())
                @<tr>
                    <td>albarà</td>
                    <td>
                        @item.Id
                    </td>
                </tr>
                @<tr>
                    <td>destinatari</td>
                    <td>
                        @item.nom
                    </td>
                </tr>
                @<tr>
                    <td>documentació</td>
                    <td>
                        <a href="@item.CustomerDocURL" target="_Blank">
                            @item.CustomerDocURL
                        </a>
                    </td>
                </tr>
                @<tr><td colspan="2">&nbsp;</td>
            </tr>
            Next
        </table>
    </div>
End If


@If Model.Deliveries.Any(Function(x) x.EtiquetesTransport IsNot Nothing) Then

    @<div>
        <p>
            Els següents albarans viatjen directament a consumidor i cal identificar-los amb les etiquetes que podeu descarregar-vos dels següents enllaços:
        </p>
        <table>
            <tr>
                <td>
                    albará
                </td>
                <td>
                    client
                </td>
                <td>
                    enllaç a les etiquetes
                </td>
            </tr>

            @For Each item As DTODelivery In Model.Deliveries.FindAll(Function(x) x.EtiquetesTransport IsNot Nothing)
                @<tr>
                    <td>
                        @item.Id
                    </td>
                    <td>
                        @item.nom
                    </td>
                    <td>
                        <a href=" @FEB2.DocFile.DownloadUrl(item.EtiquetesTransport, False)" target="_Blank">
                            descarregar
                        </a>
                    </td>
                </tr>
            Next
        </table>
    </div>

End If


