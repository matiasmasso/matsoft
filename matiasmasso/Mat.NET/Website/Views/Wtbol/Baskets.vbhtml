@ModelType DTOWtbolSite
@Code
    Layout = "~/Views/shared/_Layout_FullWidth.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, ContextHelper.Lang, ViewBag.Lang)
    Dim cache = FEB.GlobalVariables.Cache(New DTOEmp(DTOEmp.Ids.MatiasMasso))

End Code

<div class="PageWrapper">
    <div class="Title">Conversiones</div>

    <div class="Grid">
        <div Class="Fch">Fecha</div>
        <div class="SkuNom Truncate">Producto</div>
        <div class="Qty">Cantidad</div>
        <div class="Price">Precio</div>
        <div class="Amt">Importe</div>

        @For Each basket In Model.Baskets
            For Each item In basket.Items
                @<div Class="Fch">@basket.Fch.ToString("dd/MM/yy HH:mm")</div>
                @<div class="SkuNom Truncate"><a href="@cache.FindSku(item.Sku.Guid).UrlCanonicas().AbsoluteUrl(lang)" target="_blank">@item.Sku.RefYNomLlarg.Esp</a></div>
                @<div class="Qty">@item.Qty</div>
                @<div class="Price">@DTOAmt.Factory(item.Price).CurFormatted</div>
                @<div class="Amt">@DTOAmt.Factory(item.Amt).CurFormatted</div>
            Next
        Next
    </div>
</div>



@Section Styles
    <style scoped>
        .PageWrapper {
            display: flex;
            flex-direction: column;
            justify-content: flex-start;
            max-width: 900px;
            margin: auto;
        }

        .Title {
            margin: 0 auto;
            color: gray;
            font-size: large;
            font-weight: 600;
        }

        .Grid {
            margin-top:15px;
            display: grid;
            grid-template-columns: 120px 1fr 80px 80px 80px;
        }

            .Fch {
            }

            .SkuNom {
            }

            .Qty {
                text-align: right;
            }

            .Price {
                text-align: right;
            }

            .Amt {
                text-align: right;
            }
    </style>
End Section
