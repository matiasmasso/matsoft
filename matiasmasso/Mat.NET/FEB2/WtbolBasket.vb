Public Class WtbolBasket
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOWtbolBasket)
        Return Await Api.Fetch(Of DTOWtbolBasket)(exs, "WtbolBasket", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oWtbolBasket As DTOWtbolBasket) As Boolean
        If Not oWtbolBasket.IsLoaded And Not oWtbolBasket.IsNew Then
            Dim pWtbolBasket = Api.FetchSync(Of DTOWtbolBasket)(exs, "WtbolBasket", oWtbolBasket.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOWtbolBasket)(pWtbolBasket, oWtbolBasket, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oWtbolBasket As DTOWtbolBasket) As Task(Of Boolean)
        Return Await Api.Update(Of DTOWtbolBasket)(oWtbolBasket, exs, "WtbolBasket")
        oWtbolBasket.IsNew = False
    End Function


    Shared Async Function Delete(exs As List(Of Exception), oWtbolBasket As DTOWtbolBasket) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOWtbolBasket)(oWtbolBasket, exs, "WtbolBasket")
    End Function
End Class

Public Class WtbolBaskets
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oSite As DTOWtbolSite) As Task(Of List(Of DTOWtbolBasket))
        Return Await Api.Fetch(Of List(Of DTOWtbolBasket))(exs, "WtbolBaskets", oSite.Guid.ToString)
    End Function

End Class

