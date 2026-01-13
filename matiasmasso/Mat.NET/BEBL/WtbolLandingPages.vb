Public Class WtbolLandingPage
    Shared Function Find(oGuid As Guid) As DTOWtbolLandingPage
        Dim retval As DTOWtbolLandingPage = WtbolLandingpageLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oWtbolLandingPage As DTOWtbolLandingPage) As Boolean
        Dim retval As Boolean = WtbolLandingpageLoader.Load(oWtbolLandingPage)
        Return retval
    End Function

    Shared Function Update(oWtbolLandingPage As DTOWtbolLandingPage, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = WtbolLandingpageLoader.Update(oWtbolLandingPage, exs)
        Return retval
    End Function

    Shared Function Delete(oWtbolLandingPage As DTOWtbolLandingPage, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = WtbolLandingpageLoader.Delete(oWtbolLandingPage, exs)
        Return retval
    End Function
End Class

Public Class WtbolLandingPages
    Shared Function All(oProduct As DTOProduct, Optional includeStocksFromMgz As DTOMgz = Nothing) As List(Of DTOWtbolLandingPage)
        Dim retval As List(Of DTOWtbolLandingPage) = WtbolLandingpagesLoader.All(oProduct, includeStocksFromMgz)
        Return retval
    End Function

End Class

