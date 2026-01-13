Public Class SepaText
    Inherits _FeblBase

    Shared Async Function Update(oSepaText As DTOSepaText, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOSepaText)(oSepaText, exs, "SepaText")
        oSepaText.IsNew = False
    End Function

End Class

Public Class SepaTexts
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOSepaText))
        Return Await Api.Fetch(Of List(Of DTOSepaText))(exs, "SepaTexts")
    End Function

End Class
