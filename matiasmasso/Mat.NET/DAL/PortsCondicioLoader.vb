Public Class PortsCondicioLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOPortsCondicio
        Dim retval As DTOPortsCondicio = Nothing
        Dim oPortsCondicio As New DTOPortsCondicio(oGuid)
        If Load(oPortsCondicio) Then
            retval = oPortsCondicio
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oPortsCondicio As DTOPortsCondicio) As Boolean
        Dim retval As Boolean
        If oPortsCondicio IsNot Nothing Then
            If Not oPortsCondicio.IsLoaded And Not oPortsCondicio.IsNew Then

                Dim sb As New System.Text.StringBuilder
                sb.AppendLine("SELECT * ")
                sb.AppendLine("FROM PortsCondicions ")
                sb.AppendLine("WHERE Guid='" & oPortsCondicio.Guid.ToString & "' ")

                Dim SQL As String = sb.ToString
                Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
                If oDrd.Read Then
                    With oPortsCondicio
                        .Nom = oDrd("Nom")
                        .Cod = oDrd("Cod")
                        .PdcMinVal = SQLHelper.GetAmtFromDataReader(oDrd("PdcMinVal"))
                        .Fee = SQLHelper.GetAmtFromDataReader(oDrd("Fee"))
                        .IsLoaded = True
                    End With
                End If

                oDrd.Close()
                retval = oPortsCondicio.IsLoaded
            End If
        End If

        Return retval
    End Function

    Shared Function Update(oPortsCondicio As DTOPortsCondicio, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oPortsCondicio, oTrans)
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


    Shared Sub Update(oPortsCondicio As DTOPortsCondicio, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM PortsCondicions ")
        sb.AppendLine("WHERE Guid='" & oPortsCondicio.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oPortsCondicio.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oPortsCondicio
            oRow("Nom") = .Nom
            oRow("PdcMinVal") = SQLHelper.NullableAmt(.PdcMinVal)
            oRow("Cod") = .Cod
            oRow("Fee") = SQLHelper.NullableAmt(.Fee)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oPortsCondicio As DTOPortsCondicio, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oPortsCondicio, oTrans)
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

    Shared Sub Delete(oPortsCondicio As DTOPortsCondicio, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE PortsCondicions ")
        sb.AppendLine("WHERE Guid='" & oPortsCondicio.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

    Shared Function Customers(oPortsCondicio As DTOPortsCondicio) As List(Of DTOGuidNom.Compact)
        Dim retval As New List(Of DTOGuidNom.Compact)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CliClient.Guid, CliGral.FullNom ")
        sb.AppendLine("FROM CliClient ")
        sb.AppendLine("INNER JOIN CliGral ON CliClient.Guid = CliGral.Guid ")
        sb.AppendLine("WHERE PortsCondicions='" & oPortsCondicio.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY CliGral.FullNom")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOGuidNom.Compact()
            With item
                .Guid = oDrd("Guid")
                .Nom = oDrd("FullNom")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class

Public Class PortsCondicionsLoader

    Shared Function All() As List(Of DTOPortsCondicio)
        Dim retval As New List(Of DTOPortsCondicio)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM PortsCondicions ")
        sb.AppendLine("ORDER BY Ord")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOPortsCondicio(oDrd("Guid"))
            With item
                .Nom = oDrd("Nom")
                .Cod = oDrd("Cod")
                .PdcMinVal = SQLHelper.GetAmtFromDataReader(oDrd("PdcMinVal"))
                .Fee = SQLHelper.GetAmtFromDataReader(oDrd("Fee"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class

