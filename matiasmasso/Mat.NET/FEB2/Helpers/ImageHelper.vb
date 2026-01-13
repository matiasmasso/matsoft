Imports System.Drawing

Public Class ImageHelper
    Shared CallBack As New Image.GetThumbnailImageAbort(AddressOf MycallBack)

    Shared Function MycallBack() As Boolean
        Return False
    End Function

    Shared Function GetThumbnailToFit(ByVal oBmp As Bitmap, Optional ByVal finalWidth As Integer = 0, Optional ByVal finalHeight As Integer = 0) As Bitmap
        Dim retval As Bitmap = Nothing

        If Not oBmp Is Nothing Then
            Dim iWidth As Integer = oBmp.Width
            Dim iHeight As Integer = oBmp.Height

            If finalWidth > 0 Then
                If iWidth > finalWidth Then
                    iHeight = iHeight * finalWidth / iWidth
                    iWidth = finalWidth
                End If
            End If

            If finalHeight > 0 Then
                If iHeight > finalHeight Then
                    iWidth = iWidth * finalHeight / iHeight
                    iHeight = finalHeight
                End If
            End If

            If iWidth = oBmp.Width And iHeight = oBmp.Height Then
                retval = oBmp
            Else
                retval = oBmp.GetThumbnailImage(iWidth, iHeight, CallBack, System.IntPtr.Zero) 'New System.IntPtr(0))
            End If

        End If

        Return retval
    End Function


    Shared Function GetThumbnailToFill(ByVal oBmp As Bitmap, Optional ByVal finalWidth As Integer = 0, Optional ByVal finalHeight As Integer = 0) As Bitmap
        Dim retval As Bitmap = Nothing

        If Not oBmp Is Nothing Then
            Dim iWidth As Integer = oBmp.Width
            Dim iHeight As Integer = oBmp.Height
            Dim iReducedWidth As Integer
            Dim iReducedHeight As Integer
            Dim X As Integer
            Dim Y As Integer

            'factors de reducció %
            Dim DcWidthFactor As Decimal = finalWidth / iWidth
            Dim DcHeightFactor As Decimal = finalHeight / iHeight

            If DcWidthFactor > DcHeightFactor Then 'portrait. Descarta header & footer
                iReducedWidth = finalWidth
                iReducedHeight = iHeight * finalWidth / iWidth
                X = 0
                Y = (iReducedHeight - finalHeight) / 2
            Else 'landscape. Descarta els costats
                iReducedWidth = iWidth * finalHeight / iHeight
                iReducedHeight = finalHeight
                X = (iReducedWidth - finalWidth) / 2
                Y = 0
            End If
            Dim oReducedBmp As Bitmap = oBmp.GetThumbnailImage(iReducedWidth, iReducedHeight, CallBack, System.IntPtr.Zero)
            Dim oRectangle As New Rectangle(X, Y, finalWidth, finalHeight)
            retval = oReducedBmp.Clone(oRectangle, oReducedBmp.PixelFormat)
        End If

        Return retval
    End Function

    Shared Function GetThumbnailToFitAndFill(ByVal oBmp As Bitmap, Optional ByVal finalWidth As Integer = 0, Optional ByVal finalHeight As Integer = 0) As Bitmap
        Dim Picture As Bitmap = GetThumbnailToFit(oBmp, finalWidth, finalHeight)
        Dim retval As New Bitmap(finalWidth, finalHeight)

        Dim X As Integer = (retval.Width - Picture.Width) / 2
        Dim Y As Integer = (retval.Height - Picture.Height) / 2
        Using GraphicsObject As Graphics = Graphics.FromImage(retval)
            Dim oRectangle As RectangleF = retval.GetBounds(GraphicsUnit.Pixel)
            GraphicsObject.FillRectangle(Brushes.White, oRectangle)
            GraphicsObject.DrawImage(Picture, X, Y)
        End Using

        Return retval
    End Function

    Shared Function Sprite(items As IEnumerable(Of Image), Optional defaultImage As Image = Nothing) As Image
        Dim retval As Image = Nothing
        If items IsNot Nothing AndAlso items.Count > 0 Then
            Dim iItemWidth As Integer = items.First.Width
            Dim itemHeight As Integer = items.First.Height
            If defaultImage Is Nothing Then defaultImage = New Bitmap(iItemWidth, itemHeight)
            Dim ColumnsCount As Integer = SpriteHelper.MAXCOLUMNS
            Dim iRows As Integer = Fix((items.Count - 1) / ColumnsCount) + 1
            Dim iCols As Integer = IIf(items.Count > ColumnsCount, ColumnsCount, items.Count)
            retval = New Bitmap(iCols * iItemWidth, iRows * itemHeight)

            Dim iCol As Integer = 0
            Dim iRow As Integer = 0
            Using GraphicsObject As Graphics = Graphics.FromImage(retval)
                ' uncomment for higher quality output
                'graph.InterpolationMode = InterpolationMode.High;
                'graph.CompositingQuality = CompositingQuality.HighQuality;
                'graph.SmoothingMode = SmoothingMode.AntiAlias;
                For Each item As System.Drawing.Image In items
                    Dim X As Integer = iCol * iItemWidth
                    Dim Y As Integer = iRow * itemHeight
                    Dim oRectangle As New RectangleF(X, Y, iItemWidth, itemHeight)
                    GraphicsObject.FillRectangle(Brushes.White, oRectangle)
                    If item Is Nothing Then
                        GraphicsObject.DrawImage(defaultImage, oRectangle)
                    Else
                        GraphicsObject.DrawImage(item, oRectangle)
                    End If

                    iCol += 1
                    If iCol >= ColumnsCount Then
                        iRow += 1
                        iCol = 0
                    End If
                Next
            End Using
        End If
        Return retval
    End Function

    Shared Function GetImgFromByteArray(ByVal oByteArray As Byte()) As Image
        Dim retval As Image = Nothing
        Dim ImageStream As System.IO.MemoryStream
        Try
            If oByteArray.GetUpperBound(0) > 0 Then
                ImageStream = New System.IO.MemoryStream(oByteArray)
                retval = Image.FromStream(ImageStream)
            Else
                retval = Nothing
            End If
        Catch ex As Exception
            retval = Nothing
        End Try

        Return retval
    End Function


    Shared Function GetByteArrayFromImg(ByVal oImg As System.Drawing.Image) As Byte()
        Dim retVal As Byte()
        If oImg Is Nothing Then
            retVal = Nothing
        Else
            Dim oMemStream As New IO.MemoryStream()
            'oImg.Save(oMemStream, System.Drawing.Imaging.ImageFormat.Jpeg)

            Dim sMime As String = GetMimeFromImage(oImg)
            Dim encoder As System.Drawing.Imaging.ImageCodecInfo = GetEncoderInfoFromMime(sMime)

            'Set image quality to reduce image and file size ...
            Dim encoderParams As New System.Drawing.Imaging.EncoderParameters(1)
            encoderParams.Param(0) = New System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100)

            oImg.Save(oMemStream, encoder, encoderParams)
            retVal = oMemStream.ToArray
            oMemStream.Close()
        End If
        Return retVal
    End Function


    Shared Function GetMimeCodFromImage(oImage As System.Drawing.Image) As DTOEnums.MimeCods
        Dim oImageFormat As System.Drawing.Imaging.ImageFormat = oImage.RawFormat
        Dim retval As DTOEnums.MimeCods = DTOEnums.MimeCods.Jpg
        If (oImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg)) Then
            retval = DTOEnums.MimeCods.Jpg
        ElseIf (oImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif)) Then
            retval = DTOEnums.MimeCods.Gif
        ElseIf (oImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Png)) Then
            retval = DTOEnums.MimeCods.Png
        ElseIf (oImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Tiff)) Then
            retval = DTOEnums.MimeCods.Tiff
        ElseIf (oImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp)) Then
            retval = DTOEnums.MimeCods.Bmp
        End If
        Return retval
    End Function

    Shared Function GetMimeFromImage(oImage As System.Drawing.Image) As String
        Dim oImageFormat As System.Drawing.Imaging.ImageFormat = oImage.RawFormat
        Dim retval As String = "image/jpeg" 'default
        If (oImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg)) Then
            retval = "image/jpeg"
        ElseIf (oImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif)) Then
            retval = "image/gif"
        ElseIf (oImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Png)) Then
            retval = "image/png"
        ElseIf (oImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Tiff)) Then
            retval = "image/tiff"
        ElseIf (oImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp)) Then
            retval = "image/bmp"
        End If
        Return retval
    End Function

    Shared Function GetMimeFromContent(ByVal sFileName As String) As DTOEnums.MimeCods
        Dim retval As DTOEnums.MimeCods '= FileSystemHelper.GetMimeFromExtension(sFileName) bucle
        If retval = DTOEnums.MimeCods.NotSet Then
            Try
                Dim oFile As New IO.FileInfo(sFileName)
                Dim sr As IO.StreamReader = New IO.StreamReader(sFileName)
                Dim line As String
                line = sr.ReadLine()
                If line.Substring(0, 13) = "<?xml version" Then
                    retval = DTOEnums.MimeCods.Xml
                ElseIf line.Substring(0, 4) = "mp41" Then
                    retval = DTOEnums.MimeCods.Mp4
                End If
            Catch ex As Exception
            End Try

        End If
        Return retval
    End Function

    Shared Function GetEncoderInfoFromMime(sMimeType As String) As System.Drawing.Imaging.ImageCodecInfo
        Dim j As Integer
        Dim retval As System.Drawing.Imaging.ImageCodecInfo = Nothing
        Dim encoders() As System.Drawing.Imaging.ImageCodecInfo = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders
        For j = 0 To encoders.Length - 1
            If encoders(j).MimeType = sMimeType Then
                retval = encoders(j)
            End If
        Next
        Return retval
    End Function


End Class
