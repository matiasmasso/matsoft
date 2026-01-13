
Public Class ImgController
    Inherits System.Web.Mvc.Controller

    Function Index(cod As Integer, id As String) As ActionResult
        Dim retval As FileStreamResult = Nothing
        Dim exs As New List(Of Exception)
        Dim oImgBytes As Byte() = GetImageFromCod(cod, id, exs)
        If oImgBytes IsNot Nothing Then
            Dim oStream As New System.IO.MemoryStream(oImgBytes)
            oStream.Position = 0
            retval = New FileStreamResult(oStream, "image/jpeg")
        End If
        Return retval
    End Function


    Function Index2(cod As String, id As String) As ActionResult
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)
        Dim oEmp As New DTOEmp(DTOEmp.Ids.MatiasMasso)
        'If cod = "64" Then Stop
        Try
            If id > "" Then
                Dim oCod As Defaults.ImgTypes = cod
                Dim oImgBytes As Byte() = GetImageFromCod(oCod, id, exs)
                If oImgBytes IsNot Nothing Then
                    'retval = New ImageResult(oImg)

                    'Dim oMimeCod As MimeCods = LegacyHelper.ImageHelper.GuessMime(oImg) provisional while we can't read it from sixlabors imagesharp
                    Dim oMimeCod As MimeCods = MimeCods.Jpg
                    'Dim oImageFormat As System.Drawing.Imaging.ImageFormat = MatHelperStd.ImageHelper.GetImageFormat(oMimeCod)
                    Dim sContentType As String = MediaHelper.ContentType(oMimeCod)
                    MyBase.HttpContext.Response.Cache.SetMaxAge(New TimeSpan(24 * 360, 0, 0))

                    Dim oStream As New System.IO.MemoryStream(oImgBytes)
                    oStream.Position = 0
                    retval = New FileStreamResult(oStream, sContentType) ' "image/jpeg")
                End If
            End If

        Catch ex As Exception
            exs.Add(ex)
            Dim sb As New System.Text.StringBuilder
            If IsNumeric(cod) Then
                sb.AppendLine("Cod: " & CType(cod, Defaults.ImgTypes).ToString())
                sb.AppendLine()
            End If
            If Request.UrlReferrer Is Nothing Then
                sb.AppendLine("Request.UrlReferrer: no referrer")
            Else
                sb.AppendLine("Request.UrlReferrer: " & Request.UrlReferrer.ToString())
            End If
            sb.AppendLine()
            sb.AppendLine("ex.Message: " & ex.Message)
            sb.AppendLine()
            sb.AppendLine("ex.StackTrace: " & ex.StackTrace)

            BEBL.MailMessageHelper.MailAdmin(oEmp, "Error a ImgController oCod=" & cod & " Id=" & id, sb.ToString())
        End Try
        Return retval
    End Function

    <OutputCache(Duration:=600, VaryByParam:="none")>
    Private Function GetImageFromCod(oCod As Defaults.ImgTypes, Id As String, exs As List(Of Exception)) As Byte()
        Dim retval As Byte() = Nothing

        'Promofarma requests all image links end with the proper image extension
        If (Id.EndsWith(".jpg") Or Id.EndsWith(".png")) Then Id = Id.Substring(0, Id.Length - 4)

        Dim oEmp As New DTOEmp(DTOEmp.Ids.MatiasMasso)
        Select Case oCod
            Case Defaults.ImgTypes.tpalogo
                Dim oGuid As New Guid(Id)
                Dim oBrand = BEBL.ProductBrand.Find(oGuid)
                retval = oBrand.Logo
            Case Defaults.ImgTypes.news265x150
                Dim oGuid As New Guid(Id)
                retval = BEBL.Noticia.Image265x150(oGuid).ByteArray
            Case Defaults.ImgTypes.stpwebthumbnail
                Dim oGuid As New Guid(Id)
                retval = If(BEBL.ProductCategory.Image(oGuid), BEBL.DefaultImage.Image(Defaults.ImgTypes.stpwebthumbnail))
            Case DTO.Defaults.ImgTypes.art150
                Dim oGuid As New Guid(Id)
                Dim oThumbnail As ImageMime = BEBL.ProductSku.ThumbnailMime(oGuid)
                If oThumbnail Is Nothing Then
                    retval = BEBL.DefaultImage.Image(DTO.Defaults.ImgTypes.art150)
                Else
                    retval = oThumbnail.ByteArray
                End If
            Case DTO.Defaults.ImgTypes.art
                Dim oGuid As New Guid(Id)
                Dim oImageMime As ImageMime = BEBL.ProductSku.ImageMime(oGuid)
                If oImageMime Is Nothing Then
                    retval = BEBL.DefaultImage.Image(DTO.Defaults.ImgTypes.art150)
                Else
                    retval = oImageMime.ByteArray
                End If
            Case Defaults.ImgTypes.salesgrafic
                Dim oGuid As New Guid(Id)
                Dim oUser As New DTOUser(oGuid)
                retval = BEBL.GrfMesValues.Image(oUser, 500, 200)

            Case Defaults.ImgTypes.galleryItem
                Dim oGuid As New Guid(Id)
                retval = BEBL.GalleryItem.ImageMime(oGuid).ByteArray
            Case Defaults.ImgTypes.download150
                Dim sHash As String = CryptoHelper.HexToString(Id)
                retval = BEBL.DocFile.Thumbnail(sHash, 150)
            Case Defaults.ImgTypes.download
                Dim sHash As String = CryptoHelper.HexToString(Id)
                retval = BEBL.DocFile.Thumbnail(sHash)
            Case Defaults.ImgTypes.webportadabrand
            Case Defaults.ImgTypes.banner
                Dim oGuid As New Guid(Id)
                Dim oBanner As DTOBanner = BEBL.Banner.Find(oGuid)
                retval = oBanner.Image
            Case Defaults.ImgTypes.incidenciathumbnailpreview
                'retval = FEBL.Incidencia.ThumbnailPreview(GetSession, Id)
            Case Defaults.ImgTypes.iban
                retval = BEBL.Iban.Img(Id, DTOLang.ESP)
            Case Defaults.ImgTypes.sorteofbfeatured200

                Dim oGuid As Guid
                If GuidHelper.IsGuid(Id) Then
                    oGuid = New Guid(Id)
                Else
                    oGuid = GuidHelper.GetGuidFromBase64(Id)
                End If
                Dim oRaffle As DTORaffle = BEBL.Raffle.Find(oGuid)
                retval = oRaffle.ImageFbFeatured
            Case Defaults.ImgTypes.sorteobanner600
                Dim oGuid As New Guid(Id)
                Dim oRaffle As DTORaffle = BEBL.Raffle.Find(oGuid)
                retval = oRaffle.ImageBanner600
            Case Defaults.ImgTypes.sorteocallaction500
                Dim oGuid As New Guid(Id)
                Dim oRaffle As DTORaffle = BEBL.Raffle.Find(oGuid)
                retval = oRaffle.ImageCallToAction500
            Case Defaults.ImgTypes.sorteowinner
                Dim oGuid As New Guid(Id)
                Dim oRaffle As DTORaffle = BEBL.Raffle.Find(oGuid)
                retval = oRaffle.ImageWinner
            Case Defaults.ImgTypes.bloggerlogo
                Dim oGuid As New Guid(Id)
                Dim oBlogger As DTOBlogger = BEBL.Blogger.Find(oGuid)
                retval = oBlogger.Logo
            Case Defaults.ImgTypes.blogmmcpostfeatured
                'Dim oGuid As New Guid(Id)
                'Dim oBlogPost = BEBL.BlogPost.FromId(Id)
                'retval = MatHelperStd.ImageHelper.DownloadFromWebsite(oBlogPost.FeaturedImageUrl)
            Case Defaults.ImgTypes.selfiethumbnail
            Case Defaults.ImgTypes.selfiefullsize
            Case Defaults.ImgTypes.selfie353h
            Case Defaults.ImgTypes.pdfpreview
                Dim oGuid As New Guid(Id)
                Dim oDictionary As Dictionary(Of Guid, Byte()) = Me.HttpContext.Application("tmpImages")
                If oDictionary IsNot Nothing Then
                    If oDictionary.ContainsKey(oGuid) Then
                        retval = oDictionary(oGuid)
                        oDictionary.Remove(oGuid)
                    End If
                End If
            Case Defaults.ImgTypes.tpalogodistribuidoroficial
                Dim oGuid As New Guid(Id)
                Dim oBrand = BEBL.ProductBrand.Find(oGuid)
                retval = oBrand.LogoDistribuidorOficial
            Case Defaults.ImgTypes.mayborn
                Dim items = BEBL.Mayborn.Sales()
                retval = BEBL.Mayborn.Grafic(items)
            Case Defaults.ImgTypes.mediaresourcethumbnail
                'Dim oGuid As New Guid(Id)
                'Dim oMediaResource As New DTOMediaResource(oGuid)
                'retval = BEBL.MediaResource.Thumbnail(oMediaResource)
            Case Defaults.ImgTypes.staff
                Dim oGuid As New Guid(Id)
                retval = BEBL.Staff.Avatar(oGuid)
            Case Defaults.ImgTypes.incentiu
                Dim oGuid As New Guid(Id)
                Dim oIncentiu As DTOIncentiu = BEBL.Incentiu.Find(oGuid)
                If oIncentiu Is Nothing Then
                    retval = BEBL.DefaultImage.Image(Defaults.ImgTypes.incentiu)
                Else
                    If oIncentiu.Thumbnail Is Nothing Then
                        retval = BEBL.DefaultImage.Image(Defaults.ImgTypes.incentiu)
                    Else
                        retval = oIncentiu.Thumbnail
                    End If
                End If
            Case Defaults.ImgTypes.wtbolsitelogo
                Dim oGuid As New Guid(Id)
                retval = BEBL.Wtbolsite.Logo(oGuid)
            Case Defaults.ImgTypes.deptbanner
                Dim oGuid As New Guid(Id)
                Dim oDept As New DTODept(oGuid)
                retval = BEBL.Dept.Banner(oDept)
        End Select

        Return retval
    End Function

End Class
