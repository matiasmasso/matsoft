@ModelType List(Of DTOBloggerPost)

    @If Model.Count > 0 Then
        @<div class="BloggerPosts">
            @For Each item In Model
                @<p>
                    <a href="@item.Url" target="_blank" title="@item.Title">@item.Title</a>
                    <br />
                    <span class="authorship">
                        @(Mvc.ContextHelper.Tradueix("por", "per", "by") & " " & item.Blogger.Title & ", " & item.Fch.ToString("d", Mvc.ContextHelper.GetCultureInfo))
                    </span>
                </p>
            Next
        </div>
    End If
