Public Class ProductChannel

#Region "CRUD"
    Shared Function Find(oGuid As Guid) As DTOProductChannel
        Dim retval As DTOProductChannel = ProductChannelLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oProductChannel As DTOProductChannel) As Boolean
        Dim retval As Boolean = ProductChannelLoader.Load(oProductChannel)
        Return retval
    End Function

    Shared Function Update(oProductChannel As DTOProductChannel, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ProductChannelLoader.Update(oProductChannel, exs)
        Return retval
    End Function

    Shared Function Delete(oProductChannel As DTOProductChannel, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ProductChannelLoader.Delete(oProductChannel, exs)
        Return retval
    End Function
#End Region

End Class

Public Class ProductChannels

    Shared Function All(oProduct As DTOProduct) As List(Of DTOProductChannel)
        Dim items As List(Of DTOProductChannel) = ProductChannelsLoader.All(oProduct)
        Dim sortedItems = items.
            OrderByDescending(Function(x) x.Product.SourceCod).
            OrderBy(Function(y) y.DistributionChannel.Ord)

        Dim retval As New List(Of DTOProductChannel)
        Dim oChannel As New DTODistributionChannel
        For Each item As DTOProductChannel In sortedItems
            If item.DistributionChannel.UnEquals(oChannel) Then
                oChannel = item.DistributionChannel
                retval.Add(item)
            End If
        Next

        Return retval
    End Function

    Shared Function All(oDistributionChannel As DTODistributionChannel) As List(Of DTOProductChannel)
        Dim items As List(Of DTOProductChannel) = ProductChannelsLoader.All(oDistributionChannel)
        Dim retval = items.
            OrderByDescending(Function(x) x.Product.SourceCod).
            OrderBy(Function(y) y.DistributionChannel.Ord).
            ToList

        Return retval
    End Function


End Class
