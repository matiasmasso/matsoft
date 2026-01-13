Public Class TamariuLoader

    Shared Function Read() As Tamariu
        Dim retval As New Tamariu()
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT TOP 1 LastOk, LastKo ")
        sb.AppendLine("FROM Tamariu ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            With retval
                .LastOk = oDrd("LastOk")
                .LastKo = oDrd("LastKo")
            End With
        End If

        oDrd.Close()
        Return retval
    End Function

    Shared Function Update(value As Boolean) As Boolean 'returns true if needs to email a blockout warning
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            retval = Update(value, oTrans)
            oTrans.Commit()
        Catch ex As Exception
            oTrans.Rollback()
            Throw New Exception("SQL exception")
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function

    Shared Function Update(value As Boolean, oTrans As SqlTransaction)
        Dim retval As Boolean = False 'true if need to email a blackout warning
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT TOP 1 Guid, LastOk, LastKo, LastWarn ")
        sb.AppendLine("FROM Tamariu ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
        Else
            oRow = oTb.Rows(0)
        End If

        If value Then
            oRow("LastOk") = Now
            oRow("LastWarn") = System.DBNull.Value
            oDA.Update(oDs)
        ElseIf IsDBNull(oRow("LastWarn")) Then
            oRow("LastKo") = Now
            oRow("LastWarn") = Now
            oDA.Update(oDs)
            retval = True
        End If
        Return retval
    End Function
End Class
