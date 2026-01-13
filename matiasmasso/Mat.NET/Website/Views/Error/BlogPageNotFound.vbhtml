@ModelType DTOWebErr
@Code
    Layout = "~/Views/Shared/_Layout_Blog.vbhtml"
    Dim lang = ContextHelper.Lang
End Code



<div class="PageWrapper">
    <div class="LeftSide">

        <h1 class="Title">@ViewBag.Title</h1>

        <p>
            @lang.Tradueix("Lamentamos informarle que no hemos podido encontrar la página que nos solicita.",
                                 "Lamentem informar que no hem trobat la pàgina que ens demana.",
                                 "We are sorry we could not find the page you are requesting",
                                 "Lamentamos informar que não podemos encontrar a página solicitada.")
        </p>
        <p>
            <a href="@Model.Url">@Model.Url</a>
        </p>
        <p>
            @lang.Tradueix("Rogamos disculpe las molestias.", "Disculpi les molesties.", "Sorry for any inconveniences.", "Rogamos aceite as nossas desculpas pelo inconveniente.")
        </p>

    </div>

    <div Class="RightSide">

        @Html.Partial("_SearchBox")
        @Html.Partial("_SubscribeMe")


        <span class="LastPostsTitle">@ContextHelper.Tradueix("Últimas publicaciones:", "Darreres publicacions:", "Last posts:", "Últimas publicações:")</span>

        <div id="LastBlogPosts"></div>

        <div class="readmore">
            <a href="/blog/posts" class="seeAllPosts">@ContextHelper.Tradueix("Ver todas las publicaciones", "Veure totes les publicacions", "See all posts", "Ver todas as publicações")</a>
        </div>
    </div>

</div>


@Section Scripts
    <script src="~/Media/js/Comments.js"></script>
    <script src="~/Scripts/SignInOrSignUp.js"></script>
    <script>
        var returnUrl = '@Model.Url()';

        $(document).ready(function () {
            LoadLastBlogPosts();
        })

        function LoadLastBlogPosts() {
            var postsDiv = $('#LastBlogPosts');
            var url = '/blog/lastpostsPartial';
            postsDiv.load(url, function () { })
        }

        $(document).on('click', '#SubscribeMe a.Submit', function (event) {
            $(this).hide();
            $('#SubscribeMe').append(spinner);
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

