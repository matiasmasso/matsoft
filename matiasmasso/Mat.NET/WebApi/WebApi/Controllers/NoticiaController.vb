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
                    .Fch = oNoticia.Fch
                    .title = oNoticia.Title.Tradueix(oUser.Lang)
                    If oNoticia.Excerpt IsNot Nothing Then
                        .excerpt = oNoticia.Excerpt.Tradueix(oUser.Lang)
                    End If
                    .url = BLLNoticia.UrlForIMat(oNoticia)
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

            Dim oNoticia As DTONoticia = BLLNoticias.LastNoticia(oUser)

            With retval
                .guid = oNoticia.Guid
                .thumbnailUrl = BLLNoticia.UrlThumbnail(oNoticia, AbsoluteUrl:=True)
                .title = oNoticia.Title.Tradueix(oUser.Lang)
                If oNoticia.Excerpt IsNot Nothing Then
                    .excerpt = oNoticia.Excerpt.Tradueix(oUser.Lang)
                End If
                .url = BLLNoticia.UrlForIMat(oNoticia)
            End With

        Catch ex As Exception
            BLLWinBug.Log(ex.Message)
        End Try
        Return retval
    End Function

End Class
