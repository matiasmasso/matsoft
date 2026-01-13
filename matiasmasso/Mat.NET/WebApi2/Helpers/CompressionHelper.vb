Imports System.IO

Public Class CompressionHelper


    Public Shared Function Compress(ByVal data As Byte(), ByVal Optional useGZipCompression As Boolean = True) As Byte()
        Dim compressionLevel As System.IO.Compression.CompressionLevel = System.IO.Compression.CompressionLevel.Fastest

        Using memoryStream As MemoryStream = New MemoryStream()

            If useGZipCompression Then

                Using gZipStream As System.IO.Compression.GZipStream = New System.IO.Compression.GZipStream(memoryStream, compressionLevel, True)
                    gZipStream.Write(data, 0, data.Length)
                End Using
            Else

                Using gZipStream As System.IO.Compression.GZipStream = New System.IO.Compression.GZipStream(memoryStream, compressionLevel, True)
                    gZipStream.Write(data, 0, data.Length)
                End Using
            End If

            Return memoryStream.ToArray()
        End Using
    End Function

    Public Shared Function IsCompressionSupported() As Boolean
        Dim AcceptEncoding As String = HttpContext.Current.Request.Headers("Accept-Encoding")
        Return ((Not String.IsNullOrEmpty(AcceptEncoding) AndAlso (AcceptEncoding.Contains("gzip") OrElse AcceptEncoding.Contains("deflate"))))
    End Function
End Class
