@ModelType DTOProductSku
@Code
    Layout = "~/Views/shared/_Layout_Pro.vbhtml"
    Dim lang As DTOLang = ContextHelper.Lang
End Code

        <div class="Properties">
            <div>
                @lang.Tradueix("Nombre corto", "Nom curt", "Short name")
            </div>
            <div class="EditButton"></div>
            <div>
                <span>@String.Format("{0} / {1} / {2} / {3}", Model.nom.Esp, Model.nom.Cat, Model.nom.Eng, Model.nom.Por)</span>
                <div class="EditControl">
                    <div><input type="text" data-prop="NomCurtLangTextEsp" value="@Model.nom.Esp" /> (Esp)</div>
                    <div><input type="text" data-prop="NomCurtLangTextCat" value="@Model.nom.Cat" /> (Cat)</div>
                    <div><input type="text" data-prop="NomCurtLangTextEng" value="@Model.nom.Eng" /> (Eng)</div>
                    <div><input type="text" data-prop="NomCurtLangTextPor" value="@Model.nom.Por" /> (Por)</div>
                </div>

            </div>
            <div>
                @lang.Tradueix("Nombre largo", "Nom llarg", "Long name")
            </div>
            <div class="EditButton"></div>
            <div>
                <span>@Model.RefYNomLlarg.Tradueix(lang)</span>
                <input type="text" data-prop="NomLlarg" class="EditControl" value="@Model.RefYNomLlarg.Tradueix(lang)" />
            </div>
            <div>
                @lang.Tradueix("Ref. proveedor", "Ref. proveidor", "Ref. supplier")
            </div>
            <div class="EditButton"></div>
            <div>
                <span>@Model.refProveidor</span>
                <input type="text" data-prop="RefProveidor" class="EditControl" value="@Model.refProveidor" />
            </div>
            <div>
                @lang.Tradueix("Nombre proveedor", "Nom proveidor", "Supplier name")
            </div>
            <div class="EditButton"></div>
            <div>
                <span>@Model.nomProveidor</span>
                <input type="text" data-prop="NomProveidor" class="EditControl" value="@Model.nomProveidor" />
            </div>
            <div>
                @lang.Tradueix("Cod.barras EAN", "Codi barres EAN", "EAN bar code")
            </div>
            <div class="EditButton_Disabled"></div>
            <div>
                <span>@DTOEan.eanValue(Model.ean13)</span>
                <input type="text" data-prop="Ean13" class="EditControl" value="@DTOEan.eanValue(Model.ean13)" />
            </div>
            <div>
                @lang.Tradueix("CNAP")
            </div>
            <div class="EditButton_Disabled"></div>
            <div>
                <span>@DTOCnap.FullNom(Model.cnap, lang)</span>
            </div>
            <div>
                @lang.Tradueix("Cod.Arancelario (TARIC)", "Cod.Arancelari (TARIC)", "Customs Tax code (TARIC)")
            </div>
            <div class="EditButton_Disabled"></div>
            <div><span>@DTOCodiMercancia.FullNom(Model.codiMercancia)</span></div>
            <div>
                @lang.Tradueix("Made in")
            </div>
            <div class="EditButton"></div>
            <div>
                <span>@DTOCountry.FullNom(Model.madeIn, lang)</span>
                <select class="EditControl" data-prop="MadeIn">
                    @if Model.madeIn IsNot Nothing Then
                        @<option value="">@lang.Tradueix("(Selecciona un pais)", "(Tria un pais)", "(Pick a country)")</option>
                        @<option selected value="@Model.madeIn.Guid.ToString">@DTOCountry.FullNom(Model.madeIn, lang)</option>
                    Else
                        @<option selected value="">@lang.Tradueix("(Selecciona un pais)", "(Tria un pais)", "(Pick a country)")</option>
                    End If
                </select>
            </div>
            <div>
                @lang.Tradueix("Producto obsoleto", "Producte obsolet", "outdated product")
            </div>
            <div class="EditButton"></div>
            <div>
                <span>@IIf(Model.obsoleto, lang.Tradueix("Sí", "Sí", "Yes"), lang.Tradueix("No", "No", "Not"))</span>
                <select class="EditControl" data-prop="Obsoleto">
                    <option value="true" @IIf(Model.obsoleto, "selected", "")>@lang.Tradueix("Sí", "Sí", "Yes")</option>
                    <option value="false" @IIf(Model.obsoleto, "", "selected")>@lang.Tradueix("No", "No", "Not")</option>
                </select>
            </div>
            <div class="SubmitRow">
                <input type="button" class="Submit" value='@lang.Tradueix("Aceptar", "Acceptar", "Submit", "Aceitar")' />
            </div>
        </div>

@Section Scripts
    <script>
        var model = @Html.Raw(Model.Serialized());

        $(document).on('click', '.EditButton', function () {
            $(this).next().children().toggle();
        });

        $(document).on('click', '.SubmitRow .Submit', function () {
            Update();
        });

        function Update() {
            $('.SubmitRow .Submit').toggle();
            $('.SubmitRow').append(spinner);
            
            model.Nom.Esp = $('.Properties').find('[data-prop=NomCurtLangTextEsp]').val();
            model.Nom.Cat = $('.Properties').find('[data-prop=NomCurtLangTextCat]').val();
            model.Nom.Eng = $('.Properties').find('[data-prop=NomCurtLangTextEng]').val();
            model.Nom.Por = $('.Properties').find('[data-prop=NomCurtLangTextPor]').val();
            model.NomLlarg = $('.Properties').find('[data-prop=NomLlarg]').val();
            model.refProveidor = $('.Properties').find('[data-prop=RefProveidor]').val() || '';
            model.nomProveidor = $('.Properties').find('[data-prop=NomProveidor]').val() || '';
            model.madeIn = $('.Properties').find('[data-prop=MadeIn]').val();
            model.obsoleto = $('.Properties').find('[data-prop=Obsoleto]').val();


            var url = "/pro/proProductSku/update";
            $.post(url, model, function (result) {
            })
                .done(function (result) {
                    spinner.remove();
                    if (result.success) {

                    } else {
                        $('.SubmitRow .Submit').toggle();
                        alert('failed: ' + result.message);
                    }
                })
                .fail(function (xhr, textStatus, errorThrown) {
                    spinner.remove();
                    $('.SubmitRow .Submit').toggle();
                    alert('failed: ' + xhr.responseText);
                })
        }


    </script>
End Section

@Section Styles
    <style>
        .Properties {
            display: grid;
            grid-template-columns: auto 16px 1fr;
            grid-gap: 10px;
        }

        .EditButton {
            width: 16px;
            height: 16px;
            background-image: url('/Media/Img/Ico/edit.png');
            cursor: pointer;
        }
        .EditButton_Disabled {
            width: 16px;
            height: 16px;
            background-image: url('/Media/Img/Ico/edit_Disabled.png');
            cursor: pointer;
        }
        .EditButton:Hover {
            background-image: url('/Media/Img/Ico/edit_Active.png');
        }

        .EditControl {
            display: none;
        }

        .SubmitRow {
            grid-column: 1 / 4; /* span from grid column line 1 to 3 (i.e., span 2 columns) */
            display:flex;
            flex-direction: row;
            justify-content: end;
        }
    </style>
End Section