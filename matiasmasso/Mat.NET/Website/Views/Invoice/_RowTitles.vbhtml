@Code
    
End Code

<div class="RowHdr">
    <div class="CellTxt">
        @ContextHelper.Tradueix("Concepto", "Concepte", "Concept")
    </div>
    <div class="CellNum">
        @ContextHelper.Tradueix("Cantidad", "Quantitat", "Quantity")
    </div>
    <div class="CellPrice">
        @ContextHelper.Tradueix("Precio", "Preu", "Price", "Preço")
    </div>
    @If ViewData("DiscountExists") Then
        @<div class="CellDto">
            @ContextHelper.Tradueix("Dto.", "Dte.", "Disc.")
        </div>
    End If
    <div class="CellAmt">
        @ContextHelper.Tradueix("Importe", "Import", "Amount")
    </div>
</div>