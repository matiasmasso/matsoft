Public Class Xec

    Shared Function Find(oGuid As Guid) As DTOXec
        Dim retval As DTOXec = XecLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oXec As DTOXec) As Boolean
        Return XecLoader.Load(oXec)
    End Function

    Shared Function Update(oXec As DTOXec, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = XecLoader.Update(oXec, exs)
        Return retval
    End Function

    Shared Function Delete(oXec As DTOXec, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = XecLoader.Delete(oXec, exs)
        Return retval
    End Function

    Public Shared Function UpdateXecRebut(oXec As DTOXec, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = XecLoader.UpdateXecRebut(oXec, exs)
        Return retval
    End Function

End Class



Public Class Xecs
    Shared Function Headers(Optional oLliurador As DTOContact = Nothing) As List(Of DTOXec)
        Dim retval As List(Of DTOXec) = XecsLoader.Headers(oLliurador)
        Return retval
    End Function

    Shared Function All(oEmp As DTOEmp, oStatusCod As DTOXec.StatusCods, Optional oCodPresentacio As DTOXec.ModalitatsPresentacio = DTOXec.ModalitatsPresentacio.NotSet) As List(Of DTOXec)
        Dim retval As List(Of DTOXec) = XecsLoader.All(oEmp, oStatusCod, oCodPresentacio)
        Return retval
    End Function

    Shared Function All(oCca As DTOCca) As List(Of DTOXec)
        Return XecsLoader.All(oCca)
    End Function

End Class
