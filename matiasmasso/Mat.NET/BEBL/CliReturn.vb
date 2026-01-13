Public Class CliReturn

    Shared Function Find(oGuid As Guid) As DTOCliReturn
        Dim retval As DTOCliReturn = CliReturnLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Update(oCliReturn As DTOCliReturn, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = CliReturnLoader.Update(oCliReturn, exs)
        Return retval
    End Function

    Shared Function Delete(oCliReturn As DTOCliReturn, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = CliReturnLoader.Delete(oCliReturn, exs)
        Return retval
    End Function

End Class



Public Class CliReturns
    Shared Function All() As List(Of DTOCliReturn)
        Dim retval As List(Of DTOCliReturn) = CliReturnsLoader.All()
        Return retval
    End Function
End Class
