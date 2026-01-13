Public Class ComputerLoader


    Shared Function Find(oGuid As Guid) As DTOComputer
        Dim retval As DTOComputer = Nothing
        Dim oComputer As New DTOComputer(oGuid)
        If Load(oComputer) Then
            retval = oComputer
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oComputer As DTOComputer) As Boolean
        If Not oComputer.IsLoaded And Not oComputer.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM Computer ")
            sb.AppendLine("WHERE Guid='" & oComputer.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oComputer
                    .Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                    .FchFrom = SQLHelper.GetFchFromDataReader(oDrd("FchFrom"))
                    .FchTo = SQLHelper.GetFchFromDataReader(oDrd("FchTo"))
                    .Text = SQLHelper.GetStringFromDataReader(oDrd("Text"))
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oComputer.IsLoaded
        Return retval
    End Function

    Shared Function Update(oComputer As DTOComputer, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oComputer, oTrans)
            oTrans.Commit()
            oComputer.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oComputer As DTOComputer, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Computer ")
        sb.AppendLine("WHERE Guid='" & oComputer.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oComputer.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oComputer
            oRow("Nom") = SQLHelper.NullableString(.Nom)
            oRow("Text") = SQLHelper.NullableString(.Text)
            oRow("FchFrom") = SQLHelper.NullableFch(.FchFrom)
            oRow("FchTo") = SQLHelper.NullableFch(.FchTo)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oComputer As DTOComputer, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oComputer, oTrans)
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


    Shared Sub Delete(oComputer As DTOComputer, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Computer WHERE Guid='" & oComputer.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

End Class

Public Class ComputersLoader

    Shared Function All() As List(Of DTOComputer)
        Dim retval As New List(Of DTOComputer)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Computer ")
        sb.AppendLine("ORDER BY Nom")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOComputer(oDrd("Guid"))
            With item
                .Nom = oDrd("Nom")
                .FchFrom = SQLHelper.GetFchFromDataReader(oDrd("FchFrom"))
                .FchTo = SQLHelper.GetFchFromDataReader(oDrd("FchTo"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class

