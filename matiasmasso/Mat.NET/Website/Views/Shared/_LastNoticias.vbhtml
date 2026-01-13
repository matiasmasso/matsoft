@Code
    Dim exs As New List(Of Exception)
    Dim oUser = ContextHelper.FindUserSync()
    Dim oNoticias = FEB.Noticias.LastNoticiasSync(exs, oUser)
End Code

<style>
    .news-wrap {
        margin-bottom:5px;
    }

</style>

@For Each oNoticia As DTONoticia In oNoticias.Where(Function(x) x.Fch <= DTO.GlobalVariables.Today()).Take(5)

    @<div class="MatBox180 news-wrap">
         <a href="@FEB.Noticia.UrlFriendly(oNoticia, False)">
             <div class="MatBoxHeaderOrange">
                 @oNoticia.Fch.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"))
             </div>
             <img src="@FEB.Noticia.UrlThumbnail(oNoticia)">
             <div class="MatBoxFooter">
                 @oNoticia.Title.Tradueix(ContextHelper.Lang())
             </div>
        </a>
</div>

Next
    
<div class="readmore">
    <a href="/news">
        @ContextHelper.Tradueix("ver todas las noticias...", "veure totes les noticies...", "see all news...")
    </a>
</div>
