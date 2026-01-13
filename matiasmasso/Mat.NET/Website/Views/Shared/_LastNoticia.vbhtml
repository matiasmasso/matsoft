@ModelType DTOUser
@Code
    Dim exs As New List(Of Exception)
    Dim oProduct As DTOProduct = ContextHelper.GetLastProductBrowsed()
    Dim oNoticia As DTONoticia = FEB.Noticias.LastNoticiaSync(exs, Model, oProduct)
End Code

@If oNoticia IsNot Nothing Then
    @<div class="MatBox180">
        <a href="@FEB.Noticia.UrlFriendly(oNoticia)">
            <div class="MatBoxHeaderOrange">
                @Html.Raw(ContextHelper.Tradueix("últimas noticias", "darreres noticies", "last news", "últimas notícias"))
            </div>
            <img src="@FEB.Noticia.UrlThumbnail(oNoticia)" alt="@oNoticia.Title.Tradueix(ContextHelper.lang())">
            <div class="MatBoxFooter">
                @oNoticia.Title.Tradueix(ContextHelper.lang())
            </div>
        </a>
    </div>
end if


