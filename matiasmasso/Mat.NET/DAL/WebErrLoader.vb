Public Class WebErrLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOWebErr
        Dim retval As DTOWebErr = Nothing
        Dim oWebErr As New DTOWebErr(oGuid)
        If Load(oWebErr) Then
            retval = oWebErr
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oWebErr As DTOWebErr) As Boolean
        If Not oWebErr.IsLoaded And Not oWebErr.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT WebErr.Url, WebErr.Referrer, WebErr.Fch, WebErr.Usr ")
            sb.AppendLine(", WebErr.Cod, WebErr.Ip, WebErr.Browser, WebErr.Msg ")
            sb.AppendLine(", Email.Adr, Email.Nickname, Email.Nom, Email.Cognoms ")
            sb.AppendLine("FROM WebErr ")
            sb.AppendLine("LEFT OUTER JOIN Email ON WebErr.Usr = Email.Guid ")
            sb.AppendLine("WHERE WebErr.Guid='" & oWebErr.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oWebErr
                    .Cod = oDrd("Cod")
                    .Fch = oDrd("Fch")
                    .Url = SQLHelper.GetStringFromDataReader(oDrd("Url"))
                    .Referrer = SQLHelper.GetStringFromDataReader(oDrd("Referrer"))
                    .Ip = SQLHelper.GetStringFromDataReader(oDrd("Ip"))
                    .Browser = SQLHelper.GetStringFromDataReader(oDrd("Browser"))
                    .Msg = SQLHelper.GetStringFromDataReader(oDrd("Msg"))
                    If Not IsDBNull(oDrd("Usr")) Then
                        .User = New DTOUser(oDrd("Usr"))
                        With .User
                            .emailAddress = SQLHelper.GetStringFromDataReader(oDrd("Adr"))
                            .nickName = SQLHelper.GetStringFromDataReader(oDrd("Nickname"))
                            .nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                            .cognoms = SQLHelper.GetStringFromDataReader(oDrd("Cognoms"))
                        End With
                    End If
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oWebErr.IsLoaded
        Return retval
    End Function

    Shared Function Update(oWebErr As DTOWebErr, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oWebErr, oTrans)
            oTrans.Commit()
            oWebErr.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oWebErr As DTOWebErr, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM WebErr ")
        sb.AppendLine("WHERE Guid='" & oWebErr.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oWebErr.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oWebErr
            oRow("Cod") = .Cod
            oRow("Url") = SQLHelper.NullableString(.Url)
            oRow("Referrer") = SQLHelper.NullableString(.Referrer)
            oRow("Ip") = SQLHelper.NullableString(.Ip)
            oRow("Browser") = SQLHelper.NullableString(.Browser)
            oRow("Msg") = SQLHelper.NullableString(.Msg)
            oRow("Fch") = .Fch
            oRow("Usr") = SQLHelper.NullableBaseGuid(.User)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oWebErr As DTOWebErr, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oWebErr, oTrans)
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


    Shared Sub Delete(oWebErr As DTOWebErr, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE WebErr WHERE Guid='" & oWebErr.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class WebErrsLoader

    Shared Function All() As List(Of DTOWebErr)
        Dim retval As New List(Of DTOWebErr)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT WebErr.Guid, WebErr.Url, WebErr.Referrer, WebErr.Fch, WebErr.Usr ")
        sb.AppendLine(", WebErr.Cod, WebErr.Ip, WebErr.Browser, WebErr.Msg ")
        sb.AppendLine(", Email.Adr, Email.Nickname, Email.Nom, Email.Cognoms ")
        sb.AppendLine("FROM WebErr ")
        sb.AppendLine("LEFT OUTER JOIN Email ON WebErr.Usr = Email.Guid ")
        sb.AppendLine("ORDER BY WebErr.Fch DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOWebErr(oDrd("Guid"))
            With item
                .Cod = oDrd("Cod")
                .Fch = oDrd("Fch")
                .Url = SQLHelper.GetStringFromDataReader(oDrd("Url"))
                .Referrer = SQLHelper.GetStringFromDataReader(oDrd("Referrer"))
                .Ip = SQLHelper.GetStringFromDataReader(oDrd("Ip"))
                .Browser = SQLHelper.GetStringFromDataReader(oDrd("Browser"))
                .Msg = SQLHelper.GetStringFromDataReader(oDrd("Msg"))
                If Not IsDBNull(oDrd("Usr")) Then
                    .User = New DTOUser(oDrd("Usr"))
                    With .User
                        .emailAddress = SQLHelper.GetStringFromDataReader(oDrd("Adr"))
                        .nickName = SQLHelper.GetStringFromDataReader(oDrd("Nickname"))
                        .nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                        .cognoms = SQLHelper.GetStringFromDataReader(oDrd("Cognoms"))
                    End With
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function Reset(exs As List(Of Exception)) As Boolean
        Dim SQL As String = "DELETE WebErr"
        SQLHelper.ExecuteNonQuery(SQL, exs)
        Return exs.Count = 0
    End Function
End Class

