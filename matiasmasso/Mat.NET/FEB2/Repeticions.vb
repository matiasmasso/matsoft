Public Class Repeticions
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oProduct As DTOProduct) As Task(Of List(Of DTORepeticio))
        Return Await Api.Fetch(Of List(Of DTORepeticio))(exs, "Repeticions", oProduct.Guid.ToString())
    End Function

End Class
