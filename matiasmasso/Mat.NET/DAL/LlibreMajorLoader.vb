Public Class LlibreMajorLoader

    Shared Function Excel(oExercici As DTOExercici, oLang As DTOLang) As MatHelper.Excel.Sheet
        Dim sFilename As String = String.Format("{0}.{1} Llibre Major.xlsx", oExercici.Emp.Org.PrimaryNifValue(), oExercici.Year)
        Dim retval As New MatHelper.Excel.Sheet(oExercici.Year, sFilename)
        With retval
            .AddColumn(oLang.Tradueix("cuenta", "compte", "account"), MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn(oLang.Tradueix("descripción", "descripció", "description"), MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn(oLang.Tradueix("registro", "registre", "log"), MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn(oLang.Tradueix("fecha", "data", "Date"), MatHelper.Excel.Cell.NumberFormats.DDMMYY)
            .AddColumn(oLang.Tradueix("concepto", "concepte", "concept"), MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn(oLang.Tradueix("debe", "deure", "debit"), MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn(oLang.Tradueix("haber", "haver", "credit"), MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn(oLang.Tradueix("saldo", "saldo", "balance"), MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn(oLang.Tradueix("documento", "document", "document"), MatHelper.Excel.Cell.NumberFormats.W50)
        End With

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Cca.Cca, Cca.Fch, Cca.Txt, Cca.Hash ")
        sb.AppendLine(", Ccb.Eur, Ccb.Dh, Ccb.CtaGuid, Ccb.ContactGuid ")
        sb.AppendLine(", PgcCta.Id AS CtaId, PgcCta.Esp as CtaNomEsp, PgcCta.Cat as CtaNomCat, PgcCta.Eng as CtaNomEng ")
        sb.AppendLine(", (CASE WHEN Ccb.Dh = PgcCta.Act THEN 0 ELSE 1 END) AS Reverse ")
        sb.AppendLine(", CliGral.Cli, CliGral.RaoSocial ")
        sb.AppendLine("FROM Ccb ")
        sb.AppendLine("INNER JOIN Cca ON Ccb.CcaGuid=Cca.Guid ")
        sb.AppendLine("INNER JOIN PgcCta ON Ccb.CtaGuid=PgcCta.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON Ccb.ContactGuid=CliGral.Guid ")
        sb.AppendLine("WHERE Cca.Emp = " & oExercici.Emp.Id & " ")
        sb.AppendLine("AND Cca.Yea = " & oExercici.Year & " ")
        sb.AppendLine("ORDER BY PgcCta.Id, CliGral.RaoSocial, Cca.Fch, Cca.Cca")

        Dim SQL As String = sb.ToString
        Dim previousId As String = ""
        Dim saldo As Decimal = 0
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read

            Dim iCca = oDrd("Cca")
            Dim dtFch = oDrd("Fch")
            Dim sConcept = oDrd("Txt")
            Dim sHash = SQLHelper.GetStringFromDataReader(oDrd("Hash"))
            Dim url = DTODocFile.downloadUrl(sHash, True)
            Dim sCtaid = oDrd("CtaId")
            Dim sCtaNom = SQLHelper.GetLangTextFromDataReader(oDrd, "CtaNomEsp", "CtaNomCat", "CtaNomEng", "CtaNomEsp").Tradueix(oLang)
            Dim iContactId = SQLHelper.GetIntegerFromDataReader(oDrd("Cli"))
            Dim sContactNom = SQLHelper.GetStringFromDataReader(oDrd("RaoSocial"))
            Dim id = String.Format("{0} {1}", sCtaid, sContactNom)
            Dim oDh As DTOCcb.DhEnum = oDrd("Dh")
            Dim DcEur = oDrd("Eur")

            Dim reverse As Boolean = oDrd("Reverse") = 1
            If id <> previousId Then
                previousId = id
                saldo = 0
            End If
            saldo = IIf(reverse, saldo - DcEur, saldo + DcEur)

            Dim oRow As MatHelper.Excel.Row = retval.AddRow
            oRow.AddCell(DTOPgcCta.FormatAccountId(sCtaid, iContactId))
            oRow.AddCell(DTOPgcCta.FormatAccountDsc(sCtaNom, sContactNom))
            oRow.AddCell(iCca)
            oRow.AddCell(dtFch)
            oRow.AddCell(sConcept)
            oRow.AddCell(IIf(oDh = DTOCcb.DhEnum.debe, DcEur, 0))
            oRow.AddCell(IIf(oDh = DTOCcb.DhEnum.haber, DcEur, 0))
            oRow.AddCell(saldo)
            oRow.AddCell(url)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
