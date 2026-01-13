Public Class WebPageAlias

    Shared Function Find(oGuid As Guid) As DTOWebPageAlias
        Dim retval As DTOWebPageAlias = WebPageAliasLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function FromUrl(oWebPageAlias As DTOWebPageAlias) As DTOWebPageAlias
        Dim retval As DTOWebPageAlias = WebPageAliasLoader.FromUrl(oWebPageAlias)
        Return retval
    End Function

    Shared Function Load(ByRef oWebPageAlias As DTOWebPageAlias) As Boolean
        Dim retval As Boolean = WebPageAliasLoader.Load(oWebPageAlias)
        Return retval
    End Function

    Shared Function Update(oWebPageAlias As DTOWebPageAlias, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = WebPageAliasLoader.Update(oWebPageAlias, exs)
        Return retval
    End Function

    Shared Function Delete(oWebPageAlias As DTOWebPageAlias, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = WebPageAliasLoader.Delete(oWebPageAlias, exs)
        Return retval
    End Function


End Class

Public Class WebPagesAlias

    Shared Function All() As List(Of DTOWebPageAlias)
        Dim retval As List(Of DTOWebPageAlias) = WebPagesAliasLoader.All()
        Return retval
    End Function


End Class
