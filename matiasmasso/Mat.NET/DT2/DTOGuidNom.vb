Public Class DTOGuidNom

    Inherits DTOBaseGuid
    Property nom As String
    'Property Nom As String

    Public Sub New(oGuid As Guid, Optional sNom As String = "")
        MyBase.New(oGuid)
        _Nom = sNom
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

    Shared Function Factory(oGuid As Guid, Optional sNom As String = "") As DTOGuidNom
        Dim retval = New DTOGuidNom(oGuid)
        With retval
            .Nom = sNom
        End With
        Return retval
    End Function
End Class
