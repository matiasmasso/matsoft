Public Class GlobalVariables

    Shared Property Emps As List(Of DTOEmp)
    Shared Property UsersCache As Dictionary(Of Guid, DTOUser)
    Shared Property CachedImages As Models.CachedImages

    Shared Function Emp(oId As DTOEmp.Ids) As DTOEmp
        Return _Emps.FirstOrDefault(Function(x) x.Id = oId)
    End Function

End Class
