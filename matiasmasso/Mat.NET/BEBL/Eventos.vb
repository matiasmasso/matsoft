Public Class Eventos
    Shared Function Headers(oUser As DTOUser, Optional OnlyVisible As Boolean = True) As List(Of DTOEvento)
        Dim retval As List(Of DTOEvento) = EventosLoader.Headers(oUser, OnlyVisible)
        Return retval
    End Function

    Shared Function NextEvento(oUser As DTOUser) As DTOEvento
        Dim retval As DTOEvento = NoticiasLoader.NextEvento(oUser)
        Return retval
    End Function
End Class