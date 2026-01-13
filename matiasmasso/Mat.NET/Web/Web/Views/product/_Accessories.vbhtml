@ModelType List(Of DTOProductSku)

@If Model.Count = 0 Then

    @<div class="ItemsCount">
        @Html.Raw(Mvc.ContextHelper.Tradueix("ningún accesorio disponible en estos momentos", "Cap accessori disponible en aquests moments", "Sorry no accessories available yet"))
    </div>

Else

    @<div class="ItemsCount">
        @If Model.count = 1 Then
            @Html.Raw(Mvc.ContextHelper.Tradueix("Se ha encontrado el siguiente accesorio para este producto", "S'ha trobat el següent accessori d'aquest producte", "See next accessory found for this product"))
        Else
            @Html.Raw(Mvc.ContextHelper.Tradueix("Se han encontrado " & Model.Count & " accesorios de este producto", "S'han trobat " & Model.Count & " accessoris d'aquest producte", Model.Count & " accessories found for this product"))
        End If
    </div>

    @<div class="Accessories">
    @For Each item As DTOProductSku In Model
        Dim nom As String = ""
        If item.includeCategoryWithAccessoryNom Then
            nom = item.Category.Nom.Tradueix(Mvc.ContextHelper.Lang)
        End If
        nom = nom & " " & item.Nom.Tradueix(Mvc.ContextHelper.Lang())

        @<a href="@item.getUrl()">
            <div class="Item">

                <div class="Thumbnail">
                    <img src="@item.thumbnailUrl()" width="@DTOProductSku.THUMBNAILWIDTH" height="@DTOProductSku.THUMBNAILHEIGHT" />
                </div>

                <div class="Text">
                    <div class="Nom">@nom</div>
                    <div class="Excerpt">
                        @Html.Raw(FEB2.Product.Excerpt(item, Mvc.ContextHelper.Lang()))
                    </div>
                </div>

            </div>
        </a>
    Next
</div>

End If



