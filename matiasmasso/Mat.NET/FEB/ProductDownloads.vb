Public Class ProductDownload
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOProductDownload)
        Return Await Api.Fetch(Of DTOProductDownload)(exs, "ProductDownload", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oProductDownload As DTOProductDownload) As Boolean
        If Not oProductDownload.IsLoaded And Not oProductDownload.IsNew Then
            Dim pProductDownload = Api.FetchSync(Of DTOProductDownload)(exs, "ProductDownload", oProductDownload.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOProductDownload)(pProductDownload, oProductDownload, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), value As DTOProductDownload) As Task(Of Boolean)
        Dim retval As Boolean
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            If value.DocFile IsNot Nothing Then
                oMultipart.AddFileContent("docfile_thumbnail", value.DocFile.Thumbnail)
                oMultipart.AddFileContent("docfile_stream", value.DocFile.Stream)
            End If
            retval = Await Api.Upload(oMultipart, exs, "ProductDownload")
        End If
        Return retval
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oProductDownload As DTOProductDownload) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOProductDownload)(oProductDownload, exs, "ProductDownload")
    End Function

End Class

Public Class ProductDownloads
    Inherits _FeblBase

    Shared Async Function LastCompatibilityReport(oProduct As DTOProduct, exs As List(Of Exception)) As Task(Of DTOProductDownload)
        Dim retval = Await Api.Fetch(Of DTOProductDownload)(exs, "ProductDownloads/LastCompatibilityReport", oProduct.Guid.ToString())
        Return retval
    End Function

    Shared Async Function All(oTarget As DTOBaseGuid, exs As List(Of Exception)) As Task(Of List(Of DTOProductDownload))
        Return Await Api.Fetch(Of List(Of DTOProductDownload))(exs, "ProductDownloads", oTarget.Guid.ToString())
    End Function

    Shared Async Function All(exs As List(Of Exception), oSrc As DTOProductDownload.Srcs) As Task(Of List(Of DTOProductDownload))
        Return Await Api.Fetch(Of List(Of DTOProductDownload))(exs, "ProductDownloads/FromSrc", oSrc)

        Dim retval = Await Api.Fetch(Of List(Of DTOProductDownload))(exs, "ProductDownloads/FromSrc", oSrc)
        For Each item In retval
            'item.restoreObjects()
        Next
        Return retval

    End Function

    Shared Function BrandDownloadsUrl(oBrand As DTOProductBrand, oSrc As DTOProductDownload.Srcs, Optional AbsoluteUrl As Boolean = False) As String
        Return UrlHelper.Factory(AbsoluteUrl, oSrc.ToString, DTOProductBrand.UrlSegment(oBrand))
    End Function


    Shared Async Function FromProductOrChildren(exs As List(Of Exception), oProduct As DTOProduct, Optional ByVal IncludeObsoletos As Boolean = True, Optional ByVal OnlyConsumerEnabled As Boolean = False, Optional oSrc As DTOProductDownload.Srcs = DTOProductDownload.Srcs.NotSet) As Task(Of List(Of DTOProductDownload))
        Return Await Api.Fetch(Of List(Of DTOProductDownload))(exs, "ProductDownloads/FromProductOrChildren", oProduct.Guid.ToString, OpcionalBool(IncludeObsoletos), OpcionalBool(OnlyConsumerEnabled), oSrc)
    End Function

    Shared Function ExistsFromProductOrParentSync(oProduct As DTOProduct, Optional ByVal IncludeObsoletos As Boolean = True, Optional ByVal OnlyConsumerEnabled As Boolean = False, Optional oSrc As DTOProductDownload.Srcs = DTOProductDownload.Srcs.NotSet) As Boolean
        Dim exs As New List(Of Exception)
        Return Api.FetchSync(Of Boolean)(exs, "ProductDownloads/ExistsFromProductOrParent", oProduct.Guid.ToString, OpcionalBool(IncludeObsoletos), OpcionalBool(OnlyConsumerEnabled), oSrc)
    End Function

    Shared Async Function ProductModels(exs As List(Of Exception), oSrc As DTOProductDownload.Srcs, oLang As DTOLang) As Task(Of DTOProductDownload.ProductModel)
        Return Await Api.Fetch(Of DTOProductDownload.ProductModel)(exs, "ProductDownloads/productModels", oSrc, oLang.Tag)
    End Function


End Class
