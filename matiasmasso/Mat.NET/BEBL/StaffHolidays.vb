Public Class StaffHoliday
    Shared Function Find(oGuid As Guid) As DTOStaffHoliday
        Dim retval As DTOStaffHoliday = StaffHolidayLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Update(oStaffHoliday As DTOStaffHoliday, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = StaffHolidayLoader.Update(oStaffHoliday, exs)
        Return retval
    End Function

    Shared Function Delete(oStaffHoliday As DTOStaffHoliday, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = StaffHolidayLoader.Delete(oStaffHoliday, exs)
        Return retval
    End Function

End Class



Public Class StaffHolidays
    Shared Function All(oEmp As DTOEmp, Optional oStaff As DTOStaff = Nothing) As List(Of DTOStaffHoliday)
        Dim retval As List(Of DTOStaffHoliday) = StaffHolidaysLoader.All(oEmp, oStaff)
        Return retval
    End Function


End Class
