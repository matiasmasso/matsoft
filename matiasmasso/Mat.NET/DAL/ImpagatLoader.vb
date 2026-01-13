Public Class ImpagatLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOImpagat
        Dim retval As DTOImpagat = Nothing
        Dim oImpagat As New DTOImpagat(oGuid)
        If Load(oImpagat) Then
            retval = oImpagat
        End If
        Return retval
    End Function

    Shared Function FromCsb(oCsb As DTOCsb) As DTOImpagat
        Dim retval As DTOImpagat = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Guid ")
        sb.AppendLine("FROM Impagats ")
        sb.AppendLine("WHERE CsbGuid ='" & oCsb.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            Dim oGuid As Guid = oDrd("Guid")
            retval = New DTOImpagat(oGuid)
            retval.Csb = oCsb
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function Load(ByRef oImpagat As DTOImpagat) As Boolean
        If Not oImpagat.IsLoaded And Not oImpagat.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT  Impagats.CsbGuid, Impagats.RefBanc, Impagats.Gastos, Impagats.PagatACompte ")
            sb.AppendLine("      , Impagats.fchAFP, Impagats.fchSdo, Impagats.Status, Impagats.Obs ")
            sb.AppendLine("      , Impagats.ASNEFalta, Impagats.ASNEFbaixa ")
            sb.AppendLine("      , Impagats.CcaIncobrable, Incobrable.Cca AS IdIncobrable, Incobrable.Fch AS FchIncobrable, Incobrable.Txt AS TxtIncobrable ")
            sb.AppendLine("      , Csb.CsaGuid, Csb.Doc, Csb.vto, Csb.CliGuid, Csb.Eur ")
            sb.AppendLine("      , Csa.Fch AS CsaFch, Csa.BancGuid, Csa.Csb AS CsaId ")
            sb.AppendLine("      , CsaIban.Ccc AS CsaCcc, CsaBanc.SepaCoreIdentificador ")
            sb.AppendLine("      , CsaIban.BankBranch AS CsaBankBranch, CsaBn2.Bank AS CsaBankGuid, CsaBn1.Abr AS CsaBankNom, CsaBn1.Swift AS CsaBankSwift ")
            sb.AppendLine("      , CliGral.FullNom ")
            sb.AppendLine("      , VwIban.* ")
            sb.AppendLine("FROM Impagats ")
            sb.AppendLine("INNER JOIN Csb ON Impagats.CsbGuid = Csb.Guid ")
            sb.AppendLine("INNER JOIN Csa ON Csb.CsaGuid = Csa.Guid ")
            sb.AppendLine("INNER JOIN CliGral ON Csb.CliGuid = CliGral.Guid ")

            sb.AppendLine("INNER JOIN Iban AS CsaIban ON Csa.BancGuid = CsaIban.ContactGuid AND CsaIban.Cod=" & CInt(DTOIban.Cods.Banc) & " ")
            sb.AppendLine("INNER JOIN Bn2 AS CsaBn2 ON CsaIban.BankBranch = CsaBn2.Guid ")
            sb.AppendLine("INNER JOIN Bn1 AS CsaBn1 ON CsaBn2.Bank = CsaBn1.Guid ")
            sb.AppendLine("INNER JOIN CliBnc AS CsaBanc ON Csa.BancGuid = CsaBanc.Guid ")

            sb.AppendLine("LEFT OUTER JOIN VwIban ON Csb.SepaMandato = VwIban.IbanGuid ")

            sb.AppendLine("LEFT OUTER JOIN Cca AS Incobrable ON Impagats.CcaIncobrable = Incobrable.Guid ")
            sb.AppendLine("WHERE Impagats.Guid ='" & oImpagat.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                Dim oContact As New DTOContact(oDrd("CliGuid"))
                With oContact
                    .FullNom = oDrd("FullNom")
                End With

                Dim oCsaBankBranch As DTOBankBranch = Nothing
                If Not IsDBNull(oDrd("CsaBankBranch")) And Not IsDBNull(oDrd("CsaBankGuid")) Then
                    Dim oCsaBank As New DTOBank(oDrd("CsaBankGuid"))
                    With oCsaBank
                        .NomComercial = oDrd("CsaBankNom")
                        .Swift = oDrd("CsaBankSwift")
                    End With
                    oCsaBankBranch = New DTOBankBranch(oDrd("CsaBankBranch"))
                    oCsaBankBranch.Bank = oCsaBank
                End If

                Dim oCsaIban As New DTOIban
                With oCsaIban
                    .Digits = oDrd("CsaCcc")
                    .BankBranch = oCsaBankBranch
                End With

                Dim oBanc As New DTOBanc(oDrd("BancGuid"))
                With oBanc
                    .Nom = oDrd("CsaBankNom")
                    .Iban = oCsaIban
                    .SepaCoreIdentificador = SQLHelper.GetStringFromDataReader(oDrd("SepaCoreIdentificador"))
                End With

                Dim oCsa As New DTOCsa(oDrd("CsaGuid"))
                With oCsa
                    .Id = oDrd("CsaId")
                    .Fch = oDrd("CsaFch")
                    .Banc = oBanc
                End With

                Dim BlLoadCsb As Boolean = True
                If oImpagat.Csb IsNot Nothing Then
                    If oImpagat.Csb.IsLoaded Then BlLoadCsb = False
                End If

                Dim oCsb As New DTOCsb(oDrd("CsbGuid"))
                With oCsb
                    .Csa = oCsa
                    .Id = oDrd("Doc")
                    .Contact = oContact
                    .Amt = DTOAmt.Factory(CDec(oDrd("Eur")))
                    .Vto = oDrd("Vto")
                    .Iban = SQLHelper.getIbanFromDataReader(oDrd)
                End With

                With oImpagat
                    If BlLoadCsb Then .Csb = oCsb
                    .RefBanc = SQLHelper.GetStringFromDataReader(oDrd("RefBanc"))
                    .Gastos = DTOAmt.Factory(CDec(oDrd("Gastos")))
                    .PagatACompte = DTOAmt.Factory(CDec(oDrd("PagatACompte")))
                    .FchAFP = SQLHelper.GetFchFromDataReader(oDrd("FchAFP"))
                    .FchSdo = SQLHelper.GetFchFromDataReader(oDrd("FchSdo"))
                    .Status = oDrd("Status")
                    .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                    .AsnefAlta = SQLHelper.GetFchFromDataReader(oDrd("ASNEFAlta"))
                    .AsnefBaixa = SQLHelper.GetFchFromDataReader(oDrd("ASNEFBaixa"))
                    If Not IsDBNull(oDrd("CcaIncobrable")) Then
                        .CcaIncobrable = New DTOCca(oDrd("CcaIncobrable"))
                        With .CcaIncobrable
                            .Fch = oDrd("FchIncobrable")
                            .Concept = oDrd("TxtIncobrable")
                            .Id = oDrd("IdIncobrable")
                        End With
                    End If
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oImpagat.IsLoaded
        Return retval
    End Function

    Shared Function Update(oImpagat As DTOImpagat, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oImpagat, oTrans)
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


    Shared Sub Update(oImpagat As DTOImpagat, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Impagats ")
        sb.AppendLine("WHERE Guid='" & oImpagat.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oImpagat.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oImpagat
            oRow("CsbGuid") = .Csb.Guid
            oRow("RefBanc") = .RefBanc
            oRow("Gastos") = .Gastos.Val
            oRow("PagatACompte") = SQLHelper.NullableAmt(.PagatACompte)
            oRow("FchAFP") = SQLHelper.NullableFch(.FchAFP)
            oRow("FchSdo") = SQLHelper.NullableFch(.FchSdo)
            oRow("Status") = .Status
            oRow("Obs") = SQLHelper.NullableString(.Obs)
            oRow("ASNEFAlta") = SQLHelper.NullableFch(.AsnefAlta)
            oRow("ASNEFBaixa") = SQLHelper.NullableFch(.AsnefBaixa)
            oRow("CcaIncobrable") = SQLHelper.NullableBaseGuid(.CcaIncobrable)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oImpagat As DTOImpagat, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oImpagat, oTrans)
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


    Shared Sub Delete(oImpagat As DTOImpagat, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Impagats WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oImpagat.Guid.ToString())
    End Sub

    Shared Function CobraPerVisa(oLog As DTOTpvLog, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            CcaLoader.Update(oLog.Result, oTrans)
            Update(oLog.Request, oTrans)
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

#End Region

End Class

Public Class ImpagatsLoader

    Shared Function All(oEmp As DTOEmp, OrderBy As DTOImpagat.OrderBy, Optional oContact As DTOContact = Nothing) As List(Of DTOImpagat)
        Dim retval As New List(Of DTOImpagat)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT  Impagats.Guid, Impagats.CsbGuid, Impagats.RefBanc, Impagats.Gastos, Impagats.PagatACompte ")
        sb.AppendLine("      , Impagats.fchAFP, Impagats.fchSdo, Impagats.Status, Impagats.Obs ")
        sb.AppendLine("      , Impagats.ASNEFalta, Impagats.ASNEFbaixa ")
        sb.AppendLine("      , Csb.CsaGuid, Csb.Doc, Csb.vto, Csb.CliGuid, Csb.Eur ")
        sb.AppendLine("      , Csa.Fch AS CsaFch, Csa.Csb AS CsaId, Csa.BancGuid AS CsaBancGuid ")
        sb.AppendLine("      , CliGral.FullNom ")
        sb.AppendLine("      , CsaIban.Guid AS CsaIbanGuid, VwCsaIban.BankBranchGuid AS CsaBankBranch, VwCsaIban.BankGuid AS CsaBank, VwCsaIban.BankBranchLocationGuid AS CsaBankBranchLocation, VwCsaIban.IbanCcc AS CsaCcc ")
        sb.AppendLine("      , VwCsaIban.BankBranchAdr AS CsaBankBranchAdr, VwCsaIban.BankBranchLocationNom AS CsaBankBranchLocationNom, VwCsaIban.BankNom AS CsaBankNom, VwCsaIban.BankAlias AS CsaBankAbr ")
        sb.AppendLine("      , VwCsaIban.BankBranchCountryGuid AS CsaBankCountry, VwCsaIban.BankBranchCountryISO AS CsaBankCountryISO ")
        sb.AppendLine("      , VwIban.* ")
        sb.AppendLine("      , VwAddress.* ")
        sb.AppendLine("FROM Impagats ")
        sb.AppendLine("INNER JOIN Csb ON Impagats.CsbGuid = Csb.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwAddress ON Csb.CliGuid = VwAddress.SrcGuid ")
        sb.AppendLine("LEFT OUTER JOIN VwIban ON Csb.SepaMandato = VwIban.IbanGuid ")
        sb.AppendLine("INNER JOIN Csa ON Csb.CsaGuid = Csa.Guid ")
        sb.AppendLine("INNER JOIN CliGral ON Csb.CliGuid = CliGral.Guid ")
        sb.AppendLine("INNER JOIN Iban AS CsaIban ON Csa.BancGuid = CsaIban.ContactGuid AND CsaIban.Cod=" & CInt(DTOIban.Cods.Banc) & " ")
        sb.AppendLine("LEFT OUTER JOIN VwIban AS VwCsaIban ON CsaIban.Guid = VwCsaIban.IbanGuid ")

        sb.AppendLine("WHERE Impagats.Status <" & DTOImpagat.StatusCodes.Saldat & " ")
        sb.AppendLine("AND Impagats.CcaIncobrable IS NULL ")
        If oContact Is Nothing Then
            sb.AppendLine("AND Csa.Emp =" & oEmp.Id & " ")
        Else
            sb.AppendLine("AND Csb.CliGuid ='" & oContact.Guid.ToString & "' ")
        End If

        Select Case OrderBy
            Case DTOImpagat.OrderBy.Vto
                sb.AppendLine("ORDER BY Csb.Vto")
            Case DTOImpagat.OrderBy.cliNomVto
                sb.AppendLine("ORDER BY CliGral.FullNom, Csb.Vto")
        End Select

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oCsa As New DTOCsa(oDrd("CsaGuid"))
            With oCsa
                .Id = oDrd("CsaId")
                .Fch = oDrd("CsaFch")
                .Banc = New DTOBanc(oDrd("CsaBancGuid"))
                .Banc.Iban = SQLHelper.getIbanFromDataReader(oDrd, FieldIbanGuid:="CsaIbanGuid",
                                                             FieldBankBranchCountryGuid:="CsaBankCountry",
                                                             FieldBankBranchCountryISO:="CsaCountryISO",
                                                             FieldBankBranchLocationGuid:="CsaBankBranchLocation",
                                                             FieldBankBranchLocationNom:="CsaBankBranchLocationNom",
                                                             FieldBank:="CsaBank",
                                                             FieldBankNom:="CsaBankNom",
                                                             FieldBankAlias:="CsaBankAbr",
                                                             FieldBankBranchGuid:="CsaBankBranch",
                                                             FieldBankBranchAdr:="CsaBankBranchAdr",
                                                             FieldIbanCcc:="CsaCcc") 'New DTOBanc(oDrd("CsaBancGuid"))
            End With
            Dim oCsb As New DTOCsb(oDrd("CsbGuid"))
            With oCsb
                .Csa = oCsa
                If oContact Is Nothing Then
                    .Contact = New DTOContact(oDrd("CliGuid"))
                    .Contact.FullNom = oDrd("FullNom")
                Else
                    .Contact = oContact
                End If
                .Contact.Address = SQLHelper.GetAddressFromDataReader(oDrd)
                .Iban = SQLHelper.getIbanFromDataReader(oDrd)
                .Amt = DTOAmt.Factory(CDec(oDrd("Eur")))
                .Vto = oDrd("Vto")
            End With

            Dim item As New DTOImpagat(oDrd("Guid"))
            With item
                .Csb = oCsb
                .RefBanc = SQLHelper.GetStringFromDataReader(oDrd("RefBanc"))
                .Gastos = DTOAmt.Factory(CDec(oDrd("Gastos")))
                .PagatACompte = DTOAmt.Factory(CDec(oDrd("PagatACompte")))
                .FchAFP = SQLHelper.GetFchFromDataReader(oDrd("FchAFP"))
                .FchSdo = SQLHelper.GetFchFromDataReader(oDrd("FchSdo"))
                .Status = oDrd("Status")
                .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                .AsnefAlta = SQLHelper.GetFchFromDataReader(oDrd("ASNEFAlta"))
                .AsnefBaixa = SQLHelper.GetFchFromDataReader(oDrd("ASNEFBaixa"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(Optional oRep As DTORep = Nothing) As List(Of DTOImpagat)
        Dim retval As New List(Of DTOImpagat)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Csb.Guid AS CsbGuid, Csb.Eur, Csb.CliGuid, Csb.Vto ")
        sb.AppendLine(", Impagats.Guid as ImpagatGuid, Impagats.Gastos, Impagats.PagatACompte, CliGral.FullNom ")
        sb.AppendLine("From Impagats ")
        sb.AppendLine("INNER JOIN Csb on Impagats.CsbGuid = Csb.Guid ")
        If oRep IsNot Nothing Then
            sb.AppendLine("INNER JOIN CliAdr ON Csb.CliGuid = CliAdr.SrcGuid AND CliAdr.Cod = 1 ")
            sb.AppendLine("INNER JOIN VwAreaParent ON CliAdr.Zip=VwAreaParent.ChildGuid ")
            sb.AppendLine("INNER JOIN RepProducts ON VwAreaParent.ParentGuid = RepProducts.Area AND (FchFrom IS NULL OR FchFrom<=GETDATE()) AND (FchTo IS NULL OR FchTo >=GETDATE()) AND RepProducts.Rep='" & oRep.Guid.ToString & "' AND RepProducts.Cod=1 ")
        End If
        sb.AppendLine("INNER JOIN CliGral ON Csb.CliGuid = CliGral.Guid ")
        sb.AppendLine("WHERE Impagats.Status< " & CInt(DTOImpagat.StatusCodes.Saldat) & " ")
        sb.AppendLine("AND Impagats.CcaIncobrable IS NULL ")
        sb.AppendLine("GROUP BY Csb.Guid, Csb.Eur, Impagats.Guid, Impagats.Gastos, Impagats.PagatACompte, Csb.Vto, Csb.CliGuid, CliGral.FullNom ")
        sb.AppendLine("ORDER BY CliGral.FullNom, Csb.Vto DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oCsb As New DTOCsb(oDrd("CsbGuid"))
            With oCsb
                .Contact = New DTOContact(oDrd("CliGuid"))
                .Contact.FullNom = oDrd("FullNom")
                .Amt = DTOAmt.Factory(CDec(oDrd("Eur")))
                .Vto = oDrd("Vto")
            End With

            Dim item As New DTOImpagat(oDrd("ImpagatGuid"))
            With item
                .Csb = oCsb
                .Gastos = DTOAmt.Factory(CDec(oDrd("Gastos")))
                .PagatACompte = DTOAmt.Factory(CDec(oDrd("PagatACompte")))
                '.Status = oDrd("Status")
                '.Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Update(oImpagats As List(Of DTOImpagat), ByRef oCca As DTOCca, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oImpagats, oCca, oTrans)
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

    Shared Sub Update(oImpagats As List(Of DTOImpagat), ByRef oCca As DTOCca, oTrans As SqlTransaction)
        For Each oImpagat As DTOImpagat In oImpagats
            CsbLoader.SetResult(oImpagat.Csb, oCca, DTOCsb.Results.Impagat, oTrans)
            CcaLoader.Update(oCca, oTrans)
            ImpagatLoader.Update(oImpagat, oTrans)
        Next
    End Sub

    Shared Function Kpis(oEmp As DTOEmp, fromYear As Integer) As List(Of DTOKpi)
        Dim paidKpi = DTOKpi.Factory(DTOKpi.Ids.Efectes_Vençuts)
        Dim unpaidKpi = DTOKpi.Factory(DTOKpi.Ids.Efectes_Impagats)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Year(Csb.Vto) AS Year, Month(Csb.Vto) AS Month ")
        sb.AppendLine(", SUM(CASE WHEN Csb.Result=1 THEN Csb.Eur ELSE 0 END) AS Paid ")
        sb.AppendLine(", SUM(CASE WHEN Csb.Result=3 THEN Csb.Eur ELSE 0 END) AS UnPaid ")
        sb.AppendLine("FROM Csb ")
        sb.AppendLine("INNER JOIN Csa ON Csb.CsaGuid = Csa.Guid ")
        sb.AppendLine("WHERE Csa.Emp=" & oEmp.Id & " ")
        sb.AppendLine("AND Year(Csb.Vto)>=" & fromYear & " ")
        sb.AppendLine("GROUP BY Year(Csb.Vto), Month(Csb.Vto) ")
        sb.AppendLine("ORDER BY Year(Csb.Vto) DESC, Month(Csb.Vto) DESC ")
        Dim SQL = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not IsDBNull(oDrd("Paid")) Then
                paidKpi.YearMonths.Add(New DTOYearMonth(oDrd("Year"), oDrd("Month"), oDrd("Paid")))
            End If
            If Not IsDBNull(oDrd("UnPaid")) Then
                unpaidKpi.YearMonths.Add(New DTOYearMonth(oDrd("Year"), oDrd("Month"), oDrd("UnPaid")))
            End If
        Loop
        oDrd.Close()

        Dim retval As New List(Of DTOKpi)
        retval.Add(paidKpi)
        retval.Add(unpaidKpi)
        Return retval
    End Function
End Class
