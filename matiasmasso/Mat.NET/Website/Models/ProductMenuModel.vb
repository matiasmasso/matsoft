Public Class ProductMenuModel
    Property Items As List(Of BoxViewModel)

    Public Sub New()
        MyBase.New()
        _Items = New List(Of BoxViewModel)
    End Sub
End Class
