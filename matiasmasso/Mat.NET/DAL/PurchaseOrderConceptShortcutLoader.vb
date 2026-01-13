Public Class PurchaseOrderConceptShortcutLoader


    Shared Function Find(oGuid As Guid) As DTOPurchaseOrder.ConceptShortcut
        Dim retval As DTOPurchaseOrder.ConceptShortcut = Nothing
        Dim value As New DTOPurchaseOrder.ConceptShortcut(oGuid)
        If Load(value) Then
            retval = value
        End If
        Return retval
    End Function

    Shared Function Load(ByRef value As DTOPurchaseOrder.ConceptShortcut) As Boolean
        If Not value.IsLoaded And Not value.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Pdd.Src, Pdd.Searchkey ")
            sb.AppendLine(", VwLangText.Esp, VwLangText.Cat, VwLangText.Eng, VwLangText.Por ")
            sb.AppendLine("FROM Pdd ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText ON Pdd.Guid = VwLangText.Guid ")
            sb.AppendLine("WHERE Pdd.Guid='" & value.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With value
                    .Searchkey = oDrd("Searchkey")
                    .Src = oDrd("Src")
                    SQLHelper.LoadLangTextFromDataReader(.Concept, oDrd)
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = value.IsLoaded
        Return retval
    End Function

    Shared Function Update(value As DTOPurchaseOrder.ConceptShortcut, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(value, oTrans)
            LangTextLoader.Update(value.Concept, oTrans)
            oTrans.Commit()
            value.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(value As DTOPurchaseOrder.ConceptShortcut, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Pdd ")
        sb.AppendLine("WHERE Guid='" & value.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = value.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With value
            oRow("Searchkey") = .Searchkey
            oRow("Src") = .Src
            .IsLoaded = True
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(value As DTOPurchaseOrder.ConceptShortcut, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            LangTextLoader.Delete(value.Concept, oTrans)
            Delete(value, oTrans)
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


    Shared Sub Delete(value As DTOPurchaseOrder.ConceptShortcut, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Pdd WHERE Guid='" & value.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

End Class

Public Class PurchaseOrderConceptShortcutsLoader

    Shared Function All() As List(Of DTOPurchaseOrder.ConceptShortcut)
        Dim retval As New List(Of DTOPurchaseOrder.ConceptShortcut)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Pdd.Guid, Pdd.Searchkey, Pdd.Src ")
        sb.AppendLine(", VwLangText.Esp, VwLangText.Cat, VwLangText.Eng, VwLangText.Por ")
        sb.AppendLine("FROM Pdd ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText ON Pdd.Guid = VwLangText.Guid ")
        sb.AppendLine("ORDER BY Pdd.Searchkey")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOPurchaseOrder.ConceptShortcut(oDrd("Guid"))
            With item
                .Searchkey = oDrd("Searchkey")
                .Src = oDrd("Src")
                SQLHelper.LoadLangTextFromDataReader(.Concept, oDrd)
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class
