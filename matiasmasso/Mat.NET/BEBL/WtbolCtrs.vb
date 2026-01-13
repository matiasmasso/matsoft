Public Class WtbolCtr

    Shared Function Log(oLandingPage As DTOWtbolLandingPage, sIp As String, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = WtbolCtrLoader.Log(oLandingPage, sIp, exs)
        Return retval
    End Function

    Shared Function Delete(oWtbolCtr As DTOWtbolCtr, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = WtbolCtrLoader.Delete(oWtbolCtr, exs)
        Return retval
    End Function

End Class

Public Class WtbolCtrs

    Shared Function All(Optional site As DTOWtbolSite = Nothing, Optional FchFrom As Date = Nothing, Optional FchTo As Date = Nothing) As List(Of DTOWtbolCtr)
        Dim retval As List(Of DTOWtbolCtr) = WtbolCtrsLoader.All(site, FchFrom, FchTo)
        Return retval
    End Function

End Class