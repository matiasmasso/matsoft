Public Class [Default]
    Shared Async Function Find(cod As DTODefault.Codis, exs As List(Of Exception)) As Task(Of DTODefault)
        Return Await Api.Fetch(Of DTODefault)(exs, "Default", cod)
    End Function

    Shared Async Function Find(cod As DTODefault.Codis, oEmp As DTOEmp, exs As List(Of Exception)) As Task(Of DTODefault)
        Return Await Api.Fetch(Of DTODefault)(exs, "Default", cod, oEmp.Id)
    End Function

    Shared Function FindSync(cod As DTODefault.Codis, oEmp As DTOEmp, exs As List(Of Exception)) As DTODefault
        Return Api.FetchSync(Of DTODefault)(exs, "Default", cod, oEmp.Id)
    End Function

    Shared Function FindSync(cod As DTODefault.Codis, exs As List(Of Exception)) As DTODefault
        Return Api.FetchSync(Of DTODefault)(exs, "Default", cod)
    End Function

    Shared Function Load(ByRef oDefault As DTODefault, exs As List(Of Exception)) As Boolean
        If Not oDefault.IsLoaded And Not oDefault.IsNew Then
            Dim pDefault As DTODefault
            If oDefault.Emp Is Nothing Then
                pDefault = Api.FetchSync(Of DTODefault)(exs, "Default", oDefault.Cod)
            Else
                pDefault = Api.FetchSync(Of DTODefault)(exs, "Default", oDefault.Cod, oDefault.Emp.Id)
            End If

            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTODefault)(pDefault, oDefault, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oDefault As DTODefault, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTODefault)(oDefault, exs, "Default")
        oDefault.IsNew = False
    End Function


    Shared Async Function Delete(oDefault As DTODefault, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTODefault)(oDefault, exs, "Default")
    End Function

    Shared Async Function EmpValue(oEmp As DTOEmp, oCod As DTODefault.Codis, exs As List(Of Exception)) As Task(Of String)
        Dim retval As String = ""
        Dim oDefault = Await Find(oCod, oEmp, exs)
        If oDefault IsNot Nothing Then
            retval = oDefault.Value
        End If
        Return retval
    End Function

    Shared Function EmpValueSync(oEmp As DTOEmp, oCod As DTODefault.Codis, exs As List(Of Exception)) As String
        Dim retval As String = ""
        Dim oDefault = FindSync(oCod, oEmp, exs)
        If oDefault IsNot Nothing Then
            retval = oDefault.Value
        End If
        Return retval
    End Function

    Shared Async Function EmpAmt(oEmp As DTOEmp, oCod As DTODefault.Codis, exs As List(Of Exception)) As Task(Of DTOAmt)
        Dim retval As DTOAmt = Nothing
        Dim src As String = Await EmpValue(oEmp, oCod, exs)
        If TextHelper.VbIsNumeric(src) Then
            retval = DTOAmt.Factory(CDec(src))
        Else
            retval = DTOAmt.Factory(0)
        End If
        Return retval
    End Function

    Shared Async Function EmpDecimal(oEmp As DTOEmp, oCod As DTODefault.Codis, exs As List(Of Exception)) As Task(Of Decimal)
        Dim retval As Decimal = 0
        Dim src As String = Await EmpValue(oEmp, oCod, exs)
        If TextHelper.VbIsNumeric(src) Then
            retval = CDec(src)
        End If
        Return retval
    End Function

    Shared Async Function EmpInteger(oEmp As DTOEmp, oCod As DTODefault.Codis, exs As List(Of Exception)) As Task(Of Integer)
        Dim retval As Integer = 0
        Dim src As String = Await EmpValue(oEmp, oCod, exs)
        If TextHelper.VbIsNumeric(src) Then
            retval = CInt(src)
        End If
        Return retval
    End Function
    Shared Function EmpIntegerSync(oEmp As DTOEmp, oCod As DTODefault.Codis, exs As List(Of Exception)) As Integer
        Dim retval As Integer = 0
        Dim src As String = EmpValueSync(oEmp, oCod, exs)
        If TextHelper.VbIsNumeric(src) Then
            retval = CInt(src)
        End If
        Return retval
    End Function

    Shared Async Function EmpBoolean(oEmp As DTOEmp, oCod As DTODefault.Codis, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim src As String = Await EmpValue(oEmp, oCod, exs)
        If src <> "" And src <> "0" Then
            retval = True
        End If
        Return retval
    End Function

    Shared Async Function Contact(oEmp As DTOEmp, oCodi As DTODefault.Codis, exs As List(Of Exception)) As Task(Of DTOContact)
        Dim retval As DTOContact = Nothing
        Dim oGuid As Guid = Await EmpGuid(oEmp, oCodi, exs)
        If Not oGuid.Equals(Guid.Empty) Then
            retval = Await FEB2.Contact.Find(oGuid, exs)
        End If
        Return retval
    End Function
    Shared Function ContactSync(oEmp As DTOEmp, oCodi As DTODefault.Codis, exs As List(Of Exception)) As DTOContact
        Dim retval As DTOContact = Nothing
        Dim oGuid As Guid = EmpGuidSync(oEmp, oCodi, exs)
        If Not oGuid.Equals(Guid.Empty) Then
            retval = FEB2.Contact.FindSync(oGuid, exs)
        End If
        Return retval
    End Function

    Shared Async Function EmpGuid(oEmp As DTOEmp, oCod As DTODefault.Codis, exs As List(Of Exception)) As Task(Of Guid)
        Dim retval As Guid = Guid.Empty
        Dim src As String = Await EmpValue(oEmp, oCod, exs)
        If src > "" Then
            retval = New Guid(src)
        End If
        Return retval
    End Function

    Shared Function EmpGuidSync(oEmp As DTOEmp, oCod As DTODefault.Codis, exs As List(Of Exception)) As Guid
        Dim retval As Guid = Guid.Empty
        Dim src As String = EmpValueSync(oEmp, oCod, exs)
        If src > "" Then
            retval = New Guid(src)
        End If
        Return retval
    End Function

    Shared Async Function SetEmpBoolean(oEmp As DTOEmp, oCod As DTODefault.Codis, value As Boolean, exs As List(Of Exception)) As Task(Of Boolean)
        Dim oDefault As New DTODefault
        With oDefault
            .Cod = oCod
            .Emp = oEmp
            .value = If(value, "1", "")
        End With

        Dim retval = Await Update(oDefault, exs)
        Return retval
    End Function

    Shared Async Function SetEmpValue(oEmp As DTOEmp, oCod As DTODefault.Codis, value As String, exs As List(Of Exception)) As Task(Of Boolean)
        Dim oDefault As New DTODefault
        With oDefault
            .Cod = oCod
            .Emp = oEmp
            .Value = value
        End With

        Dim retval = Await Update(oDefault, exs)
        Return retval
    End Function

    Shared Async Function SetEmpValue(oEmp As DTOEmp, oCod As DTODefault.Codis, value As DTOAmt, exs As List(Of Exception)) As Task(Of Boolean)
        Dim oDefault As New DTODefault
        With oDefault
            .Cod = oCod
            .Emp = oEmp
            .Value = value.Eur.ToString()
        End With

        Dim retval = Await Update(oDefault, exs)
        Return retval
    End Function

    Shared Async Function SetEmpValue(oEmp As DTOEmp, oCod As DTODefault.Codis, value As DTOBaseGuid, exs As List(Of Exception)) As Task(Of Boolean)
        Dim oDefault As New DTODefault
        With oDefault
            .Cod = oCod
            .Emp = oEmp
            .Value = value.Guid.ToString
        End With

        Dim retval = Await Update(oDefault, exs)
        Return retval
    End Function

    Shared Async Function GlobalValue(oCod As DTODefault.Codis, exs As List(Of Exception)) As Task(Of String)
        Dim retval As String = ""
        Dim oDefault As DTODefault = Await Find(oCod, exs)
        If oDefault IsNot Nothing Then
            retval = oDefault.Value
        End If
        Return retval
    End Function

    Shared Function GlobalValueSync(oCod As DTODefault.Codis, exs As List(Of Exception)) As String
        Dim retval As String = ""
        Dim oDefault As DTODefault = FindSync(oCod, exs)
        If oDefault IsNot Nothing Then
            retval = oDefault.Value
        End If
        Return retval
    End Function
End Class

Public Class Defaults

    Shared Async Function All(oEmp As DTOEmp, exs As List(Of Exception)) As Task(Of List(Of DTODefault))
        Return Await Api.Fetch(Of List(Of DTODefault))(exs, "Defaults", oEmp.Id)
    End Function

End Class
