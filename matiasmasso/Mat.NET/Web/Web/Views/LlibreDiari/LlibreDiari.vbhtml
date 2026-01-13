@Code
    Dim exs As New List(Of Exception)
    Layout = "~/Views/shared/_Layout_FullWidth.vbhtml"

    Dim oExercicis = FEB2.Exercicis.AllSync(exs, Mvc.GlobalVariables.Emp)
    Dim oCcas As List(Of DTOCca) = Nothing
    If oExercicis.Count > 0 Then
        oCcas = FEB2.LlibreDiari.HeadersSync(exs, oExercicis(0))
    End If
    Dim sTitle As String = Mvc.ContextHelper.Tradueix("Libro Diario", "Llibre Diari", "Journal of Accounting")
    ViewBag.Title = sTitle
    Dim pagesize As Integer = 15
End Code

<div class="pagewrapper">
    <section class="PageTitle">
        <span class="TitleText">
            @sTitle
        </span>
        @If oExercicis.Count > 0 Then
            @<a href='@FEB2.LlibreDiari.ExcelUrl(oExercicis(0))' title='@(Mvc.ContextHelper.Tradueix("Descargar fichero Excel", "Descarregar fitxer Excel", "Download Excel file")) '>
                <img class="Excel" src="~/Media/Img/48x48/Excel48.png" />
            </a>
        End If
    </section>


    <section class="YearSelector">
        <select>
            @For Each oExercici As DTOExercici In oExercicis
                If oExercici.Equals(oExercicis.First) Then
                    @<option selected value="@oExercici.Guid.ToString">@oExercici.Year</option>
                Else
                    @<option value="@oExercici.Guid.ToString">@oExercici.Year</option>
                End If
            Next
        </select>
    </section>


    @If oExercicis.Count = 0 Then
        @<div>
            @Mvc.ContextHelper.Tradueix("No constan datos", "No consten dades", "No data available")
        </div>
    Else
        @<div id="Items">
            @Html.Partial("LlibreDiari_", oCcas.Take(pagesize))
        </div>

        @<div id='Pagination' data-paginationurl='@Url.Action("pageindexchanged")' data-guid="@oExercicis(0).Guid.ToString" data-pagesize='@pagesize' data-itemscount='@oCcas.Count'></div>
    End If


</div>



@Section Scripts
    <script src="~/Media/js/Tables.js"></script>
    <script>
        $(document).on('change', '.YearSelector select', function (e) {
            var guid = $(this).val();
            var url = '@Url.Action("FromYear")';
            $('.loading').show();
            $('#Items').load(url, { guid: guid }, function (result) {
                $('.loading').hide();
            });
        });
    </script>
End Section
@Section Styles
    <style>
        main {
            max-width: 700px;
        }

        .TitleText {
            vertical-align: top;
        }

        .Excel {
            float: right;
        }
    </style>
End Section