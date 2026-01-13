Public Class CsbLoader
#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOCsb
        Dim retval As DTOCsb = Nothing
        Dim oCsb As New DTOCsb(oGuid)
        If Load(oCsb) Then
            retval = oCsb
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oCsb As DTOCsb) As Boolean
        If Not oCsb.IsLoaded And Not oCsb.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Csb.CsaGuid, Csa.Csb, Csb.Doc, Csa.Fch, Csa.BancGuid, Csa.Gts, Csa.CcaGuid ")
            sb.AppendLine(", Csa.Descomptat, Csa.efts, Csa.Eur AS CsaEur, Csa.Gts, Csa.Dias, Csa.ImportMig, Csa.Tae ")
            sb.AppendLine(", Csa.FileFormat, Csa.CcaGuid, CsaIban.Ccc AS CsaCcc, CsaBanc.SepaCoreIdentificador ")
            sb.AppendLine(", Csb.Guid as CsbGuid ")
            sb.AppendLine(", Csb.Doc, Csb.CliGuid, Csb.Nom AS CliNom, Csb.Txt ")
            sb.AppendLine(", Csb.Result, Csb.Eur AS CsbEur, Csb.SepaMandato, Csb.SepaTipoAdeudo ")
            sb.AppendLine(", Csb.Vto, Csb.CcaVtoGuid ")
            sb.AppendLine(", CsaIban.BankBranch AS CsaBankBranch, CsaBn2.Bank AS CsaBankGuid, CsaBn1.Abr AS CsaBankAlias, CsaBn1.Nom AS CsaBankRaoSocial, CsaBn1.Swift AS CsaBankSwift")
            sb.AppendLine(", Csb.Ccc AS CsbCcc, CsbIban.Mandato_Fch, CsbIban.BankBranch AS CsbBankBranch, CsbBn2.Location AS Bn2LocationGuid, Bn2Location.Nom AS Bn2LocationNom, CsbBn2.Adr AS Bn2Adr, CsbBn2.Bank AS CsbBankGuid, CsbBn1.Abr AS CsbBankAlias, CsbBn1.Nom AS CsbBankRaoSocial, CsbBn1.Swift AS CsbBankSwift")
            sb.AppendLine(", Pnd.Guid AS PndGuid ")
            sb.AppendLine("FROM Csa ")
            sb.AppendLine("INNER JOIN Iban AS CsaIban ON Csa.BancGuid = CsaIban.ContactGuid AND CsaIban.Cod=" & CInt(DTOIban.Cods.Banc) & " ")
            sb.AppendLine("INNER JOIN Bn2 AS CsaBn2 ON CsaIban.BankBranch = CsaBn2.Guid ")
            sb.AppendLine("INNER JOIN Bn1 AS CsaBn1 ON CsaBn2.Bank = CsaBn1.Guid ")
            sb.AppendLine("INNER JOIN CliBnc AS CsaBanc ON Csa.BancGuid = CsaBanc.Guid ")

            sb.AppendLine("INNER JOIN Csb ON Csa.Guid = Csb.CsaGuid ")
            sb.AppendLine("LEFT OUTER JOIN Iban AS CsbIban ON Csb.SepaMandato = CsbIban.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Bn2 AS CsbBn2 ON CsbIban.BankBranch = CsbBn2.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Location AS Bn2Location ON CsbBn2.Location= Bn2Location.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Bn1 AS CsbBn1 ON CsbBn2.Bank = CsbBn1.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Pnd ON Csb.Guid = Pnd.CsbGuid ")

            sb.AppendLine("LEFT OUTER JOIN Cca ON Csa.CcaGuid = Cca.Guid ")
            sb.AppendLine("INNER JOIN CliGral ON Csb.CliGuid = CliGral.Guid ")
            sb.AppendLine("WHERE Csb.Guid='" & oCsb.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then

                Dim oCsaBankBranch As DTOBankBranch = Nothing
                If Not IsDBNull(oDrd("CsaBankBranch")) And Not IsDBNull(oDrd("CsaBankGuid")) Then
                    Dim oCsaBank As New DTOBank(oDrd("CsaBankGuid"))
                    With oCsaBank
                        .NomComercial = oDrd("CsaBankAlias")
                        .RaoSocial = oDrd("CsaBankRaoSocial")
                        .Swift = oDrd("CsaBankSwift")
                    End With
                    oCsaBankBranch = New DTOBankBranch(oDrd("CsaBankBranch"))
                    oCsaBankBranch.Bank = oCsaBank
                End If

                Dim oCsaIban As New DTOIban()
                With oCsaIban
                    .Digits = oDrd("CsaCcc")
                    .BankBranch = oCsaBankBranch
                End With

                Dim oBanc As New DTOBanc(oDrd("BancGuid"))
                With oBanc
                    .Nom = SQLHelper.GetStringFromDataReader(oDrd("CsaBankRaoSocial"))
                    .Abr = SQLHelper.GetStringFromDataReader(oDrd("CsaBankAlias"))
                    .Iban = oCsaIban
                    .SepaCoreIdentificador = SQLHelper.GetStringFromDataReader(oDrd("SepaCoreIdentificador"))
                End With

                Dim oCsa As New DTOCsa(oDrd("CsaGuid"))
                With oCsa
                    .Id = oDrd("Csb")
                    .Fch = oDrd("Fch")
                    .banc = oBanc
                    .descomptat = oDrd("Descomptat")
                    .efectes = SQLHelper.GetIntegerFromDataReader(oDrd("efts"))
                    .Nominal = SQLHelper.GetAmtFromDataReader(oDrd("CsaEur"))
                    .Despeses = SQLHelper.GetAmtFromDataReader(oDrd("Gts"))
                    .Dias = SQLHelper.GetIntegerFromDataReader(oDrd("Dias"))
                    .ImportMig = SQLHelper.GetDecimalFromDataReader(oDrd("ImportMig"))
                    .Tae = SQLHelper.GetDecimalFromDataReader(oDrd("Tae"))
                    .FileFormat = oDrd("FileFormat")
                    If Not IsDBNull(oDrd("CcaGuid")) Then
                        .Cca = New DTOCca(oDrd("CcaGuid"))
                    End If
                    .Items = New List(Of DTOCsb)
                End With

                Dim oContact As New DTOContact(oDrd("CliGuid"))
                With oContact
                    .Nom = SQLHelper.GetStringFromDataReader(oDrd("CliNom"))
                End With

                Dim oCsbBankBranch As DTOBankBranch = Nothing
                If Not IsDBNull(oDrd("CsbBankBranch")) And Not IsDBNull(oDrd("CsbBankGuid")) Then
                    Dim oCsbBank As New DTOBank(oDrd("CsbBankGuid"))
                    With oCsbBank
                        .NomComercial = oDrd("CsbBankAlias")
                        .RaoSocial = oDrd("CsbBankRaoSocial")
                        .Swift = oDrd("CsbBankSwift")
                    End With
                    oCsbBankBranch = New DTOBankBranch(oDrd("CsbBankBranch"))
                    With oCsbBankBranch
                        .Bank = oCsbBank
                        .Address = SQLHelper.GetStringFromDataReader(oDrd("Bn2Adr"))
                        If Not IsDBNull(oDrd("Bn2LocationGuid")) Then
                            .Location = New DTOLocation(oDrd("Bn2LocationGuid"))
                            .Location.Nom = SQLHelper.GetStringFromDataReader(oDrd("Bn2LocationNom"))
                        End If
                    End With
                End If

                Dim oIban As New DTOIban(oDrd("SepaMandato"))
                With oIban
                    .Titular = oContact
                    .Cod = DTOIban.Cods.Client
                    .Digits = SQLHelper.GetStringFromDataReader(oDrd("CsbCcc"))
                    .FchFrom = SQLHelper.GetFchFromDataReader(oDrd("Mandato_Fch"))
                    .BankBranch = oCsbBankBranch
                End With



                With oCsb
                    .Csa = oCsa
                    .Id = oDrd("Doc")
                    .Contact = oContact
                    .Txt = SQLHelper.GetStringFromDataReader(oDrd("Txt"))
                    .Vto = oDrd("Vto")
                    If Not IsDBNull(oDrd("CcaVtoGuid")) Then
                        .ResultCca = New DTOCca(oDrd("CcaVtoGuid"))
                    End If
                    If Not IsDBNull(oDrd("PndGuid")) Then
                        .Pnd = New DTOPnd(oDrd("PndGuid"))
                    End If
                    .Result = oDrd("Result")
                    .Amt = DTOAmt.Factory(oDrd("CsbEur"))
                    .Iban = oIban
                    .SepaTipoAdeudo = oDrd("SepaTipoAdeudo")
                    .IsLoaded = True
                End With

                oCsa.Items.Add(oCsb)

                oDrd.Close()
            End If
        End If
        Dim retval As Boolean = oCsb.IsLoaded
        Return retval
    End Function

    Shared Function Update(oCsb As DTOCsb, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oCsb, oTrans)
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


    Shared Sub Update(oCsb As DTOCsb, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Csb ")
        sb.AppendLine("WHERE Guid='" & oCsb.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oCsb.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oCsb
            oRow("Doc") = .Id
            oRow("CsaGuid") = SQLHelper.NullableBaseGuid(.Csa)
            oRow("CliGuid") = SQLHelper.NullableBaseGuid(.Contact)
            oRow("Nom") = .Contact.Nom
            oRow("Txt") = SQLHelper.NullableString(.Txt)
            oRow("Vto") = .Vto.Date
            oRow("CcaVtoGuid") = SQLHelper.NullableBaseGuid(.ResultCca)
            oRow("Result") = .Result

            'oRow("Impagat") = (.Result = DTOCsb.Results.Impagat) '------------TO DEPRECATE
            'oRow("Reclamat") = (.Result = DTOCsb.Results.Reclamat) '----------TO DEPRECATE

            If .Amt IsNot Nothing Then
                oRow("Eur") = .Amt.Eur
                oRow("Cur") = .Amt.Cur.Tag
                oRow("Val") = .Amt.Val
            End If

            oRow("Ccc") = .Iban.Digits
            oRow("SepaMandato") = .Iban.Guid
            oRow("SepaTipoAdeudo") = .SepaTipoAdeudo

        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oCsb As DTOCsb, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oCsb, oTrans)
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


    Shared Sub Delete(oCsb As DTOCsb, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Csb WHERE Guid='" & oCsb.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub


    Shared Function SaveVto(ByRef oCsb As DTOCsb, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            'guarda l'assentament
            CcaLoader.Update(oCsb.ResultCca, oTrans)

            'actualitza l'efecte
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("UPDATE Csb SET Result=" & CInt(DTOCsb.Results.Vençut) & ", CcaVtoGuid='" & oCsb.ResultCca.Guid.ToString & "' ")
            sb.AppendLine("FROM Csb ")
            sb.AppendLine("WHERE Guid='" & oCsb.Guid.ToString & "' ")
            Dim SQL As String = sb.ToString
            Dim iCount As Integer = SQLHelper.ExecuteNonQuery(SQL, oTrans)

            'dona de baixa el pendent
            sb = New System.Text.StringBuilder
            sb.AppendLine("UPDATE Pnd ")
            sb.AppendLine("SET Status=" & CInt(DTOPnd.StatusCod.saldat) & " ")
            sb.AppendLine(", StatusGuid='" & oCsb.ResultCca.Guid.ToString & "' ")
            sb.AppendLine("WHERE CsbGuid ='" & oCsb.Guid.ToString & "' ")
            SQL = sb.ToString
            iCount = SQLHelper.ExecuteNonQuery(SQL, oTrans)

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


    Shared Function RevertVto(ByRef oCca As DTOCca, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            'actualitza l'efecte
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("UPDATE Csb SET Result=0, CcaVtoGuid=NULL ")
            sb.AppendLine("FROM Csb ")
            sb.AppendLine("WHERE CcaVtoGuid='" & oCca.Guid.ToString & "' ")
            Dim SQL As String = sb.ToString
            Dim iCount As Integer = SQLHelper.ExecuteNonQuery(SQL, oTrans)

            'Recupera el pendent
            sb = New System.Text.StringBuilder
            sb.AppendLine("UPDATE Pnd ")
            sb.AppendLine("SET Status=" & CInt(DTOPnd.StatusCod.pendent) & " ")
            sb.AppendLine(", StatusGuid=NULL ")
            sb.AppendLine("WHERE StatusGuid ='" & oCca.Guid.ToString & "' ")
            SQL = sb.ToString
            iCount = SQLHelper.ExecuteNonQuery(SQL, oTrans)

            'elimina l'assentament
            CcaLoader.Delete(oCca, oTrans)

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


    Shared Sub SetResult(ByRef oCsb As DTOCsb, oCcaResult As DTOCca, oResult As DTOCsb.Results, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("UPDATE Csb ")
        sb.AppendLine("SET Csb.Result=" & DTOCsb.Results.Impagat & " ")
        If oCsb.ResultCca Is Nothing Then
            sb.AppendLine(", Csb.CcaVtoGuid=NULL ")
        Else
            sb.AppendLine(", Csb.CcaVtoGuid='" & oCsb.ResultCca.Guid.ToString & "' ")
        End If

        sb.AppendLine("WHERE Csb.Guid ='" & oCsb.Guid.ToString & "' ")

        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub


    Shared Function Reclama(oUser As DTOUser, oCsb As DTOCsb, ByRef oCca As DTOCca, oPnd As DTOPnd, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean


        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction

        Try
            'retrocedeix venciment anterior si n'hi ha
            If oCsb.ResultCca IsNot Nothing Then
                CcaLoader.Load(oCsb.ResultCca)
                Dim iLastBlockedCcaYea As Integer = DefaultLoader.EmpInteger(DTODefault.Codis.LastBlockedCcaYea, oUser.Emp)
                If oCsb.ResultCca.Fch.Year > iLastBlockedCcaYea Then
                    CcaLoader.Delete(oCsb.ResultCca, oTrans)
                Else
                    'filtra els assentaments no trobats
                    If oCsb.ResultCca.Fch.Year > 1985 Then
                        Throw New Exception("no es pot retrocedir l'assentament del venciment perque el llibre de l'any passat está ja tancat")
                    End If
                End If
            End If

            PndLoader.Update(oPnd, oTrans) 'torna a posar l'efecte en circulació
            CcaLoader.Update(oCca, oTrans)

            oCsb.Result = DTOCsb.Results.Reclamat
            oCsb.ResultCca = oCca
            Update(oCsb, oTrans)


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

    Shared Function RetrocedeixReclamacio(oUser As DTOUser, oCsb As DTOCsb, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction

        Try
            'retrocedeix venciment anterior si n'hi ha
            If oCsb.ResultCca IsNot Nothing Then
                Dim iLastBlockedCcaYea As Integer = DefaultLoader.EmpInteger(DTODefault.Codis.LastBlockedCcaYea, oUser.Emp)
                CcaLoader.Load(oCsb.ResultCca)
                If oCsb.ResultCca.Fch.Year >= 1985 Then
                    If oCsb.ResultCca.Fch.Year > iLastBlockedCcaYea Then
                        CcaLoader.Delete(oCsb.ResultCca, oTrans)
                    Else
                        Throw New Exception("no es pot retrocedir l'assentament del venciment perque el llibre de l'any passat está ja tancat")
                    End If
                End If
            End If

            oCsb.Result = DTOCsb.Results.Pendent
            oCsb.ResultCca = Nothing
            Update(oCsb, oTrans)

            oCsb.Pnd.Status = DTOPnd.StatusCod.enCirculacio
            PndLoader.Update(oCsb.Pnd, oTrans)

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

Public Class CsbsLoader

    Shared Function All(oEmp As DTOEmp,
                        Optional banc As DTOBanc = Nothing,
                        Optional year As Integer = 0,
                        Optional customer As DTOContact = Nothing) As List(Of DTOCsb)
        Dim retval As New List(Of DTOCsb)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Csb.Guid, Csb.CsaGuid, Csb.CliGuid, Csb.Eur ")
        sb.AppendLine(", Csb.Vto, Csb.Txt, Csb.Result, Csb.CcaVtoGuid, CliGral.RaoSocial ")
        sb.AppendLine("From Csb ")
        sb.AppendLine("INNER JOIN CliGral ON Csb.CliGuid = CliGral.Guid ")
        sb.AppendLine("INNER JOIN Csa ON Csb.CsaGuid = Csa.Guid ")
        sb.AppendLine("WHERE CliGral.Emp = " & oEmp.Id)
        If banc IsNot Nothing Then
            sb.AppendLine("AND Csa.BancGuid = '" & banc.Guid.ToString & "' ")
        End If
        If year > 0 Then
            sb.AppendLine("AND Csa.Yea=" & year & " ")
        End If
        If customer IsNot Nothing Then
            sb.AppendLine("AND Csb.CliGuid='" & customer.Guid.ToString & "' ")
        End If

        sb.AppendLine("ORDER BY Vto DESC, CliGral.RaoSocial ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOCsb(oDrd("Guid"))
            With item
                .Csa = New DTOCsa(oDrd("CsaGuid"))
                If customer Is Nothing Then
                    .Contact = New DTOCustomer(oDrd("CliGuid"))
                    .Contact.nom = oDrd("RaoSocial")
                Else
                    .Contact = customer
                End If
                .Amt = DTOAmt.Factory(CDec(oDrd("Eur")))
                .Vto = oDrd("Vto")
                .Txt = SQLHelper.GetStringFromDataReader(oDrd("Txt"))
                .Result = oDrd("Result")
                If Not IsDBNull(oDrd("CcaVtoGuid")) Then
                    .ResultCca = New DTOCca(oDrd("CcaVtoGuid"))
                End If

            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function CsbResults(oEmp As DTOEmp) As List(Of DTOCsbResult)
        Dim retval As New List(Of DTOCsbResult)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Csb.Vto, Csb.Result, SUM(Csb.Eur) AS Eur ")
        sb.AppendLine("FROM Csb ")
        sb.AppendLine("INNER JOIN Csa ON Csb.CsaGuid = Csa.Guid ")
        sb.AppendLine("WHERE Csa.Emp = " & oEmp.Id & " ")
        sb.AppendLine("GROUP BY Csb.Vto, Csb.Result ")
        sb.AppendLine("ORDER BY Csb.Vto DESC, Csb.Result ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOCsbResult
            With item
                .vto = oDrd("Vto")
                .result = oDrd("Result")
                .Eur = oDrd("Eur")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oIban As DTOIban) As List(Of DTOCsb)
        IbanLoader.Load(oIban)

        Dim retval As New List(Of DTOCsb)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Csb.Guid, Csb.CsaGuid, Csb.Doc AS CsbId, Csb.CliGuid, Csb.Eur, Csb.Vto, Csb.Txt, Csb.Result, Csb.CcaVtoGuid ")
        sb.AppendLine(", Csa.BancGuid, Csa.BancGuid, CliBnc.Abr, Csa.Csb AS CsaId, Csa.Fch ")
        sb.AppendLine("From Csb ")
        sb.AppendLine("INNER JOIN Csa ON Csb.CsaGuid = Csa.Guid ")
        sb.AppendLine("INNER JOIN CliBnc ON Csa.BancGuid= CliBnc.Guid ")
        sb.AppendLine("WHERE Csb.SepaMandato = '" & oIban.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY Csa.Yea DESC, Csa.Csb DESC, Csb.Doc")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oBanc As New DTOBanc(oDrd("BancGuid"))
            oBanc.Abr = oDrd("Abr")

            Dim oCsa As New DTOCsa(oDrd("CsaGuid"))
            With oCsa
                .Banc = oBanc
                .Fch = oDrd("Fch")
                .Id = oDrd("CsaId")
            End With

            Dim item As New DTOCsb(oDrd("Guid"))
            With item
                .Id = oDrd("CsbId")
                .Contact = oIban.Titular
                .Csa = oCsa
                .Amt = DTOAmt.Factory(CDec(oDrd("Eur")))
                .Vto = oDrd("Vto")
                .Txt = SQLHelper.GetStringFromDataReader(oDrd("Txt"))
                .Result = oDrd("Result")
                If Not IsDBNull(oDrd("CcaVtoGuid")) Then
                    .ResultCca = New DTOCca(oDrd("CcaVtoGuid"))
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function PendentsDeVto(oEmp As DTOEmp, DtFch As Date, Optional ByRef ShowProgress As ProgressBarHandler = Nothing) As List(Of DTOCsb)
        Dim CancelRequest As Boolean
        If ShowProgress IsNot Nothing Then
            ShowProgress(0, 1000, 0, "llegint efectes de la base de dades", CancelRequest)
        End If

        Dim retval As New List(Of DTOCsb)
        Dim sb As New System.Text.StringBuilder

        sb.AppendLine("SELECT Csb.Guid AS CsbGuid, Csa.Guid AS CsaGuid, Csa.Yea, Csa.Csb AS CsaId, Csb.Doc AS CsbId ")
        sb.AppendLine(", Csa.BancGuid, CliBnc.Abr ")
        sb.AppendLine(", Csa.Fch, Csb.Vto, Csb.Eur, Csb.Txt, Csb.Ccc, Csb.CliGuid, CliGral.RaoSocial ")
        sb.AppendLine(", CcaVtoGuid, Result ")
        sb.AppendLine("From Csb ")
        sb.AppendLine("INNER JOIN Csa ON Csb.CsaGuid = Csa.Guid ")
        sb.AppendLine("INNER JOIN CliBnc ON Csa.BancGuid= CliBnc.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Cca AS CcaVto ON Csb.CcaVtoGuid=CcaVto.Guid ")
        sb.AppendLine("WHERE Csa.Emp=" & oEmp.Id & " ")
        sb.AppendLine("AND Csa.Descomptat=1 ")
        sb.AppendLine("AND CcaVto.Yea<>" & DtFch.Year & " ") 'evita els reclamats abans de cap d'any
        sb.AppendLine("AND ('" & Format(DtFch, "yyyyMMdd") & "' BETWEEN Csa.fch AND CSB.vto) ")
        sb.AppendLine("ORDER BY CliBnc.Abr, Csa.BancGuid, Csa.Yea, Csa.Csb, CAST(Csb.Doc AS INT) ")
        Dim SQL As String = sb.ToString

        Dim oBanc As New DTOBanc()
        Dim exs As New List(Of Exception)
        Dim oDs As DataSet = SQLHelper.GetDataset(SQL, exs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each oRow As DataRow In oTb.Rows
            If Not oBanc.Guid.Equals(oRow("BancGuid")) Then
                oBanc = New DTOBanc(oRow("BancGuid"))
                oBanc.Abr = oRow("Abr")
            End If

            Dim oTitular As New DTOContact(oRow("CliGuid"))
            oTitular.Nom = oRow("RaoSocial")

            Dim oCsa As New DTOCsa(oRow("CsaGuid"))
            With oCsa
                .Banc = oBanc
                .Fch = oRow("Fch")
                .Id = oRow("CsaId")
            End With

            Dim item As New DTOCsb(oRow("CsbGuid"))
            With item
                .Id = oRow("CsbId")
                .Contact = oTitular
                .Csa = oCsa
                .Amt = DTOAmt.Factory(CDec(oRow("Eur")))
                .Vto = oRow("Vto")
                .Txt = SQLHelper.GetStringFromDataReader(oRow("Txt"))
                .Result = oRow("Result")
                If Not IsDBNull(oRow("CcaVtoGuid")) Then
                    .ResultCca = New DTOCca(oRow("CcaVtoGuid"))
                End If
            End With
            retval.Add(item)
            If ShowProgress IsNot Nothing Then
                ShowProgress(0, oTb.Rows.Count, retval.Count, "llegint efectes de la base de dades", CancelRequest)
            End If
        Next
        Return retval
    End Function

    Shared Function PendentsDeGirar(oEmp As DTOEmp, exs As List(Of Exception), Optional oCountry As DTOCountry = Nothing, Optional blSepa As Boolean = True) As List(Of DTOCsb)
        Dim retval As New List(Of DTOCsb)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Pnd.Guid AS PndGuid, Pnd.Eur, Pnd.Vto, Pnd.ContactGuid ,Pnd.Fra, Fra.Fch AS FraFch ")
        sb.AppendLine(", CliGral.Cli, CliGral.RaoSocial, Iban.Guid AS IbanGuid, Iban.Ccc ")
        sb.AppendLine(", Iban.BankBranch, Iban.Mandato_Fch, Bn2.Bank, Bn1.Abr AS BankAbr, Bn1.Nom AS BankNom, Bn1.Sepa, Bn1.Swift, Iban.Hash ")
        sb.AppendLine(", Bn1.Country AS Bn1Country, Country.ExportCod AS Bn1ExportCod, Country.ISO as CountryISO ")
        sb.AppendLine(", VwAddress.* ")
        sb.AppendLine("FROM Pnd ")
        sb.AppendLine("INNER JOIN CliGral ON Pnd.ContactGuid = CliGral.Guid ")
        'sb.AppendLine("LEFT OUTER JOIN Iban ON Iban.ContactGuid = Pnd.ContactGuid AND (Mandato_Fch IS NULL OR Mandato_Fch<=GETDATE()) AND (Caduca_Fch IS NULL OR Caduca_Fch>GETDATE()) ")
        sb.AppendLine("LEFT OUTER JOIN Iban ON Iban.ContactGuid = Pnd.ContactGuid AND (Mandato_Fch IS NULL OR Mandato_Fch<=Pnd.Vto) AND (Caduca_Fch IS NULL OR Caduca_Fch>Pnd.Vto) ")
        If blSepa Then
            sb.AppendLine("AND FchApproved IS NOT NULL ")
        End If
        sb.AppendLine("LEFT OUTER JOIN Bn2 ON Iban.BankBranch = Bn2.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Bn1 ON Bn2.Bank = Bn1.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Country ON Bn1.Country = Country.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Fra ON Pnd.FraGuid = Fra.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwAddress ON Pnd.ContactGuid = VwAddress.SrcGuid ")
        sb.AppendLine("WHERE Pnd.Emp=" & oEmp.Id & " ")
        sb.AppendLine("AND (Pnd.Cfp=" & DTOPaymentTerms.CodsFormaDePago.DomiciliacioBancaria & " OR Pnd.cfp=" & DTOPaymentTerms.CodsFormaDePago.EfteAndorra & ") ")
        sb.AppendLine("AND Pnd.Ad='D' ")
        sb.AppendLine("AND Pnd.CsbGuid IS NULL ")
        sb.AppendLine("AND Pnd.Status=0 ")

        If oCountry IsNot Nothing Then
            sb.AppendLine("AND (Iban.Ccc IS NULL OR Iban.Ccc LIKE '" & oCountry.ISO & "%') ")
        End If
        sb.AppendLine("ORDER BY Pnd.Vto ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oCustomer As New DTOCustomer(oDrd("ContactGuid"))
            With oCustomer
                .Nom = oDrd("RaoSocial")
                .Id = oDrd("Cli")
                .Address = SQLHelper.GetAddressFromDataReader(oDrd)
            End With
            'If oDrd("ccc").ToString.StartsWith("AD55") Then Stop
            Dim oIban As DTOIban = Nothing
            If IsDBNull(oDrd("Ccc")) Then
                exs.Add(New Exception("Client  sense Iban: " & CDate(oDrd("Vto")).ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES")) & " " & oCustomer.nom))

            Else
                Dim oBankBranch As DTOBankBranch = Nothing
                If Not IsDBNull(oDrd("BankBranch")) Then
                    Dim oBank As DTOBank = Nothing
                    If Not IsDBNull(oDrd("BankBranch")) Then
                        oBank = New DTOBank(oDrd("Bank"))
                        oBank.RaoSocial = SQLHelper.GetStringFromDataReader(oDrd("BankNom"))
                        oBank.NomComercial = SQLHelper.GetStringFromDataReader(oDrd("BankAbr"))
                        oBank.Swift = oDrd("Swift")
                        oBank.SEPAB2B = oDrd("Sepa")
                        If Not IsDBNull(oDrd("Bn1Country")) Then
                            oBank.Country = New DTOCountry(oDrd("Bn1Country"))
                            oBank.Country.ExportCod = oDrd("Bn1ExportCod")
                            oBank.Country.ISO = oDrd("CountryISO")
                        End If
                    End If
                    oBankBranch = New DTOBankBranch(oDrd("BankBranch"))
                    oBankBranch.Bank = oBank
                End If

                oIban = New DTOIban(oDrd("IbanGuid"))
                With oIban
                    .Digits = oDrd("Ccc")
                    .FchFrom = SQLHelper.GetFchFromDataReader(oDrd("Mandato_Fch"))
                    .BankBranch = oBankBranch
                    If Not IsDBNull(oDrd("Hash")) Then
                        .DocFile = New DTODocFile
                        .DocFile.Hash = oDrd("Hash")
                    End If
                End With

                Dim oPnd As New DTOPnd(oDrd("PndGuid"))

                If oIban.IsSepa = blSepa Then

                    Dim oEfecte As New DTOCsb
                    With oEfecte

                        .Iban = oIban
                        .Contact = oCustomer
                        .Amt = DTOAmt.Factory(CDec(oDrd("Eur")))
                        .Vto = oDrd("Vto")

                        If .Amt.Eur = 0 Then
                            exs.Add(New Exception("efecte venciment " & .vto.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES")) & " sense import: " & oCustomer.FullNom))
                        End If

                        .Pnd = oPnd
                        If IsDBNull(oDrd("Fra")) Then
                        Else
                            If IsDBNull(oDrd("FraFch")) Then
                                If IsNumeric(oDrd("Fra")) Then
                                    .Txt = "factura " & SQLHelper.GetStringFromDataReader(oDrd("Fra"))
                                Else
                                    .Txt = SQLHelper.GetStringFromDataReader(oDrd("Fra"))
                                End If
                            Else
                                Dim DtFraFch As Date = oDrd("FraFch")
                                .FraNum = oDrd("Fra")
                                .FraYea = DtFraFch.Year
                                .txt = "factura " & .fraNum & " del " & DtFraFch.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"))
                            End If
                        End If

                    End With
                    retval.Add(oEfecte)
                End If
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function PendentsDeVencer(oEmp As DTOEmp) As List(Of DTOCsb)
        Dim retval As New List(Of DTOCsb)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Csb.CsaGuid, Csa.Csb AS CsaId, Csa.Fch AS CsaFch, Csa.Descomptat, Csa.BancGuid ")
        sb.AppendLine(", Csb.Guid AS CsbGuid, Csb.Doc AS CsbId, Csb.Vto, Csb.Eur, Csb.CliGuid, Csb.Txt ")
        sb.AppendLine(", Fra.Fra, Fra.Fch AS FraFch ")
        sb.AppendLine(", Pnd.Guid AS PndGuid ")
        sb.AppendLine("FROM Csb ")
        sb.AppendLine("INNER JOIN Csa ON Csb.CsaGuid = Csa.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Pnd ON Csb.Guid = Pnd.CsbGuid ")
        sb.AppendLine("LEFT OUTER JOIN Fra ON Csa.Emp = Fra.Emp AND Fra.Yea=Pnd.Yef AND Fra.Fra=(CASE WHEN (ISNUMERIC(Pnd.Fra)=1 AND CHARINDEX(',',Pnd.Fra)<0) THEN Pnd.Fra ELSE -1 END) ")
        sb.AppendLine("WHERE Csa.Emp = " & oEmp.Id & " ")
        sb.AppendLine("AND Csb.Result=" & CInt(DTOCsb.Results.Pendent) & " ")
        sb.AppendLine("AND Csb.Vto <= '" & Format(DTO.GlobalVariables.Today(), "yyyyMMdd") & "' ")
        sb.AppendLine("ORDER BY Csb.Vto, Year(Csa.Fch), Csa.Csb, Csb.Doc ")

        Dim oCsa = DTOCsa.Factory(oEmp)
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oCsa.Guid.Equals(oDrd("CsaGuid")) Then
                oCsa = New DTOCsa(oDrd("CsaGuid"))
                With oCsa
                    .Id = oDrd("CsaId")
                    .Fch = oDrd("CsaFch")
                    .Banc = New DTOBanc(oDrd("BancGuid"))
                    .Descomptat = oDrd("Descomptat")
                End With
            End If

            Dim oPnd As DTOPnd = Nothing
            If Not IsDBNull(oDrd("PndGuid")) Then
                oPnd = New DTOPnd(oDrd("PndGuid"))
            End If

            Dim item As New DTOCsb(oDrd("CsbGuid"))
            With item
                .Csa = oCsa
                .Id = oDrd("CsbId")
                .Vto = oDrd("Vto")
                .Amt = DTOAmt.Factory(oDrd("Eur"))
                .Contact = New DTOContact(oDrd("CliGuid"))
                .Pnd = oPnd
                .Txt = SQLHelper.GetStringFromDataReader(oDrd("Txt"))
            End With

            retval.Add(item)
        Loop
        oDrd.Close()

        Return retval
    End Function


    Shared Function NextVtosToNotify(oEmp As DTOEmp, Fch As Date) As List(Of DTOCsb)
        Dim retval As New List(Of DTOCsb)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Csb.Guid, Csb.CsaGuid, Csb.Doc, Csb.SepaMandato, Csb.Ccc, Csb.CliGuid, Csb.Vto, Csb.Eur, Csa.Csb AS CsaId ")
        sb.AppendLine(", CliGral.LangId, CliGral.RaoSocial ")
        sb.AppendLine("FROM Csb ")
        sb.AppendLine("INNER JOIN Csa ON Csb.CsaGuid = Csa.Guid ")
        sb.AppendLine("INNER JOIN CliGral ON Csb.CliGuid=CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN MailingLog ON Csb.Guid=MailingLog.Guid ")
        sb.AppendLine("WHERE MailingLog.Guid IS NULL ")
        sb.AppendLine("AND CliGral.Emp = " & CInt(oEmp.Id) & " ")
        sb.AppendLine("AND CcaVtoGuid IS NULL ")
        sb.AppendLine("AND (Csb.Vto BETWEEN '" & Format(DTO.GlobalVariables.Today(), "yyyyMMdd") & "' AND '" & Format(Fch, "yyyyMMdd") & "') ")
        sb.AppendLine("AND Csb.Result =" & DTOCsb.Results.Pendent & " ")
        sb.AppendLine("ORDER BY Csb.VTO")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOCsb(oDrd("Guid"))
            With item
                .Csa = New DTOCsa(oDrd("CsaGuid"))
                .Csa.id = oDrd("CsaId")
                .Id = oDrd("Doc")
                .Iban = New DTOIban(oDrd("SepaMandato"))
                .Iban.Digits = oDrd("Ccc")
                .Contact = New DTOContact(oDrd("CliGuid"))
                .Contact.Lang = SQLHelper.GetLangFromDataReader(oDrd("LangId"))
                .Contact.Nom = oDrd("RaoSocial")
                .Vto = oDrd("Vto")
                .Amt = DTOAmt.Factory(CDec(oDrd("Eur")))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function mailingLogs(oEmp As DTOEmp, year As Integer) As List(Of DTOCsb)
        Dim retval As New List(Of DTOCsb)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT MailingLog.fch ")
        sb.AppendLine(", MailingLog.Usuari, Email.adr, Email.Nickname ")
        sb.AppendLine(", Csb.Guid, Csb.vto, Csb.Eur, Csb.CliGuid, Csb.Nom, Csb.txt ")
        sb.AppendLine("FROM Csb ")
        sb.AppendLine("INNER JOIN MailingLog ON Csb.Guid = MailingLog.Guid ")
        sb.AppendLine("INNER JOIN Email ON MailingLog.Usuari = Email.Guid ")
        sb.AppendLine("WHERE YEAR(MailingLog.Fch)=" & year & " ")
        sb.AppendLine("ORDER BY Csb.Vto DESC, Csb.Guid, MailingLog.Fch ")
        Dim SQL = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Dim oCsb As New DTOCsb
        Do While oDrd.Read
            If Not oCsb.Guid.Equals(oDrd("Guid")) Then
                oCsb = New DTOCsb(oDrd("Guid"))
                With oCsb
                    .Vto = oDrd("Vto")
                    .Amt = SQLHelper.GetAmtFromDataReader(oDrd("Eur")).Trimmed
                    .Contact = New DTOContact(oDrd("CliGuid"))
                    .Contact.nom = oDrd("Nom")
                    .Txt = SQLHelper.GetStringFromDataReader(oDrd("txt"))
                End With
                retval.Add(oCsb)
            End If

            Dim oLog As New DTOMailingLog()
            With oLog
                .user = New DTOUser(oDrd("Usuari"))
                .user.nickName = SQLHelper.GetStringFromDataReader(oDrd("Nickname"))
                .user.emailAddress = oDrd("adr")
                .fch = oDrd("Fch")
            End With
            oCsb.mailingLogs.Add(oLog)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function CountPerMandato(oIban As DTOIban) As Integer
        Dim retval As Integer
        Dim SQL As String = "SELECT Count(DISTINCT Guid) AS Csbs FROM CSB WHERE SepaMandato='" & oIban.Guid.ToString & "' "
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        oDrd.Read()
        If Not IsDBNull(oDrd("Csbs")) Then
            retval = oDrd("Csbs")
        End If
        oDrd.Close()
        Return retval
    End Function
End Class
