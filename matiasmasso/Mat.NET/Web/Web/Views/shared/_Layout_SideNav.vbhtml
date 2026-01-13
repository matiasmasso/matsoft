@Code
    Layout = "~/Views/shared/_Layout_2.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)
End Code

<div class="PageWrapperHorizontal">
    @If Not Mvc.ContextHelper.isTrimmed() Then
        @<div Class="LeftSideNav">
            @Html.Partial("_SideNav")
        </div>
    End If

    <div class="ContentColumn">
        @RenderBody()
    </div>

</div>


@Section Scripts
    @RenderSection("Scripts", False)
End Section

@Section Styles
    @RenderSection("Styles", False)

    <style>
        .PageWrapperHorizontal {
            justify-content: start;
        }

        .ContentColumn {
            width: 100%;
        }

        @@media(max-width:700px) {
            .LeftSideNav {
                display: none;
            }
        }
    </style>
End Section

@Section AdditionalMetaTags
    @RenderSection("AdditionalMetaTags", False)
End Section