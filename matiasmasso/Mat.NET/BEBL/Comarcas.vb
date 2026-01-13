Public Class Comarca


    Shared Function Find(oGuid As Guid) As DTOComarca
        Dim retval As DTOComarca = ComarcaLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oComarca As DTOComarca) As Boolean
        Dim retval As Boolean = ComarcaLoader.Load(oComarca)
        Return retval
    End Function

    Shared Function Update(oComarca As DTOComarca, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ComarcaLoader.Update(oComarca, exs)
        Return retval
    End Function

    Shared Function Delete(oComarca As DTOComarca, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ComarcaLoader.Delete(oComarca, exs)
        Return retval
    End Function


End Class

Public Class Comarcas

    Shared Function All(oZona As DTOZona) As List(Of DTOComarca)
        Dim retval As List(Of DTOComarca) = ComarcasLoader.All(oZona)
        Return retval
    End Function

End Class
