@ModelType DTOWtbolLandingPage.LandingPageModel
@Code
    Layout = "~/Views/shared/_Layout_Pro.vbhtml"
    Dim lang As DTOLang = ContextHelper.Lang
End Code

        <div class="Properties">
            <div>
                @lang.Tradueix("Marca Comercial", "Marca Comercial", "Commercial Brand")
            </div>
            <div class="EditButton ProductEditButton"></div>
            <div>
                <span>@DTOProduct.brandNom(Model.Product)</span>
                <div data-prop="Brand" class="EditControl" data-guid="@DTOProduct.brandGuid(Model.Product).ToString()">
                    <select>
                        @If DTOProduct.brandGuid(Model.Product) = Nothing Then
                            @<option selected value="">@lang.Tradueix("(Selecciona una marca comercial)", "(Tria una marca comercial)", "(Pick a commercial brand)")</option>
                        Else
                            @<option value="">@lang.Tradueix("(Selecciona una marca comercial)", "(Tria una marca comercial)", "(Pick a commercial brand)")</option>
                            @<option selected value="@DTOProduct.brandGuid(Model.Product).ToString()">@DTOProduct.brandNom(Model.Product)</option>
                        End If
                    </select>
                </div>
            </div>

            <div>
                @lang.Tradueix("Categoría", "Categoria", "Category")
            </div>
            <div class="EditButton ProductEditButton"></div>
            <div>
                <span>@DTOProduct.CategoryNom(Model.Product)</span>
                <div data-prop="Category" class="EditControl" data-guid="@DTOProduct.CategoryGuid(Model.Product).ToString()">
                    <select>
                        @If DTOProduct.CategoryGuid(Model.Product) = Nothing Then
                            @<option selected value="">@lang.Tradueix("(Selecciona una categoría)", "(Tria una categoria)", "(Pick a category)")</option>
                        Else
                            @<option value="">@lang.Tradueix("(Selecciona una categoría)", "(Tria una categoria)", "(Pick a category)")</option>
                            @<option selected value="@DTOProduct.CategoryGuid(Model.Product).ToString()">@DTOProduct.CategoryNom(Model.Product)</option>
                        End If
                    </select>
                </div>
            </div>

            <div>
                @lang.Tradueix("Producto", "Producte", "Product")
            </div>
            <div class="EditButton ProductEditButton"></div>
            <div>
                <span>@DTOProduct.SkuNom(Model.Product)</span>
                <div data-prop="Sku" class="EditControl" data-guid="@DTOProduct.SkuGuid(Model.Product).ToString()">
                    <select>

                        @If DTOProduct.SkuGuid(Model.Product) = Nothing Then
                            @<option selected value="">@lang.Tradueix("(Selecciona un producto)", "(Tria un producte)", "(Pick a product)")</option>
                        Else
                            @<option value="">@lang.Tradueix("(Selecciona un producto)", "(Tria un producte)", "(Pick a product)")</option>
                            @<option selected value="@DTOProduct.SkuGuid(Model.Product).ToString()">@DTOProduct.SkuNom(Model.Product)</option>
                        End If
                    </select>
                </div>
            </div>

            <div>
                @lang.Tradueix("Fecha", "Data", "Date")
            </div>
            <div>&nbsp;</div>
            <div>
                <span>@DateTime.Parse(Model.FchCreated).ToString("dd/MM/yy HH:mm")</span>
            </div>

            <div>
                @lang.Tradueix("Landing Page")
            </div>
            <div class="EditButton"></div>
            <div>
                <span>@Model.Url</span>
                <input type="text" 
                       data-prop="Url" 
                       class="EditControl" 
                       placeholder="@Model.Url"
                       value="@Model.Url" />
            </div>

            <div Class="SubmitRow">
                <input type="button" Class="Submit" value='@lang.Tradueix("Aceptar", "Acceptar", "Submit")' hidden />
            </div>

        </div>

@Section Scripts
    <script>
        var model = @Html.Raw(System.Web.Helpers.Json.Encode(Model)) ;
        var controlWaitingForLoadCompletion;
        var catalog = [];
        var catalogIsLoaded = false;

        $(document).ready(function () {
            LoadCatalog();
        });

        $(document).on('catalogLoaded', function (e, argument) {
            catalogIsLoaded = true;
            if (controlWaitingForLoadCompletion) {
                spinner20.remove();
                controlWaitingForLoadCompletion.next().children().toggle();
            }
        });

        $(document).on('click', '.ProductEditButton', function () {
            if (catalogIsLoaded) {
                $(this).next().children().toggle();
            } else {
                controlWaitingForLoadCompletion = $(this);
                $(this).next().append(spinner20);
            }
        });

        $(document).on('click', '.EditButton:not(.ProductEditButton)', function () {
            $(this).next().children().toggle();
        });

        $(document).on('click', '.SubmitRow .Submit', function () {
            Update();
        });

        $(document).on('change', '.EditControl', function () {
            $('.SubmitRow .Submit').show();
        });

        $(document).on('change', '[data-prop="Brand"] select', function () {
            DropdownChanged($(this).parent());
            LoadCategories();
            ResetLabel($('[data-prop="Category"]'));
            ResetLabel($('[data-prop="Sku"]'));
            ClearDropdown($('[data-prop="Sku"] select'));
        });

        $(document).on('change', '[data-prop="Category"] select', function () {
            DropdownChanged($(this).parent());
            LoadSkus();
            ResetLabel($('[data-prop="Sku"]'));
        });

        function ResetLabel(control) {
            var span = control.siblings('span');
            span.text('');
            span.show();
            control.hide();
        }

        function DropdownChanged(control) {
            //on change, close control and reset the label
            var span = control.siblings('span');
            var guid = control.find("select").val();
            if (guid == '') {
                span.text('');
            } else {
                var nom = control.find("select option:selected").text();
                span.text(nom)
            }
            control.hide();
            span.show();
        }

        function ClearDropdown(dropdown) {
            dropdown.find('option').not(':first').remove();
        }

        function propName(editButton) {
            var label = editButton.next().children().first();
            var editingControl = label.next();
            var retval = editingControl.data('prop');
            return retval;
        }


        function LoadBrands() {
            var dropdown = $('[data-prop="Brand"] select');
            var guid = dropdown.val();
            ClearDropdown(dropdown);
            $.each(catalog, function (key, item) {
                if(item.Guid == guid) {
                    dropdown.append('<option selected value=' + item.Guid + '>' + item.Nom + '</div>');
                } else {
                    dropdown.append('<option value=' + item.Guid + '>' + item.Nom + '</div>');
                }
            });
        }

        function LoadCategories() {
            var dropdown = $('[data-prop="Category"] select');
            var guid = dropdown.val();
            ClearDropdown(dropdown);
            $.each(SelectedBrand().Categories, function (key, item) {
                if (item.Guid == guid) {
                    dropdown.append('<option selected value=' + item.Guid + '>' + item.Nom + '</div>');
                } else {
                    dropdown.append('<option value=' + item.Guid + '>' + item.Nom + '</div>');
                }
            });
        }

        function LoadSkus() {
            var dropdown = $('[data-prop="Sku"] select');
            var guid = dropdown.val();
            var nom = dropdown.children('option:selected').text();
            var found = false;
            ClearDropdown(dropdown);
            $.each(SelectedCategory().Skus, function (key, item) {
                if (item.Guid == guid) {
                    dropdown.append('<option selected value=' + guid + '>' + nom + '</div>');
                    found = true;
                } else {
                    dropdown.append('<option value=' + item.Guid + '>' + item.Nom + '</div>');
                }
            });
            if (guid > '' && !found) {
                dropdown.append('<option selected class="Outdated" value=' + guid + '>' + nom + '</div>');
            }

        }

        function SelectedBrand() {
            var guid = $('[data-prop="Brand"] select').val();
            var matchingItems = catalog.filter(function (item) { return item.Guid === guid; });
            var retval = matchingItems[0];
            return retval;
        }

        function SelectedCategory() {
            var retval;
            if (SelectedBrand()) {
                var guid = $('[data-prop="Category"] select').val(); //data('guid');
                var matchingItems = SelectedBrand().Categories.filter(function (item) { return item.Guid === guid; });
                retval = matchingItems[0];
            }
            return retval;
        }

        function SelectedSku() {
            var retval;
            if (SelectedCategory()) {
                var guid = $('[data-prop="Sku"] select').val(); //data('guid');
                var matchingItems = SelectedCategory().Skus.filter(function (item) { return item.Guid === guid; });
                retval = matchingItems[0];
            }
            return retval;
        }

        function SelectedProduct() {
            var guid;
            var src;
            if (SelectedSku()) {
                guid = SelectedSku().Guid;
                src = @CInt(DTOProduct.SourceCods.Sku);
            } else if (SelectedCategory()) {
                guid = SelectedCategory().Guid;
                src = @CInt(DTOProduct.SourceCods.category);
            } else if (SelectedBrand()) {
                guid = SelectedBrand().Guid;
                src = @CInt(DTOProduct.SourceCods.brand);
            }
            var retval = { "Guid": guid, "sourceCod": src };
            return retval;
        }

        function Update() {
            alert(model.FchCreated);
                $('.SubmitRow .Submit').toggle();
                $('.SubmitRow').append(spinner);
                model.Product = SelectedProduct();
                model.Url = $('.Properties').find('[data-prop=Url]').val();

                var url = "/pro/proWtbolSite/updateLandingPage";

                $.ajax({
                    url: url,
                    dataType: 'json',
                    type: 'post',
                    contentType: 'application/json',
                    data: JSON.stringify(model),
                    processData: false,
                    success: function (data, textStatus, jQxhr) {
                        spinner.remove();
                        alert('data: ' + data);
                        alert('textStatus: ' + textStatus);
                        alert('jQxhr: ' + jQxhr);
                        //location.href = '/pro/proWtbolSite/LandingPages/@@Model.SiteGuid';
                    },
                    error: function (jqXhr, textStatus, errorThrown) {
                        spinner.remove();
                        $('.SubmitRow .Submit').toggle();
                        alert(errorThrown);
                        alert(jqXhr.responseText);
                    }
                });

            }

        function LoadCatalog() {
            var url = '@MmoUrl.apiUrl("ProductCatalog/CustomerBasicTree", Model.CustomerGuid.ToString, lang.Tag)';
            $.getJSON(url, function(result) {
                if(result) {
                    catalog = result;
                    LoadBrands();
                    if (SelectedBrand())
                        LoadCategories();
                    if (SelectedCategory())
                            LoadSkus();
                    $(document).trigger('catalogLoaded', catalog);
                } else {
                    alert('failed');
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
            grid-gap:   10px;
        }

        .EditButton {
            width:   16px;
            height: 16px;
            background-image: url('/Media/Img/Ico/edit.png');
            cursor: pointer;
        }

        .EditButton_Disabled {
            width:   16px;
            height: 16px;
            background-image: url('/Media/Img/Ico/edit_Disabled.png');
            cursor: pointer;
        }

        .EditButton :hover {
            background-image: url('/Media/Img/Ico/edit_Active.png');
        }

        .EditControl {
            display: none;
        }


            .EditControl select {
                width: 100%;
                max-width:  400px;
                padding: 4px 7px 2px 4px;
            }

        input[type=text].EditControl {
            width: 100%;
            padding: 4px 7px 2px 4px;
        }

        select option.Outdated {
            background: rgba(255, 160, 122, 0.5);
        }


        .SubmitRow {
            grid-column:   1 / 4; /* span from grid column line 1 to 3 (i.e., span 2 columns) */
            display: flex;
            flex-direction: row;
            justify-content: end;
        }
    </style>
End Section
