Public Class TaskScheduleLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOTaskSchedule
        Dim retval As DTOTaskSchedule = Nothing
        Dim oTaskSchedule As New DTOTaskSchedule(oGuid)
        If Load(oTaskSchedule) Then
            retval = oTaskSchedule
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oTaskSchedule As DTOTaskSchedule) As Boolean
        If Not oTaskSchedule.IsLoaded And Not oTaskSchedule.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM TaskSchedule ")
            sb.AppendLine("WHERE Guid='" & oTaskSchedule.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oTaskSchedule
                    .Task = New DTOTask(oDrd("Task"))
                    .enabled = oDrd("Enabled")
                    .weekDays = DTOTaskSchedule.decodedWeekdays(oDrd("WeekDays"))
                    .mode = oDrd("Mode")
                    .timeInterval = oDrd("TimeInterval")
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oTaskSchedule.IsLoaded
        Return retval
    End Function


    Shared Function Update(oTaskSchedule As DTOTaskSchedule, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oTaskSchedule, oTrans)
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


    Shared Sub Update(oTaskSchedule As DTOTaskSchedule, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM TaskSchedule ")
        sb.AppendLine("WHERE Guid='" & oTaskSchedule.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oTaskSchedule.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oTaskSchedule
            oRow("Task") = .Task.Guid
            oRow("Enabled") = .enabled
            oRow("Mode") = .mode
            oRow("TimeInterval") = TimeHelper.ISO8601(.timeInterval)
            oRow("WeekDays") = .encodedWeekdays()
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oTaskSchedule As DTOTaskSchedule, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Delete(oTaskSchedule, oTrans)
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

    Shared Sub Delete(oTaskSchedule As DTOTaskSchedule, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE TaskSchedule WHERE Guid='" & oTaskSchedule.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class TaskSchedulesLoader

    Shared Function All(oTask As DTOTask) As List(Of DTOTaskSchedule)
        Dim retval As New List(Of DTOTaskSchedule)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM TaskSchedule ")
        sb.AppendLine("WHERE Task='" & oTask.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY WeekDays, Hour, Minute")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOTaskSchedule(oDrd("Guid"))
            With item
                .Task = oTask
                .Enabled = oDrd("Enabled")
                .weekDays = DTOTaskSchedule.decodedWeekdays(oDrd("WeekDays"))
                .mode = oDrd("Mode")
                .timeInterval = TimeHelper.fromISO8601(oDrd("TimeInterval"))
                .IsLoaded = True
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class

