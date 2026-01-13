@ModelType DTOWtbolSiteModel
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)
    Dim oCultureInfo = Mvc.ContextHelper.GetCultureInfo()
End Code


    <div class="Grid5Cols">
        @For Each item In Model.Site.LandingPages
            @<div class="Row">
                <span class="CellBrand">@DTOProduct.brandNom(item.Product)</span>
                <span class="CellCategory">@DTOProduct.CategoryNom(item.Product)</span>
                <span class="CellSku">@DTOProduct.SkuNom(item.Product)</span>
                <span class="CellUrl">@item.Segment()</span>
                <span class="CellFch">@item.FchCreated.ToString("G", oCultureInfo)</span>
            </div>
        Next
    </div>



<div id="DetailsForm" hidden="hidden">
    <div id="DetailsFormHeader">
        <h3>Landing page</h3>
        <img src="~/Media/Img/Ico/Cross24.png" />
    </div>
    <div id="DetailsFormWrapper">
        <div>
            <div>@Mvc.ContextHelper.Tradueix("Marca comercial", "Marca comercial", "Brand")</div>
            <select id="BrandSelector"></select>
        </div>
        <div>
            <div>@Mvc.ContextHelper.Tradueix("Categoria", "Categorya", "Product category")</div>
            <select id="CategorySelector"></select>
        </div>
        <div>
            <div>@Mvc.ContextHelper.Tradueix("Producto", "Producte", "Product")</div>
            <select id="SkuSelector"></select>
        </div>
        <div>
            <div>Url</div>
            <input type="text" id="LandingPageUrl"/>
        </div>
        <div class="SubmitRow">
            <input type="submit" class="Submit" />
        </div>
    </div>
</div>


@Section Styles
    <style scoped>

        .Grid5Cols {
            display: grid;
            grid-template-columns: auto auto auto auto auto;
            font-size: 0.7em;
        }

            .Grid5Cols .Row {
                display: contents;
            }

            .Grid5Cols Span {
                padding: 5px;
            }

                .Grid5Cols .Row:hover span {
                    background-color:   #167ac6;
                    color: white;
                    cursor: pointer;
                }


        #DetailsForm {
            position: absolute;
            top: 20%;
            left: 50%;
            border: 1px solid;
            box-shadow: 5px 10px #888888;
            padding: 0;
            min-width: 600px;
        }

        #DetailsFormHeader {
            display:flex;
            justify-content: space-between;
            color: white;
            padding: 4px;
            border-bottom: 1px solid gray;
            background-color: #167ac6;
            cursor: move;
        }

        #DetailsFormHeader img {
            cursor: pointer;
        }



        #DetailsFormWrapper {
            padding: 20px;
            background-color: antiquewhite;
        }
            #DetailsFormWrapper select, #DetailsFormWrapper input[type=text] {
                font-size: 1em;
                width: 90%;
                margin: 5px;
            }

        .SubmitRow {
            text-align: right;
        }
    </style>
End Section


@Section Scripts
    <script>
        $(document).ready(function () {
            /*$('#DetailsForm').hide();*/
        })

        $(document).on('click', '.Row span', function () {
            $(this).toggleClass('Active');
            if ($(this).hasClass('Active')) {
                var url = $(this).siblings('.CellUrl')[0].innerText;
                $('#LandingPageUrl').val(url);
            }
            $('#DetailsForm').toggle('fast');
        })

        $(document).on('click', '#DetailsFormHeader img', function () {
            $('#DetailsForm').hide();
        })
    </script>
End Section
