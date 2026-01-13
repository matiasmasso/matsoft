Public Class MediaResourcesHelper

    Shared Function Factory(oUser As DTOUser, sFullPath As String, exs As List(Of Exception)) As DTOMediaResource
        Dim retval As DTOMediaResource = Nothing
        Dim oByteArray As Byte() = Nothing
        Dim sFilename As String = System.IO.Path.GetFileName(sFullPath)
        If sFilename.Length <= DTOMediaResource.MAXNOMLENGTH Then
            If FileSystemHelper.GetStreamFromFile(sFullPath, oByteArray, exs) Then
                retval = DTOMediaResource.Factory(oUser, oByteArray)
                With retval
                    .Nom = sFilename
                    .Mime = MimeHelper.GetMimeFromExtension(sFilename)

                    If LoadMimeDetails(retval, exs) Then
                        .Filename = sFilename
                        .IsLoaded = True
                    End If

                End With
            End If
        Else
            exs.Add(New Exception("el nom del fitxer no pot passar de 80 caracters"))
        End If
        Return retval
    End Function


    Shared Function LoadFromStream(oUser As DTOUser, oByteArray As Byte(), ByRef oMediaResource As DTOMediaResource, exs As List(Of Exception), Optional sFilename As String = "") As Boolean
        Dim retval As Boolean
        oMediaResource = DTOMediaResource.Factory(oUser, oByteArray)
        With oMediaResource
            If sFilename = "" Then
                .Mime = MimeHelper.GuessMime(oByteArray)
            Else
                .Filename = sFilename
                .Nom = sFilename
                .Mime = MimeHelper.GetMimeFromExtension(sFilename)
            End If
            If LoadMimeDetails(oMediaResource, exs) Then
                .IsLoaded = True
                .IsNew = True
                retval = True
            End If
        End With
        Return retval
    End Function

    Shared Function LoadMimeDetails(oMediaResource As DTOMediaResource, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = True
        With oMediaResource
            Select Case .Mime
                Case MimeCods.Pdf
                    retval = LoadPdf(oMediaResource, exs)
                Case MimeCods.Ai
                    .Thumbnail = LegacyHelper.ImageHelper.Converter(My.Resources.ai_140)
                Case MimeCods.Rtf
                    .Thumbnail = LegacyHelper.ImageHelper.Converter(My.Resources.word) ' pasar a 140x140
                Case MimeCods.Zip
                    Dim oFiles = ZipHelper.Extract(.Stream)
                    If oFiles.Count > 0 Then
                        Dim oFirstDocfile = LegacyHelper.DocfileHelper.Factory(exs, oFiles.First.ByteArray)
                        .Thumbnail = oFirstDocfile.Thumbnail
                    End If
                    If .Thumbnail Is Nothing Then .Thumbnail = LegacyHelper.ImageHelper.Converter(My.Resources.Zip_140)
                Case MimeCods.Jpg, MimeCods.Gif, MimeCods.Png, MimeCods.Bmp, MimeCods.Tif, MimeCods.Tiff
                    Dim oImg As SixLabors.ImageSharp.Image = MatHelperStd.ImageHelper.GetImgFromByteArray(.Stream)
                    .Size = New SixLabors.ImageSharp.Size(oImg.Width, oImg.Height)
                    .VRes = oImg.Metadata.VerticalResolution
                    .HRes = oImg.Metadata.HorizontalResolution
                    .Thumbnail = MatHelperStd.ImageHelper.GetThumbnailToFill(oImg, DTOMediaResource.THUMBWIDTH, DTOMediaResource.THUMBHEIGHT)
                Case MimeCods.Ppt, MimeCods.Pptx
                Case MimeCods.Docx
                Case MimeCods.Mpg, MimeCods.Mp4
                    .Thumbnail = LegacyHelper.ImageHelper.Converter(My.Resources.NoImg140)
            End Select
        End With
        Return retval
    End Function

    Shared Function LoadPdf(ByRef oMediaResource As DTOMediaResource, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim oPdf As LegacyHelper.GhostScriptHelper.Pdf = Nothing
        If LegacyHelper.DocfileHelper.Load(oPdf, oMediaResource.Stream, DTOMediaResource.THUMBWIDTH, DTOMediaResource.THUMBHEIGHT, exs) Then
            With oMediaResource
                .Size = oPdf.Size
                .Pags = oPdf.PageCount
                .Thumbnail = oPdf.Thumbnail
            End With
            retval = True
        End If
        Return retval
    End Function

End Class
