Public Class AlbBloqueig

    Shared Function Find(oGuid As Guid) As DTOAlbBloqueig
        Dim retval As DTOAlbBloqueig = AlbBloqueigLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Search(oContact As DTOContact, oCodi As DTOAlbBloqueig.Codis, exs As List(Of Exception)) As DTOAlbBloqueig
        Dim retval As DTOAlbBloqueig = AlbBloqueigLoader.Search(oContact, oCodi, exs)
        Return retval
    End Function

    Shared Function Update(oAlbBloqueig As DTOAlbBloqueig, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = AlbBloqueigLoader.Update(oAlbBloqueig, exs)
        Return retval
    End Function

    Shared Function Delete(oAlbBloqueig As DTOAlbBloqueig, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = AlbBloqueigLoader.Delete(oAlbBloqueig, exs)
        Return retval
    End Function

    Shared Function BloqueigEnd(oAlbBloqueig As DTOAlbBloqueig, exs As List(Of Exception)) As Boolean
        Return AlbBloqueigLoader.BloqueigEnd(oAlbBloqueig, exs)
    End Function

End Class



Public Class AlbBloqueigs
    Shared Function All(oEmp As DTOEmp) As List(Of DTOAlbBloqueig)
        Dim retval As List(Of DTOAlbBloqueig) = AlbBloqueigsLoader.All(oEmp)
        Return retval
    End Function
End Class
