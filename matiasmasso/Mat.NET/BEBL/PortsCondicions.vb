Public Class PortsCondicio

    Shared Function Find(oGuid As Guid) As DTOPortsCondicio
        Dim retval As DTOPortsCondicio = PortsCondicioLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Update(value As DTOPortsCondicio, exs As List(Of Exception)) As Boolean
        Return PortsCondicioLoader.Update(value, exs)
    End Function

    Shared Function Delete(value As DTOPortsCondicio, exs As List(Of Exception)) As Boolean
        Return PortsCondicioLoader.Delete(value, exs)
    End Function

    Shared Function Customers(value As DTOPortsCondicio) As List(Of DTOGuidNom.Compact)
        Return PortsCondicioLoader.Customers(value)
    End Function
End Class

Public Class PortsCondicions

    Shared Function All() As List(Of DTOPortsCondicio)
        Dim retval As List(Of DTOPortsCondicio) = PortsCondicionsLoader.All
        Return retval
    End Function

End Class