Public Class InventariItemLoader

    Shared Function Find(oGuid As Guid) As DTOImmoble.InventariItem
        Dim retval As DTOImmoble.InventariItem = Nothing
        Dim oItem As New DTOImmoble.InventariItem(oGuid)
        If Load(oItem) Then
            retval = oItem
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oItem As DTOImmoble.InventariItem) As Boolean
        If Not oItem.IsLoaded And Not oItem.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Immoble.Guid AS ImmobleGuid, Immoble.Nom AS ImmobleNom ")
            sb.AppendLine(", InventariItems.* ")
            sb.AppendLine("FROM InventariItems ")
            sb.AppendLine("INNER JOIN Immoble ON InventariItems.Immoble = Immoble.Guid ")
            sb.AppendLine("WHERE InventariItems.Guid='" & oItem.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oItem
                    .Immoble = New DTOImmoble(oDrd("Immoble"))
                    .Nom = oDrd("Nom")
                    .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oItem.IsLoaded
        Return retval
    End Function

    Shared Function Update(oItem As DTOImmoble.InventariItem, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oItem, oTrans)
            oTrans.Commit()
            oItem.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oItem As DTOImmoble.InventariItem, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM InventariItems ")
        sb.AppendLine("WHERE Guid='" & oItem.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oItem.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oItem
            oRow("Immoble") = .Immoble.Guid
            oRow("Nom") = .Nom
            oRow("Obs") = SQLHelper.NullableString(.Obs)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oItem As DTOImmoble.InventariItem, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oItem, oTrans)
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


    Shared Sub Delete(oItem As DTOImmoble.InventariItem, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE InventariItems WHERE Guid='" & oItem.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

End Class

Public Class InventariItemsLoader

    Shared Function All(oImmoble As DTOImmoble) As DTOImmoble.InventariItem.Collection
        Dim retval As New DTOImmoble.InventariItem.Collection
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM InventariItems ")
        sb.AppendLine("WHERE Immoble = '" & oImmoble.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY Nom")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOImmoble.InventariItem(oDrd("Guid"))
            With item
                .Nom = oDrd("Nom")
                .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class

