Public Class FilterTargets

    Shared Function All(oTarget As DTOBaseGuid) As List(Of DTOFilter.Item)
        Return FilterTargetsLoader.All(oTarget)
    End Function

    Shared Function Filters(oGuids As List(Of Guid)) As List(Of DTOFilter)
        Return FilterTargetsLoader.Filters(oGuids)
    End Function

    Shared Function Update(exs As List(Of Exception), oTarget As DTOBaseGuid, oItems As List(Of DTOFilter.Item)) As Boolean
        Return FilterTargetsLoader.Update(exs, oTarget, oItems)
    End Function

    Shared Function Delete(exs As List(Of Exception), oTarget As DTOBaseGuid) As Boolean
        Return FilterTargetsLoader.Delete(exs, oTarget)
    End Function

End Class
