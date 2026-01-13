Public Class Rol

    Shared Function Find(Id As DTORol.Ids) As DTORol
        Dim retval As DTORol = RolLoader.Find(Id)
        Return retval
    End Function

    Shared Function Update(oRol As DTORol, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = RolLoader.Update(oRol, exs)
        Return retval
    End Function

    Shared Function Delete(oRol As DTORol, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = RolLoader.Delete(oRol, exs)
        Return retval
    End Function

End Class



Public Class Rols
    Shared Function All() As List(Of DTORol)
        Dim retval As List(Of DTORol) = RolsLoader.All()
        Return retval
    End Function
End Class
