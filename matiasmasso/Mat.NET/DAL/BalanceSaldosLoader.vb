Public Class BalanceSaldosLoader

    Shared Function SumasYSaldos(oExercici As DTOExercici, Optional FchTo As Date = Nothing) As List(Of DTOBalanceSaldo)
        Dim retval As New List(Of DTOBalanceSaldo)
        If FchTo = Nothing Then FchTo = DTO.GlobalVariables.Today()
        Dim FchFrom As New Date(FchTo.Year, 1, 1)
        Dim sCurrentFrom As String = Format(FchFrom, "yyyyMMdd")
        Dim sCurrentTo As String = Format(FchTo, "yyyyMMdd")
        Dim sPreviousFrom As String = Format(FchFrom.AddYears(-1), "yyyyMMdd")
        Dim sPreviousTo As String = Format(FchTo.AddYears(-1), "yyyyMMdd")

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Ccb.CtaGuid, PgcCta.Id, PgcCta.PgcClass, PgcCta.Esp, PgcCta.Cat, PgcCta.Eng ")
        sb.AppendLine(", SUM(CASE WHEN (Cca.Fch BETWEEN '" & sCurrentFrom & "' AND '" & sCurrentTo & "') AND Ccb.Dh=1 THEN Ccb.Eur ELSE 0 END) As CurrentDeb ")
        sb.AppendLine(", SUM(CASE WHEN (Cca.Fch BETWEEN '" & sCurrentFrom & "' AND '" & sCurrentTo & "') AND Ccb.Dh=2 THEN Ccb.Eur ELSE 0 END) As CurrentHab ")
        sb.AppendLine(", SUM(CASE WHEN (Cca.Fch BETWEEN '" & sPreviousFrom & "' AND '" & sPreviousTo & "') AND Ccb.Dh=1 THEN Ccb.Eur ELSE 0 END) AS PreviousDeb ")
        sb.AppendLine(", SUM(CASE WHEN (Cca.Fch BETWEEN '" & sPreviousFrom & "' AND '" & sPreviousTo & "') AND Ccb.Dh=2 THEN Ccb.Eur ELSE 0 END) AS PreviousHab ")
        sb.AppendLine("FROM Ccb ")
        sb.AppendLine("INNER JOIN PgcCta ON Ccb.CtaGuid = PgcCta.Guid ")
        sb.AppendLine("INNER JOIN Cca ON Ccb.CcaGuid = Cca.Guid ")
        sb.AppendLine("WHERE Cca.Emp=" & CInt(oExercici.Emp.Id) & " ")
        sb.AppendLine("AND Cca.ccd<>" & CInt(DTOCca.CcdEnum.TancamentComptes) & " ")
        sb.AppendLine("AND Cca.ccd<>" & CInt(DTOCca.CcdEnum.TancamentExplotacio) & " ")
        sb.AppendLine("AND Cca.ccd<>" & CInt(DTOCca.CcdEnum.TancamentBalanç) & " ")
        sb.AppendLine("GROUP BY Ccb.CtaGuid, PgcCta.Id, PgcCta.PgcClass, PgcCta.Esp, PgcCta.Cat, PgcCta.Eng ")
        sb.AppendLine("HAVING SUM(CASE WHEN Cca.Fch BETWEEN '" & sCurrentFrom & "' AND '" & sCurrentTo & "' THEN (CASE WHEN Ccb.Dh=1 THEN Ccb.Eur ELSE -Ccb.Eur END) END)<>0 OR SUM(CASE WHEN Cca.Fch BETWEEN '" & sPreviousFrom & "' AND '" & sPreviousTo & "' THEN (CASE WHEN Ccb.Dh=1 THEN Ccb.Eur ELSE -Ccb.Eur END) END)<>0 ")
        sb.AppendLine("ORDER BY PgcCta.Id")

        Dim item As DTOBalanceSaldo
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oGuid As Guid = oDrd("CtaGuid")
            item = New DTOBalanceSaldo(oGuid)
            With item
                .Id = oDrd("Id")

                If Not IsDBNull(oDrd("PgcClass")) Then
                    .PgcClass = New DTOPgcClass(oDrd("PgcClass"))
                    .Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "Esp", "Cat", "Eng")
                End If

                .CurrentDeb = SQLHelper.GetDecimalFromDataReader(oDrd("CurrentDeb"))
                .CurrentHab = SQLHelper.GetDecimalFromDataReader(oDrd("CurrentHab"))
                .PreviousDeb = SQLHelper.GetDecimalFromDataReader(oDrd("PreviousDeb"))
                .PreviousHab = SQLHelper.GetDecimalFromDataReader(oDrd("PreviousHab"))
            End With
            retval.Add(item)

        Loop
        oDrd.Close()
        Return retval
    End Function




    Shared Function All(oExercici As DTOExercici, oCta As DTOPgcCta, FchTo As Date, ShowProgress As ProgressBarHandler) As List(Of DTOBalanceSaldo)
        Dim CancelRequest As Boolean
        If ShowProgress IsNot Nothing Then
            ShowProgress(0, 1000, 0, "carregant dades", CancelRequest)
        End If

        Dim FchFrom As New Date(FchTo.Year, 1, 1)
        Dim sCurrentFrom As String = Format(FchFrom, "yyyyMMdd")
        Dim sCurrentTo As String = Format(FchTo, "yyyyMMdd")
        Dim sPreviousFrom As String = Format(FchFrom.AddYears(-1), "yyyyMMdd")
        Dim sPreviousTo As String = Format(FchTo.AddYears(-1), "yyyyMMdd")

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Ccb.CtaGuid, PgcCta.Id, PgcCta.Esp, PgcCta.Cat, PgcCta.Eng ")
        sb.AppendLine(", Ccb.ContactGuid, CliGral.Cli, CliGral.RaoSocial ")
        sb.AppendLine(", SUM(CASE WHEN (Cca.Fch BETWEEN '" & sCurrentFrom & "' AND '" & sCurrentTo & "') AND Ccb.Dh=1 THEN Ccb.Eur ELSE 0 END) As CurrentDeb ")
        sb.AppendLine(", SUM(CASE WHEN (Cca.Fch BETWEEN '" & sCurrentFrom & "' AND '" & sCurrentTo & "') AND Ccb.Dh=2 THEN Ccb.Eur ELSE 0 END) As CurrentHab ")
        sb.AppendLine(", SUM(CASE WHEN (Cca.Fch BETWEEN '" & sPreviousFrom & "' AND '" & sPreviousTo & "') AND Ccb.Dh=1 THEN Ccb.Eur ELSE 0 END) AS PreviousDeb ")
        sb.AppendLine(", SUM(CASE WHEN (Cca.Fch BETWEEN '" & sPreviousFrom & "' AND '" & sPreviousTo & "') AND Ccb.Dh=2 THEN Ccb.Eur ELSE 0 END) AS PreviousHab ")
        sb.AppendLine("FROM Ccb ")
        sb.AppendLine("INNER JOIN Cca ON Ccb.CcaGuid = Cca.Guid ")
        sb.AppendLine("INNER JOIN PgcCta ON Ccb.CtaGuid=PgcCta.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON Ccb.ContactGuid = CliGral.Guid ")
        sb.AppendLine("WHERE Cca.Emp=" & CInt(oExercici.Emp.Id) & " ")
        sb.AppendLine("AND Cca.ccd<>" & CInt(DTOCca.CcdEnum.TancamentComptes) & " ")
        sb.AppendLine("AND Cca.ccd<>" & CInt(DTOCca.CcdEnum.TancamentExplotacio) & " ")
        sb.AppendLine("AND Cca.ccd<>" & CInt(DTOCca.CcdEnum.TancamentBalanç) & " ")

        If oCta IsNot Nothing Then
            sb.AppendLine("AND Ccb.CtaGuid ='" & oCta.Guid.ToString & "' ")
        End If

        sb.AppendLine("GROUP BY Ccb.CtaGuid, PgcCta.Id, PgcCta.Esp, PgcCta.Cat, PgcCta.Eng ")
        sb.AppendLine(", Ccb.ContactGuid, CliGral.Cli, CliGral.RaoSocial ")
        sb.AppendLine("HAVING SUM(CASE WHEN Cca.Fch BETWEEN '" & sCurrentFrom & "' AND '" & sCurrentTo & "' THEN (CASE WHEN Ccb.Dh=1 THEN Ccb.Eur ELSE -Ccb.Eur END) END)<>0 OR SUM(CASE WHEN Cca.Fch BETWEEN '" & sPreviousFrom & "' AND '" & sPreviousTo & "' THEN (CASE WHEN Ccb.Dh=1 THEN Ccb.Eur ELSE -Ccb.Eur END) END)<>0 ")

        sb.AppendLine("ORDER BY PgcCta.Id ")
        sb.AppendLine(", CliGral.RaoSocial ")

        Dim retval As New List(Of DTOBalanceSaldo)
        Dim exs As New List(Of Exception)
        Dim SQL As String = sb.ToString
        Dim oDs As DataSet = SQLHelper.GetDataset(SQL, exs)
        Dim oRows As DataRowCollection = oDs.Tables(0).Rows
        For Each oRow As DataRow In oRows
            Dim item As New DTOBalanceSaldo()
            With item
                .Id = oRow("Id")
                .Nom = SQLHelper.GetLangTextFromDataRow(oRow, "Esp", "Cat", "Eng", "Esp")

                If Not IsDBNull(oRow("ContactGuid")) Then
                    .Contact = New DTOContact(oRow("ContactGuid"))
                    .Contact.Id = oRow("Cli")
                    .Contact.Nom = oRow("RaoSocial")
                End If

                .CurrentDeb = SQLHelper.GetDecimalFromDataReader(oRow("CurrentDeb"))
                .CurrentHab = SQLHelper.GetDecimalFromDataReader(oRow("CurrentHab"))
                .PreviousDeb = SQLHelper.GetDecimalFromDataReader(oRow("PreviousDeb"))
                .PreviousHab = SQLHelper.GetDecimalFromDataReader(oRow("PreviousHab"))
            End With
            retval.Add(item)

            If ShowProgress IsNot Nothing Then
                ShowProgress(0, oRows.Count, retval.Count, "carregant dades", CancelRequest)
            End If
            If CancelRequest Then Exit For
        Next
        Return retval
    End Function
End Class
