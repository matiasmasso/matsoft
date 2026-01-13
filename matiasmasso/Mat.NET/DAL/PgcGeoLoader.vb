Public Class PgcGeosLoader
    Shared Function FromExercici(oExercici As DTOExercici) As List(Of DTOPgcGeo) 'DEPRECATED PgcGrup, PgcCta.PgcPlan
        Dim retval As New List(Of DTOPgcGeo)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT PgcCta.Guid, PgcCta.Id AS CtaId, PgcCta.Cat AS CtaNom ")
        sb.AppendLine(",SUM(CASE WHEN DH=PgcCta.Act THEN Ccb.Eur ELSE -Ccb.Eur END) AS TOT ")
        sb.AppendLine(",SUM(CASE WHEN VwAddress.CEE = 1 AND DH=PgcCta.Act THEN Ccb.Eur WHEN VwAddress.CEE = 1 AND Ccb.DH=PgcCta.Act THEN -Ccb.Eur ELSE 0 END) AS CEE ")
        sb.AppendLine(",SUM(CASE WHEN VwAddress.CountryISO = 'ES' AND Ccb.DH=PgcCta.Act THEN Ccb.Eur WHEN VwAddress.CountryISO = 'ES' AND Ccb.DH=PgcCta.Act THEN -Ccb.Eur ELSE 0 END) AS ESP ")
        sb.AppendLine(",SUM(CASE WHEN VwAddress.RegioNom = 'CATALUNYA' AND Ccb.DH=PgcCta.Act THEN Ccb.Eur WHEN VwAddress.RegioNom = 'CATALUNYA' AND Ccb.DH=PgcCta.Act THEN -Ccb.Eur ELSE 0 END) AS CCAA  ")
        sb.AppendLine("FROM Ccb ")
        sb.AppendLine("INNER JOIN PgcCta ON Ccb.CtaGuid=PgcCta.Guid ")
        sb.AppendLine("INNER JOIN Cca ON Ccb.CcaGuid=Cca.Guid ")
        sb.AppendLine("INNER JOIN VwAddress ON CCB.ContactGuid = VwAddress.SrcGuid ")
        sb.AppendLine("WHERE CCA.Emp = " & oExercici.Emp.Id & " ")
        sb.AppendLine("AND YEAR(Cca.Fch) = " & oExercici.Year & " ")
        sb.AppendLine("AND CCA.CCD < " & CInt(DTOCca.CcdEnum.TancamentComptes) & " ")
        sb.AppendLine("GROUP BY PgcCta.Guid, PgcCta.Id, PgcCta.Cat ")
        sb.AppendLine("ORDER BY PgcCta.Id")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oPgcCta As New DTOPgcCta
            Dim item As New DTOPgcGeo
            With item
                .Guid = oDrd("Guid")
                .CtaId = oDrd("CtaId")
                .CtaNom = SQLHelper.GetStringFromDataReader(oDrd("CtaNom"))
                .Tot = SQLHelper.GetDecimalFromDataReader(oDrd("Tot"))
                .CEE = SQLHelper.GetDecimalFromDataReader(oDrd("CEE"))
                .Esp = SQLHelper.GetDecimalFromDataReader(oDrd("Esp"))
                .CCAA = SQLHelper.GetDecimalFromDataReader(oDrd("CCAA"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
