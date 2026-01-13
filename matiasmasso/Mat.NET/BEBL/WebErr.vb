Public Class WebErr

    Shared Function Find(oGuid As Guid) As DTOWebErr
        Return WebErrLoader.Find(oGuid)
    End Function

    Shared Function Update(oWebErr As DTOWebErr, exs As List(Of Exception)) As Boolean
        Return WebErrLoader.Update(oWebErr, exs)
    End Function

    Shared Function Delete(oWebErr As DTOWebErr, exs As List(Of Exception)) As Boolean
        Return WebErrLoader.Delete(oWebErr, exs)
    End Function

End Class



Public Class WebErrs
    Shared Function All() As List(Of DTOWebErr)
        Dim retval As List(Of DTOWebErr) = WebErrsLoader.All()
        Return retval
    End Function

    Shared Function Reset(exs As List(Of Exception)) As Boolean
        Return WebErrsLoader.Reset(exs)
    End Function
End Class
