Public Class Feedback

    Shared Function Find(oGuid As Guid) As DTOFeedback
        Return FeedbackLoader.Find(oGuid)
    End Function

    Shared Function Update(oFeedback As DTOFeedback, exs As List(Of Exception)) As Boolean
        Return FeedbackLoader.Update(oFeedback, exs)
    End Function

    Shared Function Delete(oFeedback As DTOFeedback, exs As List(Of Exception)) As Boolean
        Return FeedbackLoader.Delete(oFeedback, exs)
    End Function

End Class



Public Class Feedbacks

End Class
