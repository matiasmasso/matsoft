Public Class SepaMe
    Shared Function Find(oGuid As Guid) As DTOSepaMe
        Return SepaMeLoader.Find(oGuid)
    End Function

    Shared Function Update(oSepaMe As DTOSepaMe, exs As List(Of Exception)) As Boolean
        Return SepaMeLoader.Update(oSepaMe, exs)
    End Function

    Shared Function Delete(oSepaMe As DTOSepaMe, exs As List(Of Exception)) As Boolean
        Return SepaMeLoader.Delete(oSepaMe, exs)
    End Function

End Class



Public Class SepaMes
    Shared Function All() As List(Of DTOSepaMe)
        Return SepaMesLoader.All()
    End Function
End Class

