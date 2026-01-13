Public Class [Default]

    Shared Function Find(oCod As DTODefault.Codis, Optional oEmp As DTOEmp = Nothing) As DTODefault
        Dim retval As DTODefault = DefaultLoader.Find(oCod, oEmp)
        Return retval
    End Function

    Shared Function Update(oDefault As DTODefault, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = DefaultLoader.Update(oDefault, exs)
        Return retval
    End Function

    Shared Function Delete(oDefault As DTODefault, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = DefaultLoader.Delete(oDefault, exs)
        Return retval
    End Function

End Class



Public Class Defaults

    Shared Function All(oEmp As DTOEmp) As List(Of DTODefault)
        Dim retval As List(Of DTODefault) = DefaultsLoader.All(oEmp)
        Return retval
    End Function

End Class
