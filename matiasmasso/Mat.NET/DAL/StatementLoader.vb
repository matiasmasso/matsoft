Public Class StatementLoader

    Shared Function Years(oContact As DTOContact) As List(Of Integer)
        Dim retval As New List(Of Integer)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Year(Fch) AS Year ")
        sb.AppendLine("FROM VwCca ")
        sb.AppendLine("WHERE ContactGuid = '" & oContact.Guid.ToString & "' ")
        sb.AppendLine("GROUP BY YEAR(Fch)")
        sb.AppendLine("ORDER BY YEAR(Fch) DESC")
        Dim SQL = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            retval.Add(oDrd("Year"))
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Items(oContact As DTOContact, year As Integer) As DTOStatement
        Dim retval As New DTOStatement
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT CcaGuid, CcbGuid, PndGuid, Ccd, CcaId, Fch, txt, Eur, Cur, Pts, Dh, [Hash] ")
        sb.AppendLine(", CtaGuid, CtaId, CtaCod, CtaAct, CtaEsp, CtaCat, CtaEng ")
        sb.AppendLine("FROM VwCca ")
        sb.AppendLine("WHERE ContactGuid = '" & oContact.Guid.ToString & "' ")
        sb.AppendLine("AND YEAR(Fch) = " & year & " ")
        sb.AppendLine("ORDER BY CtaId, Fch, CcaId")
        Dim SQL = sb.ToString
        Dim oCta As New DTOPgcCta
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oCta.Guid.Equals(oDrd("CtaGuid")) Then
                oCta = New DTOPgcCta(oDrd("CtaGuid"))
                With oCta
                    .Id = oDrd("CtaId")
                    .Cod = oDrd("CtaCod")
                    .Act = oDrd("CtaAct")
                    .Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "CtaEsp", "CtaCat", "CtaEng")
                End With
                retval.Ctas.Add(oCta)
            End If
            Dim oItem As New DTOStatement.Item
            With oItem
                .CcaGuid = oDrd("CcaGuid")
                .CcbGuid = oDrd("CcbGuid")
                .CtaGuid = oDrd("CtaGuid")
                .PndGuid = SQLHelper.GetGuidFromDataReader(oDrd("PndGuid"))
                .Ccd = SQLHelper.GetIntegerFromDataReader(oDrd("Ccd"))
                .CcaId = oDrd("CcaId")
                .Fch = oDrd("Fch")
                .Concept = oDrd("Txt")
                .Amt = SQLHelper.GetAmtCompactFromDataReader(oDrd, "Pts", "Cur", "Eur")
                .Dh = oDrd("Dh")
                .Hash = SQLHelper.GetStringFromDataReader(oDrd("Hash"))
            End With
            retval.Items.Add(oItem)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
