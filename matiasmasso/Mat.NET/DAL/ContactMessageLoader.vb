Public Class ContactMessageLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOContactMessage
        Dim retval As DTOContactMessage = Nothing
        Dim oContactMessage As New DTOContactMessage(oGuid)
        If Load(oContactMessage) Then
            retval = oContactMessage
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oContactMessage As DTOContactMessage) As Boolean
        If Not oContactMessage.IsLoaded And Not oContactMessage.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM ContactMessage ")
            sb.AppendLine("WHERE Guid='" & oContactMessage.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oContactMessage
                    .Email = oDrd("Email")
                    .Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                    .Location = SQLHelper.GetStringFromDataReader(oDrd("Location"))
                    .Text = SQLHelper.GetStringFromDataReader(oDrd("Text"))
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oContactMessage.IsLoaded
        Return retval
    End Function

    Shared Function Update(oContactMessage As DTOContactMessage, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oContactMessage, oTrans)
            oTrans.Commit()
            oContactMessage.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oContactMessage As DTOContactMessage, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM ContactMessage ")
        sb.AppendLine("WHERE Guid='" & oContactMessage.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oContactMessage.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oContactMessage
            oRow("Email") = .Email
            oRow("Nom") = SQLHelper.NullableString(.Nom)
            oRow("Location") = SQLHelper.NullableString(.Location)
            oRow("Text") = SQLHelper.NullableString(.Text)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oContactMessage As DTOContactMessage, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oContactMessage, oTrans)
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


    Shared Sub Delete(oContactMessage As DTOContactMessage, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE ContactMessage WHERE Guid='" & oContactMessage.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class ContactMessagesLoader

    Shared Function All() As List(Of DTOContactMessage)
        Dim retval As New List(Of DTOContactMessage)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Guid, Email, FchCreated, Nom, Location ")
        sb.AppendLine("FROM ContactMessage ")
        sb.AppendLine("ORDER BY FchCreated DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oContactMessage As New DTOContactMessage(oDrd("Guid"))
            With oContactMessage
                .Email = oDrd("Email")
                .FchCreated = oDrd("FchCreated")
                .Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                .Location = SQLHelper.GetStringFromDataReader(oDrd("Location"))
            End With
            retval.Add(oContactMessage)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class
