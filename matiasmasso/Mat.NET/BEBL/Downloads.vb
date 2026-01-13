Public Class Download
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

Public Class Downloads
    Shared Function All(oTarget As DTOBaseGuidCodNom) As List(Of DTOProductDownload)
        Dim retval = ProductDownloadsLoader.All(oTarget)
        Return retval
    End Function

    Shared Function FromProductOrParent(oProduct As DTOProduct) As List(Of DTOProductDownload)
        Dim retval As List(Of DTOProductDownload) = ProductDownloadsLoader.FromProductOrParent(oProduct)
        Return retval
    End Function

End Class
