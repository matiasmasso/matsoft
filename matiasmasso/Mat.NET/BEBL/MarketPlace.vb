Public Class MarketPlace

    Shared Function Find(oGuid As Guid) As DTOMarketPlace
        Return MarketPlaceLoader.Find(oGuid)
    End Function
    Shared Function FindSku(marketplace As Guid, sku As Guid) As DTOMarketplaceSku
        Return MarketPlaceLoader.FindSku(marketplace, sku)
    End Function

    Shared Function Update(oMarketPlace As DTOMarketPlace, exs As List(Of Exception)) As Boolean
        Return MarketPlaceLoader.Update(oMarketPlace, exs)
    End Function

    Shared Function UpdateSku(oMarketPlaceSku As DTOMarketplaceSku, exs As List(Of Exception)) As Boolean
        Return MarketPlaceLoader.UpdateSku(oMarketPlaceSku, exs)
    End Function

    Shared Function Delete(oMarketPlace As DTOMarketPlace, exs As List(Of Exception)) As Boolean
        Return MarketPlaceLoader.Delete(oMarketPlace, exs)
    End Function

    Public Shared Function Catalog(oMarketplace As DTOMarketPlace) As List(Of DTOMarketplaceSku)
        Return MarketPlaceLoader.Catalog(oMarketplace)
    End Function

    Shared Function EnableSkus(oItems As List(Of DTOMarketplaceSku), blEnabled As Boolean, exs As List(Of Exception)) As Boolean
        Return MarketPlaceLoader.EnableSkus(oItems, blEnabled, exs)
    End Function

    Shared Function Offers(oMarketPlace As DTOMarketPlace) As List(Of DTOOffer)
        Return MarketPlaceLoader.Offers(oMarketPlace)
    End Function

End Class



Public Class MarketPlaces
    Shared Function All(oEmp As DTOEmp) As List(Of DTOMarketPlace)
        Dim retval As List(Of DTOMarketPlace) = MarketPlacesLoader.All(oEmp)
        Return retval
    End Function

    Shared Async Function SyncTask(oTaskLog As DTOTaskLog, oEmp As DTOEmp) As Task(Of DTOTaskLog)
        Dim retval As DTOTaskLog = oTaskLog
        Dim exs As New List(Of Exception)
        Dim value = Await BEBL.Integracions.Worten.Offers.UpdateStocks(exs, oEmp, oTaskLog)

        If exs.Count = 0 Then
            retval = oTaskLog
        Else
            retval.SetResult(DTOTask.ResultCods.Failed, "error al sincronitzar els stocks amb Worten", exs)
        End If

        Return retval
    End Function
End Class
