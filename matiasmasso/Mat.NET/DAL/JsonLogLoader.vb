Public Class JsonLogLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOJsonLog
        Dim retval As DTOJsonLog = Nothing
        Dim oJsonLog As New DTOJsonLog(oGuid)
        If Load(oJsonLog) Then
            retval = oJsonLog
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oJsonLog As DTOJsonLog) As Boolean
        If Not oJsonLog.IsLoaded And Not oJsonLog.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM JsonLog ")
            sb.AppendLine("WHERE Guid='" & oJsonLog.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oJsonLog
                    .FchCreated = oDrd("FchCreated")
                    .Json = oDrd("Json")
                    .Result = oDrd("Result")
                    If Not IsDBNull(oDrd("Schema")) Then
                        .Schema = New DTOJsonSchema(oDrd("Schema"))
                    End If
                    .ResultTarget = SQLHelper.GetBaseGuidFromDataReader(oDrd("ResultTarget"))
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oJsonLog.IsLoaded
        Return retval
    End Function

    Shared Function Update(oJsonLog As DTOJsonLog, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oJsonLog, oTrans)
            oTrans.Commit()
            oJsonLog.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oJsonLog As DTOJsonLog, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM JsonLog ")
        sb.AppendLine("WHERE Guid='" & oJsonLog.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oJsonLog.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oJsonLog
            oRow("Json") = .Formatted(False)
            oRow("Result") = .Result
            oRow("ResultTarget") = SQLHelper.NullableBaseGuid(.ResultTarget)
            oRow("Schema") = SQLHelper.NullableBaseGuid(.Schema)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oJsonLog As DTOJsonLog, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oJsonLog, oTrans)
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


    Shared Sub Delete(oJsonLog As DTOJsonLog, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE JsonLog WHERE Guid='" & oJsonLog.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class JsonLogsLoader

    Shared Function All(Optional schemaNom As String = "", Optional includeStream As Boolean = False, Optional searchkey As String = "") As List(Of DTOJsonLog)
        Dim retval As New List(Of DTOJsonLog)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT JsonLog.Guid, JsonLog.Result, JsonLog.ResultTarget, JsonLog.FchCreated ")
        If includeStream Then
            sb.AppendLine(", JsonLog.Json ")
        Else
            sb.AppendLine(", LEN(CAST(JsonLog.Json AS VARCHAR(MAX))) AS Length ")
        End If
        sb.AppendLine(", JsonLog.[Schema] AS SchemaGuid , JsonSchema.Nom ")
        sb.AppendLine("FROM JsonLog ")
        sb.AppendLine("LEFT OUTER JOIN JsonSchema ON JsonLog.[Schema] = JsonSchema.Guid ")

        Dim conditions As New List(Of String)
        If schemaNom.isNotEmpty Then
            conditions.Add("JsonSchema.Nom = '" & schemaNom & "' ")
        End If

        If searchkey.isNotEmpty Then
            conditions.Add("CAST(JsonLog.Json AS VARCHAR(MAX)) LIKE '%" & searchkey & "%' ")
        End If

        For Each condition In conditions
            sb.Append(IIf(condition = conditions.First, "WHERE ", "AND "))
            sb.AppendLine(condition)
        Next

        sb.AppendLine("ORDER BY FchCreated DESC ")
        Dim SQL = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOJsonLog(oDrd("Guid"))
            Try
                With item
                    .FchCreated = oDrd("Fchcreated")
                    .Result = oDrd("Result")
                    .ResultTarget = SQLHelper.GetBaseGuidFromDataReader(oDrd("ResultTarget"))
                    If Not IsDBNull(oDrd("SchemaGuid")) Then
                        .Schema = New DTOJsonSchema(oDrd("SchemaGuid"))
                        .Schema.Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                    End If
                    If includeStream Then
                        .Json = oDrd("Json")
                    Else
                        .Length = oDrd("Length")
                    End If
                End With
            Catch ex As Exception
                'Stop
            End Try
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
