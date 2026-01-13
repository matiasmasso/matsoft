@ModelType DTOUser
@Code
    Dim exs As New List(Of Exception)

    Dim oProduct As DTOProduct = Mvc.ContextHelper.GetLastProductBrowsed()
    Dim oVideo As DTOYouTubeMovie = FEB2.YouTubeMovies.LastSync(oProduct, Model, Mvc.ContextHelper.lang(), exs)
End Code

<style scoped>

    .video-front {
        position: relative;
        display:block;
        width: 180px;
        height: 150px;
        padding:0;
        border:1px solid gray;
    }

    .iframe-underneath {
        width: 178px;
        height: 91px;
        position: absolute;
        left: 0px;
        top: 24px;
    }

</style>





<a class="video-front" href="/videos">
    <div class="MatBoxHeaderPurple" style="z-index:3;">
        videos
    </div>

         <iframe seamless class="iframe-underneath" width="178" height="91"
                src='https://www.youtube.com/embed/@Html.Raw(oVideo.YoutubeId & "?autoplay=0&showinfo=0&controls=0")'
                 frameborder="0"
                allowfullscreen></iframe>

    <div class="MatBoxFooter">
        @Mvc.ContextHelper.Tradueix("ver todos los videos disponibles", "veure tots els videos disponibles", "see all videos available", "Ver todos os vídeos disponíveis")
    </div>
</a>