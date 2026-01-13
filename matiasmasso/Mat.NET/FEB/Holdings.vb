Public Class Holding
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOHolding)
        Return Await Api.Fetch(Of DTOHolding)(exs, "Holding", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oHolding As DTOHolding) As Boolean
        If Not oHolding.IsLoaded And Not oHolding.IsNew Then
            Dim pHolding = Api.FetchSync(Of DTOHolding)(exs, "Holding", oHolding.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOHolding)(pHolding, oHolding, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oHolding As DTOHolding) As Task(Of Boolean)
        Return Await Api.Update(Of DTOHolding)(oHolding, exs, "Holding")
        oHolding.IsNew = False
    End Function


    Shared Async Function Delete(exs As List(Of Exception), oHolding As DTOHolding) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOHolding)(oHolding, exs, "Holding")
    End Function

    Shared Async Function PendingPurchaseOrders(exs As List(Of Exception), oHolding As DTOHolding) As Task(Of List(Of DTOPurchaseOrder))
        Return Await Api.Fetch(Of List(Of DTOPurchaseOrder))(exs, "Holding/PendingPurchaseOrders", oHolding.Guid.ToString())
    End Function

End Class

Public Class Holdings
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of List(Of DTOHolding))
        Return Await Api.Fetch(Of List(Of DTOHolding))(exs, "Holdings", oEmp.Id)
    End Function

End Class
