Public Class EdiOrder
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOEdiOrder)
        Return Await Api.Fetch(Of DTOEdiOrder)(exs, "EdiOrder", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oEdiOrder As DTOEdiOrder) As Boolean
        If Not oEdiOrder.IsLoaded And Not oEdiOrder.IsNew Then
            Dim pEdiOrder = Api.FetchSync(Of DTOEdiOrder)(exs, "EdiOrder", oEdiOrder.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOEdiOrder)(pEdiOrder, oEdiOrder, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oEdiOrder As DTOEdiOrder) As Task(Of Boolean)
        Return Await Api.Update(Of DTOEdiOrder)(oEdiOrder, exs, "EdiOrder")
        oEdiOrder.IsNew = False
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oEdiOrder As DTOEdiOrder) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOEdiOrder)(oEdiOrder, exs, "EdiOrder")
    End Function
End Class

Public Class EdiOrders
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception)) As Task(Of DTOEdiOrder.Collection)
        Return Await Api.Fetch(Of DTOEdiOrder.Collection)(exs, "EdiOrders")
    End Function

End Class