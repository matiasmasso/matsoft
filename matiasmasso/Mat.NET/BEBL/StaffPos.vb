Public Class StaffPos

    Shared Function Find(oGuid As Guid) As DTOStaffPos
        Dim retval As DTOStaffPos = StaffPosLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oStaffPos As DTOStaffPos) As Boolean
        Dim retval As Boolean = StaffPosLoader.Load(oStaffPos)
        Return retval
    End Function

    Shared Function Update(oStaffPos As DTOStaffPos, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = StaffPosLoader.Update(oStaffPos, exs)
        Return retval
    End Function

    Shared Function Delete(oStaffPos As DTOStaffPos, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = StaffPosLoader.Delete(oStaffPos, exs)
        Return retval
    End Function

End Class

Public Class StaffPoss

    Shared Function All() As List(Of DTOStaffPos)
        Dim retval As List(Of DTOStaffPos) = StaffPossLoader.All()
        Return retval
    End Function

    Shared Function Delete(oStaffPoss As List(Of DTOStaffPos), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = StaffPossLoader.Delete(oStaffPoss, exs)
        Return retval
    End Function

End Class