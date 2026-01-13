@Code
    Dim exs As New List(Of Exception)

    Dim oUser = Mvc.ContextHelper.FindUserSync()
    Dim oProduct As DTOProduct = Mvc.ContextHelper.GetLastProductBrowsed()
    Dim oNoticia As DTONoticia = FEB2.Noticias.LastNoticiaSync(exs, oUser, oProduct)
End Code

@If oNoticia IsNot Nothing Then
    @<div >
         <a href="@FEB2.Noticia.UrlFriendly(oNoticia)">

             <div class="MobileHomeTitle">
                 @oNoticia.Title.Tradueix(Mvc.ContextHelper.lang())
             </div>

             <div class="MobileHomeThumbnail">
                 <img src="@FEB2.Noticia.UrlThumbnail(oNoticia)" alt="@oNoticia.Title.Tradueix(Mvc.ContextHelper.lang())" />
             </div>

         </a>
           
            <div class="MobileHomeExcerpt">
                @Html.Raw(oNoticia.Excerpt.Tradueix(Mvc.ContextHelper.lang()))
            </div>

    </div>  
End If


