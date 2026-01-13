@Code
    Layout = "~/Views/Shared/_Layout_FullWidth.vbhtml"
    Dim oWebsession As DTO.DTOSession = BLL.BLLSession.Find(Session("SessionId"))

    Dim Years As List(Of Integer) = BLL.BLLModel349.Years()
    Dim oExercici As DTO.DTOExercici = Nothing
    Dim items As List(Of DTO.DTOModel349) = Nothing

    If Years.Count > 0 Then
        oExercici = BLL.BLLExercici.FromYear(Years(0))
        items = BLL.BLLModel349.All(oExercici.Year)
    End If
End Code

<div class="pagewrapper">
    <section class="year">
        <select>
            @For Each year As Integer In Years
                @<option @IIf(year = oExercici.Year, "selected", "")>@year</option>
            Next
        </select>
    </section>

    <section class="detail">
        @Html.Partial("AeatMod349_", items)
    </section>
</div>

@Section Styles
    <style>
        main {
            max-width: 100%;
            margin: auto;
            font-size:0.7em;
        }
        .CellTxt.Nif {
            min-width:80px;
            max-width:100px;
        }
        .CellTxt.Nom {
            min-width:180px;
            max-width:500px;
        }
    </style>
End Section

@Section Scripts
    <script>
        $(document).on('change','section.year select', function () {
            $('.loading').show();
            var url='@Url.Action("FromYear")'
            var year = $(this).val();
            $('section.detail').load(url, { year: year }, function () { $('.loading').hide(); })
        });
    </script>
End Section