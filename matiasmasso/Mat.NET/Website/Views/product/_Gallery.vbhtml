@ModelType List(Of List(Of DTOMediaResource))

<div class="ImgGallery">
    @If Model.Count = 0 Then
        @<div>
            @ContextHelper.Tradueix("No se han encontrado imágenes disponibles", "No s'han trobat imatges disponibles", "No images available", "Não há imagens disponíveis")
        </div> Else

        For Each oSection In Model

            @<div Class="CategoryHeader">
                @DTOMediaResource.CodTitle(oSection.First(), ContextHelper.Lang())
            </div>

            @<div Class="CategoryBody" data-cod="@oSection.First.Cod">
                @For Each item As DTOMediaResource In oSection
                    @<div class="Item">
                        <a href="@FEB.MediaResource.Url(item)">
                            <img src="@FEB.MediaResource.ThumbnailUrl(item)" />
                            <div class="Truncate">
                                @item.Nom
                                <br />
                                @item.Features()
                            </div>
                        </a>
                    </div>
                Next
            </div>

            @<div Class="ShowMore" data-cod="@oSection.First.Cod">
                <a href="#" data-cod="@oSection.First.Cod">mostrar más</a>
            </div>
            @<div Class="ShowLess" hidden="hidden" data-cod="@oSection.First.Cod">
                <a href="#" data-cod="@oSection.First.Cod">mostrar menos</a>
            </div>
        Next
    End If
</div>
