Imports System.Runtime.CompilerServices
Public Module ImageExtensions

    <Extension()>
    Public Function Bytes(value As System.Drawing.Image) As Byte()
        Dim retval As Byte() = Nothing
        Using ms As New IO.MemoryStream
            value.Save(ms, Imaging.ImageFormat.Jpeg)
            retval = ms.ToArray()
        End Using
        Return retval
    End Function




End Module

