Public Class MarketPlace
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOMarketPlace)
        Return Await Api.Fetch(Of DTOMarketPlace)(exs, "MarketPlace", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oMarketPlace As DTOMarketPlace) As Boolean
        If Not oMarketPlace.IsLoaded And Not oMarketPlace.IsNew Then
            Dim pMarketPlace = Api.FetchSync(Of DTOMarketPlace)(exs, "MarketPlace", oMarketPlace.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOMarketPlace)(pMarketPlace, oMarketPlace, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oMarketPlace As DTOMarketPlace) As Task(Of Boolean)
        Return Await Api.Update(Of DTOMarketPlace)(oMarketPlace, exs, "MarketPlace")
        oMarketPlace.IsNew = False
    End Function


    Shared Async Function Delete(exs As List(Of Exception), oMarketPlace As DTOMarketPlace) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOMarketPlace)(oMarketPlace, exs, "MarketPlace")
    End Function
End Class

Public Class MarketPlaces
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of List(Of DTOMarketPlace))
        Return Await Api.Fetch(Of List(Of DTOMarketPlace))(exs, "MarketPlaces", oEmp.Id)
    End Function

End Class
