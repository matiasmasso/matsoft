Public Class MimeHelper

    Shared Function GetExtensionFromMime(oMime As MimeCods) As String
        Dim retval As String = "." & oMime.ToString
        Return retval
    End Function

    Shared Function GetMimeFromExtension(ByVal sFileName As String) As MimeCods
        Dim retval As MimeCods
        Dim iPos As Integer = sFileName.LastIndexOf(".")
        If iPos >= 0 Then
            Dim sExtension As String = sFileName.Substring(iPos)
            Dim sUcaseExtension As String = sExtension.ToUpper
            Select Case sUcaseExtension
                Case ".JPG"
                    retval = MimeCods.Jpg
                Case ".JPEG"
                    retval = MimeCods.Jpg
                Case ".GIF"
                    retval = MimeCods.Gif
                Case ".BMP"
                    retval = MimeCods.Bmp
                Case ".TIF"
                    retval = MimeCods.Tif
                Case ".TIFF"
                    retval = MimeCods.Tiff
                Case ".PNG"
                    retval = MimeCods.Png
                Case ".ZIP"
                    retval = MimeCods.Zip
                Case ".PDF"
                    retval = MimeCods.Pdf
                Case ".XPS"
                    retval = MimeCods.Xps
                Case ".XML"
                    retval = MimeCods.Xml
                Case ".XLS"
                    retval = MimeCods.Xls
                Case ".XLSX"
                    retval = MimeCods.Xlsx
                Case ".XML"
                    retval = MimeCods.Xml
                Case ".MPG"
                    retval = MimeCods.Mpg
                Case ".RTF"
                    retval = MimeCods.Rtf
                Case ".EPS"
                    retval = MimeCods.Eps
                Case ".AI"
                    retval = MimeCods.Ai
                Case ".WMV"
                    retval = MimeCods.Wmv
                Case ".PLA"
                    retval = MimeCods.Txt
                Case ".TXT"
                    retval = MimeCods.Txt
                Case ".WAV"
                    retval = MimeCods.Wav
                Case ".CER"
                    retval = MimeCods.Cer
                Case ".DOC"
                    retval = MimeCods.Doc
                Case ".DOCX"
                    retval = MimeCods.Docx
                Case ".CSV"
                    retval = MimeCods.Csv
                Case ".EPUB"
                    retval = MimeCods.EPub
                Case ".MOV"
                    retval = MimeCods.Mov
                Case ".MP4"
                    retval = MimeCods.Mp4
                Case Else
                    retval = GetMimeFromContent(sFileName)
            End Select
        End If

        Return retval
    End Function

    Shared Function GetMediaType(oMime As MimeCods) As String
        Dim retval As String = ""
        Select Case oMime
            Case MimeCods.Jpg
                retval = "image/jpeg"
            Case MimeCods.Png
                retval = "image/png"
            Case MimeCods.Gif
                retval = "image/png"
        End Select
        Return retval
    End Function

    Shared Function GetMimeFromContent(ByVal sFileName As String) As MimeCods
        Dim retval As MimeCods '= MimeHelper.GetMimeFromExtension(sFileName) bucle
        If retval = MimeCods.NotSet Then
            Try
                Dim oFile As New IO.FileInfo(sFileName)
                Dim sr As IO.StreamReader = New IO.StreamReader(sFileName)
                Dim line As String
                line = sr.ReadLine()
                retval = GuessMime(line)
            Catch ex As Exception
            End Try

        End If
        Return retval
    End Function

    Shared Function GuessMime(oStream As Byte()) As MimeCods
        Dim retval As MimeCods = MimeCods.NotSet

        If oStream.Length > 2 Then
            Dim sHeader As String = Chr(oStream(0)).ToString() & Chr(oStream(1)).ToString & Chr(oStream(2)).ToString & Chr(oStream(3)).ToString & Chr(oStream(4)).ToString
            retval = GuessMime(sHeader)
        End If

        If retval = 0 Then
            Dim bmp As Byte() = System.Text.Encoding.ASCII.GetBytes("BM")
            Dim gif As Byte() = System.Text.Encoding.ASCII.GetBytes("GIF")
                Dim mp4 As Byte() = System.Text.Encoding.ASCII.GetBytes("MP41")
                Dim png As Byte() = {137, 80, 78, 71}
                Dim tiff As Byte() = {73, 73, 42}
                Dim tiff2 As Byte() = {77, 77, 42}
                Dim jpeg As Byte() = {255, 216, 255, 224} 'hex = "FF-D8"
                Dim jpeg2 As Byte() = {255, 216, 255, 225}

            If bmp.SequenceEqual(oStream.Take(bmp.Length)) Then
                retval = MimeCods.Bmp
            ElseIf gif.SequenceEqual(oStream.Take(gif.Length)) Then
                retval = MimeCods.Gif
            ElseIf png.SequenceEqual(oStream.Take(png.Length)) Then
                retval = MimeCods.Png
            ElseIf tiff.SequenceEqual(oStream.Take(tiff.Length)) Then
                retval = MimeCods.Tiff
            ElseIf tiff2.SequenceEqual(oStream.Take(tiff2.Length)) Then
                retval = MimeCods.Tiff
            ElseIf jpeg.SequenceEqual(oStream.Take(jpeg.Length)) Then
                retval = MimeCods.Jpg
            ElseIf jpeg2.SequenceEqual(oStream.Take(jpeg2.Length)) Then
                retval = MimeCods.Jpg
            ElseIf mp4.SequenceEqual(oStream.Take(mp4.Length)) Then
                retval = MimeCods.Mp4
                End If
            Return retval
        End If
        Return retval
    End Function


    Shared Function GuessMime(line As String) As MimeCods
        Dim retval As MimeCods = MimeCods.NotSet

        If line.StartsWith("<?xml version") Then
            retval = MimeCods.Xml
        ElseIf line.StartsWith("<DOCU") Then
            retval = MimeCods.Xml
        ElseIf line.StartsWith("%PDF") Then
            retval = MimeCods.Pdf
        ElseIf line.StartsWith("MP41") Then
            retval = MimeCods.Mp4
        ElseIf line.StartsWith("GIF8") Then
            retval = MimeCods.Gif
        ElseIf line.StartsWith("mp41") Then
            retval = MimeCods.Mp4
        End If

        Return retval
    End Function

End Class
