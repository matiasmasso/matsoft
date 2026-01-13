Public Class DTONoticiaDestacada
    Inherits DTOBaseGuid

    Property Noticia As DTOBaseGuid
    Property FchFrom As Date
    Property FchTo As Date
    Property Professionals As Boolean

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub
End Class
