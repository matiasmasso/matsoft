Public Class ConsumerTicket

    Shared Function Find(oGuid As Guid) As DTOConsumerTicket
        Return ConsumerTicketLoader.Find(oGuid)
    End Function

    Shared Function Find(oMarketPlace As DTOMarketPlace, orderId As String) As DTOConsumerTicket
        Return ConsumerTicketLoader.Find(oMarketPlace, orderId)
    End Function

    Shared Function FromDelivery(oDelivery As DTODelivery) As DTOConsumerTicket
        Return ConsumerTicketLoader.FromDelivery(oDelivery)
    End Function

    Shared Function Update(oConsumerTicket As DTOConsumerTicket, exs As List(Of Exception)) As Boolean
        Return ConsumerTicketLoader.Update(oConsumerTicket, exs)
    End Function

    Shared Function Delete(oConsumerTicket As DTOConsumerTicket, exs As List(Of Exception)) As Boolean
        Return ConsumerTicketLoader.Delete(oConsumerTicket, exs)
    End Function

End Class



Public Class ConsumerTickets
    Shared Function All(oEmp As DTOEmp, year As Integer) As List(Of DTOConsumerTicket)
        Dim retval As List(Of DTOConsumerTicket) = ConsumerTicketsLoader.All(year, oEmp)
        Return retval
    End Function
    Shared Function All(oMarketPlace As DTOMarketPlace, year As Integer) As List(Of DTOConsumerTicket)
        Dim retval As List(Of DTOConsumerTicket) = ConsumerTicketsLoader.All(year, MarketPlace:=oMarketPlace)
        Return retval
    End Function
End Class

