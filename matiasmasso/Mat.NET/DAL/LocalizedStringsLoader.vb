Public Class LocalizedStringLoader


    Shared Function Find(oGuid As Guid) As DTOLocalizedString
        Dim retval As DTOLocalizedString = Nothing
        Dim oLocalizedString As New DTOLocalizedString(oGuid)
        If Load(oLocalizedString) Then
            retval = oLocalizedString
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oLocalizedString As DTOLocalizedString) As Boolean
        If Not oLocalizedString.IsLoaded And Not oLocalizedString.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Guid, [Key], Locale, Value ")
            sb.AppendLine("FROM LocalizedString ")
            sb.AppendLine("WHERE Guid='" & oLocalizedString.Guid.ToString & "' ")
            sb.AppendLine("ORDER BY [Key], Locale")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                If Not oLocalizedString.IsLoaded Then
                    With oLocalizedString
                        .key = oDrd("Key")
                        .items = New List(Of DTOLocalizedString.item)
                        .IsLoaded = True
                    End With
                End If
                Dim item As New DTOLocalizedString.item
                With item
                    .locale = oDrd("Locale")
                    .value = SQLHelper.GetStringFromDataReader(oDrd("Value"))
                End With
                oLocalizedString.items.Add(item)
            Loop

            oDrd.Close()
        End If

        Dim retval As Boolean = oLocalizedString.IsLoaded
        Return retval
    End Function

    Shared Function Update(oLocalizedString As DTOLocalizedString, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Update(oLocalizedString, oTrans)
            oTrans.Commit()
            oLocalizedString.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oLocalizedString As DTOLocalizedString, ByRef oTrans As SqlTransaction)
        Delete(oLocalizedString, oTrans)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM LocalizedString ")
        sb.AppendLine("WHERE Guid='" & oLocalizedString.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        For Each item In oLocalizedString.items
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oLocalizedString.Guid
            oRow("Key") = oLocalizedString.key
            oRow("Locale") = item.locale
            oRow("Value") = SQLHelper.NullableString(item.value)
        Next
        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oLocalizedString As DTOLocalizedString, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oLocalizedString, oTrans)
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


    Shared Sub Delete(oLocalizedString As DTOLocalizedString, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE LocalizedString WHERE Guid='" & oLocalizedString.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

End Class

Public Class LocalizedStringsLoader
    Shared Function All() As List(Of DTOLocalizedString)
        Dim retval As New List(Of DTOLocalizedString)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Guid, [Key], Locale, Value ")
        sb.AppendLine("FROM LocalizedString ")
        sb.AppendLine("ORDER BY [Key], Locale")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oLocalizedString As New DTOLocalizedString
        Do While oDrd.Read
            If Not oLocalizedString.Guid.Equals(oDrd("Guid")) Then
                oLocalizedString = New DTOLocalizedString(oDrd("Guid"))
                oLocalizedString.key = oDrd("Key")
            End If
            Dim item As New DTOLocalizedString.item
            With item
                .locale = oDrd("Locale")
                .value = SQLHelper.GetStringFromDataReader(oDrd("Value"))
            End With
            oLocalizedString.items.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class
