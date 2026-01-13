@ModelType List(Of DTOPgcClass)
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, ContextHelper.Lang, ViewBag.Lang)
End Code


<h1>@ViewBag.Title</h1>


<div class="Grid">

    <div class="Row">
        <div class="CellTxt">
            @ContextHelper.Tradueix("Concepto", "Concepte", "Concept")
        </div>
        <div class="CellNum CurrentYear">
            @Html.Raw(ViewBag.Fch.Year)
        </div>
        <div class="CellNum PreviousYear">
            @Html.Raw(ViewBag.Fch.Year - 1)
        </div>
    </div>

    @For Each oClass As DTOPgcClass In Model
        @Html.Partial("ClassItem_", oClass)
    Next

</div>

@Section styles
    <link href="~/Media/Css/tables.css" rel="stylesheet" />
    <style>

        .RootPgcClass {
            font-weight: 700;
        }

        .CellNum {
            max-width: 100px;
        }

        @@media screen and (max-width:400px) {
            .PreviousYear {
                display: none;
            }
        }
    </style>
End Section







