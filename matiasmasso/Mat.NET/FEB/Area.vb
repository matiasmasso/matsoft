Public Class Area

    Shared Function FindSync(oGuid As Guid, exs As List(Of Exception)) As DTOArea
        Return Api.FetchSync(Of DTOArea)(exs, "Area", oGuid.ToString())
    End Function
    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOArea)
        Return Await Api.Fetch(Of DTOArea)(exs, "Area", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oArea As DTOArea, exs As List(Of Exception)) As Boolean
        If Not oArea.IsLoaded And Not oArea.IsNew Then
            Dim pArea = Api.FetchSync(Of DTOArea)(exs, "Area", oArea.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOArea)(pArea, oArea, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

End Class
