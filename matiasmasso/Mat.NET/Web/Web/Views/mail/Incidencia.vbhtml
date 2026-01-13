@ModelType DTOIncidencia
@Code
    Layout = "~/Views/mail/_Layout.vbhtml"
    Dim oLang = Model.Customer.Lang
End Code
       
<div style="font-size:0.9em;line-height:1.2em;">
    <p>
        @oLang.Tradueix("Gracias por utilizar nuestro servicio de registro de incidencias online",
                              "Gràcies per utilitzar el nostre servei de registre de incidències online",
                              "Thanks for using our online postsales service")
        <br />
        @oLang.Tradueix("Su incidencia ha sido registrada correctamente con los siguientes datos:",
                         "La seva incidència ha estat registrada correctament amb les següents dades:",
                         "Your incidence has been successfully registered with following data:")
        <br />
    </p>

    <table cellspacing="2" style="font-size:0.9em;line-height:1.2em;width:100%;margin:10px;">
        @For Each item As KeyValuePair(Of String, String) In FEB2.Incidencia.Summary(Model, oLang)
            @<tr>
                <td valign="top" style="border:1px solid #9f9e9e; padding:0.4em 1em 0.4em 1em;">
                    @item.Key
                </td>
                <td valign="top" style="border:1px solid #9f9e9e; padding:0.4em 1em 0.4em 1em;">
                    @Html.Raw(item.Value)
                </td>
            </tr>
        Next
    </table>

    <p>
    @oLang.Tradueix("Nuestro personal de Servicio de Asistencia Técnica se pondrá en contacto con Vd. en breve.",
                                        "El nostre personal de Servei d’Assistència Tècnica es posarà en contacte amb vostès en breu",
                                        "Our support staff will contact you as soon as possible")
    </p>
</div>