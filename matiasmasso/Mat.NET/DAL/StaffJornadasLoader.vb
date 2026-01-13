Public Class StaffJornadaLoader
#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOStaffJornada
        Dim retval As DTOStaffJornada = Nothing
        Dim oStaffJornada As New DTOStaffJornada(oGuid)
        If Load(oStaffJornada) Then
            retval = oStaffJornada
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oStaffJornada As DTOStaffJornada) As Boolean
        If Not oStaffJornada.IsLoaded And Not oStaffJornada.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT StaffJornada.Guid, StaffJornada.FchFrom, StaffJornada.FchTo, StaffJornada.Cod, StaffJornada.Obs, CliStaff.Abr ")
            sb.AppendLine("FROM StaffJornada ")
            sb.AppendLine("INNER JOIN CliStaff ON StaffJornada.Staff = CliStaff.Guid ")
            sb.AppendLine("WHERE StaffJornada.Guid='" & oStaffJornada.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oStaffJornada
                    .Staff = New DTOStaff(oDrd("Staff"))
                    .Staff.Abr = oDrd("Abr")
                    .FchFrom = oDrd("FchFrom")
                    .FchTo = oDrd("FchTo")
                    .Cod = oDrd("Cod")
                    .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oStaffJornada.IsLoaded
        Return retval
    End Function

    Shared Function Update(oStaffJornada As DTOStaffJornada, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oStaffJornada, oTrans)
            oTrans.Commit()
            oStaffJornada.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oStaffJornada As DTOStaffJornada, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM StaffJornada ")
        sb.AppendLine("WHERE Guid='" & oStaffJornada.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oStaffJornada.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oStaffJornada
            oRow("Staff") = .Staff.Guid
            oRow("FchFrom") = .FchFrom
            oRow("FchTo") = .FchTo
            oRow("Cod") = .Cod
            oRow("Obs") = SQLHelper.NullableString(.Obs)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oStaffJornada As DTOStaffJornada, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oStaffJornada, oTrans)
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


    Shared Sub Delete(oStaffJornada As DTOStaffJornada, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE StaffJornada WHERE Guid='" & oStaffJornada.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class StaffJornadasLoader

    Shared Function All(oStaff As DTOStaff, year As Integer, month As Integer) As List(Of DTOStaffJornada)
        Dim retval As New List(Of DTOStaffJornada)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT StaffJornada.Guid, StaffJornada.FchFrom, StaffJornada.FchTo, StaffJornada.Cod, StaffJornada.Obs ")
        sb.AppendLine("FROM StaffJornada ")
        sb.AppendLine("WHERE StaffJornada.Staff = '" & oStaff.Guid.ToString & "' ")
        sb.AppendLine("AND YEAR(StaffJornada.FchFrom) = " & year & " ")
        sb.AppendLine("AND MONTH(StaffJornada.FchFrom) = " & month & " ")
        sb.AppendLine("ORDER BY StaffJornada.FchFrom")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOStaffJornada(oDrd("Guid"))
            With item
                .Staff = oStaff
                .FchFrom = oDrd("FchFrom")
                .FchTo = oDrd("FchTo")
                .Cod = oDrd("Cod")
                .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class

