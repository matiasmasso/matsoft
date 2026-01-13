Public Class DTOPgcEpgBase
    Inherits DTOBaseGuid

    Property parent As DTOPgcEpgBase
    Property nom As DTOLangText
    Property ordinal As String
    Property cod As Integer


    Public Enum Cods
        NotSet
        Epg0
        Epg1
        Epg2
        Cta
    End Enum

    Property Children As List(Of DTOPgcEpgBase)

    Public Sub New()
        MyBase.New()
        _Nom = New DTOLangText()
        _Children = New List(Of DTOPgcEpgBase)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _Nom = New DTOLangText()
        _Children = New List(Of DTOPgcEpgBase)
    End Sub

End Class
