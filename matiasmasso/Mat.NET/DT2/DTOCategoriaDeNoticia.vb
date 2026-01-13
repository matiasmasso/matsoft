Public Class DTOCategoriaDeNoticia
    Inherits DTOBaseGuid
    Property Nom As String
    Property Excerpt As String
    Property FchLastEdited As DateTime

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Public Shadows Function ToString() As String
        Return _Nom
    End Function

End Class
