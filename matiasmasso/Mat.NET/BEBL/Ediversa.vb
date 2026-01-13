Public Class Ediversa 'BEBL

    Shared Function ProcessaInbox(oUser As DTOUser, exs As List(Of Exception)) As Boolean
        Return BEBL.EDiversaOrders.ProcessAllValidated(oUser, exs)
    End Function

End Class
