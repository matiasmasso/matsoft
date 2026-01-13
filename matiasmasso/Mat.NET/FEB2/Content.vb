Public Class Content
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOContent)
        Return Await Api.Fetch(Of DTOContent)(exs, "Content", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oContent As DTOContent) As Boolean
        If Not oContent.IsLoaded And Not oContent.IsNew Then
            Dim pContent = Api.FetchSync(Of DTOContent)(exs, "Content", oContent.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOContent)(pContent, oContent, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function SearchByUrl(exs As List(Of Exception), sUrlFriendlySegment As String) As Task(Of DTOContent)
        Dim retval = Await Api.Execute(Of String, DTOContent)(sUrlFriendlySegment, exs, "Content/SearchByUrl")
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oContent As DTOContent) As Task(Of Boolean)
        Return Await Api.Update(Of DTOContent)(oContent, exs, "Content")
        oContent.IsNew = False
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oContent As DTOContent) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOContent)(oContent, exs, "Content")
    End Function

End Class

Public Class Contents
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOContent))
        Return Await Api.Fetch(Of List(Of DTOContent))(exs, "Contents")
    End Function

End Class
