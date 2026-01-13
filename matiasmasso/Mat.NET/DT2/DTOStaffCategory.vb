Public Class DTOStaffCategory
    Inherits DTOBaseGuid

    Property segSocialGrup As DTOSegSocialGrup
    Property nom As String
    Property ord As Integer

    Public Sub New()
        MyBase.New
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub
End Class


