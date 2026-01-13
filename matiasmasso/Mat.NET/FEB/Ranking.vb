Public Class Ranking
    Inherits _FeblBase

    Shared Async Function CustomerRanking(exs As List(Of Exception), oUser As DTOUser, Optional oProduct As DTOProduct = Nothing) As Task(Of DTORanking)
        Dim retval = Await Api.Fetch(Of DTORanking)(exs, "Ranking/CustomerRanking", oUser.Guid.ToString, OpcionalGuid(oProduct))
        Return retval
    End Function


    Shared Async Function LoadItems(exs As List(Of Exception), value As DTORanking) As Task(Of DTORanking)
        Dim retval As DTORanking = Nothing
        value.Items = New List(Of DTORankingItem)
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            retval = Await Api.Upload(Of DTORanking)(oMultipart, exs, "Ranking/LoadItems")
        End If
        Return retval
        'Return Await Api.Execute(Of DTORanking, DTORanking)(oRanking, exs, "Ranking/LoadItems")
    End Function

    Shared Async Function LoadCatalog(exs As List(Of Exception), oRanking As DTORanking) As Task(Of DTORanking)
        oRanking.Items = New List(Of DTORankingItem)
        Return Await Api.Execute(Of DTORanking, DTORanking)(oRanking, exs, "Ranking/LoadCatalog")
    End Function


    Shared Async Function SetProveidor(exs As List(Of Exception), oRanking As DTORanking, oProveidor As DTOContact) As Task(Of DTORanking)
        oRanking.Proveidor = oProveidor
        Return Await LoadCatalog(exs, oRanking)
    End Function

    Shared Async Function SetProduct(exs As List(Of Exception), oRanking As DTORanking, oProduct As DTOProduct) As Task(Of DTORanking)
        oRanking.Product = oProduct
        Return Await LoadItems(exs, oRanking)
    End Function

    Shared Async Function SetChannel(exs As List(Of Exception), oRanking As DTORanking, oChannel As DTODistributionChannel) As Task(Of DTORanking)
        oRanking.Channel = oChannel
        Return Await LoadItems(exs, oRanking)
    End Function



End Class
