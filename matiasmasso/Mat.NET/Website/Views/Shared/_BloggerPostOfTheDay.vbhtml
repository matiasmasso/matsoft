@ModelType DTOBloggerPost

<div class="MatBox180">
    <a href="@DTOBloggerPost.FullUrl(Model)" target="_blank">
        <div class="MatBoxHeaderGreen">
            @Html.Raw(ContextHelper.Tradueix("Post recomendado", "Post recomenat", "Recommended post"))
        </div>
        <img src="@FEB.Blogger.LogoUrl(Model.Blogger)" alt="@Model.Blogger.Title">
        
        <div class="MatBoxFooter">
            @Model.Title
        </div>
         
    </a>
</div>

