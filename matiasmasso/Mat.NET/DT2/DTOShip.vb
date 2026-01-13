Public Class DTOShip
    Inherits DTOBaseGuid

    Property MMSI As Integer
    Property Nom As String

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub
End Class
