Public Class Neighbours

    Shared Async Function NearestNeighbours(exs As List(Of Exception), oUser As DTOUser, oCoordenadas As GeoHelper.Coordenadas, Optional iCount As Integer = 7, Optional includeSellout As Boolean = False) As Task(Of List(Of DTONeighbour))
        Return Await Api.Fetch(Of List(Of DTONeighbour))(exs, "Neighbours", oUser.Guid.ToString, oCoordenadas.latitud, oCoordenadas.longitud, iCount, If(includeSellout, 1, 0))
    End Function


End Class
