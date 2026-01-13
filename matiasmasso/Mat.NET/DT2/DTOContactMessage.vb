Public Class DTOContactMessage
    Inherits DTOBaseGuid
    Property Email As String
    Property Nom As String
    Property Location As String
    Property Text As String
    Property FchCreated As DateTime

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub
End Class
