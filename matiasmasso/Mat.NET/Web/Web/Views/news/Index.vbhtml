@ModelType DTONoticia.Srcs
@Code
    Dim exs As New List(Of Exception)
    Layout = "~/Views/Shared/_Layout.vbhtml"

    Dim oUser = Mvc.ContextHelper.FindUserSync()
    Dim sTitlebarClass As String = ""
    Dim oNoticias As IEnumerable(Of DTONoticiaBase)
    Select Case Model
        Case DTONoticia.Srcs.News
            sTitlebarClass = "MatBoxHeaderOrange"
            'salta't la primera perque ja surt al marge esquerra com a ultima noticia
            oNoticias = New List(Of DTONoticia) 'FEB2.Noticias.LastNoticiasSync(exs, oUser).Skip(1).ToList
        Case DTONoticia.Srcs.Eventos
            sTitlebarClass = "MatBoxHeaderBlue"
            oNoticias = FEB2.Eventos.HeadersSync(exs, oUser)
    End Select
    Dim lang = If(ViewBag.Lang, Mvc.ContextHelper.Lang)

End Code

<style scoped>
    .list-wrapper {
        clear:both;
    }
    
     .box-wrap {
        margin-bottom: 5px;
        display: inline-block;
    }
</style>

<div class="list-wrapper">
    @For Each oNoticia As DTONoticia In oNoticias

        @<div class="MatBox180 box-wrap">
    <a href="@FEB2.Noticia.UrlFriendly(oNoticia, False)">
        @If Model = DTONoticia.Srcs.Eventos Then
        @<div class='MatBoxHeaderBlue'>
             @DirectCast(oNoticia, DTOEvento).FchFrom.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"))
        </div>
        Else
        @<div class='MatBoxHeaderOrange'>
            @oNoticia.fch.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"))
        </div>
        End If

        <img src="@FEB2.Noticia.UrlThumbnail(oNoticia)">

        <div class="MatBoxFooter">
            @oNoticia.Title.Tradueix(Mvc.ContextHelper.lang())
        </div>
    </a>
</div>
    Next
</div>
