Public Class ProductPlugin

    Shared Function Find(oGuid As Guid) As DTOProductPlugin
        Return ProductPluginLoader.Find(oGuid)
    End Function

    Shared Function Sprite(oGuid As Guid) As Byte()
        Dim oImages = ProductPluginLoader.Sprite(oGuid)
        Dim retval = LegacyHelper.SpriteBuilder.Factory(oImages, DTOProductPlugin.Item.Width, DTOProductPlugin.Item.Height)
        Return retval
    End Function

    Shared Function Update(oProductPlugin As DTOProductPlugin, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        If ProductPluginLoader.Update(oProductPlugin, exs) Then
            BEBL.ServerCache.NotifyUpdate(Models.ServerCache.Tables.ProductPlugins)
            retval = True
        End If
        Return retval
    End Function

    Shared Function Delete(oProductPlugin As DTOProductPlugin, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        If ProductPluginLoader.Delete(oProductPlugin, exs) Then
            BEBL.ServerCache.NotifyUpdate(Models.ServerCache.Tables.ProductPlugins)
            retval = True
        End If
        Return retval
    End Function

End Class



Public Class ProductPlugins
    Shared Function All(oProduct As DTOProduct) As List(Of DTOProductPlugin)
        Dim retval As List(Of DTOProductPlugin) = ProductPluginsLoader.All(oProduct)
        Return retval
    End Function
End Class
