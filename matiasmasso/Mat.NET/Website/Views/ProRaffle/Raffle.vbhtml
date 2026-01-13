@ModelType DTORaffle
@Code
    Layout = "~/Views/shared/_Layout_Pro.vbhtml"
    Dim lang As DTOLang = ContextHelper.Lang
End Code

<div class="Properties">

    <div class="Row" data-prop="Title">
        <div>
            @lang.Tradueix("Título", "Titol", "Title")
        </div>
        <div class="EditButton"></div>
        <div>
            <span>@Model.Title)</span>
            <input type="text" class="EditControl" value="@Model.Title" />
        </div>
    </div>

    <div class="Row" data-prop="Lang">
        <div>
            @lang.Tradueix("Idioma", "Idioma", "Language")
        </div>
        <div class="EditButton"></div>
        <div>
            <span>@Model.Lang.Nom(lang)</span>
            <select class="EditControl">
                <option value="">@lang.Tradueix("(Selecciona un idioma)", "(Tria un idioma)", "(Pick a language)")</option>
                @For Each oLang In DTOLang.Collection.All()
                    @if Model.Lang IsNot Nothing AndAlso Model.Lang.Equals(oLang) Then
                        @<option selected value="@oLang.Tag">@oLang.Nom(lang)</option>
                    Else
                        @<option value="@oLang.Tag">@oLang.Nom(lang)</option>
                    End If
                Next
            </select>
        </div>
    </div>

    <div class="Row" data-prop="Country">
        <div>
            @lang.Tradueix("Pais", "Pais", "Country")
        </div>
        <div class="EditButton"></div>
        <div>
            <span>@DTOCountry.FullNom(Model.Country, lang)</span>
            <select class="EditControl">
                <option value="">@lang.Tradueix("(Selecciona un pais)", "(Tria un pais)", "(Pick a country)")</option>
                @For Each oCountry As DTOCountry In ViewBag.Countries
                    If Model.Country IsNot Nothing AndAlso Model.Country.Equals(oCountry) Then
                        @<option selected value="@oCountry.Guid.ToString">@DTOCountry.FullNom(oCountry, lang)</option>
                    Else
                        @<option value="@oCountry.Guid.ToString">@DTOCountry.FullNom(oCountry, lang)</option>
                    End If
                Next
            </select>
        </div>
    </div>

    <div class="Row" data-prop="FchFrom">
        <div>
            @lang.Tradueix("Inicio", "Inici", "Start")
        </div>
        <div class="EditButton"></div>
        <div>
            <span>@Model.FchFrom.ToString("dd/MM/yy HH:mm")</span>
            <input type="date" data-prop="FchFrom" class="EditControl" value="@Model.FchFrom.ToString("yyyy-MM-dd")" />
            <input type="time" data-prop="TimeFrom" class="EditControl" value="@Model.FchFrom.ToString("HH:mm")" />
        </div>
    </div>

    <div class="Row" data-prop="FchTo">
        <div>
            @lang.Tradueix("Final", "Final", "End")
        </div>
        <div class="EditButton"></div>
        <div>
            <span>@Model.FchTo.ToString("dd/MM/yy HH:mm")</span>
            <input type="date" data-prop="FchTo" class="EditControl" value="@Model.FchTo.ToString("yyyy-MM-dd")" />
            <input type="time" data-prop="TimeTo" class="EditControl" value='@Model.FchTo.ToString("HH:mm")' />
        </div>
    </div>

    <div class="Row" data-prop="Visible">
        <div>
            @lang.Tradueix("Visible", "Visible", "Visible")
        </div>
        <div class="EditButton"></div>
        <div>
            <span>@IIf(Model.Visible, lang.Tradueix("Sí", "Sí", "Yes"), lang.Tradueix("No", "No", "Not"))</span>
            <select class="EditControl">
                <option value="true" @IIf(Model.Visible, "selected", "")>@lang.Tradueix("Sí", "Sí", "Yes")</option>
                <option value="false" @IIf(Model.Visible, "", "selected")>@lang.Tradueix("No", "No", "Not")</option>
            </select>
        </div>
    </div>

    <div class="Row" data-prop="Product">
        <div>
            @lang.Tradueix("Producto", "Producte", "Product")
        </div>
        <div class="EditButton"></div>
        <div>
            @If Model.Product Is Nothing Then
                @<span>&nbsp;</span>
            Else
                @<span>@Model.Product.FullNom</span>
            End If
            <div class="EditControl">
                <!--TODO: carregar cataleg al clicar-->
            </div>
        </div>
    </div>

    <div class="Row" data-prop="CostPrize">
        <div>
            @lang.Tradueix("Valor del premio", "Valor del premi", "Prize cost")
        </div>
        <div class="EditButton"></div>
        <div>
            <span>@DTOAmt.CurFormatted(Model.CostPrize)</span>
            <input type="number" class="EditControl" value="@DTOAmt.EurOrDefault(Model.CostPrize).ToString()" />
        </div>
    </div>

    <div class="Row" data-prop="CostPubli">
        <div>
            @lang.Tradueix("Inversión en publicidad", "Inversió en publicitat", "Publicity investment")
        </div>
        <div class="EditButton"></div>
        <div>
            <span>@DTOAmt.CurFormatted(Model.CostPubli)</span>
            <input type="number" class="EditControl" value="@DTOAmt.EurOrDefault(Model.CostPubli).ToString()" />
        </div>
    </div>

    <div class="Row" data-prop="Winner">
        <div>
            @lang.Tradueix("Ganador", "Guanyador", "Winner")
        </div>
        <div class="EditButton"></div>
        <div>
            @If Model.Winner Is Nothing Then
                @<span>&nbsp;</span>
            Else
                @<span>
                    @Model.Winner.User.FullNom() <br />
                    <a href='mailto:@Model.Winner.User.EmailAddress'>
                        @Model.Winner.User.EmailAddress
                    </a><br />
                    @Model.Winner.User.FullLocation(lang) <br />
                    @lang.Tradueix("Distribuidor:") <br />
                    <a href='/pro/proAtlas/contact/@Model.Winner.Distribuidor.Guid.ToString()' target="_blank">
                        @Model.Winner.Distribuidor.nomComercialOrDefault() <br />
                        @If Model.Winner.Distribuidor.address IsNot Nothing Then
                            @Html.Raw(Model.Winner.Distribuidor.address.ToHtml()) @<br />
                        End If
                    </a>
                </span>
            End If
        </div>
    </div>


    <div class="Row" data-prop="Thumbnail">
        <div>
            @lang.Tradueix("Thumbnail")
        </div>
        <div class="EditButton"></div>
        <div>
            <img src="@Model.ImageFbFeaturedUrl()" />
        </div>
    </div>

    <div class="Row" data-prop="Banner">
        <div>
            @lang.Tradueix("Banner")
        </div>
        <div class="EditButton"></div>
        <div>
            <img src="@Model.BannerUrl()" />
        </div>
    </div>


    <div class="SubmitRow">
        <input type="button" class="Submit" value='@lang.Tradueix("Aceptar", "Acceptar", "Submit", "Aceitar")' hidden />
    </div>


</div>

@Section Scripts
    <script>
        var model = @Html.Raw(Model.Serialized());

        $(document).on('click', '.EditButton', function () {
            $(this).next().children().toggle();
        });

        $(document).on('click', '.SubmitRow .Submit', function () {
            // Update();
        });

        $(document).on('change', '.EditControl', function () {
            $('.SubmitRow .Submit').show();
        });

        function Update() {
            $('.SubmitRow .Submit').toggle();
            $('.SubmitRow').append(spinner);

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

        .Properties .Row {
            display:contents;
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
            display: flex;
            flex-direction: row;
            justify-content: end;
        }
    </style>
End Section