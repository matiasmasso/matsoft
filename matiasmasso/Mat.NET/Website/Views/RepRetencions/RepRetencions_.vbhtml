@ModelType  IEnumerable(Of DTORepCertRetencio)
@Code
    
End Code



<div class="Grid">

    <div class="RowHdr">
        <div class="CellFch">
            @ContextHelper.Tradueix("Fecha", "Data", "Date")
        </div>
        <div class="CellIco">
        </div>
        <div class="CellAmt">
            @ContextHelper.Tradueix("Base Imponible", "Base Imponible", "Taxable amount")
        </div>
        <div class="CellAmt">
            @ContextHelper.Tradueix("IVA", "IVA", "VAT")
        </div>
        <div class="CellAmt">
            @ContextHelper.Tradueix("IRPF", "IRPF", "IRPF")
        </div>
        <div class="CellAmt">
            @ContextHelper.Tradueix("Líquido", "Liquid", "Cash")
        </div>
    </div>


    @For Each item As DTORepCertRetencio In Model
        @<div class="Row">
            <div class="CellFch">
                @Format(item.Fch, "dd/MM/yy")
            </div>
            <div class="CellIco">
                    <a href="@FEB.RepCertRetencio.CertFactoryUrl(item)">
                        <img src="~/Media/Img/Ico/pdf.gif" />
                    </a>
            </div>
            <div class="CellAmt">
                @DTOAmt.CurFormatted(DTORepCertRetencio.BaseImponible(item))
            </div>
            <div class="CellAmt">
                @DTOAmt.CurFormatted(DTORepCertRetencio.IVA(item))
            </div>
             <div class="CellAmt">
                 @DTOAmt.CurFormatted(DTORepCertRetencio.IRPF(item))
             </div>
             <div class="CellAmt">
                 @DTOAmt.CurFormatted(DTORepCertRetencio.Liquid(item))
             </div>
        </div>
    Next
</div>


