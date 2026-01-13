Public Class Repeticions

    Shared Function All(oProduct As DTOProduct) As List(Of DTORepeticio)
        Dim retval As List(Of DTORepeticio) = RepeticionsLoader.All(oProduct)
        Return retval
    End Function

End Class