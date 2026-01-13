Public Class Nomina

#Region "CRUD"
    Shared Function Find(oCca As DTOCca) As DTONomina
        Dim retval As DTONomina = NominaLoader.Find(oCca)
        Return retval
    End Function

    Shared Function Load(ByRef oNomina As DTONomina) As Boolean
        Dim retval As Boolean = NominaLoader.Load(oNomina)
        Return retval
    End Function

    Shared Function Update(oNomina As DTONomina, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = NominaLoader.Update(oNomina, exs)
        Return retval
    End Function

    Shared Function Delete(oNomina As DTONomina, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = NominaLoader.Delete(oNomina, exs)
        Return retval
    End Function
#End Region

End Class

Public Class Nominas

    Shared Function All(oStaff As DTOStaff) As List(Of DTONomina)
        Dim retval As List(Of DTONomina) = NominesLoader.All(oStaff:=oStaff)
        Return retval
    End Function

    Shared Function All(oUser As DTOUser, Optional year As Integer = 0) As List(Of DTONomina)
        Dim retval As List(Of DTONomina) = NominesLoader.All(oUser:=oUser, year:=year)
        Return retval
    End Function

    Shared Function All(oExercici As DTOExercici) As List(Of DTONomina)
        Dim retval As List(Of DTONomina) = NominesLoader.All(oExercici)
        Return retval
    End Function

End Class
