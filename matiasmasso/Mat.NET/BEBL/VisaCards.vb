Public Class VisaCard

    Shared Function Find(oGuid As Guid) As DTOVisaCard
        Dim retval As DTOVisaCard = VisaCardLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oVisaCard As DTOVisaCard) As Boolean
        Dim retval As Boolean = VisaCardLoader.Load(oVisaCard)
        Return retval
    End Function

    Shared Function Update(oVisaCard As DTOVisaCard, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = VisaCardLoader.Update(oVisaCard, exs)
        Return retval
    End Function

    Shared Function Delete(oVisaCard As DTOVisaCard, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = VisaCardLoader.Delete(oVisaCard, exs)
        Return retval
    End Function


End Class

Public Class VisaCards

    Shared Function All(oEmp As DTOEmp, Optional oContact As DTOContact = Nothing, Optional Active As Boolean = True) As List(Of DTOVisaCard)
        Dim retval As List(Of DTOVisaCard) = VisaCardsLoader.All(oEmp, oContact, Active)
        Return retval
    End Function

End Class

