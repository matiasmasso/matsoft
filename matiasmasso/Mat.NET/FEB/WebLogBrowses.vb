Public Class WebLogBrowse
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOWebLogBrowse)
        Return Await Api.Fetch(Of DTOWebLogBrowse)(exs, "WebLogBrowse", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oWebLogBrowse As DTOWebLogBrowse) As Boolean
        If Not oWebLogBrowse.IsLoaded And Not oWebLogBrowse.IsNew Then
            Dim pWebLogBrowse = Api.FetchSync(Of DTOWebLogBrowse)(exs, "WebLogBrowse", oWebLogBrowse.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOWebLogBrowse)(pWebLogBrowse, oWebLogBrowse, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oWebLogBrowse As DTOWebLogBrowse) As Task(Of Boolean)
        Return Await Api.Update(Of DTOWebLogBrowse)(oWebLogBrowse, exs, "WebLogBrowse")
        oWebLogBrowse.IsNew = False
    End Function


    Shared Async Function Delete(exs As List(Of Exception), oWebLogBrowse As DTOWebLogBrowse) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOWebLogBrowse)(oWebLogBrowse, exs, "WebLogBrowse")
    End Function
End Class

Public Class WebLogBrowses
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oDoc As DTOBaseGuid) As Task(Of List(Of DTOWebLogBrowse))
        Return Await Api.Fetch(Of List(Of DTOWebLogBrowse))(exs, "WebLogBrowses", oDoc.Guid.ToString())
    End Function

End Class
