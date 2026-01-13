@ModelType DTOSearchRequest
@Code
    Layout = "~/Views/Shared/_Layout_Blog.vbhtml"
End Code

<div class="PageWrapperVertical">
    <div class="Caption">
        @DTOSearchRequest.FoundCaption(Model)
    </div>

    @For Each oSearchResult As DTOSearchRequest.Result In Model.Results
        @<a href = "@oSearchResult.Url" class="Truncate" >
            @oSearchResult.Caption
        </a>
    Next

</div>


    @Section Styles
        <style>
            .PageWrapperVertical .Caption {
                padding: 20px 0;
            }

            .PageWrapperVertical a {
                display: block;
                padding: 4px 7px 2px 4px;
            }
                .PageWrapperVertical a:hover {
                    color: #167ac6;
                }

                    @@media(max-width: 500px) {
                        .PageWrapperVertical a {
                            padding: 20px 0;
                            border-top: 1px solid gray;
                        }
                    }
        </style>
    End Section
