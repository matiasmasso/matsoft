@ModelType List(Of DTOWebPortadaBrand)
@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code



@section hreflang
    <link rel="alternate" hreflang="es" href="/es" />
    <link rel="alternate" hreflang="ca" href="/ca" />
    <link rel="alternate" hreflang="en" href="/en" />
End Section

<div class="portada" >
    <div class="HideOnNarrow">
        <div class="bannerRotator"><a><img /></a></div>

        <div class="Brandbox">
            @For Each item As DTOWebPortadaBrand In Model
                @<a href="@FEB2.ProductBrand.Url(item.Brand)" title="@item.Brand.Nom">
                    <img src="@FEB2.WebPortadaBrand.ImageUrl(item)" alt="@item.Brand.Nom" />
                </a>
            Next
        </div>
    </div>
    <div class="ShowOnNarrow">
        @Html.Partial("_MobileHome")
    </div>
</div>



@Section Scripts
    <script src="~/Media/js/BannerRotator.js"></script>
End Section

@Section Styles
<style>
    .pagewrapper {
        text-align: center;
    }

    .portada {
        display: inline-block;
        text-align: left;
    }

        .portada a {
            display: inline-block;
        }

    .Brandbox {
        text-align: left;
    }


</style>
End Section