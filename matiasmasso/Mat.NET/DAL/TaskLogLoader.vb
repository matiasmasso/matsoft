Public Class TaskLogLoader

#Region "CRUD"
    Shared Function Find(oGuid As Guid) As DTOTaskLog
        Dim retval As DTOTaskLog = Nothing
        Dim oTaskLog As New DTOTaskLog(oGuid)
        If Load(oTaskLog) Then
            retval = oTaskLog
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oTaskLog As DTOTaskLog) As Boolean
        If Not oTaskLog.IsLoaded And Not oTaskLog.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM TaskLog ")
            sb.AppendLine("WHERE Guid='" & oTaskLog.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oTaskLog
                    .Fch = oDrd("Fch")
                    .ResultCod = oDrd("ResultCod")
                    .ResultMsg = SQLHelper.GetStringFromDataReader(oDrd("ResultMsg"))
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oTaskLog.IsLoaded
        Return retval
    End Function

    Shared Function Update(oTask As DTOTask, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oTask, oTrans)
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


    Shared Sub Update(oTask As DTOTask, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM TaskLog ")
        sb.AppendLine("WHERE Guid='" & oTask.LastLog.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oTask.LastLog.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oTask
            oRow("Task") = .Guid
            oRow("Fch") = .LastLog.Fch
            oRow("ResultCod") = .LastLog.ResultCod
            oRow("ResultMsg") = .LastLog.ResultMsg
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oTasklog As DTOTaskLog, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Delete(oTasklog, oTrans)
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


    Shared Sub Delete(oTaskLog As DTOTaskLog, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE TaskLog WHERE Guid='" & oTaskLog.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region



End Class

Public Class TaskLogsLoader

    Shared Function All(oTask As DTOTask) As List(Of DTOTaskLog)
        Dim retval As New List(Of DTOTaskLog)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Guid, Fch, ResultCod ")
        sb.AppendLine("FROM TaskLog ")
        sb.AppendLine("WHERE Task='" & oTask.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY Fch DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOTaskLog(oDrd("Guid"))
            With item
                .Fch = oDrd("Fch")
                .ResultCod = oDrd("ResultCod")
                '.ResultMsg = SQLHelper.GetStringFromDataReader(oDrd("ResultMsg")) (triga massa)
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Delete(oTaskLogs As List(Of DTOTaskLog), exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE TaskLog ")
        sb.AppendLine("WHERE (")
        For Each oTask As DTOTaskLog In oTaskLogs
            If oTask.UnEquals(oTaskLogs.First) Then sb.Append("OR ")
            sb.AppendLine("Guid='" & oTask.Guid.ToString & "' ")
        Next
        sb.AppendLine(") ")

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Dim SQL As String = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL, oTrans)
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

