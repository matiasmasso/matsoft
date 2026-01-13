@ModelType List(Of DTOProductSku)
@Code
    Dim lang = If(ViewBag.Lang Is Nothing, ContextHelper.Lang, ViewBag.Lang)
End Code

<div class="CollectionGallery">
    @For Each item In Model
        @<div class="CollectionItem">
            <a href="@item.UrlCanonicas.RelativeUrl(lang)" title="@DTOProductSku.FullNom(item)">
                <img src="@item.thumbnailUrl()" alt="@DTOProductSku.FullNom(item)" width="@DTOProductSku.THUMBNAILWIDTH" height="@DTOProductSku.THUMBNAILHEIGHT" />
                <div>
                    @item.nom.Tradueix(ContextHelper.Lang())
                </div>
            </a>
        </div>
    Next

</div>


