Public Class WebMenuGroup

    Shared Function Find(oGuid As Guid, Optional oRol As DTORol = Nothing) As DTOWebMenuGroup
        Dim retval As DTOWebMenuGroup = WebMenuGroupLoader.Find(oGuid, oRol)
        Return retval
    End Function

    Shared Function Update(oWebMenuGroup As DTOWebMenuGroup, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = WebMenuGroupLoader.Update(oWebMenuGroup, exs)
        Return retval
    End Function

    Shared Function Delete(oWebMenuGroup As DTOWebMenuGroup, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = WebMenuGroupLoader.Delete(oWebMenuGroup, exs)
        Return retval
    End Function

End Class

Public Class WebMenuGroups

    Shared Function All(Optional oUser As DTOUser = Nothing, Optional JustActiveItems As Boolean = False) As List(Of DTOWebMenuGroup)
        Dim retval As List(Of DTOWebMenuGroup) = WebMenuGroupsLoader.All(oUser, JustActiveItems)
        Return retval
    End Function

End Class
