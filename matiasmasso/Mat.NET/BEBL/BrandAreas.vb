Public Class BrandArea


    Shared Function Find(oGuid As Guid) As DTOBrandArea
        Dim retval As DTOBrandArea = BrandAreaLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Update(oBrandArea As DTOBrandArea, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = BrandAreaLoader.Update(oBrandArea, exs)
        Return retval
    End Function

    Shared Function Delete(oBrandArea As DTOBrandArea, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = BrandAreaLoader.Delete(oBrandArea, exs)
        Return retval
    End Function

End Class



Public Class BrandAreas
    Shared Function All(oBrand As DTOProductBrand) As List(Of DTOBrandArea)
        Dim retval As List(Of DTOBrandArea) = BrandAreasLoader.FromBrand(oBrand)
        Return retval
    End Function

End Class

