@ModelType DTOBlogPostModel
@Code
    Layout = "~/Views/shared/_Layout_Blog.vbhtml"
    Dim lang = If(ViewBag.Lang, Mvc.ContextHelper.Lang)
End Code

<div class="PageWrapper">
    <div class="LeftSide">
        <h3 class="Title">@ViewBag.Title</h3>

        <div class="Contingut">
            @Html.Raw(Model.Text)
        </div>

        <div id="Comments"></div>

    </div>

    <div Class="RightSide">

        @Html.Partial("_SearchBox")
        @Html.Partial("_SubscribeMe")


        <span class="LastPostsTitle">@Mvc.ContextHelper.Tradueix("Últimas publicaciones:", "Darreres publicacions:", "Last posts:", "Últimas publicações:")</span>

        @Html.Partial("~/Views/BlogPost/_BlogPosts.vbhtml", Model.Posts)
    </div>

</div>


@Section Scripts
    <script src="~/Media/js/Comments.js"></script>
    <script src="~/Scripts/SignInOrSignUp.js"></script>
    <script>
        var returnUrl = '@Model.Url()';

        $(document).ready(function () {
            loadComments($('#Comments'),'@Model.Guid.ToString()',@CInt(DTOPostComment.ParentSources.Blog));
        })

        $(document).on('click', '#SubscribeMe a.Submit', function (event) {
            $(this).hide();
            $('#SubscribeMe').append(spinner)
         });
    </script>
End Section

@Section Styles
    <link href="~/Media/Css/Noticias.css" rel="stylesheet" />
    <link href="~/Media/Css/Feedback.css" rel="stylesheet" />
    <link href="~/Media/Css/Comments.css" rel="stylesheet" />
    <!--<link href="~/Media/Css/Comments.css" rel="stylesheet" />-->
    <link href="~/Media/Css/SISU.css" rel="stylesheet" />
    <Style scoped>
        .PageWrapper {
            max-width: 950px !important;
            flex-direction: row !important;
            justify-content: space-between;
        }

            .PageWrapper .LeftSide {
                max-width: 600px;
            }

            .PageWrapper .RightSide {
                max-width: 300px;
            }

        h3.Title {
            margin-top: 0;
        }

        .size-full {
            width: 100%;
            height: auto;
        }


        .readmore {
            margin-top: 20px;
        }

        .psubtitle {
            margin: 0 0 10px 0;
            padding: 0 5px 10px 0;
            font-Style: italic;
            color: darkgray;
        }

        .red {
            color: red;
        }

        .BlogPosts .Item img {
            width: 100%;
        }

        @@media(max-width: 900px) {

            .PageWrapper {
                flex-direction: column !important;
                align-items: center;
            }

                .PageWrapper .RightSide {
                    max-width: 100%;
                }
        }
    </Style>

End Section
