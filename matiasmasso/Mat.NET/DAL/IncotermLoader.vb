Public Class IncotermLoader

    Shared Function Find(id As String) As DTOIncoterm
        Dim retval As DTOIncoterm = Nothing
        Dim oIncoterm = DTOIncoterm.Factory(id)
        If Load(oIncoterm) Then
            retval = oIncoterm
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oIncoterm As DTOIncoterm) As Boolean
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Incoterm ")
        sb.AppendLine("WHERE Id='" & oIncoterm.Id & "' ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            With oIncoterm
                .Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
            End With
        End If

        oDrd.Close()

        Dim retval As Boolean = True
        Return retval
    End Function

    Shared Function Update(oIncoterm As DTOIncoterm, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oIncoterm, oTrans)
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


    Shared Sub Update(oIncoterm As DTOIncoterm, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Incoterm ")
        sb.AppendLine("WHERE Id='" & oIncoterm.Id & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Id") = oIncoterm.Id
        Else
            oRow = oTb.Rows(0)
        End If

        With oIncoterm
            oRow("Nom") = SQLHelper.NullableString(.Nom)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oIncoterm As DTOIncoterm, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oIncoterm, oTrans)
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


    Shared Sub Delete(oIncoterm As DTOIncoterm, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Incoterm WHERE Id='" & oIncoterm.Id & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

End Class

Public Class IncotermsLoader

    Shared Function All() As List(Of DTOIncoterm)
        Dim retval As New List(Of DTOIncoterm)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Incoterm ")
        sb.AppendLine("ORDER BY Nom")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item = DTOIncoterm.Factory(oDrd("Id"))
            With item
                .Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class
