Public Class CustomerPlatformloader

    Shared Function Find(oGuid As Guid) As DTOCustomerPlatform
        Dim retval As DTOCustomerPlatform = Nothing
        Dim oCustomerPlatform As New DTOCustomerPlatform(oGuid)
        If Load(oCustomerPlatform) Then
            retval = oCustomerPlatform
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oCustomerPlatform As DTOCustomerPlatform) As Boolean
        Dim retval As Boolean
        Dim SQL As String = "SELECT * FROM CustomerPlatform CP " _
                            & "LEFT OUTER JOIN CliAdr ON CP.Guid=CliAdr.SrcGuid AND CliAdr.Cod=1 " _
                            & "LEFT OUTER JOIN Zip ON Zip.Guid=CliAdr.Zip " _
                            & "LEFT OUTER JOIN Location ON Location.Guid=Zip.Location " _
                            & "INNER JOIN CustomerPlatformDestination CD ON CP.Guid=CD.Platform " _
                            & "WHERE CP.Guid='" & oCustomerPlatform.Guid.ToString & "' "
        Dim FirstRec As Boolean
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not FirstRec Then
                oCustomerPlatform.Customer = New DTOContact(oDrd("Customer"))
                FirstRec = False
            End If
            Dim oDestination As New DTOPlatformDestination(oDrd("Destination"))
            oCustomerPlatform.Destinations.Add(oDestination)
            retval = True
        Loop

        oDrd.Close()

        Return retval
    End Function

    Shared Function Update(oCustomerPlatform As DTOCustomerPlatform, ByRef exs As list(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Update(oCustomerPlatform, oTrans)
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


    Shared Sub Update(oCustomerPlatform As DTOCustomerPlatform, ByRef oTrans As SqlTransaction)
        UpdateHeader(oCustomerPlatform, oTrans)
        UpdateItems(oCustomerPlatform, oTrans)
    End Sub

    Shared Sub UpdateHeader(oCustomerPlatform As DTOCustomerPlatform, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM CustomerPlatform WHERE Guid=@Guid"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@GUID", oCustomerPlatform.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oCustomerPlatform.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oCustomerPlatform
            oRow("Customer") = .Customer.Guid
        End With

        oDA.Update(oDs)
    End Sub


    Shared Sub UpdateItems(oCustomerPlatform As DTOCustomerPlatform, ByRef oTrans As SqlTransaction)
        DeleteItems(oCustomerPlatform, oTrans)

        Dim SQL As String = "SELECT * FROM CustomerPlatformDestination WHERE Platform=@Guid"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oCustomerPlatform.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each oItem As DTOPlatformDestination In oCustomerPlatform.Destinations
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Platform") = oCustomerPlatform.Guid
            oRow("Destination") = oItem.Guid
        Next

        oDA.Update(oDs)
    End Sub


    Shared Function Delete(oCustomerPlatform As DTOCustomerPlatform, ByRef exs As list(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oCustomerPlatform, oTrans)
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


    Shared Sub Delete(oCustomerPlatform As DTOCustomerPlatform, ByRef oTrans As SqlTransaction)
        DeleteItems(oCustomerPlatform, oTrans)
        DeleteHeader(oCustomerPlatform, oTrans)
    End Sub

    Shared Sub DeleteHeader(oCustomerPlatform As DTOCustomerPlatform, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE CustomerPlatform WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oCustomerPlatform.Guid.ToString())
    End Sub

    Shared Sub DeleteItems(oCustomerPlatform As DTOCustomerPlatform, ByRef oTrans As SqlTransaction)
        With oCustomerPlatform
            Dim SQL As String = "DELETE CustomerPlatformDestination WHERE Platform=@Guid"
            SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oCustomerPlatform.Guid.ToString())
        End With
    End Sub

End Class

Public Class CustomerPlatformsLoader
    Shared Function All(oParent As DTOContact) As List(Of DTOCustomerPlatform)
        Dim retval As New List(Of DTOCustomerPlatform)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CustomerPlatform.Guid ")
        sb.AppendLine(", CliGral.Gln, CliClient.Ref ")
        sb.AppendLine("FROM CustomerPlatform ")
        sb.AppendLine("INNER JOIN CliGral ON CustomerPlatform.Guid = CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliClient ON CliGral.Guid = CliClient.Guid ")
        sb.AppendLine("WHERE CustomerPlatform.Customer='" & oParent.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOCustomerPlatform(oDrd("Guid"))
            With item
                If Not IsDBNull(oDrd("GLN")) Then
                    .gln = DTOEan.Factory(oDrd("GLN"))
                End If
                .Nom = SQLHelper.GetStringFromDataReader(oDrd("Ref"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class


