@ModelType Tuple(Of DTOAeatModel, DTOAeatDoc.Header.Collection)
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"

    Dim oAeatModel = Model.Item1
    Dim oDocs = Model.Item2
End Code
<div class="pagewrapper">

    <div Class="Title">
        <h1>@ViewBag.Title</h1>
        <select id="Years">
            @For Each year As Integer In oDocs.Years
                @<option>@year</option>
            Next
        </select>
    </div>

    @If oDocs.Count = 0 Then
        @<div>
            @ContextHelper.Tradueix("No constan modelos declarados", "No consten models declarats", "No declared forms are available")
        </div>
    Else

        @<div Class="Grid-container">
            @For Each item In oDocs.Where(Function(x) x.Fch.Year = oDocs.Years.First)
                @<div class="Grid-item">
                     <a href="@item.DownloadUrl()" target="_blank">
                         <div class="Fch">
                             @Format(item.Fch, "dd/MM/yy")
                         </div>
                         <img src="@item.ThumbnailUrl" width="@DTODocFile.THUMB_WIDTH" height="@DTODocFile.THUMB_HEIGHT" />
                     </a>

                </div>
            Next
        </div>

    End If
</div>


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
        }

            .Grid-container .Grid-item {
                width: 150px;
                height: auto;
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
        var model = @Html.Raw(oDocs.Serialized());

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
                html += '<div class="CellFch">' + fch + '</div>'
                html += '<img src="' + item.ThumbnailUrl + '" width="@DTODocFile.THUMB_WIDTH" height="@DTODocFile.THUMB_HEIGHT" />';
                html += '</a>';
                html += '</div>';
            }
            return html;
        }

        function jsonDate(item) {
            var retval = new Date(item.Fch);
            return retval;
        }

        function formattedFch(item) {
            var date = jsonDate(item);
            var retval = date.getDate() + '/' + (date.getMonth() + 1) + '/' + date.getFullYear();
            return retval;
        }

    </script>
End Section