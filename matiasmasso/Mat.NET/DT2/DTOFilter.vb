Public Class DTOFilter
    Inherits DTOBaseGuid
    Property stringKey As String

    Public Sub New()
        MyBase.New
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub
End Class
