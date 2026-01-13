Public Class CheckedGuidNomEventArgs
    Inherits EventArgs

    Public Property Value As DTOCheckedGuidNom

    Public Sub New(Value As DTOCheckedGuidNom)
        MyBase.New()
        _Value = Value
    End Sub

End Class
