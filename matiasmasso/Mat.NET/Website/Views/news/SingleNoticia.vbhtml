@ModelType DTONoticia
@Code
    Dim exs As New List(Of Exception)
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, ContextHelper.Lang, ViewBag.Lang)
    '
    Dim oNoticia As DTONoticia = Model

    Dim sUserGuid As String = Request.QueryString("UserGuid")
    Dim oUser As DTOUser = Nothing
    If MatHelperStd.GuidHelper.IsGuid(sUserGuid) Then
        oUser = New DTOUser(New Guid(sUserGuid))
    End If

    'Dim finalText = DTONoticia.Filtered(oNoticia, oUser).Tradueix(oLang)
    Dim finalText = DTONoticia.Filtered(oNoticia, oUser).Html(lang)
    Dim fbImg = MatHelperStd.TextHelper.FbImg(finalText)
    Dim fbDescription = oNoticia.Excerpt.Tradueix(lang)
End Code


@Section AdditionalMetaTags
    <meta property="og:title" content="@oNoticia.Title.Tradueix(lang)" />
    @If fbImg > "" Then
        @<meta property="og:image:url" content="@fbImg" />
    End If
    <meta property="og:type" content="article" />
    <meta property="og:url" content="@Model.Url.AbsoluteUrl(lang)" />
    @If fbDescription > "" Then
        @<meta Property="og:description" content="@Html.Raw(fbDescription)" />
    End If
End Section




<div class="LeftSide">
    <div>
        <h1>@ViewBag.Title</h1>
    </div>


    @If oNoticia.Src <> DTOContent.Srcs.Content Then
        @<div Class="psubtitle">
            @String.Format(lang.Tradueix("noticia publicada el {0}", "noticia publicada el {0}", "post published on {0}"), oNoticia.Fch.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES")))
            <br />
            @If oNoticia.professional Then
                @<span class="red">
                    @lang.Tradueix("de acceso exclusivo a profesionales", "d'accés exclusiu per professionals", "professional exclusive access", "de acesso exclusivo a profissionais")
                </span>
                @<br />
            End If
            @If oNoticia.VisitCount = 0 Then
                lang.Tradueix("¡enhorabuena, eres el primero en leer esta noticia!", "enhorabona, ets el primer en llegir aquesta noticia!", "congratulations, you are the first one to read this post!")
            Else
                String.Format(lang.Tradueix("leída {0:#,##0} veces hasta ahora", "llegida {0:#,##0} vegades fins ara", "read {0:#,##0} times until now"), oNoticia.VisitCount)
            End If
        </div>
    End If

    <div class="Contingut">
        @Html.Raw(finalText)
    </div>

    <div class="readmore">
        <a href="/noticias">
            @lang.Tradueix("ver todas las noticias...", "veure totes les noticies...", "see all news...")
        </a>
    </div>

    <div id="Comments"></div>


</div>

@If Not ContextHelper.isTrimmed() Then
    @<div Class="RightSide">
        <div Class="Noticias"></div>
        <div Class="readmore">
            <a href="/noticias">
                @lang.Tradueix("ver todas las noticias...", "veure totes les noticies...", "see all news...")
            </a>
        </div>
    </div>
End If


<input type="hidden" id="HiddenComment" data-parent="@Model.Guid.ToString()" data-src="@CInt(DTOPostComment.ParentSources.Noticia)" /> <!--Required for comments-->


@Section Scripts
    <script src="~/Media/js/ProductPlugin.js"></script>
    <script src="~/Media/js/Comments.js"></script>
    <Script src="~/Media/js/Noticias.js"></Script>

    <Script>
        var isTrimmed = @(If(ContextHelper.isTrimmed, 1, 0));

        $(document).ready(function () {
            if (isTrimmed == 0) {
                loadLastNews();
            }
            loadLastComments();
        });

        function loadLastNews() {
            var postsDiv = $('.RightSide .Noticias');
            var Url = '/news/PartialNoticias/@ViewBag.Lang.tag';
            postsDiv.load(Url, function() {});
        }

        function loadLastComments() {
            var commentsDiv = $('#Comments');
            var target = '@Model.Guid.ToString()';
            var targetSrc = '@CInt(DTOPostComment.ParentSources.Noticia)';
            loadComments(commentsDiv, target, targetSrc, 0);
        }

    </Script>
End Section

@Section Styles
    <script src="https://kit.fontawesome.com/05a6a08892.js" crossorigin="anonymous"></script>
    <link href="~/Media/Css/Plugin.css" rel="stylesheet" />
    <link href="~/Styles/Site.css" rel="stylesheet" />
    <link href="~/Media/Css/Noticias.css" rel="stylesheet" />
    <link href="~/Media/Css/Comments.css" rel="stylesheet" />
    <Style scoped>

        .ContentColumn {
            display: flex;
            flex-direction: row;
            column-gap: 15px;
            max-width: 100%;
            width: 100%;
        }

            .ContentColumn .LeftSide {
                max-width: 600px;
                width: 100%;
            }

            .ContentColumn .RightSide {
                max-width: 265px;
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

        @@media(max-width: 1000px) {
            .RightSide {
                display: none;
            }
        }
    </Style>

End Section

