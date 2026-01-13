Public Class WebPageAliasLoader


#Region "CRUD"

    Shared Function FromUrl(oWebPageAlias As DTOWebPageAlias) As DTOWebPageAlias
        Dim retval As DTOWebPageAlias = Nothing
        Dim urlFrom = oWebPageAlias.UrlFrom
        If urlFrom.StartsWith("/") Then urlFrom = urlFrom.Substring(1)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM WebPageAlias ")
        sb.AppendLine("WHERE UrlFrom='" & urlFrom & "' ")
        If oWebPageAlias.domain <> DTOWebPageAlias.Domains.All Then
            sb.AppendLine("AND (domain=" & CInt(oWebPageAlias.domain) & ") ")
        End If
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOWebPageAlias
            With retval
                .Guid = oDrd("Guid")
                .UrlFrom = urlFrom
                .domain = oDrd("Domain")
                .UrlTo = oDrd("UrlTo")
            End With
        End If

        oDrd.Close()
        Return retval
    End Function

    Shared Function Find(oGuid As Guid) As DTOWebPageAlias
        Dim retval As DTOWebPageAlias = Nothing
        Dim oWebPageAlias As New DTOWebPageAlias(oGuid)
        If Load(oWebPageAlias) Then
            retval = oWebPageAlias
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oWebPageAlias As DTOWebPageAlias) As Boolean

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM WebPageAlias ")
        If oWebPageAlias.Guid = Nothing Then
            sb.AppendLine("WHERE UrlFrom='" & oWebPageAlias.UrlFrom & "' ")
        Else
            sb.AppendLine("WHERE Guid='" & oWebPageAlias.Guid.ToString & "' ")
        End If

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            With oWebPageAlias
                .Guid = oDrd("Guid")
                .UrlFrom = oDrd("UrlFrom")
                .domain = oDrd("Domain")
                .UrlTo = oDrd("UrlTo")
            End With
        End If

        oDrd.Close()

        Dim retval As Boolean = True
        Return retval
    End Function

    Shared Function Update(oWebPageAlias As DTOWebPageAlias, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oWebPageAlias, oTrans)
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


    Shared Sub Update(oWebPageAlias As DTOWebPageAlias, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM WebPageAlias ")
        sb.AppendLine("WHERE Guid ='" & oWebPageAlias.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oWebPageAlias.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oWebPageAlias
            oRow("UrlFrom") = oWebPageAlias.UrlFrom
            oRow("Domain") = .domain
            oRow("UrlTo") = .UrlTo
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oWebPageAlias As DTOWebPageAlias, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oWebPageAlias, oTrans)
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


    Shared Sub Delete(oWebPageAlias As DTOWebPageAlias, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE WebPageAlias WHERE UrlFrom='" & oWebPageAlias.UrlFrom & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class WebPagesAliasLoader

    Shared Function All() As List(Of DTOWebPageAlias)
        Dim retval As New List(Of DTOWebPageAlias)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM WebPageAlias ")
        sb.AppendLine("ORDER BY UrlFrom")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOWebPageAlias(oDrd("Guid"))
            With item
                .UrlFrom = oDrd("UrlFrom")
                .domain = oDrd("Domain")
                .UrlTo = oDrd("UrlTo")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class

