@Modeltype DTONomina.Header.Collection
@Code
    Layout = "~/Views/Shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, ContextHelper.Lang, ViewBag.Lang)
End Code


@If Model.Count > 0 Then
    @<div Class="Title">
        <h1>@ViewBag.Title</h1>
        <select id="Years">
            @For Each year As Integer In Model.Years()
                @<option>@year</option>
            Next
        </select>
    </div>


    @<div Class="Grid-container">
        @For Each item In Model.Where(Function(x) x.Fch.Year = Model.Years(0))
            @<div class="Grid-item">
                <a href="@item.DownloadUrl()" target="_blank">
                    <img src="@item.ThumbnailUrl" width="@DTODocFile.THUMB_WIDTH" height="@DTODocFile.THUMB_HEIGHT" />
                    <div class="Fch">
                        @Format(item.Fch, "dd/MM/yy")
                    </div>
                </a>

            </div>
        Next
    </div>

    @Html.Partial("_AvailableOnAppstore")

Else
    @<h1>@ViewBag.Title</h1>
    @<p>
        @Html.Raw(lang.tradueix("No se han encontrado nóminas de este usuario", "No s'ha trobat cap nómina d'aquest usuari", "No payrolls found for current user"))
    </p>
End If


@Section Styles
    <style scoped>
        .Title {
            display: flex;
            margin-bottom: 20px;
        }

            .Title h1 {
                padding: 2px 10px 0 0;
            }

        #Years {
            border: 1px solid #EEEEEE;
            font-family: Open Sans, Arial;
            font-size: 2em;
            font-weight: bold;
        }

        .Grid-container {
            display: grid;
            grid-gap: 10px;
            grid-template-columns: repeat(auto-fill, 150px);
            grid-auto-flow: row;
            margin-bottom:15px;
        }

            .Grid-container .Grid-item {
                width: 150px;
                height: 200px;
                border: 1px solid gray;
                text-align: center;
                padding: 0;
            }

                .Grid-container .Grid-item img {
                    width: 100%;
                    height: auto;
                    padding: 0;
                    margin: 0;
                }
    </style>
End Section


@Section Scripts
    <script>
        var model = @Html.Raw(Model.Serialized());

        $(document).on("change", "#Years", function (e) {
            var selectedYear = $(this).val();
            var items = filteredItems(selectedYear);
            var html = gridContent(items);
            $('.Grid-container').html(html);
        });

        function filteredItems(selectedYear) {
            var retval = model.filter(item => {
                var date = jsonDate(item);
                var year = date.getFullYear();
                return selectedYear == year;
            });
            return retval;
        }

        function gridContent(items) {
            var html = '';
            for (i = 0; i < items.length; i++) {
                var item = items[i];
                var fch = formattedFch(item);
                html += '<div class="Grid-item">';
                html += '<a href="' + item.DownloadUrl + '" target="_blank">';
                html += '<img src="' + item.ThumbnailUrl + '" width="@DTODocFile.THUMB_WIDTH" height="@DTODocFile.THUMB_HEIGHT" />';
                html += '<div class="CellFch">' + fch + '</div>'
                html += '</a>';
                html += '</div>';
            }
            return html;
        }

        function jsonDate(item) {
            var fch = item.Fch;
            var retval = new Date(parseInt(fch.substr(6)));
            return retval;
        }

        function formattedFch(item) {
            var date = jsonDate(item);
            var retval = date.getDate() + '/' + (date.getMonth() + 1) + '/' + date.getFullYear();
            return retval;
        }

    </script>
End Section