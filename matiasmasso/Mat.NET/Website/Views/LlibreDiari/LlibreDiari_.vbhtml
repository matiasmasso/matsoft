@ModelType  IEnumerable(Of DTOCca)
@Code
    
End Code



<div class="Grid">

    <div class="RowHdr">
        <div class="CellShortTxt">
            @ContextHelper.Tradueix("Asiento", "Assentament", "Log")
        </div>
        <div class="CellFch">
            @ContextHelper.Tradueix("Fecha", "Data", "Date")
        </div>
        <div class="CellIco">
            &nbsp;
        </div>
        <div class="CellTxt">
            @ContextHelper.Tradueix("Concepto", "Concepte", "Concept")
        </div>
    </div>


    @For Each item As DTOCca In Model
        @<div class="Row SelectableRow" data-url="@FEB.Cca.Url(item)">
            <div class="CellShortTxt">
                @item.Id
            </div>
             <div class="CellFch">
                 @item.fch.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"))
             </div>
             <div class='@IIf(item.DocFile Is Nothing, "CellIco", "CellPdf")'>
                 &nbsp;
             </div>
             <div class="CellTxt">
                 @item.Concept
             </div>
        </div>
    Next
</div>


