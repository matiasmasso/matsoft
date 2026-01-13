@ModelType DTOWtbolSite
@Code
    Layout = "~/Views/shared/_Layout_Pro.vbhtml"
    Dim lang As DTOLang = Mvc.ContextHelper.Lang
End Code

        <div class="Properties">
            <div>
                @lang.Tradueix("Nombre", "Nom", "Name")
            </div>
            <div class="EditButton"></div>
            <div>
                <span>@Model.Nom</span>
                <input type="text" data-prop="Nom" class="EditControl" value="@Model.Nom" />
            </div>

            <div>
                @lang.Tradueix("Activado", "Activat", "Active")
            </div>
            <div class="EditButton"></div>
            <div>
                <span>@IIf(Model.Active, lang.Tradueix("Sí", "Sí", "Yes"), lang.Tradueix("No", "No", "Not"))</span>
                <select class="EditControl" data-prop="Active">
                    <option value="true" @IIf(Model.Active, "selected", "")>@lang.Tradueix("Sí", "Sí", "Yes")</option>
                    <option value="false" @IIf(Model.Active, "", "selected")>@lang.Tradueix("No", "No", "Not")</option>
                </select>
            </div>

            <div>
                @lang.Tradueix("Web")
            </div>
            <div class="EditButton"></div>
            <div>
                <span>@Model.Web</span>
                <input type="text" data-prop="Web" class="EditControl" value="@Model.Web" />
            </div>

            <div>
                @lang.Tradueix("Hatch merchant Id")
            </div>
            <div class="EditButton"></div>
            <div>
                <span>@Model.MerchantId</span>
                <input type="text" data-prop="MerchantId" class="EditControl" value="@Model.MerchantId" />
            </div>

            <div>
                @lang.Tradueix("Contacto técnico")
            </div>
            <div class="EditButton"></div>
            <div>
                <span>@Model.ContactNom</span>
                <input type="text" data-prop="ContactNom" class="EditControl" value="@Model.ContactNom" />
            </div>

            <div>
                @lang.Tradueix("Email")
            </div>
            <div class="EditButton"></div>
            <div>
                <span>@Model.ContactEmail</span>
                <input type="text" data-prop="ContactEmail" class="EditControl" value="@Model.ContactEmail" />
            </div>

            <div>
                @lang.Tradueix("Teléfono")
            </div>
            <div class="EditButton"></div>
            <div>
                <span>@Model.ContactTel</span>
                <input type="text" data-prop="ContactTel" class="EditControl" value="@Model.ContactTel" />
            </div>


            <div>
                @lang.Tradueix("Landing pages")
            </div>
            <div>&nbsp;</div>
            <div>
                <span>@Model.LandingPagesCount</span>
                @if (Model.LastLandingPagesUpload() IsNot Nothing) Then
                    @<span>(@Model.LastLandingPagesUpload().ToString() )</span>
                End If
            </div>

            <div>
                @lang.Tradueix("Stocks")
            </div>
            <div>&nbsp;</div>
            <div>
                @if (Model.FchLastStocks <> Nothing) Then
                    @<span>@Model.FchLastStocks.ToString("dd/MM/yy HH: mm") </span>
                End If
            </div>



            <div Class="SubmitRow">
                <input type="button" Class="Submit" value='@lang.Tradueix("Aceptar", "Acceptar", "Submit", "Aceitar")' hidden />
            </div>


        </div>

@Section Scripts
    <script>
            var model = @Html.Raw(Model.SerializedWithNoLandingPages());

            $(document).on('click', '.EditButton', function () {
                $(this).next().children().toggle();
            });

            $(document).on('click', '.SubmitRow .Submit', function () {
                Update();
            });

            $(document).on('change', '.EditControl', function () {
                $('.SubmitRow .Submit').show();
            });

            function Update() {
                $('.SubmitRow .Submit').toggle();
                $('.SubmitRow').append(spinner);

                model.Nom = $('.Properties').find('[data-prop=Nom]').val() || '';
                model.Active = $('.Properties').find('[data-prop=Active]').val();
                model.Web = $('.Properties').find('[data-prop=Web]').val() || '';
                model.MerchantId = $('.Properties').find('[data-prop=MerchantId]').val() || '';
                model.ContactNom = $('.Properties').find('[data-prop=ContactNom]').val() || '';
                model.ContactEmail = $('.Properties').find('[data-prop=ContactEmail]').val() || '';
                model.ContactTel = $('.Properties').find('[data-prop=ContactTel]').val() || '';


                var url = "/pro/proWtbolSite/updateSite";

                $.ajax({
                    url: url,
                    dataType: 'json',
                    type: 'post',
                    contentType: 'application/json',
                    data: JSON.stringify(model),
                    processData: false,
                    success: function (data, textStatus, jQxhr) {
                        spinner.remove();
                        location.href = '/pro/proWtbolSites';
                    },
                    error: function (jqXhr, textStatus, errorThrown) {
                        spinner.remove();
                        $('.SubmitRow .Submit').toggle();
                        alert(errorThrown);
                        alert(jqXhr.responseText);
                    }
                });

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
            display: flex;
            flex-direction: row;
            justify-content: end;
        }
    </style>
End Section
