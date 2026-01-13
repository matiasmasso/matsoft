@ModelType DTOCliApertura
@Code
    Layout = "~/Views/Mail/_Layout2.vbhtml"
    Dim oLang As DTOLang = DTOApp.current.lang

    Dim BlPresencial As Boolean
    Dim BlPuericultura As Boolean
    If Model.ContactClass IsNot Nothing Then
        Select Case Model.ContactClass.Guid.ToString
            Case DTOContactClass.Wellknown(DTOContactClass.Wellknowns.BotigaPuericultura).Guid.ToString
                BlPresencial = True
                BlPuericultura = True
            Case DTOContactClass.Wellknown(DTOContactClass.Wellknowns.Farmacia).Guid.ToString, DTOContactClass.Wellknown(DTOContactClass.Wellknowns.Guarderia).Guid.ToString
                BlPresencial = True
                BlPuericultura = False
            Case DTOContactClass.Wellknown(DTOContactClass.Wellknowns.Online).Guid.ToString
                BlPresencial = False
                BlPuericultura = False
        End Select
    End If

End Code

<table style="font-family: Helvetica Neue, Helvetica, Arial, sans-serif;">
    <tr>
        <td colspan="2">
            <p>Gracias por su interés en distribuir nuestras marcas.</p>
            <p>Para su referencia, detallamos a continuación los datos que nos ha facilitado.</p>
            <p>
                Pasamos nota al comercial de la zona que se pondrá en contacto con Vd. en breve para concertar una entrevista.
            </p>
            <p>Reciba un cordial saludo</p>
</td>
    </tr>
    <tr><td colspan="2">&nbsp;</td></tr>
    <tr>
        <td>Contacto</td>
        <td>@Model.Nom</td>
    </tr>
    <tr>
        <td>Razón Social</td>
        <td>@Model.RaoSocial</td>
    </tr>
    <tr>
        <td>Nombre Comercial</td>
        <td>@Model.NomComercial</td>
    </tr>
    <tr>
        <td>NIF</td>
        <td>@Model.NIF</td>
    </tr>
    <tr>
        <td>Dirección</td>
        <td>@Model.Adr</td>
    </tr>
    <tr>
        <td>Código Postal</td>
        <td>@Model.Zip</td>
    </tr>
    <tr>
        <td>Población</td>
        <td>@Model.Cit</td>
    </tr>
    <tr>
        <td>Teléfonos</td>
        <td>@Model.Tel</td>
    </tr>
    <tr>
        <td>Email</td>
        <td>@Model.Email</td>
    </tr>
    <tr>
        <td>Web</td>
        <td>@Model.Web</td>
    </tr>

    @If BlPresencial Then
        @<tr>
            <td>Superficie</td>
            <td>@Model.CodSuperficieText(oLang)</td>
        </tr>
    End If

    <tr>
        <td>Volumen</td>
        <td>@Model.CodVolumenText(oLang)</td>
    </tr>

    @If BlPuericultura Then
        @<tr>
             <td>Cuota puericultura</td>
             <td>@(Model.SharePuericultura & "%")</td>
        </tr>

        @<tr>
            <td>Otros sectores</td>
            <td>@Model.OtherShares</td>
        </tr>
    End If

    <tr>
        <td>Puntos de venta</td>
        <td>@Model.CodSalePointText(oLang)</td>
    </tr>
    <tr>
        <td>Asociaciones</td>
        <td>@Model.Associacions</td>
    </tr>
    <tr>
        <td>Antigüedad</td>
        <td>@Model.CodAntiguedadText(oLang)</td>
    </tr>
    <tr>
        <td>Fecha apertura</td>
        <td>
            @Model.FchApertura.ToString("dd/MM/yy")
        </td>
    </tr>

    @If BlPuericultura Then
        @<tr>
            <td>Experiencia</td>
            <td>@Model.CodExperienciaText(oLang)</td>
        </tr>
        @<tr>
            <td>Marcas de interés</td>
            <td>
                @For Each oBrand In Model.Brands
                    @<div>@oBrand.Nom</div>
                Next
            </td>
        </tr>
        @<tr>
            <td>Otras marcas</td>
            <td>@Model.OtherBrands</td>
        </tr>
    End If

    <tr>
        <td>Observaciones</td>
        <td>@Model.Obs</td>
    </tr>
</table>
