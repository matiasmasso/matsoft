@ModelType  IEnumerable(Of DTOInvoice)
@Code
    
End Code


<div class="Grid">

    <div class="RowHdr">
        <div class="CellNum">
            @Mvc.ContextHelper.Tradueix("Factura","Factura","Invoice")
        </div>
        <div class="CellIco">
        </div>
         <div class="CellFch">
              @Mvc.ContextHelper.Tradueix("Fecha","Data","Date")
         </div>
         <div class="CellAmt">
             @Mvc.ContextHelper.Tradueix("Importe","Import","Amount")
         </div>
    </div>


    @For Each item As DTOInvoice In Model
        @<div class="Row">
            <div class="CellNum">
                <a href="@FEB2.Invoice.Url(item)">
                     @item.Num
                </a>
            </div>
             <div class="CellIco">
                 @If item.DocFile IsNot Nothing Then
                    @<a href="@FEB2.DocFile.DownloadUrl(item.DocFile, False)">
                         <img src="~/Media/Img/Ico/pdf.gif" />
                    </a>
                 End If
             </div>
             <div class="CellFch">
                 @Format(item.Fch, "dd/MM/yy")
             </div>
             <div class="CellAmt">
                 @DTOAmt.CurFormatted(item.Total)
             </div>
        </div>
    Next
</div>


