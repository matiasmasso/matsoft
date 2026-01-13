Public Class DTOLocalizedString
    Inherits DTOBaseGuid
    Property key As String
    Property items As List(Of item)

    Public Sub New()
        MyBase.New()
        _items = New List(Of DTOLocalizedString.item)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _items = New List(Of DTOLocalizedString.item)
    End Sub

    Public Function FindOrAddItem(locale As String) As item
        Dim retval = _items.FirstOrDefault(Function(x) x.locale = locale)
        If retval Is Nothing Then
            retval = New item
            retval.locale = locale
        End If
        Return retval
    End Function

    Public Class item
        Property locale As String
        Property value As String

    End Class
End Class
