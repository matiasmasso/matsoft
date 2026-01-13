@ModelType MatHelperStd.SpriteHelper.Sprite
@Code
    Layout = "~/Views/shared/_Layout.vbhtml"
End Code


<div class="DeptsSpriteContainer">
    @For Each item In Model.items
        @<a href="@item.url" target="_parent" title="@item.caption">
            <div class="DeptsSpriteItem bigText" style="background-position: @(item.offsetX)px @(item.offsetY)px ;">@item.caption</div>
        </a>
    Next
</div>




@Section Styles

    <style>

    .DeptsSpriteContainer {
        display: flex;
        flex-direction: column;
        align-items: center;
    }

    .DeptsSpriteItem {
        background-image: url("@Model.url");
        background-repeat: no-repeat;
        height: @(Model.itemHeight)px;
        width: @(Model.itemWidth)px;
        margin-bottom: 10px;
    }

        .bigText {
            font-family: 'Roboto', sans-serif;
            font-weight: 900;
            color: white;
            text-transform: uppercase;
            font-size: 2rem;
        }

    </style>

End Section
