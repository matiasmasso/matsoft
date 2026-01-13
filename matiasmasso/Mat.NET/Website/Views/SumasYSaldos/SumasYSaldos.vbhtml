@Modeltype DTOContact
@Code
    Layout = "~/Views/Shared/_Layout_FullWidth.vbhtml"
    
    Dim exs As New List(Of Exception)

    Dim Years As List(Of Integer) = FEB.SumasYSaldos.YearsSync(exs, Website.GlobalVariables.Emp, Model)
    Dim oExercici As DTOExercici = Nothing
    Dim oSaldos As List(Of DTOPgcSaldo) = Nothing
    Dim oContactGuid As Guid = Nothing

    If Years.Count > 0 Then
        oExercici = DTOExercici.FromYear(Website.GlobalVariables.Emp, Years(0))
        If Model Is Nothing Then
            oSaldos = FEB.SumasYSaldos.SummarySync(exs, oExercici)
        Else
            oSaldos = FEB.SumasYSaldos.AllSync(exs, oExercici, False, DTO.Defaults.ContactRange.OnlyThisContact, Model)
            oContactGuid = Model.Guid
        End If
    End If
End Code

<div class="pagewrapper">
    <section class="year">
        <select data-contact="@oContactGuid.ToString">
            @For Each year As Integer In Years
                @<option @IIf(year = oExercici.Year, "selected", "")>@year</option>
            Next
        </select>

        @If Model IsNot Nothing Then
            @<a href="@FEB.Contact.Url(Model)">
                @Model.FullNom
            </a>
        End If
    </section>

    <section class="detail">
        @If oSaldos IsNot Nothing Then
            @Html.Partial("_SumasYSaldos", oSaldos)
        End If

    </section>
</div>

@Section Styles
    <style>
        .pagewrapper {
            max-width:600px;
            margin:auto;
        }
    </style>
End Section
@Section Scripts
    <script src="~/Media/js/SumasYSaldos.js"></script>
End Section