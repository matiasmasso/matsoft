Public Class IbanLoader

    Shared Function Find(oGuid As Guid) As DTOIban
        Dim retval As DTOIban = Nothing
        Dim oMandato As New DTOIban(oGuid)
        If Load(oMandato) Then
            retval = oMandato
        End If
        Return retval
    End Function

    Shared Function FromCcc(Ccc As String) As DTOIban
        Dim retval As DTOIban = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT TOP 1 Iban.Guid ")
        sb.AppendLine("FROM Iban ")
        sb.AppendLine("WHERE Iban.Ccc='" & DTOIban.CleanCcc(Ccc) & "' ")
        sb.AppendLine("ORDER BY Iban.FchApproved DESC ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOIban(oDrd("Guid"))
            retval.Digits = Ccc
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function Load(ByRef oIban As DTOIban) As Boolean
        Dim retval As Boolean
        If oIban IsNot Nothing Then

            If Not oIban.IsLoaded And Not oIban.IsNew Then

                Dim sb As New System.Text.StringBuilder
                sb.AppendLine("SELECT Iban.ContactGuid, Iban.Cod, Iban.BankBranch, Iban.Ccc, Iban.Mandato_Fch, Iban.Format, Iban.Status, Iban.Caduca_Fch, Iban.Hash ")
                sb.AppendLine(", Iban.FchDownloaded, Iban.UsrDownloaded, UsrDownloaded.adr AS UsrDownloadedEmail ")
                sb.AppendLine(", Iban.FchUploaded, Iban.UsrUploaded, UsrUploaded.adr AS UsrUploadedEmail, Iban.FchApproved, Iban.UsrApproved, UsrApproved.adr AS UsrApprovedEmail ")
                sb.AppendLine(", Iban.PersonNom, Iban.PersonDni ")
                sb.AppendLine(", CliGral.FullNom ")
                sb.AppendLine(", Country.Guid AS CountryGuid, Country.ISO as CountryISO ")
                sb.AppendLine(", Bn1.Bn1, Bn1.Abr, Bn1.Nom, Bn1.Swift AS Bn1Swift ")
                sb.AppendLine(", Bn2.agc, Bn2.Bank, Bn2.Adr, Bn2.Location, Location.Nom as LocationNom ")
                sb.AppendLine(", IbanStructure.BankPosition, IbanStructure.BankLength, IbanStructure.BankFormat, IbanStructure.BranchPosition, IbanStructure.BranchLength, IbanStructure.BranchFormat ")
                sb.AppendLine(", IbanStructure.CheckDigitsPosition, IbanStructure.CheckDigitsLength, IbanStructure.CheckDigitsFormat, IbanStructure.AccountPosition, IbanStructure.AccountLength, IbanStructure.AccountFormat ")
                sb.AppendLine(", BF.Width, BF.Height, BF.Mime, BF.Thumbnail, BF.FchCreated AS DocfileFchCreated, BF.Size ")
                sb.AppendLine("FROM Iban ")
                sb.AppendLine("INNER JOIN CliGral ON Iban.ContactGuid = CliGral.Guid ")
                sb.AppendLine("LEFT OUTER JOIN Bn2 ON Iban.BankBranch = Bn2.Guid ")
                sb.AppendLine("LEFT OUTER JOIN Bn1 ON Bn2.Bank = Bn1.Guid ")
                sb.AppendLine("LEFT OUTER JOIN Location ON Bn2.Location = Location.Guid ")
                sb.AppendLine("LEFT OUTER JOIN Country ON substring(Iban.CCC,1,2) = Country.ISO ")
                sb.AppendLine("LEFT OUTER JOIN IbanStructure ON Country.ISO = IbanStructure.CountryISO ")
                sb.AppendLine("LEFT OUTER JOIN DocFile BF ON Iban.Hash = BF.Hash Collate SQL_Latin1_General_CP1_CI_AS ")
                sb.AppendLine("LEFT OUTER JOIN Email AS UsrDownloaded ON UsrDownloaded.Guid = Iban.UsrDownloaded ")
                sb.AppendLine("LEFT OUTER JOIN Email AS UsrUploaded ON UsrUploaded.Guid = Iban.UsrUploaded ")
                sb.AppendLine("LEFT OUTER JOIN Email AS UsrApproved ON UsrApproved.Guid = Iban.UsrApproved ")
                sb.AppendLine("WHERE Iban.Guid='" & oIban.Guid.ToString & "' ")
                Dim SQL As String = sb.ToString
                Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
                If oDrd.Read Then
                    With oIban
                        .Titular = New DTOContact(oDrd("ContactGuid"))
                        .Titular.FullNom = oDrd("FullNom")
                        .PersonNom = SQLHelper.GetStringFromDataReader(oDrd("PersonNom"))
                        .PersonDni = SQLHelper.GetStringFromDataReader(oDrd("PersonDni"))
                        .Cod = oDrd("Cod")
                        .Format = oDrd("Format")

                        Dim oCountry As DTOCountry = Nothing
                        If Not IsDBNull(oDrd("CountryGuid")) Then
                            oCountry = New DTOCountry(oDrd("CountryGuid"))
                            oCountry.ISO = oDrd("CountryISO")
                        End If

                        If Not IsDBNull(oDrd("BankBranch")) Then
                            .BankBranch = New DTOBankBranch(oDrd("BankBranch"))
                            With .BankBranch
                                If Not IsDBNull(oDrd("Bank")) Then
                                    .Bank = New DTOBank(oDrd("Bank"))
                                    With .Bank
                                        .Country = oCountry
                                        .Id = oDrd("Bn1")
                                        .NomComercial = oDrd("Abr")
                                        .RaoSocial = oDrd("Nom")
                                        If Not IsDBNull(oDrd("Bn1Swift")) Then
                                            .Swift = oDrd("Bn1Swift")
                                        End If
                                    End With
                                End If
                                .Id = SQLHelper.GetStringFromDataReader(oDrd("Agc"))
                                .Address = SQLHelper.GetStringFromDataReader(oDrd("adr"))
                                If Not IsDBNull(oDrd("Location")) Then
                                    .Location = New DTOLocation(oDrd("Location"))
                                    .Location.Nom = SQLHelper.GetStringFromDataReader(oDrd("LocationNom"))
                                End If
                            End With
                        End If
                        Dim s As String = ""
                        If Not IsDBNull(oDrd("BankPosition")) Then
                            .IbanStructure = New DTOIban.Structure
                            With .IbanStructure
                                .Country = oCountry
                                .BankPosition = oDrd("BankPosition")
                                .BankLength = oDrd("BankLength")
                                .BankFormat = oDrd("BankFormat")
                                .BranchPosition = oDrd("BranchPosition")
                                .BranchLength = oDrd("BranchLength")
                                .BranchFormat = oDrd("BranchFormat")
                                .CheckDigitsPosition = oDrd("CheckDigitsPosition")
                                .CheckDigitsLength = oDrd("CheckDigitsLength")
                                .CheckDigitsFormat = oDrd("CheckDigitsFormat")
                                .AccountPosition = oDrd("AccountPosition")
                                .AccountLength = oDrd("AccountLength")
                                .AccountFormat = oDrd("AccountFormat")
                                .IsLoaded = True
                            End With
                        End If

                        .Digits = oDrd("CCC")
                        .Status = oDrd("Status")
                        If IsDBNull(oDrd("Mandato_Fch")) Then
                            .FchFrom = Nothing
                        Else
                            .FchFrom = oDrd("Mandato_Fch")
                        End If
                        If IsDBNull(oDrd("Caduca_Fch")) Then
                            .FchTo = Nothing
                        Else
                            .FchTo = oDrd("Caduca_Fch")
                        End If
                        If Not IsDBNull(oDrd("Hash")) Then
                            .DocFile = New DTODocFile
                            With .DocFile
                                .Hash = oDrd("Hash").ToString
                                .Mime = oDrd("Mime")
                                .Size = New Size(CInt(oDrd("Width")), CInt(oDrd("Height")))
                                .Length = SQLHelper.GetIntegerFromDataReader(oDrd("Size"))
                                .FchCreated = SQLHelper.GetFchFromDataReader(oDrd("DocfileFchCreated"))
                                '.Thumbnail = oDrd("Thumbnail")
                            End With
                        End If
                        If Not IsDBNull(oDrd("UsrDownloaded")) Then
                            .UsrDownloaded = New DTOUser(DirectCast(oDrd("UsrDownloaded"), Guid))
                            .UsrDownloaded.EmailAddress = oDrd("UsrDownloadedEmail")
                            .FchDownloaded = oDrd("FchDownloaded")
                        End If
                        If Not IsDBNull(oDrd("UsrUploaded")) Then
                            .UsrUploaded = New DTOUser(DirectCast(oDrd("UsrUploaded"), Guid))
                            .UsrUploaded.EmailAddress = oDrd("UsrUploadedEmail")
                            .FchUploaded = oDrd("FchUploaded")
                        End If
                        If Not IsDBNull(oDrd("UsrApproved")) Then
                            .UsrApproved = New DTOUser(DirectCast(oDrd("UsrApproved"), Guid))
                            .UsrApproved.EmailAddress = oDrd("UsrApprovedEmail")
                            .FchApproved = oDrd("FchApproved")
                        End If
                        .IsLoaded = True
                    End With
                End If

                oDrd.Close()
            End If
            retval = oIban.IsLoaded
        End If

        Return retval
    End Function

    Shared Function Update(oIban As DTOIban, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Update(oIban, oTrans)
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

    Shared Sub Update(oIban As DTOIban, ByRef oTrans As SqlTransaction)
        If oIban IsNot Nothing Then
            DocFileLoader.Update(oIban.DocFile, oTrans)
            UpdateMain(oIban, oTrans)
        End If
    End Sub

    Shared Sub UpdateMain(oIban As DTOIban, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM Iban WHERE Guid='" & oIban.Guid.ToString & "' "
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oIban.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oIban
            oRow("Cod") = .Cod
            oRow("Format") = .Format
            oRow("ContactGuid") = .Titular.Guid
            oRow("PersonNom") = SQLHelper.NullableString(.PersonNom)
            oRow("PersonDni") = SQLHelper.NullableString(.PersonDni)
            oRow("BankBranch") = SQLHelper.NullableBaseGuid(.BankBranch)
            oRow("Ccc") = .Digits
            oRow("Status") = .Status
            oRow("Caduca_Fch") = SQLHelper.NullableFch(.FchTo)
            If .FchFrom = Nothing Then
                oRow("Mandato_Fch") = New Date(1985, 5, 28)
            Else
                oRow("Mandato_Fch") = .FchFrom
            End If

            oRow("FchDownloaded") = SQLHelper.NullableFch(.FchDownloaded)
            oRow("UsrDownloaded") = SQLHelper.NullableBaseGuid(.UsrDownloaded)
            oRow("FchUploaded") = SQLHelper.NullableFch(.FchUploaded)
            oRow("UsrUploaded") = SQLHelper.NullableBaseGuid(.UsrUploaded)
            oRow("FchApproved") = SQLHelper.NullableFch(.FchApproved)
            oRow("UsrApproved") = SQLHelper.NullableBaseGuid(.UsrApproved)

            oRow("Hash") = SQLHelper.NullableDocFile(.DocFile)
        End With

        oDA.Update(oDs)
    End Sub


    Shared Function Delete(oMandato As DTOIban, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oMandato, oTrans)
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

    Shared Sub Delete(oIban As DTOIban, oTrans As SqlTransaction)
        DocFileLoader.Delete(oIban.DocFile, oTrans)
        DeleteMain(oIban, oTrans)
    End Sub

    Shared Sub DeleteMain(oIban As DTOIban, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Iban WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oIban.Guid.ToString())
    End Sub

End Class

Public Class IbansLoader
    Shared Function Valids(oEmp As DTOEmp, Optional DtFch As Date = Nothing) As List(Of DTOIban)
        Dim retval As New List(Of DTOIban)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Iban.Guid, Iban.ContactGuid ")
        sb.AppendLine("FROM Iban ")
        sb.AppendLine("INNER JOIN CliGral ON Iban.ContactGuid = CliGral.Guid ")
        sb.AppendLine("WHERE CliGral.Emp=" & oEmp.Id & " ")
        sb.AppendLine("AND Iban.Cod=" & DTOIban.Cods.client & " ")
        If DtFch = Nothing Then
            sb.AppendLine("AND Iban.MANDATO_FCH <= GETDATE() ")
            sb.AppendLine("AND (Iban.CADUCA_FCH IS NULL OR Iban.CADUCA_FCH > GETDATE())")
        Else
            Dim sFch = Format(DtFch, "yyyyMMdd")
            sb.AppendLine("AND Iban.MANDATO_FCH<='" & sFch & "' ")
            sb.AppendLine("AND (Iban.CADUCA_FCH IS NULL OR Iban.CADUCA_FCH>'" & sFch & "')")
        End If
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOIban(oDrd("Guid"))
            With item
                .Titular = New DTOContact(oDrd("ContactGuid"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Clients(oEmp As DTOEmp) As List(Of DTOIban)
        Dim retval As New List(Of DTOIban)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Iban.Guid, Iban.ContactGuid, Iban.Mandato_Fch, Iban.Caduca_Fch ")
        sb.AppendLine(", Iban.Ccc, Iban.Hash, CliGral.FullNom ")
        sb.AppendLine(", Iban.FchDownloaded, Iban.FchUploaded, Iban.FchApproved ")
        sb.AppendLine(", Iban.BankBranch, Bn1.Abr, Bn1.Nom, Bn1.Swift, Bn2.Bank, Bn2.Location, Bn2.Adr, Location.Nom as LocationNom ")
        sb.AppendLine(", IbanStructure.BankPosition, IbanStructure.BankLength, IbanStructure.BranchPosition, IbanStructure.BranchLength ")
        sb.AppendLine(", IbanStructure.CheckDigitsPosition, IbanStructure.CheckDigitsLength, IbanStructure.AccountPosition, IbanStructure.AccountLength ")
        sb.AppendLine("FROM Iban ")
        sb.AppendLine("INNER JOIN CliGral ON Iban.ContactGuid=CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Bn2 ON Iban.BankBranch = Bn2.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Bn1 ON Bn2.Bank = Bn1.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Location ON Bn2.Location = Location.Guid ")
        sb.AppendLine("LEFT OUTER JOIN IbanStructure ON substring(Iban.CCC,1,2) = IbanStructure.CountryISO ")
        sb.AppendLine("WHERE CliGral.Emp=" & oEmp.Id & " ")
        sb.AppendLine("AND Iban.Cod=" & DTOIban.Cods.client & " ")
        sb.AppendLine("ORDER BY (CASE WHEN Iban.FchApproved IS NULL THEN 1 ELSE 0 END), (CASE WHEN Iban.FchUploaded IS NULL THEN 0 ELSE 1 END), FchDownloaded DESC ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOIban(oDrd("Guid"))
            With item
                .Digits = oDrd("Ccc")
                If Not IsDBNull(oDrd("BankPosition")) Then
                    .IbanStructure = New DTOIban.Structure
                    With .IbanStructure
                        .BankPosition = oDrd("BankPosition")
                        .BankLength = oDrd("BankLength")
                        .BranchPosition = oDrd("BranchPosition")
                        .BranchLength = oDrd("BranchLength")
                        .CheckDigitsPosition = oDrd("CheckDigitsPosition")
                        .CheckDigitsLength = oDrd("CheckDigitsLength")
                        .AccountPosition = oDrd("AccountPosition")
                        .AccountLength = oDrd("AccountLength")
                    End With
                End If

                If Not IsDBNull(oDrd("BankBranch")) Then
                    .BankBranch = New DTOBankBranch(oDrd("BankBranch"))
                    With .BankBranch
                        If item.IbanStructure IsNot Nothing Then
                            .Id = item.IbanStructure.GetBranchId(item.Digits)
                        End If
                        If Not IsDBNull(oDrd("Bank")) Then
                            .Bank = New DTOBank(oDrd("Bank"))
                            With .Bank
                                If item.IbanStructure IsNot Nothing Then
                                    .Id = item.IbanStructure.GetBankId(item.Digits)
                                End If
                                .Swift = SQLHelper.GetStringFromDataReader(oDrd("Swift"))
                                .NomComercial = SQLHelper.GetStringFromDataReader(oDrd("Abr"))
                                .RaoSocial = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                            End With
                        End If
                        .Address = SQLHelper.GetStringFromDataReader(oDrd("Adr"))
                        If Not IsDBNull(oDrd("Location")) Then
                            .Location = New DTOLocation(oDrd("Location"))
                        End If
                        If Not IsDBNull(oDrd("LocationNom")) Then
                            .Location.Nom = oDrd("LocationNom")
                        End If
                    End With
                End If


                .Titular = New DTOContact(oDrd("ContactGuid"))
                .Titular.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                .FchFrom = SQLHelper.GetFchFromDataReader(oDrd("Mandato_Fch"))
                .FchTo = SQLHelper.GetFchFromDataReader(oDrd("Caduca_Fch"))
                .FchDownloaded = SQLHelper.GetFchFromDataReader(oDrd("FchDownloaded"))
                .FchUploaded = SQLHelper.GetFchFromDataReader(oDrd("FchUploaded"))
                .FchApproved = SQLHelper.GetFchFromDataReader(oDrd("FchApproved"))
                .DocFile = SQLHelper.GetDocFileFromDataReaderHash(oDrd("Hash"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(Optional oEmp As DTOEmp = Nothing, Optional oContact As DTOContact = Nothing,
                    Optional oFormat As DTOIban.Formats = -1, Optional OnlyVigent As Boolean = False,
                    Optional oStatus As DTOIban.StatusEnum = DTOIban.StatusEnum.all, Optional oCod As DTOIban.Cods = DTOIban.Cods._NotSet,
                    Optional DtFch As Date = Nothing) As List(Of DTOIban)

        Dim retval As New List(Of DTOIban)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Iban.Guid, Iban.Cod, Iban.Format, Iban.ContactGuid, Iban.Ccc, Iban.BankBranch,Iban.Mandato_Fch, Iban.Status, Iban.Caduca_Fch, Iban.Hash ")
        sb.AppendLine(", Iban.FchDownloaded, Iban.UsrDownloaded, Iban.FchUploaded, Iban.UsrUploaded, Iban.FchApproved, Iban.UsrApproved ")
        sb.AppendLine(", CliGral.FullNom, Bn1.Abr, Bn1.Nom, Bn1.Swift, Bn2.Bank, Bn2.Adr, VwLocation.* ")
        sb.AppendLine(", IbanStructure.BankPosition, IbanStructure.BankLength, IbanStructure.BranchPosition, IbanStructure.BranchLength ")
        sb.AppendLine(", IbanStructure.CheckDigitsPosition, IbanStructure.CheckDigitsLength, IbanStructure.AccountPosition, IbanStructure.AccountLength ")
        sb.AppendLine("FROM Iban ")
        sb.AppendLine("INNER JOIN CliGral ON Iban.ContactGuid = CliGral.Guid ")

        sb.AppendLine("LEFT OUTER JOIN Bn2 ON Iban.BankBranch = Bn2.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Bn1 ON Bn2.Bank = Bn1.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwLocation ON Bn2.Location = VwLocation.LocationGuid ")
        sb.AppendLine("LEFT OUTER JOIN IbanStructure ON substring(Iban.CCC,1,2) = IbanStructure.CountryISO ")

        If oContact Is Nothing Then
            sb.AppendLine("WHERE CliGral.Emp = " & oEmp.Id & " ")
        Else
            sb.AppendLine("WHERE Iban.ContactGuid = '" & oContact.Guid.ToString & "' ")
        End If

        If OnlyVigent = True OrElse DtFch <> Nothing Then
            If DtFch = Nothing Then DtFch = DTO.GlobalVariables.Today()
            sb.AppendLine("AND (Iban.Mandato_Fch is NULL OR Iban.Mandato_Fch <='" & Format(DtFch, "yyyyMMdd") & " 23:59:59') ")
            sb.AppendLine("AND (Iban.Caduca_Fch is NULL OR Iban.Caduca_Fch >='" & Format(DtFch, "yyyyMMdd") & "') ")
        End If

        Select Case oStatus
            Case DTOIban.StatusEnum.pendingDownload
                sb.AppendLine("AND UsrDownloaded IS NULL AND UsrUploaded IS NULL AND UsrUploaded IS NULL ")
            Case DTOIban.StatusEnum.pendingUpload
                sb.AppendLine("AND UsrDownloaded IS NOT NULL AND UsrUploaded IS NULL AND UsrApproved IS NULL ")
            Case DTOIban.StatusEnum.pendingApproval
                sb.AppendLine("AND UsrUploaded IS NOT NULL AND UsrApproved IS NULL ")
            Case DTOIban.StatusEnum.downloaded
                sb.AppendLine("AND UsrDownloaded IS NOT NULL OR UsrUploaded IS NOT NULL OR UsrApproved IS NOT NULL ")
            Case DTOIban.StatusEnum.uploaded
                sb.AppendLine("AND UsrUploaded IS NOT NULL OR UsrApproved IS NOT NULL ")
            Case DTOIban.StatusEnum.approved
                sb.AppendLine("AND UsrApproved IS NOT NULL ")
        End Select

        If oCod <> DTOIban.Cods._NotSet Then
            sb.AppendLine("AND Iban.Cod=" & CInt(oCod) & " ")
        End If

        sb.AppendLine("ORDER BY Iban.Mandato_Fch DESC")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOIban(oDrd("Guid"))
            With item
                .Cod = oDrd("Cod")
                .Format = oDrd("Format")
                If oContact Is Nothing Then
                    .Titular = New DTOContact(oDrd("ContactGuid"))
                    .Titular.FullNom = oDrd("FullNom")
                Else
                    .Titular = oContact
                End If
                .Digits = oDrd("CCC")

                If Not IsDBNull(oDrd("BankPosition")) Then
                    .IbanStructure = New DTOIban.Structure
                    With .IbanStructure
                        .BankPosition = oDrd("BankPosition")
                        .BankLength = oDrd("BankLength")
                        .BranchPosition = oDrd("BranchPosition")
                        .BranchLength = oDrd("BranchLength")
                        .CheckDigitsPosition = oDrd("CheckDigitsPosition")
                        .CheckDigitsLength = oDrd("CheckDigitsLength")
                        .AccountPosition = oDrd("AccountPosition")
                        .AccountLength = oDrd("AccountLength")
                    End With
                End If

                If Not IsDBNull(oDrd("BankBranch")) Then
                    .BankBranch = New DTOBankBranch(oDrd("BankBranch"))
                    With .BankBranch
                        If item.IbanStructure IsNot Nothing Then
                            .Id = item.IbanStructure.GetBranchId(item.Digits)
                        End If
                        If Not IsDBNull(oDrd("Bank")) Then
                            .Bank = New DTOBank(oDrd("Bank"))
                            With .Bank
                                If item.IbanStructure IsNot Nothing Then
                                    .Id = item.IbanStructure.GetBankId(item.Digits)
                                End If
                                .Swift = SQLHelper.GetStringFromDataReader(oDrd("Swift"))
                                .NomComercial = SQLHelper.GetStringFromDataReader(oDrd("Abr"))
                                .RaoSocial = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                            End With
                        End If
                        .Address = SQLHelper.GetStringFromDataReader(oDrd("Adr"))
                        .Location = SQLHelper.GetLocationFromDataReader(oDrd)
                    End With
                End If
                '.Status = oDrd("Status")
                If IsDBNull(oDrd("Mandato_Fch")) Then
                    .FchFrom = Nothing
                Else
                    .FchFrom = oDrd("Mandato_Fch")
                End If
                If IsDBNull(oDrd("Caduca_Fch")) Then
                    .FchTo = Nothing
                Else
                    .FchTo = oDrd("Caduca_Fch")
                End If
                If Not IsDBNull(oDrd("Hash")) Then
                    .DocFile = New DTODocFile(oDrd("Hash").ToString())
                End If

                If Not IsDBNull(oDrd("UsrDownloaded")) Then
                    .UsrDownloaded = New DTOUser(DirectCast(oDrd("UsrDownloaded"), Guid))
                    .FchDownloaded = oDrd("FchDownloaded")
                End If
                If Not IsDBNull(oDrd("UsrUploaded")) Then
                    .UsrUploaded = New DTOUser(DirectCast(oDrd("UsrUploaded"), Guid))
                    .FchUploaded = oDrd("FchUploaded")
                End If
                If Not IsDBNull(oDrd("UsrApproved")) Then
                    .UsrApproved = New DTOUser(DirectCast(oDrd("UsrApproved"), Guid))
                    .FchApproved = oDrd("FchApproved")
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function FromBank(oEmp As DTOEmp, oBank As DTOBank, Optional OnlyVigent As Boolean = False) As List(Of DTOIban)
        Dim retval As New List(Of DTOIban)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Iban.Guid, Iban.Format, Iban.ContactGuid, Iban.Ccc, Iban.BankBranch,Iban.Mandato_Fch, Iban.Status, Iban.Caduca_Fch, Iban.Hash ")
        sb.AppendLine(", Iban.FchDownloaded, Iban.UsrDownloaded, Iban.FchUploaded, Iban.UsrUploaded, Iban.FchApproved, Iban.UsrApproved ")
        sb.AppendLine(", CliGral.FullNom, Bn1.Abr, Bn1.Nom, Bn2.Bank, Bn2.Location, Bn2.Adr, Location.Nom as LocationNom ")
        sb.AppendLine("FROM Iban ")
        sb.AppendLine("INNER JOIN CliGral ON Iban.ContactGuid = CliGral.Guid ")

        sb.AppendLine("INNER JOIN Bn2 ON Iban.BankBranch = Bn2.Guid ")
        sb.AppendLine("INNER JOIN Bn1 ON Bn2.Bank = Bn1.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Location ON Bn2.Location = Location.Guid ")

        sb.AppendLine("WHERE CliGral.Emp=" & oEmp.Id & " ")
        sb.AppendLine("AND Bn2.Bank='" & oBank.Guid.ToString & "' ")
        If OnlyVigent = True Then
            sb.AppendLine("AND (Iban.Mandato_Fch is NULL OR Iban.Mandato_Fch <='" & Format(Today, "yyyyMMdd") & "') ")
            sb.AppendLine("AND (Iban.Caduca_Fch is NULL OR Iban.Caduca_Fch >='" & Format(Today, "yyyyMMdd") & "') ")
        End If


        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOIban(oDrd("Guid"))
            With item
                .Format = oDrd("Format")
                .Titular = New DTOContact(oDrd("ContactGuid"))
                .Titular.FullNom = oDrd("FullNom")
                .Digits = oDrd("CCC")
                If Not IsDBNull(oDrd("BankBranch")) Then
                    .BankBranch = New DTOBankBranch(oDrd("BankBranch"))
                    With .BankBranch
                        .Bank = oBank
                        If Not IsDBNull(oDrd("Location")) Then
                            .Location = New DTOLocation(oDrd("Location"))
                            .Location.Nom = oDrd("LocationNom")
                        End If
                        .Address = oDrd("adr")
                    End With
                End If
                .Status = oDrd("Status")
                If IsDBNull(oDrd("Mandato_Fch")) Then
                    .FchFrom = Nothing
                Else
                    .FchFrom = oDrd("Mandato_Fch")
                End If
                If IsDBNull(oDrd("Caduca_Fch")) Then
                    .FchTo = Nothing
                Else
                    .FchTo = oDrd("Caduca_Fch")
                End If
                If Not IsDBNull(oDrd("Hash")) Then
                    .DocFile = New DTODocFile(oDrd("Hash").ToString())
                End If

                If Not IsDBNull(oDrd("UsrDownloaded")) Then
                    .UsrDownloaded = New DTOUser(DirectCast(oDrd("UsrDownloaded"), Guid))
                    .FchDownloaded = oDrd("FchDownloaded")
                End If
                If Not IsDBNull(oDrd("UsrUploaded")) Then
                    .UsrUploaded = New DTOUser(DirectCast(oDrd("UsrUploaded"), Guid))
                    .FchUploaded = oDrd("FchUploaded")
                End If
                If Not IsDBNull(oDrd("UsrApproved")) Then
                    .UsrApproved = New DTOUser(DirectCast(oDrd("UsrApproved"), Guid))
                    .FchApproved = oDrd("FchApproved")
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function FromBankBranch(oEmp As DTOEmp, oBranch As DTOBankBranch, Optional OnlyVigent As Boolean = False) As List(Of DTOIban)
        Dim retval As New List(Of DTOIban)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Iban.Guid, Iban.Format, Iban.ContactGuid, Iban.Ccc, Iban.BankBranch,Iban.Mandato_Fch, Iban.Status, Iban.Caduca_Fch, Iban.Hash ")
        sb.AppendLine(", Iban.FchDownloaded, Iban.UsrDownloaded, Iban.FchUploaded, Iban.UsrUploaded, Iban.FchApproved, Iban.UsrApproved ")
        sb.AppendLine(", CliGral.FullNom, Bn1.Abr, Bn1.Nom, Bn2.Bank, Bn2.Location, Bn2.Adr, Location.Nom as LocationNom ")
        sb.AppendLine("FROM Iban ")
        sb.AppendLine("INNER JOIN CliGral ON Iban.ContactGuid = CliGral.Guid ")

        sb.AppendLine("INNER JOIN Bn2 ON Iban.BankBranch = Bn2.Guid ")
        sb.AppendLine("INNER JOIN Bn1 ON Bn2.Bank = Bn1.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Location ON Bn2.Location = Location.Guid ")

        sb.AppendLine("WHERE CliGral.Emp = " & oEmp.Id & " ")
        sb.AppendLine("AND Bn2.Guid='" & oBranch.Guid.ToString & "' ")
        If OnlyVigent = True Then
            sb.AppendLine("AND (Iban.Mandato_Fch is NULL OR Iban.Mandato_Fch <='" & Format(Today, "yyyyMMdd") & "') ")
            sb.AppendLine("AND (Iban.Caduca_Fch is NULL OR Iban.Caduca_Fch >='" & Format(Today, "yyyyMMdd") & "') ")
        End If


        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOIban(oDrd("Guid"))
            With item
                .Format = oDrd("Format")
                .Titular = New DTOContact(oDrd("ContactGuid"))
                .Titular.FullNom = oDrd("FullNom")
                .Digits = oDrd("CCC")
                If Not IsDBNull(oDrd("BankBranch")) Then
                    .BankBranch = oBranch
                End If
                .Status = oDrd("Status")
                If IsDBNull(oDrd("Mandato_Fch")) Then
                    .FchFrom = Nothing
                Else
                    .FchFrom = oDrd("Mandato_Fch")
                End If
                If IsDBNull(oDrd("Caduca_Fch")) Then
                    .FchTo = Nothing
                Else
                    .FchTo = oDrd("Caduca_Fch")
                End If
                If Not IsDBNull(oDrd("Hash")) Then
                    .DocFile = New DTODocFile(oDrd("Hash").ToString())
                End If

                If Not IsDBNull(oDrd("UsrDownloaded")) Then
                    .UsrDownloaded = New DTOUser(DirectCast(oDrd("UsrDownloaded"), Guid))
                    .FchDownloaded = oDrd("FchDownloaded")
                End If
                If Not IsDBNull(oDrd("UsrUploaded")) Then
                    .UsrUploaded = New DTOUser(DirectCast(oDrd("UsrUploaded"), Guid))
                    .FchUploaded = oDrd("FchUploaded")
                End If
                If Not IsDBNull(oDrd("UsrApproved")) Then
                    .UsrApproved = New DTOUser(DirectCast(oDrd("UsrApproved"), Guid))
                    .FchApproved = oDrd("FchApproved")
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function PendingUploads(oUser As DTOUser) As List(Of DTOIban)
        Dim retval As New List(Of DTOIban)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Iban.Guid, Iban.Format, Iban.ContactGuid, Iban.Ccc, Iban.BankBranch,Iban.Mandato_Fch, Iban.Status, Iban.Caduca_Fch, Iban.Hash ")
        sb.AppendLine(", Iban.FchDownloaded, Iban.UsrDownloaded, Iban.FchUploaded, Iban.UsrUploaded, Iban.FchApproved, Iban.UsrApproved ")
        sb.AppendLine(", Iban.PersonNom, Iban.PersonDni ")
        sb.AppendLine(", CliGral.FullNom, Bn1.Abr, Bn1.Nom, Bn2.Bank, Bn2.Location, Bn2.Adr, Location.Nom as LocationNom ")
        sb.AppendLine("FROM Iban ")
        sb.AppendLine("INNER JOIN CliGral ON Iban.ContactGuid = CliGral.Guid ")
        sb.AppendLine("INNER JOIN Email_Clis ON CliGral.Guid = Email_Clis.ContactGuid ")
        sb.AppendLine("LEFT OUTER JOIN Bn2 ON Iban.BankBranch = Bn2.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Bn1 ON Bn2.Bank = Bn1.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Location ON Bn2.Location = Location.Guid ")
        sb.AppendLine("WHERE Email_Clis.EmailGuid ='" & oUser.Guid.ToString & "' ")
        sb.AppendLine("AND Iban.UsrUploaded IS NULL ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOIban(oDrd("Guid"))
            With item
                .Format = oDrd("Format")
                .Titular = New DTOContact(oDrd("ContactGuid"))
                .Titular.FullNom = oDrd("FullNom")
                .Digits = oDrd("Ccc")
                If Not IsDBNull(oDrd("BankBranch")) Then
                    .BankBranch = New DTOBankBranch(oDrd("BankBranch"))
                    With .BankBranch
                        .Bank = New DTOBank(oDrd("Bank"))
                        With .Bank
                            If Not IsDBNull(oDrd("Abr")) Then
                                .NomComercial = oDrd("Abr")
                            End If
                            If Not IsDBNull(oDrd("Nom")) Then
                                .RaoSocial = oDrd("Nom")
                            End If
                        End With
                        If Not IsDBNull(oDrd("Location")) Then
                            .Location = New DTOLocation(oDrd("Location"))
                            .Location.Nom = oDrd("LocationNom")
                        End If
                        .Address = oDrd("adr")
                    End With
                End If
                .Status = oDrd("Status")
                .PersonNom = SQLHelper.GetStringFromDataReader(oDrd("PersonNom"))
                .PersonDni = SQLHelper.GetStringFromDataReader(oDrd("PersonDni"))
                If IsDBNull(oDrd("Mandato_Fch")) Then
                    .FchFrom = Nothing
                Else
                    .FchFrom = oDrd("Mandato_Fch")
                End If
                If IsDBNull(oDrd("Caduca_Fch")) Then
                    .FchTo = Nothing
                Else
                    .FchTo = oDrd("Caduca_Fch")
                End If
                If Not IsDBNull(oDrd("Hash")) Then
                    .DocFile = New DTODocFile(oDrd("Hash").ToString())
                End If

                If Not IsDBNull(oDrd("UsrDownloaded")) Then
                    .UsrDownloaded = New DTOUser(DirectCast(oDrd("UsrDownloaded"), Guid))
                    .FchDownloaded = oDrd("FchDownloaded")
                End If
                If Not IsDBNull(oDrd("UsrUploaded")) Then
                    .UsrUploaded = New DTOUser(DirectCast(oDrd("UsrUploaded"), Guid))
                    .FchUploaded = oDrd("FchUploaded")
                End If
                If Not IsDBNull(oDrd("UsrApproved")) Then
                    .UsrApproved = New DTOUser(DirectCast(oDrd("UsrApproved"), Guid))
                    .FchApproved = oDrd("FchApproved")
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
