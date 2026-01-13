Public Class PortsCondicio
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oPortsCondicio As DTOPortsCondicio) As Task(Of DTOPortsCondicio)
        Return Await Api.Fetch(Of DTOPortsCondicio)(exs, "PortsCondicio", oPortsCondicio.Guid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oPortsCondicio As DTOPortsCondicio) As Boolean
        If Not oPortsCondicio.IsLoaded And Not oPortsCondicio.IsNew Then
            Dim pPortsCondicio = Api.FetchSync(Of DTOPortsCondicio)(exs, "PortsCondicio", oPortsCondicio.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOPortsCondicio)(pPortsCondicio, oPortsCondicio, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), value As DTOPortsCondicio) As Task(Of Boolean)
        Return Await Api.Update(Of DTOPortsCondicio)(value, exs, "PortsCondicio")
        value.IsNew = False
    End Function

    Shared Async Function Delete(exs As List(Of Exception), value As DTOPortsCondicio) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOPortsCondicio)(value, exs, "PortsCondicio")
    End Function

    Shared Async Function Customers(exs As List(Of Exception), oPortsCondicio As DTOPortsCondicio) As Task(Of List(Of DTOCustomer))
        Dim retval As New List(Of DTOCustomer)
        Dim values = Await Api.Fetch(Of List(Of DTOGuidNom.Compact))(exs, "PortsCondicio/customers", oPortsCondicio.Guid.ToString())
        If retval IsNot Nothing Then
            For Each value In values
                Dim item As New DTOCustomer(value.Guid)
                item.FullNom = value.Nom
                retval.Add(item)
            Next
        End If
        Return retval
    End Function

End Class

Public Class PortsCondicions
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOPortsCondicio))
        Return Await Api.Fetch(Of List(Of DTOPortsCondicio))(exs, "PortsCondicions")
    End Function

End Class
