@ModelType DTOUser
@Code
    Dim exs As New List(Of Exception)
    Dim oProduct As DTOProduct = Mvc.ContextHelper.GetLastProductBrowsed()
    Dim oNoticia As DTONoticia = FEB2.Noticias.LastNoticiaSync(exs, Model, oProduct)
End Code

@If oNoticia IsNot Nothing Then
    @<div class="MatBox180">
        <a href="@FEB2.Noticia.UrlFriendly(oNoticia)">
            <div class="MatBoxHeaderOrange">
                @Html.Raw(Mvc.ContextHelper.Tradueix("últimas noticias", "darreres noticies", "last news", "últimas notícias"))
            </div>
            <img src="@FEB2.Noticia.UrlThumbnail(oNoticia)" alt="@oNoticia.Title.Tradueix(Mvc.ContextHelper.lang())">
            <div class="MatBoxFooter">
                @oNoticia.Title.Tradueix(Mvc.ContextHelper.lang())
            </div>
        </a>
    </div>
end if


