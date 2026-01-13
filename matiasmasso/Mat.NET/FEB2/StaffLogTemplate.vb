Public Class StaffLogTemplate
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oStaff As DTOStaff, weekday As DayOfWeek) As Task(Of DTOStaffLogTemplate)
        Return Await Api.Fetch(Of DTOStaffLogTemplate)(exs, "StaffLogTemplate", oStaff.Guid.ToString, weekday)
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef value As DTOStaffLogTemplate) As Boolean
        Dim pStaffLogTemplate = Api.FetchSync(Of DTOStaffLogTemplate)(exs, "StaffLogTemplate", value.Staff.Guid.ToString(), value.WeekDay)
        If exs.Count = 0 Then
            DTOBaseGuid.CopyPropertyValues(Of DTOStaffLogTemplate)(pStaffLogTemplate, value, exs)
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), value As DTOStaffLogTemplate) As Task(Of Boolean)
        Return Await Api.Update(Of DTOStaffLogTemplate)(value, exs, "StaffLogTemplate")
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oStaffLogTemplate As DTOStaffLogTemplate) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOStaffLogTemplate)(oStaffLogTemplate, exs, "StaffLogTemplate")
    End Function
End Class

Public Class StaffLogTemplates
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oStaff As DTOStaff) As Task(Of List(Of DTOStaffLogTemplate))
        Return Await Api.Fetch(Of List(Of DTOStaffLogTemplate))(exs, "StaffLogTemplates", oStaff.Guid.ToString())
    End Function

    Shared Async Function Update(exs As List(Of Exception), values As List(Of DTOStaffLogTemplate)) As Task(Of Boolean)
        Return Await Api.Update(Of List(Of DTOStaffLogTemplate))(values, exs, "StaffLogTemplates")
    End Function

End Class
