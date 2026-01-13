Public Class RepBasket
    Inherits _FeblBase

    Shared Async Function Update(exs As List(Of Exception), oOrder As DTOPurchaseOrder) As Task(Of Integer)
        Return Await Api.Update(Of DTOPurchaseOrder, Integer)(oOrder, exs, "RepBasket/update")
    End Function


End Class
