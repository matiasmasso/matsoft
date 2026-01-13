@ModelType DTOCategoriaDeNoticia
@Code
    Dim exs As New List(Of Exception)
    Layout = "~/Views/Shared/_Layout.vbhtml"

    Dim oUser = ContextHelper.FindUserSync()
    Dim oNoticias = FEB.Noticias.FromCategoriaSync(exs, oUser, Model)
End Code

<style>
    .list-wrapper {
        clear: both;
    }

    .box-wrap {
        margin-bottom: 5px;
        display: inline-block;
    }
</style>

<div class="list-wrapper">
    @For Each oNoticia As DTONoticia In oNoticias

        @<div class="MatBox180 box-wrap">
            <a href="@FEB.Noticia.UrlFriendly(oNoticia, False)">
                <div class="MatBoxHeaderOrange">
                    @oNoticia.fch.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"))
                </div>
                @If oNoticia.Image265x150 Is Nothing Then
                    @<img src="~/Media/Img/Defaults/News.jpg" />
                Else
                    @<img src="@FEB.Noticia.UrlThumbnail(oNoticia)">
                End If
                <div class="MatBoxFooter">
                    @oNoticia.Title.Tradueix(ContextHelper.lang())
                </div>
            </a>
        </div>
    Next
</div>