Public Class SatRecall

    Shared Function Find(oGuid As Guid) As DTOSatRecall
        Dim retval As DTOSatRecall = SatRecallLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function fromIncidencia(oIncidencia As DTOIncidencia) As DTOSatRecall
        Dim retval As DTOSatRecall = SatRecallLoader.fromIncidencia(oIncidencia)
        If retval Is Nothing Then
            retval = New DTOSatRecall
            If BEBL.Incidencia.Load(oIncidencia) Then
                retval.Incidencia = oIncidencia
                If retval.Address Is Nothing Then
                    BEBL.Customer.Load(oIncidencia.Customer)
                    retval.Address = oIncidencia.Customer.Address
                End If
                If oIncidencia.Codi IsNot Nothing Then
                    retval.Defect = oIncidencia.Codi.Esp
                End If
                retval.ContactPerson = oIncidencia.ContactPerson
                retval.Tel = oIncidencia.Tel
            End If
        End If
        Return retval
    End Function

    Shared Function Update(oSatRecall As DTOSatRecall, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = SatRecallLoader.Update(oSatRecall, exs)
        Return retval
    End Function

    Shared Function Delete(oSatRecall As DTOSatRecall, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = SatRecallLoader.Delete(oSatRecall, exs)
        Return retval
    End Function

End Class



Public Class SatRecalls
    Shared Function All(oEmp As DTOEmp) As List(Of DTOSatRecall)
        Dim retval As List(Of DTOSatRecall) = SatRecallsLoader.All(oEmp)
        Return retval
    End Function
End Class
