Public Class BancPoolLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOBancPool
        Dim retval As DTOBancPool = Nothing
        Dim oBancPool As New DTOBancPool(oGuid)
        If Load(oBancPool) Then
            retval = oBancPool
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oBancPool As DTOBancPool) As Boolean
        If Not oBancPool.IsLoaded And Not oBancPool.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT BancPool.*, Bn1.Nom ")
            sb.AppendLine("FROM BancPool ")
            sb.AppendLine("INNER JOIN Bn1 ON BancPool.Bank=Bn1.Guid ")
            sb.AppendLine("WHERE BancPool.Guid='" & oBancPool.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oBancPool
                    .Bank = New DTOBank(CType(oDrd("Bank"), Guid))
                    .Bank.RaoSocial = oDrd("Nom")
                    .Fch = CDate(oDrd("Fch"))
                    '.FchTo = SQLHelper.GetFchFromDataReader(oDrd("FchTo"))
                    .ProductCategory = CType(oDrd("ProductCategory"), DTOBancPool.ProductCategories)
                    .Amt = DTOAmt.Factory(CDec(oDrd("Eur")))
                    .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oBancPool.IsLoaded
        Return retval
    End Function

    Shared Function Update(oBancPool As DTOBancPool, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oBancPool, oTrans)
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


    Shared Sub Update(oBancPool As DTOBancPool, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM BancPool ")
        sb.AppendLine("WHERE Guid='" & oBancPool.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oBancPool.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oBancPool
            oRow("Bank") = .Bank.Guid
            oRow("Fch") = .Fch
            'oRow("FchTo") = SQLHelper.NullableFch(.FchTo)
            oRow("ProductCategory") = CInt(.ProductCategory)
            oRow("Eur") = .Amt.Eur
            oRow("Obs") = SQLHelper.NullableString(.Obs)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oBancPool As DTOBancPool, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oBancPool, oTrans)
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


    Shared Sub Delete(oBancPool As DTOBancPool, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE BancPool WHERE Guid='" & oBancPool.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class BancPoolsLoader

    Shared Function All(Optional oBank As DTOBank = Nothing, Optional DtFch As Date = Nothing) As List(Of DTOBancPool)
        Dim retval As New List(Of DTOBancPool)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT BancPool.Guid, BancPool.Fch, BancPool.ProductCategory, BancPool.Eur ")
        sb.AppendLine(", BancPool.Bank, Bn1.Nom ")
        sb.AppendLine("FROM BancPool ")
        sb.AppendLine("INNER JOIN Bn1 ON BancPool.Bank=Bn1.Guid ")
        If oBank IsNot Nothing Or DtFch <> Nothing Then
            sb.Append("WHERE ")
            If oBank IsNot Nothing Then
                sb.AppendLine("BancPool.Bank = '" & oBank.Guid.ToString & "' ")
                If DtFch <> Nothing Then sb.Append("AND ")
            End If
            If DtFch <> Nothing Then
                sb.AppendLine("BancPool.Fch <= '" & Format(DtFch, "yyyyMMdd") & "' ")
            End If
        End If
        sb.AppendLine("ORDER BY Bn1.Nom, BancPool.Fch")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOBancPool(oDrd("Guid"))
            With item
                .Bank = New DTOBank(CType(oDrd("Bank"), Guid))
                .Bank.RaoSocial = oDrd("Nom")
                .Fch = CDate(oDrd("Fch"))
                '.FchTo = SQLHelper.GetFchFromDataReader(oDrd("FchTo"))
                .ProductCategory = CType(oDrd("ProductCategory"), DTOBancPool.ProductCategories)
                .Amt = DTOAmt.Factory(CDec(oDrd("Eur")))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class

