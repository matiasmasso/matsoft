Public Class PrvCliNumLoader
    Shared Function Find(oProveidor As DTOProveidor, clinum As String) As DTOPrvCliNum
        Dim retval As DTOPrvCliNum = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT PrvCliNum.Customer ")
        sb.AppendLine("FROM PrvCliNum ")
        sb.AppendLine("WHERE PrvClinum.Proveidor='" & oProveidor.Guid.ToString & "' ")
        sb.AppendLine("AND PrvClinum.CliNum='" & clinum & "' ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOPrvCliNum()
            retval.Customer = New DTOCustomer(oDrd("Customer"))
            retval.CliNum = clinum
        End If
        oDrd.Close()
        Return retval
    End Function
End Class
Public Class PrvCliNumsLoader

    Shared Function All(oProveidor As DTOProveidor) As List(Of DTOPrvCliNum)
        Dim retval As New List(Of DTOPrvCliNum)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT PrvCliNum.Customer, PrvCliNum.CliNum ")
        sb.AppendLine(", CliGral.FullNom ")
        sb.AppendLine("FROM PrvCliNum ")
        sb.AppendLine("INNER JOIN CliGral ON PrvCliNum.Customer = CliGral.Guid ")
        sb.AppendLine("ORDER BY CliGral.FullNom")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOPrvCliNum()
            With item
                .CliNum = oDrd("CliNum")
                .Customer = New DTOCustomer(oDrd("Customer"))
                .Customer.FullNom = oDrd("FullNom")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Update(oProveidor As DTOProveidor, oPrvCliNums As List(Of DTOPrvCliNum), exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oProveidor, oTrans)
            Update(oProveidor, oPrvCliNums, oTrans)
            oTrans.Commit()
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function

    Shared Sub Update(oProveidor As DTOProveidor, oPrvCliNums As List(Of DTOPrvCliNum), ByRef oTrans As SqlTransaction)
        Dim retval As New DTOTaskResult
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM PrvCliNum ")
        sb.AppendLine("WHERE Proveidor='" & oProveidor.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        For Each item In oPrvCliNums
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Proveidor") = oProveidor.Guid
            oRow("CliNum") = item.CliNum
            oRow("Customer") = item.Customer.Guid
        Next

        oDA.Update(oDs)
    End Sub

    Shared Sub Delete(oProveidor As DTOProveidor, ByRef oTrans As SqlTransaction)
        Dim retval As New DTOTaskResult
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE PrvCliNum ")
        sb.AppendLine("WHERE Proveidor='" & oProveidor.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        Dim result = SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub
End Class
