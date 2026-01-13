Public Class Cod

    Shared Function Find(oGuid As Guid) As DTOCod
        Return CodLoader.Find(oGuid)
    End Function

    Shared Function Update(oCod As DTOCod, exs As List(Of Exception)) As Boolean
        Return CodLoader.Update(oCod, exs)
    End Function

    Shared Function Delete(oCod As DTOCod, exs As List(Of Exception)) As Boolean
        Return CodLoader.Delete(oCod, exs)
    End Function

End Class



Public Class Cods
    Shared Function All(oParent As DTOCod) As DTOCod.Collection
        Return CodsLoader.All(oParent)
    End Function

    Shared Function Sort(exs As List(Of Exception), oGuids As List(Of Guid)) As Boolean
        Return CodsLoader.Sort(exs, oGuids)
    End Function
End Class
