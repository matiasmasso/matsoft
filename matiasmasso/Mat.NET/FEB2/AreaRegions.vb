Public Class AreaRegio

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOAreaRegio)
        Return Await Api.Fetch(Of DTOAreaRegio)(exs, "AreaRegio", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oAreaRegio As DTOAreaRegio, exs As List(Of Exception)) As Boolean
        If Not oAreaRegio.IsLoaded And Not oAreaRegio.IsNew Then
            Dim pAreaRegio = Api.FetchSync(Of DTOAreaRegio)(exs, "AreaRegio", oAreaRegio.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOAreaRegio)(pAreaRegio, oAreaRegio, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oAreaRegio As DTOAreaRegio, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOAreaRegio)(oAreaRegio, exs, "AreaRegio")
        oAreaRegio.IsNew = False
    End Function


    Shared Async Function Delete(oAreaRegio As DTOAreaRegio, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOAreaRegio)(oAreaRegio, exs, "AreaRegio")
    End Function
End Class

Public Class AreaRegions

    Shared Async Function All(oCountry As DTOCountry, exs As List(Of Exception)) As Task(Of List(Of DTOAreaRegio))
        Return Await Api.Fetch(Of List(Of DTOAreaRegio))(exs, "AreaRegions", oCountry.Guid.ToString())
    End Function

End Class

