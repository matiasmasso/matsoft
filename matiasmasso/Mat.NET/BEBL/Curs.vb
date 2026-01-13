Public Class Cur
    Shared Property Current As DTOCur

    Shared Function Update(oCur As DTOCur, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = CurLoader.Update(oCur, exs)
        Return retval
    End Function

End Class

Public Class Curs
    Shared Function All() As DTOCur.Collection
        Dim retval = CursLoader.All()
        Return retval
    End Function
End Class
