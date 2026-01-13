Public Class StaffSchedLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOStaffSched
        Dim retval As DTOStaffSched = Nothing
        Dim oStaffSched As New DTOStaffSched(oGuid)
        If Load(oStaffSched) Then
            retval = oStaffSched
        End If
        Return retval
    End Function


    Shared Function Vigent(ByRef oEmp As DTOEmp) As DTOStaffSched
        Dim retval As DTOStaffSched = Nothing

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT TOP 1 StaffSched.Guid ")
        sb.AppendLine("FROM StaffSched ")
        sb.AppendLine("WHERE StaffSched.Emp = " & CInt(oEmp.Id) & " ")
        sb.AppendLine("AND StaffSched.FchTo IS NULL ")
        sb.AppendLine("ORDER BY StaffSched.FchFrom DESC ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOStaffSched(oDrd("Guid"))
        End If
        oDrd.Close()

        Return retval
    End Function



    Shared Function Load(ByRef oStaffSched As DTOStaffSched) As Boolean
        If Not oStaffSched.IsLoaded And Not oStaffSched.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT StaffSched.Emp, StaffSched.Staff, StaffSched.FchFrom, StaffSched.FchTo, StaffSched.Obs ")
            sb.AppendLine(", CliStaff.Abr, CliGral.RaoSocial ")
            sb.AppendLine(", StaffSchedItem.Guid AS ItemGuid, StaffSchedItem.Cod, StaffSchedItem.Weekday, StaffSchedItem.FromHour, StaffSchedItem.FromMinutes, StaffSchedItem.ToHour, StaffSchedItem.Tominutes ")
            sb.AppendLine("FROM StaffSched ")
            sb.AppendLine("LEFT OUTER JOIN CliStaff ON StaffSched.Staff = CliStaff.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CliGral ON StaffSched.Staff = CliGral.Guid ")
            sb.AppendLine("LEFT OUTER JOIN StaffSchedItem ON StaffSched.Guid = StaffSchedItem.StaffSched ")
            sb.AppendLine("WHERE StaffSched.Guid='" & oStaffSched.Guid.ToString & "' ")
            sb.AppendLine("ORDER BY StaffSchedItem.Cod, StaffSchedItem.WeekDay, StaffSchedItem.FromHour, StaffSchedItem.FromMinutes")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                If Not oStaffSched.IsLoaded Then
                    Dim oStaff As DTOStaff = Nothing
                    If Not IsDBNull(oDrd("Staff")) Then
                        oStaff = New DTOStaff(oDrd("Staff"))
                        With oStaff
                            .Abr = SQLHelper.GetStringFromDataReader(oDrd("Abr"))
                            .Nom = SQLHelper.GetStringFromDataReader(oDrd("RaoSocial"))
                        End With
                    End If
                    With oStaffSched
                        .Emp = New DTOEmp(oDrd("Emp"))
                        .Staff = oStaff
                        .FchFrom = oDrd("FchFrom")
                        .FchTo = SQLHelper.GetFchFromDataReader(oDrd("FchTo"))
                        .IsLoaded = True
                    End With
                End If

                If Not IsDBNull(oDrd("ItemGuid")) Then
                    Dim item As New DTOStaffSched.Item(oDrd("ItemGuid"))
                    With item
                        .Cod = oDrd("Cod")
                        .weekDay = oDrd("WeekDay")
                        .FromHour = oDrd("FromHour")
                        .FromMinutes = oDrd("FromMinutes")
                        .ToHour = oDrd("ToHour")
                        .ToMinutes = oDrd("ToMinutes")
                    End With
                    oStaffSched.Items.Add(item)
                End If
            Loop

            oDrd.Close()
        End If

        Dim retval As Boolean = oStaffSched.IsLoaded
        Return retval
    End Function

    Shared Function Update(oStaffSched As DTOStaffSched, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oStaffSched, oTrans)
            oTrans.Commit()
            oStaffSched.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oStaffSched As DTOStaffSched, ByRef oTrans As SqlTransaction)
        UpdateHeader(oStaffSched, oTrans)
        updateItems(oStaffSched, oTrans)
    End Sub

    Shared Sub UpdateHeader(oStaffSched As DTOStaffSched, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM StaffSched ")
        sb.AppendLine("WHERE Guid='" & oStaffSched.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oStaffSched.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oStaffSched
            oRow("Emp") = .Emp.Id
            oRow("Staff") = SQLHelper.NullableBaseGuid(.Staff)
            oRow("FchFrom") = .FchFrom
            oRow("FchTo") = SQLHelper.NullableFch(.FchTo)
            oRow("Obs") = SQLHelper.NullableString(.Obs)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateItems(oStaffSched As DTOStaffSched, ByRef oTrans As SqlTransaction)
        DeleteItems(oStaffSched, oTrans)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM StaffSchedItem ")
        sb.AppendLine("WHERE StaffSched='" & oStaffSched.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each item In oStaffSched.Items
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("StaffSched") = oStaffSched.Guid
            With item
                oRow("Guid") = .Guid
                oRow("Cod") = .Cod
                oRow("WeekDay") = .weekDay
                oRow("FromHour") = .FromHour
                oRow("FromMinutes") = .FromMinutes
                oRow("ToHour") = .ToHour
                oRow("ToMinutes") = .ToMinutes
            End With
        Next

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oStaffSched As DTOStaffSched, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oStaffSched, oTrans)
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


    Shared Sub Delete(oStaffSched As DTOStaffSched, ByRef oTrans As SqlTransaction)
        DeleteItems(oStaffSched, oTrans)
        DeleteHeader(oStaffSched, oTrans)
    End Sub

    Shared Sub DeleteHeader(oStaffSched As DTOStaffSched, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE StaffSched WHERE Guid='" & oStaffSched.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteItems(oStaffSched As DTOStaffSched, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE StaffSchedItem WHERE StaffSched='" & oStaffSched.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class StaffSchedsLoader

    Shared Function All(oEmp As DTOEmp) As List(Of DTOStaffSched)
        Dim retval As New List(Of DTOStaffSched)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT StaffSched.Guid, StaffSched.FchFrom, StaffSched.FchTo, StaffSched.Obs ")
        sb.AppendLine(", StaffSchedItem.Guid AS ItemGuid, StaffSchedItem.Cod, StaffSchedItem.Weekday, StaffSchedItem.FromHour, StaffSchedItem.FromMinutes, StaffSchedItem.ToHour, StaffSchedItem.Tominutes ")
        sb.AppendLine("FROM StaffSched ")
        sb.AppendLine("LEFT OUTER JOIN StaffSchedItem ON StaffSched.Guid = StaffSchedItem.StaffSched ")
        sb.AppendLine("WHERE StaffSched.Emp='" & oEmp.Id & "' ")
        sb.AppendLine("AND StaffSched.Staff IS NULL ")
        sb.AppendLine("ORDER BY StaffSched.FchFrom DESC, StaffSchedItem.Cod, StaffSchedItem.WeekDay, StaffSchedItem.FromHour, StaffSchedItem.FromMinutes")
        Dim oStaffSched As New DTOStaffSched
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oStaffSched.Guid.Equals(oDrd("Guid")) Then
                oStaffSched = New DTOStaffSched(oDrd("Guid"))
                With oStaffSched
                    .Emp = oEmp
                    .FchFrom = oDrd("FchFrom")
                    .FchTo = SQLHelper.GetFchFromDataReader(oDrd("FchTo"))
                    .IsLoaded = True
                End With
                retval.Add(oStaffSched)
            End If

            If Not IsDBNull(oDrd("ItemGuid")) Then
                Dim item As New DTOStaffSched.Item(oDrd("ItemGuid"))
                With item
                    .Cod = oDrd("Cod")
                    .weekDay = oDrd("WeekDay")
                    .FromHour = oDrd("FromHour")
                    .FromMinutes = oDrd("FromMinutes")
                    .ToHour = oDrd("ToHour")
                    .ToMinutes = oDrd("ToMinutes")
                End With
                oStaffSched.Items.Add(item)
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oStaff As DTOStaff) As List(Of DTOStaffSched)
        Dim retval As New List(Of DTOStaffSched)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT StaffSched.Guid, StaffSched.Emp, StaffSched.FchFrom, StaffSched.FchTo, StaffSched.Obs ")
        sb.AppendLine(", CliStaff.Abr, CliGral.RaoSocial ")
        sb.AppendLine(", StaffSchedItem.Guid AS ItemGuid, StaffSchedItem.Cod, StaffSchedItem.Weekday, StaffSchedItem.FromHour, StaffSchedItem.FromMinutes, StaffSchedItem.ToHour, StaffSchedItem.Tominutes ")
        sb.AppendLine("FROM StaffSched ")
        sb.AppendLine("LEFT OUTER JOIN CliStaff ON StaffSched.Staff = CliStaff.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON StaffSched.Staff = CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN StaffSchedItem ON StaffSched.Guid = StaffSchedItem.StaffSched ")
        sb.AppendLine("WHERE StaffSched.Staff='" & oStaff.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY StaffSched.FchFrom DESC, StaffSchedItem.Cod, StaffSchedItem.WeekDay, StaffSchedItem.FromHour, StaffSchedItem.FromMinutes")
        Dim oStaffSched As New DTOStaffSched
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oStaffSched.Guid.Equals(oDrd("Guid")) Then
                oStaffSched = New DTOStaffSched(oDrd("Guid"))
                With oStaffSched
                    .Emp = New DTOEmp(oDrd("Emp"))
                    .Staff = oStaff
                    .FchFrom = oDrd("FchFrom")
                    .FchTo = SQLHelper.GetFchFromDataReader(oDrd("FchTo"))
                    .IsLoaded = True
                End With
                retval.Add(oStaffSched)
            End If

            If Not IsDBNull(oDrd("ItemGuid")) Then
                Dim item As New DTOStaffSched.Item(oDrd("ItemGuid"))
                With item
                    .Cod = oDrd("Cod")
                    .weekDay = oDrd("WeekDay")
                    .FromHour = oDrd("FromHour")
                    .FromMinutes = oDrd("FromMinutes")
                    .ToHour = oDrd("ToHour")
                    .ToMinutes = oDrd("ToMinutes")
                End With
                oStaffSched.Items.Add(item)
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class