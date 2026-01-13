Public Class CcbLoader

End Class

Public Class CcbsLoader

    Shared Function All(oContact As DTOContact, oExercici As DTOExercici, oCta As DTOPgcCta, Optional FchTo As Date = Nothing) As List(Of DTOCcb)
        Dim retval As New List(Of DTOCcb)
        Dim oTrimmedExercici = oExercici.Trimmed
        Dim oTrimmedContact As DTOContact = Nothing
        If oContact IsNot Nothing Then oTrimmedContact = oContact.Trimmed()

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Cca.Cca, Cca.Fch, Cca.Txt, Cca.Ccd, Cca.Hash ")
        sb.AppendLine(", Ccb.CcaGuid, Ccb.Eur, Ccb.Cur, Ccb.Pts, Ccb.Dh ")
        sb.AppendLine("FROM Ccb ")
        sb.AppendLine("INNER JOIN Cca ON Ccb.CcaGuid = Cca.Guid ")
        sb.AppendLine("WHERE Cca.Emp = " & oExercici.Emp.Id & " ")
        sb.AppendLine("AND Year(Cca.Fch) = " & oExercici.Year & " ")
        sb.AppendLine("AND Ccb.CtaGuid = '" & oCta.Guid.ToString & "' ")
        If oContact Is Nothing Then
            sb.AppendLine("AND Ccb.contactGuid IS NULL ")
        Else
            If oContact.Guid.Equals(Guid.Empty) Then
                sb.AppendLine("AND Ccb.contactGuid IS NULL ")
            Else
                sb.AppendLine("AND Ccb.contactGuid = '" & oContact.Guid.ToString & "' ")
            End If
        End If
        If FchTo <> Nothing Then
            sb.AppendLine("AND Cca.Fch<'" & Format(FchTo, "yyyyMMdd") & "' ")
        End If
        sb.AppendLine("ORDER BY Cca.fch, Cca.ccd, Cca.cdn, Cca.Txt ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oCca As New DTOCca(oDrd("CcaGuid"))
            With oCca
                .Id = oDrd("Cca")
                .Fch = oDrd("Fch")
                .Concept = oDrd("Txt")
                .Ccd = SQLHelper.GetIntegerFromDataReader(oDrd("Ccd"))
                If Not IsDBNull(oDrd("Hash")) Then
                    .DocFile = New DTODocFile
                    .DocFile.Hash = oDrd("Hash")
                End If
                .Exercici = oTrimmedExercici
            End With
            Dim item As New DTOCcb
            With item
                .Cca = oCca
                .Cta = oCta
                .Contact = oTrimmedContact
                .Dh = oDrd("Dh")
                .Amt = DTOAmt.Factory(CDec(oDrd("Eur")), oDrd("Cur").ToString, CDec(oDrd("Pts")))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function LlibreMajor(oExercici As DTOExercici, Optional ShowProgress As ProgressBarHandler = Nothing) As List(Of DTOCcb)
        Dim CancelRequest As Boolean
        If ShowProgress IsNot Nothing Then
            ShowProgress(0, 1000, 0, "carregant comptes de la base de dades", CancelRequest)
        End If

        Dim retval As New List(Of DTOCcb)
        Dim iYear As Integer
        If oExercici IsNot Nothing Then iYear = oExercici.Year
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Cca.Cca, Cca.Fch, Cca.Txt, Cca.Ccd, Cca.Hash ")
        sb.AppendLine(", Ccb.CcaGuid, Ccb.Guid, Ccb.Eur, Ccb.Cur, Ccb.Pts, Ccb.Dh ")
        sb.AppendLine(", Ccb.CtaGuid, PgcCta.Guid, PgcCta.Id AS CtaId, PgcCta.Esp, PgcCta.Cat, PgcCta.Eng, PgcCta.Act ")
        sb.AppendLine(", Ccb.ContactGuid, CliGral.RaoSocial, CliGral.Cli ")
        sb.AppendLine("FROM Ccb ")
        sb.AppendLine("INNER JOIN PgcCta ON Ccb.CtaGuid=PgcCta.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON Ccb.ContactGuid = CliGral.Guid ")
        sb.AppendLine("INNER JOIN Cca ON Ccb.CcaGuid = Cca.Guid ")
        sb.AppendLine("WHERE Cca.Emp = " & oExercici.Emp.Id & " ")
        sb.AppendLine("AND Cca.Yea = " & oExercici.Year & " ")
        sb.AppendLine("ORDER BY PgcCta.Id, CliGral.RaoSocial, Cca.Fch, Cca.ccd, cca.cdn ")
        Dim SQL As String = sb.ToString

        Dim oCta As New DTOPgcCta
        Dim oContact As New DTOContact

        Dim exs As New List(Of Exception)
        Dim oDs As DataSet = SQLHelper.GetDataset(SQL, exs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each oRow As DataRow In oTb.Rows
            If Not oCta.Guid.Equals(oRow("CtaGuid")) Then
                oCta = New DTOPgcCta(oRow("CtaGuid"))
                With oCta
                    .Id = oRow("CtaId")
                    .Nom = SQLHelper.GetLangTextFromDataRow(oRow, "Esp", "Cat", "Eng", "Esp")
                    .Act = oRow("Act")
                End With
            End If

            If IsDBNull(oRow("ContactGuid")) Then
                oContact = Nothing
            Else
                If oContact Is Nothing OrElse Not oContact.Guid.Equals(oRow("ContactGuid")) Then
                    oContact = New DTOContact(oRow("ContactGuid"))
                    With oContact
                        .Id = oRow("Cli")
                        .Nom = oRow("RaoSocial")
                    End With
                End If
            End If

            Dim oCca As New DTOCca(oRow("CcaGuid"))
            With oCca
                .Id = oRow("Cca")
                .Fch = oRow("Fch")
                .Concept = oRow("Txt")
                .Ccd = SQLHelper.GetIntegerFromDataReader(oRow("Ccd"))
                If Not IsDBNull(oRow("Hash")) Then
                    .DocFile = New DTODocFile
                    .DocFile.Hash = oRow("Hash")
                End If
                .Exercici = oExercici
            End With

            Dim item As New DTOCcb
            With item
                .Cca = oCca
                .Cta = oCta
                .Contact = oContact
                .Dh = oRow("Dh")
                .Amt = DTOAmt.Factory(CDec(oRow("Eur")), oRow("Cur").ToString, CDec(oRow("Pts")))
            End With
            retval.Add(item)

            If ShowProgress IsNot Nothing Then
                ShowProgress(0, oTb.Rows.Count, retval.Count, "carregant comptes de la base de dades", CancelRequest)
            End If

            If CancelRequest Then Exit For
        Next

        Return retval
    End Function


    Shared Function All(oExercici As DTOExercici, oCta As DTOPgcCta) As List(Of DTOCcb)
        Dim retval As New List(Of DTOCcb)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Ccb.Guid, Ccb.CcaGuid, Cca.Cca, Cca.Fch, Cca.Txt, Cca.Ccd, Cca.Hash ")
        sb.AppendLine(", Ccb.ContactGuid, CliGral.FullNom, CliGral.RaoSocial ")
        sb.AppendLine(", CliGral.NIF, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod ")
        sb.AppendLine(", Ccb.Dh, Ccb.Eur ")
        sb.AppendLine("FROM Cca ")
        sb.AppendLine("INNER JOIN Ccb ON Cca.Guid = Ccb.CcaGuid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON Ccb.ContactGuid = CliGral.Guid ")
        sb.AppendLine("WHERE Cca.Emp=" & oExercici.Emp.Id & " ")
        sb.AppendLine("AND Cca.Yea= " & oExercici.Year & " ")
        sb.AppendLine("AND Ccb.CtaGuid='" & oCta.Guid.ToString() & "' ")
        sb.AppendLine("ORDER BY Cca.Cca, Ccb.Lin")
        Dim SQL As String = sb.ToString
        Dim oCca As New DTOCca
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oCca.Guid.Equals(oDrd("CcaGuid")) Then
                oCca = New DTOCca(oDrd("CcaGuid"))
                With oCca
                    .Exercici = oExercici
                    .Id = oDrd("Cca")
                    .Fch = oDrd("Fch")
                    .Concept = oDrd("Txt")
                    .Ccd = SQLHelper.GetIntegerFromDataReader(oDrd("Ccd"))
                    If Not IsDBNull(oDrd("Hash")) Then
                        .DocFile = New DTODocFile(oDrd("Hash"))
                    End If
                End With
            End If
            Dim oContact As DTOContact = Nothing
            If Not IsDBNull(oDrd("ContactGuid")) Then
                oContact = New DTOContact(oDrd("ContactGuid"))
                With oContact
                    .FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                    .Nom = SQLHelper.GetStringFromDataReader(oDrd("RaoSocial"))
                    .Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
                End With
            End If
            Dim oCcb As New DTOCcb(oDrd("Guid"))
            With oCcb
                .Cca = oCca
                .Cta = oCta
                .Contact = oContact
                .Dh = oDrd("Dh")
                .Amt = DTOAmt.Factory(CDec(oDrd("Eur")))
            End With
            oCca.Items.Add(oCcb)
            retval.Add(oCcb)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oEmp As DTOEmp, oYearMonth As DTOYearMonth) As List(Of DTOCcb)
        Dim retval As New List(Of DTOCcb)
        Dim oExercici As New DTOExercici(oEmp, oYearMonth.Year)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Ccb.Guid, Ccb.CcaGuid, Cca.Cca, Cca.Fch, Cca.Txt, Cca.Ccd ")
        sb.AppendLine(", Ccb.CtaGuid, PgcCta.Id AS CtaId, PgcCta.Cod AS CtaCodi, PgcCta.Esp, PgcCta.Cat, PgcCta.Eng ")
        sb.AppendLine(", Ccb.ContactGuid, CliGral.FullNom, CliGral.RaoSocial ")
        sb.AppendLine(", CliGral.NIF, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod ")
        sb.AppendLine(", Ccb.Dh, Ccb.Eur ")
        sb.AppendLine("FROM Cca ")
        sb.AppendLine("INNER JOIN Ccb ON Cca.Guid = Ccb.CcaGuid ")
        sb.AppendLine("INNER JOIN PgcCta ON Ccb.CtaGuid = PgcCta.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON Ccb.ContactGuid = CliGral.Guid ")
        sb.AppendLine("WHERE Cca.Emp=" & oEmp.Id & " ")
        sb.AppendLine("AND Cca.Yea=" & oYearMonth.Year & " ")
        sb.AppendLine("AND Month(Cca.Fch)=" & oYearMonth.Month & " ")
        sb.AppendLine("ORDER BY Cca.Cca, Ccb.Lin")
        Dim SQL As String = sb.ToString
        Dim oCca As New DTOCca
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oCca.Guid.Equals(oDrd("CcaGuid")) Then
                oCca = New DTOCca(oDrd("CcaGuid"))
                With oCca
                    .Exercici = oExercici
                    .Id = oDrd("Cca")
                    .Fch = oDrd("Fch")
                    .Concept = oDrd("Txt")
                    .Ccd = SQLHelper.GetIntegerFromDataReader(oDrd("Ccd"))
                End With
            End If
            Dim oCta As New DTOPgcCta(oDrd("CtaGuid"))
            With oCta
                .Id = oDrd("CtaId")
                .Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "Esp", "Cat", "Eng")
                .Codi = oDrd("CtaCodi")
            End With
            Dim oContact As DTOContact = Nothing
            If Not IsDBNull(oDrd("ContactGuid")) Then
                oContact = New DTOContact(oDrd("ContactGuid"))
                With oContact
                    .FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                    .Nom = SQLHelper.GetStringFromDataReader(oDrd("RaoSocial"))
                    .Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
                End With
            End If
            Dim oCcb As New DTOCcb(oDrd("Guid"))
            With oCcb
                .Cca = oCca
                .Cta = oCta
                .Contact = oContact
                .Dh = oDrd("Dh")
                .Amt = DTOAmt.Factory(CDec(oDrd("Eur")))
            End With
            oCca.Items.Add(oCcb)
            retval.Add(oCcb)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class
