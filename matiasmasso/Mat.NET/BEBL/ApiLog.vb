Public Class ApiLog

    Shared Function Log(oApiLog As DTOApiLog, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ApiLogLoader.Log(oApiLog, exs)
        Return retval
    End Function

End Class
