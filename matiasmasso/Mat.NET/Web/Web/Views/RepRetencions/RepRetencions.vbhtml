@ModelType List(Of DTORepCertRetencio)
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)
    Dim pagesize As Integer = 15
End Code


        <h1>@ViewBag.Title</h1>

        @If Model.Count = 0 Then
            @<div>
                @Mvc.ContextHelper.Tradueix("No nos constan liquidaciones de comisiones", "No ens consten liquidacions de comisions", "No commissions have been logged")
            </div>
        Else
            @<div>
                <div id="Items">
                    @Html.Partial("RepRetencions_", Model.Take(pagesize))
                </div>

                <div id='Pagination' data-paginationurl='@Url.Action("pageindexchanged")' data-guid="@Model.First.Rep.Guid.ToString" data-pagesize='@pagesize' data-itemscount='@Model.Count'></div>
            </div>
        End If


@Section Styles
    <style>
        .Grid {
            display: grid;
            grid-template-columns: 1fr 20px 1fr 1fr 1fr 1fr;
            grid-template-rows: 3em;
            align-items: center;
        }

        .RowHdr, .Row {
            display: contents;
        }


        .CellIco {
        }

        .CellFch {
            text-align: center;
        }

        .CellAmt {
            text-align: right;
        }
    </style>

End Section
