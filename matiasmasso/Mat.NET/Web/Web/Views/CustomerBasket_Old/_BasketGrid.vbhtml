@Code
    Dim oWebsession As DTO.DTOSession = BLL.BLLSession.Find(Session("SessionId"))
End Code

<div class="Grid">
    <div class="RowHdr">
        <div class="CellNum">@oWebSession.Tradueix("Cant", "Quant", "Quant")</div>
        <div class="CellTxt">@oWebSession.Tradueix("Concepto", "Concepte", "Concept")</div>
        <div class="CellAmt">@oWebsession.Tradueix("Precio", "Preu", "Price", "Preço")</div>
        <div class="CellDto">@oWebSession.Tradueix("Dto", "Dte", "Dct")</div>
        <div class="CellAmt">@oWebSession.Tradueix("Importe", "Import", "Amount")</div>
        <div class="CellIco">&nbsp;</div>
    </div>
    <div class="Row" id="BasketTotal">
        <div class="CellNum">&nbsp;</div>
        <div class="CellTxt">total</div>
        <div class="CellAmt">&nbsp;</div>
        <div class="CellDto">&nbsp;</div>
        <div class="CellAmt">0</div>
        <div class="CellIco">&nbsp;</div>
    </div>
</div>


