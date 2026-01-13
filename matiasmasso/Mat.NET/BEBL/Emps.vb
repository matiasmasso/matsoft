Public Class Emp

    Shared Function Find(Id As DTOEmp.Ids) As DTOEmp
        Dim retval As DTOEmp = EmpLoader.Find(Id)
        Return retval
    End Function

    Shared Function Load(ByRef oEmp As DTOEmp) As Boolean
        Dim retval As Boolean = EmpLoader.Load(oEmp)
        Return retval
    End Function

    Shared Function GetValue(oEmp As DTOEmp, oCod As DTODefault.Codis) As String
        Dim retval As String = ""
        Dim oDefault As DTODefault = DefaultLoader.Find(oCod, oEmp)
        If oDefault IsNot Nothing Then
            retval = oDefault.Value
        End If

        Return retval
    End Function

    Shared Function Create(exs As List(Of Exception), oUser As DTOUser) As Boolean
        Dim retval As Boolean = EmpLoader.Create(exs, oUser)
        Return retval
    End Function

    Shared Function Update(exs As List(Of Exception), oEmp As DTOEmp) As Boolean
        Dim retval As Boolean = EmpLoader.Update(exs, oEmp)
        Return retval
    End Function
End Class

Public Class Emps

    Shared Function Compact(oUser As DTOUser) As List(Of Models.Base.IdNom)
        Dim retval = EmpsLoader.Compact(oUser)
        Return retval
    End Function

    Shared Function All(oUser As DTOUser) As List(Of DTOEmp)
        Dim retval = EmpsLoader.All(oUser)
        Return retval
    End Function
End Class
