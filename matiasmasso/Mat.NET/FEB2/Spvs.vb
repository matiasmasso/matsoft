Public Class Spv
    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOSpv)
        Dim retval As DTOSpv = Await Api.Fetch(Of DTOSpv)(exs, "spv", oGuid.ToString())
        retval.restoreObjects()
        Return retval
    End Function

    Shared Async Function FromId(oEmp As DTOEmp, iYea As Integer, iNum As Integer, exs As List(Of Exception)) As Task(Of DTOSpv)
        Dim retval As DTOSpv = Await Api.Fetch(Of DTOSpv)(exs, "spv", oEmp.Id, iYea, iNum)
        If retval IsNot Nothing Then retval.restoreObjects()
        Return retval
    End Function

    Shared Function FromIdSync(oEmp As DTOEmp, iYea As Integer, iNum As Integer, exs As List(Of Exception)) As DTOSpv
        Dim retval As DTOSpv = Api.FetchSync(Of DTOSpv)(exs, "spv", oEmp.Id, iYea, iNum)
        retval.restoreObjects()
        Return retval
    End Function

    Shared Function Load(ByRef oSpv As DTOSpv, exs As List(Of Exception)) As Boolean
        If Not oSpv.IsLoaded And Not oSpv.IsNew Then
            Dim pSpv = Api.FetchSync(Of DTOSpv)(exs, "spv", oSpv.Guid.ToString())
            If exs.Count = 0 And pSpv IsNot Nothing Then
                DTOBaseGuid.CopyPropertyValues(Of DTOSpv)(pSpv, oSpv, exs)
                oSpv.restoreObjects()
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oSpv As DTOSpv, exs As List(Of Exception)) As Task(Of DTOSpv)
        Dim retval = Await Api.Execute(Of DTOSpv, DTOSpv)(oSpv, exs, "Spv")
        retval.restoreObjects()
        oSpv.IsNew = False
        Return retval
    End Function

    Shared Async Function Delete(oSpv As DTOSpv, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOSpv)(oSpv, exs, "Spv")
    End Function


End Class

Public Class Spvs
    Inherits _FeblBase

    Shared Async Function ArrivalPending(oUser As DTOUser, exs As List(Of Exception)) As Task(Of List(Of DTOSpv))
        Dim retval = Await Api.Fetch(Of List(Of DTOSpv))(exs, "spvs/ArrivalPending", oUser.Guid.ToString())
        For Each oSpv In retval
            oSpv.restoreObjects()
        Next
        Return retval
    End Function

    Shared Async Function SpvsNotRead(oUser As DTOUser, exs As List(Of Exception)) As Task(Of List(Of DTOSpv))
        Dim retval = Await Api.Fetch(Of List(Of DTOSpv))(exs, "spvs/ReadPending", oUser.Guid.ToString())
        For Each oSpv In retval
            oSpv.restoreObjects()
        Next
        Return retval
    End Function

    Shared Async Function Headers(exs As List(Of Exception), oEmp As DTOEmp, Optional oCustomer As DTOCustomer = Nothing, Optional year As Integer = 0) As Task(Of List(Of DTOSpv))
        Dim retval = Await Api.Fetch(Of List(Of DTOSpv))(exs, "spvs/Headers", oEmp.Id, OpcionalGuid(oCustomer), year)
        For Each oSpv In retval
            oSpv.restoreObjects()
        Next
        Return retval
    End Function

    Shared Async Function OpenHeaders(exs As List(Of Exception), oEmp As DTOEmp, Optional oCustomer As DTOCustomer = Nothing) As Task(Of List(Of DTOSpv))
        Dim retval = Await Api.Fetch(Of List(Of DTOSpv))(exs, "spvs/openHeaders", oEmp.Id, OpcionalGuid(oCustomer))
        For Each oSpv In retval
            oSpv.restoreObjects()
        Next
        Return retval
    End Function

    Shared Async Function All(exs As List(Of Exception), oSpvIn As DTOSpvIn) As Task(Of List(Of DTOSpv))
        Dim retval = Await Api.Fetch(Of List(Of DTOSpv))(exs, "spvIn/spvs", oSpvIn.Guid.ToString())
        For Each oSpv In retval
            oSpv.restoreObjects()
        Next
        Return retval
    End Function
End Class
