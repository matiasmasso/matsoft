Imports System.Drawing

Public Class ImageHelper

    Shared Function Sprite(items As IEnumerable(Of Image), Optional defaultImage As Image = Nothing) As Image
        Dim retval As Image = Nothing
        If items IsNot Nothing AndAlso items.Count > 0 Then
            Dim oFirstItem = defaultImage
            If oFirstItem Is Nothing Then oFirstItem = items.FirstOrDefault(Function(x) x IsNot Nothing)
            Dim iItemWidth As Integer = oFirstItem.Width
            Dim itemHeight As Integer = oFirstItem.Height
            If defaultImage Is Nothing Then defaultImage = New Bitmap(iItemWidth, itemHeight)
            Dim ColumnsCount As Integer = DTOSprite.MAXCOLUMNS
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
            'Dim oMemStream As New IO.MemoryStream()
            'oImg.Save(oMemStream, System.Drawing.Imaging.ImageFormat.Jpeg)

            Dim sMime As String = GetMimeFromImage(oImg)
            Dim encoder As System.Drawing.Imaging.ImageCodecInfo = GetEncoderInfoFromMime(sMime)

            'Set image quality to reduce image and file size ...
            Dim encoderParams As New System.Drawing.Imaging.EncoderParameters(1)
            encoderParams.Param(0) = New System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100)

            'oImg.Save(oMemStream, encoder, encoderParams)
            'retVal = oMemStream.ToArray
            'oMemStream.Close()

            Dim src As New Bitmap(oImg)
            Using oMemStream As New IO.MemoryStream()
                src.Save(oMemStream, encoder, encoderParams)
                retVal = oMemStream.ToArray
            End Using

        End If
        Return retVal
    End Function


    Shared Function GetMimeCodFromImage(oImage As System.Drawing.Image) As MimeCods
        Dim oImageFormat As System.Drawing.Imaging.ImageFormat = oImage.RawFormat
        Dim retval As MimeCods = MimeCods.Jpg
        If (oImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg)) Then
            retval = MimeCods.Jpg
        ElseIf (oImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif)) Then
            retval = MimeCods.Gif
        ElseIf (oImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Png)) Then
            retval = MimeCods.Png
        ElseIf (oImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Tiff)) Then
            retval = MimeCods.Tiff
        ElseIf (oImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp)) Then
            retval = MimeCods.Bmp
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
End Class
