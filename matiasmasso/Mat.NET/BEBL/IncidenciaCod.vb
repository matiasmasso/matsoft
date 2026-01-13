Public Class IncidenciaCod


    Shared Function Find(oGuid As Guid) As DTOIncidenciaCod
        Dim retval As DTOIncidenciaCod = IncidenciaCodLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oIncidenciaCod As DTOIncidenciaCod) As Boolean
        Dim retval As Boolean = IncidenciaCodLoader.Load(oIncidenciaCod)
        Return retval
    End Function

    Shared Function Update(oIncidenciaCod As DTOIncidenciaCod, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = IncidenciaCodLoader.Update(oIncidenciaCod, exs)
        Return retval
    End Function

    Shared Function Delete(oIncidenciaCod As DTOIncidenciaCod, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = IncidenciaCodLoader.Delete(oIncidenciaCod, exs)
        Return retval
    End Function


End Class

Public Class IncidenciaCods

    Shared Function All(Optional oCod As DTOIncidenciaCod.cods = DTOIncidenciaCod.cods.NotSet) As List(Of DTOIncidenciaCod)
        Dim retval As List(Of DTOIncidenciaCod) = IncidenciaCodsLoader.All(oCod)
        Return retval
    End Function

End Class