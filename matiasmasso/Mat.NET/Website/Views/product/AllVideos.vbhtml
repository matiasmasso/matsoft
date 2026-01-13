@ModelType  DTOYouTubeMovie.ProductModel

@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, ContextHelper.Lang, ViewBag.Lang)
End Code


<h1 class="Title">@ViewBag.Title</h1>


        <div>
            @ContextHelper.Tradueix("¿De qué producto buscas videos?", "De quin producte vols els videos?", "What product are you looking for videos?", "De que produto procuras videos?")
        </div>

        <div class="ProductSelection">
            <select id="Brands">
                <option selected value=""
                        label='@ContextHelper.Tradueix("(Selecciona una marca comercial)", "(Selecciona una marca comercial)", "(Pick a brand name)", "(Seleciona uma marca)")'>
                </option>
                @For Each brand In Model.Catalog
                    @<option value="@brand.Guid.ToString()">@brand.Nom</option>
                Next
            </select>
            <select id="Categories">
                <option selected value=""
                        label='@ContextHelper.Tradueix("(Selecciona una categoría)", "(selecciona una categoría)", "(Pick a product category)", "(Seleciona uma categoria)")'>
                </option>
            </select>
            <select id="Skus">
                <option selected value=""
                        label='@ContextHelper.Tradueix("(Selecciona un producto)", "(Selecciona un producte)", "(Pick a product)", "(Seleciona um produto)")'>
                </option>
            </select>
        </div>


        <div Class="Grid"></div>




@Section Scripts

    <script>
        var model = @Html.Raw(System.Web.Helpers.Json.Encode(Model));

        function ShowVideos() {
            $('.Grid .Item').remove();
            SelectedVideos().forEach(AppendVideo);
        }

        function AppendVideo(video, index, arr) {
            var iFrame = $('<iframe />');
            iFrame.attr('id', video.Guid);
            iFrame.attr('src', '//www.youtube.com/embed/' + video.YouTubeId + '?controls=0&showinfo=0&rel=0');
            iFrame.attr('frameborder', '0');
            iFrame.attr('allowfullscreen', '');

            var label = $('<label />');
            label.attr('for', video.Guid);
            label.text(video.Nom);

            var container = $('<div />');
            container.attr('class', 'Item');
            container.append(iFrame);
            container.append(label);

            $('.Grid').append(container);
        }

        $(document).on('change', '#Brands', function () {
            emptyDropdown($('#Categories'));
            emptyDropdown($('#Skus'));
            if ($(this).val() != '')
                SelectedBrand().Categories.forEach(appendCategory);
            ShowVideos();
        });

        $(document).on('change', '#Categories', function () {
            emptyDropdown($('#Skus'));
            if ($(this).val() != '')
                SelectedCategory().Skus.forEach(appendSku);
            ShowVideos()
        });

        $(document).on('change', '#Skus', function () {
            ShowVideos()
        });

        function appendCategory(category, index, arr) {
            $('#Categories').append('<option value="' + category.Guid + '">' + category.Nom + '</option>');
        }

        function appendSku(sku, index, arr) {
            if (sku.Nom.Esp) {
                $('#Skus').append('<option value="' + sku.Guid + '">' + sku.Nom + '</option>');
            }
        }

        function SelectedVideos() {
            var retval = [];
            var productGuid  = SelectedProduct().Guid;
            var productVideos = model.ProductVideos.filter(function (item) { return item.Product === productGuid; })[0];
            if (productVideos) {
                var productVideoGuids = productVideos.Videos;
                retval = model.Videos.filter(function (item) { return (productVideoGuids.indexOf(item.Guid) > -1); });
            }
            return retval;
        }

        function SelectedProduct() {
            var retval;
            if (SelectedSku() > '')
                retval = SelectedSku();
            else if (SelectedCategory() > '')
                retval = SelectedCategory();
            else if (SelectedBrand() > '')
                retval = SelectedBrand();
            return retval;
        }

        function SelectedBrand() {
            var guid = $('#Brands').val();
            var retval = model.Catalog.filter(function (item) { return item.Guid === guid; })[0];
            return retval;
        }

        function SelectedCategory() {
            var retval;
            var guid = $('#Categories').val();
            if(SelectedBrand())
                retval = SelectedBrand().Categories.filter(function (item) { return item.Guid === guid; })[0];
            return retval;
        }

        function SelectedSku() {
            var retval;
            var guid = $('#Skus').val();
            if (SelectedCategory())
                retval = SelectedCategory().Skus.filter(function (item) { return item.Guid === guid; })[0];
            return retval;
        }

        function emptyDropdown(select) {
            select.find('option').not(':first').remove();
        }
    </script>
End Section

@Section Styles
    <style>

        .ContentColumn {
            width: 100%;
        }

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
                width: 100%;
            }

        @@media(max-width:700px) {

            .ProductSelection {
                flex-direction: column;
            }

                .ProductSelection select:not(:first-child) {
                    margin-top: 10px;
                }
        }

        /*----------------------------------------------------------*/

        .Grid {
            display: grid;
            grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
            max-width: 100%;
            margin-top: 20px;
            grid-gap: 10px;
        }

            .Grid .Item {
                width: 300px;
                overflow: hidden;
                justify-self: center;
                box-sizing: border-box;
                border: 1px solid gray;
                background-image: url('../../Media/Img/preloaders/Spinner64px.gif');
                background-repeat: no-repeat;
                background-position: center;
            }

                .Grid .Item label {
                    display: block;
                    padding: 10px;
                }
    </style>
End Section
