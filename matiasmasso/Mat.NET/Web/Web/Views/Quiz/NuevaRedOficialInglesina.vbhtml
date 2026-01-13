@Modeltype List(Of maxisrvr.DTONuevaRedOficialInglesina)
@Code
    Layout = "~/Views/Shared/_Layout_fullWidth.vbhtml"
    Dim oWebsession As DTO.DTOSession = BLL.BLLSession.Find(Session("SessionId"))
End Code

<style scoped>

    .pagewrapper {
        padding: 20px 7px 0 10px;
        max-width:500px;
        margin:auto;
        text-align:left;
    }

    .Thanks {
        padding: 20px 7px 10px 10px;
        max-width:500px;
        min-height:400px;
        margin:auto;
    }

    .ContactRow {
        padding: 15px 0 5px 0;
    }

    .CellCheckbox {
        display:inline-block;
        width:20px;
        vertical-align:top;
    }

    .CellNom {
        display:inline-block;
        width:320px;
    }

    select {
        background-color: #fcf4af;
    }

    .RowSubmit {
        text-align:right;
    }

    .WordSection1 {
        text-align:center;
    }

    .Category0 {
        background-color:lightyellow;
    }
    .Category1 {
        background-color:#E2EFDA;
    }
    .Category2 {
        background-color:#DDEBF7;
    }
    .Category3 {
        background-color:#FFF2CC;
    }

    .Outdated {
        padding:20px 7px 20px 4px;
        color:red;
        font-size:1.2em;
        font-weight:600;
    }

</style>

<div class="pagewrapper">
    <div class="Outdated">
        Este formulario caducó el 31/12/2014<br/>
        Si desea formar parte de la red de Distribuciión de Inglesina por favor contacte con su representante o con nuestras oficinas.
    </div>
    <fieldset>
        <legend>Nueva Red Comercial de Distribuidores Oficiales de Inglesina</legend>
        <p>
            Solicito la inclusión en la Red Comercial de Distribuidores Oficiales de Inglesina de los siguientes establecimientos, para los que me comprometo a mantener la exposición permanente indicada por el fabricante en función de la superficie declarada:
        </p>
        @For Each item As MaxiSrvr.DTONuevaRedOficialInglesina In Model
            @<div class="ContactRow" data-guid="@item.Contact.Guid.ToString">

                <div class="CellCheckbox">
                    <input type="checkbox" @IIf(item.Category > 0, "checked", "") />
                </div>

                <div class="CellNom">
                    <b>@item.Contact.Nom_o_NomComercial</b><br/>
                    <select @Html.Raw("class='Category" & CInt(item.Category).ToString & "' " & IIf(item.Category = 0, "hidden='hidden'", ""))>
                        <option value="0" @IIf(item.Category = 0, "selected", "")>(seleccionar superficie)</option>
                        <option value="1" @IIf(item.Category = 1, "selected", "")>más de 500m2</option>
                        <option value="2" @IIf(item.Category = 2, "selected", "")>de 100 a 500m2</option>
                        <option value="3" @IIf(item.Category = 3, "selected", "")>menos de 100m2</option>
                    </select>
                    <br />@Html.Raw(item.Contact.Adr.ToHTML)
                </div>

            </div>

        Next


        <div class="RowSubmit">
            <input type="button" value="Aceptar" disabled="disabled" />
        </div>
    </fieldset>
    <br/>

    <div>
        <div >
            <table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 style='border-collapse:collapse'>
                <tr><td style='text-align:left;width:400px;padding:4px 7px 2px 4px;border:solid windowtext 1.0px;background:#F2F2F2;'><p class=MsoNormal><span style='color:black;mso-fareast-language:ES'>Categoría de establecimiento<o:p></o:p></span></p></td><td nowrap valign=bottom style='width:100px;border:solid windowtext 1.0px;border-left:none;background:#F2F2F2;'><p class=MsoNormal align=center style='text-align:center'><span style='color:black;mso-fareast-language:ES'>C<o:p></o:p></span></p></td><td nowrap valign=bottom style='width:100px;border:solid windowtext 1.0px;border-left:none;background:#F2F2F2;'><p class=MsoNormal align=center style='text-align:center'><span style='color:black;mso-fareast-language:ES'>B<o:p></o:p></span></p></td><td nowrap valign=bottom style='width:100px;border:solid windowtext 1.0px;border-left:none;background:#F2F2F2;'><p class=MsoNormal align=center style='text-align:center'><span style='color:black;mso-fareast-language:ES'>A<o:p></o:p></span></p></td></tr>
                <tr><td style='text-align:left;padding:4px 7px 2px 4px;border:solid windowtext 1.0px;border-top:none;background:#E7E6E6;'><p class=MsoNormal><span style='color:black;mso-fareast-language:ES'>Superficie<o:p></o:p></span></p></td><td nowrap valign=bottom style='padding:4px 7px 2px 4px;border-top:none;border-left:none;border-bottom:solid windowtext 1.0px;border-right:solid windowtext 1.0px;background:#E7E6E6;'><p class=MsoNormal align=center style='text-align:center'><span style='color:black;mso-fareast-language:ES'>&lt;100 m2<o:p></o:p></span></p></td><td nowrap valign=bottom style='padding:4px 7px 2px 4px;border-top:none;border-left:none;border-bottom:solid windowtext 1.0px;border-right:solid windowtext 1.0px;background:#E7E6E6;'><p class=MsoNormal align=center style='text-align:center'><span style='color:black;mso-fareast-language:ES'>100-500 m2<o:p></o:p></span></p></td><td nowrap valign=bottom style='padding:4px 7px 2px 4px;border-top:none;border-left:none;border-bottom:solid windowtext 1.0px;border-right:solid windowtext 1.0px;background:#E7E6E6;'><p class=MsoNormal align=center style='text-align:center'><span style='color:black;mso-fareast-language:ES'>&gt; 500 m2<o:p></o:p></span></p></td></tr>
                <tr><td style='text-align:left;padding:4px 7px 2px 4px;border:solid windowtext 1.0px;border-top:none;'><p class=MsoNormal><u><span style='color:#0563C1;mso-fareast-language:ES'><a href="https://www.matiasmasso.es/inglesina/trilogy"><span style='color:#0563C1'>Cochecito de paseo Trilogy</span></a><o:p></o:p></span></u></p></td><td nowrap rowspan=2 style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0px;border-right:solid windowtext 1.0px;background:#FFF2CC;'><p class=MsoNormal align=center style='text-align:center'><span style='color:black;mso-fareast-language:ES'>X<o:p></o:p></span></p></td><td nowrap rowspan=2 style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0px;border-right:solid windowtext 1.0px;background:#DDEBF7;'><p class=MsoNormal align=center style='text-align:center'><span style='color:black;mso-fareast-language:ES'>X<o:p></o:p></span></p></td><td nowrap valign=bottom style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0px;border-right:solid windowtext 1.0px;background:#E2EFDA;'><p class=MsoNormal align=center style='text-align:center'><span style='color:black;mso-fareast-language:ES'>X<o:p></o:p></span></p></td></tr>
                <tr><td style='text-align:left;padding:4px 7px 2px 4px;border:solid windowtext 1.0px;border-top:none;'><p class=MsoNormal><u><span style='color:#0563C1;mso-fareast-language:ES'><a href="https://www.matiasmasso.es/inglesina/OTUTTO_DELUXE"><span style='color:#0563C1'>Cochecito de paseo Otutto</span></a><o:p></o:p></span></u></p></td><td nowrap valign=bottom style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0px;border-right:solid windowtext 1.0px;background:#E2EFDA;'><p class=MsoNormal align=center style='text-align:center'><span style='color:black;mso-fareast-language:ES'>X<o:p></o:p></span></p></td></tr>
                <tr><td style='text-align:left;padding:4px 7px 2px 4px;border:solid windowtext 1.0px;border-top:none;'><p class=MsoNormal><u><span style='color:#0563C1;mso-fareast-language:ES'><a href="https://www.matiasmasso.es/inglesina/CLASSICA"><span style='color:#0563C1'>Cochecito de paseo Classica</span></a><o:p></o:p></span></u></p></td><td nowrap valign=bottom style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0px;border-right:solid windowtext 1.0px;'><p class=MsoNormal align=center style='text-align:center'><span style='color:black;mso-fareast-language:ES'>&nbsp;<o:p></o:p></span></p></td><td nowrap valign=bottom style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0px;border-right:solid windowtext 1.0px;'><p class=MsoNormal align=center style='text-align:center'><span style='color:black;mso-fareast-language:ES'>&nbsp;<o:p></o:p></span></p></td><td nowrap rowspan=3 style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0px;border-right:solid windowtext 1.0px;background:#E2EFDA;'><p class=MsoNormal align=center style='text-align:center'><span style='color:black;mso-fareast-language:ES'>X<o:p></o:p></span></p></td></tr>
                <tr><td style='text-align:left;padding:4px 7px 2px 4px;border:solid windowtext 1.0px;border-top:none;'><p class=MsoNormal><u><span style='color:#0563C1;mso-fareast-language:ES'><a href="https://www.matiasmasso.es/inglesina/QUAD"><span style='color:#0563C1'>Cochecito de paseo Quad</span></a><o:p></o:p></span></u></p></td><td nowrap valign=bottom style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0px;border-right:solid windowtext 1.0px;'><p class=MsoNormal align=center style='text-align:center'><span style='color:black;mso-fareast-language:ES'>&nbsp;<o:p></o:p></span></p></td><td nowrap valign=bottom style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0px;border-right:solid windowtext 1.0px;'><p class=MsoNormal align=center style='text-align:center'><span style='color:black;mso-fareast-language:ES'>&nbsp;<o:p></o:p></span></p></td></tr>
                <tr><td style='text-align:left;padding:4px 7px 2px 4px;border:solid windowtext 1.0px;border-top:none;'><p class=MsoNormal><u><span style='color:#0563C1;mso-fareast-language:ES'><a href="https://www.matiasmasso.es/inglesina/VITTORIA"><span style='color:#0563C1'>Cochecito de paseo Vittoria</span></a><o:p></o:p></span></u></p></td><td nowrap valign=bottom style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0px;border-right:solid windowtext 1.0px;'><p class=MsoNormal align=center style='text-align:center'><span style='color:black;mso-fareast-language:ES'>&nbsp;<o:p></o:p></span></p></td><td nowrap valign=bottom style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0px;border-right:solid windowtext 1.0px;'><p class=MsoNormal align=center style='text-align:center'><span style='color:black;mso-fareast-language:ES'>&nbsp;<o:p></o:p></span></p></td></tr>
                <tr><td style='text-align:left;padding:4px 7px 2px 4px;border:solid windowtext 1.0px;border-top:none;'><p class=MsoNormal><u><span style='color:#0563C1;mso-fareast-language:ES'><a href="https://www.matiasmasso.es/inglesina/TRIP"><span style='color:#0563C1'>Sillita de paseo Trip</span></a><o:p></o:p></span></u></p></td><td nowrap valign=bottom style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0px;border-right:solid windowtext 1.0px;background:#FFF2CC;'><p class=MsoNormal align=center style='text-align:center'><span style='color:black;mso-fareast-language:ES'>X<o:p></o:p></span></p></td><td nowrap valign=bottom style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0px;border-right:solid windowtext 1.0px;background:#DDEBF7;'><p class=MsoNormal align=center style='text-align:center'><span style='color:black;mso-fareast-language:ES'>X<o:p></o:p></span></p></td><td nowrap valign=bottom style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0px;border-right:solid windowtext 1.0px;background:#E2EFDA;'><p class=MsoNormal align=center style='text-align:center'><span style='color:black;mso-fareast-language:ES'>X<o:p></o:p></span></p></td></tr>
                <tr><td style='text-align:left;padding:4px 7px 2px 4px;border:solid windowtext 1.0px;border-top:none;'><p class=MsoNormal><u><span style='color:#0563C1;mso-fareast-language:ES'><a href="https://www.matiasmasso.es/inglesina/GUSTO"><span style='color:#0563C1'>Trona Gusto</span></a><o:p></o:p></span></u></p></td><td nowrap valign=bottom style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0px;border-right:solid windowtext 1.0px;'><p class=MsoNormal align=center style='text-align:center'><span style='color:black;mso-fareast-language:ES'>&nbsp;<o:p></o:p></span></p></td><td nowrap rowspan=2 style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0px;border-right:solid windowtext 1.0px;background:#DDEBF7;'><p class=MsoNormal align=center style='text-align:center'><span style='color:black;mso-fareast-language:ES'>X<o:p></o:p></span></p></td><td nowrap valign=bottom style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0px;border-right:solid windowtext 1.0px;background:#E2EFDA;'><p class=MsoNormal align=center style='text-align:center'><span style='color:black;mso-fareast-language:ES'>X<o:p></o:p></span></p></td></tr>
                <tr><td style='text-align:left;padding:4px 7px 2px 4px;border:solid windowtext 1.0px;border-top:none;'><p class=MsoNormal><u><span style='color:#0563C1;mso-fareast-language:ES'><a href="https://www.matiasmasso.es/inglesina/ZUMA"><span style='color:#0563C1'>Trona Zuma</span></a><o:p></o:p></span></u></p></td><td nowrap valign=bottom style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0px;border-right:solid windowtext 1.0px;'><p class=MsoNormal align=center style='text-align:center'><span style='color:black;mso-fareast-language:ES'>&nbsp;<o:p></o:p></span></p></td><td nowrap valign=bottom style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0px;border-right:solid windowtext 1.0px;background:#E2EFDA;'><p class=MsoNormal align=center style='text-align:center'><span style='color:black;mso-fareast-language:ES'>X<o:p></o:p></span></p></td></tr>
                <tr><td style='text-align:left;padding:4px 7px 2px 4px;border:solid windowtext 1.0px;border-top:none;'><p class=MsoNormal><u><span style='color:#0563C1;mso-fareast-language:ES'><a href="https://www.matiasmasso.es/inglesina/LODGE"><span style='color:#0563C1'>Cuna Lodge</span></a><o:p></o:p></span></u></p></td><td nowrap valign=bottom style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0px;border-right:solid windowtext 1.0px;'><p class=MsoNormal align=center style='text-align:center'><span style='color:black;mso-fareast-language:ES'>&nbsp;<o:p></o:p></span></p></td><td nowrap valign=bottom style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0px;border-right:solid windowtext 1.0px;'><p class=MsoNormal align=center style='text-align:center'><span style='color:black;mso-fareast-language:ES'>&nbsp;<o:p></o:p></span></p></td><td nowrap valign=bottom style='border-top:none;border-left:none;border-bottom:solid windowtext 1.0px;border-right:solid windowtext 1.0px;background:#E2EFDA;'><p class=MsoNormal align=center style='text-align:center'><span style='color:black;mso-fareast-language:ES'>X<o:p></o:p></span></p></td></tr>
            </table>
        </div>
    </div>

</div>

<div class="Thanks" hidden="hidden">
    <fieldset>
        <legend>Nueva Red Comercial de Distribuidores Oficiales de Inglesina</legend>
        <p>
            Gracias por registrarse.
        </p>
        <p>
            Acabamos de enviarle un email de confirmación a @BLL.BLLSession.UserEmailAddress(oWebsession) .
        </p>
        </fieldset>
</div>

@Section Script
    <script src="~/Media/js/NuevaRedOficialInglesina.js"></script>
End Section