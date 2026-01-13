Public Class Insolvencia

    Shared Function Find(oGuid As Guid) As DTOInsolvencia
        Dim retval As DTOInsolvencia = InsolvenciaLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Update(oInsolvencia As DTOInsolvencia, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = InsolvenciaLoader.Update(oInsolvencia, exs)
        Return retval
    End Function

    Shared Function Delete(oInsolvencia As DTOInsolvencia, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = InsolvenciaLoader.Delete(oInsolvencia, exs)
        Return retval
    End Function

    Shared Function IsInsolvent(oContact As DTOContact) As Boolean
        Dim retval As Boolean = InsolvenciaLoader.IsInsolvent(oContact)
        Return retval
    End Function

End Class



Public Class Insolvencias
    Shared Function All() As List(Of DTOInsolvencia)
        Dim retval As List(Of DTOInsolvencia) = InsolvenciasLoader.All()
        Return retval
    End Function
End Class
