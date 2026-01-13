@Code
    Dim exs As New List(Of Exception)
    Dim oUser = ContextHelper.FindUserSync()
    Dim oEventos = FEB.Eventos.HeadersSync(exs, oUser)
End Code

<style>
    .news-wrap {
        margin-bottom: 5px;
    }
</style>

@For Each oEvento In oEventos.Take(5)

    @<div class="MatBox180 news-wrap">
        <a href="@FEB.Evento.UrlFriendly(oEvento, False)">
            <div class="MatBoxHeaderBlue">
                @oEvento.FchFrom.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"))
            </div>
            <img src="@FEB.Noticia.UrlThumbnail(oEvento)">
            <div class="MatBoxFooter">
                @oEvento.Title.Tradueix(ContextHelper.lang())
            </div>
        </a>
    </div>

Next

<div class="readmore">
    <a href="/eventos">
        @ContextHelper.Tradueix("ver todos los eventos...", "veure totes els esdeveniments...", "see all events...")
    </a>
</div>
