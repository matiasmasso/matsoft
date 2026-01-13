Public Class EmpLoader

    Shared Function Find(oId As DTOEmp.Ids) As DTOEmp
        Dim retval As DTOEmp = Nothing
        Dim oEmp As New DTOEmp(oId)
        If Load(oEmp) Then
            retval = oEmp
        End If
        Return retval
    End Function

    Shared Function FromNif(sNif As String) As DTOEmp
        Dim retval As DTOEmp = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Emp.Emp FROM Emp ")
        sb.AppendLine("INNER JOIN CliGral ON Emp.Org = CliGral.Guid ")
        sb.AppendLine("WHERE CliGral.NIF = '" & sNif & "' ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = Find(oDrd("Emp"))
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function Load(ByRef oEmp as DTOEmp) As Boolean
        If Not oEmp.IsLoaded And Not oEmp.IsNew Then
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Emp.Emp, Emp.Org, Emp.Nom, Emp.Abr, Emp.Mgz, Emp.Taller ")
            sb.AppendLine(", Emp.Cnae, Emp.DadesRegistrals, Emp.Ip ")
            sb.AppendLine(", Emp.Domini, Emp.MsgFrom, Emp.MailboxPwd, Emp.MailboxUsr, Emp.MailboxPort, Emp.MailboxSmtp ")
            sb.AppendLine(", CliGral.RaoSocial ")
            sb.AppendLine(", CliGral.NIF, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod ")
            sb.AppendLine(", VwAddress.* ")
            sb.AppendLine(", Mgz.FullNom AS MgzFullNom ")
            sb.AppendLine("FROM Emp ")
            sb.AppendLine("LEFT OUTER JOIN CliGral ON Emp.Org = CliGral.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CliGral AS Mgz ON Emp.Mgz = Mgz.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwAddress ON Emp.Org = VwAddress.SrcGuid ")
            sb.AppendLine("WHERE Emp.Emp = " & oEmp.Id & " ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oEmp
                    .Nom = oDrd("NOM")
                    .Abr = oDrd("ABR")

                    If Not IsDBNull(oDrd("Org")) Then
                        .Org = New DTOContact(oDrd("Org"))
                        With .Org
                            .Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
                            .Nom = SQLHelper.GetStringFromDataReader(oDrd("RaoSocial"))
                            .Address = SQLHelper.GetAddressFromDataReader(oDrd)
                        End With
                    End If

                    If Not IsDBNull(oDrd("Mgz")) Then
                        .Mgz = New DTOMgz(oDrd("Mgz"))
                        .Mgz.FullNom = SQLHelper.GetStringFromDataReader(oDrd("MgzFullNom"))
                    End If

                    If Not IsDBNull(oDrd("Taller")) Then
                        .Taller = New DTOTaller(oDrd("Taller"))
                    End If

                    If Not IsDBNull(oDrd("DADESREGISTRALS")) Then
                        .DadesRegistrals = oDrd("DADESREGISTRALS")
                    End If

                    If Not IsDBNull(oDrd("DOMINI")) Then
                        .Domini = oDrd("DOMINI").ToString
                    End If

                    .Ip = SQLHelper.GetStringFromDataReader(oDrd("Ip"))

                    .Cnae = SQLHelper.GetStringFromDataReader(oDrd("Cnae"))
                    .MsgFrom = SQLHelper.GetStringFromDataReader(oDrd("MSGFROM"))
                    .MailboxPwd = SQLHelper.GetStringFromDataReader(oDrd("MAILBOXPWD"))
                    .MailboxUsr = SQLHelper.GetStringFromDataReader(oDrd("MAILBOXUSR"))
                    .MailBoxPort = SQLHelper.GetStringFromDataReader(oDrd("MailboxPort"))
                    .MailBoxSmtp = SQLHelper.GetStringFromDataReader(oDrd("MailboxSmtp"))

                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oEmp.IsLoaded
        Return retval
    End Function


    Shared Function Create(exs As List(Of Exception), oUser As DTOUser) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oUser.Emp, oTrans)
            UserLoader.Update(oUser, oTrans)
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

    Shared Function Update(exs As List(Of Exception), oEmp As DTOEmp) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oEmp, oTrans)
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

    Shared Sub Update(oEmp as DTOEmp, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Emp ")
        sb.AppendLine("WHERE Emp=" & oEmp.Id & " ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            If oEmp.Id = 0 Then oEmp.Id = LastId(oTrans) + 1

            oRow("Emp") = oEmp.Id
        Else
            oRow = oTb.Rows(0)
        End If

        With oEmp
            oRow("Nom") = .Nom
            oRow("Abr") = .Abr

            If .Org Is Nothing Then
                oRow("Org") = System.DBNull.Value
            Else
                oRow("Org") = .Org.Guid
            End If

            If .Mgz Is Nothing Then
                oRow("Mgz") = System.DBNull.Value
            Else
                oRow("Mgz") = .Mgz.Guid
            End If

            oRow("DadesRegistrals") = .DadesRegistrals
            oRow("Domini") = .Domini
            oRow("Ip") = SQLHelper.NullableString(.Ip)
            oRow("Cnae") = SQLHelper.NullableString(.Cnae)
            oRow("MsgFrom") = .MsgFrom
            oRow("MAILBOXPWD") = .MailboxPwd
            oRow("MAILBOXUSR") = .MailboxUsr
            oRow("MailboxPort") = .MailBoxPort
            oRow("MailboxSmtp") = .MailBoxSmtp
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function LastId(ByRef oTrans As SqlTransaction) As Integer
        Dim retval As Integer
        Dim SQL As String = "SELECT TOP 1 Emp AS LastId FROM Emp ORDER BY Emp DESC"

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

    Shared Function Delete(oEmp As DTOEmp, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oEmp, oTrans)
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


    Shared Sub Delete(oEmp As DTOEmp, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Emp WHERE Emp=@Emp"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Emp", oEmp.Id)
    End Sub


End Class

Public Class EmpsLoader
    Shared Function Compact(oUser As DTOUser) As List(Of Models.Base.IdNom)
        Dim retval As New List(Of Models.Base.IdNom)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Emp.Emp, Emp.Nom, Emp.Abr ")
        sb.AppendLine("FROM Emp ")
        sb.AppendLine("INNER JOIN Email ON Emp.Emp = Email.Emp AND Email.Adr = '" & oUser.EmailAddress & "' ")
        sb.AppendLine("ORDER BY Emp.Emp ")
        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oEmp As New Models.Base.IdNom(oDrd("Emp"))
            With oEmp
                .Nom = SQLHelper.GetStringFromDataReader(oDrd("Abr"))
                If String.IsNullOrEmpty(.Nom) Then
                    .Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                End If
            End With
            retval.Add(oEmp)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oUser As DTOUser) As List(Of DTOEmp)
        Dim retval As New List(Of DTOEmp)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Emp.Emp, Emp.Org, Emp.Nom, Emp.Abr ")
        sb.AppendLine(", Emp.Cnae, Emp.DadesRegistrals ")
        sb.AppendLine(", Emp.Domini, Emp.MsgFrom ")
        sb.AppendLine(", CliGral.RaoSocial, CliGral.GLN ")
        sb.AppendLine(", CliGral.NIF, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod ")
        sb.AppendLine(", VwAddress.* ")
        sb.AppendLine(", Emp.Mgz AS MgzGuid, Mgz.Nom AS MgzNom ")
        sb.AppendLine(", Emp.Taller ")
        sb.AppendLine("FROM Emp ")
        sb.AppendLine("INNER JOIN Email ON Emp.Emp = Email.Emp AND Email.Adr = '" & oUser.EmailAddress & "' ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON Emp.Org = CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN  Mgz ON Emp.Mgz =  Mgz.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwAddress ON Emp.Org = VwAddress.SrcGuid ")
        sb.AppendLine("ORDER BY Emp.Emp ")
        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oEmp = New DTOEmp(CInt(oDrd("Emp")))
            With oEmp
                .Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                .Abr = SQLHelper.GetStringFromDataReader(oDrd("Abr"))

                If Not IsDBNull(oDrd("Org")) Then
                    .Org = New DTOContact(oDrd("Org"))
                    With .Org
                        .Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
                        .Nom = SQLHelper.GetStringFromDataReader(oDrd("RaoSocial"))
                        .Address = SQLHelper.GetAddressFromDataReader(oDrd)
                        .GLN = SQLHelper.GetEANFromDataReader(oDrd("GLN"))
                    End With
                End If

                If Not IsDBNull(oDrd("MgzGuid")) Then
                    .Mgz = New DTOMgz(oDrd("MgzGuid"))
                    .Mgz.Nom = SQLHelper.GetStringFromDataReader(oDrd("MgzNom"))
                End If

                .DadesRegistrals = SQLHelper.GetStringFromDataReader(oDrd("DADESREGISTRALS"))
                .Domini = SQLHelper.GetStringFromDataReader(oDrd("DOMINI"))
                .Cnae = SQLHelper.GetStringFromDataReader(oDrd("Cnae"))
                .MsgFrom = SQLHelper.GetStringFromDataReader(oDrd("MsgFrom"))

                If Not IsDBNull(oDrd("Taller")) Then
                    .Taller = New DTOTaller(oDrd("Taller"))
                End If

                .IsLoaded = True
            End With
            retval.Add(oEmp)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
