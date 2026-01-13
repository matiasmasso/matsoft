Public Class WebMenuItem
    Shared Function Find(oGuid As Guid) As DTOWebMenuItem
        Dim retval As DTOWebMenuItem = WebMenuItemLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(value As DTOWebMenuItem) As Boolean
        Dim retval As Boolean = WebMenuItemLoader.Load(value)
        Return retval
    End Function

    Shared Function Update(oWebMenuItem As DTOWebMenuItem, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = WebMenuItemLoader.Update(oWebMenuItem, exs)
        Return retval
    End Function

    Shared Function Delete(oWebMenuItem As DTOWebMenuItem, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = WebMenuItemLoader.Delete(oWebMenuItem, exs)
        Return retval
    End Function

End Class

