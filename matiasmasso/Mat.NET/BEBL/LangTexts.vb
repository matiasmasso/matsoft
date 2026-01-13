Public Class LangText

    Shared Function Search(oRequest As DTOSearchRequest) As DTOSearchRequest
        Dim oResults As New List(Of DTOSearchRequest.Result)
        oResults.AddRange(LangTextLoader.SearchProducts(oRequest))
        oResults.AddRange(LangTextLoader.SearchNoticias(oRequest))
        oResults.AddRange(LangTextLoader.SearchBlog(oRequest))
        oRequest.Results = oResults
        Return oRequest
    End Function

    Shared Function Find(oGuid As Guid, src As DTOLangText.Srcs) As DTOLangText
        Return LangTextLoader.Find(oGuid, src)
    End Function

    Shared Function Update(exs As List(Of Exception), oLangText As DTOLangText) As Boolean
        Return LangTextLoader.Update(exs, oLangText)
    End Function

End Class
Public Class LangTexts

    Shared Function All(Optional src As DTOLangText.Srcs = DTOLangText.Srcs.notset, Optional oLang As DTOLang = Nothing, Optional searchkey As String = "") As List(Of DTOLangText)
        Return LangTextsLoader.All(src, oLang, searchkey)
    End Function

    Shared Function MissingTranslations() As List(Of DTOLangText)
        Return LangTextsLoader.MissingTranslations()
    End Function

End Class
