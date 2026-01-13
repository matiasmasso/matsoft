Public Class UserLoader

    Shared Function Find(oGuid As Guid) As DTOUser
        Dim retval As DTOUser = Nothing
        Dim oUser As New DTOUser(oGuid)
        If Load(oUser) AndAlso Not String.IsNullOrEmpty(oUser.EmailAddress) Then
            retval = oUser
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oUser As DTOUser) As Boolean
        Dim oSubscription = DTOSubscription.Wellknown(DTOSubscription.Wellknowns.Facturacio)
        If Not oUser.IsLoaded Then
            Dim SQL As String = "SELECT Email.*,Email_Clis.ContactGuid, CliGral.FullNom, CliGral.Obsoleto AS CliObsoleto, " _
            & "Country.Guid AS CountryGuid, Country.Nom_Esp AS CountryEsp, Country.Nom_Cat AS CountryCat, Country.Nom_Eng AS CountryEng " _
            & ", SscEmail.SscGuid " _
            & "FROM Email " _
            & "LEFT OUTER JOIN Email_Clis ON Email_Clis.EmailGuid=Email.Guid " _
            & "LEFT OUTER JOIN CliGral ON Email_Clis.ContactGuid = CliGral.Guid " _
            & "LEFT OUTER JOIN Country ON Email.Pais= Country.ISO " _
            & "LEFT OUTER JOIN SscEmail ON Email.Guid= SscEmail.Email AND SscEmail.SscGuid = '" & oSubscription.Guid.ToString() & "' " _
            & "WHERE Email.Guid='" & oUser.Guid.ToString & "' ORDER BY CliGral.Obsoleto , CliGral.FullNom "

            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Load(oUser, oDrd)
            oDrd.Close()
        End If

        Dim retval As Boolean = oUser.IsLoaded
        Return retval
    End Function

    Shared Function Exists(oEmp As DTOEmp, ByRef sEmail As String) As Boolean
        Dim SQL As String = "SELECT Guid FROM Email WHERE Emp=" & oEmp.Id & " AND Adr='" & sEmail.Trim & "'"
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim retval As Boolean = oDrd.Read
        oDrd.Close()
        Return retval
    End Function

    Shared Function FromEmail(oEmp As DTOEmp, sEmail As String) As DTOUser
        Dim retval As DTOUser = Nothing
        Dim oSubscription = DTOSubscription.Wellknown(DTOSubscription.Wellknowns.Facturacio)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("Select Email.*, Email_Clis.ContactGuid, CliGral.FullNom, CliGral.Obsoleto AS CliObsoleto ")
        sb.AppendLine(", Country.Guid AS CountryGuid, Country.Nom_Esp As CountryEsp, Country.Nom_Cat As CountryCat, Country.Nom_Eng AS CountryEng ")
        sb.AppendLine(", SscEmail.SscGuid ")
        sb.AppendLine("FROM Email ")
        sb.AppendLine("LEFT OUTER JOIN Email_Clis ON Email_Clis.EmailGuid=Email.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON Email_Clis.ContactGuid = CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Country ON Email.Pais= Country.ISO ")
        sb.AppendLine("LEFT OUTER JOIN SscEmail ON Email.Guid= SscEmail.Email And SscEmail.SscGuid = '" & oSubscription.Guid.ToString() & "' ")
        sb.AppendLine("WHERE Email.Emp=" & oEmp.Id & " ")
        sb.AppendLine("And Email.Adr='" & sEmail & "' ")
        sb.AppendLine("ORDER BY CliGral.Obsoleto, CliGral.FullNom")
        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)

        Load(retval, oDrd)
        oDrd.Close()
        Return retval
    End Function

    Shared Sub Load(ByRef oUser As DTOUser, oDrd As SqlDataReader)
        Do While oDrd.Read
            If oUser Is Nothing Then
                oUser = New DTOUser(DirectCast(oDrd("Guid"), Guid))
            End If
            If Not oUser.IsLoaded Then

                With oUser
                    .emp = New DTOEmp(oDrd("Emp"))
                    .EmailAddress = SQLHelper.GetStringFromDataReader(oDrd("Adr"))
                    .Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                    .Cognoms = SQLHelper.GetStringFromDataReader(oDrd("Cognoms"))
                    .NickName = SQLHelper.GetStringFromDataReader(oDrd("NickName"))
                    If Not IsDBNull(oDrd("Sex")) Then
                        .Sex = oDrd("Sex")
                    End If
                    If Not IsDBNull(oDrd("BirthYea")) Then
                        .BirthYear = oDrd("BirthYea")
                    End If
                    If Not IsDBNull(oDrd("CountryGuid")) Then
                        .Country = New DTOCountry(oDrd("CountryGuid"))
                        .Country.ISO = SQLHelper.GetStringFromDataReader(oDrd("Pais"))
                        .Country.LangNom.Esp = oDrd("CountryEsp")
                        .Country.LangNom.Cat = oDrd("CountryCat")
                        .Country.LangNom.Eng = oDrd("CountryEng")
                    End If
                    If Not IsDBNull(oDrd("ZipCod")) Then
                        .ZipCod = oDrd("ZipCod").ToString
                    End If
                    If Not IsDBNull(oDrd("Location")) Then
                        .Location = New DTOLocation(oDrd("Location"))
                    End If
                    .LocationNom = SQLHelper.GetStringFromDataReader(oDrd("LocationNom"))
                    .ProvinciaNom = SQLHelper.GetStringFromDataReader(oDrd("ProvinciaNom"))
                    .Tel = SQLHelper.GetStringFromDataReader(oDrd("Tel").ToString())
                    .ChildCount = SQLHelper.GetIntegerFromDataReader(oDrd("ChildCount"))
                    .LastChildBirthday = SQLHelper.GetFchFromDataReader(oDrd("LastChildBirthday"))

                    .eFras = Not IsDBNull(oDrd("SscGuid"))
                    .Password = SQLHelper.GetStringFromDataReader(oDrd("Pwd"))
                    .Rol = New DTORol(oDrd("Rol"))
                    .Lang = DTOLang.Factory(oDrd("Lang"))
                    .Source = DirectCast(oDrd("Source"), DTOUser.Sources)
                    If IsDBNull(oDrd("DefaultContactGuid")) Then
                        If Not IsDBNull(oDrd("ContactGuid")) Then
                            .Contact = New DTOContact(oDrd("ContactGuid"))
                        End If
                    Else
                        .Contact = New DTOContact(oDrd("DefaultContactGuid"))
                    End If
                    .NoNews = oDrd("NoNews")
                    If Not IsDBNull(oDrd("BadMailGuid")) Then .BadMail = New DTOCod(oDrd("BadMailGuid"))
                    .Privat = oDrd("Privat")
                    .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                    .Obsoleto = oDrd("Obsoleto")
                    .FchCreated = SQLHelper.GetFchFromDataReader(oDrd("FchCreated"))
                    .FchActivated = SQLHelper.GetFchFromDataReader(oDrd("FchActivated"))
                    .FchDeleted = SQLHelper.GetFchFromDataReader(oDrd("FchDeleted"))
                    .Contacts = New List(Of DTOContact)
                    .IsLoaded = True
                End With
            End If
            If Not IsDBNull(oDrd("ContactGuid")) Then
                Dim oContact As New DTOContact(oDrd("ContactGuid"))
                oContact.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                If Not IsDBNull(oDrd("CliObsoleto")) Then
                    oContact.Obsoleto = oDrd("CliObsoleto")
                End If
                oUser.Contacts.Add(oContact)
            End If
        Loop

        If oUser IsNot Nothing Then
            If oUser.Contact Is Nothing Then
                If oUser.Contacts IsNot Nothing Then
                    If oUser.Contacts.Count > 0 Then
                        oUser.Contact = oUser.Contacts(0)
                    End If
                End If
            End If
            oUser.IsLoaded = True
        End If


    End Sub

    Shared Function Update(oUser As DTOUser, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Update(oUser, oTrans)
            UpdateIdentity(oUser, oTrans)
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

    Shared Function UpdateIdentity(oUser As DTOUser, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            UpdateIdentity(oUser, oTrans)
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


    Shared Sub UpdateIdentity(oUser As DTOUser, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM AspNetUsers WHERE Id='" & oUser.Guid.ToString & "'"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Id") = oUser.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oUser
            oRow("UserName") = .EmailAddress
            oRow("NormalizedUserName") = .EmailAddress.ToUpper
            oRow("Email") = .EmailAddress
            oRow("NormalizedEmail") = .EmailAddress.ToUpper
            oRow("PasswordHash") = .HashIdentityPassword()
            oRow("SecurityStamp") = Guid.NewGuid().ToString()
            oRow("ConcurrencyStamp") = Guid.NewGuid().ToString()
            oRow("EmailConfirmed") = True
            oRow("PhoneNumberConfirmed") = False
            oRow("TwoFactorEnabled") = False
            oRow("LockoutEnabled") = True
            oRow("AccessFailedCount") = 0
        End With
        oDA.Update(oDs)
    End Sub



    Shared Sub Update(oUser As DTOUser, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM Email WHERE Guid='" & oUser.Guid.ToString & "'"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oUser.Guid
            oRow("Emp") = oUser.Emp.Id
        Else
            oRow = oTb.Rows(0)
        End If

        With oUser
            oRow("Adr") = .EmailAddress
            oRow("Nom") = IIf(.Nom = "-", "", .Nom)
            oRow("Cognoms") = IIf(.Cognoms = "-", "", .Cognoms)
            oRow("NickName") = .NickName
            oRow("Sex") = .Sex
            If .Rol IsNot Nothing Then
                If .Rol.Id <> DTORol.Ids.NotSet Then
                    oRow("Rol") = .Rol.Id
                End If
            End If
            If .Password = "" Then .Password = TextHelper.RandomString(6)
            oRow("Pwd") = .Password
            oRow("Hash") = Helpers.CryptoHelper.Hash(.EmailAddress, .Password)
            If .Lang IsNot Nothing Then
                oRow("Lang") = .Lang.Id.ToString
            End If
            oRow("Source") = .Source
            oRow("tel") = .Tel

            oRow("address") = .Adr
            oRow("zipcod") = .ZipCod
            If .Location IsNot Nothing Then
                oRow("Location") = .Location.Guid
            End If
            oRow("LocationNom") = .LocationNom
            oRow("ProvinciaNom") = .ProvinciaNom
            If .Country IsNot Nothing Then
                oRow("pais") = .Country.ISO
            Else
                If .Location IsNot Nothing Then
                    If .Location.Zona IsNot Nothing Then
                        If .Location.Zona.Country IsNot Nothing Then
                            oRow("pais") = .Location.Zona.Country.ISO
                        End If
                    End If
                End If
            End If
            If .Birthday <> Nothing Then
                oRow("Birthday") = .Birthday
                .BirthYear = .Birthday.Year
            End If
            oRow("BirthYea") = .BirthYear
            If .ChildCount > -1 Then
                oRow("ChildCount") = .ChildCount
            End If
            If .LastChildBirthday <> Nothing Then
                oRow("LastChildBirthday") = .LastChildBirthday
            End If
            If .Contact IsNot Nothing Then
                oRow("DefaultContactGuid") = .Contact.Guid
            End If

            oRow("NoNews") = .NoNews
            oRow("Obs") = SQLHelper.NullableString(.Obs)
            oRow("Obsoleto") = .Obsoleto
            oRow("BadMailGuid") = SQLHelper.NullableBaseGuid(.BadMail)
            If .FchCreated <> Nothing Then
                oRow("FchCreated") = .FchCreated
            End If

            oRow("FchActivated") = SQLHelper.NullableFch(.FchActivated)
            oRow("FchDeleted") = SQLHelper.NullableFch(.FchDeleted)
        End With



        oDA.Update(oDs)
    End Sub

    Shared Function UpdateContacts(oUser As DTOUser, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            UpdateContacts(oUser, oTrans)
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

    Shared Sub UpdateContacts(oUser As DTOUser, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Email_Clis WHERE EmailGuid = '" & oUser.Guid.ToString() & "'"
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
        For idx As Integer = 0 To oUser.Contacts.Count - 1
            SQL = "INSERT INTO Email_Clis (EmailGuid, ContactGuid, Ord) VALUES('" & oUser.Guid.ToString() & "','" & oUser.Contacts(idx).Guid.ToString() & "'," & idx & ") "
            SQLHelper.ExecuteNonQuery(SQL, oTrans)
        Next
    End Sub

    Shared Function Delete(oUser As DTOUser, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            DeleteIdentity(oUser, oTrans)
            Delete(oUser, oTrans)
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

    Shared Sub DeleteIdentity(oUser As DTOUser, ByRef oTrans As SqlTransaction)
        With oUser
            Dim SQL As String = "DELETE AspNetUsers WHERE Id=@Id"
            SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Id", oUser.Guid.ToString())
        End With
    End Sub


    Shared Sub Delete(oUser As DTOUser, ByRef oTrans As SqlTransaction)
        With oUser
            Dim SQL As String = "DELETE Email WHERE Guid=@Guid"
            SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oUser.Guid.ToString())
        End With
    End Sub


    Shared Sub LoadContacts(ByRef oUser As DTOUser)
        oUser.Contacts = New List(Of DTOContact)

        Dim SQL As String = "SELECT CliGral.Guid, CliGral.FullNom, CliGral.Obsoleto " _
        & "FROM EMAIL_CLIS " _
        & "INNER JOIN CliGral ON EMAIL_CLIS.ContactGuid=CliGral.Guid  " _
        & "WHERE EMAIL_CLIS.EmailGuid=@Guid " _
        & "ORDER BY CliGral.Obsoleto, CliGral.FullNom"

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Guid", oUser.Guid.ToString())
        Do While oDrd.Read
            Dim oGuid As Guid = oDrd("Guid")
            Dim oContact As New DTOContact(oGuid)
            oContact.FullNom = oDrd("FullNom").ToString
            oContact.Obsoleto = oDrd("Obsoleto")
            oUser.Contacts.Add(oContact)
        Loop
        oDrd.Close()
    End Sub

    Shared Function Customers(oUser As DTOUser) As List(Of DTOCustomer)
        Dim retval As New List(Of DTOCustomer)
        Dim sb As New System.Text.StringBuilder
        sb.Append("SELECT EMAIL_CLIS.ContactGuid, CliGral.RaoSocial, CliGral.NomCom ")
        sb.Append(", CliGral.NIF, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod ")
        sb.Append(", VwAddress.* ")
        sb.Append(", CliClient.NoRep, CliClient.NoIncentius, CliClient.OrdersToCentral, CliClient.CcxGuid ")
        sb.Append("FROM EMAIL_CLIS ")
        sb.Append("INNER JOIN CliGral ON EMAIL_CLIS.ContactGuid=CliGral.Guid ")
        sb.Append("INNER JOIN CliClient ON CliGral.Guid=CliClient.Guid ")
        sb.Append("INNER JOIN VwAddress ON CliGral.Guid = VwAddress.SrcGuid ")
        sb.Append("WHERE EMAIL_CLIS.EmailGuid=@Guid ")
        sb.Append("AND CliGral.Obsoleto=0 ")
        sb.Append("AND (CliGral.Rol =" & DTORol.Ids.CliFull & " OR CliGral.Rol =" & DTORol.Ids.CliLite & ") ")
        sb.Append("ORDER BY CliGral.RaoSocial")
        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Guid", oUser.Guid.ToString())
        Do While oDrd.Read
            Dim oCustomer = New DTOCustomer(oDrd("ContactGuid"))
            With oCustomer
                .Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
                .Nom = oDrd("RaoSocial")
                .NomComercial = oDrd("NomCom")
                .Address = SQLHelper.GetAddressFromDataReader(oDrd)
                .NoRep = oDrd("NoRep")
                .OrdersToCentral = oDrd("OrdersToCentral")
                .NoIncentius = oDrd("NoIncentius")
                If Not IsDBNull(oDrd("CcxGuid")) Then
                    .Ccx = New DTOCustomer(oDrd("CcxGuid"))
                End If
            End With

            retval.Add(oCustomer)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function GetProveidors(oUser As DTOUser) As List(Of DTOProveidor)
        Dim retval As New List(Of DTOProveidor)
        Dim sb As New System.Text.StringBuilder
        sb.Append("SELECT EMAIL_CLIS.ContactGuid, CliGral.RaoSocial, CliGral.NomCom ")
        sb.Append(", CliGral.NIF, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod ")
        sb.Append(", VwAddress.* ")
        sb.Append("FROM EMAIL_CLIS ")
        sb.Append("INNER JOIN CliGral ON EMAIL_CLIS.ContactGuid=CliGral.Guid ")
        sb.Append("INNER JOIN CliPrv ON CliGral.Guid=CliPrv.Guid ")
        sb.Append("INNER JOIN VwAddress ON CliGral.Guid = VwAddress.SrcGuid ")
        sb.Append("WHERE EMAIL_CLIS.EmailGuid=@Guid ")
        sb.Append("AND CliGral.Obsoleto=0 AND CliGral.Rol = " & CInt(DTORol.Ids.Manufacturer) & " ")
        sb.Append("ORDER BY CliGral.RaoSocial")
        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Guid", oUser.Guid.ToString())
        Do While oDrd.Read
            Dim oProveidor = New DTOProveidor(oDrd("ContactGuid"))
            With oProveidor
                .Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
                .Nom = oDrd("RaoSocial")
                .NomComercial = oDrd("NomCom")
                .Address = SQLHelper.GetAddressFromDataReader(oDrd)
            End With

            retval.Add(oProveidor)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function BrandManufacturers(oUser As DTOUser) As List(Of DTOProveidor)
        Dim retval As New List(Of DTOProveidor)
        Dim sb As New System.Text.StringBuilder
        sb.Append("SELECT EMAIL_CLIS.ContactGuid, CliGral.RaoSocial, CliGral.NomCom ")
        sb.Append(", CliGral.NIF, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod ")
        sb.Append(", VwAddress.* ")
        sb.Append("FROM EMAIL_CLIS ")
        sb.Append("INNER JOIN Tpa ON Tpa.Proveidor=EMAIL_CLIS.ContactGuid ")
        sb.Append("INNER JOIN CliGral ON EMAIL_CLIS.ContactGuid=CliGral.Guid ")
        sb.Append("INNER JOIN VwAddress ON CliGral.Guid = VwAddress.SrcGuid ")
        sb.Append("WHERE EMAIL_CLIS.EmailGuid='" & oUser.Guid.ToString & "' ")
        sb.Append("AND CliGral.Obsoleto=0 AND CliGral.Rol = " & CInt(DTORol.Ids.Manufacturer) & " ")
        sb.Append("ORDER BY CliGral.RaoSocial")
        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oCustomer = New DTOProveidor(oDrd("ContactGuid"))
            With oCustomer
                .Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
                .Nom = oDrd("RaoSocial")
                .NomComercial = oDrd("NomCom")
                .Address = SQLHelper.GetAddressFromDataReader(oDrd)
            End With

            retval.Add(oCustomer)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function UpdatePassword(oUser As DTOUser) As Boolean
        Dim SQL As String = "UPDATE Email SET Pwd=@Pwd, Hash=@Hash WHERE Guid=@Guid"
        Dim exs As New List(Of Exception)
        SQLHelper.ExecuteNonQuery(SQL, exs, "@Guid", oUser.Guid.ToString, "@Pwd", oUser.Password, "@Hash", Helpers.CryptoHelper.Hash(oUser.EmailAddress, oUser.Password))
        Return True
    End Function

    Shared Function Contacts(oUser As DTOUser) As List(Of DTOContact)
        Dim retval As New List(Of DTOContact)

        If oUser IsNot Nothing Then
            Dim SQL As String = "SELECT Email_Clis.ContactGuid, CliGral.FullNom,CliGral.Obsoleto " _
        & ", CliGral.Emp, CliGral.Rol, CliGral.Nif, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod, VwAddress.* " _
        & "FROM Email_Clis " _
        & "INNER JOIN VwAddress ON Email_Clis.ContactGuid = VwAddress.SrcGuid " _
        & "INNER JOIN CliGral ON Email_Clis.ContactGuid = CliGral.Guid " _
        & "WHERE Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' " _
        & "ORDER BY CliGral.Obsoleto, CliGral.FullNom"

            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                Dim oContact As New DTOContact(oDrd("ContactGuid"))
                With oContact
                    .Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
                    .Address = SQLHelper.GetAddressFromDataReader(oDrd)
                    .FullNom = oDrd("FullNom")
                    .emp = New DTOEmp
                    .emp.Id = oDrd("Emp")
                    .rol = New DTORol(oDrd("Rol"))
                    .Obsoleto = oDrd("Obsoleto")
                End With
                retval.Add(oContact)
            Loop
            oDrd.Close()

        End If
        Return retval
    End Function

    Shared Function GetRep(oUser As DTOUser) As DTORep
        Dim retval As DTORep = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Email_Clis.ContactGuid, CliRep.Abr, CliGral.Cli, CliGral.Rol, CliGral.LangId ")
        sb.AppendLine("FROM Email_Clis ")
        sb.AppendLine("INNER JOIN CliRep ON Email_Clis.ContactGuid = CliRep.Guid ")
        sb.AppendLine("INNER JOIN CliGral ON CliRep.Guid = CliGral.Guid ")
        sb.AppendLine("WHERE Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTORep(oDrd("ContactGuid"))
            With retval
                .Nom = oDrd("Abr")
                .Id = oDrd("Cli")
                .Rol = New DTORol(oDrd("Rol"))
                .Lang = DTOLang.Factory(oDrd("LangId"))
            End With
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function GetStaff(oUser As DTOUser) As DTOStaff
        Dim retval As DTOStaff = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Email_Clis.ContactGuid ")
        sb.AppendLine(", CliStaff.Abr, CliStaff.Teletrabajo ")
        sb.AppendLine(", CliGral.Rol, CliGral.LangId, CliGral.RaoSocial ")
        sb.AppendLine("FROM Email_Clis ")
        sb.AppendLine("INNER JOIN CliStaff ON Email_Clis.ContactGuid = CliStaff.Guid ")
        sb.AppendLine("INNER JOIN CliGral ON CliStaff.Guid = CliGral.Guid ")
        sb.AppendLine("WHERE Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOStaff(oDrd("ContactGuid"))
            With retval
                .Nom = oDrd("RaoSocial")
                .Abr = oDrd("Abr")
                .Rol = New DTORol(oDrd("Rol"))
                .Teletrabajo = oDrd("Teletrabajo")
                .Lang = DTOLang.Factory(oDrd("LangId"))
            End With
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function GetCustomers(oUser As DTOUser) As List(Of DTOCustomer)
        Dim retval As New List(Of DTOCustomer)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Email_Clis.ContactGuid, CliGral.FullNom, CliGral.RaoSocial, CliGral.NomCom ")
        sb.AppendLine(", CliGral.NIF, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod ")
        sb.AppendLine(", VwAddress.* ")
        sb.AppendLine("FROM Email_Clis ")
        sb.AppendLine("INNER JOIN CliClient ON Email_Clis.ContactGuid = CliClient.Guid ")
        sb.AppendLine("INNER JOIN VwAddress ON Email_Clis.ContactGuid = VwAddress.SrcGuid ")
        sb.AppendLine("INNER JOIN CliGral ON CliClient.Guid = CliGral.Guid ")
        sb.AppendLine("WHERE Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' AND CliGral.Obsoleto=0 ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOCustomer(oDrd("ContactGuid"))
            With item
                .Nom = SQLHelper.GetStringFromDataReader(oDrd("RaoSocial"))
                .NomComercial = SQLHelper.GetStringFromDataReader(oDrd("NomCom"))
                .FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                .Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
                .Address = SQLHelper.GetAddressFromDataReader(oDrd)
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function GuessFromFirstLetters(src As String) As DTOUser
        Dim retval As DTOUser = Nothing
        Dim SQL As String = "SELECT TOP 1 Guid, Adr FROM Email WHERE Adr LIKE '" & src & "%' ORDER BY Adr"
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOUser(DirectCast(oDrd("Guid"), Guid))
            With retval
                .EmailAddress = oDrd("Adr")
            End With
        End If
        oDrd.Close()
        Return retval
    End Function

End Class

Public Class UsersLoader

    Shared Function Professionals(oEmp As DTOEmp) As List(Of DTOContact)
        Dim retval As New List(Of DTOContact)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Email.Guid, Email.Adr, Email.Rol ")
        sb.AppendLine(", CliGral.RaoSocial, CliGral.ContactClass ")
        sb.AppendLine(", VwAddress.* ")
        sb.AppendLine(", Email_Clis.ContactGuid ")
        sb.AppendLine(", SscEmail.SscGuid ")
        sb.AppendLine("FROM CliGral ")
        sb.AppendLine("INNER JOIN Email_Clis ON CliGral.Guid = Email_Clis.ContactGuid ")
        sb.AppendLine("INNER JOIN Email ON Email_Clis.EmailGuid = Email.Guid ")
        sb.AppendLine("INNER JOIN VwAddress ON CliGral.Guid = VwAddress.SrcGuid ")
        sb.AppendLine("LEFT OUTER JOIN SscEmail ON Email.Guid = SscEmail.Email ")
        sb.AppendLine("WHERE CliGral.Emp=" & oEmp.Id & " ")
        sb.AppendLine("AND CliGral.Obsoleto=0 ")
        sb.AppendLine("AND Email.BadMailGuid IS NULL ")
        sb.AppendLine("AND Email.NoNews=0 ")
        sb.AppendLine("AND Email.Obsoleto=0 ")
        sb.AppendLine("ORDER BY VwAddress.CountryISO, VwAddress.ZonaNom, VwAddress.LocationNom, CliGral.RaoSocial, CliGral.Guid, Email.Adr, Email.Guid, SscEmail.Ssc")

        Dim oUser As New DTOUser
        Dim oContact As New DTOContact
        Dim SQL As String = sb.ToString
        Dim oTels As List(Of DTOUser) = Nothing
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oContact.Guid.Equals(oDrd("ContactGuid")) Then
                oContact = New DTOContact(oDrd("Guid"))
                With oContact
                    .Nom = oDrd("RaoSocial")
                    .Address = SQLHelper.GetAddressFromDataReader(oDrd)
                    If Not IsDBNull(oDrd("ContactClass")) Then
                        .ContactClass = New DTOContactClass(oDrd("ContactClass"))
                    End If
                End With
                retval.Add(oContact)
                oUser = New DTOUser
            End If
            If Not oUser.Guid.Equals(oDrd("Guid")) Then
                oUser = New DTOUser(oDrd("Guid"))
                With oUser
                    .EmailAddress = SQLHelper.GetStringFromDataReader(oDrd("Adr"))
                    .Rol = New DTORol(SQLHelper.GetIntegerFromDataReader(oDrd("Rol")))
                End With
                oContact.Emails.Add(oUser)
            End If
            If Not IsDBNull(oDrd("SscGuid")) Then
                Dim oSsc As New DTOSubscription(CType(oDrd("SscGuid"), Guid))
                oUser.subscriptions.Add(oSsc)
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oEmp As DTOEmp, Optional IncludeObsolets As Boolean = False) As List(Of DTOUser)
        Dim retval As New List(Of DTOUser)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Email.Guid, Email.Adr, Email.Rol, Email.Nom, Email.Cognoms, Email.Birthyea, Email.Tel, Email.Source, Email.FchCreated, Email.FchDeleted, Country.Guid as CountryGuid, Email.Pais ")
        sb.AppendLine("FROM Email ")
        sb.AppendLine("LEFT OUTER JOIN Country ON Email.Pais = Country.ISO ")
        sb.AppendLine("WHERE Email.Emp=" & oEmp.Id & " ")
        If Not IncludeObsolets Then
            sb.Append("AND Email.Obsoleto=0 ")
        End If
        sb.AppendLine("ORDER BY Email.FchCreated DESC")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oUser As New DTOUser(DirectCast(oDrd("Guid"), Guid))
            With oUser
                .EmailAddress = oDrd("Adr")
                .Rol = New DTORol(oDrd("Rol"))
                .Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                .Cognoms = SQLHelper.GetStringFromDataReader(oDrd("Cognoms"))
                If Not IsDBNull(oDrd("CountryGuid")) Then
                    .Country = New DTOCountry(oDrd("CountryGuid"))
                    .Country.ISO = oDrd("Pais")
                End If
                .BirthYear = SQLHelper.GetIntegerFromDataReader(oDrd("Birthyea"))
                .Tel = SQLHelper.GetStringFromDataReader(oDrd("Tel"))
                .Source = SQLHelper.GetIntegerFromDataReader(oDrd("Source"))
                .FchCreated = SQLHelper.GetFchFromDataReader(oDrd("FchCreated"))
                .FchDeleted = SQLHelper.GetFchFromDataReader(oDrd("FchDeleted"))
            End With
            retval.Add(oUser)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oContact As DTOContact, Optional IncludeObsolets As Boolean = False) As List(Of DTOUser)
        Dim retval As New List(Of DTOUser)
        Dim oSubscription = DTOSubscription.Wellknown(DTOSubscription.Wellknowns.Facturacio)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Email_Clis.EmailGuid, Email.FchCreated, Email.Nom, Email.Cognoms, Email.Nickname ")
        sb.AppendLine(", Email.Rol, Email.Lang, Email.FchCreated, Email.Adr ")
        sb.AppendLine(", Email.Privat, Email.BadMailGuid, Email.NoNews, Email.Obsoleto, SscEmail.SscGuid ")
        sb.AppendLine("FROM Email_Clis ")
        sb.AppendLine("INNER JOIN Email ON Email_Clis.EmailGuid = Email.Guid ")
        sb.AppendLine("LEFT OUTER JOIN SscEmail ON Email.Guid= SscEmail.Email AND SscEmail.SscGuid = '" & oSubscription.Guid.ToString() & "' ")
        sb.AppendLine("WHERE Email_Clis.ContactGuid = '" & oContact.Guid.ToString & "' ")
        If Not IncludeObsolets Then
            sb.Append("AND Email.Obsoleto=0 AND Email.BadmailGuid IS NULL ")
        End If
        sb.AppendLine("ORDER BY Email.FchCreated")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oUser As New DTOUser(DirectCast(oDrd("EmailGuid"), Guid))
            With oUser
                .emailAddress = oDrd("Adr")
                .nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                .cognoms = SQLHelper.GetStringFromDataReader(oDrd("Cognoms"))
                .nickName = SQLHelper.GetStringFromDataReader(oDrd("NickName"))
                .nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                .EFras = Not IsDBNull(oDrd("SscGuid"))
                If Not IsDBNull(oDrd("BadmailGuid")) Then .BadMail = New DTOCod(oDrd("BadMailGuid"))
                .Privat = oDrd("Privat")
                .noNews = oDrd("NoNews")
                .obsoleto = oDrd("Obsoleto")
                .rol = New DTORol(oDrd("Rol"))
                .lang = DTOLang.Factory(oDrd("Lang"))
                .fchCreated = SQLHelper.GetFchFromDataReader(oDrd("FchCreated"))
            End With
            retval.Add(oUser)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Search(oEmp As DTOEmp, searchKey As String) As List(Of DTOUser)
        Dim retval As New List(Of DTOUser)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Email.Guid, Email.FchCreated, Email.Nom, Email.Cognoms, Email.Nickname, Email.Rol, Email.Lang, Email.FchCreated, Email.Adr ")
        sb.AppendLine("FROM Email ")
        sb.AppendLine("WHERE Emp = " & oEmp.Id & " ")
        sb.AppendLine("AND Email.Adr LIKE '%" & searchKey & "%' ")
        sb.AppendLine("ORDER BY Email.Adr")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oUser As New DTOUser(DirectCast(oDrd("Guid"), Guid))
            With oUser
                .EmailAddress = oDrd("Adr")
                .Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                .Cognoms = SQLHelper.GetStringFromDataReader(oDrd("Cognoms"))
                .NickName = SQLHelper.GetStringFromDataReader(oDrd("NickName"))
                .Rol = New DTORol(oDrd("Rol"))
                .Lang = DTOLang.Factory(oDrd("Lang"))
                .FchCreated = SQLHelper.GetFchFromDataReader(oDrd("FchCreated"))
            End With
            retval.Add(oUser)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function SearchCount(oEmp As DTOEmp, searchKey As String) As Integer
        Dim retval As Integer
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT COUNT(Guid) AS UsersCount ")
        sb.AppendLine("FROM Email ")
        sb.AppendLine("WHERE Emp = " & oEmp.Id & " ")
        sb.AppendLine("AND Email.Adr LIKE '%" & searchKey & "%' ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = SQLHelper.GetIntegerFromDataReader(oDrd("UsersCount"))
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function Search(searchKey As DTOUser) As List(Of DTOUser)
        Dim retval As New List(Of DTOUser)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Email.Guid, Email.FchCreated, Email.Nom, Email.Cognoms, Email.Nickname, Email.Rol, Email.Lang, Email.FchCreated, Email.Adr ")
        sb.AppendLine("FROM Email ")
        sb.AppendLine("WHERE 1=1 ")
        If searchKey.EmailAddress > "" Then
            sb.Append("AND Email.Adr LIKE '%" & searchKey.EmailAddress & "%' ")
        End If
        If searchKey.Nom > "" Then
            sb.Append("AND Email.Nom LIKE '%" & searchKey.Nom & "%' ")
        End If
        If searchKey.Cognoms > "" Then
            sb.Append("AND Email.Cognoms LIKE '%" & searchKey.Cognoms & "%' ")
        End If
        If searchKey.NickName > "" Then
            sb.Append("AND Email.NickName LIKE '%" & searchKey.NickName & "%' ")
        End If
        sb.AppendLine("ORDER BY Email.Adr")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oUser As New DTOUser(DirectCast(oDrd("Guid"), Guid))
            With oUser
                .EmailAddress = oDrd("Adr")
                .Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                .Cognoms = SQLHelper.GetStringFromDataReader(oDrd("Cognoms"))
                .NickName = SQLHelper.GetStringFromDataReader(oDrd("NickName"))
                .Rol = New DTORol(oDrd("Rol"))
                .Lang = DTOLang.Factory(oDrd("Lang"))
                .FchCreated = SQLHelper.GetFchFromDataReader(oDrd("FchCreated"))
            End With
            retval.Add(oUser)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class

