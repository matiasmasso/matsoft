Public Class ECITransmGroup
    Inherits _FeblBase

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOECITransmGroup)
        Return Await Api.Fetch(Of DTOECITransmGroup)(exs, "ECITransmGroup", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oECITransmGroup As DTOECITransmGroup, exs As List(Of Exception)) As Boolean
        If Not oECITransmGroup.IsLoaded And Not oECITransmGroup.IsNew Then
            Dim pECITransmGroup = Api.FetchSync(Of DTOECITransmGroup)(exs, "ECITransmGroup", oECITransmGroup.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOECITransmGroup)(pECITransmGroup, oECITransmGroup, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oECITransmGroup As DTOECITransmGroup, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOECITransmGroup)(oECITransmGroup, exs, "ECITransmGroup")
        oECITransmGroup.IsNew = False
    End Function

    Shared Async Function Delete(oECITransmGroup As DTOECITransmGroup, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOECITransmGroup)(oECITransmGroup, exs, "ECITransmGroup")
    End Function
End Class

Public Class ECITransmGroups
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOECITransmGroup))
        Return Await Api.Fetch(Of List(Of DTOECITransmGroup))(exs, "ECITransmGroups")
    End Function

End Class

