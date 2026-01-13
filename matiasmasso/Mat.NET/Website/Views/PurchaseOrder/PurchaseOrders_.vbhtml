@ModelType  IEnumerable(Of DTOPurchaseOrder)
@Code
    
End Code



<div class="Grid">

        <span class="CellNum">
            @ContextHelper.Tradueix("Pedido", "Comanda", "Order")
        </span>
        <span class="CellIco">
        </span>
        <span class="CellFch">
            @ContextHelper.Tradueix("Fecha", "Data", "Date")
        </span>
        <span class="CellTxt">
            @ContextHelper.Tradueix("Concepto", "Concepte", "Concept")
        </span>
        <span class="CellAmt">
            @ContextHelper.Tradueix("Importe", "Import", "Amount")
        </span>


    @For Each item As DTOPurchaseOrder In Model
            @<span Class="CellNum">
                <a href = "@FEB.PurchaseOrder.Url(item)" >
                    @item.num
                </a>
            </span>
            @<span class="CellIco">
                @If item.DocFile IsNot Nothing Then
                    @<a href="@FEB.DocFile.DownloadUrl(item.DocFile, False)">
                        <img src="~/Media/Img/Ico/pdf.gif" />
                    </a>
                End If
            </span>
             @<span class="CellFch">
                 @Format(item.fch, "dd/MM/yy")
             </span>
             @<span class="CellTxt">
                 @item.concept
             </span>
            @<span class="CellAmt">
                @DTOAmt.CurFormatted(item.sumaDeImports())
            </span>
    Next
</div>


