@ModelType List(Of DTOPurchaseOrder)
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, ContextHelper.Lang, ViewBag.Lang)

    ViewBag.Title = lang.Tradueix("Mis pedidos", "Les meves comandes", "My orders")
    Dim CliGuid As Guid = Nothing
    If Model.Count > 0 Then CliGuid = Model(0).Contact.Guid
    Dim pagesize As Integer = 15
End Code


<h1 class="PageTitle">@ViewBag.Title</h1>
    @If Model.Count = 0 Then
        @<div>
            @ContextHelper.Tradueix("No nos constan pedidos registrados", "No ens consten comandes registrades", "No orders have been logged")
        </div>
    Else
        @<div>
            <div id="Items">
                @Html.Partial("PurchaseOrders_", Model.Take(pagesize))
            </div>

            <div id='Pagination' data-paginationurl='@Url.Action("pageindexchanged")' data-guid="@CliGuid.ToString" data-pagesize='@pagesize' data-itemscount='@Model.Count'></div>
        </div>
    End If


@Section Scripts
    <script src="~/Scripts/Pagination.js"></script>
End Section

@Section Styles
    <link href="~/Styles/Pagination.css" rel="stylesheet" />

    <style scoped>
        .ContentColumn{
            max-width: 600px;
            margin:0 auto;
        }

        .Grid {
            display: grid;
            grid-template-columns: 60px 32px 80px 1fr 130px;
            border-top: 1px solid gray;
            border-right: 1px solid gray;
        }
            .Grid > span {
                padding: 8px 4px 2px 4px;
                border-left: 1px solid gray;
                border-bottom: 1px solid gray;
            }


                .Grid > span:nth-child(10n+6),
                .Grid > span:nth-child(10n+7),
                .Grid > span:nth-child(10n+8),
                .Grid > span:nth-child(10n+9),
                .Grid > span:nth-child(10n+10) {
                    background-color: #EFEFEF;
                }
        .CellTxt {
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
        }
        .CellAmt, .CellNum {
            text-align: right;
            white-space: nowrap;
        }
        .CellFch, .CellIco {
            text-align: center;
        }
            
    </style>
End Section




