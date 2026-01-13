Public Class DownloadController
    Inherits _BaseController

    <HttpPost>
    <Route("api/downloads")>
    Public Function downloads(target As DTOGuidNom) As List(Of DUI.Download)
        Dim retval As New List(Of DUI.Download)
        Dim oBaseGuid As New DTOBaseGuid(target.Guid)
        Dim oDownloads As List(Of DTOProductDownload) = BLLProductDownloads.All(oBaseGuid)
        For Each oDownload As DTOProductDownload In oDownloads
            Dim item As New DUI.Download
            With item
                .Nom = oDownload.DocFile.Nom
                .Features = BLLDocFile.MediaFeatures(oDownload.DocFile)
                .ThumbnailUrl = BLLDocFile.ThumbnailUrl(oDownload.DocFile, True)
                .FileUrl = BLLDocFile.DownloadUrl(oDownload.DocFile, True)
            End With
            retval.Add(item)
        Next
        Return retval
    End Function
End Class
