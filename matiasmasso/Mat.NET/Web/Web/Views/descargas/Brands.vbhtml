@ModelType  DTOProductDownload.ProductModel

@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)
End Code


        <h1 class="Title">@ViewBag.Title</h1>

        <div>
            @Select Case Model.Src
                Case DTOProductDownload.Srcs.compatibilidad
                    @Mvc.ContextHelper.Tradueix("¿De qué producto buscas las compatibilidades?", "De quin producte vols les compatibilitats?", "What product do you want the compatibility list for?", "De que produto procuras confirmar a compatibilidade de instalação com o teu veículo?")
                Case Else
                    @Mvc.ContextHelper.Tradueix("¿De qué producto buscas las instrucciones?", "De quin producte vols les instruccions?", "What product do you want the user manual for?", "De que produto procuras as instruções?")
            End Select
        </div>

        <div class="ProductSelection">
            <select id="Brands">
                <option selected value="">
                    @Mvc.ContextHelper.Tradueix("(Selecciona una marca comercial)", "(Selecciona una marca comercial)", "(Pick a brand name)", "(Seleciona uma marca)")
                </option>
                @For Each brand In Model.Catalog
                    @<option value="@brand.Guid.ToString()">@brand.Nom</option>
                Next
            </select>
            <select id="Categories">
                <option selected value="">
                    @Mvc.ContextHelper.Tradueix("(Selecciona una categoría)", "(selecciona una categoría)", "(Pick a product category)", "(Seleciona uma categoria)")
                </option>
                                    </select>
            <select id="Skus">
                <option selected value="">
                    @Mvc.ContextHelper.Tradueix("(Selecciona un producto)", "(Selecciona un producte)", "(Pick a product)", "(Seleciona um produto)")
                </option>
                                    </select>
        </div>

        <div Class="DownloadsGallery">
            @For Each item In Model.Files
                @<div class="Item" data-brand="@item.BrandGuid.ToString()"
                      data-category="@item.CategoryGuid.ToString()"
                      data-sku="@item.SkuGuid.ToString()">

                    <a href="@item.DownloadUrl()">
                        <div class="ImageContainer" style="background-image:url(@item.ThumbnailUrl());">
                        </div>
                        <div class="Features">
                            <div>
                                @item.Features
                                <br />
                                @(Mvc.ContextHelper.Tradueix("Descargas: ") & item.LogCount)
                            </div>
                            @If Not String.IsNullOrEmpty(item.Nom) Then
                                @<div>@item.Nom</div>
                            End If
                        </div>
                    </a>
                </div>
            Next
        </div>



@Section Styles
    <style>

                            .ProductSelection {
                                display: flex;
            justify-content: space-between;
            column-gap: 20px;
            margin-top: 15px;
        }
        .ProductSelection select {
            box-sizing: border-box;
            font-size: 1em;
            padding: 4px 7px 2px 4px;
            border: 1px solid gray;
            width:100%;
        }

        @@media(max-width:700px) {

            .ProductSelection {
                flex-direction:  column;
            }
                .ProductSelection select:not(:first-child) {
                    margin-top: 10px;
                }
        }

        /*----------------------------------------------------------*/

        .DownloadsGallery {
            display:  grid;
            grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
            max-width: 100%;
            margin-top: 20px;
            grid-gap: 10px;
        }

            .DownloadsGallery .Item {
                display:  none;
                width: 300px;
                overflow: hidden;
                justify-self: center;
                box-sizing: border-box;
                border: 1px solid gray;
            }
                .DownloadsGallery .Item a {
                    display: flex;
                    flex-direction: row;
                    align-items: start;
                }
                .DownloadsGallery .Item : hover {
                    background-color: #F0F0F0;
                }
                .DownloadsGallery .Item .ImageContainer {
                    height:  150px;
                    width: 130px;
                    background-repeat: no-repeat;
                    background-size: cover;
                }
                .DownloadsGallery .Item .Features {
                    padding:  10px ;
                    font-size: 0.8em;
                }
                .DownloadsGallery .Item .Features div: first-child {
                    margin-bottom: 15px;
                }
            .DownloadsGallery img {
                width: 100%;
                height: auto;
            }
    </style>
End Section

@Section Scripts
    <script>
                            var catalog = @Html.Raw(Model.Catalog.Serialized());
        var noGuid = '00000000-0000-0000-0000-000000000000';

        $(document).on('change', '#Brands', function () {
            emptyDropdown($('#Categories'));
            emptyDropdown($('#Skus'));
            $('.DownloadsGallery .Item').hide();
            if ($(this).val() != '') {
                var files = $('.DownloadsGallery .Item[data-brand=' + SelectedBrand().Guid + ']').length;
                $('.DownloadsGallery .Item[data-brand=' + SelectedBrand().Guid + '][data-category=' + noGuid + '][data-sku=' + noGuid + ']').show();
                var brand = SelectedBrand();

                if (brand.Categories.length == 0) {
                    $('#Categories').hide();
                    $('#Skus').hide();
                } else {
                    $('#Categories').show();
                    SelectedBrand().Categories.forEach(appendCategory);
                }
            }
        });

        $(document).on('change', '#Categories', function () {
            emptyDropdown($('#Skus'));
            $('.DownloadsGallery .Item').hide();
            var guid = $(this).val();
            if (guid != '') {
                $('.DownloadsGallery .Item[data-category=' + SelectedCategory().Guid + '][data-sku=' + noGuid + ']').show();
                var category = SelectedCategory();

                if (category.Skus.length == 0) {
                    $('#Skus').hide();
                } else {
                    $('#Skus').show();
                    category.Skus.forEach(appendSku);
                }
            }
        });

        $(document).on('change', '#Skus', function () {
            $('.DownloadsGallery .Item[data-sku=' + SelectedSku().Guid + ']').show();
        });

        function appendCategory(category, index, arr) {
            $('#Categories').append('<option value="' + category.Guid + '">' + category.Nom + '</option>');
        }

        function appendSku(sku, index, arr) {
            if (sku.NomCurt) {
                $('#Skus').append('<option value="' + sku.Guid + '">' + sku.NomCurt + '</option>');
            }
        }

        function SelectedBrand() {
            var guid = $('#Brands').val();
            var retval = catalog.filter(function (item) { return item.Guid === guid; })[0];
            return retval;
        }

        function SelectedCategory() {
            var guid = $('#Categories').val();
            var retval = SelectedBrand().Categories.filter(function (item) { return item.Guid === guid; })[0];
            return retval;
        }

        function SelectedSku() {
            var guid = $('#Skus').val();
            var category = SelectedCategory();
            var retval = category.Skus.filter(function (item) { return item.Guid === guid; })[0];
            return retval;
        }

        function emptyDropdown(select) {
            select.find('option').not(':first').remove();
        }
    </script>
    End Section