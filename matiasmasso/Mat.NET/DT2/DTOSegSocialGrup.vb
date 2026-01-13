Public Class DTOSegSocialGrup
    Inherits DTOBaseGuid

    Property nom As String
    Property id As Integer

    Public Sub New()
        MyBase.New
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

End Class
