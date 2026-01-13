Public Class CondicioCapitol

    Shared Function Find(oGuid As Guid) As DTOCondicio.Capitol
        Dim retval As DTOCondicio.Capitol = CondicioCapitolLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Update(oCondicioCapitol As DTOCondicio.Capitol, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = CondicioCapitolLoader.Update(oCondicioCapitol, exs)
        Return retval
    End Function

    Shared Function Delete(oCondicioCapitol As DTOCondicio.Capitol, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = CondicioCapitolLoader.Delete(oCondicioCapitol, exs)
        Return retval
    End Function

End Class



Public Class CondicioCapitols

    Shared Function Headers(oCondicio As DTOCondicio) As DTOCondicio.Capitol.Collection
        Dim retval As DTOCondicio.Capitol.Collection = CondicioCapitolsLoader.Headers(oCondicio)
        Return retval
    End Function

End Class
