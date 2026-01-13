Public Class ECITransmGroup
    Shared Function Find(oGuid As Guid) As DTOECITransmGroup
        Dim retval As DTOECITransmGroup = ECITransmGroupLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oECITransmGroup As DTOECITransmGroup) As Boolean
        Dim retval As Boolean = ECITransmGroupLoader.Load(oECITransmGroup)
        Return retval
    End Function

    Shared Function Update(oECITransmGroup As DTOECITransmGroup, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ECITransmGroupLoader.Update(oECITransmGroup, exs)
        Return retval
    End Function

    Shared Function Delete(oECITransmGroup As DTOECITransmGroup, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ECITransmGroupLoader.Delete(oECITransmGroup, exs)
        Return retval
    End Function
End Class

Public Class ECITransmGroups

    Shared Function All() As List(Of DTOECITransmGroup)
        Dim retval As List(Of DTOECITransmGroup) = ECITransmGroupsLoader.All()
        Return retval
    End Function

End Class