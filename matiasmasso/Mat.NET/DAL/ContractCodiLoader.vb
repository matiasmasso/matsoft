Public Class ContractCodiLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOContractCodi
        Dim retval As DTOContractCodi = Nothing
        Dim oContractCodi As New DTOContractCodi(oGuid)
        If Load(oContractCodi) Then
            retval = oContractCodi
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oContractCodi As DTOContractCodi) As Boolean
        If Not oContractCodi.IsLoaded And Not oContractCodi.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM Contract_Codis ")
            sb.AppendLine("WHERE Guid='" & oContractCodi.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oContractCodi
                    .Nom = oDrd("Nom")
                    .Amortitzable = oDrd("Amortitzable")
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oContractCodi.IsLoaded
        Return retval
    End Function

    Shared Function Update(oContractCodi As DTOContractCodi, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oContractCodi, oTrans)
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


    Shared Sub Update(oContractCodi As DTOContractCodi, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Contract_Codis ")
        sb.AppendLine("WHERE Guid='" & oContractCodi.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oContractCodi.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oContractCodi
            oRow("Nom") = .Nom
            oRow("Amortitzable") = .Amortitzable
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oContractCodi As DTOContractCodi, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oContractCodi, oTrans)
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


    Shared Sub Delete(oContractCodi As DTOContractCodi, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Contract_Codis WHERE Guid='" & oContractCodi.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class ContractCodisLoader

    Shared Function All() As List(Of DTOContractCodi)
        Dim retval As New List(Of DTOContractCodi)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Contract_Codis ")
        sb.AppendLine("ORDER BY Nom")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOContractCodi(oDrd("Guid"))
            With item
                .Nom = oDrd("Nom")
                .Amortitzable = oDrd("Amortitzable")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Delete(oContractCodis As List(Of DTOContractCodi), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ContractCodisLoader.Delete(oContractCodis, exs)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE Contract_Codis ")
        sb.AppendLine("WHERE (")
        For Each item As DTOContractCodi In oContractCodis
            If item.UnEquals(oContractCodis.First) Then
                sb.Append("OR ")
            End If
            sb.Append("Guid='" & item.Guid.ToString & "' ")
        Next
        sb.AppendLine(")")
        Return retval
    End Function

End Class
