@ModelType List(Of DTOCustomer)
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, ContextHelper.Lang, ViewBag.Lang)

    ViewBag.Title = ContextHelper.Tradueix("Mis facturas", "Les meves factures", "My invoices")
    'Layout = "~/Views/shared/_Layout.vbhtml"
End Code


<h1>@ViewBag.Title</h1>

<div>
    <select id="ContactNom">
        @For Each oCustomer As DTOCustomer In Model
            @<option value="@oCustomer.Guid.ToString" @IIf(oCustomer.Guid.Equals(Model(0).Guid), "selected", "")>@(oCustomer.Nifs.PrimaryNifValue() & ": " & oCustomer.Nom) </option>
        Next
    </select>
</div>

@If Model.Count = 0 Then
    @<span>@ContextHelper.Tradueix("No hay facturas registradas", "No hi han dades registrades", "Not records available")</span>
Else
    @<div id="Contact">
        @Html.Partial("Facturas_", Model(0))
    </div>
End If




@Section Styles
    <link href="~/Media/Css/tables.css" rel="stylesheet" />
    <style scoped>
        .ContentColumn {
            max-width: 450px;
        }

        #ContactNom {
            margin: 0 0 20px 0;
        }

        /*-------------------- tables ----------------------*/

        .pagewrapper {
            max-width: 400px;
            margin: auto;
        }
    </style>
End Section

@Section Scripts
    <script src="~/Scripts/Pagination.js"></script>
    <script>

    $(document).ready(function () {
        $('#ContactNom').change(function () {
            var url = '@Url.Action("contactChanged")';
            var data = { contact: $(this).val() };
            $('#Contact').load(url, data);
        });

    });

    </script>
End Section