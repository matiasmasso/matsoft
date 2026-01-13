@ModelType DTONoticia
@Code
    Dim exs As New List(Of Exception)
    Layout = "~/Views/Shared/_Layout_Minimal.vbhtml"

    Dim oNoticia As DTONoticia = Model

    Dim sUserGuid As String = Request.QueryString("UserGuid")
    Dim oUser = ContextHelper.FindUserSync()
    If MatHelperStd.GuidHelper.IsGuid(sUserGuid) Then
        oUser = FEB.User.FindSync(New Guid(sUserGuid), exs)
    End If
End Code

<style scoped>
    .noticia-wrapper {
        max-width: 500px;
        margin: auto;
    }

    .ptitle {
        margin: 0;
        padding: 0;
        font-size: 14pt;
        font-weight: 800;
    }

    .psubtitle {
        margin: 0 0 10px 0;
        padding: 0 5px 10px 0;
        font-style: italic;
        color: darkgray;
    }

    .red {
        color: red;
    }
</style>


<div class="noticia-wrapper">
    <div class="ptitle">
        @oNoticia.Title.Tradueix(ContextHelper.lang())
    </div>

    <div class="psubtitle">
        @String.Format(ContextHelper.Tradueix("noticia publicada el {0}", "noticia publicada el {0}", "post published on {0}"), oNoticia.fch.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES")))<br />
        @If oNoticia.Professional Then
            @<span class="red">
                @ContextHelper.Tradueix("de acceso exclusivo a profesionales", "d'accés exclusiu per professionals", "professional exclusive access")
            </span>
            @<br />
        End If
        @If oNoticia.VisitCount = 0 Then
            @ContextHelper.Tradueix("¡enhorabuena, eres el primero en leer esta noticia!", "enhorabona, ets el primer en llegir aquesta noticia!", "congratulations, you are the first one to read this post!")
        Else
            @String.Format(ContextHelper.Tradueix("leída {0} veces hasta ahora", "llegida {0} vegades fins ara", "read {0} times until now"), Format(oNoticia.VisitCount, "#,##0"))
        End If
    </div>

    @Html.Raw(DTONoticia.Filtered(oNoticia, oUser).Tradueix(ContextHelper.lang()))

    <div class="readmore">
        <a href="/news">
            @ContextHelper.Tradueix("ver todas las noticias...", "veure totes les noticies...", "see all news...")
        </a>
    </div>

    <div id="Comments"></div>

</div>

@Section Scripts
    <script src="~/Media/js/Comments.js"></script>
    <Script>
        $(document).ready(function(){
            loadComments($('#Comments'),'@Model.Guid.ToString()');
        });
    </Script>
End Section

@Section Styles
    <script src="~/Media/css/Comments.css"></script>
    <style>
        .readmore {
            margin-top: 20px;
        }
    </style>
End Section
