Imports System.Runtime.CompilerServices
Public Module ImageExtensions



    <Extension()>
    Public Function Bytes(value As Image) As Byte()
        Dim retval As Byte() = Nothing
        If value IsNot Nothing Then
            Using ms As New IO.MemoryStream
                value.Save(ms, Imaging.ImageFormat.Jpeg)
                retval = ms.ToArray()
            End Using
        End If
        Return retval
    End Function




End Module

