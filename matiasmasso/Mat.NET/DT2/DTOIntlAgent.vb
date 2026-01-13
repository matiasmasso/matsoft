Public Class DTOIntlAgent
    Inherits DTOContact

    Property Mode As Modes
    Property Principal As DTOContact
    Property Areas As List(Of DTOArea)
    Property Agents As List(Of DTOContact)

    Public Enum Modes
        Representants
        Representades
    End Enum

    Public Sub New()
        MyBase.New()
        _Agents = New List(Of DTOContact)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _Agents = New List(Of DTOContact)
    End Sub
End Class
