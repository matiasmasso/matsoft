@ModelType DTOPgcExtracte
@Code
    Layout = "~/Views/Shared/_Layout_FullWidth.vbhtml"
    
    Dim exs As New List(Of Exception)
    Dim oContactGuid As Guid = Nothing
    If Model.Contact IsNot Nothing Then oContactGuid = Model.Contact.Guid
End Code



<section class="Year">
    <select data-contact="@oContactGuid.ToString" data-cta="@Model.Cta.Guid.ToString">
        @For Each year As Integer In FEB.SumasYSaldos.YearsSync(exs, Website.GlobalVariables.Emp, Model.Contact, Model.Cta)
            @<option @IIf(year = Model.Exercici.Year, "selected", "")>@year</option>
        Next
    </select>
</section>

<section class="Detail">
    @Html.Partial("_Extracte", Model)
</section>


@Section Styles
    <style>
        .pagewrapper {
            max-width: 900px;
            margin: auto;
        }
        section.Year {
            display:inline-block;
            float:right;
            text-align:right;
        }
        section.Year:before {
            clear:both;
        }

    </style>
End Section

@Section Scripts
    <script src="~/Media/js/PgcExtracte.js"></script>
End Section
