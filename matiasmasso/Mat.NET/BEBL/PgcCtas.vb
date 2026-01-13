Public Class PgcCta
    Shared Function Find(oGuid As Guid) As DTOPgcCta
        Dim retval As DTOPgcCta = PgcCtaLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oPgcCta As DTOPgcCta) As Boolean
        Dim retval As Boolean = PgcCtaLoader.Load(oPgcCta)
        Return retval
    End Function

    Shared Function Update(oPgcCta As DTOPgcCta, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = PgcCtaLoader.Update(oPgcCta, exs)
        Return retval
    End Function

    Shared Function Delete(oPgcCta As DTOPgcCta, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = PgcCtaLoader.Delete(oPgcCta, exs)
        Return retval
    End Function

    Shared Function IsActivable(oCta As DTOPgcCta) As Boolean
        Load(oCta)
        Dim retval As Boolean = oCta.Id.StartsWith("2")
        Return retval
    End Function


    Shared Function isQuotaIrpfWithLoad(ByRef oCta As DTOPgcCta) As Boolean 'TO DEPRECATE
        Load(oCta)
        Dim retval As Boolean = oCta.isQuotaIrpf
        Return retval
    End Function

    Shared Function Saldo(oEmp As DTOEmp, oCta As DTOPgcCta, Optional oContact As DTOContact = Nothing, Optional ByVal DtFch As Date = Nothing) As DTOAmt
        Dim RetVal As DTOAmt = PgcCtaLoader.Saldo(oEmp, oCta, oContact, DtFch)
        Return RetVal
    End Function

    Shared Function FromCod(oCod As DTOPgcPlan.Ctas, oExercici As DTOExercici) As DTOPgcCta
        Dim retval As DTOPgcCta = PgcCtaLoader.FromCod(oCod, oExercici)
        Return retval
    End Function

    Shared Function FromCod(oCod As DTOPgcPlan.Ctas, oEmp As DTOEmp) As DTOPgcCta
        Dim oExercici = New DTOExercici(oEmp, DTO.GlobalVariables.Today().Year)
        Dim retval As DTOPgcCta = PgcCtaLoader.FromCod(oCod, oExercici)
        Return retval
    End Function

    Shared Function FromId(oPlan As DTOPgcPlan, sId As String) As DTOPgcCta
        Dim retval As DTOPgcCta = PgcCtaLoader.FromId(oPlan, sId)
        Return retval
    End Function
End Class

Public Class PgcCtas
    Shared Function Current(Optional year As Integer = 0) As List(Of DTOPgcCta)
        If year = 0 Then year = DTO.GlobalVariables.Today().Year
        Dim retval = PgcCtasLoader.Current(year)
        Return retval
    End Function


    Shared Function All(oPlan As DTOPgcPlan, Optional sSearchKey As String = "") As DTOPgcCta.Collection
        Dim retval As DTOPgcCta.Collection = PgcCtasLoader.All(oPlan, sSearchKey)
        Return retval
    End Function

    Shared Function All(oPlan As DTOPgcPlan, oClass As DTOPgcClass) As List(Of DTOPgcCta)
        Dim retval As List(Of DTOPgcCta) = PgcCtasLoader.All(oPlan, oClass)
        Return retval
    End Function

    Shared Function All(oExercici As DTOExercici, oContact As DTOContact) As List(Of DTOPgcCta)
        Dim retval As List(Of DTOPgcCta) = PgcCtasLoader.All(oExercici, oContact)
        Return retval
    End Function

End Class
