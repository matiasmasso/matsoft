@ModelType MatHelperStd.SpriteHelper.Sprite
@Code
    Layout = "" 'prevent layout inheritance from _ViewStart.vbhtml
End Code

<style>
    .hScrollContainer {
        display: flex;
        align-items: center;
        justify-content: center;
    }

    .item {
        display: flex;
        background-image: url("@Model.url");
        background-repeat: no-repeat;
        height: @Model.itemHeight;
        width: @Model.itemWidth;
    }

    .hScroll {
        display: flex;
        overflow-x: auto;
    }

        .hScroll::-webkit-scrollbar {
            display: none;
        }

        .hScroll a {
            text-decoration: none;
        }

        .hScroll .label {
            font-size: 0.8em;
            font-family: Arial, Helvetica, sans-serif;
            color: gray;
            margin: 0;
            padding: 0;
            max-width: @Model.itemWidth;
            text-align: center;
            overflow: hidden;
            white-space: nowrap;
            text-overflow: ellipsis;
        }


    #ChevronLeft {
        padding: 0 10px 0 0;
    }

    #ChevronRight {
        padding: 0 0 0 10px;
    }

</style>



<div class="hScrollContainer">


    @If Model.items.Count > 1 Then
        @<a href="#" id="ChevronLeft">
            <img src="~/Media/Img/Ico/ChevronLeft.png" />
        </a>
    End If

    <div class="hScroll">
        @For Each item In Model.items
            @<a href="@item.url" target="_parent" title="@item.caption">
                <div class="item" style="background-position: @item.offsetX @item.offsetY ;"></div>

                <div class="label">
                    @item.caption
                </div>
            </a>
        Next
    </div>


    @If Model.items.Count > 1 Then
        @<a href="#" id="ChevronRight">
            <img src="~/Media/Img/Ico/ChevronRight.png" />
        </a>
    End If

</div>

<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
<script>

    $(document).ready(function () {

            $("#ChevronLeft").fadeTo("slow", 0.2);
    });

    $(document).on('click', '#ChevronLeft', function (event) {
        event.preventDefault();
        var itemWidth = @Model.itemWidth;
        var pos = Math.round($(".hScroll").scrollLeft());
        var next = pos - itemWidth;
        $("#ChevronRight").fadeTo("slow", 1);
        $("#ChevronRight").prop('disabled', false);
        $(".hScroll").animate({ scrollLeft: next }, 1000, function () {
            var newPos = $(".hScroll").scrollLeft();
            if (newPos == 0) {
                $("#ChevronLeft").fadeTo("slow", 0.2);
                $("#ChevronLeft").prop('disabled', true);
            }

        });

    });



    $(document).on('click', '#ChevronRight', function (event) {
        event.preventDefault();
        var itemWidth = @Model.itemWidth;
        var pos = Math.round($(".hScroll").scrollLeft());
        var next = pos + itemWidth;
        $("#ChevronLeft").fadeTo("slow", 1);
        $("#ChevronLeft").prop('disabled', false);
        $(".hScroll").animate({ scrollLeft: next }, 1000, function () {
            var newPos = Math.round($(".hScroll").scrollLeft());
            if (newPos == pos) {
                $("#ChevronRight").fadeTo("slow", 0.2);
                $("#ChevronRight").prop('disabled', true);
            }
        });

    });


</script>
