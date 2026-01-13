Public Class MVPerson
    Property Guid As Guid
    Property Nom As String

    Public Sub New(sNom As String)
        MyBase.New
        _Guid = Guid.NewGuid
        _Nom = sNom
    End Sub

End Class

