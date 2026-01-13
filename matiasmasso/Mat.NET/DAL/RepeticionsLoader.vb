Public Class RepeticionsLoader
    Shared Function All(oProduct As DTOProduct, Optional DtFchFrom As Date = Nothing, Optional DtFchTo As Date = Nothing) As List(Of DTORepeticio)
        Dim retval As New List(Of DTORepeticio)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Pdc.CliGuid, CliGral.FullNom, COUNT(DISTINCT Pnc.PdcGuid) AS Orders, SUM(Pnc.Qty) AS Qty, SUM(Pnc.Qty*Pnc.Eur*(100- Pnc.Dto)/100) AS Eur ")
        sb.AppendLine("FROM Pdc ")
        sb.AppendLine("INNER JOIN Pnc ON Pdc.Guid=Pnc.PdcGuid ")
        sb.AppendLine("INNER JOIN CliGral ON Pdc.CliGuid = CliGral.Guid ")
        sb.AppendLine("INNER JOIN VwProductParent ON Pnc.ArtGuid = VwProductParent.Child ")
        sb.AppendLine("WHERE VwProductParent.Parent = '" & oProduct.Guid.ToString & "' ")

        If DtFchFrom <> Nothing Then
            sb.AppendLine("AND Pdc.Fch>='" & Format(DtFchFrom, "yyyyMMdd") & "' ")
        End If

        If DtFchTo <> Nothing Then
            sb.AppendLine("AND Pdc.Fch<='" & Format(DtFchTo, "yyyyMMdd") & "' ")
        End If

        sb.AppendLine("AND Pdc.Cod=" & CInt(DTOPurchaseOrder.Codis.Client) & " ")
        sb.AppendLine("GROUP BY Pdc.CliGuid, CliGral.FullNom ")
        sb.AppendLine("ORDER BY Orders DESC, Eur DESC, Qty DESC")
        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTORepeticio(oDrd("CliGuid"))
            With item
                .FullNom = oDrd("FullNom")
                .Orders = SQLHelper.GetIntegerFromDataReader(oDrd("Orders"))
                .Qty = SQLHelper.GetIntegerFromDataReader(oDrd("Qty"))
                .Eur = SQLHelper.GetDecimalFromDataReader(oDrd("Eur"))
            End With

            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class
