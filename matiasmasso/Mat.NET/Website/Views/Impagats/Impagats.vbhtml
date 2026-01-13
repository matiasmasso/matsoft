@ModelType List(Of DTOImpagat)
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"

End Code


<h1>@ViewBag.Title</h1>

<div class="Grid">

    <div class="RowHdr">
        <div class="CellFch">
            @ContextHelper.Tradueix("Vencimiento", "Venciment", "Due Date")
        </div>
        <div class="CellAmt">
            @ContextHelper.Tradueix("Importe", "Import", "Amount")
        </div>
        <div class="CellTxt">
            @ContextHelper.Tradueix("Deudor", "Deutor", "Customer")
        </div>
    </div>


    @For Each item As DTOImpagat In Model
        @<div class="Row" data-url="@FEB.Contact.Url(item.Csb.Contact)">
            <div class="CellFch">
                @Format(item.Csb.Vto, "dd/MM/yy")
            </div>
            <div class="CellAmt">
                @DTOAmt.CurFormatted(item.Csb.Amt)
            </div>
            <div class="CellTxt">
                @item.Csb.Contact.FullNom
            </div>
        </div>
    Next
</div>




@Section Styles
    <link href="~/Media/Css/tables.css" rel="stylesheet" />
End Section