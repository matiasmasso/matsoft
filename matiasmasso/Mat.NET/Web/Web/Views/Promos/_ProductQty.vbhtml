@ModelType DTO.DTOProductSku

<div data-sku="@Model.Guid.ToString">
    <div>
        <div class="SkuThumb">
            <a href="@BLL.BLLProductSku.Url(Model)" target="_blank">
                <img src="@BLL.BLLProductSku.ThumbnailUrl(Model)">
            </a>
        </div>

        <select>
            <option>0</option>
            <option selected>1</option>
            <option>2</option>
            <option>3</option>
            <option>4</option>
            <option>5</option>
            <option>6</option>
        </select>

        <a href="@BLL.BLLProductSku.Url(Model)" target="_blank">
            @Model.NomCurt
        </a>
    </div>


</div>