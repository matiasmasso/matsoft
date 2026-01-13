@ModelType DTOIncidenciaQuery


@If Model.result.Count = 0 Then
    @<div>
        <img src="~/Media/Img/Ico/info_16.jpg" />&nbsp;
        @ContextHelper.Tradueix("No nos constan incidencias registradas", "No ens consten incidencies registrades", "No support incidences have been logged")
    </div>  Else
    @<div class="Grid">

    <span class="CellId">
        @ContextHelper.Tradueix("Registro", "Registre", "#Id")
    </span>
    <span class="CellFch">
        @ContextHelper.Tradueix("Fecha", "Data", "Date")
    </span>
    <span class="CellIco">
        @ContextHelper.Tradueix("Imágenes", "Images", "Images")
    </span>
    <span class="CellIco">
        @ContextHelper.Tradueix("Vídeos")
    </span>
    <span class="CellTxt">
        @ContextHelper.Tradueix("Producto", "Producte", "Product")
    </span>
    <span class="CellTxt">
        @ContextHelper.Tradueix("Nº de serie", "Nª de serie", "Serial number")
    </span>
    <span class="CellTxt">
        @ContextHelper.Tradueix("Fecha fabricación", "Data fabricació", "Manuf.date")
    </span>
    <span class="CellTxt">
        @ContextHelper.Tradueix("Concepto", "Concepte", "Concept")
    </span>
    <span class="CellFch">
        @ContextHelper.Tradueix("Cierre", "Tancament", "Closed")
    </span>


    @For Each item As DTOIncidencia In Model.result
        @<div class="Item" data-url='@FEB.UrlHelper.Factory(False, item.UrlSegment)'>
    <span Class="CellId">
        @item.num
    </span>
    <span class="CellFch">
        @item.fch.ToString("dd/MM/yy")
    </span>
    <span class='@IIf(item.existImages, "CellCamera", "CellIco")'>
    </span>
    <span class='@IIf(item.existVideos, "CellVideo", "CellIco")'>
    </span>
    <span class="CellTxt">
        @item.product.FullNom()
    </span>
    <span class="CellTxt">
        @item.serialNumber
    </span>
    <span class="CellTxt">
        @item.ManufactureDate
    </span>
    <span class="CellTxt">
        @If item.codi IsNot Nothing Then
            @<span>@item.codi.nom.Tradueix(ContextHelper.Lang)</span>
        End If
    </span>
    <span class="CellFch">
        @If item.fchClose = Nothing Then
            @<span>&nbsp;</span>
        Else
            @item.fchClose.ToString("dd/MM/yy")
        End If
    </span>
</div>
    Next
</div>

End If






