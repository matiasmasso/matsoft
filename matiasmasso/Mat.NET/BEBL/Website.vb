Public Class Website


    Shared Function Noticias(oUser As DTOUser, oLang As DTOLang, pageIdx As Integer) As DTO.WebsiteModels.Noticias
        Dim retval = WebsiteModels.Noticias.Factory(oUser, oLang)

        Dim oNoticias = NoticiasLoader.All(DTONoticiaBase.Srcs.News)

        retval.SideMenu = New List(Of DTOBox)
        retval.SideMenu.Add(DTOBox.Factory("Noticias", "/Noticias"))
        retval.SideMenu.Add(DTOBox.Factory("Eventos", "/Eventos"))
        retval.SideMenu.Add(DTOBox.Factory("Blog", "/Blog"))

        retval.Gallery = DTOGallery.Factory(DTONoticia.IMAGEWIDTH, DTONoticia.IMAGEHEIGHT, "", oNoticias.Count, pageIdx)
        Dim oUserGuid As Guid = System.Guid.Empty
        If oUser IsNot Nothing Then oUserGuid = oUser.Guid
        For i As Integer = retval.Gallery.Pagination.PageFirstItem To retval.Gallery.Pagination.PageLastItem
            Dim oNoticia = oNoticias(i)
            retval.Gallery.AddItem(oNoticia.title.tradueix(oLang), oNoticia.ThumbnailUrl, oNoticia.friendlyUrl(False), oNoticia.Guid.ToString)
        Next

        Return retval
    End Function
End Class
