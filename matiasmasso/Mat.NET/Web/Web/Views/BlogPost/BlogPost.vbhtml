@ModelType DTO.DTOBlogPostModel
@Code
    Layout = "~/Views/shared/_Layout_Blog.vbhtml"
    Dim lang = If(ViewBag.Lang, Mvc.ContextHelper.Lang)
End Code


@Section AdditionalMetaTags
    <!--------------------Facebook metatags------------------------ -->
    <meta property="og:title" content="@Html.Raw(Model.Title)" />
    @If Model.FbImg > "" Then
        @<meta property="og:image:url" content="@Model.FbImg" />
    End If
    <meta property="og:type" content="article" />
    <meta property="og:url" content="@Model.FbUrl(Mvc.ContextHelper.Domain)" />
    @If Model.Excerpt > "" Then
        @<meta Property="og:description" content="@Html.Raw(Model.Excerpt)" />
    End If
    <!------------------------------------------------------------- -->

End Section

<div class="PageWrapper">
    <div class="LeftSide">
        <h1 class="Title">@ViewBag.Title</h1>

        <div class="Contingut">
            @Html.Raw(MatHelperStd.TextHelper.Html(Model.Text))
        </div>

        <div id="Comments">
            @Html.Partial("_CommentsTree", Model.Comments)
        </div>

    </div>

    @If Not Mvc.ContextHelper.isTrimmed() Then
        @<div Class="RightSide">

            @Html.Partial("_SearchBox")
            @Html.Partial("_SubscribeMe")


            <span class="LastPostsTitle">@Mvc.ContextHelper.Tradueix("Últimas publicaciones:", "Darreres publicacions:", "Last posts:", "Últimas publicações:")</span>

            @Html.Partial("~/Views/BlogPost/_BlogPosts.vbhtml", Model.Posts)

            <div class="readmore">
                <a href="/blog/posts" class="seeAllPosts">@Mvc.ContextHelper.Tradueix("Ver todas las publicaciones", "Veure totes les publicacions", "See all posts", "Ver todas as publicações")</a>
            </div>
        </div>
    End If

</div>


@Section Scripts
    <script src="~/Media/js/Comments.js"></script>
    <script src="~/Scripts/SignInOrSignUp.js"></script>
    <script>
        var returnUrl = '@Model.Url().RelativeUrl(lang)';

        $(document).ready(function () {
            //loadComments($('#Comments'), '@@Model.Guid.ToString()',3,0);
        })

        $(document).on('click', '#SubscribeMe a.Submit', function (event) {
            $(this).hide();
            $('#SubscribeMe').append(spinner);
         });
    </script>
End Section

@Section Styles
    <script src="https://kit.fontawesome.com/05a6a08892.js" crossorigin="anonymous"></script>
    <link href="~/Media/Css/Plugin.css" rel="stylesheet" />
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

        .RightSide .readmore {
            text-align: right;
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

        .seeAllPosts {
            justify-self: end;
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
