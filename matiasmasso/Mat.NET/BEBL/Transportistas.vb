Public Class Transportista

    Shared Function Find(oGuid As Guid) As DTOTransportista
        Dim retval As DTOTransportista = TransportistaLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Exists(oContact As DTOContact) As Boolean
        Dim retval As Boolean = TransportistaLoader.Exists(oContact)
        Return retval
    End Function

    Shared Function Load(ByRef oTransportista As DTOTransportista) As Boolean
        Dim retval As Boolean = TransportistaLoader.Load(oTransportista)
        Return retval
    End Function

    Shared Function Update(oTransportista As DTOTransportista, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = TransportistaLoader.Update(oTransportista, exs)
        Return retval
    End Function

    Shared Function Delete(oTransportista As DTOTransportista, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = TransportistaLoader.Delete(oTransportista, exs)
        Return retval
    End Function



End Class

Public Class Transportistas

    Shared Function All(oEmp As DTOEmp, Optional BlOnlyActive As Boolean = False) As List(Of DTOTransportista)
        Dim retval As List(Of DTOTransportista) = TransportistasLoader.All(oEmp, BlOnlyActive)
        Return retval
    End Function

End Class
