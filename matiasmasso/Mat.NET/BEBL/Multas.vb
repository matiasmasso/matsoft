Public Class Multa

    Shared Function Find(oGuid As Guid) As DTOMulta
        Dim retval As DTOMulta = MultaLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Update(oMulta As DTOMulta, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = MultaLoader.Update(oMulta, exs)
        Return retval
    End Function

    Shared Function Delete(oMulta As DTOMulta, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = MultaLoader.Delete(oMulta, exs)
        Return retval
    End Function

End Class

Public Class Multas
    Shared Function All(oSubjecte As DTOBaseGuid) As List(Of DTOMulta)
        Dim retval As List(Of DTOMulta) = MultasLoader.All(oSubjecte)
        Return retval
    End Function

End Class
