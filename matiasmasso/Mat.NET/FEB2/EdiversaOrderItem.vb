Public Class EdiversaOrderItem


    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOEdiversaOrderItem)
        Return Await Api.Fetch(Of DTOEdiversaOrderItem)(exs, "EdiversaOrderItem", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oEdiversaOrderItem As DTOEdiversaOrderItem, exs As List(Of Exception)) As Boolean
        If Not oEdiversaOrderItem.IsLoaded And Not oEdiversaOrderItem.IsNew Then
            Dim pEdiversaOrderItem = Api.FetchSync(Of DTOEdiversaOrderItem)(exs, "EdiversaOrderItem", oEdiversaOrderItem.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOEdiversaOrderItem)(pEdiversaOrderItem, oEdiversaOrderItem, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oEdiversaOrderItem As DTOEdiversaOrderItem, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOEdiversaOrderItem)(oEdiversaOrderItem, exs, "EdiversaOrderItem")
        oEdiversaOrderItem.IsNew = False
    End Function


    Shared Async Function Delete(oEdiversaOrderItem As DTOEdiversaOrderItem, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOEdiversaOrderItem)(oEdiversaOrderItem, exs, "EdiversaOrderItem")
    End Function


End Class


