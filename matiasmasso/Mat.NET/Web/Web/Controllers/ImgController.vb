Imports SixLabors.ImageSharp

Public Class ImgController
    Inherits _MatController

    Async Function Index(cod As String, id As String) As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)
        'If cod = "64" Then Stop
        Try
            If id > "" Then
                Dim oCod As DTO.Defaults.ImgTypes = cod
                Dim oImg As Image = Await GetImageFromCod(oCod, id, exs)
                If oImg IsNot Nothing Then
                    'retval = New ImageResult(oImg)

                    Dim oMimeCod As MimeCods = LegacyHelper.ImageHelper.GuessMime(oImg)
                    Dim oImageFormat As System.Drawing.Imaging.ImageFormat = LegacyHelper.ImageHelper.GetImageFormat(oMimeCod)
                    Dim sContentType As String = MediaHelper.ContentType(oMimeCod)
                    MyBase.HttpContext.Response.Cache.SetMaxAge(New TimeSpan(24 * 360, 0, 0))

                    Dim oStream As New System.IO.MemoryStream
                    oImg.SaveAsJpeg(oStream)
                    oStream.Position = 0
                    retval = New FileStreamResult(oStream, sContentType) ' "image/jpeg")
                End If
            End If

        Catch ex As Exception
            exs.Add(ex)
            Dim sb As New System.Text.StringBuilder
            If IsNumeric(cod) Then
                sb.AppendLine("Cod: " & CType(cod, DTO.Defaults.ImgTypes).ToString())
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
            FEB2.MailMessage.MailAdminSync("Error a ImgController oCod=" & cod & " Id=" & id, sb.ToString, exs)
        End Try
        Return retval
    End Function

    <OutputCache(Duration:=600, VaryByParam:="none")>
    Private Async Function GetImageFromCod(oCod As DTO.Defaults.ImgTypes, Id As String, exs As List(Of Exception)) As Threading.Tasks.Task(Of Image)
        Dim retval As Image = Nothing

        Select Case oCod
            Case DTO.Defaults.ImgTypes.TpaLogo
                Dim oGuid As New Guid(Id)
                retval = Await FEB2.ProductBrand.Logo(exs, oGuid)
            Case DTO.Defaults.ImgTypes.News265x150
                Dim oGuid As New Guid(Id)
                retval = Await FEB2.Noticia.Image265x150(exs, oGuid)
            Case DTO.Defaults.ImgTypes.StpWebThumbnail
                Dim oGuid As New Guid(Id)
                Dim oProductCategory As New DTOProductCategory(oGuid)
                retval = Await FEB2.ProductCategory.Thumbnail(exs, oProductCategory)
                If retval Is Nothing Then
                    retval = Await FEB2.DefaultImage.Image(DTO.Defaults.ImgTypes.StpWebThumbnail, exs)
                End If
            Case DTO.Defaults.ImgTypes.Art150
                Dim oGuid As New Guid(Id)
                Dim oSku As New DTOProductSku(oGuid)
                Dim oImage = Await FEB2.ProductSku.Image(exs, oSku)
                If oImage Is Nothing Then
                    retval = Await FEB2.DefaultImage.Image(DTO.Defaults.ImgTypes.Art150, exs)
                Else
                    retval = LegacyHelper.ImageHelper.GetThumbnailToFit(oImage, 148, 170)
                End If
            Case DTO.Defaults.ImgTypes.Art
                Dim oGuid As New Guid(Id)
                Dim oSku As New DTOProductSku(oGuid)
                Dim oImage = Await FEB2.ProductSku.Image(exs, oSku)
                If oImage Is Nothing Then
                    retval = Await FEB2.DefaultImage.Image(DTO.Defaults.ImgTypes.Art150, exs)
                Else
                    retval = Await FEB2.ProductSku.Image(exs, oSku)
                End If
            Case DTO.Defaults.ImgTypes.Art150
                Dim oGuid As New Guid(Id)
                Dim oSku As New DTOProductSku(oGuid)
                Dim oImage = Await FEB2.ProductSku.Image(exs, oSku)
                If oImage Is Nothing Then
                    retval = Await FEB2.DefaultImage.Image(DTO.Defaults.ImgTypes.Art150, exs)
                Else
                    retval = Await FEB2.ProductSku.Image(exs, oSku)
                End If

            Case DTO.Defaults.ImgTypes.SalesGrafic
                Dim oGuid As New Guid(Id)
                Dim oUser As New DTOUser(oGuid)
                retval = Await FEB2.GrfMesValue.Image(exs, oUser, 500, 200)

            Case DTO.Defaults.ImgTypes.galleryItem
                Dim oGuid As New Guid(Id)
                retval = Await FEB2.GalleryItem.Image(oGuid, exs)
            Case DTO.Defaults.ImgTypes.CliLogo
                Dim oGuid As New Guid(Id)
                Dim oContact As New DTOContact(oGuid)
                retval = Await FEB2.Contact.Logo(exs, oContact)
            Case DTO.Defaults.ImgTypes.Download150
                Dim sHash As String = CryptoHelper.HexToString(Id)
                retval = Await FEB2.DocFile.Thumbnail(exs, sHash, 150)
            Case DTO.Defaults.ImgTypes.Download
                Dim sHash As String = CryptoHelper.HexToString(Id)
                retval = Await FEB2.DocFile.Thumbnail(exs, sHash)
            Case DTO.Defaults.ImgTypes.banner
                Dim oGuid As New Guid(Id)
                Dim oBanner As DTOBanner = Await FEB2.Banner.Find(oGuid, exs)
                retval = oBanner.Image
            Case DTO.Defaults.ImgTypes.IncidenciaThumbnailPreview
                'Dim oSession = MyBase.GetSession
                'Dim oFiles As List(Of DTODocFile) = oSession.Incidencia.Attachments()
                ' Dim oDocFile As DTODocFile = oFiles.Find(Function(x) x.Filename = Id)
                'retval = DTODocFile.ThumbnailPreview(oDocFile)
            Case DTO.Defaults.ImgTypes.Iban
                retval = Await FEB2.Iban.Img(exs, Id)
            Case DTO.Defaults.ImgTypes.SorteoFbFeatured200

                Dim oGuid As Guid
                If GuidHelper.IsGuid(Id) Then
                    oGuid = New Guid(Id)
                Else
                    oGuid = GuidHelper.GetGuidFromBase64(Id)
                End If
                retval = Await FEB2.Raffle.ImageFbFeatured(exs, oGuid)
            Case DTO.Defaults.ImgTypes.SorteoBanner600
                Dim oGuid As New Guid(Id)
                retval = Await FEB2.Raffle.ImageBanner600(exs, oGuid)
            Case DTO.Defaults.ImgTypes.SorteoCallAction500
                Dim oGuid As New Guid(Id)
                retval = Await FEB2.Raffle.ImageCallToAction500(exs, oGuid)
            Case DTO.Defaults.ImgTypes.SorteoWinner
                Dim oGuid As New Guid(Id)
                retval = Await FEB2.Raffle.ImageWinner(exs, oGuid)
            Case DTO.Defaults.ImgTypes.BloggerLogo
                Dim oGuid As New Guid(Id)
                retval = Await FEB2.Blogger.Logo(oGuid, exs)
            Case DTO.Defaults.ImgTypes.selfiethumbnail
            Case DTO.Defaults.ImgTypes.SelfieFullSize
            Case DTO.Defaults.ImgTypes.Selfie353h
            Case DTO.Defaults.ImgTypes.PdfPreview
                Dim oGuid As New Guid(Id)
                Dim oDictionary As Dictionary(Of Guid, Image) = Me.HttpContext.Application("tmpImages")
                If oDictionary IsNot Nothing Then
                    If oDictionary.ContainsKey(oGuid) Then
                        retval = oDictionary(oGuid)
                        oDictionary.Remove(oGuid)
                    End If
                End If
            Case DTO.Defaults.ImgTypes.TpaLogoDistribuidorOficial
                Dim oGuid As New Guid(Id)
                retval = Await FEB2.ProductBrand.LogoDistribuidorOficial(exs, oGuid)
            Case DTO.Defaults.ImgTypes.Mayborn
                Dim items = Await FEB2.Mayborn.Sales(exs)
                retval = LegacyHelper.DTOGraph.Image(items)
            Case DTO.Defaults.ImgTypes.mediaresourcethumbnail
                'Dim oGuid As New Guid(Id)
                'Dim oMediaResource As New DTOMediaResource(oGuid)
                'retval = Await FEB2.MediaResource.Thumbnail(exs, oMediaResource)
            Case DTO.Defaults.ImgTypes.Staff
                Dim oGuid As New Guid(Id)
                retval = Await FEB2.Staff.Avatar(exs, oGuid)
            Case DTO.Defaults.ImgTypes.Incentiu
                Dim oGuid As New Guid(Id)
                Dim oIncentiu = Await FEB2.Incentiu.Find(exs, oGuid)
                If oIncentiu Is Nothing Then
                    retval = Await FEB2.DefaultImage.Image(DTO.Defaults.ImgTypes.Incentiu, exs)
                Else
                    If oIncentiu.Thumbnail Is Nothing Then
                        retval = Await FEB2.DefaultImage.Image(DTO.Defaults.ImgTypes.Incentiu, exs)
                    Else
                        retval = oIncentiu.Thumbnail
                    End If
                End If
            Case DTO.Defaults.ImgTypes.WtbolSiteLogo
                Dim oGuid As New Guid(Id)
                Dim oSite = New DTOWtbolSite(oGuid)
                retval = Await FEB2.WtbolSite.Logo(exs, oSite)
                'Case DTO.Defaults.ImgTypes.BancLogos
                'Dim oEmp As New DTOEmp(Id)
                'retval = BLLBancs.Sprite(oEmp)
        End Select

        Return retval
    End Function

    Function renderPdf(maxWidth As Integer, maxHeight As Integer) As JsonResult
        Dim retval As JsonResult = Nothing
        Dim oFile As Byte() = MyBase.PostedFile()
        If oFile IsNot Nothing Then
            Dim oImg As Image = LegacyHelper.DocfileHelper.GetPdfThumbnail(oFile, maxWidth, maxHeight)

            Dim oDictionary As Dictionary(Of Guid, Image) = Me.HttpContext.Application("tmpImages")
            If oDictionary Is Nothing Then
                oDictionary = New Dictionary(Of Guid, Image)
                Me.HttpContext.Application("tmpImages") = oDictionary
            End If
            Dim oGuid As Guid = Guid.NewGuid
            oDictionary.Add(oGuid, oImg)

            Dim myData As Object = New With {.url = "/img/" & CInt(DTO.Defaults.ImgTypes.PdfPreview).ToString & "/" & oGuid.ToString}
            retval = Json(myData, JsonRequestBehavior.AllowGet)
        End If
        Return retval
    End Function

End Class

Public Class ImageResult
    'a eliminar??
    Inherits ActionResult

    Private ReadOnly _Image As System.Drawing.Image

    Public Sub New(oImage As System.Drawing.Image)
        MyBase.New()
        _Image = oImage
    End Sub

    Public Overrides Sub ExecuteResult(context As ControllerContext)
        If _Image IsNot Nothing Then
            Dim oMimeCod As MimeCods = LegacyHelper.ImageHelper.GuessMime(_Image)
            Dim oImageFormat As System.Drawing.Imaging.ImageFormat = LegacyHelper.ImageHelper.GetImageFormat(oMimeCod)
            Dim sContentType As String = MediaHelper.ContentType(oMimeCod)
            context.HttpContext.Response.Cache.SetMaxAge(New TimeSpan(24 * 360, 0, 0))
            _Image.Save(context.HttpContext.Response.OutputStream, oImageFormat)
            context.HttpContext.Response.Flush()
            'context.HttpContext.Response.OutputStream.Position = 0

            'Dim oStream As System.IO.MemoryStream = context.HttpContext.Response.OutputStream
            'oStream.Position = 0
            '_Image.Save(oStream, oImageFormat)
        End If
    End Sub
End Class