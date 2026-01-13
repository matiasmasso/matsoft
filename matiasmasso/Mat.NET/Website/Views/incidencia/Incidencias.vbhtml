@ModelType DTOIncidenciaQuery

@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, ContextHelper.Lang, ViewBag.Lang)
End Code


    <div class="title">
        <div>
            <h1>@ViewBag.Title</h1>
            <select id="CurrentYear">
                <option value="@DTO.GlobalVariables.Today().Year" selected>@DTO.GlobalVariables.Today().Year</option>
                @For Year As Integer = DTO.GlobalVariables.Today().Year - 1 To DTO.GlobalVariables.Today().Year - 4 Step -1
                    @<option value="@Year">@Year</option>
                Next
            </select>
            <div class="Spinner64"></div>
        </div>

        <a id="ExcelLink" href='/incidencias/excel/@Model.Year' title='@(Model.Lang.Tradueix("Descargar fichero Excel", "Descarregar fitxer Excel", "Download Excel file")) '>
            <img class="Excel" src="~/Media/Img/48x48/Excel48.png" />
        </a>
    </div>

    <div id="Items">
    </div>



@Section Styles
    <style>

        .title {
            display: flex;
            justify-content: space-between;
        }

        .title div {
            display: inline-flex;
            flex-direction: row;
            align-items: center;
        }
        .title select {
            max-height: 20px;
            margin-left: 20px;
        }

        .Grid {
            display: grid;
            grid-template-columns: 70px 80px 80px 80px 1fr 120px 100px 1fr 80px;
            border-top: 1px solid gray;
            border-right: 1px solid gray;
        }

            .Grid > span, .Grid > .Item > span {
                padding: 8px 4px 2px 4px;
                border-left: 1px solid gray;
                border-bottom: 1px solid gray;
            }

            /*(2 x num.de columnes n + num.de columnes+1*/
                .Grid > span:nth-child(18n+10),
                .Grid > span:nth-child(18n+11),
                .Grid > span:nth-child(18n+12),
                .Grid > span:nth-child(18n+13),
                .Grid > span:nth-child(18n+14),
                .Grid > span:nth-child(18n+15),
                .Grid > span:nth-child(18n+16)
                .Grid > span:nth-child(18n+17)
                .Grid > span:nth-child(18n+18) {
                    background-color: #EFEFEF;
                }

        .Item {
            display: contents;
        }

            .Item:hover span {
                background-color: #dbeaf4;
                cursor: pointer;
            }

        .CellId {
            text-align: right;
            text-align: center;
        }

        .CellFch, .CellIco {
            text-align: center;
        }

        .CellTxt {
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
        }

       .CellCamera {
            background-image: url('/Media/Img/Ico/Camera.png');
            padding-top: 2px;
            background-repeat: no-repeat;
            background-position: center;
        }

        .CellVideo {
            background-image: url('/Media/Img/Ico/video20x31.png');
            padding-top: 2px;
            background-repeat: no-repeat;
            background-position: center;
        }
    </style>
End Section

@Section Scripts

    <script>
        $(document).ready(function () {
            LoadIncidencias();
        });

        $(document).on('change', '#CurrentYear', function () {
            UpdateExcelLink();
            LoadIncidencias();
        });

        $(document).on('click', '.Item', function (e) {
            var url = $(this).data("url");
            window.location.href = url;
        });

        function LoadIncidencias() {
            $('.Spinner64').show();
            var url = '@Url.Action("PartialIncidencias")';
            var data = { id: CurrentYear() };
            $('#Items').load(url, data, function (result) {
                $('.Spinner64').hide();
            });
        }

        function UpdateExcelLink() {
            $('#ExcelLink').attr('href','/incidencias/excel/' + CurrentYear())
        }

        function CurrentYear() {
            return $('#CurrentYear').val();
        }
    </script>


End Section

