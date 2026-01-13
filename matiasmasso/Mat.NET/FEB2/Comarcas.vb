Public Class Comarca

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOComarca)
        Return Await Api.Fetch(Of DTOComarca)(exs, "Comarca", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oComarca As DTOComarca, exs As List(Of Exception)) As Boolean
        If Not oComarca.IsLoaded And Not oComarca.IsNew Then
            Dim pComarca = Api.FetchSync(Of DTOComarca)(exs, "Comarca", oComarca.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOComarca)(pComarca, oComarca, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oComarca As DTOComarca, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOComarca)(oComarca, exs, "Comarca")
        oComarca.IsNew = False
    End Function


    Shared Async Function Delete(oComarca As DTOComarca, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOComarca)(oComarca, exs, "Comarca")
    End Function
End Class

Public Class Comarcas

    Shared Async Function All(oZona As DTOZona, exs As List(Of Exception)) As Task(Of List(Of DTOComarca))
        Return Await Api.Fetch(Of List(Of DTOComarca))(exs, "Comarcas", oZona.Guid.ToString())
    End Function

End Class
