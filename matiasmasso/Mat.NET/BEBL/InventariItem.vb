Public Class InventariItem

    Shared Function Find(oGuid As Guid) As DTOImmoble.InventariItem
        Return InventariItemLoader.Find(oGuid)
    End Function

    Shared Function Update(oInventariItemLoader As DTOImmoble.InventariItem, exs As List(Of Exception)) As Boolean
        Return InventariItemLoader.Update(oInventariItemLoader, exs)
    End Function

    Shared Function Delete(oInventariItemLoader As DTOImmoble.InventariItem, exs As List(Of Exception)) As Boolean
        Return InventariItemLoader.Delete(oInventariItemLoader, exs)
    End Function

End Class



Public Class InventariItems
    Shared Function All(oImmoble As DTOImmoble) As DTOImmoble.InventariItem.Collection
        Return InventariItemsLoader.All(oImmoble)
    End Function
End Class

