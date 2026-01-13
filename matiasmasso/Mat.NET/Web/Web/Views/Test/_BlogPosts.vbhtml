@ModelType List(Of DTOBlog2PostModel)
@Code
    Dim idx As Integer
End Code

<div class="Grid">
    @For Each item In Model
        idx += 1
        @<a href="@item.Url()" class='@IIf(idx < 9, "FirstItems", "More")'>
            <div class="Item">
                <div>@item.Fch.Date().ToString("d", Mvc.ContextHelper.GetCultureInfo())</div>
                <img src="@item.ThumbnailUrl()" />
                <div>@item.Title</div>
            </div>
        </a>
    Next
</div>
