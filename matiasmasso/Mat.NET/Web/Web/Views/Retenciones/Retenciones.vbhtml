@ModelType List(Of DTOCertificatIrpf)

@Code
    Layout = "~/Views/Shared/_Layout_sideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)
    Dim more As Integer = 4
End Code

<h1>@ViewBag.Title</h1>

<div id="DocsGrid" class="Flex-container">
    @For Each item In Model
        @<div class='Grid-item'>
             <a href="@FEB2.DocFile.DownloadUrl(item.DocFile, False)">
                 <div class="truncate period">
                     @item.FullPeriod()
                 </div>
                 <img src="@FEB2.DocFile.ThumbnailUrl(item.DocFile)" width='150' height='auto' />
             </a>
        </div>
    Next


</div>

@If Model.Count > more Then
    @<div class="ShowMore">
        <a href="#"> @lang.tradueix("ver más", "veure'n més", "see more")</a>
    </div>
    @<div class="ShowLess" hidden>
        <a href="#"> @lang.tradueix("ver menos", "veure menys", "see less")</a>
    </div>
End If

@Html.Partial("_AvailableOnAppstore")

@Section Styles

    <style scoped>

        .Flex-container {
            display: flex;
            height: 220px;
            column-gap: 10px;
            overflow-y: hidden;
            flex-wrap: wrap;
        }

        .Grid-container {
            display: grid;
            grid-gap: 10px;
            grid-template-columns: repeat(auto-fill, 150px);
            grid-auto-flow: row;
        }


        .ShowMore, .ShowLess {
            border-bottom: 1px solid gray;
            margin-left: 150px;
            text-align: right;
        }
        .period {
            text-align:center;
        }
    </style>
End Section

@Section Scripts
    <script>
        $(document).on('click', 'div.ShowMore a', function (e) {
            event.preventDefault();
            $('#DocsGrid').removeClass("Flex-container");
            $('#DocsGrid').addClass("Grid-container");
            $('.ShowMore').hide();
            $('.ShowLess').show();
        });
        $(document).on('click', 'div.ShowLess a', function (e) {
            event.preventDefault();
            $('#DocsGrid').removeClass("Grid-container");
            $('#DocsGrid').addClass("Flex-container");
            $('.ShowMore').show();
            $('.ShowLess').hide();
        });
    </script>
End Section
