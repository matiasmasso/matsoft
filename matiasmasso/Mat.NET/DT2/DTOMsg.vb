Public Class DTOMsg
    Inherits DTOBaseGuid

    Property Id As Integer
    Property User As DTOUser
    Property Fch As Date
    Property Text As String

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub
End Class