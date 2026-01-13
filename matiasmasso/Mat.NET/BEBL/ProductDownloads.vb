Public Class ProductDownload

    Shared Function Find(oGuid As Guid) As DTOProductDownload
        Dim retval As DTOProductDownload = ProductDownloadLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oProductDownload As DTOProductDownload) As Boolean
        Dim retval As Boolean = ProductDownloadLoader.Load(oProductDownload)
        Return retval
    End Function

    Shared Function Update(oProductDownload As DTOProductDownload, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ProductDownloadLoader.Update(oProductDownload, exs)
        Return retval
    End Function

    Shared Function Delete(oProductDownload As DTOProductDownload, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ProductDownloadLoader.Delete(oProductDownload, exs)
        Return retval
    End Function

End Class

Public Class ProductDownloads

    Shared Function All(oTarget As DTOBaseGuid, Optional oSrc As DTOProductDownload.Srcs = DTOProductDownload.Srcs.notSet) As List(Of DTOProductDownload)
        Dim retval As List(Of DTOProductDownload) = ProductDownloadsLoader.All(oTarget, oSrc)
        Return retval
    End Function

    Shared Function ProductModels(oSrc As DTOProductDownload.Srcs, oLang As DTOLang) As DTOProductDownload.ProductModel
        Return ProductDownloadsLoader.ProductModels(oSrc, oLang)
    End Function

    Shared Function All(oSrc As DTOProductDownload.Srcs) As List(Of DTOProductDownload)
        'per la web tipus wwww.matiasmasso.es/catalogos
        Dim retval As List(Of DTOProductDownload) = ProductDownloadsLoader.All(oSrc)
        Return retval
    End Function

    Shared Function FromProductOrChildren(oProduct As DTOProduct, Optional ByVal IncludeObsoletos As Boolean = True, Optional ByVal OnlyConsumerEnabled As Boolean = False, Optional oSrc As DTOProductDownload.Srcs = DTOProductDownload.Srcs.NotSet) As List(Of DTOProductDownload)
        Dim retval As List(Of DTOProductDownload) = ProductDownloadsLoader.FromProductOrChildren(oProduct, IncludeObsoletos, OnlyConsumerEnabled, oSrc)
        Return retval
    End Function


    Shared Function ExistsFromProductOrParent(oProduct As DTOProduct, Optional ByVal IncludeObsoletos As Boolean = True, Optional ByVal OnlyConsumerEnabled As Boolean = False, Optional oSrc As DTOProductDownload.Srcs = DTOProductDownload.Srcs.NotSet) As Boolean
        Dim retval As Boolean = ProductDownloadsLoader.ExistsFromProductOrParent(oProduct, IncludeObsoletos, OnlyConsumerEnabled, oSrc)
        Return retval
    End Function
End Class
