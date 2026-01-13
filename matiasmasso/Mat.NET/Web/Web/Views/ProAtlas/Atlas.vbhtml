@Code
    Layout = "~/Views/shared/_Layout_Pro.vbhtml"
    Dim lang = Mvc.ContextHelper.Lang
End Code

        <div class="Atlas">
            <div class="SubmitComplexRow">
                <input type="Text" id="SearchBox" />
                <input type="Button" class="Submit" value='@Mvc.ContextHelper.Tradueix("Buscar", "Cercar", "Search")' />
            </div>

            <div class="Grid Results" data-contextmenu="Contacts"></div>

            <div class="ContextMenu" data-grid="Contacts">
                <a href="#" data-url="/pro/proAtlas/Contact/{guid}" target="_blank">@lang.Tradueix("Ficha", "Fitxa", "Properties")</a>
            </div>
        </div>



@Section Scripts
    <script src="~/Media/js/ContextMenu.js"></script>
    <script>
        var contextmenu = $(".ContextMenu");
        var selectedItem;


        $(document).on('click', '.Atlas .SubmitComplexRow .Submit', function (e) {
            var submitRow = $(this).parent();
            var button = $(this);
            var searchbox = submitRow.find('input[type=text]');
            var resultsDiv = $('.Atlas .Results');
            var url = '/pro/proAtlas/searchPartial';
            var data = { searchKey: searchbox.val() };
            button.hide();
            submitRow.append(spinner);
            resultsDiv.load(url, data, function () {
                spinner.remove();
                button.show();
            });
        });

        $(document).on('click', '.Atlas .Results div', function (e) {
            $('.Atlas .Results div').removeClass('Active');
            $(this).addClass('Active');
        });


        $(document).on('contextmenu', '.Atlas', function (e) {
            return LoadContextMenu('[data-menu=Contact]', e.pageX, e.pageY);
        });

    </script>
End Section


@Section Styles
    <style>
        .MainContent {
            max-width: 600px;
        }
        .Atlas .Results a {
            display:block;
            padding: 4px 7px 2px 4px;
        }

        .Atlas .Results > a:hover {
            background-color: #167ac6;
            color: white;
            cursor: pointer;
        }



    </style>
End Section

