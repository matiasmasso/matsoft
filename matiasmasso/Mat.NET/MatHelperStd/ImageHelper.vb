Imports System.IO
Imports SixLabors.ImageSharp
Imports SixLabors.ImageSharp.Formats
Imports SixLabors.ImageSharp.Formats.Jpeg
Imports SixLabors.ImageSharp.Processing

Public Class ImageHelper

    Shared Function MycallBack() As Boolean
        Return False
    End Function


    Shared Function Resize(oByteArray As Byte(), width As Integer, height As Integer) As Byte()
        Dim retval As Byte() = Nothing
        Using outStream As New MemoryStream
            Using src As Image = Image.Load(oByteArray)
                Dim clone = src.Clone(Function(context) context.Resize(New ResizeOptions With {
                .Mode = ResizeMode.Max,
                .Size = New Size(width, height)
                }))
                retval.Save(outStream, New JpegEncoder With {.Quality = 100})
            End Using
        End Using
        Return retval
    End Function

    Shared Function resize(src As Image, width As Integer, height As Integer) As Image
        Dim retval = src.Clone(Function(i) i.Resize(width, height)) '.Crop(New Rectangle(x, y, cropWidth, cropHeight)))
        Return retval
    End Function

    Shared Function crop(src As Image, x As Integer, y As Integer, width As Integer, height As Integer) As Image
        Dim oCropRectangle As New Rectangle(x, y, width, height)
        Dim retval = src.Clone(Function(i) i.Crop(oCropRectangle))
        Return retval
    End Function

    Shared Function memoryStream(oImage As Image, oMime As MimeCods) As MemoryStream
        Dim retval As New MemoryStream
        Select Case oMime
            Case MimeCods.Jpg
                oImage.Save(retval, New JpegEncoder With {.Quality = 100})
            Case MimeCods.Gif
                oImage.Save(retval, New SixLabors.ImageSharp.Formats.Gif.GifEncoder)
            Case Else
                oImage.Save(retval, New JpegEncoder With {.Quality = 100})
        End Select
        Return retval
    End Function



    Shared Function GetThumbnailToFit(ByVal oBmp As Image, Optional ByVal finalWidth As Integer = 0, Optional ByVal finalHeight As Integer = 0) As Image
        Dim retval As Image = Nothing

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
                retval = resize(oBmp, iWidth, iHeight)
            End If

        End If

        Return retval
    End Function


    Shared Function GetThumbnailToFill(ByVal src As Image, Optional ByVal finalWidth As Integer = 0, Optional ByVal finalHeight As Integer = 0) As Image
        Dim retval As Image = Nothing

        If Not src Is Nothing Then
            Dim iWidth As Integer = src.Width
            Dim iHeight As Integer = src.Height
            Dim DcWidthFactor As Decimal = finalWidth / iWidth
            Dim DcHeightFactor As Decimal = finalHeight / iHeight
            Dim DcFactor = Math.Max(DcWidthFactor, DcHeightFactor)
            Dim X = (iWidth * DcFactor - finalWidth) / 2
            Dim Y = (iHeight * DcFactor - finalHeight) / 2
            Dim oRect As New Rectangle(X, Y, finalWidth, finalHeight)
            retval = src.Clone(Function(i) i.Resize(iWidth * DcFactor, iHeight * DcFactor))
            retval.Mutate(Function(i) i.Crop(oRect))
        End If

        Return retval
    End Function

    Shared Function GetByteArrayFromImg(ByVal oImg As Image, Optional mime As MimeCods = MimeCods.Jpg) As Byte()
        Dim retval As Byte() = Nothing
        If oImg IsNot Nothing Then
            Using oStream As New System.IO.MemoryStream
                Select Case mime
                    Case MimeCods.Gif
                        oImg.SaveAsGif(oStream)
                    Case Else
                        oImg.SaveAsJpeg(oStream)
                End Select
                retval = oStream.ToArray
            End Using
        End If
        Return retval
    End Function


    Shared Function GetImgFromByteArray(ByVal oByteArray As Byte(), Optional ByRef oMime As MimeCods = MimeCods.NotSet) As Image
        Dim oImageFormat As IImageFormat = Nothing
        Dim retval As Image = Image.Load(oByteArray, oImageFormat)
        Dim sMimeNom = oImageFormat.Name
        If Not [Enum].TryParse(Of MimeCods)(sMimeNom, oMime) Then
            oMime = MimeCods.Jpg
        End If
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

    Shared Function fromStream(oStream As Stream) As Image
        Dim retval = Image.Load(oStream)
        Return retval
    End Function


    Shared Function DownloadFromWebsite(exs As List(Of Exception), ByVal url As String) As Image
        Dim retval As Image = Nothing
        Try
            'Creamos la petición
            Dim request As System.Net.WebRequest = System.Net.WebRequest.Create(url)
            Dim response As System.Net.WebResponse = request.GetResponse()
            Dim responseStream As System.IO.Stream = response.GetResponseStream()
            retval = Image.Load(responseStream)
        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval
    End Function

    Shared Async Function DownloadFromWebsiteAsync(exs As List(Of Exception), ByVal url As String) As Task(Of Image)
        Dim retval As Image = Nothing
        Try
            retval = Await Task.Run(Function() DownloadFromWebsite(exs, url))
        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval
    End Function

    Shared Function ImageMime(oByteArray As Byte()) As ImageMime
        Dim retval As ImageMime = New ImageMime()
        Dim format As IImageFormat = Nothing
        retval.ByteArray = oByteArray
        Dim oImage = SixLabors.ImageSharp.Image.Load(oByteArray, format)

        Dim oMime As MimeCods
        If [Enum].TryParse(Of MimeCods)(format.Name, True, oMime) Then
            retval.Mime = oMime
        End If

        Return retval
    End Function

    Shared Function GetImageMediaType(oMime As MimeCods) As String
        Dim retval As String = "image/jpeg" 'default
        Select Case oMime
            Case MimeCods.Jpg
                retval = "image/jpeg"
            Case MimeCods.Png
                retval = "image/png"
            Case MimeCods.Tif, MimeCods.Tiff
                retval = "image/tiff"
            Case MimeCods.Gif
                retval = "image/gif"
        End Select

        Return retval
    End Function

End Class
