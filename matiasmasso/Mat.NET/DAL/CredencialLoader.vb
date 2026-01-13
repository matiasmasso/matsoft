Public Class CredencialLoader

    Shared Function Find(oGuid As Guid) As DTOCredencial
        Dim retval As DTOCredencial = Nothing
        Dim oCredencial As New DTOCredencial(oGuid)

        If Load(oCredencial) Then
            retval = oCredencial
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oCredencial As DTOCredencial) As Boolean
        If Not oCredencial.IsLoaded Then

            Dim sb As New Text.StringBuilder
            sb.AppendLine("SELECT Credencial.Referencia, Credencial.Url, Credencial.Usuari, Credencial.Password, Credencial.Obs ")
            sb.AppendLine(", Credencial.FchCreated, Credencial.UsrCreated, Credencial.FchLastEdited, Credencial.UsrLastEdited ")
            sb.AppendLine(", UsrCreated.Adr AS UsrCreatedEmailAddress, UsrCreated.Nickname AS UsrCreatedNickname ")
            sb.AppendLine(", UsrLastEdited.Adr AS UsrLastEditedEmailAddress, UsrLastEdited.Nickname AS UsrLastEditedNickname ")
            sb.AppendLine("FROM Credencial ")
            sb.AppendLine("LEFT OUTER JOIN Email AS UsrCreated ON Credencial.UsrCreated = UsrCreated.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Email AS UsrLastEdited ON Credencial.UsrLastEdited = UsrLastEdited.Guid ")
            sb.AppendLine("WHERE Credencial.Guid='" & oCredencial.Guid.ToString & "' ")
            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oCredencial
                    .referencia = oDrd("Referencia").ToString
                    .url = oDrd("Url").ToString
                    .usuari = oDrd("Usuari").ToString
                    .password = oDrd("Password").ToString
                    .obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                    .rols = New List(Of DTORol)
                    .owners = New List(Of DTOUser)
                    .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd)

                    .IsLoaded = True
                End With

                SQL = "SELECT Rol FROM CredencialRol WHERE Credencial='" & oCredencial.Guid.ToString & "'"
                oDrd = SQLHelper.GetDataReader(SQL)
                Do While oDrd.Read
                    Dim oRol As New DTORol(oDrd("Rol"))
                    oCredencial.Rols.Add(oRol)
                Loop

                sb = New Text.StringBuilder
                sb.AppendLine("SELECT CredencialOwner.[Owner], Email.Adr, Email.Nickname ")
                sb.AppendLine("FROM CredencialOwner ")
                sb.AppendLine("INNER JOIN Email ON CredencialOwner.[Owner] = Email.Guid ")
                sb.AppendLine("WHERE CredencialOwner.Credencial='" & oCredencial.Guid.ToString & "' ")
                SQL = sb.ToString
                oDrd = SQLHelper.GetDataReader(SQL)
                Do While oDrd.Read
                    Dim oOwner As New DTOUser(DirectCast(oDrd("Owner"), Guid))
                    With oOwner
                        .EmailAddress = SQLHelper.GetStringFromDataReader(oDrd("Adr"))
                        .NickName = SQLHelper.GetStringFromDataReader(oDrd("NickName"))
                    End With
                    oCredencial.Owners.Add(oOwner)
                Loop

            End If
            oDrd.Close()
        End If

        Dim retval As Boolean = oCredencial.IsLoaded
        Return retval
    End Function

    'Shared Sub Log(sSearchKey As String, iResults As Integer, oEmail As Email)
    'Dim oItem As New Credencial(sSearchKey, iResults, oEmail)
    'Dim exs as New List(Of exception)
    'Update(oItem, exs)
    'End Sub

    Shared Function Update(oCredencial As DTOCredencial, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Update(oCredencial, oTrans)
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

    Protected Shared Sub Update(oCredencial As DTOCredencial, ByRef oTrans As SqlTransaction)
        UpdateHeader(oCredencial, oTrans)
        UpdateRols(oCredencial, oTrans)
        UpdateOwners(oCredencial, oTrans)
    End Sub

    Protected Shared Sub UpdateHeader(oCredencial As DTOCredencial, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM Credencial WHERE Guid='" & oCredencial.Guid.ToString & "'"

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oCredencial.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oCredencial
            oRow("Referencia") = .Referencia
            oRow("Url") = .Url
            oRow("usuari") = .Usuari
            oRow("Password") = .Password
            oRow("Obs") = SQLHelper.GetStringFromDataReader(.Obs)
            SQLHelper.SetUsrLog(.UsrLog, oRow)
        End With

        oDA.Update(oDs)
    End Sub

    Protected Shared Sub UpdateOwners(oCredencial As DTOCredencial, ByRef oTrans As SqlTransaction)
        DeleteOwners(oCredencial, oTrans)

        Dim SQL As String = "SELECT * FROM CredencialOwner WHERE Credencial='" & oCredencial.Guid.ToString & "'"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        For Each oOwner As DTOUser In oCredencial.Owners
            Dim oRow As DataRow = oTb.NewRow
            oRow("Credencial") = oCredencial.Guid
            oRow("Owner") = oOwner.Guid
            oTb.Rows.Add(oRow)
        Next

        oDA.Update(oDs)
    End Sub

    Protected Shared Sub UpdateRols(oCredencial As DTOCredencial, ByRef oTrans As SqlTransaction)
        DeleteRols(oCredencial, oTrans)

        Dim SQL As String = "SELECT * FROM CredencialRol WHERE Credencial='" & oCredencial.Guid.ToString & "'"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        For Each oRol As DTORol In oCredencial.Rols
            Dim oRow As DataRow = oTb.NewRow
            oRow("Credencial") = oCredencial.Guid
            oRow("Rol") = oRol.Id
            oTb.Rows.Add(oRow)
        Next

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oCredencial As DTOCredencial, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oCredencial, oTrans)
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


    Protected Shared Sub Delete(oCredencial As DTOCredencial, ByRef oTrans As SqlTransaction)
        DeleteOwners(oCredencial, oTrans)
        DeleteRols(oCredencial, oTrans)
        DeleteHeader(oCredencial, oTrans)
    End Sub

    Protected Shared Sub DeleteHeader(oCredencial As DTOCredencial, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Credencial WHERE Guid='" & oCredencial.Guid.ToString & "'"
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Protected Shared Sub DeleteRols(oCredencial As DTOCredencial, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE CredencialRol WHERE Credencial='" & oCredencial.Guid.ToString & "'"
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Protected Shared Sub DeleteOwners(oCredencial As DTOCredencial, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE CredencialOwner WHERE Credencial='" & oCredencial.Guid.ToString & "'"
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub



End Class


Public Class CredencialsLoader

    Shared Function Headers(oUser As DTOUser) As List(Of DTOCredencial)
        Dim retval As New List(Of DTOCredencial)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Credencial.Guid, Credencial.Referencia, Credencial.Url, Credencial.Usuari, Credencial.Password ")
        sb.AppendLine("FROM Credencial ")
        sb.AppendLine("INNER JOIN ( ")
        sb.AppendLine("         SELECT Credencial FROM CredencialOwner WHERE CredencialOwner.Owner='" & oUser.Guid.ToString & "' ")
        sb.AppendLine("         UNION ")
        sb.AppendLine("         SELECT Credencial FROM CredencialRol WHERE CredencialRol.Rol=" & oUser.Rol.Id & " ")
        sb.AppendLine("         ) X ON Credencial.Guid = X.Credencial ")
        sb.AppendLine("GROUP BY Guid, Referencia, Url, Usuari, Password ")
        sb.AppendLine("ORDER BY Referencia")


        sb = New System.Text.StringBuilder
        sb.AppendLine("SELECT Credencial.Guid, Credencial.Referencia, Credencial.Url, Credencial.Usuari, Credencial.Password, CredencialOwner.Owner, CredencialRol.Rol ")
        sb.AppendLine("FROM Credencial ")
        sb.AppendLine("LEFT OUTER JOIN CredencialOwner ON Credencial.Guid = CredencialOwner.Credencial ")
        sb.AppendLine("LEFT OUTER JOIN CredencialRol ON Credencial.Guid = CredencialRol.Credencial ")
        sb.AppendLine("WHERE CredencialOwner.Owner='" & oUser.Guid.ToString & "' OR CredencialRol.Rol='" & oUser.Rol.Id & "' ")
        sb.AppendLine("GROUP BY Credencial.Guid, Credencial.Referencia, Credencial.Url, Credencial.Usuari, Credencial.Password, CredencialOwner.Owner, CredencialRol.Rol ")
        sb.AppendLine("ORDER BY Credencial.Referencia, Credencial.Guid, CredencialOwner.Owner, CredencialRol.Rol")

        Dim SQL As String = sb.ToString

        Dim oCredencial As New DTOCredencial
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oCredencial.Guid.Equals(oDrd("Guid")) Then
                oCredencial = New DTOCredencial(oDrd("Guid"))
                With oCredencial
                    .Referencia = SQLHelper.GetStringFromDataReader(oDrd("Referencia"))
                    .Url = SQLHelper.GetStringFromDataReader(oDrd("Url"))
                    .Usuari = SQLHelper.GetStringFromDataReader(oDrd("Usuari"))
                    .Password = SQLHelper.GetStringFromDataReader(oDrd("Password"))
                End With
                retval.Add(oCredencial)
            End If
            If Not IsDBNull(oDrd("Owner")) Then
                Dim oOwnerGuid As Guid = oDrd("Owner")
                If Not oCredencial.Owners.Exists(Function(x) x.Guid.Equals(oOwnerGuid)) Then
                    oCredencial.Owners.Add(New DTOUser(DirectCast(oDrd("Owner"), Guid)))
                End If
            End If
            If Not IsDBNull(oDrd("Rol")) Then
                If Not oCredencial.Rols.Exists(Function(x) x.Id = oDrd("Rol")) Then
                    oCredencial.Rols.Add(New DTORol(oDrd("Rol")))
                End If
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function Owners(Optional oEmp As DTOEmp = Nothing) As List(Of DTOUser)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CredencialOwner.Owner, Email.Adr, Email.Rol, Email.Lang ")
        sb.AppendLine("FROM CredencialOwner ")
        sb.AppendLine("INNER JOIN Email ON CredencialOwner.Owner = Email.Guid ")
        If oEmp IsNot Nothing Then
            sb.AppendLine("WHERE Email.Emp = " & CInt(oEmp.Id) & " ")
        End If
        sb.AppendLine("GROUP BY CredencialOwner.Owner, Email.Adr, Email.Rol, Email.Lang ")
        sb.AppendLine("ORDER BY Email.Adr ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim retval As New List(Of DTOUser)
        Do While oDrd.Read
            Dim oUser As New DTOUser(DirectCast(oDrd("Owner"), Guid))
            oUser.Rol = New DTORol(oDrd("Rol"))
            oUser.Lang = SQLHelper.GetLangFromDataReader(oDrd("Lang"))
            oUser.EmailAddress = SQLHelper.GetStringFromDataReader(oDrd("Adr"))
            retval.Add(oUser)
        Loop
        oDrd.Close()

        Return retval
    End Function

End Class