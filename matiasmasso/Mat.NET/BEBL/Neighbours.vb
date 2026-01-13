Public Class Neighbours
    Shared Function NearestNeighbours(oUser As DTOUser, oCoordenadas As GeoHelper.Coordenadas, Optional iCount As Integer = 7, Optional includeSellout As Boolean = False) As List(Of DTONeighbour)
        Dim retval As List(Of DTONeighbour) = NeighboursLoader.NearestNeighbours(oUser, oCoordenadas, iCount, includeSellout)
        Return retval
    End Function

End Class
