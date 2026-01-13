Public Class Immoble
    Shared Function Find(oGuid As Guid) As DTOImmoble
        Dim retval As DTOImmoble = ImmobleLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Update(oImmoble As DTOImmoble, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ImmobleLoader.Update(oImmoble, exs)
        Return retval
    End Function

    Shared Function Delete(oImmoble As DTOImmoble, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ImmobleLoader.Delete(oImmoble, exs)
        Return retval
    End Function

End Class



Public Class Immobles

    Shared Function Bundle(oUser As DTOUser) As DTOImmoble.Bundle
        Return ImmoblesLoader.Bundle(oUser)
    End Function

    Shared Function All(oEmp As Models.Base.IdNom) As List(Of DTOImmoble)
        Dim retval As List(Of DTOImmoble) = ImmoblesLoader.All(oEmp)
        Return retval
    End Function
End Class

