Public Class Banner

    Shared Function Find(oGuid As Guid) As DTOBanner
        Return BannerLoader.Find(oGuid)
    End Function

    Shared Function Image(oGuid As Guid) As ImageMime
        Return BannerLoader.Image(oGuid)
    End Function

    Shared Function Update(oBanner As DTOBanner, exs As List(Of Exception)) As Boolean
        Return BannerLoader.Update(oBanner, exs)
    End Function

    Shared Function Delete(oBanner As DTOBanner, exs As List(Of Exception)) As Boolean
        Return BannerLoader.Delete(oBanner, exs)
    End Function

End Class




Public Class Banners
    Shared Function All(includeObsolets As Boolean) As List(Of DTOBanner)
        Dim retval As List(Of DTOBanner) = BannersLoader.All(includeObsolets)
        Return retval
    End Function

    Shared Function Sprite(oGuids As List(Of Guid)) As Byte()
        Dim oImages = BannersLoader.Sprite(oGuids)
        Dim retval As Byte() = LegacyHelper.SpriteBuilder.Factory(oImages, DTOBanner.THUMBWIDTH, DTOBanner.THUMBHEIGHT)
        Return retval
    End Function

    Shared Function Active(oLang As DTOLang) As List(Of DTOBanner)
        Dim retval As List(Of DTOBanner) = BannersLoader.Active(oLang)
        Return retval
    End Function
End Class
