Public Class ProductChannel
    Inherits _FeblBase

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOProductChannel)
        Return Await Api.Fetch(Of DTOProductChannel)(exs, "ProductChannel", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oProductChannel As DTOProductChannel, exs As List(Of Exception)) As Boolean
        If Not oProductChannel.IsLoaded And Not oProductChannel.IsNew Then
            Dim pProductChannel = Api.FetchSync(Of DTOProductChannel)(exs, "ProductChannel", oProductChannel.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOProductChannel)(pProductChannel, oProductChannel, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oProductChannel As DTOProductChannel, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOProductChannel)(oProductChannel, exs, "ProductChannel")
        oProductChannel.IsNew = False
    End Function

    Shared Async Function Delete(oProductChannel As DTOProductChannel, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOProductChannel)(oProductChannel, exs, "ProductChannel")
    End Function
End Class

Public Class ProductChannels
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oProduct As DTOProduct) As Task(Of List(Of DTOProductChannel))
        Dim retval = Await Api.Fetch(Of List(Of DTOProductChannel))(exs, "ProductChannels/FromProduct", oProduct.Guid.ToString())
        Return retval
    End Function

    Shared Async Function All(exs As List(Of Exception), oChannel As DTODistributionChannel) As Task(Of List(Of DTOProductChannel))
        Return Await Api.Fetch(Of List(Of DTOProductChannel))(exs, "ProductChannels/FromChannel", oChannel.Guid.ToString())
    End Function

End Class
