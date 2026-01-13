@ModelType List(Of DTOProductSku)
@Code
    Layout = "~/Views/shared/_Layout_Pro.vbhtml"
    Dim lang = ContextHelper.Lang
End Code

<div class="Catalog">

    <form method="post" action="/pro/proCatalog/Search" class="SkuSearch">
        <input type="text" name="SearchKey" value="@ViewBag.SearchKey" />
    </form>


    <div class="Grid Skus" data-contextmenu="Skus">
        <div class="HeaderRow">
            <div>@lang.Tradueix("Marca", "Marca", "Brand name")</div>
            <div>@lang.Tradueix("Categoría", "Categoria", "Category")</div>
            <div>@lang.Tradueix("Producto", "Producte", "Product")</div>
            <div>@lang.Tradueix("M+O")</div>
            <div>@lang.Tradueix("Ref.fabr.", "Ref.fabr.", "Manuf.code")</div>
            <div>@lang.Tradueix("EAN")</div>
            <div class="Stk">@lang.Tradueix("Stock")</div>
            <div class="Pvp">@lang.Tradueix("PVP", "PVP", "RRPP")</div>
        </div>

        @For Each oSku In Model.Where(Function(x) x.obsoleto = False).ToList()
            @<div Class="Row" data-guid="@oSku.Guid.ToString()">
                <div class="Brand Truncate">@DTOProductSku.brandNom(oSku)</div>
                <div class="Ctg Truncate">@DTOProductSku.CategoryNom(oSku)</div>
                <div class="Sku Truncate">@oSku.Nom.Tradueix(lang)</div>
                <div class="Id">@oSku.id</div>
                <div class="Ref">@oSku.refProveidor</div>
                <div class="Ean">@DTOProductSku.Ean(oSku)</div>
                <div Class="Stk">@oSku.stockAvailable()</div>
                <div Class="Pvp">@Html.Raw(If((oSku.rrpp Is Nothing), "", oSku.rrpp.Formatted()))</div>
            </div>
        Next

        @If Model.Any(Function(x) x.obsoleto) Then
            @<div Class="HeaderRow Obsoletos" >
                <div>@lang.Tradueix("Productos obsoletos:", "Productes obsolets:", "Outdated products:")</div>
            </div>

            @For Each oSku In Model.Where(Function(x) x.obsoleto).ToList()
                @<div Class="Row" data-guid="@oSku.Guid.ToString()">
                    <div class="Brand Truncate">@DTOProductSku.brandNom(oSku)</div>
                    <div class="Ctg Truncate">@DTOProductSku.CategoryNom(oSku)</div>
                    <div class="Sku Truncate">@oSku.Nom.Tradueix(lang)</div>
                    <div class="Id">@oSku.id</div>
                    <div class="Ref">@oSku.refProveidor</div>
                    <div class="Ean">@DTOProductSku.Ean(oSku)</div>
                    <div Class="Stk">@oSku.stockAvailable()</div>
                    <div Class="Pvp">@Html.Raw(If((oSku.rrpp Is Nothing), "", oSku.rrpp.Formatted()))</div>
                </div>
            Next
        End If

    </div>

    <div Class="ContextMenu" data-grid="Skus">
        <a href="#" data-url="/pro/proCatalog/Sku/{guid}">@lang.Tradueix("Ficha", "Fitxa", "Properties")</a>
        <a href="#" data-url="/pro/proCatalog/resourceUpload/{guid}">@lang.Tradueix("Subir recurso", "Pujar recurs", "Resource upload")</a>
    </div>

</div>



@Section Scripts
    <script src="~/Media/js/ContextMenu.js"></script>
    <script>

    </script>
End Section
@Section Styles
    <style>
        .Aside {
            width: 200px;
        }

        .SkuSearch {
            text-align: right;
            padding-bottom: 15px;
        }

        .Skus {
            display: grid;
            grid-template-columns: minmax(100px, 120px) minmax(100px, 120px) 1fr 60px 100px 100px 100px 100px;
        }

            .Skus .HeaderRow div {
                color: gray;
                font-weight: 600;
                margin-bottom: 10px;
            }

            .Skus .HeaderRow, .Skus .Row {
                display: contents;
            }

                .Skus .HeaderRow div, .Skus .Row div {
                    padding: 4px 7px 2px 4px;
                }

                .Skus .HeaderRow.Obsoletos {
                }

                    .Skus .HeaderRow.Obsoletos div {
                        grid-column: 1 / 9;
                        margin-top: 15px;
                    }


            .Skus .Brand, .Skus .Ctg, .Skus .Sku {
                white-space: nowrap;
                overflow-x: hidden;
            }

            .Skus .Stk, .Skus .Pvp {
                text-align: right;
            }


        @@media screen and (max-width: 600px) {
            .Skus {
                grid-template-areas: "brand ctg sku id ref ean"
            }
        }
    </style>
End Section

