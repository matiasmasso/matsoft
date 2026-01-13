Public Class EdiversaRemadv

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOEdiversaRemadv)
        Return Await Api.Fetch(Of DTOEdiversaRemadv)(exs, "EdiversaRemadv", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oEdiversaRemadv As DTOEdiversaRemadv, exs As List(Of Exception)) As Boolean
        If Not oEdiversaRemadv.IsLoaded And Not oEdiversaRemadv.IsNew Then
            Dim pEdiversaRemadv = Api.FetchSync(Of DTOEdiversaRemadv)(exs, "EdiversaRemadv", oEdiversaRemadv.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOEdiversaRemadv)(pEdiversaRemadv, oEdiversaRemadv, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oEdiversaRemadv As DTOEdiversaRemadv, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOEdiversaRemadv)(oEdiversaRemadv, exs, "EdiversaRemadv")
        oEdiversaRemadv.IsNew = False
    End Function

    Shared Async Function Retrocedeix(oEdiversaRemadv As DTOEdiversaRemadv, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOEdiversaRemadv)(oEdiversaRemadv, exs, "EdiversaRemadv")
    End Function
End Class

Public Class EdiversaRemadvs

    Shared Async Function All(DisplayObsolets As Boolean, exs As List(Of Exception)) As Task(Of List(Of DTOEdiversaRemadv))
        Dim retval = Await Api.Fetch(Of List(Of DTOEdiversaRemadv))(exs, "EdiversaRemadvs", If(DisplayObsolets, 1, 0))
        For Each remadv In retval
            For Each item In remadv.Items
                item.Parent = remadv
            Next
        Next
        Return retval
    End Function

End Class
