@Code
    Dim exs As New List(Of Exception)
    Dim oUser = Mvc.ContextHelper.FindUserSync()
    Dim oEventos = FEB2.Eventos.HeadersSync(exs, oUser)
End Code

<style>
    .news-wrap {
        margin-bottom: 5px;
    }
</style>

@For Each oEvento In oEventos.Take(5)

    @<div class="MatBox180 news-wrap">
        <a href="@FEB2.Evento.UrlFriendly(oEvento, False)">
            <div class="MatBoxHeaderBlue">
                @oEvento.FchFrom.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"))
            </div>
            <img src="@FEB2.Noticia.UrlThumbnail(oEvento)">
            <div class="MatBoxFooter">
                @oEvento.Title.Tradueix(Mvc.ContextHelper.lang())
            </div>
        </a>
    </div>

Next

<div class="readmore">
    <a href="/eventos">
        @Mvc.ContextHelper.Tradueix("ver todos los eventos...", "veure totes els esdeveniments...", "see all events...")
    </a>
</div>
