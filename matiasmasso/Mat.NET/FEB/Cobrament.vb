Public Class Cobrament
    Inherits _FeblBase

    Shared Async Function Update(exs As List(Of Exception), oCobrament As DTOCobrament) As Task(Of Boolean)
        Return Await Api.Update(Of DTOCobrament)(oCobrament, exs, "Cobrament")
    End Function

End Class
