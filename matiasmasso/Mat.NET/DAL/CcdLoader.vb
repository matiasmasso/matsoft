Public Class CcdLoader

End Class

Public Class CcdsLoader

    Shared Function Descuadres(oExercici As DTOExercici, oCta As DTOPgcCta) As List(Of DTOCcd)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT X.ContactGuid, CliGral.FullNom, SUM(X.Pendent) AS Pendent, SUM(X.Saldo) AS Saldo FROM ( ")
        sb.AppendLine("SELECT ContactGuid, Pnd.Eur AS Pendent, 0 AS Saldo, Pnd.Guid ")
        sb.AppendLine("FROM Pnd ")
        sb.AppendLine("INNER JOIN PgcCta ON Pnd.CtaGuid=PgcCta.Guid AND Pnd.Status<10 ")
        sb.AppendLine("WHERE Pnd.Emp =" & CInt(oExercici.Emp.Id) & " ")
        sb.AppendLine("AND Pnd.CtaGuid = '" & oCta.Guid.ToString & "' ")
        sb.AppendLine("UNION ")
        sb.AppendLine("SELECT ContactGuid, 0 AS Pendent, (CASE WHEN Ccb.Dh=1 THEN Eur ELSE -Eur END) AS Saldo, Ccb.Guid ")
        sb.AppendLine("FROM Ccb ")
        sb.AppendLine("INNER JOIN Cca ON Ccb.CcaGuid=Cca.Guid AND Cca.Yea=Year(GETDATE()) AND Cca.Emp =" & CInt(oExercici.Emp.Id) & " ")
        sb.AppendLine("INNER JOIN PgcCta ON Ccb.CtaGuid=PgcCta.Guid  ")
        sb.AppendLine("WHERE Ccb.CtaGuid = '" & oCta.Guid.ToString & "' ")
        sb.AppendLine(") X  ")
        sb.AppendLine("INNER JOIN CliGral ON X.ContactGuid = CliGral.Guid ")
        sb.AppendLine("GROUP BY CliGral.FullNom, X.ContactGuid ")
        sb.AppendLine("HAVING SUM(X.Pendent) <> SUM(X.Saldo)")

        Dim retval As New List(Of DTOCcd)
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oContact As New DTOContact(oDrd("ContactGuid"))
            With oContact
                .FullNom = oDrd("FullNom")
                .Emp = oExercici.Emp
            End With
            Dim item As New DTOCcd
            With item
                .Exercici = oExercici
                .Contact = oContact
                .Cta = oCta
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function



End Class
