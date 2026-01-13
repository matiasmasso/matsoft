@ModelType DTOSoftwareDownload.Collection
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, ContextHelper.Lang, ViewBag.Lang)
End Code

<h1>@ViewBag.Title</h1>

@For Each item In Model
    @<a href="@item.Url">
        <div class="Title">@item.Title.Tradueix(lang)</div>
        <div class="Excerpt">
            @item.Excerpt.Tradueix(lang)
        </div>
    </a>
Next

@Section Styles
    <style scoped>
        .Title {
            font-weight: 600;
            padding: 20px 7px 2px 0;
        }

        .Excerpt {
            padding: 4px 7px 2px 20px;
        }
    </style>
End Section 