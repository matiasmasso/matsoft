Public Class Correspondencia

    Shared Function Find(oGuid As Guid) As DTOCorrespondencia
        Dim retval As DTOCorrespondencia = CorrespondenciaLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Update(oCorrespondencia As DTOCorrespondencia, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = CorrespondenciaLoader.Update(oCorrespondencia, exs)
        Return retval
    End Function

    Shared Function Delete(oCorrespondencia As DTOCorrespondencia, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = CorrespondenciaLoader.Delete(oCorrespondencia, exs)
        Return retval
    End Function

End Class



Public Class Correspondencias
    Shared Function All(oContact As DTOContact) As List(Of DTOCorrespondencia)
        Dim retval As List(Of DTOCorrespondencia) = CorrespondenciasLoader.All(oContact)
        Return retval
    End Function
End Class