@Code
    
End Code

<div class="RowHdr">
    <div class="CellTxt">
        @Mvc.ContextHelper.Tradueix("Concepto", "Concepte", "Concept")
    </div>
    <div class="CellNum">
        @Mvc.ContextHelper.Tradueix("Cantidad", "Quantitat", "Quantity")
    </div>
    <div class="CellPrice">
        @Mvc.ContextHelper.Tradueix("Precio", "Preu", "Price", "Preço")
    </div>
    @If ViewData("DiscountExists") Then
        @<div class="CellDto">
            @Mvc.ContextHelper.Tradueix("Dto.", "Dte.", "Disc.")
        </div>
    End If
    <div class="CellAmt">
        @Mvc.ContextHelper.Tradueix("Importe", "Import", "Amount")
    </div>
</div>