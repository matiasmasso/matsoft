Public Class PurchaseOrderConceptShortcut

    Shared Function Find(oGuid As Guid) As DTOPurchaseOrder.ConceptShortcut
        Return PurchaseOrderConceptShortcutLoader.Find(oGuid)
    End Function

    Shared Function Update(oTemplate As DTOPurchaseOrder.ConceptShortcut, exs As List(Of Exception)) As Boolean
        Return PurchaseOrderConceptShortcutLoader.Update(oTemplate, exs)
    End Function

    Shared Function Delete(oTemplate As DTOPurchaseOrder.ConceptShortcut, exs As List(Of Exception)) As Boolean
        Return PurchaseOrderConceptShortcutLoader.Delete(oTemplate, exs)
    End Function

End Class



Public Class PurchaseOrderConceptShortcuts
    Shared Function All() As List(Of DTOPurchaseOrder.ConceptShortcut)
        Return PurchaseOrderConceptShortcutsLoader.All()
    End Function
End Class
