@ModelType DTOCliApertura
@Code
    Layout = "~/Views/Mail/_Layout2.vbhtml"
    Dim oLang As DTOLang = Model.Lang

    Dim BlPresencial As Boolean
    Dim BlPuericultura As Boolean
    If Model.ContactClass IsNot Nothing Then
        Select Case Model.ContactClass.Guid.ToString
            Case DTOContactClass.Wellknown(DTOContactClass.Wellknowns.botigaPuericultura).Guid.ToString
                BlPresencial = True
                BlPuericultura = True
            Case DTOContactClass.Wellknown(DTOContactClass.Wellknowns.farmacia).Guid.ToString, DTOContactClass.Wellknown(DTOContactClass.Wellknowns.guarderia).Guid.ToString
                BlPresencial = True
                BlPuericultura = False
            Case DTOContactClass.Wellknown(DTOContactClass.Wellknowns.online).Guid.ToString
                BlPresencial = False
                BlPuericultura = False
        End Select
    End If

    Dim labelStyle = "background-color: #F5F5F5; padding:10px 7px 10px 15px;"
    Dim valueStyle = "background-color: #FFFFFF; padding:10px 7px 10px 15px;"

End Code

<table style="font-family: Helvetica Neue, Helvetica, Arial, sans-serif; border-collapse: separate;" >
    <tr>
        <td colspan="2">
            <p>@oLang.Tradueix("Gracias por su interés en distribuir nuestras marcas.",,, "Agradecemos a sua preferência por fazer parte da nossa rede de distribuidores das nossas marcas.")</p>
            <p>@oLang.Tradueix("Para su referencia, detallamos a continuación los datos que nos ha facilitado.",,, "Para a sua referência, abaixo detalhamos os dados que nos facilitou.")</p>
            <p>
                @oLang.Tradueix("Pasamos nota al comercial de la zona que se pondrá en contacto con Vd. en breve para concertar una entrevista.",,, "Passamos a informação ao comercial da sua zona, que contactará consigo em breve para confirmar uma entrevista.")
            </p>
            <p>@oLang.Tradueix("Reciba un cordial saludo",,, "Com os melhores cumprimentos")</p>
        </td>
    </tr>
    <tr><td colspan="2">&nbsp;</td></tr>
    <tr>
        <td width="50%" style="@labelStyle">@oLang.Tradueix("Persona de contacto", "Persona de contacte", "Contact person", "Pessoa de contacto")</td>
        <td style="@valueStyle">@Model.Nom</td>
    </tr>
    <tr >
        <td style="@labelStyle">@oLang.Tradueix("Razón social de facturación", "Rao Social de facturació", "Corporate name", "Razão social de facturação")</td>
        <td style="@valueStyle">@Model.RaoSocial</td>
    </tr>
    <tr>
        <td style="@labelStyle">@oLang.Tradueix("Nombre Comercial del establecimiento", "Nom Comercial de l'establiment", "Commercial name", "Nome comercial do estabelecimento")</td>
        <td style="@valueStyle">@Model.NomComercial</td>
    </tr>
    <tr>
        <td style="@labelStyle">NIF</td>
        <td style="@valueStyle">@Model.NIF</td>
    </tr>
    <tr>
        <td style="@labelStyle">@oLang.Tradueix("Dirección del establecimiento", "Adreça de l'establiment", "Store address", "Direção do estabelecimento")</td>
        <td style="@valueStyle">@Model.Adr</td>
    </tr>
    <tr>
        <td style="@labelStyle">@oLang.Tradueix("Código Postal", "Codi postal", "Zip code", "Código Postal")</td>
        <td style="@valueStyle">@Model.Zip</td>
    </tr>
    <tr>
        <td style="@labelStyle">@oLang.Tradueix("Población", "Població", "Location", "Localidade")</td>
        <td style="@valueStyle">@Model.Cit</td>
    </tr>
    <tr>
        <td style="@labelStyle">@oLang.Tradueix("telefonos de contacto", "telefons de contacte", "contact phone numbers", "telefones de contacto")</td>
        <td style="@valueStyle">@Model.Tel</td>
    </tr>
    <tr>
        <td style="@labelStyle">Email</td>
        <td style="@valueStyle">@Model.Email</td>
    </tr>
    <tr>
        <td style="@labelStyle">Web</td>
        <td style="@valueStyle">@Model.Web</td>
    </tr>

    @If BlPresencial Then
        @<tr>
            <td style="@labelStyle">@oLang.Tradueix("Superficie aproximada de exposición", "Superficie aproximada d'exposició", "Approximated surface", "Superfície aproximada de exposição")</td>
            <td style="@valueStyle">@Model.CodSuperficieText(oLang)</td>
        </tr>
    End If

    <tr>
        <td style="@labelStyle">@oLang.Tradueix("Volumen de facturación anual previsto", "Volum previst de facturació anual", "Expected yearly turnover", "Facturação anual esperada")</td>
        <td style="@valueStyle">@Model.CodVolumenText(oLang)</td>
    </tr>

    @If BlPuericultura Then
        @<tr>
            <td style="@labelStyle">@oLang.Tradueix("Cuota aproximada de la puericultura en la cifra de su negocio (de cero a cien)", "quota aproximada de la puericultura en la xifra del seu negoci (de zero a cent)", "Child care share on your business turnover (from 0 to 100)", "Que quota aproximada ocupa a puericultura na cifra do seu negócio (de zero a cem)")</td>
            <td style="@valueStyle">@(Model.SharePuericultura & "%")</td>
        </tr>

        @<tr>
            <td style="@labelStyle">@oLang.Tradueix("¿Vende algo más que puericultura en su establecimiento? (textil, mobiliario, jugueteria...)", "Ven algo més que puericultura al seu establiment? (textil, mobiliari, jogueteria...)", "Are you selling alternative goods in your store? (textile, furniture, toys...)", "Vende algum outro produto além de puericultura no seu estabelecimento? (textil, mobiliário, brinquedos...)")</td>
            <td style="@valueStyle">@Model.OtherShares</td>
        </tr>
    End If

    <tr>
        <td style="@labelStyle">@oLang.Tradueix("¿Cuantos puntos de venta gestiona o piensa abrir en breve?", "Quants punts de venda gestiona o pensa obrir a curt termini?", "How many sale points do you manage or expect to manage at short term?", "Quantos pontos de venda gere ou pensa abrir em breve?")</td>
        <td style="@valueStyle">@Model.CodSalePointText(oLang)</td>
    </tr>
    <tr>
        <td style="@labelStyle">@oLang.Tradueix("¿Pertenece a alguna franquicia, grupo, asociación de comerciantes, etc.?", "Pertany a alguna franquicia, grup, associació de comerciants, etc.?", "Do you belong to any joint venture, group or retail association?", "Pertence a alguma franquicia, grupo, associação de comerciantes, etc.?")</td>
        <td style="@valueStyle">@Model.Associacions</td>
    </tr>
    <tr>
        <td style="@labelStyle">@oLang.Tradueix("¿Qué antigüedad tiene su negocio?", "Quina antiguitat té el seu negoci?", "How long has your business been running?", "Que antiguidade tem o seu negócio?")</td>
        <td style="@valueStyle">@Model.CodAntiguedadText(oLang)</td>
    </tr>
    <tr>
        <td style="@labelStyle">@oLang.Tradueix("Fecha inicio actividad o apertura prevista", "Data inici activitat o apertura prevista", "Start date or expected opening date", "Data de abertura")</td>
        <td style="@valueStyle">
            @Model.FchApertura.ToString("dd/MM/yy")
        </td>
    </tr>

    @If BlPuericultura Then
        @<tr>
            <td style="@labelStyle">@oLang.Tradueix("¿Tiene alguna experiencia previa?", "Disposa de alguna experiencia previa?", "Any previous experience?", "Tem alguma experiência prévia?")</td>
            <td style="@valueStyle">@Model.CodExperienciaText(oLang)</td>
        </tr>
        @<tr>
            <td style="@labelStyle">@oLang.Tradueix("Por favor seleccione las marcas que está Vd. interesado/a en distribuir", "Si us plau sel.leccioni les marques que está interessat a distribuir", "Please check the brands you are interested on distributing", "Por favor selecione as marcas que está interessado/a em distribuir")</td>
            <td style="@valueStyle">
                @For Each oBrand In Model.Brands
                    @<div>@oBrand.Nom</div>
                Next
            </td>
        </tr>
        @<tr>
            <td style="@labelStyle">@oLang.Tradueix("¿Comercializa ya o piensa comercializar otras marcas de puericultura? Por favor cite cuales", "¿Comercialitza ja o pensa comercialitzar altres marques de puericultura? Si us plau citi quines", "Do you distribute or plan to distribute alternative brands? Please let us know which ones", "Actualmente já comercializa ou pensa comercializar outras marcas de puericultura? Por favor cite quais")</td>
            <td style="@valueStyle">@Model.OtherBrands</td>
        </tr>
    End If

    <tr>
        <td style="@labelStyle">@oLang.Tradueix("Detalle cualquier otra información que considere relevante de su negocio", "Exposi qualsevol altre informació que consideri rellevant del seu negoci", "Let us know whatever info you feel relevant for your business", "Detalhe qualquer outra informação que considere relevante sobre o seu negócio")</td>
        <td style="@valueStyle">@Model.Obs</td>
    </tr>
</table>
