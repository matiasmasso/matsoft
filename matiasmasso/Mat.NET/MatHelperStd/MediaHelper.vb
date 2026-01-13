Public Class MediaHelper


    Shared Function LengthFormatted(iLength As Double) As String
        Dim retVal As String = ""
        Select Case iLength
            Case Is < 1000
                retVal = String.Format(iLength, "#0") & " bytes"
            Case Is < 10000
                retVal = String.Format(Math.Round(iLength / 1000, 1, MidpointRounding.AwayFromZero), "#0,0") & " Kb"
            Case Is < 1000000
                retVal = String.Format(Math.Round(iLength / 1000, 0, MidpointRounding.AwayFromZero), "#0") & " Kb"
            Case Is < 10000000
                retVal = String.Format(Math.Round(iLength / 1000000, 1, MidpointRounding.AwayFromZero), "#0,0") & " Mb"
            Case Is < 10000000000
                retVal = String.Format(Math.Round(iLength / 1000000, 0, MidpointRounding.AwayFromZero), "#0,0") & " Mb"
            Case Else
                retVal = String.Format(Math.Round(iLength / 1000000000, 0, MidpointRounding.AwayFromZero), "#0,0") & " Gb"
        End Select
        Return retVal
    End Function


    Shared Function ContentType(oMimeCod As MimeCods) As String
        Dim retval As String = GetContentTypeFromMimeCod(oMimeCod)
        Return retval
    End Function


    Shared Function GetContentTypeFromMimeCod(oMimeCod As MimeCods) As String
        Dim s As String = ""
        Select Case oMimeCod
            Case MimeCods.Gif
                s = "image/gif"
            Case MimeCods.Jpg
                s = "image/jpeg"
            Case MimeCods.Bmp
                s = "image/x-ms-bmp"
            Case MimeCods.Tif, MimeCods.Tiff
                s = "image/tiff"
            Case MimeCods.Png
                s = "image/x-png"
            Case MimeCods.Pdf
                s = "application/pdf"
            Case MimeCods.Xps
                s = "application/vnd.ms-package.xps-fixeddocument+xml"
            Case MimeCods.Zip
                s = "application/zip"
            Case MimeCods.Xls, MimeCods.Csv
                s = "application/vnd.ms-excel"
            Case MimeCods.Xlsx
                s = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Case MimeCods.Mpg
                s = "video/mpeg"
            Case MimeCods.Rtf
                s = "application/vnd.ms-word"
            Case MimeCods.Eps, MimeCods.Ai
                s = "application/postscript"
            Case MimeCods.Eps, MimeCods.Ai
                s = "video/x-ms-wmv"
            Case MimeCods.Pla, MimeCods.Txt
                s = "text/plain"
            Case MimeCods.Wav
                s = "audio/x-wav"
            Case MimeCods.Cer
                s = "application/x-x509-user-cert"
            Case MimeCods.Doc
                s = "application/application/msword"
            Case MimeCods.Docx
                s = "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
            Case MimeCods.EPub
                s = "application/epub+zip"
            Case MimeCods.Mp4
                s = "video/mp4"
            Case Else
                'ContentType = "video/x-ms-asf" (.asf)
                'ContentType = "video/avi"
                'ContentType = "application/msword"
                'ContentType = "audio/wav"
                'ContentType = "audio/mpeg3"
                'ContentType = "video/mpeg"
                'ContentType = "application/rtf"
                'ContentType = "text/html"
                'ContentType = "text/asp"
                s = "application/octet-stream"
        End Select
        Return s
    End Function

    Shared Function IsMimeExtension(sFilename As String, oMime As MimeCods) As Boolean
        Dim retval As Boolean
        Dim sExtension As String = GetExtensionFromMime(oMime).ToLower
        Dim iLen As Integer = sFilename.Length
        Dim iPos As Integer = sFilename.ToLower.LastIndexOf(sExtension)
        If iPos > 0 And iPos = iLen - sExtension.Length Then
            retval = True
        End If
        Return retval
    End Function

    Shared Function GetExtensionFromMime(oMime As MimeCods) As String
        Dim retval As String = "." & oMime.ToString
        Return retval
    End Function

End Class
