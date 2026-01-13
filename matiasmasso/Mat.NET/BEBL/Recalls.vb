Public Class Recall

    Shared Function Find(oGuid As Guid) As DTORecall
        Dim retval As DTORecall = RecallLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oRecall As DTORecall) As Boolean
        Dim retval As Boolean = RecallLoader.Load(oRecall)
        Return retval
    End Function

    Shared Function Update(oRecall As DTORecall, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = RecallLoader.Update(oRecall, exs)
        Return retval
    End Function

    Shared Function Delete(oRecall As DTORecall, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = RecallLoader.Delete(oRecall, exs)
        Return retval
    End Function


End Class

Public Class Recalls

    Shared Function All() As List(Of DTORecall)
        Dim retval As List(Of DTORecall) = RecallsLoader.All()
        Return retval
    End Function

End Class