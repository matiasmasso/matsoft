Public Class ListItem2
    Property key As Integer
    Property value As String

    Public Sub New(key As Integer, value As String)
        MyBase.New
        _key = key
        _value = value
    End Sub
End Class
