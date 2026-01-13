Public Class WebMenuItem
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOWebMenuItem)
        Return Await Api.Fetch(Of DTOWebMenuItem)(exs, "WebMenuItem", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oWebMenuItem As DTOWebMenuItem) As Boolean
        If Not oWebMenuItem.IsLoaded And Not oWebMenuItem.IsNew Then
            Dim pWebMenuItem = Api.FetchSync(Of DTOWebMenuItem)(exs, "WebMenuItem", oWebMenuItem.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOWebMenuItem)(pWebMenuItem, oWebMenuItem, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oWebMenuItem As DTOWebMenuItem) As Task(Of Boolean)
        Return Await Api.Update(Of DTOWebMenuItem)(oWebMenuItem, exs, "WebMenuItem")
        oWebMenuItem.IsNew = False
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oWebMenuItem As DTOWebMenuItem) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOWebMenuItem)(oWebMenuItem, exs, "WebMenuItem")
    End Function
End Class
