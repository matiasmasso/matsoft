Public Class DTOComarca
    Inherits DTOArea

    Shadows Property Zona As DTOZona

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub
End Class
