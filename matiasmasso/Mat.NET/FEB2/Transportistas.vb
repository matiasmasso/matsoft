Public Class Transportista
    Inherits _FeblBase

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOTransportista)
        Return Await Api.Fetch(Of DTOTransportista)(exs, "Transportista", oGuid.ToString())
    End Function

    Shared Function ExistsSync(oContact As DTOContact, exs As List(Of Exception)) As Boolean
        Return Api.FetchSync(Of Boolean)(exs, "Transportista/Exists", oContact.Guid.ToString())
    End Function

    Shared Function Load(ByRef oTransportista As DTOTransportista, exs As List(Of Exception)) As Boolean
        If Not oTransportista.IsLoaded And Not oTransportista.IsNew Then
            Dim pTransportista = Api.FetchSync(Of DTOTransportista)(exs, "Transportista", oTransportista.Guid.ToString())
            If exs.Count = 0 AndAlso pTransportista IsNot Nothing Then
                DTOBaseGuid.CopyPropertyValues(Of DTOTransportista)(pTransportista, oTransportista, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oTransportista As DTOTransportista, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOTransportista)(oTransportista, exs, "Transportista")
        oTransportista.IsNew = False
    End Function

    Shared Async Function Delete(oTransportista As DTOTransportista, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOTransportista)(oTransportista, exs, "Transportista")
    End Function
End Class

Public Class Transportistas
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp, onlyActive As Boolean) As Task(Of List(Of DTOTransportista))
        Return Await Api.Fetch(Of List(Of DTOTransportista))(exs, "Transportistas", oEmp.Id, OpcionalBool(onlyActive))
    End Function

End Class
