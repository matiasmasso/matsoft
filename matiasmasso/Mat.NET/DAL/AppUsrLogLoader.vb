Public Class AppUsrLogLoader



    Shared Sub Log(exs As List(Of Exception), oRequest As DTOAppUsrLog.Request)
        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction

        Try
            Log(oRequest, oTrans)
            oTrans.Commit()
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

    End Sub

    Shared Function LogExit(exs As List(Of Exception), oGuid As Guid) As Boolean
        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction

        Try
            Dim sb As New Text.StringBuilder
            sb.AppendLine("UPDATE AppUsrLog ")
            sb.AppendLine("SET FchTo = GETDATE() ")
            sb.AppendLine("WHERE Guid = '" & oGuid.ToString() & "' ")
            Dim SQL As String = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL, oTrans)
            oTrans.Commit()
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return exs.Count = 0
    End Function


    Shared Sub Log(oRequest As DTOAppUsrLog.Request, oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM AppUsrLog ")
        sb.AppendLine("WHERE Guid IS NULL ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow = oTb.NewRow
        oTb.Rows.Add(oRow)
        With oRequest
            oRow("Guid") = .Guid
            oRow("App") = .AppId
            If Not String.IsNullOrEmpty(.AppVersion) Then
                oRow("AppVersion") = .AppVersion '.Substring(0, 10)
            End If
            If Not String.IsNullOrEmpty(.OS) Then
                oRow("OS") = .OS '.Substring(0, 10)
            End If
            If Not String.IsNullOrEmpty(.DeviceModel) Then
                oRow("DeviceModel") = .DeviceModel '.Substring(0, 20)
            End If
            If GuidHelper.IsGuid(.DeviceId) Then
                oRow("DeviceId") = New Guid(.DeviceId)
            Else
                oRow("DeviceId") = SQLHelper.NullableString(.DeviceId)
            End If
            oRow("Usr") = SQLHelper.NullableGuid(.UserGuid)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function UserRol(oRequest As DTOAppUsrLog.Request) As DTORol
        Dim retval As DTORol = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Rol ")
        sb.AppendLine("FROM Email ")
        sb.AppendLine("WHERE Guid ='" & oRequest.UserGuid.ToString() & "' ")
        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTORol(oDrd("Rol"))
        End If

        oDrd.Close()
        Return retval
    End Function


End Class


Public Class AppUsrLogsLoader

    Shared Function all() As List(Of DTOAppUsrLog.Request)
        Dim retval As New List(Of DTOAppUsrLog.Request)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT AppUsrLog.*, Email.Adr AS EmailAddress")
        sb.AppendLine("FROM AppUsrLog ")
        sb.AppendLine("LEFT OUTER JOIN Email ON AppUsrLog.Usr = Email.Guid ")
        sb.AppendLine("ORDER BY AppUsrLog.Fch DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOAppUsrLog.Request()
            With item
                .Fch = oDrd("Fch")
                .AppId = oDrd("App")
                .AppVersion = oDrd("AppVersion")
                '.DeviceId = oDrd("DeviceId")
                .DeviceModel = oDrd("DeviceModel")
                .OS = oDrd("OS")
                If Not IsDBNull(oDrd("Usr")) Then
                    .User = New DTOGuidNom(oDrd("Usr"), SQLHelper.GetStringFromDataReader(oDrd("EmailAddress")))
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function lastLogs(AppId As DTOApp.AppTypes) As List(Of DTOAppUsrLog.Request) 'Mat.Net en IOS
        Dim retval As New List(Of DTOAppUsrLog.Request)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT UsrSession.Usuari, Email.adr AS EmailAddress, UsrSession.FchFrom, UsrSession.AppVersion ")
        sb.AppendLine("FROM UsrSession INNER JOIN ")
        sb.AppendLine("(SELECT UsrSession.Usuari, MAX(UsrSession.FchFrom) AS LastFch FROM UsrSession GROUP BY usrsession.usuari) X ")
        sb.AppendLine("ON UsrSession.Usuari = X.Usuari AND UsrSession.FchFrom = X.LastFch ")
        sb.AppendLine("INNER JOIN Email ON UsrSession.Usuari = Email.Guid ")
        sb.AppendLine("WHERE AppType = " & AppId & " ")
        sb.AppendLine("ORDER BY X.LastFch DESC ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOAppUsrLog.Request()
            With item
                .Fch = SQLHelper.GetFchFromDataReader(oDrd("FchFrom"))
                .AppVersion = SQLHelper.GetStringFromDataReader(oDrd("AppVersion"))
                If Not IsDBNull(oDrd("Usuari")) Then
                    .User = New DTOGuidNom(oDrd("Usuari"), SQLHelper.GetStringFromDataReader(oDrd("EmailAddress")))
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function lastLogs(AppId As DTOApp.AppTypes, oUser As DTOUser) As List(Of DTOAppUsrLog) 'Mat.Net en IOS
        Dim retval As New List(Of DTOAppUsrLog)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT UsrSession.FchFrom, UsrSession.FchTo ")
        sb.AppendLine("FROM UsrSession ")
        sb.AppendLine("WHERE AppType = " & AppId & " ")
        sb.AppendLine("AND Usuari = '" & oUser.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY FchFrom DESC ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOAppUsrLog()
            With item
                .FchFrom = SQLHelper.GetFchFromDataReader(oDrd("FchFrom"))
                .FchTo = SQLHelper.GetFchFromDataReader(oDrd("FchTo"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class