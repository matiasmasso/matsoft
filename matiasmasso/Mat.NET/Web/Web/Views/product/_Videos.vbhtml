@If Model.Count = 0 Then

    @<div class="itemsCount">
        @Html.Raw(Mvc.ContextHelper.Tradueix("ningún video disponible en estos momentos", "Cap video disponible en aquests moments", "Sorry no movies available yet"))
    </div>

Else

    @<div class="itemsCount">
        @If Model.count = 1 Then
            @Html.Raw(Mvc.ContextHelper.Tradueix("Se ha encontrado el siguiente video de este producto", "S'ha trobat el següent video d'aquest producte", "See next video found for this product"))
        Else
            @Html.Raw(Mvc.ContextHelper.Tradueix("Se han encontrado " & Model.Count & " videos de este producto", "S'han trobat " & Model.Count & " videos d'aquest producte", Model.Count & " videos found for this product"))
        End If
    </div>

    @<div class="VideoGallery">
        @For Each item As DTOYouTubeMovie In Model
            @<div class="iframe-container">
                <iframe src='//www.youtube.com/embed/@item.YoutubeId?controls=0&showinfo=0&rel=0'
                        frameborder="0"
                        allowfullscreen></iframe>
            </div>
        Next
    </div>

End If
