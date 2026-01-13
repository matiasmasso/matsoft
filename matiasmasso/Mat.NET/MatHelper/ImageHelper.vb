
Imports System.Drawing.Imaging
Imports DTO.DTOEnums

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

            Dim sMime As String = GetImageMediaType(oImg)
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


    Shared Function GetImageMimeCod(oImage As System.Drawing.Image) As MimeCods
        Dim oImageFormat As System.Drawing.Imaging.ImageFormat = oImage.RawFormat
        Dim retval = MimeCods.Jpg
        If (oImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg)) Then
            retval = MimeCods.Jpg
        ElseIf (oImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif)) Then
            retval = MimeCods.Gif
        ElseIf (oImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Png)) Then
            retval = MimeCods.Png
        ElseIf (oImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Tiff)) Then
            retval = MimeCods.Tif
        ElseIf (oImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp)) Then
            retval = MimeCods.Bmp
        End If
        Return retval
    End Function



    Shared Function GetImageMediaType(oImage As System.Drawing.Image) As String
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

    Shared Function CropImage(OriginalImage As Image, CropWidth As Integer, CropHeight As Integer, Optional CropX As Integer = 0, Optional CropY As Integer = 0) As Image
        Dim CropRect As New Rectangle(CropX, CropY, CropWidth, CropHeight)
        Dim retval As New Bitmap(CropRect.Width, CropRect.Height)
        If OriginalImage IsNot Nothing Then
            Using grp = Graphics.FromImage(retval)
                grp.DrawImage(OriginalImage, New Rectangle(0, 0, CropRect.Width, CropRect.Height), CropRect, GraphicsUnit.Pixel)
                OriginalImage.Dispose()
            End Using
        End If
        Return retval
    End Function

    Shared Function DownloadFromWebsite(ByVal url As String) As Image
        Dim retval As Image = Nothing
        Try
            'Creamos la petición
            Dim request As System.Net.WebRequest = System.Net.WebRequest.Create(url)
            Dim response As System.Net.WebResponse = request.GetResponse()
            Dim responseStream As System.IO.Stream = response.GetResponseStream()
            retval = New Bitmap(responseStream)
        Catch ex As Exception
        End Try
        Return retval
    End Function

    Shared Function GuessMime(oImage As System.Drawing.Image) As MimeCods
        Dim oByteArray As Byte() = MatHelper.ImageHelper.GetByteArrayFromImg(oImage)
        Dim retval As MimeCods = GuessMime(oByteArray)
        Return retval
    End Function

    Shared Function GuessMime(oByteArray As Byte()) As MimeCods
        ' see http://www.mikekunz.com/image_file_header.html  

        Dim retval As MimeCods = MimeCods.NotSet
        If oByteArray IsNot Nothing Then
            Dim bmp As Byte() = System.Text.Encoding.ASCII.GetBytes("BM")
            Dim gif As Byte() = System.Text.Encoding.ASCII.GetBytes("GIF")
            Dim mp4 As Byte() = System.Text.Encoding.ASCII.GetBytes("MP41")
            Dim png As Byte() = {137, 80, 78, 71}
            Dim tiff As Byte() = {73, 73, 42}
            Dim tiff2 As Byte() = {77, 77, 42}
            Dim jpeg As Byte() = {255, 216, 255, 224} 'hex = "FF-D8"
            Dim jpeg2 As Byte() = {255, 216, 255, 225}

            If bmp.SequenceEqual(oByteArray.Take(bmp.Length)) Then
                retval = MimeCods.Bmp
            ElseIf gif.SequenceEqual(oByteArray.Take(gif.Length)) Then
                retval = MimeCods.Gif
            ElseIf png.SequenceEqual(oByteArray.Take(png.Length)) Then
                retval = MimeCods.Png
            ElseIf tiff.SequenceEqual(oByteArray.Take(tiff.Length)) Then
                retval = MimeCods.Tiff
            ElseIf tiff2.SequenceEqual(oByteArray.Take(tiff2.Length)) Then
                retval = MimeCods.Tiff
            ElseIf jpeg.SequenceEqual(oByteArray.Take(jpeg.Length)) Then
                retval = MimeCods.Jpg
            ElseIf jpeg2.SequenceEqual(oByteArray.Take(jpeg2.Length)) Then
                retval = MimeCods.Jpg
            ElseIf mp4.SequenceEqual(oByteArray.Take(mp4.Length)) Then
                retval = MimeCods.Mp4
            End If
        End If
        Return retval
    End Function

    Shared Function GetImageFormat(oMimeCod As MimeCods) As ImageFormat
        Dim retval As ImageFormat = Nothing
        Select Case oMimeCod
            Case MimeCods.Gif
                retval = ImageFormat.Gif
            Case MimeCods.Jpg
                retval = ImageFormat.Jpeg
            Case MimeCods.Bmp
                retval = ImageFormat.Bmp
            Case MimeCods.Tif, MimeCods.Tiff
                retval = ImageFormat.Tiff
            Case MimeCods.Png
                retval = ImageFormat.Png
            Case Else
                retval = ImageFormat.Wmf
        End Select
        Return retval
    End Function

    Shared Function GetThumbnailFromText(src As String, iWidth As Integer, iHeight As Integer) As System.Drawing.Image
        Dim FontColor As Color = Color.Navy
        Dim BackColor As Color = Color.White
        Dim FontName As String = "Arial"
        Dim FontSize As Integer = 14
        Dim oBitmap As New Bitmap(794, 1123) '96dpi, 595x842 px a 72dpi
        Dim oGraphics As Graphics = Graphics.FromImage(oBitmap)
        Dim oFont As New Font(FontName, FontSize)
        Dim oPoint As New PointF(5.0F, 5.0F)
        Dim oBrushForeColor As New SolidBrush(FontColor)
        Dim oBrushBackColor As New SolidBrush(BackColor)
        oGraphics.FillRectangle(oBrushBackColor, 0, 0, oBitmap.Width, oBitmap.Height)
        oGraphics.DrawString(src, oFont, oBrushForeColor, oPoint)

        Dim oSizeF As SizeF = oGraphics.MeasureString(src, oFont)
        Dim oCropRect As New Rectangle(0, 0, CInt(oSizeF.Width), CInt(oSizeF.Height))
        Dim oCropImage As New Bitmap(oCropRect.Width, oCropRect.Height)
        Using grp = Graphics.FromImage(oCropImage)
            grp.DrawImage(oBitmap, New Rectangle(0, 0, oCropRect.Width, oCropRect.Height), oCropRect, GraphicsUnit.Pixel)
            oBitmap.Dispose()
        End Using

        Dim retval As Image = GetThumbnailToFit(oCropImage, iWidth, iHeight)
        Return retval
    End Function


    Shared Function IsImage(sFilename As String) As Boolean
        Dim retval As Boolean
        Dim iPos As Integer = sFilename.LastIndexOf(".")
        If iPos > 0 Then
            Select Case sFilename.Substring(iPos)
                Case ".jpg", ".jpeg", ".gif", ".tiff", ".tif", ".bmp", ".png", ".eps"
                    retval = True
            End Select
        End If
        Return retval
    End Function

    Shared Function IsImage(oMime As MimeCods) As Boolean
        Dim retval As Boolean
        Select Case oMime
            Case MimeCods.Jpg, MimeCods.Gif, MimeCods.Tif, MimeCods.Tiff, MimeCods.Bmp, MimeCods.Png, MimeCods.Eps
                retval = True
        End Select
        Return retval
    End Function
End Class
