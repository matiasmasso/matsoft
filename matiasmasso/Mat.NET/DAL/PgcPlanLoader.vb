Public Class PgcPlanLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOPgcPlan
        Dim retval As DTOPgcPlan = Nothing
        Dim oPgcPlan As New DTOPgcPlan(oGuid)
        If Load(oPgcPlan) Then
            retval = oPgcPlan
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oPgcPlan As DTOPgcPlan) As Boolean
        If Not oPgcPlan.IsLoaded And Not oPgcPlan.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM PgcPlan ")
            sb.AppendLine("WHERE Guid='" & oPgcPlan.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oPgcPlan
                    .Nom = oDrd("Nom")
                    .YearFrom = SQLHelper.GetIntegerFromDataReader(oDrd("YearFrom"))
                    .YearTo = SQLHelper.GetIntegerFromDataReader(oDrd("YearTo"))
                    If Not IsDBNull(oDrd("LastPlan")) Then
                        .LastPlan = New DTOPgcPlan(oDrd("LastPlan"))
                    End If
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oPgcPlan.IsLoaded
        Return retval
    End Function

    Shared Function Update(oPgcPlan As DTOPgcPlan, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oPgcPlan, oTrans)
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


    Shared Sub Update(oPgcPlan As DTOPgcPlan, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM PgcPlan ")
        sb.AppendLine("WHERE Guid='" & oPgcPlan.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oPgcPlan.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oPgcPlan
            oRow("Nom") = .Nom
            oRow("YearFrom") = SQLHelper.NullableInt(.YearFrom)
            oRow("YearTo") = SQLHelper.NullableInt(.YearTo)
            oRow("LastPlan") = SQLHelper.NullableBaseGuid(.LastPlan)
        End With

        oDA.Update(oDs)
    End Sub


    Shared Function Delete(oPgcPlan As DTOPgcPlan, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oPgcPlan, oTrans)
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


    Shared Sub Delete(oPgcPlan As DTOPgcPlan, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE PgcPlan WHERE Guid='" & oPgcPlan.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region


    Shared Function FromYear(iYear As Integer) As DTOPgcPlan
        Dim retval As DTOPgcPlan = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("Select * ")
        sb.AppendLine("FROM PgcPlan ")
        sb.AppendLine("WHERE (YearFrom<= " & iYear & " Or YearFrom Is NULL) ")
        sb.AppendLine("And (YearTo>= " & iYear & " Or YearTo Is NULL) ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOPgcPlan(oDrd("Guid"))
            With retval
                .Nom = oDrd("Nom")
                .YearFrom = SQLHelper.GetIntegerFromDataReader(oDrd("YearFrom"))
                .YearTo = SQLHelper.GetIntegerFromDataReader(oDrd("YearTo"))
                .LastPlan = New DTOPgcPlan(oDrd("LastPlan"))
            End With
        End If
        oDrd.Close()
        Return retval
    End Function
End Class

Public Class PgcPlansLoader
    Shared Function All() As List(Of DTOPgcPlan)
        Dim retval As New List(Of DTOPgcPlan)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM PgcPlan ")
        sb.AppendLine("ORDER BY YearFrom DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOPgcPlan(oDrd("Guid"))
            With item
                .Nom = oDrd("Nom")
                .YearFrom = SQLHelper.GetIntegerFromDataReader(oDrd("YearFrom"))
                .YearTo = SQLHelper.GetIntegerFromDataReader(oDrd("YearTo"))
                If Not IsDBNull(oDrd("LastPlan")) Then
                    .LastPlan = New DTOPgcPlan(oDrd("LastPlan"))
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class