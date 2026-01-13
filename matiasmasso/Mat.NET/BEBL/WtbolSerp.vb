Public Class WtbolSerp


    Shared Function Find(oGuid As Guid) As DTOWtbolSerp
        Dim retval As DTOWtbolSerp = WtbolSerpLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oWtbolSerp As DTOWtbolSerp) As Boolean
        Dim retval As Boolean = WtbolSerpLoader.Load(oWtbolSerp)
        Return retval
    End Function

    Shared Function Update(oWtbolSerp As DTOWtbolSerp, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = WtbolSerpLoader.Update(oWtbolSerp, exs)
        Return retval
    End Function

    Shared Function Delete(oWtbolSerp As DTOWtbolSerp, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = WtbolSerpLoader.Delete(oWtbolSerp, exs)
        Return retval
    End Function

End Class

Public Class WtbolSerps

    Shared Function All(Optional oSite As DTOWtbolSite = Nothing) As List(Of DTOWtbolSerp)
        Dim retval As List(Of DTOWtbolSerp) = WtbolSerpsLoader.All(oSite)
        Return retval
    End Function


End Class

