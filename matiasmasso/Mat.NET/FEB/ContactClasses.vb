Public Class ContactClass
    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOContactClass)
        Return Await Api.Fetch(Of DTOContactClass)(exs, "ContactClass", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oContactClass As DTOContactClass, exs As List(Of Exception)) As Boolean
        If Not oContactClass.IsLoaded And Not oContactClass.IsNew Then
            Dim pContactClass = Api.FetchSync(Of DTOContactClass)(exs, "ContactClass", oContactClass.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOContactClass)(pContactClass, oContactClass, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oContactClass As DTOContactClass, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOContactClass)(oContactClass, exs, "ContactClass")
        oContactClass.IsNew = False
    End Function

    Shared Async Function Delete(oContactClass As DTOContactClass, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOContactClass)(oContactClass, exs, "ContactClass")
    End Function
End Class

Public Class ContactClasses

    Shared Async Function All(exs As List(Of Exception)) As Task(Of DTOContactClass.Collection)
        Dim retval = Await Api.Fetch(Of DTOContactClass.Collection)(exs, "ContactClasses")
        Return retval
    End Function

    Shared Function AllWithChannelSync(exs As List(Of Exception)) As DTOContactClass.Collection
        Return Api.FetchSync(Of DTOContactClass.Collection)(exs, "ContactClasses/AllWithChannel")
    End Function

End Class