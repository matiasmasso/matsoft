Public Class DTONewsletter
    Inherits DTOBaseGuid
    Property Id As Integer
    Property Fch As Date
    Property Title As String
    Property Lang As DTOLang
    Property Sources As List(Of DTONewsletterSource)

    Public Sub New()
        MyBase.New()
        _Sources = New List(Of DTONewsletterSource)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub
End Class

Public Class DTONewsletterSource
    Property Pro As Boolean
    Property Ord As Integer
    Property Cod As Cods
    Property ImageUrl As String
    Property Title As DTOLangText
    Property Excerpt As DTOLangText
    Property Url As String
    Property Tag As Object

    Public Enum Cods
        NotSet
        Blog
        News
        Events
        Promo
    End Enum


End Class
