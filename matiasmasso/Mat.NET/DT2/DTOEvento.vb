Public Class DTOEvento
    Inherits DTONoticia
    Property NomEsp As String
    Property NomCat As String
    Property NomEng As String
    Property FchFrom As Date
    Property FchTo As Date
    Property Area As Object

    Public Sub New()
        MyBase.New()
        MyBase.Src = Srcs.Eventos
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub
End Class
