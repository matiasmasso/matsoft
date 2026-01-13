Public Class DTOECITransmCentre
    Inherits DTOBaseGuid

    Property Parent As DTOECITransmGroup

    Property Centre As DTOContact

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub
End Class
