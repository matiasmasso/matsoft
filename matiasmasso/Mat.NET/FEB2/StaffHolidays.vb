Public Class StaffHoliday

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOStaffHoliday)
        Return Await Api.Fetch(Of DTOStaffHoliday)(exs, "StaffHoliday", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oStaffHoliday As DTOStaffHoliday, exs As List(Of Exception)) As Boolean
        If Not oStaffHoliday.IsLoaded And Not oStaffHoliday.IsNew Then
            Dim pStaffHoliday = Api.FetchSync(Of DTOStaffHoliday)(exs, "StaffHoliday", oStaffHoliday.Guid.ToString())
            If exs.Count = 0 Then
                Dim oEmp = oStaffHoliday.Emp
                DTOBaseGuid.CopyPropertyValues(Of DTOStaffHoliday)(pStaffHoliday, oStaffHoliday, exs)
                oStaffHoliday.Emp = oEmp
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oStaffHoliday As DTOStaffHoliday, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOStaffHoliday)(oStaffHoliday, exs, "StaffHoliday")
        oStaffHoliday.IsNew = False
    End Function

    Shared Async Function Delete(oStaffHoliday As DTOStaffHoliday, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOStaffHoliday)(oStaffHoliday, exs, "StaffHoliday")
    End Function
End Class

Public Class StaffHolidays


    Shared Async Function All(oEmp As DTOEmp, oStaff As DTOStaff, exs As List(Of Exception)) As Task(Of List(Of DTOStaffHoliday))
        Dim retval As List(Of DTOStaffHoliday) = Nothing
        If oStaff Is Nothing Then
            retval = Await Api.Fetch(Of List(Of DTOStaffHoliday))(exs, "StaffHolidays/FromEmp", CInt(oEmp.Id))
        Else
            retval = Await Api.Fetch(Of List(Of DTOStaffHoliday))(exs, "StaffHolidays/FromStaff", CInt(oEmp.Id), oStaff.Guid.ToString())
        End If
        For Each item In retval
            item.Emp = oEmp
        Next
        Return retval
    End Function

End Class

