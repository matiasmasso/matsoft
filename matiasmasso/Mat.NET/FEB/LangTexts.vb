Public Class LangText
    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid, oSrc As DTOLangText.Srcs) As Task(Of DTOLangText)
        Return Await Api.Fetch(Of DTOLangText)(exs, "LangText", oGuid.ToString(), oSrc)
    End Function

    Shared Async Function Search(exs As List(Of Exception), oRequest As DTOSearchRequest) As Task(Of DTOSearchRequest)
        Return Await Api.Execute(Of DTOSearchRequest, DTOSearchRequest)(oRequest, exs, "LangText/Search")
    End Function


    Shared Function Load(exs As List(Of Exception), ByRef oLangText As DTOLangText) As Boolean
        If Not oLangText.IsLoaded Then 'And Not oLangText.IsNew Then (avoid isNew as it is always new)
            Dim pLangText = Api.FetchSync(Of DTOLangText)(exs, "LangText", oLangText.Guid.ToString(), oLangText.Src)
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOLangText)(pLangText, oLangText, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oLangText As DTOLangText) As Task(Of DTOLangText)
        Return Await Api.Update(Of DTOLangText, DTOLangText)(oLangText, exs, "LangText")
    End Function
End Class

Public Class LangTexts

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOLangText))
        Return Await Api.Fetch(Of List(Of DTOLangText))(exs, "LangTexts")
    End Function

    Shared Async Function MissingTranslations(exs As List(Of Exception)) As Task(Of List(Of DTOLangText))
        Return Await Api.Fetch(Of List(Of DTOLangText))(exs, "LangTexts/MissingTranslations")
    End Function

    Shared Async Function MissingCategories(oLang As DTOLang, exs As List(Of Exception)) As Task(Of List(Of DTOLangText))
        Return Await Api.Fetch(Of List(Of DTOLangText))(exs, "LangTexts/MissingCategories", oLang.Tag)
    End Function

    Shared Async Function MissingSkus(oLang As DTOLang, exs As List(Of Exception)) As Task(Of List(Of DTOLangText))
        Return Await Api.Fetch(Of List(Of DTOLangText))(exs, "LangTexts/MissingSkus", oLang.Tag)
    End Function

    Shared Async Function MissingWebMenuGroups(oLang As DTOLang, exs As List(Of Exception)) As Task(Of List(Of DTOLangText))
        Return Await Api.Fetch(Of List(Of DTOLangText))(exs, "LangTexts/MissingWebMenuGroups", oLang.Tag)
    End Function

    Shared Async Function MissingWebMenuItems(oLang As DTOLang, exs As List(Of Exception)) As Task(Of List(Of DTOLangText))
        Return Await Api.Fetch(Of List(Of DTOLangText))(exs, "LangTexts/MissingWebMenuItems", oLang.Tag)
    End Function

    Shared Async Function MissingWinMenuItems(oEmp As DTOEmp, oLang As DTOLang, exs As List(Of Exception)) As Task(Of List(Of DTOLangText))
        Return Await Api.Fetch(Of List(Of DTOLangText))(exs, "LangTexts/MissingWinMenuItems", oEmp.Id, oLang.Tag)
    End Function
End Class
