Public Class StaffCategory


    Shared Function Find(oGuid As Guid) As DTOStaffCategory
        Dim retval As DTOStaffCategory = StaffCategoryLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oStaffCategory As DTOStaffCategory) As Boolean
        Dim retval As Boolean = StaffCategoryLoader.Load(oStaffCategory)
        Return retval
    End Function

    Shared Function Update(oStaffCategory As DTOStaffCategory, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = StaffCategoryLoader.Update(oStaffCategory, exs)
        Return retval
    End Function

    Shared Function Delete(oStaffCategory As DTOStaffCategory, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = StaffCategoryLoader.Delete(oStaffCategory, exs)
        Return retval
    End Function


End Class

Public Class StaffCategories

    Shared Function All() As List(Of DTOStaffCategory)
        Dim retval As List(Of DTOStaffCategory) = StaffCategoriesLoader.All()
        Return retval
    End Function


End Class