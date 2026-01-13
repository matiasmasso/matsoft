Public Class StaffPos
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOStaffPos)
        Return Await Api.Fetch(Of DTOStaffPos)(exs, "StaffPos", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oStaffPos As DTOStaffPos) As Boolean
        If Not oStaffPos.IsLoaded And Not oStaffPos.IsNew Then
            Dim pStaffPos = Api.FetchSync(Of DTOStaffPos)(exs, "StaffPos", oStaffPos.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOStaffPos)(pStaffPos, oStaffPos, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oStaffPos As DTOStaffPos) As Task(Of Boolean)
        Return Await Api.Update(Of DTOStaffPos)(oStaffPos, exs, "StaffPos")
        oStaffPos.IsNew = False
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oStaffPos As DTOStaffPos) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOStaffPos)(oStaffPos, exs, "StaffPos")
    End Function
End Class

Public Class StaffPoss
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOStaffPos))
        Return Await Api.Fetch(Of List(Of DTOStaffPos))(exs, "StaffPoss")
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oStaffPoss As List(Of DTOStaffPos)) As Task(Of Boolean)
        Return Await Api.Delete(Of List(Of DTOStaffPos))(oStaffPoss, exs, "StaffPos")
    End Function
End Class
