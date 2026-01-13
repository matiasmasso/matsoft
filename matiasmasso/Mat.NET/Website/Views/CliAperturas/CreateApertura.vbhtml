@ModelType DTOCliApertura
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"


    Dim exs As New List(Of Exception)
    Dim oAllContactClasses = FEB.ContactClasses.AllWithChannelSync(exs).ToList
    Dim oContactClasses = oAllContactClasses.Where(Function(x) x.DistributionChannel.UnEquals(DTODistributionChannel.Wellknown(DTODistributionChannel.Wellknowns.Diversos))).ToList
    Dim oNullContactClass As New DTOContactClass(Guid.Empty)
    oNullContactClass.Nom = New DTOLangText("(seleccionar actividad)", "(Sel.leccionar activitat)", "(Select an activity)", "(selecionar uma atividade)")
    oContactClasses.Insert(0, oNullContactClass)
End Code

<div class="narrowForm">

    <div class="PageTitle">@ContextHelper.Tradueix("Registro de Profesional", "Registre de Professional", "Professional Application Request", "Registo de Profissional")</div>

    <div class="formulari">

        <div>
            <div class="editor-label">@ContextHelper.Tradueix("Persona de contacto", "Persona de contacte", "Contact person", "Pessoa de contacto")</div>
            <div class="editor-field Nom">
                <input type="text" maxlength="50" />
            </div>
        </div>

        <div>
            <div class="editor-label">@ContextHelper.Tradueix("Razón social de facturación", "Rao Social de facturació", "Corporate name", "Razão social de facturação")</div>
            <div class="editor-field RaoSocial">
                <input type="text" maxlength="50" />
            </div>
        </div>

        <div>
            <div class="editor-label">@ContextHelper.Tradueix("Nombre Comercial del establecimiento", "Nom Comercial de l'establiment", "Commercial name", "Nome comercial do estabelecimento")</div>
            <div class="editor-field NomComercial">
                <input type="text" maxlength="50" />
            </div>
        </div>
        
        <div>
            <div class="editor-label">NIF</div>
            <div class="editor-field NIF">
                <input type="text" maxlength="10" />
            </div>
        </div>

        <div>
            <div class="editor-label">@ContextHelper.Tradueix("Dirección del establecimiento", "Adreça de l'establiment", "Store address", "Direção do estabelecimento")</div>
            <div class="editor-field Adr">
                <input type="text" maxlength="100" />
            </div>
        </div>

        <div>
            <div class="editor-label">@ContextHelper.Tradueix("Pais", "Pais", "Country", "País")</div>
            <div class="editor-field Country">
                <select>
                    @For Each oCountry As DTOCountry In FEB.Countries.AllSync(ContextHelper.lang(), exs)
                        If oCountry.Equals(Website.GlobalVariables.Emp.DefaultCountry) Then
                            @<option selected value="@oCountry.Guid.ToString">@DTOCountry.NomTraduit(oCountry, ContextHelper.lang())</option>
                        Else
                            @<option value="@oCountry.Guid.ToString">@DTOCountry.NomTraduit(oCountry, ContextHelper.lang())</option>
                        End If
                    Next
                </select>
            </div>
        </div>

        <div>
            <div class="editor-label">@ContextHelper.Tradueix("Código Postal", "Codi postal", "Zip code", "Código Postal")</div>
            <div class="editor-field Zip">
                <input type="text" maxlength="50" />
            </div>
        </div>

        <div>
            <div class="editor-label">@ContextHelper.Tradueix("Población", "Població", "Location", "Localidade")</div>
            <div class="editor-field Location">
                <input type="text" maxlength="50" />
            </div>
        </div>
        
        <div>
            <div class="editor-label">@ContextHelper.Tradueix("telefonos de contacto", "telefons de contacte", "contact phone numbers", "telefones de contacto")</div>
            <div class="editor-field Tel">
                <input type="text" maxlength="50" />
            </div>
        </div>
                        
        <div>
            <div class="editor-label">email</div>
            <div class="editor-field Email">
                <input type="text" maxlength="100" />
            </div>
        </div>
                       
        <div>
            <div class="editor-label">web</div>
            <div class="editor-field Web">
                <input type="text" maxlength="50" />
            </div>
        <div>

            <div>
                <div class="editor-label">@ContextHelper.Tradueix("Actividad", "Activitat", "Activity", "Actividade")</div>
                <div class="editor-field Activitat">
                    <select>
                        @For Each oContactClass As DTOContactClass In oContactClasses
                            If oContactClass.Equals(Model.ContactClass) Then
                                @<option selected value="@oContactClass.Guid.ToString">@oContactClass.Nom.Tradueix(ContextHelper.lang())</option>
                            Else
                                @<option value="@oContactClass.Guid.ToString">@oContactClass.Nom.Tradueix(ContextHelper.lang())</option>
                            End If
                        Next
                    </select>
                </div>
            </div>

            <div class="ShowIfPresencial" hidden="hidden">
                <div class="editor-label">@ContextHelper.Tradueix("Superficie aproximada de exposición", "Superficie aproximada d'exposició", "Approximated surface", "Superfície aproximada de exposição")</div>
                <div class="editor-field CodSuperficie">
                    @For i As DTOCliApertura.CodsSuperficie = 1 To [Enum].GetValues(GetType(DTOCliApertura.CodsSuperficie)).Length - 1
                        @<div>
                            <Label Class="radioLabel">
                                <input type="radio" value="@cInt(i)" name="CodSuperficie" />
                                <span Class="wrappable">@DTOCliApertura.CodSuperficieText(i, ContextHelper.lang())</span>
                            </Label>
                        </div>
                    Next
                </div>
            </div>

            <div>
                <div class="editor-label">@ContextHelper.Tradueix("Volumen de facturación anual previsto", "Volum previst de facturació anual", "Expected yearly turnover", "Facturação anual esperada")</div>
                <div class="editor-field CodVolumen">
                    @For i As DTOCliApertura.CodsVolumen = 1 To [Enum].GetValues(GetType(DTOCliApertura.CodsVolumen)).Length - 1
                        @<div>
                            <Label Class="radioLabel">
                                <input type="radio" value="@CInt(i)" name="CodVolumen" />
                                <span Class="wrappable">@DTOCliApertura.CodVolumenText(i, ContextHelper.lang())</span>
                            </Label>
                        </div>
                    Next
                </div>
            </div>

            <div class="ShowIfPuericultura" hidden="hidden">
                <div class="editor-label">@ContextHelper.Tradueix("Cuota aproximada de la puericultura en la cifra de su negocio (de cero a cien)", "quota aproximada de la puericultura en la xifra del seu negoci (de zero a cent)", "Child care share on your business turnover (from 0 to 100)", "Que quota aproximada ocupa a puericultura na cifra do seu negócio (de zero a cem)")</div>
                <div class="editor-field SharePuericultura">
                    <input type="number" />
                </div>
            </div>

            <div class="ShowIfPuericultura" hidden="hidden">
                <div class="editor-label ShowIfPuericultura" hidden="hidden">@ContextHelper.Tradueix("¿Vende algo más que puericultura en su establecimiento? (textil, mobiliario, jugueteria...)", "Ven algo més que puericultura al seu establiment? (textil, mobiliari, jogueteria...)", "Are you selling alternative goods in your store? (textile, furniture, toys...)", "Vende algum outro produto além de puericultura no seu estabelecimento? (textil, mobiliário, brinquedos...)")</div>
                <div class="editor-field OtherShares">
                    <input type="text" />
                </div>
            </div>

            <div class="ShowIfPresencial" hidden="hidden">
                <div class="editor-label">@ContextHelper.Tradueix("¿Cuantos puntos de venta gestiona o piensa abrir en breve?", "Quants punts de venda gestiona o pensa obrir a curt termini?", "How many sale points do you manage or expect to manage at short term?", "Quantos pontos de venda gere ou pensa abrir em breve?")</div>
                <div class="editor-field CodSalePoint">
                    @For i As DTOCliApertura.CodsSalePoint = 1 To [Enum].GetValues(GetType(DTOCliApertura.CodsSalePoint)).Length - 1
                        @<div>
                            <Label Class="radioLabel">
                                <input type="radio" value="@CInt(i)" name="CodSalePoint" />
                                <span Class="wrappable">@DTOCliApertura.CodSalePointText(i, ContextHelper.lang())</span>
                            </Label>
                        </div>
                    Next
                </div>
            </div>

            <div>
                <div class="editor-label">@ContextHelper.Tradueix("¿Pertenece a alguna franquicia, grupo, asociación de comerciantes, etc.?", "Pertany a alguna franquicia, grup, associació de comerciants, etc.?", "Do you belong to any joint venture, group or retail association?", "Pertence a alguma franquicia, grupo, associação de comerciantes, etc.?")</div>
                <div class="editor-field Associacions">
                    <input type="text" />
                </div>
            </div>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        </div>
            <div>
                <div class="editor-label">@ContextHelper.Tradueix("¿Qué antigüedad tiene su negocio?", "Quina antiguitat té el seu negoci?", "How long has your business been running?", "Que antiguidade tem o seu negócio?")</div>
                <div class="editor-field CodAntiguedad">
                    @For i As DTOCliApertura.CodsAntiguedad = 1 To [Enum].GetValues(GetType(DTOCliApertura.CodsAntiguedad)).Length - 1
                        @<div>
                            <Label Class="radioLabel">
                                <input type="radio" value="@CInt(i)" name="CodAntiguedad" />
                                <span Class="wrappable">@DTOCliApertura.CodAntiguedadText(i, ContextHelper.lang())</span>
                            </Label>
                        </div>
                    Next
                </div>
            </div>

            <div>
                <div class="editor-label">@ContextHelper.Tradueix("Fecha inicio actividad o apertura prevista", "Data inici activitat o apertura prevista", "Start date or expected opening date", "Data de abertura")</div>
                <div class="editor-field FchApertura">
                    <input type="date" />
                </div>
            </div>                                             
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          </div>

        <div class="ShowIfPuericultura" hidden="hidden">
            <div class="editor-label">@ContextHelper.Tradueix("¿Tiene alguna experiencia previa?", "Disposa de alguna experiencia previa?", "Any previous experience?", "Tem alguma experiência prévia?")</div>
            <div class="editor-field">
                @For i As DTOCliApertura.CodsExperiencia = 1 To [Enum].GetValues(GetType(DTOCliApertura.CodsExperiencia)).Length - 1
                    @<div>
                        <Label Class="radioLabel">
                            <input type="radio" value="@CInt(i)" name="CodExperiencia" />
                            <span Class="wrappable">@DTOCliApertura.CodExperienciaText(i, ContextHelper.lang())</span>
                        </Label>
                    </div>
                Next
            </div>
        </div>

        <div class="ShowIfPuericultura" hidden="hidden">
            <div class="editor-label">@ContextHelper.Tradueix("Por favor seleccione las marcas que está Vd. interesado/a en distribuir", "Si us plau sel.leccioni les marques que está interessat a distribuir", "Please check the brands you are interested on distributing", "Por favor selecione as marcas que está interessado/a em distribuir")</div>
            <div class="editor-field Brands" >
                @For Each oBrand As DTOProductBrand In FEB.ProductBrands.AllSync(exs, Website.GlobalVariables.Emp)
                    @<div>
                        <input type="checkbox" value="@oBrand.Guid.ToString" />
                        @oBrand.Nom
                    </div>
                Next
            </div>
        </div>

        <div class="ShowIfPuericultura" hidden="hidden">
            <div class="editor-label">@ContextHelper.Tradueix("¿Comercializa ya o piensa comercializar otras marcas de puericultura? Por favor cite cuales", "¿Comercialitza ja o pensa comercialitzar altres marques de puericultura? Si us plau citi quines", "Do you distribute or plan to distribute alternative brands? Please let us know which ones", "Actualmente já comercializa ou pensa comercializar outras marcas de puericultura? Por favor cite quais")</div>
            <div class="editor-field OtherBrands">
                <input type="text" />
            </div>
        </div>
                                                
        <div>
            <div class="editor-label">@ContextHelper.Tradueix("Detalle cualquier otra información que considere relevante de su negocio", "Exposi qualsevol altre informació que consideri rellevant del seu negoci", "Let us know whatever info you feel relevant for your business", "Detalhe qualquer outra informação que considere relevante sobre o seu negócio")</div>
            <div class="editor-field Obs">
                <textarea></textarea>
            </div>
        </div>

        <div hidden="hidden" id="errors">
            @ContextHelper.Tradueix("Por favor solvente los siguientes errores:", "Si us plau solventi els següents errors", "Please amend next fields:", "Por favor solvente os seguintes erros:")
            <div id="errorsEnum">

            </div>
        </div>

        <div>
            <div class="editor-submit">
                <button>
                    @ContextHelper.Tradueix("Aceptar", "Acceptar", "Submit", "Aceitar")
                </button>
            </div>
        </div>

    </div>

    <div class="Thanks" hidden="hidden">
        <p>
            @ContextHelper.Tradueix("Gracias por su interés en nuestras marcas", "Gracies per el seu interés en les nostres marques", "Thanks for your interest on our brands", "Agradecemos o seu interesse pelas nossas marcas")
        </p>
        <p>
            @ContextHelper.Tradueix("Nuestro comercial de zona se pondrá en contacto en breve para concertar una entrevista", "El nostre comercial de zona el contactará per concertar una entrevista", "A sales representative will contact you for an interview", "O nosso comercial da sua zona, brevemente entrará em contacto consigo para agendar uma entrevista")
        </p>
            </div>

</div>





@Section Scripts
    <script>
        var activityOnline = '@DTOContactClass.Wellknown(DTOContactClass.Wellknowns.Online)'
        var activityPuericultura = '@DTOContactClass.Wellknown(DTOContactClass.Wellknowns.BotigaPuericultura)'
        var activityFarmacia = '@DTOContactClass.Wellknown(DTOContactClass.Wellknowns.Farmacia)'
        var activityGuarderia = '@DTOContactClass.Wellknown(DTOContactClass.Wellknowns.Guarderia)'

        $(document).on('change', '.editor-field.Zip input', function (e) {
            var url = '@Url.Action("/Location")';
            var data = {
                Country: $('div.editor-field.Country select').val(),
                Zip: $('div.editor-field.Zip input').val()
            };

            $.getJSON(url, { data: JSON.stringify(data) }, function (result) {
                $('div.editor-field.Location input').val(result.location);
            });
        });

        $(document).on('change', 'div.editor-field.Activitat select', function (e) {
            var activitat = $('div.editor-field.Activitat select').val();
            if (actividad == activityOnline) {
                $('.ShowIfPresencial').hide();
            } else {
                $('.ShowIfPresencial').show();
            }
            if (actividad == activityPuericultura) {
                $('.ShowIfPuericultura').show();
            } else {
                $('.ShowIfPuericultura').hide();
            }
        });


        $(document).on('click', '.editor-submit button', function (e) {
            $('.Loading').show();
            var url = '@Url.Action("/Create")';
            var data = {
                Nom: $('div.editor-field.Nom input').val(),
                RaoSocial: $('div.editor-field.RaoSocial input').val(),
                NomComercial: $('div.editor-field.NomComercial input').val(),
                NIF: $('div.editor-field.NIF input').val(),
                Adr: $('div.editor-field.Adr input').val(),
                Country: $('div.editor-field.Country select').val(),
                Zip: $('div.editor-field.Zip input').val(),
                Cit: $('div.editor-field.Location input').val(),
                Tel: $('div.editor-field.Tel input').val(),
                Email: $('div.editor-field.Email input').val(),
                Web: $('div.editor-field.Web input').val(),
                ContactClass: $('div.editor-field.Activitat select').val(),
                CodSuperficie: radioListValue('div.editor-field.CodSuperficie'),
                CodVolumen: radioListValue('div.editor-field.CodVolumen'),
                SharePuericultura: $('div.editor-field.SharePuericultura input').val(),
                OtherShares: $('div.editor-field.OtherShares input').val(),
                CodSalePoint: radioListValue('div.editor-field.CodSalePoint'),
                Associacions: $('div.editor-field.Associacions input').val(),
                CodAntiguedad: radioListValue('div.editor-field.CodAntiguedad'),
                FchApertura: $('div.editor-field.FchApertura input').val(),
                CodExperiencia: radioListValue('div.editor-field.CodExperiencia'),
                Brands: checkboxListValues('div.editor-field.Brands'),
                OtherBrands: $('div.editor-field.OtherBrands input').val(),
                Obs: $('div.editor-field.Obs textarea').val()
            };
            $.getJSON(url, { data: JSON.stringify(data) }, function (result) {
                $('.Loading').hide();
                if (result.result == 'OK') {
                    $('div.formulari').hide();
                    $('div.Thanks').show();
                    $.getJSON('@Url.Action("/EmailConfirmation")', { guid: result.guid });
                } else {
                    $('#errors').show();
                    $('#errorsEnum').html(result.message);
                };
            });
        });


        function radioListValue(prefix) {
            var retval = 0;
            var selector = prefix + ' ' + 'input:checked';
            var count = $(selector).length;
            if (count!= 0)
                                                                                                                                                                                            retval = $(selector)[0].value;
            return retval;
        }

        function checkboxListValues(selector) {
            var retval = [];
            $(selector + ' input:checked').each(function () {
                retval.push($(this).val());
            });
            return retval;
        }

    </script>
End Section

@Section styles
    <style>
        .narrowForm {
            width: 320px;
            margin: auto;
        }

        .PageTitle {
            margin-bottom:30px;
        }

        .editor-label {
            margin: 15px 7px 2px 4px;
            color: darkgray;
        }

        .editor-field {
            margin: 4px 7px 2px 30px;
        }

        .editor-field input[type=text], .editor-field Select, .editor-field textarea {
            width: 100%;
        }

        .editor-submit {
            Text-align: Right;
            margin: 20px 7px 2px 30px;
        }

        .radioLabel {
            white-Space: nowrap;
        }

        .wrappable {
            white-space: normal;
        }

        #errors {
            color:red;
        }
    </style>
End Section