@Code
    Dim oLang As DTOLang = Mvc.ContextHelper.lang()
    Dim exs As New List(Of Exception)
    Dim oBlogPost = FEB2.BlogPosts.LastPostSync(oLang, exs)
End Code

@If oBlogPost IsNot Nothing Then

        @<div Class="MatBox180">
            <a href = "@oBlogPost.VirtualPath" >
                <div class="MatBoxHeaderGreen">
                    blog
                </div>
                <img src = "@oBlogPost.FeaturedImageUrl" alt="@oBlogPost.Title">
                <div Class="MatBoxFooter">
                    @oBlogPost.Title
                </div>
            </a>
    </div>  

End If


