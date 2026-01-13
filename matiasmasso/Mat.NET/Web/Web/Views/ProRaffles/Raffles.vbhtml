@Code
    Layout = "~/Views/shared/_Layout_Pro.vbhtml"
    Dim lang = Mvc.ContextHelper.Lang
End Code

        <div class="Raffles">
            <div class="Year">
                @Mvc.ContextHelper.Tradueix("Año", "Any", "Year", "Ano")
                <select>
                    <option selected value="@Today.Year">@Today.Year</option>
                    @for year As Integer = Today.Year - 1 To 2012 Step -1
                        @<option value="@year">@year</option>
                    Next
                </select>
            </div>

            <div class="Grid" data-contextmenu="Raffles"></div>

            <div class="ContextMenu" data-grid="Raffles">
                <a href="#" data-url="/pro/proRaffle/Index/{guid}" target="_blank">@lang.Tradueix("Ficha", "Fitxa", "Properties")</a>
            </div>
        </div>


@Section Scripts
    <script src="~/Media/js/ContextMenu.js"></script>
    <script>

        $(document).ready(function () {
            loadRaffles();
        });

        $(document).on('change', '.Raffles .Year select', function () {
            loadRaffles();
        });

        $(document).on('contextmenu', '.Raffles', function (e) {
            return LoadContextMenu('[data-menu=Raffle]', e.pageX, e.pageY);
        });

        $(document).on('click', '.ContextMenu > a', function (e) {
            var menu = $(this).closest('[data-menu]');
            var grid = menu.siblings('.Grid');
            var activeItem = grid.children('.Active').first();
            var guid = activeItem.data('guid');
            var url = $(this).data('url');
            $(this).attr('href', url + guid);
        });

        $(document).on('click', '.Grid > div:not(:first)', function (e) {
            $('.Grid div').removeClass('Active');
            $(this).addClass('Active');
        });


        function loadRaffles() {
            var divGrid = $('.Raffles .Grid');
            var year = $('.Raffles .Year select').val();
            var url = '/pro/proRaffles/RafflesPartial';
            var data = { year: year };
            divGrid.empty();
            divGrid.append(spinner);
            divGrid.load(url, data, function () {
                spinner.remove();
            });
        }

    </script>
End Section


@Section Styles
    <style>
        .MainContent {
            max-width: 900px;
        }


        .Year {
            display: flex;
            flex-direction: row;
            justify-content: start;
            column-gap: 10px;
        }

        .Grid {
            margin: 20px 0;
            grid-template-columns: auto auto auto minmax(100px, 1fr) auto auto auto auto auto auto 16px;
            font-size: smaller;
        }

        .Grid .Row div {
            padding: 4px 7px 2px 4px;
        }


            .Grid .Status {
                width: 16px;
                height: 16px;
                background-repeat: no-repeat;
                background-position: center;
            }
            .Grid .Status.WinnerReacted {
                background-image: url('../../Media/Img/Ico/star_red.gif');
            }
            .Grid .Status.WinnerReacted {
                background-image: url('../../Media/Img/Ico/star_red.gif');
            }
            .Grid .Status.DistributorReacted {
                background-image: url('../../Media/Img/Ico/star.gif');
            }
            .Grid .Status.Delivered {
                background-image: url('../../Media/Img/Ico/star_blue.gif');
            }
            .Grid .Status.WinnerPictureSubmitted {
                background-image: url('../../Media/Img/Ico/ok.png');
            }


    </style>
End Section

