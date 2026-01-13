Public Class PgcSaldo2Loader

    Shared Function All(oEmp As DTOEmp, DtFch1a As Date, DtFch1b As Date, DtFch2a As Date, DtFch2b As Date) As List(Of DTOPgcSaldo2)
        Dim retval As New List(Of DTOPgcSaldo2)

        Dim sFch1a As String = Format(DtFch1a, "yyyyMMdd")
        Dim sFch1b As String = Format(DtFch1b, "yyyyMMdd")
        Dim sFch2a As String = Format(DtFch2a, "yyyyMMdd")
        Dim sFch2b As String = Format(DtFch2b, "yyyyMMdd")

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Ccb.CtaGuid, PgcCta.Id, PgcCta.Esp, PgcCta.Cat, PgcCta.Eng, PgcCta.Act ")
        sb.AppendLine(", SUM(CASE WHEN Ccb.Fch BETWEEN '" & sFch1a & "' AND '" & sFch1b & "' THEN CASE WHEN DH=PgcCta.Act THEN EUR ELSE -EUR END ELSE 0 END) AS EUR1 ")
        sb.AppendLine(", SUM(CASE WHEN Ccb.Fch BETWEEN '" & sFch2a & "' AND '" & sFch2b & "' THEN CASE WHEN DH=PgcCta.Act THEN EUR ELSE -EUR END ELSE 0 END) AS EUR2 ")
        sb.AppendLine("FROM Ccb ")
        sb.AppendLine("LEFT OUTER JOIN PgcCta ON Ccb.CtaGuid=PgcCta.Guid ")
        sb.AppendLine("WHERE Ccb.Fch BETWEEN '" & sFch1a & "' AND '" & sFch1b & "' ")
        sb.AppendLine("OR Ccb.Fch BETWEEN '" & sFch2a & "' AND '" & sFch2b & "' ")
        sb.AppendLine("GROUP BY Ccb.CtaGuid, PgcCta.Id, PgcCta.Esp, PgcCta.Cat, PgcCta.Eng, PgcCta.Act ")
        sb.AppendLine("ORDER BY PgcCta.Id ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oCta As New DTOPgcCta(oDrd("CtaGuid"))
            With oCta
                .Id = SQLHelper.GetIntegerFromDataReader(oDrd("Id"))
                .NomEsp = SQLHelper.GetStringFromDataReader(oDrd("Esp"))
                .NomCat = SQLHelper.GetStringFromDataReader(oDrd("Cat"))
                .NomEng = SQLHelper.GetStringFromDataReader(oDrd("Eng"))
                .Act = SQLHelper.GetIntegerFromDataReader(oDrd("Act"))
            End With
            Dim item As New DTOPgcSaldo2
            With item
                .Cta = oCta
                .Saldo1 = SQLHelper.GetAmtFromDataReader(oDrd("Eur1"))
                .Saldo2 = SQLHelper.GetAmtFromDataReader(oDrd("Eur2"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()

        Return retval
    End Function
End Class
