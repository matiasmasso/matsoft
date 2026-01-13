@ModelType IEnumerable(Of DTOCliApertura)
@Code
    
End Code



<div class="Grid">

    <div class="RowHdr">
        <div class="CellFch">
            @Mvc.ContextHelper.Tradueix("Fecha", "Data", "Date")
        </div>
        <div class="CellZona">
            @Mvc.ContextHelper.Tradueix("Zona", "Zona", "Zone")
        </div>
        <div class="CellLocation">
            @Mvc.ContextHelper.Tradueix("Población", "Població", "Location")
        </div>
        <div class="CellNom">
            @Mvc.ContextHelper.Tradueix("Nombre", "Nom", "Name")
        </div>
        <div class="CellStatus">
            @Mvc.ContextHelper.Tradueix("Status", "Status", "Status")
        </div>
    </div>


@For Each item As DTOCliApertura In Model
    @<div class="Row" data-url="@item.Url()">
        <div class="CellFch">
            <a href="/apertura/@item.Guid.ToString()">
                @Format(item.FchCreated, "dd/MM/yy")
            </a>
        </div>
        <div class="CellZona">
            <a href="/apertura/@item.Guid.ToString()">
                @If item.Zona IsNot Nothing Then
                    @<span>@item.Zona.Nom</span>
                End If
                </a>
        </div>
        <div class="CellLocation">
            <a href="/apertura/@item.Guid.ToString()">
                @item.Cit
                </a>
        </div>
        <div class="CellNom truncate">
            <a href="/apertura/@item.Guid.ToString()">
                @item.Nom
                </a>
        </div>
        <div class="CellStatus">
            <a href="/apertura/@item.Guid.ToString()">
                @DTOCliApertura.StatusLabel(item.CodTancament, Mvc.ContextHelper.lang())
                </a>
        </div>
    </div>  
Next
</div>