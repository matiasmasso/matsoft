Public Class NoticiaController
    Inherits _BaseController

    <HttpPost>
    <Route("api/noticias")>
    Public Function all(user As DTOBaseGuid) As List(Of DUI.Noticia)

        Dim oUser As DTOUser = Nothing
        Dim retval As New List(Of DUI.Noticia)
        Try
            oUser = BLLUser.Find(user.Guid)

            'Dim HidePro As Boolean = Not oUser.Rol.IsProfesional

            Dim oNoticias As List(Of DTONoticia) = BLLNoticias.LastNoticias(oUser) 'BLLNoticias.Headers(DTONoticiaBase.Srcs.News, HidePro)
            For Each oNoticia As DTONoticia In oNoticias
                Dim dui As New DUI.Noticia
                With dui
                    .guid = oNoticia.Guid
                    .Fch = oNoticia.Fch.Date
                    .title = oNoticia.Title.Tradueix(oUser.Lang)
                    .excerpt = BLLNoticia.Excerpt(oNoticia).Tradueix(oUser.Lang)
                    .url = BLLNoticia.UrlForIMat(oNoticia)
                    .thumbnailUrl = BLLNoticia.UrlThumbnail(oNoticia, True)
                End With
                retval.Add(dui)
            Next

        Catch ex As Exception
            BLLWinBug.Log(ex.Message)
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/noticias/current")>
    Public Function current(data As DTOBaseGuid) As DUI.Noticia

        Dim oUser As DTOUser = Nothing

        Dim retval As New DUI.Noticia()
        Try
            oUser = BLLUser.Find(data.Guid)
            'oUser = BLLUser.WellKnown(BLLUser.WellKnowns.matias, True)

            Dim oNoticia As DTONoticia = BLLNoticias.LastNoticia(oUser)

            With retval
                .guid = oNoticia.Guid
                '.Fch = oNoticia.Fch.Date.ToUniversalTime
                .Fch = oNoticia.Fch.Date
                .thumbnailUrl = BLLNoticia.UrlThumbnail(oNoticia, AbsoluteUrl:=True)
                .title = oNoticia.Title.Tradueix(oUser.Lang)
                .excerpt = BLLNoticia.Excerpt(oNoticia).Tradueix(oUser.Lang).Replace("<p>", "").Replace("</p>", vbCrLf).Replace("<br/>", vbCrLf)
                .url = BLLNoticia.UrlForIMat(oNoticia)
            End With

        Catch ex As Exception
            BLLWinBug.Log(ex.Message)
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/noticias/current2")>
    Public Function current2(user As DTOBaseGuid) As DTOCompactNoticia
        Dim retval As DTOCompactNoticia = CurrentNoticia(user)
        Return retval
    End Function

    Private Function CurrentNoticia(user As DTOBaseGuid) As DTOCompactNoticia
        Dim retval As New DTOCompactNoticia()
        Try
            Dim oUser As DTOUser = BLLUser.Find(user.Guid)
            Dim oNoticia As DTONoticia = BLLNoticias.LastNoticia(oUser)

            With retval
                .guid = oNoticia.Guid
                .fch = oNoticia.Fch.Date
                .thumbnailUrl = BLLNoticia.UrlThumbnail(oNoticia, AbsoluteUrl:=True)
                .title = oNoticia.Title.Tradueix(oUser.Lang)
                .excerpt = BLLNoticia.Excerpt(oNoticia).Tradueix(oUser.Lang).Replace("<p>", "").Replace("</p>", vbCrLf).Replace("<br/>", vbCrLf)
                .url = BLLNoticia.UrlForIMat(oNoticia)
            End With

        Catch ex As Exception
            BLLWinBug.Log(ex.Message)
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/noticias/current2")>
    Public Function currentGet() As DTOCompactNoticia
        Dim oUser As DTOUser = BLLUser.WellKnown(BLLUser.WellKnowns.toni)
        Dim retval As DTOCompactNoticia = CurrentNoticia(oUser)
        Return retval
    End Function


End Class
