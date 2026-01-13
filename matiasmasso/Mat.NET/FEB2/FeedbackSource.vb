Public Class FeedbackSource
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOFeedback.SourceClass)
        Return Await Api.Fetch(Of DTOFeedback.SourceClass)(exs, "FeedbackSource", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oFeedbackSource As DTOFeedback.SourceClass) As Boolean
        If Not oFeedbackSource.isLoaded And Not oFeedbackSource.isNew Then
            Dim pFeedbackSource = Api.FetchSync(Of DTOFeedback.SourceClass)(exs, "FeedbackSource", oFeedbackSource.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.copyPropertyValues(Of DTOFeedback.SourceClass)(pFeedbackSource, oFeedbackSource, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oFeedbackSource As DTOFeedback.SourceClass) As Task(Of Boolean)
        Return Await Api.Update(Of DTOFeedback.SourceClass)(oFeedbackSource, exs, "FeedbackSource")
        oFeedbackSource.isNew = False
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oFeedbackSource As DTOFeedback.SourceClass) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOFeedback.SourceClass)(oFeedbackSource, exs, "FeedbackSource")
    End Function
End Class

Public Class FeedbackSources
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of List(Of DTOFeedback.SourceClass))
        Return Await Api.Fetch(Of List(Of DTOFeedback.SourceClass))(exs, "FeedbackSources", oEmp.id)
    End Function

End Class
