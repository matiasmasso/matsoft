@ModelType List(Of DTO.DTOProductBrand)
@Code
    Dim oWebsession As DTO.DTOSession = BLL.BLLSession.Find(Session("SessionId"))
End Code

<div class="ProductThumbnail">
    <img />
</div>

<div class="ProductSelectors">
    <div id="ProductBrand">
        <select>
            <option selected>@oWebSession.Tradueix("(seleccionar marca)", "(sel.leccionar marca)", "(select a brand)")</option>
            @For Each oBrand As DTO.DTOProductBrand In Model
                @<option value="@oBrand.Guid.ToString">@oBrand.Nom</option>
            Next
        </select>
    </div>

    <div id="ProductCategory">
        <select disabled="disabled">
            <option value="" selected>@oWebSession.Tradueix("(seleccionar categoría)", "(sel.leccionar categoria)", "(select a category)")</option>
        </select>
    </div>

    <div id="ProductSku">
        <select disabled="disabled">
            <option value="" selected>@oWebSession.Tradueix("(seleccionar producto)", "(sel.leccionar producte)", "(select a product)")</option>
        </select>
    </div>

    <div id="QtySelection">
        <input type="number" id="Qty" />
        <input type="button" id="AddRow" value='@oWebSession.Tradueix("añadir", "afegir", "add")' />
    </div>

</div>

