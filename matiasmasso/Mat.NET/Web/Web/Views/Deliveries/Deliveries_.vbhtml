@Code
    Dim lang = Mvc.ContextHelper.Lang
    Dim visibleCount As Integer = 10
End Code



<div class="Grid">

    <div class="RowHdr">
        <div class="CellNum">
            @Mvc.ContextHelper.Tradueix("albarán", "albará", "delivery")
        </div>
        <div class="CellIco">
        </div>
        <div class="CellFch">
            @Mvc.ContextHelper.Tradueix("Fecha", "Data", "Date")
        </div>
        <div class="CellAmt">
            @Mvc.ContextHelper.Tradueix("Importe", "Import", "Amount")
        </div>
        <div class="CellTxt">
            @Mvc.ContextHelper.Tradueix("factura", "factura", "invoice")
        </div>
        <div class="CellTxt">
            &nbsp;
        </div>
    </div>


    @For Each item As DTODelivery In Model
        @<div class='Row @IIf(Model.indexOf(item) > visibleCount, "Closed", "")'>
            <div class="CellNum">
                <a href="@FEB2.Delivery.Url(item)">
                    @item.Id
                </a>
            </div>
            <div class="CellIco">
                <a href="@FEB2.Delivery.PdfUrl(item, False)">
                    <img src="~/Media/Img/Ico/pdf.gif" />
                </a>
            </div>
            <div class="CellFch">
                @Format(item.Fch, "dd/MM/yy")
            </div>
            <div class="CellAmt">
                @DTOAmt.CurFormatted(item.Import)
            </div>
            <div class="CellTxt">
                @If item.Invoice IsNot Nothing Then
                    @<a href="@FEB2.Invoice.Url(item.Invoice)">
                        @DTODelivery.invoiceText(item, Mvc.ContextHelper.Lang())
                    </a>
                End If
            </div>
            <div class="CellTxt">
                <a href="@item.followUpUrl(lang, False)" class="Tracking">
                    @lang.Tradueix("Seguimiento", "Seguiment", "Tracking")
                </a>
            </div>
        </div>
    Next
</div>

@If Model.Count > visibleCount Then
    @<div id="ShowMore"><a href="#" >@lang.Tradueix("mostrar todo", "mostrar tot", "show all")</a></div>
End If





