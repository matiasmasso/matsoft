Public Class XecsPresentacio
    Inherits _FeblBase


    Shared Async Function Update(exs As List(Of Exception), oXecsPresentacio As DTOXecsPresentacio) As Task(Of Boolean)
        Return Await Api.Update(Of DTOXecsPresentacio)(oXecsPresentacio, exs, "XecsPresentacio")
    End Function

End Class
