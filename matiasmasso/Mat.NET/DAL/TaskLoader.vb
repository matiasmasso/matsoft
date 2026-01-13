Public Class TaskLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOTask
        Dim retval As DTOTask = Nothing
        Dim oTask As New DTOTask(oGuid)
        If Load(oTask) Then
            retval = oTask
        End If
        Return retval
    End Function

    Shared Function Find(oCod As DTOTask.Cods) As DTOTask
        Dim retval As DTOTask = Nothing
        Dim SQL As String = "SELECT Guid FROM Task WHERE Cod=" & CInt(oCod)
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOTask(oDrd("Guid"))
            retval.Cod = oCod
            Load(retval)
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function Load(ByRef oTask As DTOTask) As Boolean
        If Not oTask.IsLoaded Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Task.Cod, Task.Nom, Task.Dsc ")
            sb.AppendLine(", Task.Enabled, Task.NotBefore, Task.NotAfter ")
            sb.AppendLine(", VwTaskLastLog.Guid AS LastLog, VwTaskLastLog.Fch, VwTaskLastLog.ResultCod, VwTaskLastLog.ResultMsg ")
            sb.AppendLine(", TaskSchedule.Guid AS ScheduleGuid, TaskSchedule.Enabled AS SchedEnabled, TaskSchedule.WeekDays ")
            sb.AppendLine(", TaskSchedule.Mode, TaskSchedule.TimeInterval ")
            sb.AppendLine("FROM Task ")
            sb.AppendLine("LEFT OUTER JOIN TaskSchedule ON TaskSchedule.Task = Task.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwTaskLastLog ON Task.Guid = VwTaskLastLog.Task ")
            sb.AppendLine("WHERE Task.Guid='" & oTask.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                If Not oTask.IsLoaded Then
                    With oTask
                        .Cod = SQLHelper.GetIntegerFromDataReader(oDrd("Cod"))
                        .Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                        .Dsc = SQLHelper.GetStringFromDataReader(oDrd("Dsc"))
                        .Enabled = oDrd("Enabled")
                        .NotBefore = SQLHelper.GetDateTimeOffsetFromDataReader(oDrd("NotBefore"))
                        .NotAfter = SQLHelper.GetDateTimeOffsetFromDataReader(oDrd("NotAfter"))

                        If Not IsDBNull(oDrd("LastLog")) Then
                            .LastLog = New DTOTaskLog(oDrd("LastLog"))
                            With .LastLog
                                .Fch = oDrd("Fch")
                                .ResultCod = SQLHelper.GetIntegerFromDataReader(oDrd("Resultcod"))
                                .ResultMsg = SQLHelper.GetStringFromDataReader(oDrd("ResultMsg"))
                            End With
                        End If
                        .IsLoaded = True
                    End With
                End If

                If Not IsDBNull(oDrd("ScheduleGuid")) Then
                    Dim oSchedule As New DTOTaskSchedule(oDrd("ScheduleGuid"))
                    With oSchedule
                        .Task = oTask
                        .WeekDays = DTOTaskSchedule.DecodedWeekdays(oDrd("WeekDays"))
                        .Mode = oDrd("Mode")
                        .TimeInterval = TimeHelper.fromISO8601(oDrd("TimeInterval"))
                        .Enabled = oDrd("SchedEnabled")
                    End With

                    If Not oTask.Schedules.Any(Function(x) x.Equals(oSchedule)) Then
                        oTask.Schedules.Add(oSchedule)
                    End If
                End If
            Loop
            oDrd.Close()
        End If


        Dim retval As Boolean = oTask.IsLoaded
        Return retval
    End Function

    Shared Function Update(oTask As DTOTask, ByRef exs As List(Of Exception)) As Boolean
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
        UpdateHeader(oTask, oTrans)
        UpdateSchedules(oTask, oTrans)
    End Sub
    Shared Sub UpdateHeader(oTask As DTOTask, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Task ")
        sb.AppendLine("WHERE Guid='" & oTask.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oTask.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oTask
            oRow("Cod") = SQLHelper.NullableInt(.Cod)
            oRow("Nom") = .Nom
            oRow("Dsc") = SQLHelper.NullableString(.Dsc)
            oRow("Enabled") = .Enabled
            oRow("NotBefore") = SQLHelper.NullableDateTimeOffset(.NotBefore)
            oRow("NotAfter") = SQLHelper.NullableDateTimeOffset(.NotAfter)
            'oRow("LastLog") = SQLHelper.NullableBaseGuid(.LastLog)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateSchedules(oTask As DTOTask, ByRef oTrans As SqlTransaction)
        DeleteSchedules(oTask, oTrans)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM TaskSchedule ")
        sb.AppendLine("WHERE Task ='" & oTask.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each oSchedule In oTask.Schedules
            Dim oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Task") = oTask.Guid
            With oSchedule
                oRow("Guid") = oSchedule.Guid
                oRow("Enabled") = .Enabled
                oRow("Mode") = .Mode
                oRow("TimeInterval") = TimeHelper.ISO8601(.TimeInterval)
                oRow("WeekDays") = .EncodedWeekdays()
            End With
        Next


        oDA.Update(oDs)
    End Sub

    Shared Function Log(oTask As DTOTask, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Dim oTaskLog As DTOTaskLog = oTask.LastLog
            TaskLoader.Update(oTask, oTrans)
            TaskLogLoader.Update(oTask, oTrans)

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

    Shared Function Delete(oTask As DTOTask, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oTask, oTrans)
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


    Shared Sub Delete(oTask As DTOTask, ByRef oTrans As SqlTransaction)
        DeleteSchedules(oTask, oTrans)
        DeleteHeader(oTask, oTrans)
    End Sub

    Shared Sub DeleteHeader(oTask As DTOTask, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Task WHERE Guid = '" & oTask.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteSchedules(oTask As DTOTask, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE TaskSchedule WHERE Task = '" & oTask.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

    Shared Function LastSuccessfulLog(oTask As DTOTask) As Date
        Dim retval As DateTime = Nothing
        Dim SQL As String = "SELECT TOP 1 Fch FROM TaskLog WHERE Task='" & oTask.Guid.ToString() & "' AND ResultCod=1  ORDER BY FCH DESC"
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = SQLHelper.GetDateTimeOffsetFromDataReader(oDrd("Fch")).UtcDateTime
        End If
        oDrd.Close()
        Return retval
    End Function

End Class

Public Class TasksLoader

    Shared Function All(Optional OnlyEnabled As Boolean = False) As List(Of DTOTask)
        Dim retval As New List(Of DTOTask)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Task.Guid AS TaskGuid, Task.Cod, Task.Nom, Task.Enabled as TaskEnabled, Task.NotBefore, Task.NotAfter, Task.Dsc ")
        sb.AppendLine(", TaskSchedule.Guid AS ScheduleGuid, TaskSchedule.Mode, TaskSchedule.TimeInterval ")
        sb.AppendLine(", TaskSchedule.WeekDays, TaskSchedule.Enabled AS SchedEnabled ")
        sb.AppendLine(", VwTaskLastLog.Guid AS LastLog, VwTaskLastLog.Fch, VwTaskLastLog.ResultCod, VwTaskLastLog.ResultMsg ")
        sb.AppendLine("FROM Task ")
        sb.AppendLine("LEFT OUTER JOIN TaskSchedule ON TaskSchedule.Task = Task.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwTaskLastLog ON Task.Guid = VwTaskLastLog.Task ")

        If OnlyEnabled Then
            sb.AppendLine("WHERE Task.Enabled<>0 ")
            sb.AppendLine("AND (Task.NotBefore IS NULL OR Task.NotBefore < GETDATE()) ")
            sb.AppendLine("AND (Task.NotAfter IS NULL OR Task.NotAfter > GETDATE()) ")
            sb.AppendLine("AND TaskSchedule.Enabled<>0 ")
        End If

        sb.AppendLine("ORDER BY Task.Cod, TaskSchedule.Weekdays, TaskSchedule.Hour, TaskSchedule.Minute")
        Dim SQL As String = sb.ToString

        Dim oTask As New DTOTask()
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oDrd("TaskGuid").Equals(oTask.Guid) Then
                oTask = New DTOTask(oDrd("TaskGuid"))
                With oTask
                    .Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                    .Cod = oDrd("Cod")
                    .Dsc = SQLHelper.GetStringFromDataReader(oDrd("Dsc"))
                    .NotBefore = SQLHelper.GetDateTimeOffsetFromDataReader(oDrd("NotBefore"))
                    .NotAfter = SQLHelper.GetDateTimeOffsetFromDataReader(oDrd("NotAfter"))
                    .Enabled = oDrd("TaskEnabled")

                    If Not IsDBNull(oDrd("LastLog")) Then
                        .LastLog = New DTOTaskLog(oDrd("LastLog"))
                        With .lastLog
                            .fch = oDrd("Fch")
                            .fch.truncateMilisecfractions
                            .resultCod = SQLHelper.GetIntegerFromDataReader(oDrd("ResultCod"))
                            .ResultMsg = SQLHelper.GetStringFromDataReader(oDrd("ResultMsg"))
                        End With
                    End If
                End With
                retval.Add(oTask)
            End If

            If Not IsDBNull(oDrd("ScheduleGuid")) Then
                Dim oSchedule As New DTOTaskSchedule(oDrd("ScheduleGuid"))
                With oSchedule
                    .task = oTask
                    .weekDays = DTOTaskSchedule.decodedWeekdays(oDrd("WeekDays"))
                    .mode = oDrd("Mode")
                    .timeInterval = TimeHelper.fromISO8601(oDrd("TimeInterval"))
                    .enabled = oDrd("SchedEnabled")
                End With
                If Not oTask.schedules.Any(Function(x) x.Equals(oSchedule)) Then
                    oTask.schedules.Add(oSchedule)
                End If
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class
