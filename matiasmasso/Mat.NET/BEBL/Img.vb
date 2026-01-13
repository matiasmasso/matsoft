Public Class Img



    Shared Function Factory(oCod As DTO.Defaults.ImgTypes, sId As String, oLang As DTOLang) As Byte()
        Dim retval As Byte() = Nothing
        Select Case oCod
            Case DTO.Defaults.ImgTypes.iban
                retval = Iban.Img(sId, oLang)
            Case DTO.Defaults.ImgTypes.download150
                Dim sHash As String = CryptoHelper.HexToString(sId)
                Dim oDocFile As DTODocFile = DocFileLoader.FromHash(sHash)
                Dim oImage = LegacyHelper.ImageHelper.FromBytes(oDocFile.Thumbnail())
                retval = LegacyHelper.ImageHelper.GetThumbnailToFit(oImage, 150).Bytes
            Case DTO.Defaults.ImgTypes.download
                Dim sHash As String = CryptoHelper.HexToString(sId)
                Dim oDocFile As DTODocFile = DocFileLoader.FromHash(sHash)
                retval = oDocFile.Thumbnail()
            Case Else
                Dim oGuid = GuidHelper.GetGuidFromBase64(sId)
                retval = Factory(oCod, oGuid)
        End Select

        Return retval
    End Function

    Shared Function Factory(oCod As DTO.Defaults.ImgTypes, oGuid As Guid) As Byte()
        Dim retval As Byte() = Nothing

        Select Case oCod
            Case DTO.Defaults.ImgTypes.tpalogo
                Dim oBrand As DTOProductBrand = ProductBrandLoader.Find(oGuid)
                retval = oBrand.Logo
            Case DTO.Defaults.ImgTypes.news265x150
                retval = NoticiaLoader.Image265x150(oGuid).ByteArray()
            Case DTO.Defaults.ImgTypes.stpwebthumbnail
                retval = If(ProductCategoryLoader.Image(oGuid), DefaultImageLoader.Image(DTO.Defaults.ImgTypes.stpwebthumbnail))
            Case DTO.Defaults.ImgTypes.art150
                Dim oThumbnail As ImageMime = ProductSkuLoader.ThumbnailMime(oGuid)
                If oThumbnail Is Nothing Then
                    retval = DefaultImageLoader.Image(DTO.Defaults.ImgTypes.art150)
                Else
                    retval = oThumbnail.ByteArray
                End If
            Case DTO.Defaults.ImgTypes.art
                Dim oImageMime As ImageMime = ProductSkuLoader.ImageMime(oGuid)
                If oImageMime Is Nothing Then
                    retval = DefaultImageLoader.Image(DTO.Defaults.ImgTypes.art150)
                Else
                    retval = oImageMime.ByteArray
                End If
            Case DTO.Defaults.ImgTypes.mediaresourcethumbnail
                'Dim oMediaResource As New DTOMediaResource(oGuid)
                'retval = MediaResourceLoader.Thumbnail(oMediaResource)
            Case DTO.Defaults.ImgTypes.salesgrafic
                Dim oUser As New DTOUser(oGuid)
                retval = GrfMesValues.Image(oUser, 500, 200)

            Case DTO.Defaults.ImgTypes.galleryItem
                Dim oGallery As DTOGalleryItem = GalleryItemLoader.Find(oGuid)
                retval = oGallery.Image
            Case DTO.Defaults.ImgTypes.webportadabrand 'Deprecated
            Case DTO.Defaults.ImgTypes.banner
                Dim oBanner As DTOBanner = BannerLoader.Find(oGuid)
                retval = oBanner.Image
            Case DTO.Defaults.ImgTypes.incidenciathumbnailpreview
                'retval = FEBL.Incidencia.ThumbnailPreview(GetSession, Id)
            Case DTO.Defaults.ImgTypes.sorteofbfeatured200
                Dim oRaffle As DTORaffle = RaffleLoader.Find(oGuid)
                retval = oRaffle.ImageFbFeatured
            Case DTO.Defaults.ImgTypes.sorteobanner600
                Dim oRaffle As DTORaffle = RaffleLoader.Find(oGuid)
                retval = oRaffle.ImageBanner600
            Case DTO.Defaults.ImgTypes.sorteocallaction500
                Dim oRaffle As DTORaffle = RaffleLoader.Find(oGuid)
                retval = oRaffle.ImageCallToAction500
            Case DTO.Defaults.ImgTypes.sorteowinner
                Dim oRaffle As DTORaffle = RaffleLoader.Find(oGuid)
                retval = oRaffle.ImageWinner
            Case DTO.Defaults.ImgTypes.bloggerlogo
                Dim oBlogger As DTOBlogger = BloggerLoader.Find(oGuid)
                retval = oBlogger.Logo
            Case DTO.Defaults.ImgTypes.tpalogodistribuidoroficial
                Dim oBrand As DTOProductBrand = ProductBrandLoader.Find(oGuid)
                retval = oBrand.LogoDistribuidorOficial
            Case DTO.Defaults.ImgTypes.mayborn
                retval = Mayborn.Graph(Mayborn.Modes.Sales)
            Case DTO.Defaults.ImgTypes.staff
                Dim oStaff As DTOStaff = StaffLoader.Find(oGuid)
                retval = oStaff.Avatar
            Case DTO.Defaults.ImgTypes.incentiu
                Dim oIncentiu As DTOIncentiu = IncentiuLoader.Find(oGuid)
                If oIncentiu Is Nothing Then
                    retval = DefaultImageLoader.Image(DTO.Defaults.ImgTypes.incentiu)
                Else
                    If oIncentiu.Thumbnail Is Nothing Then
                        retval = DefaultImageLoader.Image(DTO.Defaults.ImgTypes.incentiu)
                    Else
                        retval = oIncentiu.Thumbnail
                    End If
                End If
            Case DTO.Defaults.ImgTypes.wtbolsitelogo
                Dim oSite = New DTOWtbolSite(oGuid)
                retval = WtbolSiteLoader.Logo(oSite)
        End Select

        Return retval
    End Function

End Class
