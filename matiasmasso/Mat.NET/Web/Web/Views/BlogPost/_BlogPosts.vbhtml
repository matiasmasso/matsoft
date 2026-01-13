@ModelType List(Of DTOBlogPostModel)
@Code
    Dim idx As Integer
    Dim lang = If(ViewBag.Lang, Mvc.ContextHelper.Lang)
End Code

<div class="Grid BlogPosts">
    @For Each item In Model
        idx += 1
        @<a href="@item.Url().RelativeUrl(lang)" class='@IIf(idx < 9, "FirstItems", "More")' title="@item.Title">
            <div class="Item">
                <div>@item.Fch.Date().ToString("dd/MM/yy")</div>
                <img src="@item.ThumbnailUrl()" alt="@item.Title" width="@DTOContent.THUMBNAIL_WIDTH" height="@DTOContent.THUMBNAIL_HEIGHT" />
                <div>@item.Title</div>
            </div>
        </a>
    Next
</div>

