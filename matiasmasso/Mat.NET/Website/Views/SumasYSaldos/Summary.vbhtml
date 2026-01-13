@Code
    Layout = "~/Views/Shared/_Layout_SideNav.vbhtml"

    Dim exs As New List(Of Exception)

    Dim Years As List(Of Integer) = FEB.SumasYSaldos.YearsSync(exs, Website.GlobalVariables.Emp)
    Dim oExercici As DTOExercici = Nothing
    Dim oSaldos As List(Of DTOBalanceSaldo) = Nothing

    If Years.Count > 0 Then
        oExercici = DTOExercici.FromYear(Website.GlobalVariables.Emp, Years(0))
        oSaldos = FEB.Balance.SumasySaldosSync(exs, oExercici)
    End If
End Code



<div class="pagewrapper">

    <h1>
        @ViewBag.Title
        <select id="Year">
            @For Each year As Integer In Years
                @<option @IIf(year = oExercici.Year, "selected", "")>@year</option>
            Next
        </select>
    </h1>

    <section class="detail">
        @Html.Partial("_Summary", oSaldos)
    </section>
</div>

@Section Styles
    <link href="~/Media/Css/tables.css" rel="stylesheet" />
    <style scoped>
        .pagewrapper {
            max-width: 600px;
            margin: auto;
        }

        h1 {
            display: flex;
            justify-content: space-between;
        }

            h1 select {
                font-size: 1em;
                font-weight: bold;
                border: none;
            }
    </style>
End Section

@Section Scripts
    <script src="~/Media/js/SumasYSaldosSummary.js"></script>
End Section