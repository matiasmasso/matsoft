Public Class WebLogBrowseLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOWebLogBrowse
        Dim retval As DTOWebLogBrowse = Nothing
        Dim oWebLogBrowse As New DTOWebLogBrowse(oGuid)
        If Load(oWebLogBrowse) Then
            retval = oWebLogBrowse
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oWebLogBrowse As DTOWebLogBrowse) As Boolean
        If Not oWebLogBrowse.IsLoaded And Not oWebLogBrowse.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT WebLogBrowse.*, Email.adr, Email.Nickname, Email.Rol ")
            sb.AppendLine("FROM WebLogBrowse ")
            sb.AppendLine("INNER JOIN Email ON WebLogBrowse.[User] = Email.Guid ")
            sb.AppendLine("WHERE WebLogBrowse.Guid= '" & oWebLogBrowse.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                Dim oUser As New DTOUser(DirectCast(oDrd("User"), Guid))
                With oUser
                    .EmailAddress = oDrd("Adr")
                    .Nom = oDrd("Nickname")
                    .Rol = New DTORol(CInt(oDrd("Rol")))
                End With
                With oWebLogBrowse
                    .Doc = New DTOBaseGuid(DirectCast(oDrd("Doc"), Guid))
                    .Fch = oDrd("Fch")
                    .User = oUser
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oWebLogBrowse.IsLoaded
        Return retval
    End Function

    Shared Function Update(oWebLogBrowse As DTOWebLogBrowse, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oWebLogBrowse, oTrans)
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


    Shared Sub Update(oWebLogBrowse As DTOWebLogBrowse, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM WebLogBrowse ")
        sb.AppendLine("WHERE Guid=@Guid")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oWebLogBrowse.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oWebLogBrowse.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oWebLogBrowse
            oRow("Doc") = .Doc.Guid
            oRow("Fch") = .Fch
            oRow("User") = .User.Guid
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oWebLogBrowse As DTOWebLogBrowse, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oWebLogBrowse, oTrans)
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


    Shared Sub Delete(oWebLogBrowse As DTOWebLogBrowse, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE WebLogBrowse WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oWebLogBrowse.Guid.ToString())
    End Sub

#End Region

End Class

Public Class WebLogBrowsesLoader

    Shared Function All(oDoc As DTOBaseGuid) As List(Of DTOWebLogBrowse)
        Dim retval As New List(Of DTOWebLogBrowse)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT WebLogBrowse.Guid, WebLogBrowse.Fch, WebLogBrowse.[User] ")
        sb.AppendLine(", Email.adr, Email.Nom, Email.Rol ")
        sb.AppendLine("FROM WebLogBrowse ")
        sb.AppendLine("INNER JOIN Email ON WebLogBrowse.[User] = Email.Guid ")
        sb.AppendLine("WHERE Doc = '" & oDoc.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY Fch DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oUser As New DTOUser(DirectCast(oDrd("User"), Guid))
            With oUser
                .EmailAddress = oDrd("Adr")
                If Not IsDBNull(oDrd("Nom")) Then
                    .Nom = oDrd("Nom")
                End If
                .Rol = New DTORol(CInt(oDrd("Rol")))
            End With
            Dim item As New DTOWebLogBrowse(oDrd("Guid"))
            With item
                .Doc = oDoc
                .Fch = oDrd("Fch")
                .User = oUser
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class

