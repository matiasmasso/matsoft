Public Class EdiOrder

    Shared Function Find(oGuid As Guid) As DTOEdiOrder
        Return EdiOrderLoader.Find(oGuid)
    End Function

    Shared Function Update(oEdiOrder As DTOEdiOrder, exs As List(Of Exception)) As Boolean
        Return EdiOrderLoader.Update(oEdiOrder, exs)
    End Function

    Shared Function Delete(oEdiOrder As DTOEdiOrder, exs As List(Of Exception)) As Boolean
        Return EdiOrderLoader.Delete(oEdiOrder, exs)
    End Function

End Class



Public Class EdiOrders
    Shared Function All() As DTOEdiOrder.Collection
        Return EdiOrdersLoader.All()
    End Function
End Class
