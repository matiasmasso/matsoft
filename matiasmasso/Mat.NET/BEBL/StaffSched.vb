Public Class StaffSched

    Shared Function Find(oGuid As Guid) As DTOStaffSched
        Dim retval As DTOStaffSched = StaffSchedLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Vigent(oEmp As DTOEmp) As DTOStaffSched
        Dim retval As DTOStaffSched = StaffSchedLoader.Vigent(oEmp)
        If retval IsNot Nothing Then
            StaffSchedLoader.Load(retval)
        End If
        Return retval
    End Function

    Shared Function Update(oStaffSched As DTOStaffSched, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = StaffSchedLoader.Update(oStaffSched, exs)
        Return retval
    End Function

    Shared Function Delete(oStaffSched As DTOStaffSched, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = StaffSchedLoader.Delete(oStaffSched, exs)
        Return retval
    End Function

End Class



Public Class StaffScheds
    Shared Function All(oEmp As DTOEmp) As List(Of DTOStaffSched)
        Dim retval As List(Of DTOStaffSched) = StaffSchedsLoader.All(oEmp)
        Return retval
    End Function

    Shared Function All(oStaff As DTOStaff) As List(Of DTOStaffSched)
        Dim retval As List(Of DTOStaffSched) = StaffSchedsLoader.All(oStaff)
        Return retval
    End Function
End Class

