
Public Class DocfileHelper


    Shared Function ZipThumbnail(oByteArray As Byte()) As Byte()
        Dim retval As Byte() = Nothing
        Dim sFilenames As List(Of String) = LegacyHelper.ZipHelper.Filenames(oByteArray)
        If sFilenames.Count > 0 Then
            Dim oFiles = LegacyHelper.ZipHelper.Extract(oByteArray)
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

    Shared Function LoadFromString(src As String) As DTODocFile
        Dim oByteArray = Text.Encoding.UTF8.GetBytes(src)
        Dim retval As New DTODocFile()
        With retval
            .Hash = ASINHelper.GenerateRandomASIN()
            .Sha256 = CryptoHelper.Sha256(oByteArray)
            .Thumbnail = TextThumbnail(src)
            .FchCreated = DTO.GlobalVariables.Now()
            .Stream = oByteArray
            .Mime = MimeCods.Txt
            .Length = oByteArray.Length
        End With
        Return retval
    End Function

    Shared Function TextThumbnail(src As String) As Byte()
        Dim oThumbnail As New System.Drawing.Bitmap(DTODocFile.THUMB_WIDTH, DTODocFile.THUMB_HEIGHT)
        Dim oGraphics As Graphics = Graphics.FromImage(oThumbnail)
        oGraphics.FillRectangle(Brushes.White, 0, 0, oThumbnail.Width, oThumbnail.Height)
        Dim oFont = New Font("Helvetica", 10, FontStyle.Regular)
        Dim padding As Integer = 10
        Dim oRectangleF As New System.Drawing.RectangleF(padding, padding, DTODocFile.THUMB_WIDTH - 2 * padding, DTODocFile.THUMB_HEIGHT - 2 * padding)
        Dim sf As New StringFormat()
        oGraphics.DrawString(src, oFont, Brushes.Black, oRectangleF, sf)
        Dim retval As Byte() = Nothing
        Using ms As New IO.MemoryStream()
            oThumbnail.Save(ms, Imaging.ImageFormat.Jpeg)
            retval = ms.ToArray()
        End Using
        Return retval
    End Function

    Shared Function Factory(sFilename As String, exs As List(Of Exception)) As DTODocFile
        Dim retval As DTODocFile = Nothing
        Dim oByteArray As Byte() = Nothing
        If FileSystemHelper.GetStreamFromFile(sFilename, oByteArray, exs) Then
            Dim oMime = MimeHelper.GetMimeFromExtension(sFilename)
            retval = Factory(exs, oByteArray, oMime)
            retval.filename = sFilename
        End If
        Return retval
    End Function

    Shared Function LoadFromStream(oByteArray As Byte(), ByRef oDocFile As DTODocFile, exs As List(Of Exception), Optional sFilename As String = "") As Boolean
        Dim retval As Boolean
        If oByteArray Is Nothing Then
            exs.Add(New Exception("ByteArray buit al generar el DocFile"))
        Else
            oDocFile = New DTODocFile
            With oDocFile
                .Hash = AsinHelper.GenerateRandomASIN()
                .Sha256 = CryptoHelper.Sha256(oByteArray)
                .Stream = oByteArray
                .length = oByteArray.Length
                If sFilename > "" Then
                    .Filename = sFilename
                    .Mime = MimeHelper.GetMimeFromExtension(sFilename)
                End If
                If .mime = MimeCods.NotSet Then
                    .mime = MimeHelper.GuessMime(oByteArray)
                End If
                If LoadMimeDetails(oDocFile, exs) Then
                    retval = True
                End If
            End With
        End If
        Return retval
    End Function

    Shared Function LoadMimeDetails(oDocFile As DTODocFile, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = True
        With oDocFile
            Select Case .mime
                Case MimeCods.Pdf
                    retval = LoadPdf(oDocFile, exs)
                Case MimeCods.Rtf
                    '.Thumbnail = LegacyHelper.ImageHelper.Converter(My.Resources.word)
                Case MimeCods.Zip
                    Dim oFiles = ZipHelper.Extract(.Stream)
                    If oFiles.Count > 0 Then
                        Dim oFirstDocfile = DocfileHelper.Factory(exs, oFiles.First.ByteArray)
                        .Thumbnail = oFirstDocfile.Thumbnail
                    End If
                Case MimeCods.Jpg, MimeCods.Gif, MimeCods.Png, MimeCods.Bmp, MimeCods.Tif, MimeCods.Tiff
                    Dim ms As New IO.MemoryStream(.Stream)
                    Dim oImg = System.Drawing.Image.FromStream(ms)
                    .Size = New Size(oImg.Width, oImg.Height)
                    .VRes = oImg.VerticalResolution
                    .HRes = oImg.HorizontalResolution
                    Dim oThumbnail As System.Drawing.Image = LegacyHelper.ImageHelper.GetThumbnailToFit(oImg, DTODocFile.THUMB_WIDTH, DTODocFile.THUMB_HEIGHT)
                    Using ms2 As New System.IO.MemoryStream
                        oThumbnail.Save(ms2, Imaging.ImageFormat.Jpeg)
                        .Thumbnail = ms2.ToArray()
                    End Using
                Case MimeCods.Ppt, MimeCods.Pptx
                Case MimeCods.Docx
                Case MimeCods.Txt
                    Dim src As String = System.Text.ASCIIEncoding.ASCII.GetString(oDocFile.Stream)
                    Dim oThumbnail = LegacyHelper.ImageHelper.GetThumbnailFromText(src, DTODocFile.THUMB_WIDTH, DTODocFile.THUMB_HEIGHT)
                    .Thumbnail = oThumbnail.Bytes()

                Case MimeCods.Mov, MimeCods.Mp4, MimeCods.Mpg
                    Dim oImg As Image = My.Resources.video
                    .Thumbnail = ImageHelper.GetThumbnailToFit(oImg, DTODocFile.THUMB_WIDTH, DTODocFile.THUMB_HEIGHT).Bytes()
                Case Else
                    Dim oImg As Image = My.Resources.cartadeajuste
                    .Thumbnail = ImageHelper.GetThumbnailToFit(oImg, DTODocFile.THUMB_WIDTH, DTODocFile.THUMB_HEIGHT).Bytes()

            End Select
        End With
        Return retval
    End Function


    Shared Function LoadFromStream(exs As List(Of Exception), ByRef oDocfile As DTODocFile, oByteArray As Byte(), Optional oMime As MimeCods = MimeCods.NotSet) As Boolean
        Try
            With oDocfile
                .Hash = AsinHelper.GenerateRandomASIN()
                .Sha256 = CryptoHelper.Sha256(oByteArray)
                .FchCreated = DTO.GlobalVariables.Now()
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
                        .Size = New Size(oPdf.Width, oPdf.Height)
                    Case MimeCods.Rtf
                        '.Thumbnail = LegacyHelper.ImageHelper.Converter(My.Resources.word)
                    Case MimeCods.Zip
                        .Thumbnail = DocfileHelper.ZipThumbnail(.Stream)
                    Case MimeCods.Jpg, MimeCods.Gif, MimeCods.Png, MimeCods.Bmp, MimeCods.Tif, MimeCods.Tiff
                        Dim ms As New IO.MemoryStream(.Stream)
                        Dim oImg = Image.FromStream(ms)
                        .Size = New Size(oImg.Width, oImg.Height)
                        .VRes = oImg.VerticalResolution
                        .HRes = oImg.HorizontalResolution
                        .Thumbnail = ImageHelper.GetThumbnailToFit(oImg, DTODocFile.THUMB_WIDTH, DTODocFile.THUMB_HEIGHT).Bytes()
                    Case MimeCods.Ppt, MimeCods.Pptx
                    Case MimeCods.Docx
                        .Thumbnail = WordHelper.GetImgFromWordFirstPage(.Stream, exs)
                    Case MimeCods.Xlsx
                        Dim iExcelRows As Integer = 0
                        Dim iExcelCols As Integer = 0
                        '.Thumbnail = MatHelper.Excel.GetImgFromExcelFirstPage(_DocFile.Stream, iExcelCols, iExcelRows)
                        '.Thumbnail = My.Resources.Excel_Big
                        .Thumbnail = ExcelThumbnail.GetExcelThumbnail(.Stream, exs)
                        .Size = New Size(iExcelCols, iExcelRows)
                    Case MimeCods.Txt
                        Dim src As String = System.Text.ASCIIEncoding.ASCII.GetString(oByteArray)
                        '.Thumbnail = ImageHelper.GetThumbnailFromText(src, DTODocFile.THUMB_WIDTH, DTODocFile.THUMB_HEIGHT)
                    Case MimeCods.Mov, MimeCods.Mp4
                        Dim oImg As Image = My.Resources.video
                        .Thumbnail = ImageHelper.GetThumbnailToFit(oImg, DTODocFile.THUMB_WIDTH, DTODocFile.THUMB_HEIGHT).Bytes()
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
                .Size = New Size(oPdf.Width, oPdf.Height)
                .pags = oPdf.PageCount
                .Thumbnail = oPdf.Thumbnail
            End With
            retval = True
        End If
        Return retval
    End Function


    Shared Function GetPdfThumbnail(oFileBytes As Byte(), iMaxWidth As Integer, iMaxHeight As Integer) As Byte()
        Dim oPdf As GhostScriptHelper.Pdf = Nothing
        Dim exs As New List(Of Exception)
        Dim retval As Byte() = Nothing
        If Load(oPdf, oFileBytes, iMaxWidth, iMaxHeight, exs) Then
            retval = oPdf.Thumbnail
        End If
        Return retval
    End Function

    Shared Function GetThumbnail(oStream As System.IO.MemoryStream, iMaxWidth As Integer, iMaxHeight As Integer) As Byte()
        Dim oPdf As GhostScriptHelper.Pdf = Nothing
        Dim exs As New List(Of Exception)
        Dim retval As Byte() = Nothing
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
    Shared Function LoadToFit(ByRef oPdf As GhostScriptHelper.Pdf, oByteArray() As Byte, maxWidth As Integer, maxHeight As Integer, exs As List(Of Exception)) As Boolean
        Dim oStream As New System.IO.MemoryStream(oByteArray)
        Dim retval As Boolean = LoadToFit(oPdf, oStream, maxWidth, maxHeight, exs)
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
                oPdf.Thumbnail = LegacyHelper.ImageHelper.GetThumbnailToFill(ImageHelper.FromBytes(oPdf.Portrait), maxWidth, maxHeight).Bytes()
            End If
        End If
        Return retval
    End Function

    Shared Function LoadToFit(ByRef oPdf As GhostScriptHelper.Pdf, oStream As System.IO.Stream, maxWidth As Integer, maxHeight As Integer, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = GhostScriptHelper.Rasterize(oStream, oPdf, exs)
        If retval Then
            If maxWidth = 0 And maxHeight = 0 Then
                oPdf.Thumbnail = oPdf.Portrait
            Else
                Dim oPortrait = LegacyHelper.ImageHelper.FromBytes(oPdf.Portrait)
                oPdf.Thumbnail = LegacyHelper.ImageHelper.GetThumbnailToFit(oPortrait, maxWidth, maxHeight).Bytes()
            End If
        End If
        Return retval
    End Function

    Shared Function ThumbnailPreview(oDocFile As DTODocFile) As Byte()
        Dim retval As Byte() = Nothing
        If oDocFile IsNot Nothing Then
            Select Case oDocFile.Mime
                Case MimeCods.Bmp, MimeCods.Gif, MimeCods.Jpg, MimeCods.Png, MimeCods.Tif, MimeCods.Tiff
                    Dim oImg = ImageHelper.FromBytes(oDocFile.Thumbnail)
                    retval = ImageHelper.GetThumbnailToFill(oImg, 100, 100).Bytes()
                Case MimeCods.Pdf
                    'retval = LegacyHelper.ImageHelper.Converter(My.Resources.pdf_100)
            End Select
        End If
        Return retval
    End Function

End Class
