@ModelType List(Of DTO.DTOProductSku)



<style>
    .hScroll {
        display: flex;
        overflow-x: auto;
    }

        .hScroll::-webkit-scrollbar {
            display: none;
        }
</style>


<div class="hScroll">
    @For Each oSku In Model
        @<a href="@FEBL.ProductSku.Url(oSku)" target="_parent">
    <!--title="@oSku.nomLlarg"-->
    <img src="@FEBL.ProductSku.ThumbnailUrl(oSku)" alt="@oSku.nomCurt" />
</a>
    Next
</div>

