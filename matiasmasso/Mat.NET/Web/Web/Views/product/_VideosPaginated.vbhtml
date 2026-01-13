@ModelType List(Of DTOYouTubeMovie)
@Code
    
End Code

<div id="Videos">
    @If Model.Count = 0 Then
        @<div class="novideo">
            @Html.Raw(Mvc.ContextHelper.Tradueix("ningún video disponible en estos momentos", "Cap video disponible en aquests moments", "Sorry no movies available yet"))
        </div>
    Else
        For Each item As DTOYouTubeMovie In Model
            @<iframe width="293" height="200" src='//www.youtube.com/embed/@item.YoutubeId?controls=0&showinfo=0&rel=0'
                frameborder="0"
                allowfullscreen></iframe>
        Next
    End If
</div>

