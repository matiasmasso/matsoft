Public Class StaffHolidayLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOStaffHoliday
        Dim retval As DTOStaffHoliday = Nothing
        Dim oStaffHoliday As New DTOStaffHoliday(oGuid)
        If Load(oStaffHoliday) Then
            retval = oStaffHoliday
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oStaffHoliday As DTOStaffHoliday) As Boolean
        If Not oStaffHoliday.IsLoaded And Not oStaffHoliday.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT StaffHoliday.* ")
            sb.AppendLine(", CliStaff.Abr ")
            sb.AppendLine("FROM StaffHoliday ")
            sb.AppendLine("LEFT OUTER JOIN CliStaff ON StaffHoliday.Staff = CliStaff.Guid ")
            sb.AppendLine("WHERE StaffHoliday.Guid='" & oStaffHoliday.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oStaffHoliday
                    .Emp = New DTOEmp(oDrd("Emp"))
                    If Not IsDBNull(oDrd("Staff")) Then
                        .Staff = New DTOStaff(oDrd("Staff"))
                        .Staff.Abr = SQLHelper.GetStringFromDataReader(oDrd("Abr"))
                    End If
                    .Cod = oDrd("Cod")
                    .FchFrom = oDrd("FchFrom")
                    .FchTo = oDrd("FchTo")
                    .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oStaffHoliday.IsLoaded
        Return retval
    End Function

    Shared Function Update(oStaffHoliday As DTOStaffHoliday, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oStaffHoliday, oTrans)
            oTrans.Commit()
            oStaffHoliday.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oStaffHoliday As DTOStaffHoliday, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM StaffHoliday ")
        sb.AppendLine("WHERE Guid='" & oStaffHoliday.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oStaffHoliday.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oStaffHoliday
            oRow("Emp") = .Emp.Id
            oRow("Staff") = SQLHelper.NullableBaseGuid(.Staff)
            oRow("Cod") = .Cod
            oRow("FchFrom") = .FchFrom
            oRow("FchTo") = .FchTo
            oRow("Obs") = SQLHelper.NullableString(.Obs)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oStaffHoliday As DTOStaffHoliday, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oStaffHoliday, oTrans)
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


    Shared Sub Delete(oStaffHoliday As DTOStaffHoliday, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE StaffHoliday WHERE Guid='" & oStaffHoliday.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class StaffHolidaysLoader

    Shared Function All(oEmp As DTOEmp, Optional oStaff As DTOStaff = Nothing) As List(Of DTOStaffHoliday)
        Dim retval As New List(Of DTOStaffHoliday)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT StaffHoliday.* ")
        sb.AppendLine(", CliStaff.Abr ")
        sb.AppendLine("FROM StaffHoliday ")
        sb.AppendLine("LEFT OUTER JOIN CliStaff ON StaffHoliday.Staff = CliStaff.Guid ")
        sb.AppendLine("WHERE StaffHoliday.Emp=" & CInt(oEmp.Id) & " ")
        If oStaff Is Nothing Then
            sb.AppendLine("AND StaffHoliday.Staff IS NULL ")
        Else
            sb.AppendLine("AND StaffHoliday.Staff = '" & oStaff.Guid.ToString & "' ")
        End If
        sb.AppendLine("ORDER BY StaffHoliday.Staff, StaffHoliday.FchFrom ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOStaffHoliday(oDrd("Guid"))
            With item
                .Emp = New DTOEmp(oDrd("Emp"))
                If Not IsDBNull(oDrd("Staff")) Then
                    .Staff = New DTOStaff(oDrd("Staff"))
                    .Staff.Abr = SQLHelper.GetStringFromDataReader(oDrd("Abr"))
                End If
                .Cod = oDrd("Cod")
                .FchFrom = oDrd("FchFrom")
                .FchTo = oDrd("FchTo")
                .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class

