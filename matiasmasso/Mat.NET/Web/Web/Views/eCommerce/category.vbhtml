@ModelType DTO.DTOProductCategory
@Code
    Layout = "~/Views/Shared/_Layout_eCommerce.vbhtml"
    ViewData("eComPage") = DTO.Defaults.eComPages.Category

    Dim oAccessories As List(Of DTO.DTOStock) = BLL.BLLStocks.Accessories(Mvc.GlobalVariables.Emp, Model)

    Dim oWebsession As DTO.DTOSession = BLL.BLLSession.Find(Session("SessionId"))
    Dim oUser As DTO.DTOUser = oWebSession.User
    Dim oBasket As DTO.DTOBasket = BLL.BLLBasket.FindLastorNew(DTO.DTOBasket.Sites.Thorley, oUser)

End Code


<div class="pagewrapper">

    @For Each oStock As DTO.DTOStock In BLL.BLL4moms.AvailableProducts(Mvc.GlobalVariables.Emp, Model)
        @<div class="ProductSku">
            <img src="@BLL.BLLProductSku.ThumbnailUrl(oStock.Sku, True)" />
            <div class="CategoryNom truncate">@Model.Nom</div>
            <div class="SkuNom truncate">@oStock.Sku.NomCurt</div>
            <div class="RRPP">@BLL.BLLAmt.FormatEur(oStock.Sku.RRPP)</div>
            <div class="Purchase">
                <a href="#" data-sku="@oStock.Sku.Guid.ToString">comprar</a>
            </div>
        </div>
    Next
    @If oAccessories.Count > 0 Then
        @<p class="Epigraf">Accesorios disponibles:</p>
        @<hr />

        @For Each oStock As DTO.DTOStock In oAccessories
            @<div class="ProductSku">
                <img src="@BLL.BLLProductSku.ThumbnailUrl(oStock.Sku, True)" />
                <div class="CategoryNom truncate">@Model.Nom</div>
                <div class="SkuNom truncate">@oStock.Sku.NomCurt</div>
                <div class="RRPP">@BLL.BLLAmt.FormatEur(oStock.Sku.RRPP)</div>
                 <div class="Purchase">
                     <a href="#" data-sku="@oStock.Sku.Guid.ToString">comprar</a>
                 </div>
            </div>
        Next
    End If

</div>

@Section Scripts
    <script>
    $(document).ready(function() {
        $(document).on('click', '.ProductSku .Purchase a', function (e) {
            event.preventDefault();
            var data = { sku: $(this).data('sku'), qty: 1 };
            $(document).trigger('addItem', data);
            /*
            var url = '@@url.action("addItem")';
            $('#BasketSummary').load(url, data);
            */
        });
    });
    </script>
End Section

@Section Styles

<style>
    .pagewrapper {
        display:block;
        margin:auto;
        text-align:center;
        font-size:0.8em;
    }
    .ProductSku {
        position:relative;
        display:inline-block;
        height:250px;
        width:150px;
        margin:10px;
        text-align:center;
        vertical-align:middle;
    }
    .ProductSku img {
        position:absolute;
        top:0;
        left:0;
        text-align:center;
    }
    .ProductSku .CategoryNom {
        position:absolute;
        top:170px;
        width:100%;
        text-align:center;
    }
    .ProductSku .SkuNom {
        position:absolute;
        top:185px;
        width:100%;
        text-align:center;
    }
    .ProductSku .RRPP {
        position:absolute;
        top:200px;
        width:100%;
        text-align:center;
    }
    .ProductSku .Purchase {
        position:absolute;
        width:100%;
        top:225px;
        left:0;
        text-align:center;

    }

        .ProductSku .Purchase a {
        text-decoration:none;
        color:black;
        background: #ccc;
        border: 1px solid #ccc;
        cursor: pointer;
        padding:5px 15px;
        -webkit-border-radius: 5px;
        border-radius: 5px;
        margin:auto;
    }

    .ProductSku .Purchase a:hover {
        background: #ddd;
        border: 1px solid black;
    }

    .Epigraf {
        text-align:left;
        font-size:1em;
        font-weight:700;
        color:darkgray;
    }
    hr {
        color: lightgray;
    }


</style>
End Section


