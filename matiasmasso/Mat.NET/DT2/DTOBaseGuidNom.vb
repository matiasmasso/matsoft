Public Class DTOBaseGuidNom
    Inherits DTOBaseGuid

    Property nom As String


    Public Sub New()
        MyBase.New
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function Factory(oGuid As Guid, Optional sNom As String = "") As DTOBaseGuidNom
        Dim retval = New DTOBaseGuidNom(oGuid)
        With retval
            .Nom = sNom
        End With
        Return retval
    End Function
End Class
