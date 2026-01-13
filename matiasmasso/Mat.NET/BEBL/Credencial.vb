Public Class Credencial
    Shared Function Find(oGuid As Guid) As DTOCredencial
        Dim retval As DTOCredencial = CredencialLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Update(oCredencial As DTOCredencial, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = CredencialLoader.Update(oCredencial, exs)
        Return retval
    End Function

    Shared Function Delete(oCredencial As DTOCredencial, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = CredencialLoader.Delete(oCredencial, exs)
        Return retval
    End Function

End Class



Public Class Credencials
    Shared Function All(oUser As DTOUser) As List(Of DTOCredencial)
        Dim retval As List(Of DTOCredencial) = CredencialsLoader.Headers(oUser)
        Return retval
    End Function

    Shared Function Owners(oEmp As DTOEmp) As List(Of DTOUser)
        Dim retval As List(Of DTOUser) = CredencialsLoader.Owners(oEmp)
        Return retval
    End Function
End Class
