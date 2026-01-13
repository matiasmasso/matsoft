Public Class Incoterm

    Shared Function Find(id As String) As DTOIncoterm
        Return IncotermLoader.Find(id)
    End Function

    Shared Function Update(oIncoterm As DTOIncoterm, exs As List(Of Exception)) As Boolean
        Return IncotermLoader.Update(oIncoterm, exs)
    End Function

    Shared Function Delete(oIncoterm As DTOIncoterm, exs As List(Of Exception)) As Boolean
        Return IncotermLoader.Delete(oIncoterm, exs)
    End Function

End Class



Public Class Incoterms
    Shared Function All() As List(Of DTOIncoterm)
        Return IncotermsLoader.All()
    End Function
End Class
