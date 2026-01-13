Public Class EdiversaRecadv

    Shared Function Find(oGuid As Guid) As DTOEdiversaRecadv
        Return EdiversaRecadvLoader.Find(oGuid)
    End Function

    Shared Function Update(oEdiversaRecadv As DTOEdiversaRecadv, exs As List(Of Exception)) As Boolean
        Return EdiversaRecadvLoader.Update(oEdiversaRecadv, exs)
    End Function

    Shared Function Delete(oEdiversaRecadv As DTOEdiversaRecadv, exs As List(Of Exception)) As Boolean
        Return EdiversaRecadvLoader.Delete(oEdiversaRecadv, exs)
    End Function

End Class



Public Class EdiversaRecadvs
    Shared Function All() As List(Of DTOEdiversaRecadv)
        Return EdiversaRecadvsLoader.All()
    End Function
End Class

