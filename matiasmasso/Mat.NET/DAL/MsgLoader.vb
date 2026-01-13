Public Class MsgLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOMsg
        Dim retval As DTOMsg = Nothing
        Dim oMsg As New DTOMsg(oGuid)
        If Load(oMsg) Then
            retval = oMsg
        End If
        Return retval
    End Function

    Shared Function Find(Id As Integer) As DTOMsg
        Dim retval As DTOMsg = Nothing

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Msg ")
        sb.AppendLine("WHERE Id=" & Id.ToString & " ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOMsg(oDrd("Guid"))
            With retval
                .Id = oDrd("Id")
                .User = New DTOUser(DirectCast(oDrd("UsrCreated"), Guid))
                .Fch = oDrd("FchCreated")
                .Text = oDrd("Text")
                .IsLoaded = True
            End With
        End If

        oDrd.Close()
        Return retval
    End Function

    Shared Function Load(ByRef oMsg As DTOMsg) As Boolean
        If Not oMsg.IsLoaded And Not oMsg.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM Msg ")
            sb.AppendLine("WHERE Guid='" & oMsg.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oMsg
                    .Id = oDrd("Id")
                    .User = New DTOUser(DirectCast(oDrd("UsrCreated"), Guid))
                    .Fch = oDrd("FchCreated")
                    .Text = oDrd("Text")
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oMsg.IsLoaded
        Return retval
    End Function

    Shared Function Update(oMsg As DTOMsg, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oMsg, oTrans)
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


    Shared Sub Update(oMsg As DTOMsg, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Msg ")
        sb.AppendLine("WHERE Guid='" & oMsg.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oMsg.Guid
            If oMsg.Id = 0 Then oMsg.Id = LastId(oTrans) + 1
            oRow("Id") = oMsg.Id
        Else
            oRow = oTb.Rows(0)
        End If

        With oMsg
            oRow("Text") = .Text
            oRow("UsrCreated") = .User.Guid
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oMsg As DTOMsg, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oMsg, oTrans)
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


    Shared Sub Delete(oMsg As DTOMsg, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Msg WHERE Guid='" & oMsg.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Function LastId(ByRef oTrans As SqlTransaction) As Integer
        Dim retval As Integer
        Dim SQL As String = "SELECT TOP 1 Id AS LastId FROM Msg ORDER BY Id DESC"

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        If oTb.Rows.Count > 0 Then
            Dim oRow As DataRow = oTb.Rows(0)
            If Not IsDBNull(oRow("LastId")) Then
                retval = CInt(oRow("LastId"))
            End If
        End If
        Return retval
    End Function

#End Region

End Class

Public Class MsgsLoader

    Shared Function All() As List(Of DTOMsg)
        Dim retval As New List(Of DTOMsg)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Msg ")
        sb.AppendLine("ORDER BY FchCreated DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOMsg(oDrd("Guid"))
            With item
                .Fch = oDrd("FchCreated")
                .User = New DTOUser(DirectCast(oDrd("UsrCreated"), Guid))
                .Text = oDrd("Text")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class

