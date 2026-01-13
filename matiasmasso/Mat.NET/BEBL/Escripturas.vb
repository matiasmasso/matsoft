Public Class Escriptura

    Shared Function Find(oGuid As Guid) As DTOEscriptura
        Dim retval As DTOEscriptura = EscripturaLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function FromCod(oEmp As DTOEmp, oCodi As DTOEscriptura.Codis) As DTOEscriptura
        Dim retval As DTOEscriptura = EscripturaLoader.FromCod(oEmp, oCodi)
        Return retval
    End Function

    Shared Function Update(oEscriptura As DTOEscriptura, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = EscripturaLoader.Update(oEscriptura, exs)
        Return retval
    End Function

    Shared Function Delete(oEscriptura As DTOEscriptura, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = EscripturaLoader.Delete(oEscriptura, exs)
        Return retval
    End Function

End Class



Public Class Escripturas
    Shared Function All(oEmp As DTOEmp) As List(Of DTOEscriptura)
        Dim retval As List(Of DTOEscriptura) = EscripturasLoader.All(oEmp)
        Return retval
    End Function
End Class
