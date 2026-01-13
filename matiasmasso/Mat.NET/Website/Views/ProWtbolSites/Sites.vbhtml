@ModelType List(Of DTOWtbolSite)
@Code
    Layout = "~/Views/shared/_Layout_Pro.vbhtml"
    Dim lang = ContextHelper.Lang
End Code

        <div>
            <input type="text" class="SearchBox" />
        </div>

        <div class="Grid" data-contextmenu="Sites"  >
            <div class="Row">
                <div class='Icon'></div>
                <div>@lang.Tradueix("Participantes", "Participants", "Partners")</div>
                <div class="RightAlign">@lang.Tradueix("LP pendientes", "LP pendents", "pending LP")</div>
                <div class="RightAlign">@lang.Tradueix("LP aprobadas", "LP aprobades", "approved LP")</div>
                <div Class="RightAlign">@lang.Tradueix("LP denegadas", "LP denegades", "denied LP")</div>
            </div>
            @For Each item In Model
                @<div class="Row" data-guid="@item.Guid.ToString()" data-search="@item.Nom.ToLower()" data-url="@item.Url()" data-hatchfeedurl="@item.HatchFeedUrl()">
                    <div class='Icon @IIf(item.Active, "Enabled", "")'></div>
                    <div>@item.Nom</div>
                    <div class="RightAlign">@item.LandingPageStatusCount(DTOWtbolLandingPage.StatusEnum.Pending).ToString("N0")</div>
                    <div class="RightAlign">@item.LandingPageStatusCount(DTOWtbolLandingPage.StatusEnum.Approved).ToString("N0")</div>
                    <div Class="RightAlign">@item.LandingPageStatusCount(DTOWtbolLandingPage.StatusEnum.Denied).ToString("N0")</div>
                </div>
            Next
        </div>

        <div Class="ContextMenu" data-grid="Sites">
            <a href="#" data-url="/pro/proWtbolSite/Index/{guid}" target="_blank">@lang.Tradueix("Ficha", "Fitxa", "Properties")</a>
            <a href="#" data-url="/pro/proWtbolSite/LandingPages/{guid}" target="_blank">@lang.Tradueix("Landing pages")</a>
            <a href="#" data-url="/pro/proWtbolSite/Baskets/{guid}" target="_blank">@lang.Tradueix("Baskets")</a>
            <a href="#" data-action="Navigate" target="_blank"> Navegar</a>
            <a href="#" data-action="HatchFeed" target="_blank">Hatch feed</a>
        </div>

@Section Scripts
    <script src="~/Media/js/ContextMenu.js"></script>

    <script>
        var model = @Html.Raw(DTOBaseGuid.Serialized(Model))


        $(document).on('keyup', '.SearchBox', function (e) {
            var searchKey = $(this).val().toLowerCase();
            if (searchKey == '') {
                $('.Grid [data-search]').show();
            } else {
                $('.Grid [data-search]').hide();
                $('.Grid').find('[data-search*="' + searchKey + '"]').show();
            }
        });


        $(document).on('ContextMenuClick', function (e, argument) {
            switch (argument.action) {
                case "Navigate":
                    var url = argument.activeTag.data('url');
                    window.open(url, '_blank');
                    break;
                case "HatchFeed":
                    var url = argument.activeTag.data('hatchfeedurl');
                    window.open(url, '_blank');
                    break;
            }
        });

     </script>
End Section


@Section Styles
    <style>
        .MainContent {
            max-width: 600px;
            margin:auto;
        }

        .SearchBox {
            box-sizing: border-box;
            display: block;
            width: 300px;
            font-size: 1em;
            background-image: url('/Media/Img/Ico/magnifying-glass.jpg');
            background-repeat: no-repeat;
            background-position: right;
            border: 1px solid grey;
            padding: 4px 7px 2px 4px;
        }

        .Grid {
            grid-template-columns: 16px minmax(100px, 1fr) auto auto auto;
        }

            .Grid .Row div {
                padding: 4px 7px 2px 4px;
                text-overflow: ellipsis;
                white-space: nowrap;
                overflow: hidden;
            }

            .Grid .Row .Icon {
                width: 16px;
                background-repeat: no-repeat;
                background-position: left;
            }

                .Grid .Row .Icon.Enabled {
                    background-image: url('/Media/Img/Ico/ok.png');
                    padding-right: 10px;
                }

    </style>
End Section

