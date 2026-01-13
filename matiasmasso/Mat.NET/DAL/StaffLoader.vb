Public Class StaffLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOStaff
        Dim retval As DTOStaff = Nothing
        Dim oStaff As New DTOStaff(oGuid)
        If Load(oStaff) Then
            retval = oStaff
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oStaff As DTOStaff) As Boolean
        If Not oStaff.IsLoaded And Not oStaff.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT CliGral.RaoSocial, CliGral.NomCom, CliGral.LangId, CliGral.Rol, CliGral.NomAnteriorGuid, CliGral.NomNouGuid")
            sb.AppendLine(", CliGral.NIF, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod ")
            sb.AppendLine(", CliGral.Obsoleto, CliGral.GLN, CliGral.ContactClass ")
            sb.AppendLine(", ContactClass.Esp AS ContactClassNom, ContactClass.DistributionChannel ")
            sb.AppendLine(", VwAddress.* ")
            sb.AppendLine(", CliGral.FullNom ")

            sb.AppendLine(", CliStaff.Abr, CliStaff.NumSS, CliStaff.Alta, CliStaff.Baja, CliStaff.Nac, CliStaff.Sex, CliStaff.Teletrabajo ")
            sb.AppendLine(", CliStaff.StaffPos, StaffPosNom.Esp AS StaffPosNomEsp, StaffPosNom.Cat AS StaffPosNomCat, StaffPosNom.Eng AS StaffPosNomEng, StaffPosNom.Por AS StaffPosNomPor ")
            sb.AppendLine(", CliStaff.Categoria, StaffCategory.Nom AS CategoryNom ")
            sb.AppendLine(", VwIban.* ")

            sb.AppendLine("FROM CliGral ")
            sb.AppendLine("LEFT OUTER JOIN Clx ON CliGral.Guid = Clx.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwAddress ON CliGral.Guid = VwAddress.SrcGuid ")
            sb.AppendLine("LEFT OUTER JOIN ContactClass ON CliGral.ContactClass = ContactClass.Guid ")

            sb.AppendLine("INNER JOIN CliStaff ON CliGral.Guid = CliStaff.Guid ")
            sb.AppendLine("LEFT OUTER JOIN StaffPos ON CliStaff.StaffPos = StaffPos.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText StaffPosNom ON CliStaff.StaffPos = StaffPosNom.Guid AND StaffPosNom.Src = " & CInt(DTOLangText.Srcs.StaffPos) & " ")
            sb.AppendLine("LEFT OUTER JOIN VwIban ON VwIban.IbanContactGuid=CliStaff.Guid AND VwIban.IbanCod=" & CInt(DTOIban.Cods.staff) & " AND (VwIban.IbanCaducaFch IS NULL OR VwIban.IbanCaducaFch > GETDATE()) ")
            sb.AppendLine("LEFT OUTER JOIN StaffCategory ON CliStaff.Categoria = StaffCategory.Guid ")
            sb.AppendLine("WHERE CliGral.Guid='" & oStaff.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oStaff
                    .Nom = oDrd("RaoSocial")
                    .NomComercial = oDrd("NomCom")
                    .FullNom = oDrd("FullNom")
                    .Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
                    .Lang = DTOLang.Factory(oDrd("LangId"))
                    .Rol = New DTORol(oDrd("Rol"))
                    If Not IsDBNull(oDrd("NomAnteriorGuid")) Then
                        .NomAnterior = New DTOContact(oDrd("NomAnteriorGuid"))
                    End If
                    If Not IsDBNull(oDrd("NomNouGuid")) Then
                        .NomNou = New DTOContact(oDrd("NomNouGuid"))
                    End If
                    .Obsoleto = oDrd("Obsoleto")
                    .Address = SQLHelper.GetAddressFromDataReader(oDrd)
                    .GLN = SQLHelper.GetEANFromDataReader(oDrd("GLN"))
                    If Not IsDBNull(oDrd("ContactClass")) Then
                        .ContactClass = New DTOContactClass(oDrd("ContactClass"))
                        .ContactClass.Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "ContactClassNom", "ContactClassNom", "ContactClassNom", "ContactClassNom")
                        If Not IsDBNull(oDrd("DistributionChannel")) Then
                            .ContactClass.DistributionChannel = New DTODistributionChannel(oDrd("DistributionChannel"))
                        End If
                    End If
                End With

                With oStaff
                    .Abr = oDrd("Abr")
                    .NumSs = oDrd("NumSs")
                    .Alta = SQLHelper.GetFchFromDataReader(oDrd("Alta"))
                    .Baixa = SQLHelper.GetFchFromDataReader(oDrd("Baja"))
                    .Birth = SQLHelper.GetFchFromDataReader(oDrd("Nac"))
                    .Sex = oDrd("Sex")
                    .Teletrabajo = oDrd("Teletrabajo")
                    If Not IsDBNull(oDrd("StaffPos")) Then
                        .StaffPos = New DTOStaffPos(oDrd("StaffPos"))
                        SQLHelper.LoadLangTextFromDataReader(.StaffPos.LangNom, oDrd, "PosNomEsp", "PosNomCat", "PosNomEng", "PosNomPor")
                    End If
                    If Not IsDBNull(oDrd("Categoria")) Then
                        .Category = New DTOStaffCategory(oDrd("Categoria"))
                        .Category.Nom = SQLHelper.GetStringFromDataReader(oDrd("CategoryNom"))
                    End If
                    .Iban = SQLHelper.getIbanFromDataReader(oDrd)
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        ContactLoader.Load(oStaff)
        oStaff.tels = ContactLoader.Tels(oStaff)
        oStaff.emails = UsersLoader.All(oStaff, True)


        Dim retval As Boolean = oStaff.IsLoaded
        Return retval
    End Function

    Shared Function Avatar(oGuid As Guid) As Byte()
        Dim retval As Byte() = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CliStaff.Avatar ")
        sb.AppendLine("FROM CliStaff ")
        sb.AppendLine("WHERE CliStaff.Guid='" & oGuid.ToString & "' ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = oDrd("Avatar")
        End If

        oDrd.Close()
        Return retval
    End Function

    Shared Function Update(oStaff As DTOStaff, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oStaff, oTrans)
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

    Shared Sub Update(oStaff As DTOStaff, ByRef oTrans As SqlTransaction)
        'ContactLoader.Update(oStaff, oTrans)
        UpdateHeader(oStaff, oTrans)
        If oStaff.StaffPos IsNot Nothing Then
            LangTextLoader.Update(oStaff.StaffPos.LangNom, oTrans)
        End If
    End Sub


    Shared Sub UpdateHeader(oStaff As DTOStaff, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM CliStaff ")
        sb.AppendLine("WHERE CliStaff.Guid='" & oStaff.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oStaff.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oStaff
            oRow("Abr") = .Abr
            oRow("NumSs") = .NumSs
            oRow("Alta") = SQLHelper.NullableFch(.Alta)
            oRow("Baja") = SQLHelper.NullableFch(.Baixa)
            oRow("Nac") = SQLHelper.NullableFch(.Birth)
            oRow("Sex") = .Sex
            oRow("Teletrabajo") = .Teletrabajo
            oRow("Avatar") = If(.Avatar, System.DBNull.Value)
            oRow("StaffPos") = SQLHelper.NullableBaseGuid(.StaffPos)
            oRow("Categoria") = SQLHelper.NullableBaseGuid(.Category)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oStaff As DTOContact, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oStaff, oTrans)
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


    Shared Sub Delete(oStaff As DTOContact, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Iban WHERE ContactGuid='" & oStaff.Guid.ToString & "' AND Cod = " & DTOIban.Cods.staff & " "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
        SQL = "DELETE CliStaff WHERE Guid='" & oStaff.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class StaffsLoader

    Shared Function All(oEmp As DTOEmp, Optional OnlyActive As Boolean = False) As List(Of DTOStaff)
        Dim retval As New List(Of DTOStaff)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CliStaff.Guid, CliStaff.Abr, CliGral.FullNom, CliGral.RaoSocial, CliStaff.Sex, CliStaff.nac ")
        sb.AppendLine(", CliGral.NIF, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod ")
        sb.AppendLine(", CliStaff.NumSs, CliStaff.Alta, CliStaff.Baja ")
        sb.AppendLine(", CliStaff.StaffPos, StaffPosNom.Esp AS StaffPosNomEsp, StaffPosNom.Cat AS StaffPosNomCat, StaffPosNom.Eng AS StaffPosNomEng, StaffPosNom.Por AS StaffPosNomPor ")
        sb.AppendLine(", CliStaff.Categoria, StaffCategory.Nom AS CategoriaNom ")
        sb.AppendLine(", StaffCategory.SegSocialGrup, SegSocialGrups.Nom AS SegSocialGrupNom ")
        sb.AppendLine(", VwIban.* ")
        sb.AppendLine(", VwAddress.* ")
        sb.AppendLine(", VwTel.TelNum ")
        sb.AppendLine("FROM CliStaff ")
        sb.AppendLine("INNER JOIN CliGral ON CliStaff.Guid = CliGral.Guid ")
        sb.AppendLine("INNER JOIN VwAddress ON CliStaff.Guid = VwAddress.SrcGuid ")
        sb.AppendLine("LEFT OUTER JOIN VwTel ON CliStaff.Guid = vWTel.Contact ")
        sb.AppendLine("LEFT OUTER JOIN StaffPos ON CliStaff.StaffPos = StaffPos.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText StaffPosNom ON CliStaff.StaffPos = StaffPosNom.Guid AND StaffPosNom.Src = " & CInt(DTOLangText.Srcs.StaffPos) & " ")
        sb.AppendLine("LEFT OUTER JOIN StaffCategory ON CliStaff.Categoria = StaffCategory.Guid ")
        sb.AppendLine("LEFT OUTER JOIN SegSocialGrups ON StaffCategory.SegSocialGrup = SegSocialGrups.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwIban ON VwIban.IbanContactGuid=CliStaff.Guid AND VwIban.IbanCod=" & CInt(DTOIban.Cods.staff) & " AND (VwIban.IbanCaducaFch IS NULL OR VwIban.IbanCaducaFch > GETDATE()) ")
        sb.AppendLine("WHERE CliGral.Emp = " & oEmp.Id & " ")
        If OnlyActive Then
            sb.AppendLine("AND (CliStaff.Alta IS NULL OR CliStaff.Alta <= GETDATE()) ")
            sb.AppendLine("AND (CliStaff.Baja IS NULL OR CliStaff.Baja >= GETDATE()) ")
        End If
        sb.AppendLine("ORDER BY CliStaff.Abr")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOStaff(oDrd("Guid"))
            With item
                .Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
                .numSs = SQLHelper.GetStringFromDataReader(oDrd("NumSs"))
                .birth = SQLHelper.GetFchFromDataReader(oDrd("Nac"))
                .sex = SQLHelper.GetIntegerFromDataReader(oDrd("Sex"))
                .alta = SQLHelper.GetFchFromDataReader(oDrd("Alta"))
                .baixa = SQLHelper.GetFchFromDataReader(oDrd("Baja"))
                .abr = oDrd("Abr")
                .nom = oDrd("RaoSocial")
                .FullNom = oDrd("FullNom")
                .address = SQLHelper.GetAddressFromDataReader(oDrd)
                .telefon = SQLHelper.GetStringFromDataReader(oDrd("TelNum"))
                .iban = SQLHelper.getIbanFromDataReader(oDrd)
                If Not IsDBNull(oDrd("StaffPos")) Then
                    .StaffPos = New DTOStaffPos(oDrd("StaffPos"))
                    SQLHelper.LoadLangTextFromDataReader(.StaffPos.LangNom, oDrd, "PosNomEsp", "PosNomCat", "PosNomEng", "PosNomPor")
                End If

                If Not IsDBNull(oDrd("Categoria")) Then
                    .category = New DTOStaffCategory(oDrd("Categoria"))
                    With .category
                        .nom = SQLHelper.GetStringFromDataReader(oDrd("CategoriaNom"))
                        If Not IsDBNull(oDrd("SegSocialGrup")) Then
                            .SegSocialGrup = New DTOSegSocialGrup(oDrd("SegSocialGrup"))
                            .SegSocialGrup.nom = SQLHelper.GetStringFromDataReader(oDrd("SegSocialGrupNom"))
                        End If
                    End With
                End If

            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function ActiveAvatars(oEmp As DTOEmp, Optional OnlyActive As Boolean = False) As List(Of Byte())
        Dim retval As New List(Of Byte())
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CliStaff.Avatar, Clx.Img48 ")
        sb.AppendLine("FROM CliStaff ")
        sb.AppendLine("LEFT OUTER JOIN Clx ON CliStaff.Guid = Clx.Guid ")
        sb.AppendLine("INNER JOIN CliGral ON CliStaff.Guid = CliGral.Guid ")
        sb.AppendLine("INNER JOIN VwAddress ON CliStaff.Guid = VwAddress.SrcGuid ")
        sb.AppendLine("WHERE CliGral.Emp = " & oEmp.Id & " ")
        If OnlyActive Then
            sb.AppendLine("AND (CliStaff.Alta IS NULL OR CliStaff.Alta <= GETDATE()) ")
            sb.AppendLine("AND (CliStaff.Baja IS NULL OR CliStaff.Baja >= GETDATE()) ")
        End If
        sb.AppendLine("ORDER BY CliStaff.Abr")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oAvatar As Byte() = Nothing
            If IsDBNull(oDrd("Avatar")) Then
                If Not IsDBNull(oDrd("Img48")) Then
                    oAvatar = oDrd("Img48")
                End If
            Else
                    oAvatar = oDrd("Avatar")
            End If
            retval.Add(oAvatar)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Ibans(oEmp As DTOEmp) As List(Of DTOIban)
        Dim retval As New List(Of DTOIban)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Iban.Guid, CliGral.RaoSocial, Iban.ContactGuid, Iban.Ccc, Iban.BankBranch ")
        sb.AppendLine(", Bn2.Bank, Bn1.Abr AS BankNom, Bn1.Swift, Bn2.Adr, Bn2.Location AS LocationGuid, Location.Nom AS LocationNom ")
        sb.AppendLine("FROM Iban ")
        sb.AppendLine("INNER JOIN CliGral ON Iban.ContactGuid=CliGral.Guid AND Iban.Cod=" & CInt(DTOIban.Cods.Staff) & " ")
        sb.AppendLine("LEFT OUTER JOIN Bn2 ON Iban.BankBranch=Bn2.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Bn1 ON Bn2.Bank=Bn1.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Location ON Bn2.Location=Location.Guid ")
        sb.AppendLine("WHERE Mandato_Fch Is NULL Or Mandato_Fch<=GETDATE() ")
        sb.AppendLine("And Caduca_Fch IS NULL OR Caduca_Fch>=GETDATE() ")
        sb.AppendLine("ORDER BY Iban.ContactGuid")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oStaff As New DTOStaff(oDrd("ContactGuid"))
            oStaff.Nom = oDrd("RaoSocial")

            Dim item As New DTOIban(oDrd("Guid"))
            With item
                .Titular = oStaff
                .Digits = oDrd("Ccc")
                If Not IsDBNull(oDrd("BankBranch")) Then
                    .BankBranch = New DTOBankBranch(oDrd("BankBranch"))
                    With .BankBranch
                        .Address = SQLHelper.GetStringFromDataReader(oDrd("Adr"))
                        If Not IsDBNull(oDrd("LocationGuid")) Then
                            .Location = New DTOLocation(oDrd("LocationGuid"))
                            .Location.Nom = SQLHelper.GetStringFromDataReader(oDrd("LocationNom"))
                        End If
                        If Not IsDBNull(oDrd("Bank")) Then
                            .Bank = New DTOBank(oDrd("Bank"))
                            .Bank.NomComercial = SQLHelper.GetStringFromDataReader(oDrd("BankNom"))
                            .Bank.Swift = SQLHelper.GetStringFromDataReader(oDrd("Swift"))
                        End If
                    End With

                End If

            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Saldos(oExercici As DTOExercici) As List(Of DTOPgcSaldo)
        Dim oCtaNomina As DTOPgcCta = PgcCtaLoader.FromCod(DTOPgcPlan.Ctas.Nomina, oExercici)
        Dim oCtaPagas As DTOPgcCta = PgcCtaLoader.FromCod(DTOPgcPlan.Ctas.PagasTreballadors, oExercici)
        Dim retval As New List(Of DTOPgcSaldo)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Ccb.ContactGuid, CliGral.RaoSocial, CliGral.FullNom ")
        sb.AppendLine(", SUM(CASE WHEN dh = 1 THEN Ccb.Eur ELSE 0 END) AS Deb ")
        sb.AppendLine(", SUM(CASE WHEN dh = 2 THEN Ccb.Eur ELSE 0 END) AS Hab ")
        sb.AppendLine("FROM Ccb ")
        sb.AppendLine("INNER JOIN Cca ON Ccb.CcaGuid = Cca.Guid ")
        sb.AppendLine("INNER JOIN CliGral ON Ccb.ContactGuid = CliGral.Guid ")
        'sb.AppendLine("INNER JOIN (SELECT ContactGuid FROM Ccb WHERE CtaGuid='" & oCtaNomina.Guid.ToString & "' GROUP BY ContactGuid) Staff ON Ccb.ContactGuid = Staff.ContactGuid ")
        'sb.AppendLine("INNER JOIN PgcCta ON Ccb.CtaGuid = PgcCta.Guid AND (PgcCta.Id LIKE '43%' OR PgcCta.Id LIKE '40%'  OR PgcCta.Id LIKE '46%' ) ")
        sb.AppendLine("WHERE Cca.Emp=" & oExercici.Emp.Id & " AND Year(Cca.Fch)=" & oExercici.Year & " ")
        sb.AppendLine("AND Ccb.CtaGuid = '" & oCtaPagas.Guid.ToString & "' ")
        sb.AppendLine("GROUP BY Ccb.ContactGuid, CliGral.RaoSocial, CliGral.FullNom ")
        sb.AppendLine("HAVING SUM(CASE WHEN dh = 1 THEN Ccb.Eur ELSE 0 END)<>SUM(CASE WHEN dh = 2 THEN Ccb.Eur ELSE 0 END) ")
        sb.AppendLine("ORDER BY CliGral.RaoSocial")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oStaff As New DTOStaff(oDrd("ContactGuid"))
            oStaff.Nom = oDrd("RaoSocial")
            oStaff.FullNom = oDrd("FullNom")

            Dim oDebit As DTOAmt = Nothing
            If Not IsDBNull(oDrd("Deb")) Then
                oDebit = DTOAmt.Factory(CDec(oDrd("Deb")))
            End If

            Dim oCredit As DTOAmt = Nothing
            If Not IsDBNull(oDrd("Hab")) Then
                oCredit = DTOAmt.Factory(CDec(oDrd("Hab")))
            End If

            Dim item As New DTOPgcSaldo
            With item
                .Exercici = oExercici
                .Contact = oStaff
                .Debe = oDebit
                .Haber = oCredit
            End With
            retval.Add(item)
        Loop

        oDrd.Close()

        Return retval
    End Function


End Class
