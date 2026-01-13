Public Class ContactLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOContact
        Dim retval As DTOContact = Nothing
        Dim oContact As New DTOContact(oGuid)
        If Load(oContact) Then
            retval = oContact
        End If
        Return retval

    End Function


    Shared Function FromNum(oEmp As DTOEmp, iNum As Integer) As DTOContact
        Dim retval As DTOContact = Nothing
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT CliGral.Guid, CliGral.RaoSocial, CliGral.FullNom ")
        sb.AppendLine("FROM CliGral ")
        sb.AppendLine("WHERE CliGral.Emp = " & oEmp.Id & " ")
        sb.AppendLine("AND CliGral.Cli=" & iNum & " ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOContact(oDrd("Guid"))
            With retval
                .Emp = oEmp
                .Id = iNum
                .Nom = oDrd("RaoSocial")
                .FullNom = oDrd("FullNom")
            End With
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function FromGln(oGln As DTOEan) As DTOContact
        Dim retval As DTOContact = Nothing
        Dim SQL As String = "SELECT CliGral.Guid, CliGral.RaoSocial FROM CliGral WHERE Gln='" & oGln.value & "' "
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOContact(oDrd("Guid"))
            With retval
                .nom = oDrd("RaoSocial")
                .GLN = oGln
            End With
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function FromNif(oEmp As DTOEmp, nif As String) As DTOContact
        Dim retval As DTOContact = Nothing
        Dim SQL As String = "SELECT CliGral.Guid, CliGral.RaoSocial, Nif, NifCod, Nif2, Nif2Cod FROM CliGral WHERE Emp=" & oEmp.Id & " AND (Nif='" & nif & "' OR Nif2 = '" & nif & "') "
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOContact(oDrd("Guid"))
            With retval
                .Nom = oDrd("RaoSocial")
                .Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
            End With
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function Load(ByRef oContact As DTOContact) As Boolean

        If oContact IsNot Nothing AndAlso Not oContact.IsLoaded Then
            Dim oPaymentTerms As New DTOPaymentTerms

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT CliGral.*, ContactClass.Esp AS ContactClassNom, ContactClass.DistributionChannel ")
            sb.AppendLine(", VwAddress.* ")
            sb.AppendLine(", CliAnterior.FullNom AS CliAnteriorFullNom, CliNou.FullNom AS CliNouFullNom ")
            sb.AppendLine(", VwTel.TelNum ")
            sb.AppendLine(", UsrRols.Nom AS RolEsp, UsrRols.Nom_Cat AS RolCat, UsrRols.Nom_Eng AS RolEng, UsrRols.Nom_Por AS RolRor ")
            sb.AppendLine("FROM CliGral ")
            sb.AppendLine("LEFT OUTER JOIN UsrRols ON CliGral.Rol = UsrRols.Rol ")
            'sb.AppendLine("LEFT OUTER JOIN CliAdr ON CliAdr.SrcGuid = CliGral.Guid AND CliAdr.Cod = 1 ")
            sb.AppendLine("LEFT OUTER JOIN VwAddress ON CliGral.Guid = VwAddress.SrcGuid ")
            sb.AppendLine("LEFT OUTER JOIN VwTel ON CliGral.Guid = VwTel.Contact ")
            sb.AppendLine("LEFT OUTER JOIN ContactClass ON CliGral.ContactClass = ContactClass.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CliGral CliAnterior ON CliGral.NomAnteriorGuid = CliAnterior.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CliGral CliNou ON CliGral.NomNouGuid = CliNou.Guid ")
            sb.AppendLine("WHERE CliGral.Guid='" & oContact.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then

                'Dim oAddress As DTOAddress = SQLHelper.GetAddressFromDataReader(oDrd)
                'With oAddress
                '.ViaNom = SQLHelper.GetStringFromDataReader(oDrd("AdrViaNom"))
                '.Num = SQLHelper.GetStringFromDataReader(oDrd("AdrNum"))
                '.Coordenadas = New GeoHelper.Coordenadas(SQLHelper.GetDoubleFromDataReader(oDrd("Latitud")), SQLHelper.GetDoubleFromDataReader(oDrd("Longitud")))
                'End With

                With oContact
                    If .Emp Is Nothing Then .Emp = New DTOEmp(oDrd("Emp"))
                    .Id = oDrd("Cli")
                    .Nom = oDrd("RaoSocial")
                    .NomComercial = SQLHelper.GetStringFromDataReader(oDrd("NomCom"))
                    .SearchKey = SQLHelper.GetStringFromDataReader(oDrd("NomKey"))
                    .FullNom = oDrd("FullNom")
                    .Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
                    .Lang = DTOLang.Factory(oDrd("LangId"))
                    .Rol = New DTORol(oDrd("Rol"))
                    .Rol.nom = SQLHelper.GetLangTextFromDataReader(oDrd, "RolEsp", "RolCat", "RolEng", "RolPor")
                    If Not IsDBNull(oDrd("NomAnteriorGuid")) Then
                        .NomAnterior = New DTOContact(oDrd("NomAnteriorGuid"))
                        .NomAnterior.FullNom = SQLHelper.GetStringFromDataReader(oDrd("CliAnteriorFullNom"))
                    End If
                    If Not IsDBNull(oDrd("NomNouGuid")) Then
                        .NomNou = New DTOContact(oDrd("NomNouGuid"))
                        .NomNou.FullNom = SQLHelper.GetStringFromDataReader(oDrd("CliNouFullNom"))
                    End If
                    .Website = SQLHelper.GetStringFromDataReader(oDrd("Web"))
                    .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                    .Obsoleto = oDrd("Obsoleto")
                    .Address = SQLHelper.GetAddressFromDataReader(oDrd)
                    .Telefon = SQLHelper.GetStringFromDataReader(oDrd("TelNum"))
                    .GLN = SQLHelper.GetEANFromDataReader(oDrd("GLN"))
                    If Not IsDBNull(oDrd("ContactClass")) Then
                        .ContactClass = New DTOContactClass(oDrd("ContactClass"))
                        .ContactClass.Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "ContactClassNom", "ContactClassNom", "ContactClassNom", "ContactClassNom")
                        If Not IsDBNull(oDrd("DistributionChannel")) Then
                            .ContactClass.DistributionChannel = New DTODistributionChannel(oDrd("DistributionChannel"))
                        End If
                    End If
                End With
            End If
            oDrd.Close()

            oContact.Tels = ContactLoader.Tels(oContact)
            oContact.Emails = UsersLoader.All(oContact, True)
            oContact.ContactPersons = ContactLoader.SubContacts(oContact)
            oContact.IsLoaded = True
        End If

        Return oContact IsNot Nothing AndAlso oContact.IsLoaded
    End Function

    Shared Function Update(oContact As DTOContact, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Update(oContact, oTrans)
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

    Shared Sub Update(oContact As DTOContact, ByRef oTrans As SqlTransaction)
        UpdateHeader(oContact, oTrans)
        UpdateClls(oContact, oTrans)
        If oContact.ContactPersons IsNot Nothing Then
            UpdateContactPersons(oContact, oTrans)
        End If
        If oContact.Tels IsNot Nothing Then
            UpdateTels(oContact, oTrans)
        End If
        If oContact.Emails IsNot Nothing Then
            UpdateEmails(oContact, oTrans)
        End If
        If oContact.Address IsNot Nothing Then
            oContact.Address.Src = oContact
            oContact.Address.Codi = DTOAddress.Codis.Fiscal
            AddressLoader.Update(oContact.Address, oTrans)
        End If
    End Sub

    Shared Sub UpdateClls(oContact As DTOContact, ByRef oTrans As SqlTransaction)
        DeleteClls(oContact, oTrans)
        UpdateCll(oContact.Nom, oContact, DTOContact.ContactKeys.Nom, oTrans)
        If oContact.Address IsNot Nothing Then
            If oContact.Address.Zip IsNot Nothing Then
                If oContact.Address.Zip.Location IsNot Nothing Then
                    UpdateCll(oContact.Address.Zip.Location.Nom, oContact, DTOContact.ContactKeys.Poblacio, oTrans)
                End If
            End If
        End If
        If oContact.NomComercial > "" Then
            UpdateCll(oContact.NomComercial, oContact, DTOContact.ContactKeys.Comercial, oTrans)
        End If
        If oContact.SearchKey > "" Then
            UpdateCll(oContact.SearchKey, oContact, DTOContact.ContactKeys.SearchKey, oTrans)
        End If
    End Sub

    Shared Sub UpdateCll(ByVal sKey As String, oContact As DTOContact, ByVal oCode As DTOContact.ContactKeys, ByRef oTrans As SqlTransaction)
        sKey = sKey.Replace("'", "´")
        sKey = Left(sKey, 40)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("INSERT INTO Cll(Cll, ContactGuid, Cod) ")
        sb.AppendLine("VALUES (")
        sb.AppendLine("'" & sKey & "', ")
        sb.AppendLine("'" & oContact.Guid.ToString & "', ")
        sb.AppendLine(oCode)
        sb.AppendLine(") ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub UpdateContactPersons(oContact As DTOContact, ByRef oTrans As SqlTransaction)
        If Not oContact.IsNew Then DeleteContactPersons(oContact, oTrans)

        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM CliContact ")
        sb.AppendLine("WHERE ContactGuid='" & oContact.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim idx As Integer
        Dim oTb As DataTable = oDs.Tables(0)
        For Each item As String In oContact.ContactPersons
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            idx += 1
            oRow("ContactGuid") = oContact.Guid
            oRow("id") = idx
            oRow("Contact") = Left(item, 60)
        Next
        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateTels(oContact As DTOContact, ByRef oTrans As SqlTransaction)
        If Not oContact.IsNew Then DeleteTels(oContact, oTrans)

        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM CliTel ")
        sb.AppendLine("WHERE CliGuid='" & oContact.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim idx As Integer
        Dim oTb As DataTable = oDs.Tables(0)
        For Each item As DTOContactTel In oContact.Tels
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            idx += 1
            oRow("CliGuid") = oContact.Guid
            oRow("Country") = item.Country.Guid
            oRow("Num") = item.Value
            oRow("Obs") = SQLHelper.NullableString(item.Obs)
            oRow("Cod") = item.Cod
            oRow("Privat") = item.Privat
            oRow("Ord") = idx
        Next
        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateEmails(oContact As DTOContact, ByRef oTrans As SqlTransaction)
        If Not oContact.IsNew Then DeleteEmails(oContact, oTrans)

        For Each item As DTOUser In oContact.Emails
            UserLoader.Update(item, oTrans)
        Next

        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Email_Clis ")
        sb.AppendLine("WHERE ContactGuid ='" & oContact.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim idx As Integer
        Dim oTb As DataTable = oDs.Tables(0)
        For Each item As DTOUser In oContact.Emails
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            idx += 1
            oRow("ContactGuid") = oContact.Guid
            oRow("EmailGuid") = item.Guid
            oRow("Ord") = idx
        Next

        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateHeader(oContact As DTOContact, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM CliGral ")
        sb.AppendLine("WHERE Guid='" & oContact.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oContact.Guid
            If oContact.Id = 0 Then
                oContact.Id = NextId(oContact.Emp, oTrans)
            End If
            oRow("Emp") = oContact.Emp.Id
            oRow("Cli") = oContact.Id
        Else
            oRow = oTb.Rows(0)
        End If

        With oContact
            oRow("ContactClass") = SQLHelper.NullableBaseGuid(.ContactClass)
            oRow("RaoSocial") = .Nom
            oRow("NomCom") = .NomComercial
            oRow("NomKey") = SQLHelper.NullableString(.SearchKey)
            oRow("FullNom") = .FullNom
            SQLHelper.SetNullableNifs(oRow, .Nifs)
            oRow("GLN") = SQLHelper.NullableEan(.GLN)
            oRow("LangId") = .Lang.Tag.ToUpper
            oRow("Rol") = .Rol.id
            oRow("ContactClass") = SQLHelper.NullableBaseGuid(.ContactClass)
            oRow("Web") = SQLHelper.NullableString(.Website)
            oRow("NomAnteriorGuid") = SQLHelper.NullableBaseGuid(.NomAnterior)
            oRow("NomNouGuid") = SQLHelper.NullableBaseGuid(.NomNou)
            oRow("Obs") = SQLHelper.NullableString(.Obs)
            oRow("Obsoleto") = .Obsoleto

        End With

        oDA.Update(oDs)
    End Sub

    Shared Function NextId(oEmp As DTOEmp, ByRef oTrans As SqlTransaction) As Integer
        Dim retval As Integer = 1
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT MAX(Cli) AS LastId FROM CliGral WHERE Emp=" & oEmp.Id & " ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        If oTb.Rows.Count > 0 Then
            Dim oRow As DataRow = oTb.Rows(0)
            If Not IsDBNull(oRow("LastId")) Then
                retval = CInt(oRow("LastId")) + 1
            End If
        End If
        Return retval
    End Function

    Shared Function Delete(oContact As DTOContact, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oContact, oTrans)
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


    Shared Sub Delete(oContact As DTOContact, ByRef oTrans As SqlTransaction)
        DeleteContactPersons(oContact, oTrans)
        DeleteTels(oContact, oTrans)
        DeleteEmails(oContact, oTrans)
        DeleteClls(oContact, oTrans)
        AddressesLoader.Delete(oContact, oTrans)
        CustomerLoader.Delete(oContact, oTrans)
        ProveidorLoader.Delete(oContact, oTrans)
        RepLoader.Delete(oContact, oTrans)
        StaffLoader.Delete(oContact, oTrans)
        BancLoader.Delete(oContact, oTrans)
        DeleteHeader(oContact, oTrans)
    End Sub

    Shared Sub DeleteContactPersons(oContact As DTOContact, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE CliContact WHERE ContactGuid='" & oContact.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteTels(oContact As DTOContact, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE CliTel WHERE CliGuid='" & oContact.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub
    Shared Sub DeleteEmails(oContact As DTOContact, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Email_Clis WHERE ContactGuid='" & oContact.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteClls(oContact As DTOContact, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Cll WHERE ContactGuid='" & oContact.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub


    Shared Sub DeleteHeader(oContact As DTOContact, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE CliGral WHERE Guid='" & oContact.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Function Tabs(oContact As DTOContact) As List(Of DTOContact.Tabs)
        Dim retval As New List(Of DTOContact.Tabs)
        retval.Add(DTOContact.Tabs.General)

        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT CliClient.Guid AS Client ")
        sb.AppendLine(", CliPrv.Guid AS Prv ")
        sb.AppendLine(", CliRep.Guid AS Rep ")
        sb.AppendLine(", CliStaff.Guid AS Staff ")
        sb.AppendLine(", CliBnc.Guid AS Bnc ")
        sb.AppendLine(", Trp.Guid AS Trp ")
        sb.AppendLine("FROM CliGral ")
        sb.AppendLine("LEFT OUTER JOIN CliClient ON CliGral.Guid = CliClient.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliPrv ON CliGral.Guid = CliPrv.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliRep ON CliGral.Guid = CliRep.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliStaff ON CliGral.Guid = CliStaff.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliBnc ON CliGral.Guid = CliBnc.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Trp ON CliGral.Guid = Trp.Guid ")
        sb.AppendLine("WHERE CliGral.Guid = '" & oContact.Guid.ToString & "' ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not IsDBNull(oDrd("Client")) Then
                retval.Add(DTOContact.Tabs.Client)
            End If
            If Not IsDBNull(oDrd("Prv")) Then
                retval.Add(DTOContact.Tabs.Proveidor)
            End If
            If Not IsDBNull(oDrd("Rep")) Then
                retval.Add(DTOContact.Tabs.Rep)
            End If
            If Not IsDBNull(oDrd("Staff")) Then
                retval.Add(DTOContact.Tabs.Staff)
            End If
            If Not IsDBNull(oDrd("Bnc")) Then
                retval.Add(DTOContact.Tabs.Banc)
            End If
            If Not IsDBNull(oDrd("Trp")) Then
                retval.Add(DTOContact.Tabs.Transportista)
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function



    Shared Function Tels(oContact As DTOContact) As List(Of DTOContactTel)
        Dim retval As New List(Of DTOContactTel)

        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT CliTel.*, Country.ISO AS CountryISO, Country.Nom_ESP AS CountryNom, Country.PrefixeTelefonic ")
        sb.AppendLine("FROM CliTel ")
        sb.AppendLine("LEFT OUTER JOIN Country ON CliTel.Country=Country.Guid ")
        sb.AppendLine("WHERE CliTel.CliGuid='" & oContact.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY CliTel.Ord ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)

        Do While oDrd.Read
            Dim item As New DTOContactTel(oDrd("Guid"))
            With item
                .Cod = oDrd("Cod")
                If Not IsDBNull(oDrd("Country")) Then
                    .Country = New DTOCountry(oDrd("Country"))
                    .Country.ISO = SQLHelper.GetStringFromDataReader(oDrd("CountryISO"))
                    .Country.LangNom = SQLHelper.GetLangTextFromDataReader(oDrd, "CountryNom")
                    .Country.PrefixeTelefonic = SQLHelper.GetStringFromDataReader(oDrd("PrefixeTelefonic"))
                End If

                .value = oDrd("Num")
                .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                .Privat = oDrd("Privat")
                .Ord = retval.Count
            End With
            retval.Add(item)
        Loop

        oDrd.Close()
        Return retval
    End Function

    Shared Function SubContacts(oContact As DTOContact) As List(Of String)
        Dim retval As New List(Of String)

        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT CliContact.Contact ")
        sb.AppendLine("FROM CliContact ")
        sb.AppendLine("WHERE CliContact.ContactGuid='" & oContact.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY CliContact.Id ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As String = oDrd("Contact")
            retval.Add(item)
        Loop

        oDrd.Close()
        Return retval
    End Function

#End Region

    Shared Function FullNom(sRaoSocial As String, sNomCom As String, sRef As String) As String
        Dim sb As New Text.StringBuilder
        sb = New System.Text.StringBuilder
        If sRaoSocial.Contains(sNomCom) And sNomCom > "" Then
            sb.Append(sNomCom)
        Else
            sb.Append(sRaoSocial)
            If sNomCom > "" Then
                If sRaoSocial > "" Then
                    sb.Append(" '" & sNomCom & "'")
                Else
                    sb.Append(sNomCom)
                End If
            End If
            If sRef > "" Then
                sb.Append(" (" & sRef & ")")
            End If
        End If
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function Tel(oContact As DTOContact) As String
        Dim retval As String = ""
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT TOP 1 Num ")
        sb.AppendLine("FROM CliTel ")
        sb.AppendLine("WHERE CliGuid=@Guid ")
        sb.AppendLine("AND Cod=@Cod ")
        sb.AppendLine("AND Privat=0 ")
        sb.AppendLine("ORDER BY Ord ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Guid", oContact.Guid.ToString, "@Cod", CInt(DTOContactTel.Cods.tel))
        If oDrd.Read Then
            retval = oDrd("NUM")
        End If
        oDrd.Close()
        Return retval
    End Function



End Class

Public Class ContactsLoader

    Shared Function All(oEmp As DTOEmp, oArea As DTOArea) As List(Of DTOContact)
        Dim retval As New List(Of DTOContact)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CliGral.Guid, CliGral.RaoSocial, CliGral.NomCom, CliAdr.Adr, CliAdr.Cod AS AdrCod ")
        sb.AppendLine(", VwAreaNom.AreaCod, VwAreaNom.CountryGuid, VwAreaNom.CountryISO, VwAreaNom.CountryNomEsp, VwAreaNom.CountryNomCat, VwAreaNom.CountryNomEng, VwAreaNom.ZonaGuid, VwAreaNom.ZonaNom, VwAreaNom.LocationGuid, VwAreaNom.LocationNom, VwAreaNom.ZipGuid, VwAreaNom.ZipCod ")
        sb.AppendLine("FROM CliGral ")
        sb.AppendLine("INNER JOIN CliAdr ON CliAdr.SrcGuid=CliGral.Guid ")
        sb.AppendLine("INNER JOIN VwAreaParent ON CliAdr.Zip=VwAreaParent.ChildGuid ")
        sb.AppendLine("INNER JOIN VwAreaNom ON CliAdr.Zip=VwAreaNom.Guid ")
        sb.AppendLine("WHERE CliGral.Emp = " & oEmp.Id & " ")
        sb.AppendLine("AND VwAreaParent.ParentGuid = '" & oArea.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oAdr As New DTOAddress()
            With oAdr
                .Codi = oDrd("AdrCod")
                .Text = SQLHelper.GetStringFromDataReader(oDrd("Adr"))
                .Zip = AreaLoader.NewArea(DirectCast(oDrd("AreaCod"), DTOArea.Cods), DirectCast(oDrd("CountryGuid"), Guid), oDrd("CountryNomEsp").ToString, oDrd("CountryNomCat").ToString, oDrd("CountryNomEng").ToString, oDrd("CountryISO").ToString, oDrd("ZonaGuid"), oDrd("ZonaNom"), oDrd("LocationGuid"), oDrd("LocationNom"), oDrd("ZipGuid"), oDrd("ZipCod"))
            End With
            Dim item As New DTOContact(oDrd("Guid"))
            With item
                .Emp = oEmp
                .Nom = oDrd("RaoSocial")
                .NomComercial = oDrd("NomCom")
                .Address = oAdr
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oUser As DTOUser, Optional oArea As DTOArea = Nothing) As List(Of DTOContact)
        Dim retval As New List(Of DTOContact)

        UserLoader.Load(oUser)
        Select Case oUser.Rol.id
            Case DTORol.Ids.rep, DTORol.Ids.comercial
                retval = RepCustomersLoader.All(oUser, oArea, IncludeFuture:=True)
            Case Else

                Dim sb As New System.Text.StringBuilder
                sb.AppendLine("SELECT Country.Guid AS CountryGuid, Country.ISO AS CountryISO ")
                sb.AppendLine(", Country.Nom_Esp AS CountryEsp, Country.Nom_Cat AS CountryCat, Country.Nom_Eng AS CountryEng, Country.Nom_Por AS CountryPor ")
                sb.AppendLine(", Zona.Guid AS ZonaGuid, Zona.Nom AS ZonaNom ")
                sb.AppendLine(", Location.Guid AS LocationGuid, Location.Nom AS LocationNom ")
                sb.AppendLine(", CliAdr.Zip, CliAdr.Adr ")
                sb.AppendLine(", CliGral.Guid AS ContactGuid, CliGral.RaoSocial, CliGral.NomCom, CliClient.Ref ")
                sb.AppendLine(", CliAdr.Geo.Lat AS Latitude, CliAdr.Geo.Long AS Longitude ")

                sb.AppendLine("FROM CliGral ")
                sb.AppendLine("INNER JOIN CliAdr ON CliGral.Guid = CliAdr.SrcGuid AND CliGral.Emp =" & oUser.Emp.Id & " AND CliGral.Obsoleto = 0 ")
                sb.AppendLine("INNER JOIN Zip ON CliAdr.Zip=Zip.Guid ")
                sb.AppendLine("INNER JOIN Location ON Zip.Location=Location.Guid ")
                sb.AppendLine("INNER JOIN Zona ON Location.Zona=Zona.Guid ")
                sb.AppendLine("INNER JOIN Country ON Zona.Country=Country.Guid ")

                Select Case oUser.Rol.id
                    Case DTORol.Ids.superUser, DTORol.Ids.admin
                        sb.AppendLine("LEFT OUTER JOIN CliClient ON CliGral.Guid=CliClient.Guid ")

                    Case DTORol.Ids.comercial, DTORol.Ids.rep
                'aquests ja van per RepCustomersLoader

                    Case DTORol.Ids.salesManager
                        sb.AppendLine("INNER JOIN CliClient ON CliGral.Guid = CliClient.Guid AND CliClient.NoRep = 0")
                        sb.AppendLine("INNER JOIN VwAreaParent ON CliAdr.Zip = VwAreaParent.ChildGuid ")
                        sb.AppendLine("INNER JOIN BrandArea ON VwAreaParent.ParentGuid = BrandArea.Area ")

                    Case DTORol.Ids.manufacturer
                        sb.AppendLine("INNER JOIN CliClient ON CliGral.Guid = CliClient.Guid ")
                        sb.AppendLine("INNER JOIN VwAreaParent ON Zip.Guid = VwAreaParent.ChildGuid ")
                        sb.AppendLine("INNER JOIN BrandArea ON VwAreaParent.ParentGuid = BrandArea.Area ")
                        sb.AppendLine("INNER JOIN Tpa ON BrandArea.Brand = Tpa.Guid ")
                        sb.AppendLine("INNER JOIN Email_Clis ON Tpa.Proveidor = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")

                    Case Else
                        sb.AppendLine("LEFT OUTER JOIN CliClient ON CliGral.Guid=CliClient.Guid ")
                        sb.AppendLine("INNER JOIN Email_Clis ON Email_Clis.ContactGuid = CliGral.Guid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")

                End Select

                sb.AppendLine("WHERE CliGral.Emp =" & oUser.Emp.Id & " AND CliGral.Obsoleto = 0 ")

                If oArea IsNot Nothing Then
                    Dim sArea As String = oArea.Guid.ToString
                    sb.AppendLine("AND (")
                    sb.AppendLine("Zona.Country = '" & sArea & "' ")
                    sb.AppendLine("OR Location.Zona = '" & sArea & "' ")
                    sb.AppendLine("OR Zip.Location = '" & sArea & "' ")
                    sb.AppendLine("OR CliAdr.Zip = '" & sArea & "' ")
                    sb.AppendLine(") ")
                    sb.AppendLine("AND CliGral.Obsoleto = 0 ")
                End If

                sb.AppendLine("GROUP BY Country.Guid, Country.ISO ")
                sb.AppendLine(", Country.Nom_Esp, Country.Nom_Cat, Country.Nom_Eng, Country.Nom_Por ")
                sb.AppendLine(", Zona.Guid, Zona.Nom ")
                sb.AppendLine(", Location.Guid, Location.Nom ")
                sb.AppendLine(", CliAdr.Zip, CliAdr.Adr ")
                sb.AppendLine(", CliGral.Guid, CliGral.RaoSocial, CliGral.NomCom, CliClient.Ref ")
                sb.AppendLine(", CliAdr.Geo.Lat, CliAdr.Geo.Long ")

                Dim sCountryNomField As String = oUser.Lang.Tradueix("CountryEsp", "CountryCat", "CountryEng", "CountryPor")
                sb.AppendLine("ORDER BY " & sCountryNomField & " ")
                sb.AppendLine(", ZonaNom ")
                sb.AppendLine(", LocationNom ")
                sb.AppendLine(", (CASE WHEN RaoSocial='' THEN NomCom ELSE RaoSocial END) ")
                sb.AppendLine(", CliClient.Ref ")

                Dim oCountry As New DTOCountry
                Dim oZona As New DTOZona
                Dim oLocation As New DTOLocation
                Dim oZip As New DTOZip

                Dim SQL As String = sb.ToString
                Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
                Do While oDrd.Read
                    If Not oCountry.Guid.Equals(oDrd("CountryGuid")) Then
                        oCountry = New DTOCountry(oDrd("CountryGuid"))
                        With oCountry
                            .ISO = SQLHelper.GetStringFromDataReader(oDrd("CountryISO"))
                            .LangNom = SQLHelper.GetLangTextFromDataReader(oDrd, "CountryEsp", "CountryCat", "CountryEng", "CountryPor")
                            .Zonas = New List(Of DTOZona)
                        End With
                    End If
                    If Not oZona.Guid.Equals(oDrd("ZonaGuid")) Then
                        oZona = New DTOZona(oDrd("ZonaGuid"))
                        With oZona
                            .Country = oCountry
                            .Nom = SQLHelper.GetStringFromDataReader(oDrd("ZonaNom"))
                            .Locations = New List(Of DTOLocation)
                        End With
                        oCountry.Zonas.Add(oZona)
                    End If
                    If Not oLocation.Guid.Equals(oDrd("LocationGuid")) Then
                        oLocation = New DTOLocation(oDrd("LocationGuid"))
                        With oLocation
                            .Zona = oZona
                            .Nom = SQLHelper.GetStringFromDataReader(oDrd("LocationNom"))
                            .Contacts = New List(Of DTOContact)
                        End With
                        oZona.Locations.Add(oLocation)
                    End If
                    If Not oZip.Guid.Equals(oDrd("Zip")) Then
                        oZip = New DTOZip(oDrd("Zip"))
                        'oZip.ZipCod = SQLHelper.GetStringFromDataReader(oDrd("ZipCod"))
                        oZip.Location = oLocation
                    End If
                    Dim oCustomer As New DTOCustomer(oDrd("ContactGuid"))
                    With oCustomer
                        .Emp = oUser.Emp
                        .Nom = SQLHelper.GetStringFromDataReader(oDrd("RaoSocial"))
                        .NomComercial = SQLHelper.GetStringFromDataReader(oDrd("NomCom"))
                        .Ref = SQLHelper.GetStringFromDataReader(oDrd("Ref"))
                        .FullNom = ContactLoader.FullNom(.Nom, .NomComercial, .Ref)
                        .Address = New DTOAddress()
                        With .Address
                            .Text = SQLHelper.GetStringFromDataReader(oDrd("Adr"))
                            .Codi = DTOAddress.Codis.Fiscal
                            .Zip = oZip
                            If Not IsDBNull(oDrd("Latitude")) Then
                                .Coordenadas = New GeoHelper.Coordenadas(oDrd("Latitude"), oDrd("Longitude"))
                            End If
                        End With
                    End With
                    oLocation.Contacts.Add(oCustomer)
                    retval.Add(oCustomer)
                Loop
                oDrd.Close()
        End Select

        Return retval
    End Function

    Shared Function All(oExercici As DTOExercici, oCta As DTOPgcCta) As List(Of DTOContact)
        Dim retval As New List(Of DTOContact)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CliGral.Guid, CliGral.RaoSocial ")
        sb.AppendLine("FROM Ccb ")
        sb.AppendLine("INNER JOIN Cca ON Ccb.CcaGuid = Cca.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON Ccb.ContactGuid=CliGral.Guid ")
        sb.AppendLine("WHERE Cca.Emp = " & oExercici.Emp.Id & " ")
        sb.AppendLine("AND Cca.Yea = '" & oExercici.Year & "' ")
        sb.AppendLine("AND Ccb.CtaGuid = '" & oCta.Guid.ToString & "' ")
        sb.AppendLine("GROUP BY CliGral.Guid, CliGral.RaoSocial ")
        sb.AppendLine("ORDER BY CliGral.RaoSocial ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read

            Dim item As New DTOContact
            If IsDBNull(oDrd("Guid")) Then
                item = New DTOContact(Guid.Empty)
                item.FullNom = "(buit)"
            Else
                item = New DTOContact(oDrd("Guid"))
                item.Nom = oDrd("RaoSocial")
            End If
            item.Emp = oExercici.Emp
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oUser As DTOUser, oClass As DTOContactClass) As List(Of DTOContact)
        Dim retval As New List(Of DTOContact)
        Dim sb As New Text.StringBuilder

        sb.AppendLine("SELECT CliGral.Guid, CliGral.Cli, CliGral.RaoSocial, CliGral.NomCom, CliGral.FullNom, CliGral.Nif, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod, CliGral.Obsoleto ")
        sb.AppendLine(", CliAdr.Zip, Zip.Location, Location.Nom AS LocationNom ")
        sb.AppendLine(", CliAdr.Adr, CliAdr.Geo.Lat AS Lat, CliAdr.Geo.Long AS Lng ")
        sb.AppendLine(", Zona.Guid AS ZonaGuid, Zona.Nom AS ZonaNom ")
        sb.AppendLine(", Country.Guid AS CountryGuid, Country.Nom_Esp ")
        sb.AppendLine("FROM CliGral ")
        sb.AppendLine("INNER JOIN CliAdr ON CliGral.Guid=CliAdr.SrcGuid AND CliAdr.Cod=1 ")
        sb.AppendLine("INNER JOIN Zip ON CliAdr.Zip=Zip.Guid ")
        sb.AppendLine("INNER JOIN Location ON Zip.Location=Location.Guid ")
        sb.AppendLine("INNER JOIN Zona ON Location.Zona=Zona.Guid ")
        sb.AppendLine("INNER JOIN Country ON Zona.Country=Country.Guid ")

        Select Case oUser.Rol.id
            Case DTORol.Ids.comercial, DTORol.Ids.rep
                sb.AppendLine("INNER JOIN CliClient ON CliGral.Guid = CliClient.Guid AND CliClient.NoRep = 0")
                sb.AppendLine("INNER JOIN VwAreaParent ON CliAdr.Zip = VwAreaParent.ChildGuid ")
                sb.AppendLine("INNER JOIN RepProducts ON VwAreaParent.ParentGuid = RepProducts.Area ")
                sb.AppendLine("INNER JOIN ContactClass ON CliGral.Guid=ContactClass.Guid AND ContactClass.DistributionChannel = RepProducts.DistributionChannel ")
                sb.AppendLine("INNER JOIN Email_Clis ON RepProducts.Rep = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
                sb.Append("AND (RepProducts.FchFrom < GETDATE()) ")
                sb.Append("AND (RepProducts.FchTo IS NULL OR RepProducts.FchTo > GETDATE()) ")
                sb.Append("AND RepProducts.Cod = 1 ")
        End Select

        sb.AppendLine("WHERE CliGral.Emp=" & oUser.Emp.Id & " ")
        sb.AppendLine("AND CliGral.ContactClass = '" & oClass.Guid.ToString & "' ")

        Select Case oUser.Rol.id
            Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.accounts, DTORol.Ids.marketing, DTORol.Ids.taller
            Case DTORol.Ids.salesManager
                sb.AppendLine("AND ( CliGral.Rol =" & DTORol.Ids.cliFull & " ")
                sb.AppendLine("OR  CliGral.Rol =" & DTORol.Ids.cliLite & " ")
                sb.AppendLine("OR  CliGral.Rol =" & DTORol.Ids.manufacturer & " ")
                sb.AppendLine(") ")
            Case DTORol.Ids.comercial, DTORol.Ids.rep
                sb.AppendLine("AND ( CliGral.Rol =" & DTORol.Ids.cliFull & " ")
                sb.AppendLine("OR  CliGral.Rol =" & DTORol.Ids.cliLite & " ")
                sb.AppendLine(") ")
        End Select
        sb.AppendLine("GROUP BY CliGral.Guid, CliGral.Cli, CliGral.RaoSocial, CliGral.NomCom, CliGral.Fullnom, CliGral.Nif, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod, CliGral.Obsoleto ")
        sb.AppendLine(", CliAdr.Zip, Zip.Location, Location.Nom ")
        sb.AppendLine(", CliAdr.Adr, CliAdr.Geo.Lat, CliAdr.Geo.Long ")
        sb.AppendLine(", Zona.Guid, Zona.Nom ")
        sb.AppendLine(", Country.Guid, Country.Nom_Esp ")
        sb.AppendLine("ORDER BY CliGral.Obsoleto, CliGral.FullNom ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oGuid As Guid = oDrd("Guid")
            Dim oContact As New DTOContact(oDrd("Guid"))
            With oContact
                .Emp = oUser.Emp
                .Id = oDrd("Cli")
                .Nom = oDrd("RaoSocial")
                .NomComercial = oDrd("NomCom")
                .FullNom = oDrd("FullNom")
                .Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
                .Obsoleto = CBool(oDrd("Obsoleto"))
                .Address = New DTOAddress()
                With .Address
                    .Zip = New DTOZip(oDrd("Zip"))
                    .Zip.Location = New DTOLocation(oDrd("Location"))
                    .Zip.Location.Nom = SQLHelper.GetStringFromDataReader(oDrd("LocationNom"))
                    .Zip.Location.Zona = New DTOZona(oDrd("ZonaGuid"))
                    .Zip.Location.Zona.Nom = SQLHelper.GetStringFromDataReader(oDrd("ZonaNom"))
                    .Zip.Location.Zona.Country = New DTOCountry(oDrd("CountryGuid"))
                    .Zip.Location.Zona.Country.LangNom.Esp = SQLHelper.GetStringFromDataReader(oDrd("Nom_Esp"))
                    .Text = oDrd("Adr")
                    If Not IsDBNull(oDrd("Lat")) And Not IsDBNull(oDrd("Lng")) Then
                        .Coordenadas = New GeoHelper.Coordenadas(oDrd("Lat"), oDrd("Lng"))
                    End If
                End With
            End With
            retval.Add(oContact)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oEmp As DTOEmp, oChannel As DTODistributionChannel, oUser As DTOUser) As List(Of DTOContact)
        Dim retval As New List(Of DTOContact)
        Dim sb As New Text.StringBuilder

        sb.AppendLine("SELECT CliGral.Guid, CliGral.Cli, CliGral.RaoSocial, CliGral.NomCom, CliGral.FullNom, CliGral.Nif, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod, CliGral.Obsoleto ")
        sb.AppendLine(", CliAdr.Zip, Zip.Location, Location.Nom AS LocationNom ")
        sb.AppendLine(", CliAdr.Adr, CliAdr.Geo.Lat AS Lat, CliAdr.Geo.Long AS Lng ")
        sb.AppendLine(", Zona.Guid AS ZonaGuid, Zona.Nom AS ZonaNom ")
        sb.AppendLine(", Country.Guid AS CountryGuid, Country.Nom_Esp ")
        sb.AppendLine("FROM CliGral ")
        sb.AppendLine("INNER JOIN ContactClass On CliGral.ContactClass=ContactClass.Guid ")
        sb.AppendLine("INNER JOIN CliAdr ON CliGral.Guid=CliAdr.SrcGuid AND CliAdr.Cod=1 ")
        sb.AppendLine("INNER JOIN Zip ON CliAdr.Zip=Zip.Guid ")
        sb.AppendLine("INNER JOIN Location ON Zip.Location=Location.Guid ")
        sb.AppendLine("INNER JOIN Zona ON Location.Zona=Zona.Guid ")
        sb.AppendLine("INNER JOIN Country ON Zona.Country=Country.Guid ")

        Select Case oUser.Rol.id
            Case DTORol.Ids.comercial, DTORol.Ids.rep
                sb.AppendLine("INNER JOIN CliClient ON CliGral.Guid = CliClient.Guid AND CliClient.NoRep = 0")
                sb.AppendLine("INNER JOIN VwAreaParent ON CliAdr.Zip = VwAreaParent.ChildGuid ")
                sb.AppendLine("INNER JOIN RepProducts ON VwAreaParent.ParentGuid = RepProducts.Area ")
                sb.AppendLine("INNER JOIN ContactClass ON CliGral.Guid=ContactClass.Guid AND ContactClass.DistributionChannel = RepProducts.DistributionChannel ")
                sb.AppendLine("INNER JOIN Email_Clis ON RepProducts.Rep = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
                sb.Append("AND (RepProducts.FchFrom < GETDATE()) ")
                sb.Append("AND (RepProducts.FchTo IS NULL OR RepProducts.FchTo > GETDATE()) ")
                sb.Append("AND RepProducts.Cod = 1 ")
        End Select

        sb.AppendLine("WHERE CliGral.Emp=" & oEmp.Id & " ")
        sb.AppendLine("AND ContactClass.DistributionChannel = '" & oChannel.Guid.ToString & "' ")

        Select Case oUser.Rol.Id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.Accounts, DTORol.Ids.Marketing, DTORol.Ids.Taller
            Case DTORol.Ids.SalesManager
                sb.AppendLine("AND ( CliGral.Rol =" & DTORol.Ids.CliFull & " ")
                sb.AppendLine("OR  CliGral.Rol =" & DTORol.Ids.CliLite & " ")
                sb.AppendLine("OR  CliGral.Rol =" & DTORol.Ids.Manufacturer & " ")
                sb.AppendLine(") ")
            Case DTORol.Ids.Comercial, DTORol.Ids.Rep
                sb.AppendLine("AND ( CliGral.Rol =" & DTORol.Ids.CliFull & " ")
                sb.AppendLine("OR  CliGral.Rol =" & DTORol.Ids.CliLite & " ")
                sb.AppendLine(") ")
        End Select
        sb.AppendLine("GROUP BY CliGral.Guid, CliGral.Cli, CliGral.RaoSocial, CliGral.NomCom, CliGral.FullNom, CliGral.Nif, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod, CliGral.Obsoleto ")
        sb.AppendLine(", CliAdr.Zip, Zip.Location, Location.Nom ")
        sb.AppendLine(", CliAdr.Adr, CliAdr.Geo.Lat, CliAdr.Geo.Long ")
        sb.AppendLine(", Zona.Guid, Zona.Nom ")
        sb.AppendLine(", Country.Guid, Country.Nom_Esp ")
        sb.AppendLine("ORDER BY CliGral.Obsoleto, CliGral.FullNom ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oGuid As Guid = oDrd("Guid")
            Dim oContact As New DTOContact(oDrd("Guid"))
            With oContact
                .Emp = oEmp
                .Id = oDrd("Cli")
                .Nom = oDrd("RaoSocial")
                .NomComercial = oDrd("NomCom")
                .FullNom = oDrd("FullNom")
                .Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
                .Obsoleto = CBool(oDrd("Obsoleto"))
                .Address = New DTOAddress()
                With .Address
                    .Zip = New DTOZip(oDrd("Zip"))
                    .Zip.Location = New DTOLocation(oDrd("Location"))
                    .Zip.Location.Nom = SQLHelper.GetStringFromDataReader(oDrd("LocationNom"))
                    .Zip.Location.Zona = New DTOZona(oDrd("ZonaGuid"))
                    .Zip.Location.Zona.Nom = SQLHelper.GetStringFromDataReader(oDrd("ZonaNom"))
                    .Zip.Location.Zona.Country = New DTOCountry(oDrd("CountryGuid"))
                    .Zip.Location.Zona.Country.LangNom.Esp = SQLHelper.GetStringFromDataReader(oDrd("Nom_Esp"))
                    .Text = oDrd("Adr")
                    If Not IsDBNull(oDrd("Lat")) And Not IsDBNull(oDrd("Lng")) Then
                        .Coordenadas = New GeoHelper.Coordenadas(oDrd("Lat"), oDrd("Lng"))
                    End If
                End With
            End With
            retval.Add(oContact)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function FromGLNs(eanValues As HashSet(Of String)) As List(Of DTOContact)
        Dim retval As New List(Of DTOContact)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	      Idx int NOT NULL")
        sb.AppendLine("	    , Ean VARCHAR(13) NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Idx,Ean) ")

        Dim idx As Integer = 0
        For Each eanValue In eanValues
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("({0},'{1}') ", idx, eanValue)
            idx += 1
        Next

        sb.AppendLine("SELECT CliGral.Guid, CliGral.FullNom, X.Ean ")
        sb.AppendLine("FROM CliGral ")
        sb.AppendLine("INNER JOIN @Table X ON CliGral.GLN = X.Ean ")
        sb.AppendLine("ORDER BY X.Idx ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOContact(oDrd("Guid"))
            With item
                .FullNom = oDrd("FullNom")
                .GLN = DTOEan.Factory(oDrd("Ean"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Glns(oEmp As DTOEmp) As Dictionary(Of String, Guid)
        Dim retval As New Dictionary(Of String, Guid)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Gln, Guid ")
        sb.AppendLine("FROM CliGral ")
        sb.AppendLine("WHERE Emp = " & oEmp.Id & " ")
        sb.AppendLine("AND Gln IS NOT NULL ")
        sb.AppendLine("ORDER BY Gln ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            retval.Add(oDrd("Gln"), oDrd("Guid"))
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function RaonsSocials(oUser As DTOUser) As List(Of DTOContact)
        Dim retval As New List(Of DTOContact)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CliGral.Guid, CliGral.RaoSocial, CliGral.Nif, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod ")
        sb.AppendLine("FROM CliGral ")
        sb.AppendLine("LEFT OUTER JOIN CliClient ON CliGral.Guid = CliClient.Guid ")
        sb.AppendLine("INNER JOIN Email_Clis On Email_Clis.ContactGuid= CliGral.Guid ")
        sb.AppendLine("WHERE Email_Clis.EmailGuid='" & oUser.Guid.ToString & "' ")
        sb.AppendLine("AND CliClient.CcxGuid IS NULL ")
        sb.AppendLine("ORDER BY CliGral.RaoSocial ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Guid", oUser.Guid.ToString())
        Do While oDrd.Read
            Dim item As New DTOContact(oDrd("Guid"))
            With item
                .Emp = oUser.Emp
                .Nom = oDrd("RaoSocial")
                .Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function FromEmailAddress(sSearchKey As String, oUser As DTOUser) As List(Of DTOContact)

        Dim retval As New List(Of DTOContact)
        Dim sb As New Text.StringBuilder

        sb.AppendLine("SELECT CliGral.Guid, CliGral.Cli, CliGral.RaoSocial, CliGral.NomCom, CliGral.FullNom, CliGral.Nif, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod, CliGral.Obsoleto ")
        sb.AppendLine(", Email.Adr AS EmailAddress ")
        sb.AppendLine(", VwAddress.ZipGuid, VwAddress.LocationGuid, VwAddress.LocationNom, VwAddress.Adr, VwAddress.Latitud, VwAddress.Longitud ")
        sb.AppendLine("FROM CliGral ")
        sb.AppendLine("INNER JOIN VwAddress ON CliGral.Guid = VwAddress.SrcGuid ")
        sb.AppendLine("INNER JOIN Email_Clis ECX ON CliGral.Guid = ECX.ContactGuid ")
        sb.AppendLine("INNER JOIN Email ON ECX.EmailGuid = Email.Guid ")

        Select Case oUser.Rol.Id
            Case DTORol.Ids.SalesManager
                sb.AppendLine("INNER JOIN VwSalesManagerCustomers ON CliGral.Guid = VwSalesManagerCustomers.Customer ")
                sb.AppendLine("INNER JOIN Email_Clis ON VwSalesManagerCustomers.SalesManager = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
            Case DTORol.Ids.Comercial, DTORol.Ids.Rep
                sb.AppendLine("INNER JOIN VwRepCustomers ON CliGral.Guid = VwRepCustomers.Customer ")
                sb.AppendLine("INNER JOIN Email_Clis ON VwRepCustomers.Rep = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
        End Select

        sb.AppendLine("WHERE CliGral.Emp = " & oUser.Emp.Id & " ")
        sb.AppendLine("AND Email.Adr LIKE '%" & sSearchKey & "%' ")

        Select Case oUser.Rol.Id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.Accounts, DTORol.Ids.Marketing, DTORol.Ids.Taller
            Case DTORol.Ids.SalesManager
                sb.AppendLine("AND ( CliGral.Rol =" & DTORol.Ids.CliFull & " ")
                sb.AppendLine("OR  CliGral.Rol =" & DTORol.Ids.CliLite & " ")
                sb.AppendLine("OR  CliGral.Rol =" & DTORol.Ids.Manufacturer & " ")
                sb.AppendLine("OR  CliGral.Rol =" & DTORol.Ids.Rep & " ")
                sb.AppendLine("OR  CliGral.Rol =" & DTORol.Ids.Comercial & " ")
                sb.AppendLine(") ")
            Case DTORol.Ids.Comercial, DTORol.Ids.Rep
                sb.AppendLine("AND ( CliGral.Rol =" & DTORol.Ids.CliFull & " ")
                sb.AppendLine("OR  CliGral.Rol =" & DTORol.Ids.CliLite & " ")
                sb.AppendLine(") ")
        End Select

        sb.AppendLine("GROUP BY CliGral.Guid, CliGral.Cli, CliGral.RaoSocial, CliGral.NomCom, CliGral.FullNom, CliGral.Nif, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod, CliGral.Obsoleto ")
        sb.AppendLine(", Email.adr ")
        sb.AppendLine(", VwAddress.ZipGuid, VwAddress.LocationGuid, VwAddress.LocationNom, VwAddress.Adr, VwAddress.Latitud, VwAddress.Longitud ")
        sb.AppendLine("ORDER BY CliGral.Obsoleto, CliGral.FullNom")
        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@SearchKey", sSearchKey & "%")
        Do While oDrd.Read
            Dim oGuid As Guid = oDrd("Guid")
            Dim oContact As New DTOContact(oDrd("Guid"))
            With oContact
                .Emp = oUser.Emp
                .Id = oDrd("Cli")
                .Nom = oDrd("RaoSocial")
                .NomComercial = oDrd("NomCom")
                .FullNom = oDrd("FullNom") & " [" & oDrd("EmailAddress") & "]"
                .Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
                .Obsoleto = CBool(oDrd("Obsoleto"))
                .Address = SQLHelper.GetAddressFromDataReader(oDrd)
            End With
            retval.Add(oContact)
        Loop
        oDrd.Close()

        Return retval
    End Function

    Shared Function SearchByEmail(sSearchKey As String, Optional oUser As DTOUser = Nothing) As List(Of DTOContact)
        Dim retval As New List(Of DTOContact)
        Dim sb As New Text.StringBuilder

        sb.AppendLine("SELECT CliGral.Guid, CliGral.FullNom, CliGral.Obsoleto, email.adr ")
        sb.AppendLine("FROM Email ")
        sb.AppendLine("INNER JOIN Email_Clis ON Email.Guid=Email_Clis.EmailGuid ")
        sb.AppendLine("INNER JOIN CliGral ON Email_Clis.ContactGuid=CliGral.Guid ")

        Select Case oUser.Rol.Id
            Case DTORol.Ids.SalesManager
                sb.AppendLine("INNER JOIN VwSalesManagerCustomers ON CliGral.Guid = VwSalesManagerCustomers.Customer ")
                sb.AppendLine("INNER JOIN Email_Clis ON VwSalesManagerCustomers.SalesManager = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
            Case DTORol.Ids.Comercial, DTORol.Ids.Rep
                sb.AppendLine("INNER JOIN VwRepCustomers ON CliGral.Guid = VwRepCustomers.Customer ")
                sb.AppendLine("INNER JOIN Email_Clis ON VwRepCustomers.Rep = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
        End Select

        sb.AppendLine("WHERE CliGral.Emp = " & oUser.Emp.Id & "  ")
        sb.AppendLine("AND Email.Adr LIKE '%" & sSearchKey & "%' ")
        sb.AppendLine("ORDER BY CliGral.Obsoleto, CliGral.FullNom ")
        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oGuid As Guid = oDrd("Guid")
            Dim oContact As New DTOContact(oDrd("Guid"))
            With oContact
                .Emp = oUser.Emp
                '.Id = oDrd("Cli")
                '.Nom = oDrd("RaoSocial")
                '.NomComercial = oDrd("NomCom")
                .FullNom = oDrd("Adr") & " " & oDrd("FullNom")
                '.Nif = oDrd("Nif")
                .Obsoleto = CBool(oDrd("Obsoleto"))
                '.Address = SQLHelper.GetAddressFromDataReader(oDrd)
            End With
            retval.Add(oContact)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function SearchByNif(sSearchKey As String, Optional oUser As DTOUser = Nothing) As List(Of DTOContact)
        Dim retval As New List(Of DTOContact)
        Dim sb As New Text.StringBuilder

        sb.AppendLine("SELECT CliGral.Guid, CliGral.FullNom, CliGral.Obsoleto, CliGral.Nif, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod ")
        sb.AppendLine("FROM CliGral ")

        Select Case oUser.Rol.Id
            Case DTORol.Ids.SalesManager
                sb.AppendLine("INNER JOIN VwSalesManagerCustomers ON CliGral.Guid = VwSalesManagerCustomers.Customer ")
                sb.AppendLine("INNER JOIN Email_Clis ON VwSalesManagerCustomers.SalesManager = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
            Case DTORol.Ids.Comercial, DTORol.Ids.Rep
                sb.AppendLine("INNER JOIN VwRepCustomers ON CliGral.Guid = VwRepCustomers.Customer ")
                sb.AppendLine("INNER JOIN Email_Clis ON VwRepCustomers.Rep = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
        End Select

        sb.AppendLine("WHERE CliGral.Emp = " & oUser.Emp.Id & "  ")
        sb.AppendLine("AND (CliGral.Nif LIKE '%" & sSearchKey & "%' OR CliGral.Nif2 LIKE '%" & sSearchKey & "%') ")
        sb.AppendLine("ORDER BY CliGral.Obsoleto, CliGral.FullNom ")
        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oGuid As Guid = oDrd("Guid")
            Dim oContact As New DTOContact(oDrd("Guid"))
            With oContact
                .Emp = oUser.Emp
                '.Id = oDrd("Cli")
                '.Nom = oDrd("RaoSocial")
                '.NomComercial = oDrd("NomCom")
                .FullNom = "Nif:" & oDrd("Nif") & " " & oDrd("FullNom")
                .Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
                .Obsoleto = CBool(oDrd("Obsoleto"))
                '.Address = SQLHelper.GetAddressFromDataReader(oDrd)
            End With
            retval.Add(oContact)
        Loop
        oDrd.Close()
        Return retval
    End Function
    Shared Function SearchByTel(sSearchKey As String, Optional oUser As DTOUser = Nothing) As List(Of DTOContact)
        Dim retval As New List(Of DTOContact)
        Dim sb As New Text.StringBuilder

        sb.AppendLine("SELECT CliGral.Guid, CliGral.FullNom, CliGral.Obsoleto ")
        sb.AppendLine(", CliTel.Num ")
        sb.AppendLine("FROM CliTel ")
        sb.AppendLine("INNER JOIN CliGral ON CliTel.CliGuid=CliGral.Guid ")

        Select Case oUser.Rol.Id
            Case DTORol.Ids.SalesManager
                sb.AppendLine("INNER JOIN VwSalesManagerCustomers ON CliGral.Guid = VwSalesManagerCustomers.Customer ")
                sb.AppendLine("INNER JOIN Email_Clis ON VwSalesManagerCustomers.SalesManager = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
            Case DTORol.Ids.Comercial, DTORol.Ids.Rep
                sb.AppendLine("INNER JOIN VwRepCustomers ON CliGral.Guid = VwRepCustomers.Customer ")
                sb.AppendLine("INNER JOIN Email_Clis ON VwRepCustomers.Rep = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
        End Select

        sb.AppendLine("WHERE CliGral.Emp = " & oUser.Emp.Id & "  ")
        sb.AppendLine("AND CliTel.Num LIKE '%" & sSearchKey & "%' ")
        sb.AppendLine("ORDER BY CliGral.Obsoleto, CliGral.FullNom ")
        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oGuid As Guid = oDrd("Guid")
            Dim oContact As New DTOContact(oDrd("Guid"))
            With oContact
                .Emp = oUser.Emp
                '.Id = oDrd("Cli")
                '.Nom = oDrd("RaoSocial")
                '.NomComercial = oDrd("NomCom")
                .FullNom = "tel.:" & oDrd("Num") & " " & oDrd("FullNom")
                '.Nif = oDrd("Nif")
                .Obsoleto = CBool(oDrd("Obsoleto"))
                '.Address = SQLHelper.GetAddressFromDataReader(oDrd)
            End With
            retval.Add(oContact)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function Search(oUser As DTOUser, oEmp As DTOEmp, sSearchKey As String, Optional SearchBy As DTOContact.SearchBy = DTOContact.SearchBy.notset) As List(Of DTOContact)
        Dim retval As New List(Of DTOContact)

        If TextHelper.RegexMatch(sSearchKey, "^[0-9]*$") Then
            If oUser.Rol.IsStaff Then
                If sSearchKey.Length = 13 Then
                    Dim oContact As DTOContact = ContactLoader.FromGln(DTOEan.Factory(sSearchKey))
                    retval.Add(oContact)
                Else
                    Dim oContact As DTOContact = ContactLoader.FromNum(oUser.Emp, CInt(sSearchKey))
                    If oContact IsNot Nothing Then
                        retval.Add(oContact)
                    End If
                End If
            End If
        ElseIf sSearchKey.Contains("@") Then
            retval.AddRange(ContactsLoader.FromEmailAddress(sSearchKey, oUser))
        Else
            Dim sb As New Text.StringBuilder

            sb.AppendLine("SELECT CliGral.Guid, CliGral.Cli, CliGral.RaoSocial, CliGral.NomCom, CliGral.FullNom, CliGral.Nif, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod, CliGral.Obsoleto ")
            sb.AppendLine(", CliAdr.Zip, Zip.Location, Location.Nom AS LocationNom ")
            sb.AppendLine(", CliAdr.Adr, CliAdr.Geo.Lat AS Lat, CliAdr.Geo.Long AS Lng ")
            sb.AppendLine(", CliGral.ContactClass, ContactClass.DistributionChannel ")
            sb.AppendLine(", CliClient.CcxGuid, CliClient.Guid AS ClientGuid ")
            sb.AppendLine("FROM CLL ")
            sb.AppendLine("INNER JOIN CliGral On CLL.ContactGuid=CliGral.Guid ")
            sb.AppendLine("INNER JOIN CliAdr ON CliGral.Guid=CliAdr.SrcGuid AND CliAdr.Cod=1 ")
            sb.AppendLine("LEFT OUTER JOIN ContactClass ON CliGral.ContactClass = ContactClass.Guid ")
            sb.AppendLine("LEFT OUTER JOIN DistributionChannel ON ContactClass.DistributionChannel = DistributionChannel.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Zip ON CliAdr.Zip=Zip.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Location ON Zip.Location=Location.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CliClient ON CliGral.Guid=CliClient.Guid ")

            Select Case oUser.Rol.Id
                Case DTORol.Ids.SalesManager
                    sb.AppendLine("INNER JOIN VwSalesManagerCustomers ON CliGral.Guid = VwSalesManagerCustomers.Customer ")
                    sb.AppendLine("INNER JOIN Email_Clis ON VwSalesManagerCustomers.SalesManager = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
                Case DTORol.Ids.Comercial, DTORol.Ids.Rep
                    sb.AppendLine("INNER JOIN VwRepCustomers ON CliGral.Guid = VwRepCustomers.Customer ")
                    sb.AppendLine("INNER JOIN Email_Clis ON VwRepCustomers.Rep = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
            End Select
            sb.AppendLine("WHERE CliGral.Emp=" & oEmp.Id & " And (CLL.CLL COLLATE Latin1_General_CI_AI LIKE @SearchKey COLLATE Latin1_General_CI_AI OR CliGral.NomKey COLLATE Latin1_General_CI_AI LIKE @SearchKey COLLATE Latin1_General_CI_AI) ")
            Select Case oUser.Rol.Id
                Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.Accounts, DTORol.Ids.Marketing, DTORol.Ids.Taller
                Case DTORol.Ids.SalesManager
                    sb.AppendLine("AND ( CliGral.Rol =" & DTORol.Ids.CliFull & " ")
                    sb.AppendLine("OR  CliGral.Rol =" & DTORol.Ids.CliLite & " ")
                    sb.AppendLine("OR  CliGral.Rol =" & DTORol.Ids.Manufacturer & " ")
                    sb.AppendLine("OR  CliGral.Rol =" & DTORol.Ids.Rep & " ")
                    sb.AppendLine("OR  CliGral.Rol =" & DTORol.Ids.Comercial & " ")
                    sb.AppendLine(") ")
            End Select
            sb.AppendLine("GROUP BY CliGral.Guid, CliGral.Cli, CliGral.RaoSocial, CliGral.NomCom, CliGral.FullNom, CliGral.Nif, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod, CliGral.Obsoleto ")
            sb.AppendLine(", CliAdr.Zip, Zip.Location, Location.Nom ")
            sb.AppendLine(", CliAdr.Adr, CliAdr.Geo.Lat, CliAdr.Geo.Long ")
            sb.AppendLine(", CliGral.ContactClass, ContactClass.DistributionChannel ")
            sb.AppendLine(", CliClient.CcxGuid, CliClient.Guid ")
            sb.AppendLine("ORDER BY CliGral.Obsoleto, CliGral.FullNom ")

            Dim oContact As DTOContact = Nothing
            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@SearchKey", sSearchKey & "%")
            Do While oDrd.Read
                Dim oGuid As Guid = oDrd("Guid")
                If Not IsDBNull(oDrd("ClientGuid")) Then
                    oContact = New DTOCustomer(oDrd("Guid"))
                    If Not IsDBNull(oDrd("CcxGuid")) Then
                        DirectCast(oContact, DTOCustomer).Ccx = New DTOCustomer(oDrd("CcxGuid"))
                    End If
                Else
                    oContact = New DTOContact(oDrd("Guid"))
                End If
                With oContact
                    .Emp = oUser.Emp
                    .Id = oDrd("Cli")
                    .Nom = oDrd("RaoSocial")
                    .NomComercial = oDrd("NomCom")
                    .FullNom = oDrd("FullNom")
                    .Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
                    .Obsoleto = CBool(oDrd("Obsoleto"))
                    .Address = New DTOAddress()
                    If Not IsDBNull(oDrd("Zip")) Then
                        With .Address
                            .Zip = New DTOZip(oDrd("Zip"))
                            If Not IsDBNull(oDrd("Location")) Then
                                .Zip.Location = New DTOLocation(oDrd("Location"))
                                .Zip.Location.Nom = SQLHelper.GetStringFromDataReader(oDrd("LocationNom"))
                                .Text = oDrd("Adr")
                                If Not IsDBNull(oDrd("Lat")) And Not IsDBNull(oDrd("Lng")) Then
                                    .Coordenadas = New GeoHelper.Coordenadas(oDrd("Lat"), oDrd("Lng"))
                                End If
                            End If
                        End With
                    End If
                    If Not IsDBNull(oDrd("ContactClass")) Then
                        .ContactClass = New DTOContactClass(oDrd("ContactClass"))
                        If Not IsDBNull(oDrd("DistributionChannel")) Then
                            .ContactClass.DistributionChannel = New DTODistributionChannel(oDrd("DistributionChannel"))
                        End If
                    End If
                End With
                retval.Add(oContact)
            Loop
            oDrd.Close()

        End If

        Return retval

    End Function


    Shared Function MoveToClass(oClass As DTOContactClass, oContacts As List(Of DTOContact), exs As List(Of Exception)) As Boolean
        Dim sb As New Text.StringBuilder
        sb.AppendLine("UPDATE CliGral ")
        sb.AppendLine("SET ContactClass = '" & oClass.Guid.ToString & "' ")
        sb.AppendLine("WHERE ( ")
        For Each oContact As DTOContact In oContacts
            If oContact.UnEquals(oContacts.First) Then
                sb.Append("OR ")
            End If
            sb.AppendLine("CliGral.Guid ='" & oContact.Guid.ToString & "' ")
        Next
        sb.AppendLine(") ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, exs)
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Function AutoCompleteString(oEmp As DTOEmp, sKey As String) As List(Of String)
        Dim retval As New List(Of String)
        Dim iPos As Integer = sKey.IndexOf(" ")
        Dim SQL As String = "SELECT (CASE WHEN CHARINDEX(' ',CLL)=0 THEN CLL ELSE SUBSTRING(CLL,0,CHARINDEX(' ',CLL)) END) AS Retval " _
        & "FROM Cll " _
        & "INNER JOIN CliGral ON Cll.ContactGuid = CliGral.Guid " _
        & "WHERE CliGral.Emp = " & oEmp.Id & " AND Cll.CLL Like @SearchKey " _
        & "GROUP BY (CASE WHEN CHARINDEX(' ',CLL)=0 THEN CLL ELSE SUBSTRING(CLL,0,CHARINDEX(' ',CLL)) END) " _
        & "ORDER BY Retval"

        Dim iMax As Integer = 10

        Dim oDrd As SqlClient.SqlDataReader = DAL.SQLHelper.GetDataReader(SQL, "@SearchKey", sKey & "%")
        Do While oDrd.Read
            retval.Add(oDrd("Retval").ToString())
            If retval.Count > 10 Then Exit Do
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function reZip(exs As List(Of Exception), oZipTo As DTOZip, oContacts As List(Of DTOContact)) As Integer
        Dim retval As Integer

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Dim sb As New Text.StringBuilder
            sb.AppendLine("UPDATE CliAdr SET CliAdr.Zip='" & oZipTo.Guid.ToString & "' ")
            sb.AppendLine("WHERE (")
            For Each oContact In oContacts
                If Not oContact.Equals(oContacts.First) Then sb.AppendLine("OR ")
                sb.AppendLine("(CliAdr.SrcGuid='" & oContact.Guid.ToString & "' AND CliAdr.Cod=" & oContact.address.codi & ") ")
            Next
            sb.AppendLine(")")
            Dim SQL As String = sb.ToString
            retval = SQLHelper.ExecuteNonQuery(SQL, oTrans)

            oTrans.Commit()
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function
End Class


