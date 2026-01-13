Public Class PurchaseOrderConceptShortcut
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOPurchaseOrder.ConceptShortcut)
        Return Await Api.Fetch(Of DTOPurchaseOrder.ConceptShortcut)(exs, "PurchaseOrderConceptShortcut", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef value As DTOPurchaseOrder.ConceptShortcut) As Boolean
        If Not value.IsLoaded And Not value.IsNew Then
            Dim pPurchaseOrderConceptShortcut = Api.FetchSync(Of DTOPurchaseOrder.ConceptShortcut)(exs, "PurchaseOrderConceptShortcut", value.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOPurchaseOrder.ConceptShortcut)(pPurchaseOrderConceptShortcut, value, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), value As DTOPurchaseOrder.ConceptShortcut) As Task(Of Boolean)
        Return Await Api.Update(Of DTOPurchaseOrder.ConceptShortcut)(value, exs, "PurchaseOrderConceptShortcut")
        value.IsNew = False
    End Function


    Shared Async Function Delete(exs As List(Of Exception), value As DTOPurchaseOrder.ConceptShortcut) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOPurchaseOrder.ConceptShortcut)(value, exs, "PurchaseOrderConceptShortcut")
    End Function
End Class

Public Class PurchaseOrderConceptShortcuts
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOPurchaseOrder.ConceptShortcut))
        Return Await Api.Fetch(Of List(Of DTOPurchaseOrder.ConceptShortcut))(exs, "PurchaseOrderConceptShortcuts")
    End Function

End Class
