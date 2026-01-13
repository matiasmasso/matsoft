Public Class DTOCert
    Inherits DTOBaseGuid

    <JsonIgnore> Property Stream As Byte()
    <JsonIgnore> Property Image As Image
    Property Pwd As String
    Property Ext As String
    Property Caduca As Date
    Property ImageUri As Uri

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Public Function memoryStream() As System.IO.MemoryStream
        Return New System.IO.MemoryStream(_Stream)
    End Function
End Class
