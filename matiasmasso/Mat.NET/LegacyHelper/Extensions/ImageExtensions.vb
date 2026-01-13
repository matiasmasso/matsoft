Imports System.Runtime.CompilerServices
Public Module ImageExtensions



    <Extension()>
    Public Function Bytes(value As Image) As Byte()
        Return ImageHelper.GetByteArrayFromImg(value)
    End Function




End Module

