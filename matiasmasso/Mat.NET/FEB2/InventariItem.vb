Public Class InventariItem
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOImmoble.InventariItem)
        Return Await Api.Fetch(Of DTOImmoble.InventariItem)(exs, "InventariItem", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oInventariItem As DTOImmoble.InventariItem) As Boolean
        If Not oInventariItem.IsLoaded And Not oInventariItem.IsNew Then
            Dim pInventariItem = Api.FetchSync(Of DTOImmoble.InventariItem)(exs, "InventariItem", oInventariItem.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOImmoble.InventariItem)(pInventariItem, oInventariItem, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oInventariItem As DTOImmoble.InventariItem) As Task(Of Boolean)
        Return Await Api.Update(Of DTOImmoble.InventariItem)(oInventariItem, exs, "InventariItem")
        oInventariItem.IsNew = False
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oInventariItem As DTOImmoble.InventariItem) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOImmoble.InventariItem)(oInventariItem, exs, "InventariItem")
    End Function
End Class

Public Class InventariItems
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oImmoble As DTOImmoble) As Task(Of DTOImmoble.InventariItem.Collection)
        Return Await Api.Fetch(Of DTOImmoble.InventariItem.Collection)(exs, "InventariItems", oImmoble.Guid.ToString)
    End Function

End Class

