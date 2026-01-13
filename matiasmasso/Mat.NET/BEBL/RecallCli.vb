Public Class RecallCli

    Shared Function Find(oGuid As Guid) As DTORecallCli
        Dim retval As DTORecallCli = RecallCliLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oRecallCli As DTORecallCli) As Boolean
        Dim retval As Boolean = RecallCliLoader.Load(oRecallCli)
        Return retval
    End Function

    Shared Function Update(oRecallCli As DTORecallCli, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = RecallCliLoader.Update(oRecallCli, exs)
        Return retval
    End Function

    Shared Function Delete(oRecallCli As DTORecallCli, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = RecallCliLoader.Delete(oRecallCli, exs)
        Return retval
    End Function


End Class

Public Class RecallClis

    Shared Function All(oRecall As DTORecall) As List(Of DTORecallCli)
        Dim retval As List(Of DTORecallCli) = RecallClisLoader.All(oRecall)
        Return retval
    End Function

End Class