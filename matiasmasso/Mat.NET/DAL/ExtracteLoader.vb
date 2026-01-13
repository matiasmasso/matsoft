Public Class ExtracteLoader

    Shared Function Years(oEmp As DTOEmp, Optional oContact As DTOContact = Nothing, Optional oCta As DTOPgcCta = Nothing) As List(Of Integer)
        Dim retval As New List(Of Integer)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Cca.Yea ")
        sb.AppendLine("FROM Ccb ")
        sb.AppendLine("INNER JOIN Cca ON Ccb.CcaGuid = Cca.Guid ")
        sb.AppendLine("WHERE Cca.Emp = " & oEmp.Id & " ")
        If oContact IsNot Nothing Then
            sb.AppendLine("AND Ccb.ContactGuid = '" & oContact.Guid.ToString & "' ")
        End If
        If oCta IsNot Nothing Then
            sb.AppendLine("AND Ccb.CtaGuid = '" & oCta.Guid.ToString & "' ")
        End If
        sb.AppendLine("GROUP BY Cca.Yea ")
        sb.AppendLine("ORDER BY Cca.Yea DESC ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim itm As Integer = oDrd("Yea")
            retval.Add(itm)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Ctas(oEmp As DTOEmp, Year As Integer, Optional oContact As DTOContact = Nothing) As List(Of DTOPgcCta)
        Dim retval As New List(Of DTOPgcCta)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Ccb.CtaGuid, PgcCta.Id, PgcCta.act, PgcCta.Esp, PgcCta.Cat, PgcCta.Eng ")
        sb.AppendLine("FROM Ccb ")
        sb.AppendLine("INNER JOIN Cca ON Ccb.CcaGuid = Cca.Guid ")
        sb.AppendLine("INNER JOIN PgcCta ON Ccb.CtaGuid = PgcCta.Guid ")
        sb.AppendLine("WHERE Cca.Emp = " & oEmp.Id & " ")
        sb.AppendLine("AND Cca.Yea = " & Year & " ")
        If oContact Is Nothing Then
            sb.AppendLine("AND Ccb.ContactGuid IS NULL ")
        Else
            sb.AppendLine("AND Ccb.ContactGuid = '" & oContact.Guid.ToString & "' ")
        End If
        sb.AppendLine("GROUP BY Ccb.CtaGuid, PgcCta.Id, PgcCta.act, PgcCta.Esp, PgcCta.Cat, PgcCta.Eng ")
        sb.AppendLine("ORDER BY PgcCta.Id ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim itm As New DTOPgcCta(oDrd("CtaGuid"))
            With itm
                .Id = oDrd("Id")
                .Act = oDrd("Act")
                .Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "Esp", "Cat", "Eng")
            End With
            retval.Add(itm)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Ccbs(oExtracte As DTOExtracte) As List(Of DTOCcb)
        Dim retval As New List(Of DTOCcb)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Cca.Guid, Cca.Cca, Cca.txt, Cca.fch, Cca.ccd, Cca.cdn, Cca.Hash ")
        sb.AppendLine(", Ccb.ContactGuid, Ccb.Pts, Ccb.Cur, Ccb.Eur, Ccb.dh, Ccb.PndGuid  ")
        sb.AppendLine("FROM Ccb ")
        sb.AppendLine("INNER JOIN Cca ON Ccb.CcaGuid = Cca.Guid ")
        sb.AppendLine("WHERE Cca.Emp = " & CInt(oExtracte.Exercici.Emp.Id) & " ")
        sb.AppendLine("AND Cca.Yea = " & oExtracte.Exercici.Year & " ")
        sb.AppendLine("AND Ccb.CtaGuid = '" & oExtracte.Cta.Guid.ToString & "' ")
        If oExtracte.Contact Is Nothing Then
            sb.AppendLine("AND Ccb.ContactGuid IS NULL ")
        Else
            sb.AppendLine("AND Ccb.ContactGuid = '" & oExtracte.Contact.Guid.ToString & "' ")
        End If
        sb.AppendLine("ORDER BY Cca.Fch, Cca.Cca ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oCca As New DTOCca(oDrd("Guid"))
            With oCca
                .Id = oDrd("Cca")
                .Fch = oDrd("Fch")
                .Concept = oDrd("txt")
                .Ccd = oDrd("ccd")
                .Cdn = oDrd("Cdn")
                If Not IsDBNull(oDrd("Hash")) Then
                    .DocFile = New DTODocFile(oDrd("Hash"))
                End If
            End With

            Dim itm As New DTOCcb()
            With itm
                .Cca = oCca
                .Cta = oExtracte.Cta
                .Contact = oExtracte.Contact
                .Amt = DTOAmt.Factory(CDec(oDrd("Eur")), oDrd("Cur").ToString, CDec(oDrd("Pts")))
                .Dh = oDrd("dh")
                If Not IsDBNull(oDrd("PndGuid")) Then
                    .Pnd = New DTOPnd(oDrd("PndGuid"))
                End If
            End With
            retval.Add(itm)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
