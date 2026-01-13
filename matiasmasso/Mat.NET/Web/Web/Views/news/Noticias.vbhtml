@ModelType List(Of DTONoticia)
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)
End Code

<h1 class="Title">@ViewBag.Title</h1>

@Html.Partial("_Noticias", Model)


@Section Scripts
    <script src="~/Media/js/Noticias.js"></script>
End Section

@Section Styles
    <link href="~/Media/Css/Noticias.css" rel="stylesheet" />

    <style scoped>

        .ContentColumn {
            width: 100%;
        }
    </style>
End Section

@Section AdditionalMetaTags
    <link rel="alternate" href="https://www.matiasmasso.es/es/noticias" hreflang="es" />
    <link rel="alternate" href="https://www.matiasmasso.es/ca/noticies" hreflang="ca" />
    <link rel="alternate" href="https://www.matiasmasso.pt/pt/noticias" hreflang="pt" />

    @If Mvc.ContextHelper.TopLevelDomain = "pt" Then
        @<link rel="alternate" href="https://www.matiasmasso.pt/noticias" hreflang="x-default" />
        @<link rel='canonical' href="https://www.matiasmasso.pt/noticias" />
    Else
        @<link rel="alternate" href="https://www.matiasmasso.es/noticias" hreflang="x-default" />
        @<link rel='canonical' href="https://www.matiasmasso.es/noticias" />
    End If
End Section