Public Class ProductPluginItem

    Shared Function Find(oGuid As Guid) As DTOProductPlugin.Item
        Return ProductPluginItemLoader.Find(oGuid)
    End Function

    Shared Function Update(item As DTOProductPlugin.Item, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        If ProductPluginItemLoader.Update(item, exs) Then
            BEBL.ServerCache.NotifyUpdate(Models.ServerCache.Tables.ProductPlugins)
            retval = True
        End If
        Return retval
    End Function

    Shared Function Delete(Item As DTOProductPlugin.Item, exs As List(Of Exception)) As Boolean
        Return ProductPluginItemLoader.Delete(Item, exs)
    End Function
End Class
