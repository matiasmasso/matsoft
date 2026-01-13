Public Class Model349Loader
    Shared Function Years(oEmp As DTOEmp) As List(Of Integer)
        Dim retval As New List(Of Integer)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Yea ")
        sb.AppendLine("FROM Intrastat ")
        sb.AppendLine("WHERE Emp=" & CInt(oEmp.Id) & " ")
        sb.AppendLine("GROUP BY Yea ")
        sb.AppendLine("ORDER BY Yea DESC ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            retval.Add(oDrd("Yea"))
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oEmp As DTOEmp, Year As Integer) As List(Of DTOModel349)
        Dim retval As New List(Of DTOModel349)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CliGral.Guid, CliGral.RaoSocial, CliGral.NIF, Intrastat.mes ")
        sb.AppendLine(", SUM(CASE WHEN Ccb.DH = 1 THEN Ccb.EUR ELSE - Ccb.EUR END) as Amt ")
        sb.AppendLine("FROM Intrastat ")
        sb.AppendLine("INNER JOIN ImportDtl ON Intrastat.Guid = ImportDtl.Intrastat ")
        sb.AppendLine("INNER JOIN Cca ON ImportDtl.Guid = Cca.Guid ")
        sb.AppendLine("INNER JOIN Ccb ON Cca.Guid = Ccb.CcaGuid ")
        sb.AppendLine("INNER JOIN PgcCta ON Ccb.CtaGuid = PgcCta.Guid ")
        sb.AppendLine("INNER JOIN CliGral ON Ccb.ContactGuid = CliGral.Guid ")
        sb.AppendLine("WHERE Intrastat.Emp=" & CInt(oEmp.Id) & " ")
        sb.AppendLine("AND Intrastat.Yea=" & Year & " ")
        sb.AppendLine("AND PgcCta.Id LIKE '600%' ")
        sb.AppendLine("GROUP BY CliGral.Guid, CliGral.RaoSocial, CliGral.NIF, Intrastat.mes ")
        sb.AppendLine("ORDER BY CliGral.RaoSocial, Intrastat.mes")

        Dim oContact As New DTOContact
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oContact.Guid.Equals(oDrd("Guid")) Then
                oContact = New DTOContact(oDrd("Guid"))
                With oContact
                    .Nom = oDrd("RaoSocial")
                    .Nif = oDrd("Nif")
                End With
            End If

            Dim item As New DTOModel349
            With item
                .Contact = oContact
                .Year = Year
                .Month = oDrd("Mes")
                .Amt = SQLHelper.GetAmtFromDataReader(oDrd("Amt"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
