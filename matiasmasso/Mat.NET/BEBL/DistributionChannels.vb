Public Class DistributionChannel

    Shared Function Find(oGuid As Guid) As DTODistributionChannel
        Dim retval As DTODistributionChannel = DistributionChannelLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oDistributionChannel As DTODistributionChannel) As Boolean
        Dim retval As Boolean = DistributionChannelLoader.Load(oDistributionChannel)
        Return retval
    End Function

    Shared Function Update(oDistributionChannel As DTODistributionChannel, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = DistributionChannelLoader.Update(oDistributionChannel, exs)
        Return retval
    End Function

    Shared Function Delete(oDistributionChannel As DTODistributionChannel, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = DistributionChannelLoader.Delete(oDistributionChannel, exs)
        Return retval
    End Function

End Class

Public Class DistributionChannels

    Shared Function Headers(oLang As DTOLang) As List(Of DTODistributionChannel)
        Dim retval As List(Of DTODistributionChannel) = DistributionChannelsLoader.Headers(oLang)
        Return retval
    End Function

    Shared Function AllWithContacts(oEmp As DTOEmp) As List(Of DTODistributionChannel)
        Dim retval As List(Of DTODistributionChannel) = DistributionChannelsLoader.AllWithContacts(oEmp)
        Return retval
    End Function

    Shared Function All(oUser As DTOUser) As List(Of DTODistributionChannel)
        Dim retval As List(Of DTODistributionChannel) = DistributionChannelsLoader.All(oUser)
        Return retval
    End Function

End Class
