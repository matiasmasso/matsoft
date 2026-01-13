@ModelType List(Of DTONoticia)
@Code
    Dim lang = If(ViewBag.Lang, ContextHelper.Lang)
    Dim idx As Integer
End Code

<div class="Grid">
    @For Each item In Model
        idx += 1
        @<a href="@item.Url.RelativeUrl(lang)" class='@IIf(idx < 9, "FirstItems", "More")' title="@item.Title.Tradueix(lang)">
            <div class="Item">
                <div>@item.Fch.Date().ToString("dd/MM/yy")</div>
                <img src="@item.ThumbnailUrl()" alt="@item.Title.Tradueix(lang)" width="@DTOContent.THUMBNAIL_WIDTH" height="@DTOContent.THUMBNAIL_HEIGHT" />
                <div>@item.Title.Tradueix(lang)</div>
            </div>
        </a>
    Next
</div>
