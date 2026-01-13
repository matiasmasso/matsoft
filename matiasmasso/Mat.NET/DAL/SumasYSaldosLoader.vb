Public Class SumasYSaldosLoader

    Shared Function Summary(oEmp As DTOEmp, DtFch As Date) As DTOSumasYSaldos
        Dim retval As New DTOSumasYSaldos(DtFch)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Ccb.CtaGuid, PgcCta.Id AS CtaId, PgcCta.Act, PgcCta.ESP, PgcCta.CAT, PgcCta.ENG, PgcCta.Cod ")
        sb.AppendLine(", SUM(CASE WHEN Cca.CCD = 1 THEN (CASE WHEN Ccb.Dh = 1 THEN Ccb.Eur ELSE - Ccb.Eur END) ELSE 0 END) AS SdoInicial ")
        sb.AppendLine(", SUM(CASE WHEN Ccb.Dh = 1 AND Cca.CCD <> 1 THEN Ccb.Eur ELSE 0 END) AS Debe ")
        sb.AppendLine(", SUM(CASE WHEN Ccb.Dh = 2 AND Cca.CCD <> 1 THEN Ccb.Eur ELSE 0 END) AS Haber ")
        sb.AppendLine(", SUM(CASE WHEN Ccb.Dh = 1 THEN Ccb.Eur ELSE - Ccb.Eur END) AS SdoFinal ")
        sb.AppendLine("FROM Ccb ")
        sb.AppendLine("INNER JOIN Cca ON Ccb.CcaGuid = Cca.Guid ")
        sb.AppendLine("LEFT OUTER JOIN PgcCta ON Ccb.CtaGuid = PgcCta.Guid ")
        sb.AppendLine("WHERE Cca.Emp=" & oEmp.Id & " ")
        sb.AppendLine("AND Year(Cca.Fch) = " & DtFch.Year & " ")
        sb.AppendLine("AND Cca.Fch <= '" & Format(DtFch, "yyyyMMdd") & "' ")
        sb.AppendLine("GROUP BY Ccb.CtaGuid, PgcCta.Id, PgcCta.Act, PgcCta.ESP, PgcCta.CAT, PgcCta.ENG, PgcCta.Cod")
        sb.AppendLine("HAVING SUM(CASE WHEN Cca.Ccd = 1 THEN (CASE WHEN Ccb.Dh = 1 THEN Ccb.Eur ELSE - Ccb.Eur END) ELSE 0 END) <> 0 OR ")
        sb.AppendLine("SUM(CASE WHEN Ccb.Dh = 1 AND Cca.CCD <> 1 THEN Ccb.Eur ELSE 0 END) <> 0 OR ")
        sb.AppendLine("SUM(CASE WHEN Ccb.Dh = 2 AND Cca.CCD <> 1 THEN Ccb.Eur ELSE 0 END) <> 0 ")
        sb.AppendLine("ORDER BY PgcCta.Id")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOSumasYSaldosItem(oDrd("CtaGuid"))
            With item
                .Id = oDrd("CtaId")
                .Act = oDrd("Act")
                .Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "Esp", "Cat", "Eng")
                .Codi = oDrd("Cod")
                .SdoInicial = SQLHelper.GetDecimalFromDataReader((oDrd("SdoInicial")))
                .Debe = SQLHelper.GetDecimalFromDataReader((oDrd("Debe")))
                .Haber = SQLHelper.GetDecimalFromDataReader((oDrd("Haber")))
                .SdoFinal = SQLHelper.GetDecimalFromDataReader((oDrd("SdoFinal")))
            End With
            retval.items.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oEmp As DTOEmp, DtFch As Date) As DTOSumasYSaldos
        Dim retval As New DTOSumasYSaldos(DtFch)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT PgcCta.Cod, Ccb.CtaGuid, PgcCta.Id AS CtaId, PgcCta.Act, PgcCta.ESP, PgcCta.CAT, PgcCta.ENG ")
        sb.AppendLine(", Ccb.ContactGuid, CliGral.Cli, CliGral.FullNom ")
        sb.AppendLine(", SUM(CASE WHEN Cca.CCD = 1 THEN (CASE WHEN Ccb.Dh = 1 THEN Ccb.Eur ELSE - Ccb.Eur END) ELSE 0 END) AS SdoInicial ")
        sb.AppendLine(", SUM(CASE WHEN Ccb.Dh = 1 AND Cca.CCD <> 1 THEN Ccb.Eur ELSE 0 END) AS Debe ")
        sb.AppendLine(", SUM(CASE WHEN Ccb.Dh = 2 AND Cca.CCD <> 1 THEN Ccb.Eur ELSE 0 END) AS Haber ")
        sb.AppendLine(", SUM(CASE WHEN Ccb.Dh = 1 THEN Ccb.Eur ELSE - Ccb.Eur END) AS SdoFinal ")
        sb.AppendLine("FROM Ccb ")
        sb.AppendLine("INNER JOIN Cca ON Ccb.CcaGuid = Cca.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON Ccb.ContactGuid = CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN PgcCta ON Ccb.CtaGuid = PgcCta.Guid ")
        sb.AppendLine("WHERE Cca.Emp=" & oEmp.Id & " ")
        sb.AppendLine("AND Cca.yea =" & DtFch.Year & " ")
        sb.AppendLine("AND Cca.Fch < '" & Format(DtFch.AddDays(1), "yyyyMMdd") & "' ")
        sb.AppendLine("AND Cca.Ccd<>" & CInt(DTOCca.CcdEnum.TancamentComptes) & " ")
        sb.AppendLine("AND Cca.Ccd<>" & CInt(DTOCca.CcdEnum.TancamentExplotacio) & " ")
        sb.AppendLine("AND Cca.Ccd<>" & CInt(DTOCca.CcdEnum.TancamentBalanç) & " ")
        sb.AppendLine("GROUP BY PgcCta.Cod, Ccb.CtaGuid, PgcCta.Id, PgcCta.Act, PgcCta.ESP, PgcCta.CAT, PgcCta.ENG, Ccb.ContactGuid, CliGral.Cli, CliGral.FullNom ")
        sb.AppendLine("HAVING SUM(CASE WHEN Cca.Ccd = 1 THEN (CASE WHEN Ccb.Dh = 1 THEN Ccb.Eur ELSE - Ccb.Eur END) ELSE 0 END) <> 0 OR ")
        sb.AppendLine("SUM(CASE WHEN Ccb.Dh = 1 AND Cca.CCD <> 1 THEN Ccb.Eur ELSE 0 END) <> 0 OR ")
        sb.AppendLine("SUM(CASE WHEN Ccb.Dh = 2 AND Cca.CCD <> 1 THEN Ccb.Eur ELSE 0 END) <> 0 ")
        sb.AppendLine("ORDER BY PgcCta.Id, CliGral.FullNom")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oContact As DTOContact = Nothing
            If Not IsDBNull(oDrd("ContactGuid")) Then
                oContact = New DTOContact(oDrd("ContactGuid"))
                oContact.Id = SQLHelper.GetIntegerFromDataReader(oDrd("Cli"))
                oContact.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
            End If

            Dim item As New DTOSumasYSaldosItem(oDrd("CtaGuid"))
            With item
                .Id = oDrd("CtaId")
                .Act = oDrd("Act")
                .Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "Esp", "Cat", "Eng")
                .Codi = oDrd("Cod")
                .Contact = oContact
                .SdoInicial = SQLHelper.GetDecimalFromDataReader((oDrd("SdoInicial")))
                .Debe = SQLHelper.GetDecimalFromDataReader((oDrd("Debe")))
                .Haber = SQLHelper.GetDecimalFromDataReader((oDrd("Haber")))
                .SdoFinal = SQLHelper.GetDecimalFromDataReader((oDrd("SdoFinal")))
            End With
            retval.items.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
