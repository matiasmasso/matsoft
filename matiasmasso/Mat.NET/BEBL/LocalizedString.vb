Public Class LocalizedString

    Shared Function Find(oGuid As Guid) As DTOLocalizedString
        Return LocalizedStringLoader.Find(oGuid)
    End Function

    Shared Function Update(oLocalizedString As DTOLocalizedString, exs As List(Of Exception)) As Boolean
        Return LocalizedStringLoader.Update(oLocalizedString, exs)
    End Function

    Shared Function Delete(oLocalizedString As DTOLocalizedString, exs As List(Of Exception)) As Boolean
        Return LocalizedStringLoader.Delete(oLocalizedString, exs)
    End Function

End Class



Public Class LocalizedStrings
    Shared Function All() As List(Of DTOLocalizedString)
        Dim retval As List(Of DTOLocalizedString) = LocalizedStringsLoader.All()
        Return retval
    End Function
End Class

