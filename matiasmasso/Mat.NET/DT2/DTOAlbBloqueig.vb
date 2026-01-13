Public Class DTOAlbBloqueig
    Inherits DTOBaseGuid

    Property User As DTOUser
    Property Contact As DTOContact
    Property Codi As Codis
    Property Fch As DateTime

    Public Enum Codis
        PDC
        ALB
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function Factory(oUser As DTOUser, oContact As DTOContact, oCodi As Codis) As DTOAlbBloqueig
        Dim retval As New DTOAlbBloqueig
        With retval
            .User = oUser
            .Contact = oContact
            .Codi = oCodi
        End With
        Return retval
    End Function
End Class
