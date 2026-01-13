Public Class DTOValueNom
    Property Value As Integer
    Property Nom As String

    Public Sub New(value As Integer, nom As String)
        MyBase.New
        _Value = value
        _Nom = nom
    End Sub
End Class
