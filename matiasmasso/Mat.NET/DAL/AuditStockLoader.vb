Public Class AuditStockLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOAuditStock
        Dim retval As DTOAuditStock = Nothing
        Dim oAuditStock As New DTOAuditStock(oGuid)
        If Load(oAuditStock) Then
            retval = oAuditStock
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oAuditStock As DTOAuditStock) As Boolean
        If Not oAuditStock.IsLoaded And Not oAuditStock.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM AuditStock ")
            sb.AppendLine("WHERE Guid='" & oAuditStock.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oAuditStock
                    .Year = oDrd("Yea")
                    .Ref = SQLHelper.GetStringFromDataReader(oDrd("Ref"))
                    .Dsc = SQLHelper.GetStringFromDataReader(oDrd("Dsc"))
                    .Qty = SQLHelper.GetIntegerFromDataReader(oDrd("Qty"))
                    .Palet = SQLHelper.GetStringFromDataReader(oDrd("Palet"))
                    .FchEntrada = SQLHelper.GetFchFromDataReader(oDrd("FchEntrada"))
                    .Dias = SQLHelper.GetIntegerFromDataReader(oDrd("Dias"))
                    .Entrada = SQLHelper.GetStringFromDataReader(oDrd("Entrada"))
                    .Procedencia = SQLHelper.GetStringFromDataReader(oDrd("Procedencia"))
                    .Cost = SQLHelper.GetDecimalFromDataReader(oDrd("Cost"))
                    If Not IsDBNull(oDrd("Sku")) Then
                        .Sku = New DTOProductSku(oDrd("Sku"))
                    End If
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oAuditStock.IsLoaded
        Return retval
    End Function

    Shared Function Update(oAuditStock As DTOAuditStock, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oAuditStock, oTrans)
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


    Shared Sub Update(oAuditStock As DTOAuditStock, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM AuditStock ")
        sb.AppendLine("WHERE Guid='" & oAuditStock.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oAuditStock.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oAuditStock
            oRow("Yea") = .Year
            oRow("Ref") = .Ref
            oRow("Dsc") = SQLHelper.NullableString(.Dsc)
            oRow("Qty") = SQLHelper.NullableInt(.Qty)
            oRow("Palet") = SQLHelper.NullableString(.Palet)
            oRow("FchEntrada") = SQLHelper.NullableFch(.FchEntrada)
            oRow("Dias") = SQLHelper.NullableInt(.Dias)
            oRow("Entrada") = SQLHelper.NullableString(.Entrada)
            oRow("Procedencia") = SQLHelper.NullableString(.Procedencia)
            oRow("Cost") = SQLHelper.NullableDecimal(.Cost)
            oRow("Sku") = SQLHelper.NullableBaseGuid(.Sku)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oAuditStock As DTOAuditStock, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oAuditStock, oTrans)
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


    Shared Sub Delete(oAuditStock As DTOAuditStock, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE AuditStock WHERE Guid='" & oAuditStock.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class AuditStocksLoader

    Shared Function All(oExercici As DTOExercici) As List(Of DTOAuditStock)
        Dim retval As New List(Of DTOAuditStock)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM AuditStock ")
        sb.AppendLine("WHERE Yea=" & oExercici.Year & " ")
        sb.AppendLine("ORDER BY Ref")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOAuditStock(oDrd("Guid"))
            With item
                .Year = oDrd("Yea")
                .Ref = SQLHelper.GetStringFromDataReader(oDrd("Ref"))
                .Dsc = SQLHelper.GetStringFromDataReader(oDrd("Dsc"))
                .Qty = SQLHelper.GetIntegerFromDataReader(oDrd("Qty"))
                .Palet = SQLHelper.GetStringFromDataReader(oDrd("Palet"))
                .FchEntrada = SQLHelper.GetFchFromDataReader(oDrd("FchEntrada"))
                .Dias = SQLHelper.GetIntegerFromDataReader(oDrd("Dias"))
                .Entrada = SQLHelper.GetStringFromDataReader(oDrd("Entrada"))
                .Procedencia = SQLHelper.GetStringFromDataReader(oDrd("Procedencia"))
                .Cost = SQLHelper.GetDecimalFromDataReader(oDrd("Cost"))
                If Not IsDBNull(oDrd("Sku")) Then
                    .Sku = New DTOProductSku(oDrd("Sku"))
                End If
                .IsLoaded = True
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Update(oAuditStocks As List(Of DTOAuditStock), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Dim oExercici = oAuditStocks.First.Exercici
            Delete(oExercici.Year, oTrans)
            Update(oAuditStocks, oTrans)
            SetSkuGuids(oExercici.Emp, oTrans)
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


    Shared Sub Update(oAuditStocks As List(Of DTOAuditStock), ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM AuditStock ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        For Each item As DTOAuditStock In oAuditStocks
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)

            With item
                oRow("Guid") = .Guid
                oRow("Yea") = .Year
                oRow("Ref") = SQLHelper.NullableInt(.Ref)
                oRow("Dsc") = SQLHelper.NullableString(.Dsc)
                oRow("Qty") = SQLHelper.NullableInt(.Qty)
                oRow("Palet") = SQLHelper.NullableString(.Palet)
                oRow("FchEntrada") = SQLHelper.NullableFch(.FchEntrada)
                oRow("Dias") = SQLHelper.NullableInt(.Dias)
                oRow("Entrada") = SQLHelper.NullableString(.Entrada)
                oRow("Procedencia") = SQLHelper.NullableString(.Procedencia)
                oRow("Cost") = SQLHelper.NullableDecimal(.Cost)
                oRow("Sku") = SQLHelper.NullableBaseGuid(.Sku)
            End With

        Next

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(iYear As Integer, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(iYear, oTrans)

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

    Shared Sub Delete(iYear As Integer, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE AuditStock ")
        sb.AppendLine("WHERE Yea =" & iYear & " ")

        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub SetSkuGuids(oEmp As DTOEmp, ByRef oTrans As SqlTransaction)
        'assigna l'identificador intern de producte en base a la referencia pública facilitada per el magatzem
        Dim sb As New Text.StringBuilder
        sb.AppendLine("UPDATE AuditStock ")
        sb.AppendLine("SET AuditStock.Sku = VwSkuNom.SkuGuid ")
        sb.AppendLine("FROM AuditStock ")
        sb.AppendLine("INNER JOIN VwSkuNom ON VwSkuNom.Emp =" & oEmp.Id & " AND VwSkuNom.SkuRef=AuditStock.Ref ")
        sb.AppendLine("WHERE AuditStock.Sku IS NULL")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub


End Class
