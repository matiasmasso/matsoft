Public Class ProductPluginItem
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOProductPlugin.Item)
        Return Await Api.Fetch(Of DTOProductPlugin.Item)(exs, "ProductPluginItem", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oProductPluginItem As DTOProductPlugin.Item) As Boolean
        If Not oProductPluginItem.IsLoaded And Not oProductPluginItem.IsNew Then
            Dim pProductPluginItem = Api.FetchSync(Of DTOProductPlugin.Item)(exs, "ProductPluginItem", oProductPluginItem.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOProductPlugin.Item)(pProductPluginItem, oProductPluginItem, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oProductPluginItem As DTOProductPlugin.Item) As Task(Of Boolean)
        Return Await Api.Update(Of DTOProductPlugin.Item)(oProductPluginItem, exs, "ProductPluginItem")
        oProductPluginItem.IsNew = False
    End Function


    Shared Async Function Delete(exs As List(Of Exception), oProductPluginItem As DTOProductPlugin.Item) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOProductPlugin.Item)(oProductPluginItem, exs, "ProductPluginItem")
    End Function

End Class
