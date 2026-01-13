Public Class Bank

    Shared Function Find(oGuid As Guid) As DTOBank
        Dim retval As DTOBank = BankLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Logo(oGuid As Guid) As ImageMime
        Dim retval = BankLoader.Logo(oGuid)
        Return retval
    End Function

    Shared Function FromCodi(oCountry As DTOCountry, Id As String) As DTOBank
        Dim retval As DTOBank = BankLoader.Find(oCountry, Id)
        Return retval
    End Function

    Shared Function Update(oBank As DTOBank, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = BankLoader.Update(oBank, exs)
        Return retval
    End Function

    Shared Function Delete(oBank As DTOBank, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = BankLoader.Delete(oBank, exs)
        Return retval
    End Function

End Class



Public Class Banks
    Shared Function All(oCountry As DTOCountry) As List(Of DTOBank)
        Dim retval As List(Of DTOBank) = BanksLoader.FromCountry(oCountry)
        Return retval
    End Function

    Shared Function Countries(oLang As DTOLang) As List(Of DTOCountry)
        Dim retval As List(Of DTOCountry) = BanksLoader.Countries(oLang)
        Return retval
    End Function

End Class

