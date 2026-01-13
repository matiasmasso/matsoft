Public Class PremiumLine


    Shared Function Find(oGuid As Guid) As DTOPremiumLine
        Dim retval As DTOPremiumLine = PremiumLineLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oPremiumLine As DTOPremiumLine) As Boolean
        Dim retval As Boolean = PremiumLineLoader.Load(oPremiumLine)
        Return retval
    End Function

    Shared Function Update(oPremiumLine As DTOPremiumLine, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = PremiumLineLoader.Update(oPremiumLine, exs)
        Return retval
    End Function

    Shared Function Delete(oPremiumLine As DTOPremiumLine, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = PremiumLineLoader.Delete(oPremiumLine, exs)
        Return retval
    End Function

    Shared Function EmailRecipients(oPremiumLine As DTOPremiumLine) As List(Of DTOUser)
        Dim retval As List(Of DTOUser) = PremiumLineLoader.EmailRecipients(oPremiumLine)
        Return retval
    End Function

    Shared Function FromProduct(oProduct As DTOProduct) As DTOPremiumLine
        Return PremiumLineLoader.FromProduct(oProduct)
    End Function
End Class

Public Class PremiumLines

    Shared Function All() As List(Of DTOPremiumLine)
        Dim retval As List(Of DTOPremiumLine) = PremiumLinesLoader.All()
        Return retval
    End Function


End Class
