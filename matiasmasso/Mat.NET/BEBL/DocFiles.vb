Imports System.Data.SqlClient

Public Class DocFile

    Shared Function Find(sHash As String, Optional LoadStream As Boolean = False) As DTODocFile
        Return DAL.DocFileLoader.FromHash(sHash, LoadStream)
    End Function

    Shared Function FindBySha256(sha256 As String, Optional LoadStream As Boolean = False) As DTODocFile
        Return DAL.DocFileLoader.FromSha256(sha256, LoadStream)
    End Function

    Shared Function Load(ByRef oDocFile As DTODocFile, Optional LoadStream As Boolean = False) As Boolean
        Return DAL.DocFileLoader.Load(oDocFile, LoadStream)
    End Function

    Shared Function Stream(hash As String) As ImageMime
        Return DAL.DocFileLoader.Stream(hash)
    End Function

    Shared Function Thumbnail(hash As String, Optional width As Integer = 0) As Byte()
        Dim retval As Byte() = DAL.DocFileLoader.Thumbnail(hash)
        If retval IsNot Nothing And width <> 0 Then
            Dim oImage = LegacyHelper.ImageHelper.FromBytes(retval)
            Dim oThumbnail As System.Drawing.Image = LegacyHelper.ImageHelper.GetThumbnailToFit(oImage, width)
            retval = oThumbnail.Bytes()
        End If
        Return retval
    End Function

    Shared Function Update(oDocfile As DTODocFile, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = DAL.DocFileLoader.Update(oDocfile, exs)
        Return retval
    End Function

    Shared Function Log(sHash As String, oUser As DTOUser, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = DocFileLoader.Log(sHash, oUser, exs)
        Return retval
    End Function

    Shared Function ThumbnailUrl(oDocFile As DTODocFile) As String
        Dim retval As String = ""
        If oDocFile IsNot Nothing Then
            retval = BEBL.UrlHelper.ApiUrl("docfile/thumbnail", CryptoHelper.UrlFriendlyBase64(oDocFile.Hash))
        End If
        Return retval
    End Function

    Shared Function DownloadUrl(oDocFile As DTODocFile, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = ""
        If oDocFile IsNot Nothing Then
            retval = DTOWebDomain.Default(AbsoluteUrl).Url("doc", CInt(DTODocFile.Cods.download), CryptoHelper.StringToHexadecimal(oDocFile.Hash))
        End If
        Return retval
    End Function

    Shared Function Srcs(oDocfile As DTODocFile) As List(Of DTODocFileSrc)
        Return DocFileLoader.Srcs(oDocfile)
    End Function

    Shared Function Logs(oDocFile As DTODocFile) As List(Of DTODocFileLog)
        Dim retval As List(Of DTODocFileLog) = DocFileLoader.Logs(oDocFile)
        Return retval
    End Function

    Shared Function LoadFromString(src As String) As DTODocFile
        Return LegacyHelper.DocfileHelper.LoadFromString(src)
    End Function

    Shared Function LoadFromStream(exs As List(Of Exception), oDocfile As DTODocFile, oStream As Byte(), oMime As MimeCods) As Boolean
        LegacyHelper.DocfileHelper.LoadFromStream(exs, oDocfile, oDocfile.Stream, oDocfile.Mime)
        Return exs.Count = 0
    End Function

End Class
Public Class DocFiles
    Shared Function All(year As Integer) As List(Of DTODocFile)
        Dim retval As List(Of DTODocFile) = DocFilesLoader.All(year)
        Return retval
    End Function
End Class
