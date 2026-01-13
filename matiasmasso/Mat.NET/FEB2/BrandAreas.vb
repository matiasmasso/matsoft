Public Class BrandArea

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOBrandArea)
        Return Await Api.Fetch(Of DTOBrandArea)(exs, "BrandArea", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oBrandArea As DTOBrandArea, exs As List(Of Exception)) As Boolean
        If Not oBrandArea.IsLoaded And Not oBrandArea.IsNew Then
            Dim pBrandArea = Api.FetchSync(Of DTOBrandArea)(exs, "BrandArea", oBrandArea.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOBrandArea)(pBrandArea, oBrandArea, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oBrandArea As DTOBrandArea, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOBrandArea)(oBrandArea, exs, "BrandArea")
        oBrandArea.IsNew = False
    End Function

    Shared Async Function Delete(oBrandArea As DTOBrandArea, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOBrandArea)(oBrandArea, exs, "BrandArea")
    End Function
End Class

Public Class BrandAreas

    Shared Async Function All(oBrand As DTOProductBrand, exs As List(Of Exception)) As Task(Of List(Of DTOBrandArea))
        Dim retval = Await Api.Fetch(Of List(Of DTOBrandArea))(exs, "BrandAreas", oBrand.Guid.ToString())
        Return retval
    End Function

End Class

