@ModelType MatHelperStd.SpriteHelper.Sprite
@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

@section hreflang
    <link rel="alternate" hreflang="es" href="/es" />
    <link rel="alternate" hreflang="ca" href="/ca" />
    <link rel="alternate" hreflang="en" href="/en" />
End Section

<div class="portada">
    <div class="HideOnNarrow">
        <div class="bannerRotator"><a><img /></a></div>

        <div class="WebPortadaSprite">
            @For Each item In Model.items
                @<a href="@item.url" target="_parent" title="@item.caption">
                    <div class="WebPortadaSpriteItem" style="background-position: @(item.offsetX)px @(item.offsetY)px ;">&nbsp;</div>
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

    .WebPortadaSprite {
        display: flex;
        flex-wrap: wrap;
    }

    .WebPortadaSpriteItem {
        background-image: url("@Model.url");
        background-repeat: no-repeat;
        height: @(Model.itemHeight)px;
        width: @(Model.itemWidth)px;
    }

        .MobileHomeExcerpt {
            padding:0 20px;
        }

        .MobileHomeTitle {
            padding: 0 20px;
        }

    </style>
End Section