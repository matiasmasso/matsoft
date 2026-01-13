Public Class RepCliCom
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTORepCliCom)
        Return Await Api.Fetch(Of DTORepCliCom)(exs, "RepCliCom", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oRepCliCom As DTORepCliCom) As Boolean
        If Not oRepCliCom.IsLoaded And Not oRepCliCom.IsNew Then
            Dim pRepCliCom = Api.FetchSync(Of DTORepCliCom)(exs, "RepCliCom", oRepCliCom.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTORepCliCom)(pRepCliCom, oRepCliCom, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oRepCliCom As DTORepCliCom) As Task(Of Boolean)
        Return Await Api.Update(Of DTORepCliCom)(oRepCliCom, exs, "RepCliCom")
        oRepCliCom.IsNew = False
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oRepCliCom As DTORepCliCom) As Task(Of Boolean)
        Return Await Api.Delete(Of DTORepCliCom)(oRepCliCom, exs, "RepCliCom")
    End Function

    Shared Async Function Match(exs As List(Of Exception), oEmp As DTOEmp, oRep As DTORep, oCustomer As DTOCustomer, DtFch As Date, Optional oRepCliComs As List(Of DTORepCliCom) = Nothing) As Task(Of DTORepCliCom)
        If oRepCliComs Is Nothing Then oRepCliComs = Await FEB2.RepCliComs.All(exs, oEmp)
        Dim retval As DTORepCliCom = oRepCliComs.FindAll(Function(x) x.Rep.Equals(oRep) And x.Customer.Equals(oCustomer) And x.Fch <= DtFch).
            OrderBy(Function(x) x.Fch).
            LastOrDefault

        Return retval
    End Function
End Class

Public Class RepCliComs
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oRep As DTORep) As Task(Of List(Of DTORepCliCom))
        Return Await Api.Fetch(Of List(Of DTORepCliCom))(exs, "RepCliComs/fromRep", oRep.Guid.ToString())
    End Function

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of List(Of DTORepCliCom))
        Return Await Api.Fetch(Of List(Of DTORepCliCom))(exs, "RepCliComs/fromEmp", oEmp.Id)
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oRepCliComs As List(Of DTORepCliCom)) As Task(Of Boolean)
        Return Await Api.Delete(Of List(Of DTORepCliCom))(oRepCliComs, exs, "RepCliComs")
    End Function

End Class
