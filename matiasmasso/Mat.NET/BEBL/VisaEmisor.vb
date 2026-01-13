Public Class VisaEmisor


    Shared Function Find(oGuid As Guid) As DTOVisaEmisor
        Dim retval As DTOVisaEmisor = VisaEmisorLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oVisaEmisor As DTOVisaEmisor) As Boolean
        Dim retval As Boolean = VisaEmisorLoader.Load(oVisaEmisor)
        Return retval
    End Function

    Shared Function Update(oVisaEmisor As DTOVisaEmisor, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = VisaEmisorLoader.Update(oVisaEmisor, exs)
        Return retval
    End Function

    Shared Function Delete(oVisaEmisor As DTOVisaEmisor, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = VisaEmisorLoader.Delete(oVisaEmisor, exs)
        Return retval
    End Function


End Class

Public Class VisaEmisors

    Shared Function All() As List(Of DTOVisaEmisor)
        Dim retval As List(Of DTOVisaEmisor) = VisaEmisorsLoader.All()
        Return retval
    End Function

End Class
