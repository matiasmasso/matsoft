@ModelType DTOCustomer
@Code
    Layout = "~/Views/shared/_Layout_Pro.vbhtml"
    Dim lang As DTOLang = Mvc.ContextHelper.Lang
End Code

<div class="Properties">
    <div>
        @lang.Tradueix("Razon social", "Rao social", "Corporate name")
    </div>
    <div class="EditButton"></div>
    <div>
        <span>@Model.nom</span>
        <input type="text" data-prop="Nom" class="EditControl" value="@Model.nom" />
    </div>

    <div>
        @lang.Tradueix("Nombre comercial", "Nom comercial", "Commercial name")
    </div>
    <div class="EditButton"></div>
    <div>
        <span>@Model.nomComercial</span>
        <input type="text" data-prop="nomComercial" class="EditControl" value="@Model.nomComercial" />
    </div>

    <div>
        @Model.PrimaryNifCodNom(lang)
    </div>
    <div class="EditButton"></div>
    <div>
        <span>@Model.PrimaryNifValue</span>
        <input type="text" data-prop="Nif" class="EditControl" value="@Model.Nifs.PrimaryNifValue()" />
    </div>

    <div>
        @lang.Tradueix("Idioma", "Idioma", "Language")
    </div>
    <div class="EditButton"></div>
    <div>
        <span>@Model.lang.Nom(lang)</span>
        <select class="EditControl" data-prop="Lang">
            <option value="">@lang.Tradueix("(Selecciona un idioma)", "(Tria un idioma)", "(Pick a language)")</option>
            @For Each oLang In DTOLang.Collection.All()
                @if Model.lang IsNot Nothing AndAlso Model.lang.Equals(oLang) Then
                    @<option selected value="@oLang.Tag">@oLang.Nom(lang)</option>
                Else
                    @<option value="@oLang.Tag">@oLang.Nom(lang)</option>
                End If
            Next
        </select>
    </div>

    <div>
        @lang.Tradueix("Dirección", "Adréça", "Address")
    </div>
    <div class="EditButton"></div>
    <div>
        <span>@Html.Raw(DTOAddress.FullHtml(Model.address))</span>
        <div data-prop="Address" class="EditControl">
        </div>
    </div>

    <div>
        @lang.Tradueix("Web")
    </div>
    <div class="EditButton"></div>
    <div>
        <span>@Model.Website</span>
        <input type="text" data-prop="website" class="EditControl" value="@Model.Website" />
    </div>

    <div>
        @lang.Tradueix("Sorteos", "Sortejos", "Raffles")
    </div>
    <div class="EditButton"></div>
    <div>
        <span>@IIf(Model.NoRaffles, lang.Tradueix("No", "No", "Not"), lang.Tradueix("Sí", "Sí", "Yes"))</span>
        <select class="EditControl" data-prop="Active">
            <option value="true" @IIf(Model.NoRaffles, "selected", "")>@lang.Tradueix("No", "No", "Not")</option>
            <option value="false" @IIf(Model.NoRaffles, "", "selected")>@lang.Tradueix("Sí", "Sí", "Yes")</option>
        </select>
    </div>



    <div class="Contacts Grid" data-contextmenu="Tels">

        @For Each item As DTOContactTel In Model.Tels
            @<div class="Row Tel" hidden>
                <div class="Icon16 Tel"></div>
                <div><a href="tel:@item.Value">@item.Value</a></div>
                <div>@item.Obs</div>
            </div>
        Next

        @For Each item In Model.emails
            @<div class="Row Email" hidden data-guid="@item.Guid.ToString()">
                <div class="Icon16 Email"></div>
                <div>@item.emailAddress</div>
                <div>@item.NicknameOrElse()</div>
            </div>
        Next


    </div>


    <div class="SubmitRow">
        <input type="button" class="Submit" value='@lang.Tradueix("Aceptar", "Acceptar", "Submit", "Aceitar")' hidden />
    </div>

    <div class="ContextMenu" data-grid="Tels">
        <a href="#" data-url="/guid/{guid}/@Mvc.ContextHelper.GetUser().Guid.ToString" target="_blank">@lang.Tradueix("Extranet")</a>
    </div>

</div>

@Section Scripts
    <script src="~/Media/js/ContextMenu.js"></script>
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

            model.nom = $('.Properties').find('[data-prop=Nom]').val();
            model.nomComercial = $('.Properties').find('[data-prop=nomComercial]').val() || '';
            model.nif = $('.Properties').find('[data-prop=Nif]').val() || '';
            model.lang = $('.Properties').find('[data-prop=Lang]').val();
            model.website = $('.Properties').find('[data-prop=Website]').val();


            var url = "/pro/proContact/update";
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
        .MainContent {
            max-width: 900px;
        }
        .Properties {
            display: grid;
            grid-template-columns: minmax(100px, 200px) 16px 1fr;
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

        .Contacts.Grid {
            grid-column: 1 / 4;
            display: grid;
            grid-template-columns: 20px auto 1fr;
        }

            .Contacts.Grid .Row div:not(:first-child) {
                padding: 0 0 0 10px;
            }
            .Icon16.Email {
                background-image: url('/Media/Img/Ico/email16.png');
            }

        .Icon16.Tel {
            background-image: url('/Media/Img/Ico/tel16.png');
        }

        .SubmitRow {
            grid-column: 1 / 4; /* span from grid column line 1 to 3 (i.e., span 2 columns) */
            display: flex;
            flex-direction: row;
            justify-content: end;
        }


        .Drilldown::after {
            content: "\2304";
            font-size: 1.5em;
            color: black;
            display: inline-block;
            vertical-align: 15%;
            margin-left:5px;
        }

    </style>
End Section