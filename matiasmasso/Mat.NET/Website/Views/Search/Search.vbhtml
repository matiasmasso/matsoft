@ModelType DTOSearchRequest
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, ContextHelper.Lang, ViewBag.Lang)
End Code

<div class="Caption">
    @DTOSearchRequest.FoundCaption(Model)
</div>

@For Each oSearchResult As DTOSearchRequest.Result In Model.Results
    If oSearchResult.CanonicalUrl Is Nothing Then
        @<a href="@oSearchResult.Url" class="Truncate">
            @oSearchResult.Caption
        </a>
    Else
        @<a href="@oSearchResult.CanonicalUrl.RelativeUrl(ContextHelper.Lang)" class="Truncate">
            @oSearchResult.Caption
        </a>
    End If
Next


@Section Styles
    <style>

        .ContentColumn .Caption {
            padding: 0 0 20px;
        }

        .ContentColumn a {
            display: block;
        }

            .ContentColumn a:hover {
                color: #167ac6;
            }

        @@media(max-width: 500px) {
            .ContentColumn a {
                padding: 20px 0;
                border-top: 1px solid gray;
            }
        }
    </style>
End Section
