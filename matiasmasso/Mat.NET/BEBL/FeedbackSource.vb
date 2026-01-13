Public Class FeedbackSource

    Shared Function Find(oGuid As Guid) As DTOFeedback.SourceClass
        Return FeedbackSourceLoader.Find(oGuid)
    End Function

    Shared Function Update(oFeedbackSource As DTOFeedback.SourceClass, exs As List(Of Exception)) As Boolean
        Return FeedbackSourceLoader.Update(oFeedbackSource, exs)
    End Function

    Shared Function Delete(oFeedbackSource As DTOFeedback.SourceClass, exs As List(Of Exception)) As Boolean
        Return FeedbackSourceLoader.Delete(oFeedbackSource, exs)
    End Function

End Class



Public Class FeedbackSources
    Shared Function All(oEmp As DTOEmp) As List(Of DTOFeedback.SourceClass)
        Dim retval As List(Of DTOFeedback.SourceClass) = FeedbackSourcesLoader.All(oEmp)
        Return retval
    End Function
End Class
