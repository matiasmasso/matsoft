Public Class Mods145
    Shared Function GetValues(empId As DTOEmp.Ids) As List(Of DTOMod145)
        Return Mods145Loader.GetValues(empId)
    End Function
End Class
