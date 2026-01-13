Public Class WinBugLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOWinBug
        Dim retval As DTOWinBug = Nothing
        Dim oWinBug As New DTOWinBug(oGuid)
        If Load(oWinBug) Then
            retval = oWinBug
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oWinBug As DTOWinBug) As Boolean
        If Not oWinBug.IsLoaded And Not oWinBug.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM WinBug ")
            sb.AppendLine("WHERE Guid='" & oWinBug.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oWinBug
                    .Fch = oDrd("Fch")
                    .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oWinBug.IsLoaded
        Return retval
    End Function

    Shared Function Update(oWinBug As DTOWinBug, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oWinBug, oTrans)
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


    Shared Sub Update(oWinBug As DTOWinBug, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM WinBug ")
        sb.AppendLine("WHERE Guid='" & oWinBug.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oWinBug.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oWinBug
            If .Fch <> Nothing Then oRow("Fch") = .Fch
            oRow("Obs") = SQLHelper.NullableString(.Obs)
            oRow("User") = SQLHelper.NullableBaseGuid(.User)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oWinBug As DTOWinBug, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oWinBug, oTrans)
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


    Shared Sub Delete(oWinBug As DTOWinBug, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE WinBug WHERE Guid='" & oWinBug.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class WinBugsLoader

    Shared Function All() As List(Of DTOWinBug)
        Dim retval As New List(Of DTOWinBug)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM WinBug ")
        sb.AppendLine("ORDER BY Nom")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOWinBug(oDrd("Guid"))
            With item
                .Fch = oDrd("Fch")
                .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Delete(oWinBugs As List(Of DTOWinBug), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = WinBugsLoader.Delete(oWinBugs, exs)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE WinBug ")
        sb.AppendLine("WHERE (")
        For Each item As DTOWinBug In oWinBugs
            If item.UnEquals(oWinBugs.First) Then
                sb.Append("OR ")
            End If
            sb.Append("Guid='" & item.Guid.ToString & "' ")
        Next
        sb.AppendLine(")")
        Return retval
    End Function

End Class

