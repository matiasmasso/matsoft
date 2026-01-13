Public Class VivaceStockLoader

End Class

Public Class VivaceStocksLoader

    Shared Function All(oEmp As DTOEmp, DtFch As Date) As List(Of DTOVivaceStock)
        Dim retval As New List(Of DTOVivaceStock)
        Dim sb As New System.Text.StringBuilder

        Dim sFch As String = Format(DtFch, "yyyyMMdd")

        sb.AppendLine("SELECT AuditStock.Ref, AuditStock.Dsc, AuditStock.Qty, AuditStock.Palet, AuditStock.FchEntrada, AuditStock.Entrada, AuditStock.Procedencia ")
        sb.AppendLine(", Y.ArtGuid, Y.Pmc, Y.LastFch ")
        sb.AppendLine("FROM AuditStock ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuNom ON Auditstock.Ref = VwSkuNom.SkuId AND VwSkuNom.Emp = " & oEmp.Id & " ")
        sb.AppendLine("LEFT OUTER JOIN ")
        sb.AppendLine("( ")
        sb.AppendLine("	SELECT Arc.ArtGuid, MAX(Arc.Pmc) AS Pmc, X.LastFch ")
        sb.AppendLine("	FROM Arc ")
        sb.AppendLine("	INNER JOIN Alb ON Arc.AlbGuid = Alb.Guid ")
        sb.AppendLine("	INNER JOIN ( ")
        sb.AppendLine("		SELECT Arc.ArtGuid, MAX(Alb.Fch) AS LastFch ")
        sb.AppendLine("		FROM Arc ")
        sb.AppendLine("		INNER JOIN Alb ON Arc.AlbGuid = Alb.Guid ")
        sb.AppendLine("		WHERE Arc.Cod=11 AND Alb.Fch<='" & sFch & "' ")
        sb.AppendLine("		GROUP BY Arc.ArtGuid ")
        sb.AppendLine("	) X ON Arc.ArtGuid = X.ArtGuid ")
        sb.AppendLine("	WHERE Arc.Cod=11 AND Alb.Fch<'" & sFch & "' ")
        sb.AppendLine("	GROUP BY Arc.ArtGuid,  X.LastFch ")
        sb.AppendLine("	) Y ON VwSkuNom.SkuGuid = Y.ArtGuid ")
        sb.AppendLine("WHERE AuditStock.fch='" & sFch & "' ")
        sb.AppendLine("ORDER BY AuditStock.Ref, AuditStock.Palet, AuditStock.FchEntrada ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOVivaceStock()
            With item
                .Referencia = oDrd("Ref")
                .Descripcio = oDrd("Dsc")
                .Stock = oDrd("Qty")
                .Ubicacio = SQLHelper.GetStringFromDataReader(oDrd("Palet"))
                .FchEntrada = SQLHelper.GetFchFromDataReader(oDrd("FchEntrada"))
                .LastMove = SQLHelper.GetFchFromDataReader(oDrd("LastFch"))
                .Procedencia = SQLHelper.GetStringFromDataReader(oDrd("Procedencia"))
                .Cost = SQLHelper.GetAmtFromDataReader(oDrd("Pmc"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Fchs() As List(Of Date)
        Dim retval As New List(Of Date)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Fch ")
        sb.AppendLine("FROM AuditStock ")
        sb.AppendLine("GROUP BY Fch ")
        sb.AppendLine("ORDER BY Fch DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not IsDBNull(oDrd("Fch")) Then
                retval.Add(oDrd("Fch"))
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function Update(exs As List(Of Exception), oEmp As DTOEmp, DtFch As Date, oValues As List(Of DTOVivaceStock)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(DtFch, oValues, oTrans)
            oTrans.Commit()
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(DtFch As Date, values As List(Of DTOVivaceStock), ByRef oTrans As SqlTransaction)
        Delete(DtFch, oTrans)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM AuditStock ")
        sb.AppendLine("WHERE Fch='" & Format(DtFch, "yyyyMMdd") & "'")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        For Each item As DTOVivaceStock In values
            If IsNumeric(item.Referencia) Then

                Dim oRow As DataRow = oTb.NewRow
                oTb.Rows.Add(oRow)
                With item
                    oRow("Ref") = .Referencia
                    oRow("Dsc") = Left(.Descripcio, 60)
                    oRow("Qty") = .Stock
                    oRow("Palet") = Left(.Ubicacio, 50)
                    oRow("FchEntrada") = .FchEntrada
                    oRow("Procedencia") = Left(.Procedencia, 150)
                    oRow("Fch") = DtFch
                    oRow("Yea") = DtFch.Year
                End With
            End If
        Next

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(DtFch As Date, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(DtFch, oTrans)
            oTrans.Commit()
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Delete(DtFch As Date, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE AuditStock WHERE Fch='" & Format(DtFch, "yyyyMMdd") & "'"
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub


End Class

