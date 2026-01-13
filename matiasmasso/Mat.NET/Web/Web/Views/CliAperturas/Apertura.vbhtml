@ModelType DTOCliApertura
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)
End Code

<div class="narrowForm">
    <div class="PageTitle">@Mvc.ContextHelper.Tradueix("Aperturas", "Apertures", "Application Requests", "Registo de Profissional")</div>
    <div>
        <div Class="editor-label">@Mvc.ContextHelper.Tradueix("Fecha", "Data", "Date")</div>
        <div class="editor-field">@String.Format(Model.FchCreated, "dd/MM/yyyy HH:mm")</div>
    </div>
    <div>
        <div class="editor-label">@Mvc.ContextHelper.Tradueix("Nombre", "Nom", "Name", "Pessoa de contacto")</div>
        <div class="editor-field">@Model.Nom</div>
    </div>
    <div>
        <div class="editor-label">@Mvc.ContextHelper.Tradueix("Razón social", "Rao Social", "Corporate name", "Razão social de facturação")</div>
        <div class="editor-field">@Model.RaoSocial</div>
    </div>
    <div>
        <div class="editor-label">@Mvc.ContextHelper.Tradueix("Nombre Comercial", "Nom Comercial", "Commercial name", "Nome comercial do estabelecimento")</div>
        <div class="editor-field">@Model.NomComercial</div>
    </div>
    <div>
        <div class="editor-label">NIF</div>
        <div class="editor-field">@Model.NIF</div>
    </div>
    <div>
        <div class="editor-label">@Mvc.ContextHelper.Tradueix("Dirección", "Adreça", "Address", "Direção do estabelecimento")</div>
        <div class="editor-field">@Model.Adr</div>
    </div>

    @If Model.Zona IsNot Nothing Then
        @<div>
            <div Class="editor-label">@Mvc.ContextHelper.Tradueix("Pais", "Pais", "Country", "País")</div>
            <div Class="editor-field">
                @DTOCountry.NomTraduit(Model.Zona.Country, Mvc.ContextHelper.lang())
            </div>
        </div>
        @<div>
            <div Class="editor-label">@Mvc.ContextHelper.Tradueix("Zona", "Zona", "Zone")</div>
            <div Class="editor-field">@Model.Zona.Nom</div>
        </div>
    End If

    <div>
        <div class="editor-label">@Mvc.ContextHelper.Tradueix("Código Postal", "Codi postal", "Zip code", "Código Postal")</div>
        <div class="editor-field">@Model.Zip</div>
    </div>
    <div>
        <div class="editor-label">@Mvc.ContextHelper.Tradueix("Población", "Població", "Location", "Localidade")</div>
        <div class="editor-field">@Model.Cit</div>
    </div>
    <div>
        <div class="editor-label">@Mvc.ContextHelper.Tradueix("Teléfono", "Telèfon", "Phone", "telefones de contacto")</div>
        <div class="editor-field">@Model.Tel</div>
    </div>
    <div>
        <div class="editor-label">email</div>
        <div class="editor-field">@Model.email</div>
    </div>
    <div>
        <div class="editor-label">web</div>
        <div class="editor-field">@Model.web</div>
    </div>
    <div>
        <div class="editor-label">@Mvc.ContextHelper.Tradueix("Superficie", "Superficie", "Surface", "Superfície aproximada de exposição de produtos de puericultura")</div>
        <div class="editor-field">@Model.CodSuperficieText(Mvc.ContextHelper.lang())</div>
    </div>
    <div>
        <div class="editor-label">@Mvc.ContextHelper.Tradueix("Volumen", "Volum", "Turnover", "Facturação anual esperada em produtos de puericultura")</div>
        <div class="editor-field">@Model.CodVolumenText(Mvc.ContextHelper.lang())</div>
    </div>
    <div>
        <div class="editor-label">@Mvc.ContextHelper.Tradueix("Cuota puericultura", "Quota puericultura", "Child care share", "Que quota aproximada ocupa a puericultura na cifra do seu negócio (de zero a cem)")</div>
        <div class="editor-field">@Model.SharePuericultura</div>
    </div>
    <div>
        <div class="editor-label">@Mvc.ContextHelper.Tradueix("Otros", "Altres", "Others")</div>
        <div class="editor-field">@Model.OtherShares</div>
    </div>
    <div>
        <div class="editor-label">@Mvc.ContextHelper.Tradueix("Franquicia", "Franquicia", "Joint venture")</div>
        <div class="editor-field">@Model.CodSalePointText(Mvc.ContextHelper.lang())</div>
    </div>
    <div>
        <div class="editor-label">@Mvc.ContextHelper.Tradueix("Grupos", "Grups", "Groups")</div>
        <div class="editor-field">@Model.Associacions</div>
    </div>
    <div>
        <div class="editor-label">@Mvc.ContextHelper.Tradueix("Antigüedad", "Antiguitat", "Seniority")</div>
        <div class="editor-field">@Model.CodAntiguedadText(Mvc.ContextHelper.lang())</div>
    </div>
    <div>
        <div class="editor-label">@Mvc.ContextHelper.Tradueix("Fecha apertura", "Data apertura", "Opening date")</div>
        <div class="editor-field">@Format(Model.FchApertura, "dd/MM/yy")</div>
    </div>
    <div>
        <div class="editor-label">@Mvc.ContextHelper.Tradueix("Experiencia", "Experiencia", "Experience")</div>
        <div class="editor-field">@Model.CodExperiencia.ToString</div>
    </div>
    <div>
        <div class="editor-label">@Mvc.ContextHelper.Tradueix("Marcas de interés", "Marques de interés", "Interested on brands")</div>
        <div class="editor-field">
            @For Each oBrand In Model.Brands
                @<div>@oBrand.Nom</div>
            Next
        </div>
    </div>
    <div>
        <div class="editor-label">@Mvc.ContextHelper.Tradueix("Otras marcas", "Altres marques", "Other brands")</div>
        <div class="editor-field">@Model.OtherBrands</div>
    </div>
    <div>
        <div class="editor-label">@Mvc.ContextHelper.Tradueix("Observaciones", "Observacions", "Observations")</div>
        <div class="editor-field">@Model.Obs</div>
    </div>
    <div>
        <div class="editor-label">Status</div>
        <div class="editor-field Status">
            <select>
                <option value="0" @IIf(Model.codtancament = 0, "selected", "")>
                    @Mvc.ContextHelper.Tradueix("A la espera de contactar", "A la espera de contactar", "Waiting to contact")
                </option>
                <option value="1" @IIf(Model.codtancament = 1, "selected", "")>
                    @Mvc.ContextHelper.Tradueix("Visitado", "Visitat", "Visited")
                </option>
                <option value="2" @IIf(Model.codtancament = 2, "selected", "")>
                    @Mvc.ContextHelper.Tradueix("Ha pasado pedido", "Ha passat comanda", "Has already ordered")
                </option>
                <option value="3" @IIf(Model.codtancament = 3, "selected", "")>
                    @Mvc.ContextHelper.Tradueix("Cancelado", "Cancel·lat", "Canceled")
                </option>
            </select>
        </div>
    </div>
    <div>
        <div class="editor-label">@Mvc.ContextHelper.Tradueix("Comentarios", "Comentaris", "Comments")</div>
        <div class="editor-field RepObs">
            <textarea>@Model.RepObs</textarea>
        </div>
    </div>
    <div>
        <div class="editor-submit">
            <button>
                @Mvc.ContextHelper.Tradueix("Aceptar", "Acceptar", "Submit")
            </button>
        </div>
    </div>
    <div>
        <a href="/aperturas">
            @Mvc.ContextHelper.Tradueix("Ver todas las aperturas", "Veure totes les apertures", "See all application requests")
        </a>
    </div>

    <input type="hidden" id="Guid" value="@Model.guid.ToString" />
</div>

@Html.Partial("_AvailableOnAppstore")

@Section Scripts
    <script>
        $(document).on('click', '.editor-submit button', function (e) {
           $(this).parent().append(spinner20);
           $(this).hide();

            var url = '@Url.Action("/Update")'
            var data = {
                Guid: $('#Guid').val(),
                RepObs: $('.RepObs textarea').val(),
                Status: $('.Status select').val()
            };
            $.getJSON(url, { data: JSON.stringify(data) }, function (result) {
                spinner20.remove();
                window.location.href = '@Url.Action("Index", "CliAperturas")'
            })
        })

    </script>
End Section

@Section styles
    <style>
        .narrowForm {
            width: 320px;
            margin: auto;
        }

        .editor-label {
            margin: 15px 7px 2px 4px;
            color: darkgray;
        }

        .editor-field {
            margin: 4px 7px 2px 30px;
        }

            .editor-field input, .editor-field textarea {
                width: 100%;
            }

        .editor-submit {
            text-align: right;
            margin: 20px 7px 2px 30px;
        }
    </style>
End Section