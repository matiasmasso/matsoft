Public Class Plantilla

    Shared Function Find(oGuid As Guid) As DTOPlantilla
        Return PlantillaLoader.Find(oGuid)
    End Function

    Shared Function Update(oPlantilla As DTOPlantilla, oEmp As DTOEmp, exs As List(Of Exception)) As Boolean
        Return PlantillaLoader.Update(oPlantilla, oEmp, exs)
    End Function

    Shared Function Delete(oPlantilla As DTOPlantilla, exs As List(Of Exception)) As Boolean
        Return PlantillaLoader.Delete(oPlantilla, exs)
    End Function

End Class



Public Class Plantillas
    Shared Function All(oEmp As DTOEmp) As List(Of DTOPlantilla)
        Return PlantillasLoader.All(oEmp)
    End Function
End Class

