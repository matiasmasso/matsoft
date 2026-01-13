Public Class CustomerClusterLoader



    Shared Function Find(oGuid As Guid) As DTOCustomerCluster
        Dim retval As DTOCustomerCluster = Nothing
        Dim oCustomerCluster As New DTOCustomerCluster(oGuid)
        If Load(oCustomerCluster) Then
            retval = oCustomerCluster
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oCustomerCluster As DTOCustomerCluster) As Boolean
        If Not oCustomerCluster.IsLoaded And Not oCustomerCluster.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT CustomerCluster.Holding, CustomerCluster.Nom, CustomerCluster.Obs ")
            sb.AppendLine(", Holding.Nom AS HoldingNom ")
            sb.AppendLine("FROM CustomerCluster ")
            sb.AppendLine("LEFT OUTER JOIN Holding ON CustomerCluster.Holding = Holding.Guid ")
            sb.AppendLine("WHERE CustomerCluster.Guid='" & oCustomerCluster.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oCustomerCluster
                    .Holding = New DTOHolding(oDrd("Holding"))
                    .Holding.Nom = SQLHelper.GetStringFromDataReader(oDrd("HoldingNom"))
                    .Nom = oDrd("Nom")
                    .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oCustomerCluster.IsLoaded
        Return retval
    End Function

    Shared Function Update(oCustomerCluster As DTOCustomerCluster, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oCustomerCluster, oTrans)
            oTrans.Commit()
            oCustomerCluster.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oCustomerCluster As DTOCustomerCluster, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM CustomerCluster ")
        sb.AppendLine("WHERE Guid='" & oCustomerCluster.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oCustomerCluster.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oCustomerCluster
            oRow("Holding") = .Holding.Guid
            oRow("Nom") = .Nom
            oRow("Obs") = SQLHelper.NullableString(.Obs)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oCustomerCluster As DTOCustomerCluster, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oCustomerCluster, oTrans)
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


    Shared Sub Delete(oCustomerCluster As DTOCustomerCluster, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE CustomerCluster WHERE Guid='" & oCustomerCluster.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub


    Shared Function Children(oCustomerCluster As DTOCustomerCluster) As List(Of DTOCustomer)
        Dim retval As New List(Of DTOCustomer)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CliClient.Guid, CliGral.FullNom, CliGral.Obsoleto ")
        sb.AppendLine("FROM CliClient ")
        sb.AppendLine("INNER JOIN CliGral ON CliClient.Guid = CliGral.Guid ")
        sb.AppendLine("WHERE CliClient.CustomerCluster = '" & oCustomerCluster.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY CliGral.FullNom")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOCustomer(oDrd("Guid"))
            With item
                .FullNom = oDrd("FullNom")
                .Obsoleto = oDrd("Obsoleto")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function



End Class

Public Class CustomerClustersLoader

    Shared Function All(oHolding As DTOHolding) As List(Of DTOCustomerCluster)
        Dim retval As New List(Of DTOCustomerCluster)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CustomerCluster.Guid, CustomerCluster.Nom ")
        sb.AppendLine("FROM CustomerCluster ")
        sb.AppendLine("WHERE CustomerCluster.Holding = '" & oHolding.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY CustomerCluster.Nom ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOCustomerCluster(oDrd("Guid"))
            With item
                .Holding = oHolding
                .Nom = oDrd("Nom")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class

