Public Class SepaText
    Shared Function Update(oSepaText As DTOSepaText, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = SepaTextLoader.Update(oSepaText, exs)
        Return retval
    End Function

End Class

Public Class SepaTexts
    Shared Function All() As List(Of DTOSepaText)
        Dim retval As List(Of DTOSepaText) = SepaTextsLoader.All()
        Return retval
    End Function
End Class
