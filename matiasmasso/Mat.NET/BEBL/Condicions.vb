Public Class Condicio
    Shared Function Find(oGuid As Guid) As DTOCondicio
        Dim retval As DTOCondicio = CondicioLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Update(oCondicio As DTOCondicio, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = CondicioLoader.Update(oCondicio, exs)
        Return retval
    End Function

    Shared Function Delete(oCondicio As DTOCondicio, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = CondicioLoader.Delete(oCondicio, exs)
        Return retval
    End Function
End Class

Public Class Condicions
    Shared Function Headers() As List(Of DTOCondicio)
        Dim retval As List(Of DTOCondicio) = CondicionsLoader.Headers()
        Return retval
    End Function
End Class
