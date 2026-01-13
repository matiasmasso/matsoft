@Code
    Dim exs As New List(Of Exception)
    Dim oUser = Mvc.ContextHelper.FindUserSync()
    Dim oNoticias = FEB2.Noticias.LastNoticiasSync(exs, oUser)
End Code

<style>
    .news-wrap {
        margin-bottom:5px;
    }

</style>

@For Each oNoticia As DTONoticia In oNoticias.Where(Function(x) x.Fch <= Today).Take(5)

    @<div class="MatBox180 news-wrap">
         <a href="@FEB2.Noticia.UrlFriendly(oNoticia, False)">
             <div class="MatBoxHeaderOrange">
                 @oNoticia.fch.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"))
             </div>
             <img src="@FEB2.Noticia.UrlThumbnail(oNoticia)">
             <div class="MatBoxFooter">
                 @oNoticia.Title.Tradueix(Mvc.ContextHelper.lang())
             </div>
        </a>
</div>

Next
    
<div class="readmore">
    <a href="/news">
        @Mvc.ContextHelper.Tradueix("ver todas las noticias...", "veure totes les noticies...", "see all news...")
    </a>
</div>
