Public Class StaffCategory
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOStaffCategory)
        Return Await Api.Fetch(Of DTOStaffCategory)(exs, "StaffCategory", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oStaffCategory As DTOStaffCategory) As Boolean
        If Not oStaffCategory.IsLoaded And Not oStaffCategory.IsNew Then
            Dim pStaffCategory = Api.FetchSync(Of DTOStaffCategory)(exs, "StaffCategory", oStaffCategory.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOStaffCategory)(pStaffCategory, oStaffCategory, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oStaffCategory As DTOStaffCategory) As Task(Of Boolean)
        Return Await Api.Update(Of DTOStaffCategory)(oStaffCategory, exs, "StaffCategory")
        oStaffCategory.IsNew = False
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oStaffCategory As DTOStaffCategory) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOStaffCategory)(oStaffCategory, exs, "StaffCategory")
    End Function
End Class

Public Class StaffCategories
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOStaffCategory))
        Return Await Api.Fetch(Of List(Of DTOStaffCategory))(exs, "StaffCategories")
    End Function

End Class
