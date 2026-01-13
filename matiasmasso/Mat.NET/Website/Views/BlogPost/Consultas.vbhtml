@ModelType BlogConsultaModel
@Code
    Layout = "~/Views/shared/_Layout_Blog.vbhtml"
    Dim lang = If(ViewBag.Lang, ContextHelper.Lang)
End Code

<div class="PageWrapperHorizontal">

    <div class="MainContent">
        <h3 class="Title">@ViewBag.Title</h3>

        <div class="Info">
            @ContextHelper.Tradueix("Escribe aquí tu Consulta:", "Escriu aquí la teva consulta:", "Write here your request:", "Deixa-nos aquí a tua consulta:")
        </div>

        <!--
            <div class="ConsultaForm CommentThreads"
                 data-target="@@DTO.DTOContent.Wellknown(DTOContent.Wellknowns.consultasBlog).Guid.ToString()"
                 data-targetsrc="@@CInt(DTO.DTOPostComment.ParentSources.Blog)">
                @@Html.Partial("_CommentForm")
            </div>
    -->
        <div id="Comments">
            @Html.Partial("_CommentsTree", Model.Comments)
        </div>

    </div>

    <div Class="Aside">

        <span class="LastPostsTitle">@ContextHelper.Tradueix("Últimas publicaciones:", "Darreres publicacions:", "Last posts:", "Últimas publicações:")</span>
        <div id="LastBlogPosts"></div>
        <a href="/blog/posts" class="seeAllPosts">@ContextHelper.Tradueix("Ver todas las publicaciones", "Veure totes les publicacions", "See all posts", "Ver todas as publicações")</a>

    </div>

</div>


@Section Scripts
    <script src="~/Media/js/Comments.js"></script>
    <script>
        $(document).ready(function () {
            loadLastBlogPosts();
        })

        function loadLastBlogPosts() {
            var postsDiv = $('#LastBlogPosts');
            var url = '/blog/lastpostsPartial';
            postsDiv.load(url, function () { })
        }


    </script>
End Section

@Section Styles
    <link href="~/Media/Css/Noticias.css" rel="stylesheet" />
    <link href="~/Media/Css/Feedback.css" rel="stylesheet" />
    <link href="~/Media/Css/Comments.css" rel="stylesheet" />
    <!--<link href="~/Media/Css/Comments.css" rel="stylesheet" />-->
    <Style scoped>
        .Aside {
            max-width: 300px;
        }

        .MainContent {
            width: 100%;
            max-width:600px;
        }

        textarea {
            box-sizing: border-box;
            width:100%;
            min-width: 300px;
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

        .Info {
            padding: 20px 0 10px;
        }


        @@media(max-width: 700px) {

            .PageWrapperHorizontal {
                flex-direction: column !important;
                align-items: center;
            }

                .PageWrapperHorizontal .Aside {
                    max-width: 100%;
                }
        }
    </Style>

End Section
