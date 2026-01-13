Public Class Address


    Shared Function Update(oAddress As DTOAddress, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = AddressLoader.Update(oAddress, exs)
        Return retval
    End Function

    Shared Function update(exs As List(Of Exception), oUser As DTOUser, oContact As DTOContact, codi As Integer, coordenadas As GeoHelper.Coordenadas) As Boolean
        Return AddressLoader.Update(exs, oUser, oContact, codi, coordenadas)
    End Function

    Shared Function Delete(oAddress As DTOAddress, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = AddressLoader.Delete(oAddress, exs)
        Return retval
    End Function

End Class



Public Class Addresses
    Shared Function All(oContact As DTOContact, Optional oCodi As DTOAddress.Codis = DTOAddress.Codis.NotSet) As List(Of DTOAddress)
        Dim retval As New List(Of DTOAddress)
        Dim values As List(Of DTOAddress) = AddressesLoader.All(oContact)
        If oCodi = DTOAddress.Codis.NotSet Then
            retval = values
        Else
            retval = values.Where(Function(x) x.Codi = oCodi).ToList
        End If

        Return retval
    End Function
End Class
