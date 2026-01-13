Public Class DTOIncidenciaCod
    Inherits DTOBaseGuid

    Property Nom As DTOLangText
    Property Cod As cods
    Property ReposicionParcial As Boolean
    Property ReposicionTotal As Boolean

    Public Enum cods
        NotSet
        averia
        tancament
    End Enum

    Public ReadOnly Property Esp As String
        Get
            Return _Nom.Esp
        End Get
    End Property

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _Nom = New DTOLangText
    End Sub
    Public Sub New()
        MyBase.New()
        _Nom = New DTOLangText
    End Sub

    Shared Function wellknown(oCod As DTOIncidenciaCod.cods) As DTOIncidenciaCod
        Dim retval As DTOIncidenciaCod = Nothing
        Select Case oCod
            Case DTOIncidenciaCod.cods.averia
                retval = New DTOIncidenciaCod(New Guid("504AA029-D206-41ED-A6F2-CE4C80A20FA4"))
        End Select
        Return retval
    End Function

    Public Shadows Function ToString() As String
        Return _Nom.Esp
    End Function
End Class
