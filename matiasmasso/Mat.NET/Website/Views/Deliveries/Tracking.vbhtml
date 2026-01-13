@ModelType DTO.DTODelivery
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, ContextHelper.Lang, ViewBag.Lang)

End Code


<h1>@ViewBag.Title</h1>

<div class="Grid">
    <div>@lang.Tradueix("albarán", "albarà", "delivery note")</div>
    <div>@Model.Id</div>

    <div>@lang.Tradueix("destino", "destinació", "deliveruy address")</div>
    <div>@Model.Nom<br />@Html.Raw(Model.Address.ToHtml())</div>

    <div>&nbsp;</div><div>&nbsp;</div>
    <div>@lang.Tradueix("transportista", "transportista", "forwarder")</div>
    <div>@Model.Transportista.Nom</div>

    <div>@lang.Tradueix("Nº de seguimiento", "Nº de seguiment", "tracking number")</div>
    <div>@Model.Tracking</div>

    <div>@lang.Tradueix("Bultos", "Bultos", "Packages")</div>
    <div>@Model.Bultos</div>

    @If Model.Kg <> 0 Then
        @<div>@lang.Tradueix("Peso", "Pes", "Weight")</div>
        @<div>@Model.Kg Kg</div>
    End If

    <div class="Epigraf">&nbsp;</div>
    <div class="Epigraf">@lang.Tradueix("Seguimiento:", "Seguiment:", "Tracking:")</div>
    <div class="Epigraf">&nbsp;</div>

    @For Each item In Model.Trace.Items
        @<div>@item.Fch</div>
        @<div>@item.Caption</div>
    Next


</div>


@If Model.Trace.MoreInfoAvailable Then
    @<a href="@Model.GetTrackingUrl()" class="MoreInfo">
        @lang.Tradueix("Ver información actualizada en la web del transportista", "Veure informació actualitzada a la página del transportista", "see updated info in forwarder website")
    </a>
End If

@For Each package In Model.Packages
    @<a href="#" class="Bulto">
        <div class="Parcel ParcelClosed"></div>
        <div>
            @If package.Num > 0 Then
                @<div>@lang.Tradueix("Bulto num.", "Bulto num.", "Package #") @package.Num</div>
            Else
                @<div>@lang.Tradueix("Bulto", "Bulto", "Package")</div>
            End If
            <div>@lang.Tradueix("Matrícula", "Matrícula", "Plate") @package.SSCC</div>
            @If package.HasDimensions() Then
                @<div>@lang.Tradueix("Dimensiones", "Dimensions", "Measures") @package.Dimensions()</div>
            End If
            <div>@lang.Tradueix("(ver contenido)", "(veure contingut)", "(see package contents)") &#x26DB;</div>

        </div>

        <div class="Contents">
            @For Each item In package.Items
                @<div class="SkuThumbnail">
                    <img src="@item.DeliveryItem.PurchaseOrderItem.Sku.thumbnailUrl()" />
                </div>
                @<div class="Features">
                    <div>@item.QtyInPackage @lang.Tradueix("unidades", "unitats", "units")</div>
                    <div>@item.DeliveryItem.PurchaseOrderItem.Sku.Category.Brand.Nom.Tradueix(lang)</div>
                    <div>@item.DeliveryItem.PurchaseOrderItem.Sku.Category.Nom.Tradueix(lang)</div>
                    <div>@item.DeliveryItem.PurchaseOrderItem.Sku.RefYNomLlarg.Tradueix(lang)</div>
                </div>
            Next
        </div>
    </a>

Next

@Section Scripts
    <script>
        $(document).on('click', '.Bulto', function () {
            event.preventDefault();
            var parcel = $(this).find('.Parcel');
            $(parcel).toggleClass('ParcelClosed ParcelOpen');
            var contents = $(this).find('.Contents');
            if (contents.css('display') == 'none') {
                contents.css('display', 'grid')
            } else {
                contents.css('display', 'none')
            }
        })
    </script>
End Section

@Section Styles
    <link href="~/Media/Css/tables.css" rel="stylesheet" />
    <style scoped>

        .ContentColumn {
            max-width: 600px;
        }

        .Grid {
            display: grid;
            grid-template-columns: auto 1fr;
            column-gap: 20px;
            grid-column-gap: 20px;
            grid-row-gap: 5px;
        }

        .Epigraf {
            grid-column: 1 / 3; /* span from grid column line 1 to 3 (i.e., span 2 columns) */
        }

        .MoreInfo {
            display: block;
            margin-top: 20px;
        }

        .Bulto {
            margin-top: 20px;
            display: grid;
            grid-template-columns: auto 1fr;
        }

            .Bulto > div > div:nth-child(1) {
                padding: 10px 0 5px;
                font-weight: 600;
            }

        .Parcel {
            background-repeat: no-repeat;
            width: 100px;
            height: 83px;
            margin-right: 10px;
        }

        .ParcelClosed {
            background-image: url('/Media/Img/Misc/parcel.100.closed.jpg');
        }

        .ParcelOpen {
            background-image: url('/Media/Img/Misc/parcel.100.open.jpg');
        }

        .Contents {
            grid-column: 1 / 3; /* span from grid column line 1 to 3 (i.e., span 2 columns) */
            display: none;
            grid-template-columns: 100px 1fr;
        }

            .Contents img {
                width: 100%;
            }

        .SkuThumbnail {
            width: 100px;
            height: auto;
            margin-right: 10px;
        }


        .Features {
            padding: 20px 0 0 10px;
        }
    </style>
End Section






