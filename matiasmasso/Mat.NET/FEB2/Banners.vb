Public Class Banner

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOBanner)
        Return Await Api.Fetch(Of DTOBanner)(exs, "Banner", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oBanner As DTOBanner, exs As List(Of Exception)) As Boolean
        If Not oBanner.IsLoaded And Not oBanner.IsNew Then
            Dim pBanner = Api.FetchSync(Of DTOBanner)(exs, "Banner", oBanner.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOBanner)(pBanner, oBanner, exs)
                oBanner.Image = Api.FetchImageSync(exs, "banner/image", oBanner.Guid.ToString())
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function


    Shared Async Function Update(value As DTOBanner, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim serialized = Api.Serialize(value.Trimmed, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            oMultipart.AddFileContent("image", value.Image)
            retval = Await Api.Upload(oMultipart, exs, "Banner")
        End If
        Return retval
    End Function

    Shared Async Function Delete(oBanner As DTOBanner, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOBanner)(oBanner, exs, "Banner")
    End Function

    Shared Function ImageUrl(oBanner As DTOBanner) As String
        Return Api.ApiUrl("banner/image", oBanner.Guid.ToString())
    End Function



End Class

Public Class Banners
    Inherits _FeblBase

    Shared Async Function AllWithThumbnails(exs As List(Of Exception), Optional includeObsolets As Boolean = False) As Task(Of List(Of DTOBanner))
        Dim retval As New List(Of DTOBanner)
        Dim oBanners = Await Api.Fetch(Of List(Of DTOBanner))(exs, "Banners", OpcionalBool(includeObsolets))
        If exs.Count = 0 AndAlso oBanners.Count > 0 Then
            Dim oGuids = oBanners.Select(Function(x) x.Guid).ToList()
            Dim oSpriteImage = Await Api.downloadImage(Of List(Of Guid))(oGuids, exs, "banners/sprite")
            If exs.Count = 0 Then
                For i As Integer = 0 To oBanners.Count - 1
                    If includeObsolets OrElse TimeHelper.IsBetween(oBanners(i).FchFrom, DateTime.Now, oBanners(i).FchTo) Then
                        oBanners(i).Thumbnail = LegacyHelper.SpriteHelper.Extract(oSpriteImage, i, oBanners.Count)
                        retval.Add(oBanners(i))
                    End If
                Next
            End If
        End If
        Return retval
    End Function


    Shared Function ActiveUrl(oLang As DTOLang) As String
        Return MmoUrl.apiUrl("banners/active", oLang.Tag)
    End Function

    Shared Async Function Active(oLang As DTOLang, exs As List(Of Exception)) As Task(Of List(Of DTOBanner))
        Dim retval = Await Api.Fetch(Of List(Of DTOBanner))(exs, "banners/active", oLang.Tag)
        Return retval
    End Function


    Shared Async Function ActiveObjects(oLang As DTOLang, exs As List(Of Exception)) As Task(Of List(Of Object))
        Dim retval As New List(Of Object)
        Dim oBanners As List(Of DTOBanner) = Await FEB2.Banners.Active(oLang, exs)
        For Each oBanner As DTOBanner In oBanners
            Dim oJsonBanner As New With {
                .Url = FEB2.Banner.ImageUrl(oBanner),
                .Nom = oBanner.Nom,
                .NavigateTo = oBanner.NavigateTo}
            retval.Add(oJsonBanner)
        Next
        Return retval
    End Function

End Class
