@ModelType DTOCondicio
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, ContextHelper.Lang, ViewBag.Lang)


    Dim iOrd As Integer
End Code

<h1>@ViewBag.Title</h1>

@Html.Raw(Model.Excerpt.Html(lang))

@For Each item As DTOCondicio.Capitol In Model.Capitols
    iOrd += 1

    @<div class="epigraf" id="@item.Ord">@Html.Raw(iOrd.ToString & ".- ") @Html.Raw(item.Caption.Tradueix(lang)) </div>
    @<p>
        @Html.Raw(item.Text.Html(lang))
    </p>
Next

@Section Styles
    <style scoped>
        .epigraf {
            margin-top: 2.5em;
            font-size: 1.1em;
            color: #666666;
        }
    </style>
End Section