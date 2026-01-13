Public Class AppLoader

    Shared Function Find(id As DTOApp.AppTypes) As DTOApp
        Dim retval As DTOApp = Nothing
        Dim oApp As New DTOApp(id)
        If Load(oApp) Then
            retval = oApp
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oApp As DTOApp) As Boolean
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM App ")
        sb.AppendLine("WHERE Id=" & oApp.Id & " ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            With oApp
                .Nom = oDrd("Nom")
                .MinVersion = SQLHelper.GetStringFromDataReader(oDrd("MinVersion"))
                .LastVersion = SQLHelper.GetStringFromDataReader(oDrd("LastVersion"))
            End With
        End If

        oDrd.Close()

        Return True
    End Function

    Shared Function Update(oApp As DTOApp, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oApp, oTrans)
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


    Shared Sub Update(oApp As DTOApp, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM App ")
        sb.AppendLine("WHERE Id=" & oApp.Id & " ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Id") = oApp.Id
        Else
            oRow = oTb.Rows(0)
        End If

        With oApp
            oRow("Nom") = .Nom
            oRow("MinVersion") = SQLHelper.NullableString(.MinVersion)
            oRow("LastVersion") = SQLHelper.NullableString(.LastVersion)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oApp As DTOApp, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oApp, oTrans)
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


    Shared Sub Delete(oApp As DTOApp, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE App WHERE Id=" & oApp.Id & " "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

End Class

Public Class AppsLoader

    Shared Function All() As List(Of DTOApp)
        Dim retval As New List(Of DTOApp)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM App ")
        sb.AppendLine("ORDER BY Id")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOApp(oDrd("Id"))
            With item
                .Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                .LastVersion = SQLHelper.GetStringFromDataReader(oDrd("LastVersion"))
                .MinVersion = SQLHelper.GetStringFromDataReader(oDrd("MinVersion"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class

