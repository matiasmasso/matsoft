@ModelType MatHelperStd.SpriteHelper.Sprite
@Code
    Layout = "~/Views/Shared/_Layout_Test.vbhtml"
End Code



<div class="WebPortadaSprite">
    @For Each item In Model.items
        @<a href="@item.url" target="_parent" title="@item.caption">
            <div class="WebPortadaSpriteItem" style="background-position: @(item.offsetX)px @(item.offsetY)px ;"></div>
        </a>
    Next

</div>

<img src="@Model.url"/>


@Section Styles
    <style>

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

    </style>
End Section



