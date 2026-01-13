Public Class DTOAreaRegio
    Inherits DTOArea
    Shadows Property Country As DTOCountry
    Property Provincias As List(Of DTOAreaProvincia)

    Public Sub New()
        MyBase.New()
        _Provincias = New List(Of DTOAreaProvincia)
    End Sub

    Public Sub New(oGuid As Guid, Optional sNom As String = "")
        MyBase.New(oGuid, sNom)
        _Provincias = New List(Of DTOAreaProvincia)
    End Sub

    Shared Shadows Function Factory(oCountry As DTOCountry) As DTOAreaRegio
        Dim retval As New DTOAreaRegio
        With retval
            .Country = oCountry
        End With
        Return retval
    End Function
End Class
