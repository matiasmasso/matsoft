Public Class CsaLoader

    Shared Function Find(oGuid As Guid) As DTOCsa
        Dim retval As DTOCsa = Nothing
        Dim oCsa As New DTOCsa(oGuid)
        If Load(oCsa) Then
            retval = oCsa
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oCsa As DTOCsa) As Boolean
        If Not oCsa.IsLoaded And Not oCsa.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Csa.Emp, Csa.Csb, Csa.Fch, Csa.BancGuid, Csa.Gts, Csa.CcaGuid ")
            sb.AppendLine(", Csa.Descomptat, Csa.efts, Csa.Eur AS CsaEur, Csa.Gts, Csa.Dias, Csa.ImportMig, Csa.Tae ")
            sb.AppendLine(", Csa.FileFormat, Csa.CcaGuid, CsaIban.IbanCcc AS CsaCcc ")
            sb.AppendLine(", CsaBanc.SepaCoreIdentificador, CsaBanc.NormaRMECedent ")
            sb.AppendLine(", Csb.Guid as CsbGuid ")
            sb.AppendLine(", Csb.Doc, Csb.CliGuid, Csb.Nom AS CliNom, Csb.Txt ")
            sb.AppendLine(", CliGral.NIF, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod ")
            sb.AppendLine(", VwAddress.* ")
            sb.AppendLine(", Csb.Eur AS CsbEur, Csb.SepaMandato, Csb.SepaTipoAdeudo ")
            sb.AppendLine(", Csb.Vto, Csb.Result, Csb.CcaVtoGuid ")
            sb.AppendLine(", CsaIban.BankBranchGuid AS CsaBankBranch, CsaIban.BankBranchLocationGuid AS CsaBankBranchLocationGuid, CsaIban.BankBranchLocationNom AS CsaBankBranchLocationNom, CsaIban.BankBranchZonaGuid AS CsaBankBranchZonaGuid, CsaIban.BankBranchCountryGuid AS CsaBankBranchCountryGuid, CsaIban.BankBranchCountryISO AS CsaBankBranchCountryISO ")
            sb.AppendLine(", CsaIban.BankGuid AS CsaBankGuid, CsaIban.BankNom AS CsaBankNom, CsaIban.BankSwift AS CsaBankSwift ")
            sb.AppendLine(", Csb.Ccc AS CsbCcc, CsbIban.IbanMandatoFch, CsbIban.BankBranchGuid AS CsbBankBranch, CsbIban.BankGuid AS CsbBankGuid, CsbIban.BankNom AS CsbBankNom, CsbIban.BankSwift AS CsbBankSwift ")
            sb.AppendLine(", CsbIban.BankBranchLocationGuid AS Bn2LocationGuid, CsbIban.BankBranchZonaGuid AS Bn2ZonaGuid, CsbIban.BankBranchCountryGuid AS Bn2CountryGuid, CsbIban.BankBranchCountryISO AS Bn2CountryISO ")
            sb.AppendLine("FROM Csa ")
            sb.AppendLine("INNER JOIN VwIban AS CsaIban ON Csa.BancGuid = CsaIban.IbanContactGuid AND CsaIban.IbanCod=" & CInt(DTOIban.Cods.Banc) & " ")
            sb.AppendLine("INNER JOIN CliBnc AS CsaBanc ON Csa.BancGuid = CsaBanc.Guid ")

            sb.AppendLine("INNER JOIN Csb ON Csa.Guid = Csb.CsaGuid ")
            sb.AppendLine("INNER JOIN CliGral ON Csb.CliGuid = CliGral.Guid ")
            sb.AppendLine("INNER JOIN VwAddress ON Csb.CliGuid = VwAddress.SrcGuid ")
            sb.AppendLine("LEFT OUTER JOIN VwIban AS CsbIban ON Csb.SepaMandato = CsbIban.IbanGuid ")

            sb.AppendLine("LEFT OUTER JOIN Cca ON Csa.CcaGuid = Cca.Guid ")
            sb.AppendLine("WHERE Csa.Guid='" & oCsa.Guid.ToString & "' ")
            sb.AppendLine("ORDER BY Csb.Doc ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                If Not oCsa.IsLoaded Then

                    Dim oCsaBankBranch As DTOBankBranch = Nothing
                    If Not IsDBNull(oDrd("CsaBankBranch")) And Not IsDBNull(oDrd("CsaBankGuid")) Then
                        Dim oCsaBank As New DTOBank(oDrd("CsaBankGuid"))
                        With oCsaBank
                            .NomComercial = oDrd("CsaBankNom")
                            .Swift = oDrd("CsaBankSwift")
                        End With
                        oCsaBankBranch = New DTOBankBranch(oDrd("CsaBankBranch"))
                        With oCsaBankBranch
                            .Bank = oCsaBank
                            .Location = SQLHelper.GetLocationFromDataReader(oDrd,
                                                                            LocationGuidField:="CsaBankBranchLocationGuid",
                                                                            LocationNomField:="CsaBankBranchLocationNom",
                                                                            ZonaGuidField:="CsaBankBranchZonaGuid",
                                                                            CountryGuidField:="CsaBankBranchCountryGuid",
                                                                            CountryISOField:="CsaBankBranchCountryISO"
                                                                            )
                        End With
                    End If

                    Dim oIban As New DTOIban
                    With oIban
                        .Digits = oDrd("CsaCcc")
                        .BankBranch = oCsaBankBranch
                    End With

                    Dim oBanc As New DTOBanc(oDrd("BancGuid"))
                    With oBanc
                        .Nom = oDrd("CsaBankNom")
                        .Iban = oIban
                        .SepaCoreIdentificador = SQLHelper.GetStringFromDataReader(oDrd("SepaCoreIdentificador"))
                        .NormaRMECedent = SQLHelper.GetStringFromDataReader(oDrd("NormaRMECedent"))
                    End With

                    With oCsa
                        .emp = New DTOEmp(oDrd("Emp"))
                        .id = oDrd("Csb")
                        .fch = oDrd("Fch")
                        .banc = oBanc
                        .descomptat = oDrd("Descomptat")
                        .efectes = SQLHelper.GetIntegerFromDataReader(oDrd("efts"))
                        .nominal = SQLHelper.GetAmtFromDataReader(oDrd("CsaEur"))
                        .despeses = SQLHelper.GetAmtFromDataReader(oDrd("Gts"))
                        .dias = SQLHelper.GetIntegerFromDataReader(oDrd("Dias"))
                        .importMig = SQLHelper.GetDecimalFromDataReader(oDrd("ImportMig"))
                        .tae = SQLHelper.GetDecimalFromDataReader(oDrd("Tae"))
                        .fileFormat = oDrd("FileFormat")
                        If Not IsDBNull(oDrd("CcaGuid")) Then
                            .cca = New DTOCca(oDrd("CcaGuid"))
                        End If
                        .items = New List(Of DTOCsb)
                        .IsLoaded = True
                    End With
                End If


                Dim oCsb As New DTOCsb(oDrd("CsbGuid"))
                With oCsb

                    Dim oCsbBankBranch As DTOBankBranch = Nothing
                    If Not IsDBNull(oDrd("CsbBankBranch")) And Not IsDBNull(oDrd("CsbBankGuid")) Then
                        Dim oCsbBank As New DTOBank(oDrd("CsbBankGuid"))
                        With oCsbBank
                            .NomComercial = oDrd("CsbBankNom")
                            .Swift = oDrd("CsbBankSwift")
                        End With
                        oCsbBankBranch = New DTOBankBranch(oDrd("CsbBankBranch"))
                        With oCsbBankBranch
                            .Bank = oCsbBank
                            .Location = SQLHelper.GetLocationFromDataReader(oDrd,
                                                                            LocationGuidField:="Bn2LocationGuid",
                                                                            ZonaGuidField:="Bn2ZonaGuid",
                                                                            CountryGuidField:="Bn2CountryGuid",
                                                                            CountryISOField:="Bn2CountryISO"
                                                                            )
                            .Bank.Country = DTOLocation.Country(.Location)
                        End With
                    End If

                    .Csa = oCsa
                    .Id = oDrd("Doc")
                    .Contact = New DTOContact(oDrd("CliGuid"))
                    With .Contact
                        .Nom = SQLHelper.GetStringFromDataReader(oDrd("CliNom"))
                        .Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
                        .Address = SQLHelper.GetAddressFromDataReader(oDrd)
                    End With
                    .Txt = SQLHelper.GetStringFromDataReader(oDrd("Txt"))
                    .Vto = oDrd("Vto")
                    .Result = oDrd("Result")

                    Dim oIban As DTOIban = Nothing
                    If Not IsDBNull(oDrd("SepaMandato")) Then
                        oIban = New DTOIban(oDrd("SepaMandato"))
                        With oIban
                            .Titular = oCsb.Contact
                            .Cod = DTOIban.Cods.Client
                            .Digits = SQLHelper.GetStringFromDataReader(oDrd("CsbCcc"))
                            .FchFrom = SQLHelper.GetFchFromDataReader(oDrd("IbanMandatoFch"))
                            .BankBranch = oCsbBankBranch
                        End With

                    End If

                    .Amt = DTOAmt.Factory(oDrd("CsbEur"))
                    .Iban = oIban
                    .SepaTipoAdeudo = oDrd("SepaTipoAdeudo")
                    If Not IsDBNull(oDrd("CcaVtoGuid")) Then
                        .ResultCca = New DTOCca(oDrd("CcaVtoGuid"))
                    End If
                End With
                oCsa.Items.Add(oCsb)
            Loop

            oDrd.Close()
        End If

        Dim retval As Boolean = oCsa.IsLoaded
        Return retval
    End Function


    Shared Function Update(ByRef oCsa As DTOCsa, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oCsa, oTrans)
            oTrans.Commit()
            oCsa.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function

    Shared Sub Update(ByRef oCsa As DTOCsa, ByRef oTrans As SqlTransaction)
        If oCsa.Id = 0 Then SetNextId(oCsa, oTrans)

        If oCsa.Items IsNot Nothing Then
            If oCsa.Items.Count > 0 Then
                If oCsa.Items(0).Id = 0 Then
                    'numera els rebuts
                    For i As Integer = 0 To oCsa.Items.Count - 1
                        oCsa.Items(i).Id = i + 1
                    Next
                End If
            End If
        End If


        If oCsa.Cca IsNot Nothing Then
            'assigna el numero de la remesa al concepte de l'assentament
            oCsa.Cca.Concept = oCsa.Cca.Concept.Replace("{0}", oCsa.Id)
            oCsa.Cca.Cdn = oCsa.Id
            CcaLoader.Update(oCsa.Cca, oTrans)
        End If
        UpdateHeader(oCsa, oTrans)
        UpdateItems(oCsa, oTrans)
        If oCsa.IsNew Then UpdatePnds(oCsa, oTrans)

    End Sub

    Shared Sub UpdatePnds(oCsa As DTOCsa, oTrans As SqlTransaction)

        For Each item As DTOCsb In oCsa.Items
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("UPDATE PND ")
            sb.AppendLine("SET Pnd.CsbGuid='" & item.Guid.ToString & "' ")

            If oCsa.IsNew Then
                sb.AppendLine(", Pnd.Status=" & DTOPnd.StatusCod.enCirculacio & " ")
            End If

            sb.AppendLine("WHERE Pnd.Guid='" & item.Pnd.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL, oTrans)
        Next
    End Sub



    Shared Sub UpdateHeader(ByRef oCsa As DTOCsa, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM Csa WHERE Guid=@Guid"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oCsa.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oCsa.Guid
            oRow("Emp") = oCsa.Emp.Id
        Else
            oRow = oTb.Rows(0)
        End If

        With oCsa
            oRow("Yea") = .Fch.Year
            oRow("Csb") = .Id
            oRow("Fch") = .Fch
            'oRow("Bnc") = .Banc.Id 'TO DEPRECATE
            oRow("BancGuid") = .Banc.Guid
            oRow("Descomptat") = .descomptat

            Dim Sum = DTOAmt.Empty
            Dim Dias As Decimal
            'If mCsbs IsNot Nothing Then
            For Each item As DTOCsb In oCsa.Items
                Sum.Add(item.Amt)
                Dim itemDays As Integer = item.Vto.Subtract(oCsa.Fch).Days
                Dias = Dias + (item.Amt.Eur * itemDays)
            Next
            oRow("Pts") = Sum.Val
            oRow("Cur") = Sum.Cur.Tag
            oRow("Eur") = Sum.Eur
            oRow("Efts") = .Items.Count
            oRow("ImportMig") = Sum.Val / .Items.Count
            oRow("Dias") = CInt(Dias / Sum.Eur)
            'End If
            oRow("FileFormat") = .FileFormat
            If .Descomptat Then
                oRow("CcaGuid") = SQLHelper.NullableBaseGuid(.cca)
            End If

            If .Despeses Is Nothing Then
                oRow("Gts") = 0
            Else
                oRow("Gts") = .Despeses.Val
            End If
        End With

        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateItems(oCsa As DTOCsa, oTrans As SqlTransaction)
        If Not oCsa.IsNew Then DeleteItems(oCsa, oTrans)

        Dim SQL As String = "SELECT * FROM Csb WHERE CsaGuid='" & oCsa.Guid.ToString() & "'"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        For Each item As DTOCsb In oCsa.Items
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = item.Guid
            oRow("CsaGuid") = oCsa.Guid

            With item
                oRow("Doc") = .Id
                oRow("CliGuid") = .Contact.Guid
                oRow("NOM") = .Contact.Nom
                oRow("TXT") = .Txt
                oRow("VAL") = .Amt.Val
                oRow("EUR") = .Amt.Eur
                oRow("CUR") = .Amt.Cur.Tag
                oRow("VTO") = .Vto.Date
                oRow("CCC") = .Iban.Digits

                oRow("YEF") = .FraYea
                If .FraNum = Nothing Then
                    oRow("FRA") = System.DBNull.Value
                Else
                    Dim iFra As Integer
                    If Integer.TryParse(.FraNum, iFra) Then
                        oRow("FRA") = iFra
                    End If
                End If

                If Not oCsa.IsNew Then
                    oRow("RECLAMAT") = IIf(.Result = DTOCsb.Results.Reclamat, 1, 0) 'to deprecate
                    oRow("IMPAGAT") = IIf(.Result = DTOCsb.Results.Impagat, 1, 0) 'to deprecate
                    oRow("CcaVtoGuid") = SQLHelper.NullableBaseGuid(.ResultCca)
                End If

                oRow("Result") = .Result

                If .Iban IsNot Nothing Then
                    oRow("SepaMandato") = .Iban.Guid
                    'If oCsa.FileFormat = DTOCsa.FileFormats.SepaB2b Then
                    oRow("SepaTipoAdeudo") = CInt(.SepaTipoAdeudo)
                    'End If
                End If
            End With


        Next
        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oCsa As DTOCsa, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        CsaLoader.Load(oCsa)

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oCsa, oTrans)
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


    Shared Sub Delete(oCsa As DTOCsa, ByRef oTrans As SqlTransaction)
        CsaLoader.Load(oCsa)
        BackUpPnds(oCsa, oTrans)
        DeleteItems(oCsa, oTrans)
        DeleteHeader(oCsa, oTrans)
        CcaLoader.Delete(oCsa.cca, oTrans)
    End Sub

    Shared Sub DeleteItems(oCsa As DTOCsa, oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Csb WHERE CsaGuid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oCsa.Guid.ToString())
    End Sub

    Shared Sub DeleteHeader(oCsa As DTOCsa, oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Csa WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oCsa.Guid.ToString())
    End Sub

    Shared Sub SetNextId(ByRef oCsa As DTOCsa, oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT TOP 1 Csb AS LastId ")
        sb.AppendLine("FROM Csa ")
        sb.AppendLine("WHERE Csa.Emp=" & oCsa.emp.Id & " ")
        sb.AppendLine("AND Csa.Yea=" & oCsa.fch.Year & " ")
        sb.AppendLine("ORDER BY Csb DESC")
        Dim SQL As String = sb.ToString

        Dim lastId = 0
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        If oTb.Rows.Count > 0 Then
            Dim oRow As DataRow = oTb.Rows(0)
            lastId = CInt(oRow("LastId"))
        End If
        oCsa.id = lastId + 1
    End Sub


    Shared Function SaveExpenses(oCsa As DTOCsa, oCca As DTOCca, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            CsaLoader.Update(oCsa, oTrans)
            oCca.Concept = String.Format("Despeses remesa {0} al cobrament ({1} efectes)", oCsa.Id, oCsa.Items.Count)
            CcaLoader.Update(oCca, oTrans)
            oTrans.Commit()
            oCsa.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function

    Shared Sub BackUpPnds(oCsa As DTOCsa, oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("UPDATE Pnd ")
        sb.AppendLine("SET Status =" & CInt(DTOPnd.StatusCod.pendent) & ", CsbGuid=NULL ")
        sb.AppendLine("FROM Pnd  ")
        sb.AppendLine("INNER JOIN Csb ON Pnd.CsbGuid = Csb.Guid ")
        sb.AppendLine("WHERE Csb.CsaGuid ='" & oCsa.Guid.ToString() & "' ")
        Dim SQL As String = sb.ToString

        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub
End Class

Public Class CsasLoader

    Shared Function Years(Optional oEmp As DTOEmp = Nothing, Optional oBanc As DTOBanc = Nothing) As List(Of Integer)
        Dim retval As New List(Of Integer)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT YEAR(Csa.Fch) AS Yea ")
        sb.AppendLine("FROM Csa ")
        If oBanc Is Nothing Then
            sb.AppendLine("WHERE Csa.Emp = " & oEmp.Id & " ")
        Else
            sb.AppendLine("WHERE Csa.BancGuid = '" & oBanc.Guid.ToString & "' ")
        End If
        sb.AppendLine("GROUP BY YEAR(Csa.Fch) ")
        sb.AppendLine("ORDER BY YEAR(Csa.Fch) DESC ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As Integer = oDrd("Yea")
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(iYear As Integer, Optional oEmp As DTOEmp = Nothing, Optional oBanc As DTOBanc = Nothing) As List(Of DTOCsa)
        Dim retval As New List(Of DTOCsa)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Csa.Guid AS CsaGuid, Csa.Csb, Csa.Fch, Csa.BancGuid, CliBnc.Abr AS BancAbr, Csa.Gts, Csa.Descomptat ")
        sb.AppendLine(", Csb.Guid AS CsbGuid, Csb.Doc, Csb.Eur, Csb.Cur, Csb.Val, Csb.CliGuid, CliGral.FullNom, Csb.Ccc, Csb.Vto ")
        sb.AppendLine("From Csa ")
        sb.AppendLine("INNER JOIN Csb ON Csa.Guid = Csb.CsaGuid ")
        sb.AppendLine("INNER JOIN CliGral ON Csb.CliGuid = CliGral.Guid ")
        sb.AppendLine("INNER JOIN CliBnc ON Csa.BancGuid = CliBnc.Guid ")
        sb.AppendLine("WHERE YEAR(Csa.Fch) = " & iYear & " ")
        If oBanc Is Nothing Then
            sb.AppendLine("AND Csa.Emp = " & oEmp.Id & " ")
        Else
            sb.AppendLine("AND Csa.BancGuid = '" & oBanc.Guid.ToString & "' ")
        End If
        sb.AppendLine("ORDER BY Year(Csa.Fch) DESC, Csa.Csb DESC ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oCsa = DTOCsa.Factory(oEmp)
        Do While oDrd.Read
            If Not oCsa.Guid.Equals(oDrd("CsaGuid")) Then
                oCsa = New DTOCsa(oDrd("CsaGuid"))
                With oCsa
                    .Emp = oEmp
                    .Id = oDrd("Csb")
                    .Fch = oDrd("Fch")
                    .Banc = New DTOBanc(oDrd("BancGuid"))
                    .Banc.Nom = oDrd("BancAbr")
                    .Descomptat = oDrd("Descomptat")
                    .Despeses = SQLHelper.GetAmtFromDataReader(oDrd("Gts"))
                End With
                retval.Add(oCsa)
            End If
            Dim item As New DTOCsb(oDrd("CsbGuid"))
            With item
                .Id = oDrd("Doc")
                .Contact = New DTOContact(oDrd("CliGuid"))
                .Contact.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                .Iban = New DTOIban()
                .Iban.Digits = SQLHelper.GetStringFromDataReader(oDrd("Ccc"))
                .Amt = DTOAmt.Factory(CDec(oDrd("Eur")), oDrd("Cur").ToString, CDec(oDrd("Val")))
                .Vto = oDrd("Vto")
            End With
            oCsa.Items.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Update(oCsas As List(Of DTOCsa), exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            For Each item In oCsas
                CsaLoader.Update(item, oTrans)
            Next

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
End Class
