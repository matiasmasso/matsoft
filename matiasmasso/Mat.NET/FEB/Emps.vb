Public Class Emp
    Shared Property Current As DTOEmp
    Private Shared _LastUpdate As Date

    Shared Async Function Find(empid As Integer, exs As List(Of Exception)) As Task(Of DTOEmp)
        Return Await Api.Fetch(Of DTOEmp)(exs, "emp", empid)
    End Function

    Shared Function FindSync(empid As Integer, exs As List(Of Exception)) As DTOEmp
        Dim retval As DTOEmp = Api.FetchSync(Of DTOEmp)(exs, "emp", empid)
        Return retval
    End Function

    Shared Function Load(ByRef oEmp As DTOEmp, exs As List(Of Exception)) As Boolean
        If Not oEmp.IsLoaded And Not oEmp.IsNew Then
            Dim pEmp = Api.FetchSync(Of DTOEmp)(exs, "Emp", oEmp.Id)
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOEmp)(pEmp, oEmp, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oEmp As DTOEmp, oUser As DTOUser) As Task(Of Boolean)
        If oEmp.IsNew Then
            Dim oEmpUser As New DTOUser()
            With oEmpUser
                .Emp = oEmp
                .EmailAddress = oUser.EmailAddress
                .Password = oUser.Password
                .Rol = oUser.Rol
                .Lang = oUser.Lang
                .NickName = oUser.NickName
                .Nom = oUser.Nom
                .Sex = .Sex
            End With
            Return Await Api.Execute(Of DTOUser)(oEmpUser, exs, "emp/create")
        Else
            Return Await Api.Update(Of DTOEmp)(oEmp, exs, "Emp")
        End If
    End Function


    Shared Async Function GetEmpValue(oEmp As DTOEmp, oCod As DTODefault.Codis, exs As List(Of Exception)) As Task(Of String)
        Return Await Api.Fetch(Of String)(exs, "emp/value", oEmp.Id, CInt(oCod))
    End Function

End Class

Public Class Emps

    Shared Async Function All(exs As List(Of Exception), oUser As DTOUser) As Task(Of List(Of DTOEmp))
        Dim retval = Await Api.Fetch(Of List(Of DTOEmp))(exs, "emps", oUser.Guid.ToString())
        For Each item In retval
            item.IsNew = False
        Next
        Return retval
    End Function

End Class
