Public Class DTOSepaText
    Inherits DTOBaseId
    Property LangText As DTOLangText


    Public Sub New(iId As Integer)
        MyBase.New(iId)
        _LangText = New DTOLangText()
        '_LangText.srcType = DTOLangText.Srcs.SepaText
        ' _LangText.Src = Me
    End Sub
    Public Sub New()
        MyBase.New(-1)
        _LangText = New DTOLangText()
        '_LangText.srcType = DTOLangText.Srcs.SepaText
        ' _LangText.Src = Me
    End Sub
End Class
