@ModelType MatHelperStd.SpriteHelper.Sprite
@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"

End Code

    <div class="DeptsSpriteContainer">
        @For Each item In Model.items
            @<a href="@item.url" target="_parent" title="@item.caption">
                 <div class="DeptsSpriteItem imageShadow bigText" style="background-position: @(item.offsetX)px @(item.offsetY)px ;">@item.caption</div>
            </a>
        Next
    </div>




    <style>

    .DeptsSpriteContainer {
        display: flex;
        flex-wrap: wrap;
        flex-direction: column;
    }

    .DeptsSpriteItem {
        background-image: url("@Model.url");
        background-repeat: no-repeat;
        height: @(Model.itemHeight)px;
        width: @(Model.itemWidth)px;
    }


    .DeptCaption {
        text-align: right;
        margin-top: -10px;
    }

        .imageShadow {
            background: linear-gradient( rgba(0, 0, 0, 0.8), rgba(0, 0, 0, 0.8) ), url('http://amolife.com/image/images/stories/Animals/penguins%20(8).jpg');
            background-size: cover;
            width:  @(Model.itemWidth)px;
            height:  @(Model.itemHeight)px;
            margin: 10px 0 0 10px;
            position: relative;
            float: left;
        }

        .bigText {
            font-family: 'Roboto', sans-serif;
            font-weight: 900;
            color: white;
            text-transform: uppercase;
            margin: 0;
            position: absolute;
            top: 50%;
            left: 50%;
            font-size: 2rem;
            transform: translate(-50%, -50%);
        }
    </style>
