Public Class LlibreDiariLoader
    Shared Function Headers(oExercici As DTOExercici) As List(Of DTOCca)
        Dim retval As New List(Of DTOCca)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CCa.Guid, Cca.Cca, Cca.Fch, Cca.Txt, Cca.Hash ")
        sb.AppendLine("FROM Cca ")
        sb.AppendLine("WHERE Cca.Emp = " & oExercici.Emp.Id & " ")
        sb.AppendLine("AND Cca.Yea = " & oExercici.Year & " ")
        sb.AppendLine("ORDER BY Cca.Fch, Cca.Cca")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oCca As New DTOCca(oDrd("Guid"))
            With oCca
                .Id = oDrd("Cca")
                .Fch = oDrd("Fch")
                .Concept = oDrd("Txt")
                If Not IsDBNull(oDrd("Hash")) Then
                    .DocFile = New DTODocFile(oDrd("Hash"))
                End If
            End With
            retval.Add(oCca)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oExercici As DTOExercici) As List(Of DTOCca)
        Dim retval As New List(Of DTOCca)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CCa.Guid, Cca.Cca, Cca.Fch, Cca.Txt, Cca.Hash ")
        sb.AppendLine(", Ccb.Eur, Ccb.Dh, Ccb.CtaGuid, Ccb.ContactGuid ")
        sb.AppendLine(", PgcCta.Id AS CtaId, PgcCta.Esp as CtaNomEsp, PgcCta.Cat as CtaNomCat, PgcCta.Eng as CtaNomEng ")
        sb.AppendLine(", CliGral.Cli, CliGral.RaoSocial ")
        sb.AppendLine("FROM Ccb ")
        sb.AppendLine("INNER JOIN Cca ON Ccb.CcaGuid=Cca.Guid ")
        sb.AppendLine("INNER JOIN PgcCta ON Ccb.CtaGuid=PgcCta.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON Ccb.ContactGuid=CliGral.Guid ")
        sb.AppendLine("WHERE Cca.Emp = " & oExercici.Emp.Id & " ")
        sb.AppendLine("AND Cca.Yea = " & oExercici.Year & " ")
        sb.AppendLine("ORDER BY  Cca.Fch , Cca.Cca , Ccb.Lin")

        Dim SQL As String = sb.ToString
        Dim oCca As New DTOCca
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oCca.Guid.Equals(oDrd("Guid")) Then
                oCca = New DTOCca(oDrd("Guid"))
                With oCca
                    .id = oDrd("Cca")
                    .fch = oDrd("Fch")
                    .concept = oDrd("Txt")
                    .docFile = SQLHelper.GetDocFileFromDataReader(oDrd)
                End With
                retval.Add(oCca)
            End If

            Dim oCta As New DTOPgcCta(oDrd("CtaGuid"))
            With oCta
                .id = oDrd("CtaId")
                .nom = SQLHelper.GetLangTextFromDataReader(oDrd, "CtaNomEsp", "CtaNomCat", "CtaNomEng", "CtaNomEsp")
            End With

            Dim oContact As DTOContact = Nothing
            If Not IsDBNull(oDrd("ContactGuid")) Then
                oContact = New DTOContact(oDrd("ContactGuid"))
                oContact.id = oDrd("Cli")
                oContact.nom = oDrd("RaoSocial")
            End If

            Dim oCcb As New DTOCcb
            With oCcb
                .cta = oCta
                .contact = oContact
                .amt = SQLHelper.GetAmtFromDataReader(oDrd("Eur"))
                .dh = oDrd("Dh")
            End With
            oCca.items.Add(oCcb)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Excel(oExercici As DTOExercici, oLang As DTOLang) As MatHelper.Excel.Sheet
        Dim sFilename As String = String.Format("{0}.{1} Llibre Diari.xlsx", oExercici.Emp.Org.PrimaryNifValue(), oExercici.Year)
        Dim retval As New MatHelper.Excel.Sheet(oExercici.Year, sFilename)
        With retval
            .AddColumn(oLang.Tradueix("registro", "registre", "log"), MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn(oLang.Tradueix("fecha", "data", "Date"), MatHelper.Excel.Cell.NumberFormats.DDMMYY)
            .AddColumn(oLang.Tradueix("concepto", "concepte", "concept"), MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn(oLang.Tradueix("cuenta", "compte", "account"), MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn(oLang.Tradueix("descripción", "descripció", "description"), MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn(oLang.Tradueix("debe", "deure", "debit"), MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn(oLang.Tradueix("haber", "haver", "credit"), MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn(oLang.Tradueix("documento", "document", "document"), MatHelper.Excel.Cell.NumberFormats.W50)
        End With

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Cca.Cca, Cca.Fch, Cca.Txt, Cca.Hash ")
        sb.AppendLine(", Ccb.Eur, Ccb.Dh, Ccb.CtaGuid, Ccb.ContactGuid ")
        sb.AppendLine(", PgcCta.Id AS CtaId, PgcCta.Esp as CtaNomEsp, PgcCta.Cat as CtaNomCat, PgcCta.Eng as CtaNomEng ")
        sb.AppendLine(", CliGral.Cli, CliGral.RaoSocial ")
        sb.AppendLine("FROM Ccb ")
        sb.AppendLine("INNER JOIN Cca ON Ccb.CcaGuid=Cca.Guid ")
        sb.AppendLine("INNER JOIN PgcCta ON Ccb.CtaGuid=PgcCta.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON Ccb.ContactGuid=CliGral.Guid ")
        sb.AppendLine("WHERE Cca.Emp = " & oExercici.Emp.Id & " ")
        sb.AppendLine("AND Cca.Yea = " & oExercici.Year & " ")
        sb.AppendLine("ORDER BY  Cca.Fch , Cca.Cca , Ccb.Lin")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read

            Dim iCca = oDrd("Cca")
            Dim dtFch = oDrd("Fch")
            Dim sConcept = oDrd("Txt")
            Dim sHash = SQLHelper.GetStringFromDataReader(oDrd("Hash"))
            Dim url = DTODocFile.DownloadUrl(sHash, True)
            Dim sCtaid = oDrd("CtaId")
            Dim sCtaNom = SQLHelper.GetLangTextFromDataReader(oDrd, "CtaNomEsp", "CtaNomCat", "CtaNomEng", "CtaNomEsp").Tradueix(oLang)
            Dim iContactId = SQLHelper.GetIntegerFromDataReader(oDrd("Cli"))
            Dim sContactNom = SQLHelper.GetStringFromDataReader(oDrd("RaoSocial"))
            Dim oDh As DTOCcb.DhEnum = oDrd("Dh")
            Dim DcEur = oDrd("Eur")

            Dim oRow As MatHelper.Excel.Row = retval.AddRow
            oRow.AddCell(iCca)
            oRow.AddCell(dtFch)
            oRow.AddCell(sConcept)
            oRow.AddCell(DTOPgcCta.FormatAccountId(sCtaid, iContactId))
            oRow.AddCell(DTOPgcCta.FormatAccountDsc(sCtaNom, sContactNom))
            oRow.AddCell(IIf(oDh = DTOCcb.DhEnum.debe, DcEur, 0))
            oRow.AddCell(IIf(oDh = DTOCcb.DhEnum.haber, DcEur, 0))
            oRow.AddCell(url)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
