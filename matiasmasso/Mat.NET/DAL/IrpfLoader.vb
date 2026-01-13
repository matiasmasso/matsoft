Public Class IrpfLoader

    Shared Sub LoadItems(oIrpf As DTOIrpf)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Ccb.Guid, Ccb.CtaGuid, PgcCta.cod AS CtaCod, PgcCta.Id AS CtaId, PgcCta.Esp AS CtaEsp, PgcCta.Cat AS CtaCat, PgcCta.Eng AS CtaEng ")
        sb.AppendLine(", Ccb.ContactGuid, CliGral.RaoSocial, Ccb.CcaGuid, Cca.Cca, Cca.Fch, Cca.Txt ")
        sb.AppendLine(", CliGral.NIF, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod ")
        sb.AppendLine(", (CASE WHEN BookFras.BaseIrpf IS NULL THEN Nomina.IrpfBase ELSE BookFras.BaseIrpf END) AS Base, Ccb.Eur AS Quota, Ccb.Dh ")
        sb.AppendLine("FROM Ccb ")
        sb.AppendLine("INNER JOIN Cca ON Ccb.CcaGuid = Cca.Guid ")
        sb.AppendLine("INNER JOIN PgcCta ON Ccb.CtaGuid = PgcCta.Guid ")
        sb.AppendLine("INNER JOIN CliGral ON Ccb.ContactGuid = CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN BookFras ON Cca.Guid = BookFras.CcaGuid ")
        sb.AppendLine("LEFT OUTER JOIN Nomina ON Cca.Guid = Nomina.CcaGuid ")
        sb.AppendLine("WHERE PgcCta.IsQuotaIrpf = 1 AND Cca.Emp =" & oIrpf.emp.Id & " AND Year(Cca.Fch)=" & oIrpf.yearMonth.year & " AND Month(Cca.Fch)=" & oIrpf.yearMonth.month() & " AND (CASE WHEN BookFras.BaseIrpf IS NULL THEN Nomina.IrpfBase ELSE BookFras.BaseIrpf END) IS NOT NULL ")
        sb.AppendLine("ORDER BY PgcCta.Id, CliGral.RaoSocial, Ccb.ContactGuid ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oPgcCta As New DTOPgcCta
        Dim oContact As New DTOContact
        Do While oDrd.Read
            If Not oPgcCta.Guid.Equals(oDrd("CtaGuid")) Then
                oPgcCta = New DTOPgcCta(oDrd("CtaGuid"))
                With oPgcCta
                    .id = oDrd("CtaId")
                    .cod = oDrd("CtaCod")
                    .nom = SQLHelper.GetLangTextFromDataReader(oDrd, "CtaEsp", "CtaCat", "CtaEng")
                End With
                oContact = New DTOContact
            End If

            If Not oContact.Guid.Equals(oDrd("ContactGuid")) Then
                oContact = New DTOContact(oDrd("ContactGuid"))
                With oContact
                    .nom = oDrd("RaoSocial")
                    .Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
                End With
            End If

            Dim oCca As New DTOCca(oDrd("CcaGuid"))
            With oCca
                .id = oDrd("Cca")
                .concept = oDrd("Txt")
                .fch = oDrd("Fch")
            End With
            Dim oCcb As New DTOCcb(oDrd("Guid"))
            With oCcb
                .cca = oCca
                .cta = oPgcCta
                .contact = oContact
                .dh = oDrd("Dh")
                .amt = SQLHelper.GetAmtFromDataReader(oDrd("Quota"))
            End With
            Dim oItem As New DTOIrpf.Item()
            With oItem
                .Ccb = oCcb
                .Base = SQLHelper.GetAmtFromDataReader(oDrd("Base"))
            End With
            oIrpf.items.Add(oItem)
        Loop
        oDrd.Close()

    End Sub


    Shared Sub LoadSaldos(oIrpf As DTOIrpf)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Ccb.CtaGuid, Ccb.ContactGuid ")
        sb.AppendLine(", PgcCta.Id AS CtaId, PgcCta.Esp AS CtaNom, CliGral.RaoSocial ")
        sb.AppendLine(", SUM(CASE WHEN DH=1 THEN Ccb.Eur ELSE 0 END) AS Deb ")
        sb.AppendLine(", SUM(CASE WHEN DH=2 THEN Ccb.Eur ELSE 0 END) AS Hab ")
        sb.AppendLine("FROM Ccb ")
        sb.AppendLine("INNER JOIN Cca ON Ccb.CcaGuid = Cca.Guid ")
        sb.AppendLine("INNER JOIN PgcCta ON Ccb.CtaGuid = PgcCta.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON Ccb.ContactGuid = CliGral.Guid ")
        sb.AppendLine("WHERE Cca.Emp = " & oIrpf.emp.Id & " AND YEAR(Cca.Fch)= " & oIrpf.yearMonth.year & " AND MONTH(Cca.Fch)<=" & oIrpf.yearMonth.month & " AND PgcCta.isQuotaIrpf = 1 ")
        sb.AppendLine("GROUP BY Ccb.CtaGuid, Ccb.ContactGuid, PgcCta.Id, PgcCta.Esp, CliGral.RaoSocial ")
        sb.AppendLine("ORDER BY PgcCta.Id, PgcCta.Esp, CliGral.RaoSocial ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(Sql)
        Dim oPgcCta As New DTOPgcCta
        Dim oContact As New DTOContact
        Do While oDrd.Read
            Dim oSaldo As New DTOPgcSaldo
            With oSaldo
                .Epg = New DTOPgcEpgBase(oDrd("CtaGuid"))
                .Epg.nom = New DTOLangText(String.Format("{0} {1}", oDrd("CtaId"), oDrd("CtaNom")))
                If Not IsDBNull(oDrd("ContactGuid")) Then
                    .Contact = New DTOContact(oDrd("ContactGuid"))
                    .Contact.nom = oDrd("RaoSocial")
                End If
                .Debe = SQLHelper.GetAmtFromDataReader(oDrd("Deb"))
                .Haber = SQLHelper.GetAmtFromDataReader(oDrd("Hab"))
            End With
            oIrpf.saldos.Add(oSaldo)
        Loop
        oDrd.Close()
    End Sub
End Class
