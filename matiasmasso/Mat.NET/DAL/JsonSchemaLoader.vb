Public Class JsonSchemaLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOJsonSchema
        Dim retval As DTOJsonSchema = Nothing
        Dim oJsonSchema As New DTOJsonSchema(oGuid)
        If Load(oJsonSchema) Then
            retval = oJsonSchema
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oJsonSchema As DTOJsonSchema) As Boolean
        If Not oJsonSchema.IsLoaded And Not oJsonSchema.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT JsonSchema.Nom, JsonSchema.Json ")
            sb.AppendLine(", JsonSchema.FchCreated, JsonSchema.UsrCreated ")
            sb.AppendLine(", JsonSchema.FchLastEdited, JsonSchema.UsrLastEdited ")
            sb.AppendLine(", UsrCreated.Adr AS UsrCreatedEmailAddress, UsrCreated.Nickname AS UsrCreatedNickname ")
            sb.AppendLine(", UsrLastEdited.Adr AS UsrLastEditedEmailAddress, UsrLastEdited.Nickname AS UsrLastEditedNickname ")
            sb.AppendLine("FROM JsonSchema ")
            sb.AppendLine("LEFT OUTER JOIN Email AS UsrCreated ON JsonSchema.UsrCreated = UsrCreated.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Email AS UsrLastEdited ON JsonSchema.UsrLastEdited = UsrLastEdited.Guid ")
            sb.AppendLine("WHERE JsonSchema.Guid='" & oJsonSchema.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oJsonSchema
                    .Nom = oDrd("Nom")
                    .Json = SQLHelper.GetStringFromDataReader(oDrd("Json"))
                    .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd)
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oJsonSchema.IsLoaded
        Return retval
    End Function

    Shared Function Update(oJsonSchema As DTOJsonSchema, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oJsonSchema, oTrans)
            oTrans.Commit()
            oJsonSchema.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oJsonSchema As DTOJsonSchema, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM JsonSchema ")
        sb.AppendLine("WHERE Guid='" & oJsonSchema.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oJsonSchema.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oJsonSchema
            oRow("Nom") = .Nom
            oRow("Json") = .Json
            SQLHelper.SetUsrLog(.UsrLog, oRow)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oJsonSchema As DTOJsonSchema, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oJsonSchema, oTrans)
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


    Shared Sub Delete(oJsonSchema As DTOJsonSchema, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE JsonSchema WHERE Guid='" & oJsonSchema.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class JsonSchemasLoader

    Shared Function All() As List(Of DTOJsonSchema)
        Dim retval As New List(Of DTOJsonSchema)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Guid, Nom, Json ")
        sb.AppendLine("FROM JsonSchema ")
        sb.AppendLine("ORDER BY Nom")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOJsonSchema(oDrd("Guid"))
            With item
                .Nom = oDrd("Nom")
                .Json = SQLHelper.GetStringFromDataReader(oDrd("Json"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class
