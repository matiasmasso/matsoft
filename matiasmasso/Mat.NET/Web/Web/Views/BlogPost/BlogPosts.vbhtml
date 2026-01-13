@ModelType List(Of DTOBlogPostModel)
@Code
    Layout = "~/Views/shared/_Layout_Blog.vbhtml"
    Dim lang = If(ViewBag.Lang, Mvc.ContextHelper.Lang)
End Code

<div class="PageWrapperVertical">
    <h3 class="Title">@ViewBag.Title</h3>


    <div class="Grid BlogPosts">
        @For Each item In Model
            @<a href="@item.Url().RelativeUrl(lang)">
                <div class="Item">
                    <div>@item.Fch.Date().ToString("dd/MM/yy")</div>
                    <img src="@item.ThumbnailUrl()" width="265" height="150" />
                    <div>@item.Title</div>
                </div>
            </a>
        Next
    </div>

</div>



@Section Styles
    <link href="~/Media/Css/Noticias.css" rel="stylesheet" />
    <link href="~/Media/Css/Feedback.css" rel="stylesheet" />
    <Style scoped>

        .BlogPosts .Item img {
            width: 100%;
        }
    </Style>

End Section
