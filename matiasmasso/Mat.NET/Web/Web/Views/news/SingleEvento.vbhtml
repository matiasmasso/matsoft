@ModelType DTONoticia
@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"

    Dim oNoticia As DTONoticia = Model

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
</style>


<div class="noticia-wrapper">
    <div class="ptitle">
        @oNoticia.Title.Tradueix(Mvc.ContextHelper.lang())
    </div>



    <div class="psubtitle">
        @String.Format(Mvc.ContextHelper.Tradueix("evento publicado el {0}", "publicat el {0}", "published on {0}"), oNoticia.fch.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES")))<br />
        @If oNoticia.VisitCount = 0 Then
            @Mvc.ContextHelper.Tradueix("¡enhorabuena, eres el primero en leer sobre este evento!", "enhorabona, ets el primer en llegir sobre aquest esdeveniment!", "congratulations, you are the first one to read about this event!")
        Else
            @String.Format(Mvc.ContextHelper.Tradueix("leído {0} veces hasta ahora", "llegit {0} vegades fins ara", "read {0} times until now"), Format(oNoticia.VisitCount, "#,##0"))
        End If
    </div>

    <p>
        <div class="fb-share-button" data-href="@Request.RawUrl()" data-layout="button_count"></div>
    </p>

    @Html.Raw(oNoticia.Text.Tradueix(Mvc.ContextHelper.lang()))


    <div class="readmore">
        <a href="/eventos">
            @Mvc.ContextHelper.Tradueix("ver todos los eventos...", "veure tots els esdeveniments...", "see all events...")
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
End Section

