Public Class FtpserverLoader

#Region "CRUD"

    Shared Function Find(oContact As DTOBaseGuid) As DTOFtpserver
        Dim retval As DTOFtpserver = Nothing
        Dim oFtpserver As New DTOFtpserver(oContact)
        If Load(oFtpserver) Then
            retval = oFtpserver
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oFtpserver As DTOFtpserver) As Boolean
        If Not oFtpserver.IsLoaded Then
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Ftpserver.*, CliGral.FullNom ")
            sb.AppendLine(", Ftppath.Cod, Ftppath.Path ")
            sb.AppendLine("FROM Ftpserver ")
            sb.AppendLine("INNER JOIN CliGral ON Ftpserver.Owner = CliGral.Guid ")
            sb.AppendLine("LEFT OUTER JOIN FtpPath ON Ftpserver.Owner = Ftppath.Owner ")
            sb.AppendLine("WHERE Ftpserver.Owner='" & oFtpserver.Owner.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read

                With oFtpserver
                    If Not .IsLoaded Then
                        .Owner.Nom = oDrd("FullNom")
                        .Servername = oDrd("Servername")
                        .Username = oDrd("Username")
                        .Password = oDrd("Password")
                        .Port = SQLHelper.GetIntegerFromDataReader(oDrd("Port"))
                        .SSL = oDrd("SSL")
                        .PassiveMode = SQLHelper.GetBooleanFromDatareader(oDrd("PassiveMode"))
                        .IsLoaded = True
                    End If

                    If Not IsDBNull(oDrd("Path")) Then
                        Dim oPath As New DTOFtpserver.Path
                        With oPath
                            .cod = oDrd("Cod")
                            .value = oDrd("Path")
                        End With
                        .Paths.Add(oPath)
                    End If
                End With
            Loop

            oDrd.Close()
        End If
        Dim retval As Boolean = oFtpserver.IsLoaded
        Return retval
    End Function

    Shared Function Update(oFtpserver As DTOFtpserver, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oFtpserver, oTrans)
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

    Shared Sub Update(oFtpserver As DTOFtpserver, ByRef oTrans As SqlTransaction)
        UpdateServer(oFtpserver, oTrans)
        UpdatePaths(oFtpserver, oTrans)
    End Sub


    Shared Sub UpdateServer(oFtpserver As DTOFtpserver, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Ftpserver ")
        sb.AppendLine("WHERE Owner='" & oFtpserver.Owner.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Owner") = oFtpserver.Owner.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oFtpserver
            oRow("Servername") = .Servername
            oRow("Username") = .Username
            oRow("Password") = .Password
            oRow("Port") = .Port
            oRow("SSL") = .SSL
            oRow("PassiveMode") = .PassiveMode
        End With

        oDA.Update(oDs)
    End Sub

    Shared Sub UpdatePaths(oFtpserver As DTOFtpserver, ByRef oTrans As SqlTransaction)
        DeletePaths(oFtpserver, oTrans)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Ftppath ")
        sb.AppendLine("WHERE Owner='" & oFtpserver.Owner.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each oPath In oFtpserver.Paths
            Dim oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Owner") = oFtpserver.Owner.Guid
            oRow("Cod") = oPath.cod
            oRow("Path") = oPath.value
        Next

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oFtpserver As DTOFtpserver, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oFtpserver, oTrans)
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


    Shared Sub Delete(oFtpserver As DTOFtpserver, ByRef oTrans As SqlTransaction)
        DeletePaths(oFtpserver, oTrans)
        DeleteServer(oFtpserver, oTrans)
    End Sub

    Shared Sub DeletePaths(oFtpserver As DTOFtpserver, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Ftppath WHERE Owner='" & oFtpserver.Owner.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteServer(oFtpserver As DTOFtpserver, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Ftpserver WHERE Owner='" & oFtpserver.Owner.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class FtpserversLoader

    Shared Function All(oEmp As DTOEmp) As List(Of DTOFtpserver)
        Dim retval As New List(Of DTOFtpserver)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Ftpserver.*, CliGral.FullNom ")
        sb.AppendLine("FROM Ftpserver ")
        sb.AppendLine("INNER JOIN CliGral ON Ftpserver.Owner = CliGral.Guid ")
        sb.AppendLine("WHERE CliGral.Emp = " & oEmp.Id & " ")
        sb.AppendLine("ORDER BY CliGral.FullNom")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oOwner As New DTOGuidNom(oDrd("Owner"), SQLHelper.GetStringFromDataReader(oDrd("FullNom")))
            Dim item As New DTOFtpserver(oOwner)
            With item
                .Servername = SQLHelper.GetStringFromDataReader(oDrd("Servername"))
                .Username = SQLHelper.GetStringFromDataReader(oDrd("Username"))
                .Password = SQLHelper.GetStringFromDataReader(oDrd("Password"))
                .Port = SQLHelper.GetIntegerFromDataReader(oDrd("Port"))
                .SSL = SQLHelper.GetBooleanFromDatareader(oDrd("SSL"))
                .PassiveMode = SQLHelper.GetBooleanFromDatareader(oDrd("PassiveMode"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class
