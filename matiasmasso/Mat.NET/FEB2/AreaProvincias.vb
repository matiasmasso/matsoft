Public Class AreaProvincia
    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOAreaProvincia)
        Return Await Api.Fetch(Of DTOAreaProvincia)(exs, "AreaProvincia", oGuid.ToString())
    End Function

    '
    Shared Async Function FromSpanishZipCod(sZipCod As String, exs As List(Of Exception)) As Task(Of DTOAreaProvincia)
        Return Await Api.Fetch(Of DTOAreaProvincia)(exs, "AreaProvincia/FromSpanishZipCod", sZipCod)
    End Function

    Shared Function Load(ByRef oAreaProvincia As DTOAreaProvincia, exs As List(Of Exception)) As Boolean
        If Not oAreaProvincia.IsLoaded And Not oAreaProvincia.IsNew Then
            Dim pAreaProvincia = Api.FetchSync(Of DTOAreaProvincia)(exs, "AreaProvincia", oAreaProvincia.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOAreaProvincia)(pAreaProvincia, oAreaProvincia, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oAreaProvincia As DTOAreaProvincia, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOAreaProvincia)(oAreaProvincia, exs, "AreaProvincia")
        oAreaProvincia.IsNew = False
    End Function


    Shared Async Function Delete(oAreaProvincia As DTOAreaProvincia, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOAreaProvincia)(oAreaProvincia, exs, "AreaProvincia")
    End Function

    Shared Async Function Zonas(oProvincia As DTOAreaProvincia, exs As List(Of Exception)) As Task(Of List(Of DTOZona))
        Return Await Api.Fetch(Of List(Of DTOZona))(exs, "AreaProvincia/Zonas", oProvincia.Guid.ToString())
    End Function


End Class

Public Class AreaProvincias

    Shared Async Function All(oCountry As DTOCountry, exs As List(Of Exception)) As Task(Of List(Of DTOAreaProvincia))
        Return Await Api.Fetch(Of List(Of DTOAreaProvincia))(exs, "AreaProvincias", oCountry.Guid.ToString())
    End Function

End Class

