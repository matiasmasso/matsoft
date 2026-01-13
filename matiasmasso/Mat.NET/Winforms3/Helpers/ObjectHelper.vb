Imports System.IO
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters.Binary

Public Class ObjectHelper
    Shared Function Clone(Of T)(ByVal source As T) As T
        Dim retval As T = Nothing

        If GetType(T).IsSerializable Then
            If Not ReferenceEquals(source, Nothing) Then
                Using Stream As New System.IO.MemoryStream
                    Dim formatter As IFormatter = New BinaryFormatter()
                    formatter.Serialize(Stream, source)
                    Stream.Seek(0, SeekOrigin.Begin)
                    retval = CType(formatter.Deserialize(Stream), T)
                End Using
            End If
        Else
            Throw New ArgumentException("The type must be serializable.", NameOf(source))
        End If

        Return retval
    End Function
End Class
