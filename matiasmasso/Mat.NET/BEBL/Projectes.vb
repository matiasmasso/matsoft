Public Class Projecte


    Shared Function Find(oGuid As Guid) As DTOProjecte
        Dim retval As DTOProjecte = ProjecteLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oProjecte As DTOProjecte) As Boolean
        Dim retval As Boolean = ProjecteLoader.Load(oProjecte)
        Return retval
    End Function

    Shared Function Update(oProjecte As DTOProjecte, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ProjecteLoader.Update(oProjecte, exs)
        Return retval
    End Function

    Shared Function Delete(oProjecte As DTOProjecte, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ProjecteLoader.Delete(oProjecte, exs)
        Return retval
    End Function

    Shared Function Items(oProjecte As DTOProjecte) As List(Of DTOCcb)
        Dim retval As List(Of DTOCcb) = ProjecteLoader.Items(oProjecte)
        Return retval
    End Function


End Class

Public Class Projectes

    Shared Function All() As List(Of DTOProjecte)
        Dim retval As List(Of DTOProjecte) = ProjectesLoader.All()
        Return retval
    End Function


End Class

