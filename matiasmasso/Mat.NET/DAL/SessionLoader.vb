Public Class SessionLoader
    Shared Function Find(oGuid As Guid) As DTOSession
        Dim retval As DTOSession = Nothing
        Dim oSession As New DTOSession(oGuid)
        If Load(oSession) Then
            retval = oSession
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oSession As DTOSession) As Boolean
        Dim retval As Boolean
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT UsrSession.Usuari, UsrSession.Lang AS SessionLang, UsrSession.FchFrom, UsrSession.FchTo, UsrSession.AppType, UsrSession.AppVersion, UsrSession.Culture ")
        sb.AppendLine(", Email.Emp, Email.Adr, Email.Pwd, Email.Nom, Email.Cognoms, Email.Nickname, Email.Sex, Email.BirthYea ")
        sb.AppendLine(", Email.Pais, Email.ZipCod, Email.Location, Email.LocationNom, Email.ProvinciaNom, Email.Tel ")
        sb.AppendLine(", Email.Lang AS UserLang, Email.Rol, Email.Source, Email.DefaultContactGuid ")
        sb.AppendLine(", Email.FchCreated, Email.FchActivated, Email.FchDeleted ")
        sb.AppendLine("FROM UsrSession ")
        sb.AppendLine("LEFT OUTER JOIN Email ON UsrSession.Usuari=Email.Guid ")
        sb.AppendLine("WHERE UsrSession.Guid='" & oSession.Guid.ToString & "' ")

        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            With oSession
                .Lang = DTOLang.Factory(oDrd("SessionLang").ToString())
                .FchFrom = SQLHelper.GetFchFromDataReader(oDrd("FchFrom"))
                .FchTo = SQLHelper.GetFchFromDataReader(oDrd("FchTo"))
                .AppType = SQLHelper.GetIntegerFromDataReader(oDrd("AppType"))
                .AppVersion = SQLHelper.GetStringFromDataReader(oDrd("AppVersion"))
                .Culture = SQLHelper.GetStringFromDataReader(oDrd("Culture"))
                If Not IsDBNull(oDrd("Emp")) Then
                    .Emp = New DTOEmp(oDrd("Emp"))
                End If
                If IsDBNull(oDrd("Adr")) Then
                    .Rol = New DTORol(DTORol.Ids.Unregistered)
                Else
                    .User = New DTOUser(DirectCast(oDrd("Usuari"), Guid))
                    With .User
                        .Emp = New DTOEmp(oDrd("Emp"))
                        .EmailAddress = SQLHelper.GetStringFromDataReader(oDrd("Adr"))
                        .Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                        .Cognoms = SQLHelper.GetStringFromDataReader(oDrd("Cognoms"))
                        .NickName = SQLHelper.GetStringFromDataReader(oDrd("NickName"))
                        .Sex = SQLHelper.GetIntegerFromDataReader(oDrd("Sex"))
                        .BirthYear = SQLHelper.GetIntegerFromDataReader(oDrd("BirthYea"))
                        Dim CountryISO As String = SQLHelper.GetStringFromDataReader(oDrd("Pais"))
                        'If CountryISO > "" Then .Country = New DTOCountry(CountryISO)
                        .ZipCod = SQLHelper.GetStringFromDataReader(oDrd("ZipCod").ToString())
                        If Not IsDBNull(oDrd("Location")) Then
                            .Location = New DTOLocation(DirectCast(oDrd("Location"), Guid))
                        End If
                        .LocationNom = SQLHelper.GetStringFromDataReader(oDrd("LocationNom"))
                        .ProvinciaNom = SQLHelper.GetStringFromDataReader(oDrd("ProvinciaNom"))
                        .Tel = SQLHelper.GetStringFromDataReader(oDrd("Tel").ToString())
                        .Password = SQLHelper.GetStringFromDataReader(oDrd("Pwd"))
                        If IsDBNull(oDrd("Rol")) Then 'cas de haver eliminat l'usuari
                            .Rol = New DTORol(DTORol.Ids.Unregistered)
                        Else
                            .Rol = New DTORol(oDrd("Rol"))
                        End If
                        If IsDBNull(oDrd("UserLang")) Then
                            .Lang = DTOLang.ESP
                        Else
                            .Lang = DTOLang.Factory(oDrd("UserLang").ToString())
                        End If
                        .Source = SQLHelper.GetIntegerFromDataReader(oDrd("Source"))
                        If Not IsDBNull(oDrd("DefaultContactGuid")) Then
                            .Contact = New DTOContact(DirectCast(oDrd("DefaultContactGuid"), Guid))
                        End If

                        .FchCreated = SQLHelper.GetFchFromDataReader(oDrd("FchCreated"))
                        .FchActivated = SQLHelper.GetFchFromDataReader(oDrd("FchActivated"))
                        .FchDeleted = SQLHelper.GetFchFromDataReader(oDrd("FchDeleted"))
                        .Contacts = New List(Of DTOContact)
                        .IsLoaded = True
                    End With
                    .Rol = .User.Rol
                End If
                .IsAuthenticated = .Rol.IsAuthenticated
            End With
            retval = True
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function Update(oSession As DTOSession, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Update(oSession, oTrans)
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

    Protected Shared Sub Update(oSession As DTOSession, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM UsrSession WHERE Guid='" & oSession.Guid.ToString & "'"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oSession.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oSession
            oRow("Usuari") = SQLHelper.NullableBaseGuid(.User)
            oRow("FchFrom") = .FchFrom
            oRow("Lang") = .Lang.Id.ToString
            oRow("FchTo") = SQLHelper.NullableFch(.FchTo)
            oRow("AppType") = .AppType
            oRow("Culture") = SQLHelper.NullableString(.Culture)
            oRow("AppVersion") = SQLHelper.NullableString(.AppVersion)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oSession As DTOSession, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oSession, oTrans)
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

    Shared Sub Delete(oSession As DTOSession, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE UsrSession WHERE Guid='" & oSession.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Function Log(exs As List(Of Exception), oSrcGuid As Guid, Optional oUser As DTOUser = Nothing) As Boolean
        If oUser Is Nothing Then
            Dim SQL As String = "INSERT INTO WebLogBrowse(Doc) VALUES (@Doc)"
            SQLHelper.ExecuteNonQuery(SQL, exs, "@Doc", oSrcGuid.ToString())
        Else
            Dim SQL As String = "INSERT INTO WebLogBrowse(Doc,[User]) VALUES (@Doc,@User)"
            SQLHelper.ExecuteNonQuery(SQL, exs, "@Doc", oSrcGuid.ToString, "@User", oUser.Guid.ToString())
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function
End Class
