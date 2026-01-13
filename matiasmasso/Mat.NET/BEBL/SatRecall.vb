Public Class SatRecall
    Shared Function Find(oGuid As Guid) As DTOSatRecall
        Return SatRecallLoader.Find(oGuid)
    End Function

    Shared Function fromIncidencia(oIncidencia As DTOIncidencia) As DTOSatRecall
        Return SatRecallLoader.fromIncidencia(oIncidencia)
    End Function

    Shared Function Update(oSatRecall As DTOSatRecall, ByRef exs As List(Of Exception)) As Boolean
        Return SatRecallLoader.Update(oSatRecall, exs)
    End Function

    Shared Function Delete(oSatRecall As DTOSatRecall, ByRef exs As List(Of Exception)) As Boolean
        Return SatRecallLoader.Delete(oSatRecall, exs)
    End Function

End Class


Public Class SatRecalls

    Shared Function All(Optional oEmp As DTOEmp = Nothing, Optional mode As DTOSatRecall.Modes = DTOSatRecall.Modes.PerAbonar) As List(Of DTO.Models.SatRecallModel)
        Return SatRecallsLoader.All(oEmp, mode)
    End Function
End Class