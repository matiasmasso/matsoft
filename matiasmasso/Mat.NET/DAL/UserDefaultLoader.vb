Public Class UserDefaultLoader

    Shared Function GetValue(oUser As DTOUser, oCod As DTOUserDefault.Cods) As String
        Dim retval As String = ""

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM UserDefault ")
        sb.AppendLine("WHERE UserGuid ='" & oUser.Guid.ToString & "' AND Cod=" & CInt(oCod) & " ")
        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = oDrd("Value")
        End If
        oDrd.Close()
        Return retval
    End Function



    Shared Function SetValue(oUser As DTOUser, oCod As DTOUserDefault.Cods, sValue As String, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM UserDefault ")
            sb.AppendLine("WHERE UserGuid ='" & oUser.Guid.ToString & "' AND Cod=" & CInt(oCod) & " ")
            Dim SQL As String = sb.ToString

            Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
            Dim oDs As New DataSet
            oDA.Fill(oDs)
            Dim oTb As DataTable = oDs.Tables(0)
            Dim oRow As DataRow
            If oTb.Rows.Count = 0 Then
                oRow = oTb.NewRow
                oTb.Rows.Add(oRow)
                oRow("UserGuid") = oUser.Guid
                oRow("Cod") = CInt(oCod)
            Else
                oRow = oTb.Rows(0)
            End If

            oRow("Value") = sValue

            oDA.Update(oDs)

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
End Class
