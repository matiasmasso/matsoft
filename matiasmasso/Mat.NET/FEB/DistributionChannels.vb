Public Class DistributionChannel

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTODistributionChannel)
        Return Await Api.Fetch(Of DTODistributionChannel)(exs, "DistributionChannel", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oDistributionChannel As DTODistributionChannel, exs As List(Of Exception)) As Boolean
        If Not oDistributionChannel.IsLoaded And Not oDistributionChannel.IsNew Then
            Dim pDistributionChannel = Api.FetchSync(Of DTODistributionChannel)(exs, "DistributionChannel", oDistributionChannel.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTODistributionChannel)(pDistributionChannel, oDistributionChannel, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oDistributionChannel As DTODistributionChannel, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTODistributionChannel)(oDistributionChannel, exs, "DistributionChannel")
        oDistributionChannel.IsNew = False
    End Function

    Shared Async Function Delete(oDistributionChannel As DTODistributionChannel, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTODistributionChannel)(oDistributionChannel, exs, "DistributionChannel")
    End Function
End Class

Public Class DistributionChannels

    Shared Async Function All(oUser As DTOUser, exs As List(Of Exception)) As Task(Of List(Of DTODistributionChannel))
        Return Await Api.Fetch(Of List(Of DTODistributionChannel))(exs, "DistributionChannels", oUser.Guid.ToString())
    End Function

    Shared Async Function AllWithContacts(oEmp As DTOEmp, exs As List(Of Exception)) As Task(Of List(Of DTODistributionChannel))
        Return Await Api.Fetch(Of List(Of DTODistributionChannel))(exs, "DistributionChannels/AllWithContacts", oEmp.Id)
    End Function

    Shared Async Function Headers(oEmp As DTOEmp, oLang As DTOLang, exs As List(Of Exception)) As Task(Of List(Of DTODistributionChannel))
        Return Await Api.Fetch(Of List(Of DTODistributionChannel))(exs, "DistributionChannels/headers", oEmp.Id, oLang.Tag)
    End Function

End Class

