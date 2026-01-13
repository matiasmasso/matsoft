Public Class Feedback
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOFeedback)
        Return Await Api.Fetch(Of DTOFeedback)(exs, "Feedback", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oFeedback As DTOFeedback) As Boolean
        If Not oFeedback.IsLoaded And Not oFeedback.IsNew Then
            Dim pFeedback = Api.FetchSync(Of DTOFeedback)(exs, "Feedback", oFeedback.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOFeedback)(pFeedback, oFeedback, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oFeedback As DTOFeedback) As Task(Of Guid)
        Return Await Api.Update(Of DTOFeedback, Guid)(oFeedback, exs, "Feedback")
        oFeedback.IsNew = False
    End Function

    Shared Async Function SaveComment(exs As List(Of Exception), oFeedback As DTOFeedback) As Task(Of Boolean)
        Return Await Api.Execute(Of DTOFeedback)(oFeedback, exs, "Feedback/saveComment")
        oFeedback.IsNew = False
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oFeedback As DTOFeedback) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOFeedback)(oFeedback, exs, "Feedback")
    End Function
End Class

Public Class Feedbacks
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOFeedback))
        Return Await Api.Fetch(Of List(Of DTOFeedback))(exs, "Feedbacks")
    End Function

End Class
