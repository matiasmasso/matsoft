Imports SixLabors.ImageSharp
Imports LegacyHelper
Public Class DocfileHelper


    Shared Function ZipThumbnail(oByteArray As Byte()) As Image
        Dim retval As Image = Nothing
        Dim sFilenames As List(Of String) = MatHelperStd.ZipHelper.Filenames(oByteArray)
        If sFilenames.Count > 0 Then
            Dim oFiles = ZipHelper.Extract(oByteArray)
            If oFiles.Count > 0 Then
                Dim exs As New List(Of Exception)
                Dim oFirstDocfile = DocfileHelper.Factory(exs, oFiles.First.ByteArray)
                retval = oFirstDocfile.Thumbnail
            End If
        End If
        Return retval
    End Function

    Shared Function Factory(oFiles As List(Of Byte()), exs As List(Of Exception)) As List(Of DTODocFile)
        Dim retval As New List(Of DTODocFile)
        For Each oFile As Byte() In oFiles
            Dim oDocFile As DTODocFile = DocfileHelper.Factory(exs, oFile, MimeCods.NotSet)
            retval.Add(oDocFile)
        Next
        Return retval
    End Function
    Shared Function Factory(exs As List(Of Exception), oByteArray As Byte(), Optional oMime As MimeCods = MimeCods.NotSet) As DTODocFile
        Dim retval As New DTODocFile
        LoadFromStream(exs, retval, oByteArray, oMime)
        Return retval
    End Function


    Shared Function LoadFromStream(oByteArray As Byte(), ByRef oDocFile As DTODocFile, exs As List(Of Exception), Optional sFilename As String = "") As Boolean
        Dim retval As Boolean
        oDocFile = New DTODocFile
        With oDocFile
            .Stream = oByteArray
            .length = oByteArray.Length
            .hash = CryptoHelper.HashMD5(oByteArray)
            If sFilename > "" Then
                .filename = sFilename
                .mime = MimeHelper.GetMimeFromExtension(sFilename)
            End If
            If .mime = MimeCods.NotSet Then
                .mime = MimeHelper.GuessMime(oByteArray)
            End If
            If LoadMimeDetails(oDocFile, exs) Then
                retval = True
            End If
        End With
        Return retval
    End Function

    Shared Function LoadMimeDetails(oDocFile As DTODocFile, exs As List(Of Exception)) As Boolean
        'we skip loading icons on server
        Dim retval As Boolean = True
        With oDocFile
            Select Case .mime
                Case MimeCods.Pdf
                    retval = LoadPdf(oDocFile, exs)
                Case MimeCods.Rtf
                    '.Thumbnail = ImageHelper.Converter(My.Resources.word)
                Case MimeCods.Zip
                    Dim oFiles = ZipHelper.Extract(.Stream)
                    If oFiles.Count > 0 Then
                        Dim oFirstDocfile = DocfileHelper.Factory(exs, oFiles.First.ByteArray)
                        '.Thumbnail = oFirstDocfile.Thumbnail
                    End If
                Case MimeCods.Jpg, MimeCods.Gif, MimeCods.Png, MimeCods.Bmp, MimeCods.Tif, MimeCods.Tiff
                    Dim oImg As Image = MatHelperStd.ImageHelper.GetImgFromByteArray(.Stream)
                    .size = New SixLabors.ImageSharp.Size(oImg.Width, oImg.Height)
                    .vRes = oImg.Metadata.VerticalResolution
                    .hRes = oImg.Metadata.HorizontalResolution
                    '.Thumbnail = MatHelperStd.ImageHelper.GetThumbnailToFit(oImg, DTODocFile.THUMB_WIDTH, DTODocFile.THUMB_HEIGHT)
                Case MimeCods.Ppt, MimeCods.Pptx
                Case MimeCods.Docx
                Case MimeCods.Txt
                    Dim src As String = System.Text.ASCIIEncoding.ASCII.GetString(oDocFile.Stream)
                    '.Thumbnail = ImageHelper.Converter(ImageHelper.GetThumbnailFromText(src, DTODocFile.THUMB_WIDTH, DTODocFile.THUMB_HEIGHT))
            End Select
        End With
        Return retval
    End Function


    Shared Function LoadFromStream(exs As List(Of Exception), ByRef oDocfile As DTODocFile, oByteArray As Byte(), Optional oMime As MimeCods = MimeCods.NotSet) As Boolean
        Try
            With oDocfile
                .hash = CryptoHelper.HashMD5(oByteArray)
                .fchCreated = Now
                .Stream = oByteArray
                If oMime = MimeCods.NotSet Then
                    .mime = MimeHelper.GuessMime(oByteArray)
                Else
                    .mime = oMime
                End If
                .length = oByteArray.Length

                Select Case .mime
                    Case MimeCods.Pdf
                        Dim oStream As New System.IO.MemoryStream(oByteArray)
                        Dim oPdf = GhostScriptHelper.Rasterize(exs, oStream)
                        .Thumbnail = oPdf.Thumbnail
                        .pags = oPdf.PageCount
                        .size = oPdf.Size
                    Case MimeCods.Rtf
                        '.Thumbnail = ImageHelper.Converter(My.Resources.word)
                    Case MimeCods.Zip
                        .Thumbnail = DocfileHelper.ZipThumbnail(.Stream)
                    Case MimeCods.Jpg, MimeCods.Gif, MimeCods.Png, MimeCods.Bmp, MimeCods.Tif, MimeCods.Tiff
                        Dim oImg As Image = MatHelperStd.ImageHelper.GetImgFromByteArray(.Stream)
                        .size = New SixLabors.ImageSharp.Size(oImg.Width, oImg.Height)
                        .vRes = oImg.Metadata.VerticalResolution
                        .hRes = oImg.Metadata.HorizontalResolution
                        .Thumbnail = MatHelperStd.ImageHelper.GetThumbnailToFit(oImg, DTODocFile.THUMB_WIDTH, DTODocFile.THUMB_HEIGHT)
                    Case MimeCods.Ppt, MimeCods.Pptx
                    Case MimeCods.Docx
                        '.Thumbnail = ImageHelper.Converter(WordHelper.GetImgFromWordFirstPage(.Stream, exs))
                    Case MimeCods.Xlsx
                        Dim iExcelRows As Integer = 0
                        Dim iExcelCols As Integer = 0
                        '.Thumbnail = ExcelHelper.GetImgFromExcelFirstPage(_DocFile.Stream, iExcelCols, iExcelRows)
                        '.Thumbnail = My.Resources.Excel_Big
                        '.Thumbnail = ExcelThumbnail.GetExcelThumbnail(.Stream, exs)
                        .size = New SixLabors.ImageSharp.Size(iExcelCols, iExcelRows)
                    Case MimeCods.Txt
                        Dim src As String = System.Text.ASCIIEncoding.ASCII.GetString(oByteArray)
                        '.Thumbnail = ImageHelper.GetThumbnailFromText(src, DTODocFile.THUMB_WIDTH, DTODocFile.THUMB_HEIGHT)
                End Select

            End With

        Catch e As SystemException
            exs.Add(e)
        End Try
        Return exs.Count = 0
    End Function


    Shared Function LoadPdf(ByRef oDocFile As DTODocFile, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim oPdf As GhostScriptHelper.Pdf = Nothing
        If Load(oPdf, oDocFile.Stream, DTODocFile.THUMB_WIDTH, DTODocFile.THUMB_HEIGHT, exs) Then
            With oDocFile
                .size = oPdf.Size
                .pags = oPdf.PageCount
                .Thumbnail = oPdf.Thumbnail
            End With
            retval = True
        End If
        Return retval
    End Function


    Shared Function GetPdfThumbnail(oFileBytes As Byte(), iMaxWidth As Integer, iMaxHeight As Integer) As Image
        Dim oPdf As GhostScriptHelper.Pdf = Nothing
        Dim exs As New List(Of Exception)
        Dim retval As Image = Nothing
        If Load(oPdf, oFileBytes, iMaxWidth, iMaxHeight, exs) Then
            retval = oPdf.Thumbnail
        End If
        Return retval
    End Function

    Shared Function GetThumbnail(oStream As System.IO.MemoryStream, iMaxWidth As Integer, iMaxHeight As Integer) As Image
        Dim oPdf As GhostScriptHelper.Pdf = Nothing
        Dim exs As New List(Of Exception)
        Dim retval As Image = Nothing
        If Load(oPdf, oStream, iMaxWidth, iMaxHeight, exs) Then
            retval = oPdf.Thumbnail
        End If
        Return retval
    End Function

    Shared Function Load(sFilename As String, oByteArray As Byte(), maxWidth As Integer, MaxHeight As Integer, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        If FileSystemHelper.GetStreamFromFile(sFilename, oByteArray, exs) Then
            Dim oPdf As New GhostScriptHelper.Pdf
            retval = Load(oPdf, oByteArray, maxWidth, MaxHeight, exs)
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oPdf As GhostScriptHelper.Pdf, oByteArray() As Byte, maxWidth As Integer, maxHeight As Integer, exs As List(Of Exception)) As Boolean
        Dim oStream As New System.IO.MemoryStream(oByteArray)
        Dim retval As Boolean = Load(oPdf, oStream, maxWidth, maxHeight, exs)
        Return retval
    End Function

    Shared Function Load(ByRef oPdf As GhostScriptHelper.Pdf, sFilename As String, maxWidth As Integer, maxHeight As Integer, exs As List(Of Exception)) As Boolean
        Dim oStream As System.IO.Stream = New IO.FileStream(sFilename, IO.FileMode.Open, IO.FileAccess.Read)
        Dim retval As Boolean = Load(oPdf, oStream, maxWidth, maxHeight, exs)
        Return retval
    End Function

    Shared Function Load(ByRef oPdf As GhostScriptHelper.Pdf, oStream As System.IO.Stream, maxWidth As Integer, maxHeight As Integer, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = GhostScriptHelper.Rasterize(oStream, oPdf, exs)
        If retval Then
            If maxWidth = 0 And maxHeight = 0 Then
                oPdf.Thumbnail = oPdf.Portrait
            Else
                oPdf.Thumbnail = MatHelperStd.ImageHelper.GetThumbnailToFill(oPdf.Portrait, maxWidth, maxHeight)
            End If
        End If
        Return retval
    End Function

    Shared Function ThumbnailPreview(oDocFile As DTODocFile) As SixLabors.ImageSharp.Image
        Dim retval As SixLabors.ImageSharp.Image = Nothing
        If oDocFile IsNot Nothing Then
            Select Case oDocFile.mime
                Case MimeCods.Bmp, MimeCods.Gif, MimeCods.Jpg, MimeCods.Png, MimeCods.Tif, MimeCods.Tiff
                    Dim oImg As SixLabors.ImageSharp.Image = oDocFile.Thumbnail
                    retval = MatHelperStd.ImageHelper.GetThumbnailToFill(oImg, 100, 100)
                Case MimeCods.Pdf
                    'retval = ImageHelper.Converter(My.Resources.pdf_100)
            End Select
        End If
        Return retval
    End Function

End Class
