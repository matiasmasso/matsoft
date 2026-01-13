Public Class DTOEdiversaGenral
    Inherits DTOBaseGuid
    Property Ref As String
    Property Fch As Date
    Property Text As String

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub
End Class
