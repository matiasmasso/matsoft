Public Class XecLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOXec
        Dim retval As DTOXec = Nothing
        Dim oXec As New DTOXec(oGuid)
        If Load(oXec) Then
            retval = oXec
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oXec As DTOXec) As Boolean
        If Not oXec.IsLoaded And Not oXec.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Xec.Guid, Xec.XecNum, Xec.Vto, Xec.Eur, Xec.FchRecepcio ")
            sb.AppendLine(", Xec.ContactGuid, NomDebtor.FullNom AS NomDebtor, Xec.NBancGuid, NomNBanc.FullNom AS NomNBanc ")
            sb.AppendLine(", Xec.CcaRebut, CcaRebut.Fch AS CcaRebutFch, CcaRebut.txt AS CcaRebutTxt ")
            sb.AppendLine(", Xec.CcaPresentacio, CcaPresentacio.Fch AS CcaPresentacioFch, CcaPresentacio.txt AS CcaPresentacioTxt ")
            sb.AppendLine(", Xec.CcaVto, CcaVto.Fch AS CcaVtoFch, CcaVto.txt AS CcaVtoTxt ")
            sb.AppendLine(", Xec.CodPresentacio, Xec.StatusCod ")
            sb.AppendLine(", Xec.Iban, Bn1.Guid AS Bn1Guid, Bn1.Bn1, Bn1.Nom AS Bn1Nom, Bn1.Abr, Bn1.Swift, Bn2.Guid as Bn2Guid, Bn2.Agc, Bn2.Adr, Bn2.Location  ")
            sb.AppendLine(", Location.Nom AS LocationNom ")
            sb.AppendLine(", XecDetail.PndGuid, Pnd.Eur AS PndEur, Pnd.Fra, Pnd.Fch AS PndFch, Pnd.Vto AS PndVto, Pnd.AD ")
            sb.AppendLine(", Pnd.CtaGuid, PgcCta.Id AS CtaId, PgcCta.Esp AS CtaEsp, PgcCta.Cat AS CtaCat ")
            sb.AppendLine("FROM Xec ")
            sb.AppendLine("LEFT OUTER JOIN XecDetail ON Xec.Guid=XecDetail.Xec ")
            sb.AppendLine("LEFT OUTER JOIN Pnd ON XecDetail.PndGuid=Pnd.Guid ")
            sb.AppendLine("LEFT OUTER JOIN PgcCta ON Pnd.CtaGuid=PgcCta.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Country ON Country.Iso=SUBSTRING(Xec.Iban,1,2) ")
            sb.AppendLine("LEFT OUTER JOIN Bn2 ON Xec.SBankBranch=Bn2.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Bn1 ON Bn2.Bank = Bn1.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Location ON Bn2.Location =Location.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Cca AS CcaRebut ON Xec.CcaRebut=CcaRebut.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Cca AS CcaPresentacio ON Xec.CcaPresentacio=CcaPresentacio.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Cca AS CcaVto ON Xec.CcaVto=CcaVto.Guid ")
            sb.AppendLine("INNER JOIN CliGral AS NomDebtor ON Xec.ContactGuid=NomDebtor.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CliGral AS NomNBanc ON Xec.NBancGuid=NomNBanc.Guid ")
            sb.AppendLine("WHERE Xec.Guid='" & oXec.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                If Not oXec.IsLoaded Then
                    With oXec
                        .XecNum = oDrd("XecNum")
                        .Amt = SQLHelper.GetAmtFromDataReader(oDrd("Eur"))
                        .Vto = SQLHelper.GetFchFromDataReader(oDrd("Vto"))
                        .FchRecepcio = SQLHelper.GetFchFromDataReader(oDrd("FchRecepcio"))
                        .Lliurador = New DTOContact(oDrd("ContactGuid"))
                        .Lliurador.FullNom = SQLHelper.GetStringFromDataReader(oDrd("NomDebtor"))
                        If Not IsDBNull(oDrd("NBancGuid")) Then
                            .NBanc = New DTOBanc(oDrd("NbancGuid"))
                            .NBanc.FullNom = SQLHelper.GetStringFromDataReader(oDrd("NomNBanc"))
                        End If
                        If Not IsDBNull(oDrd("CcaRebut")) Then
                            .CcaRebut = New DTOCca(DirectCast(oDrd("CcaRebut"), Guid))
                        End If
                        If Not IsDBNull(oDrd("CcaPresentacio")) Then
                            .CcaPresentacio = New DTOCca(DirectCast(oDrd("CcaPresentacio"), Guid))
                        End If
                        If Not IsDBNull(oDrd("CcaVto")) Then
                            .CcaVto = New DTOCca(DirectCast(oDrd("CcaVto"), Guid))
                        End If
                        .CodPresentacio = oDrd("CodPresentacio")
                        .StatusCod = oDrd("StatusCod")

                        .Iban = New DTOIban
                        With .Iban
                            .Digits = SQLHelper.GetStringFromDataReader(oDrd("Iban"))
                            If Not IsDBNull(oDrd("Bn2Guid")) Then
                                .BankBranch = New DTOBankBranch(oDrd("Bn2Guid"))
                                With .BankBranch
                                    .Id = SQLHelper.GetStringFromDataReader(oDrd("Agc"))
                                    .Address = SQLHelper.GetStringFromDataReader(oDrd("Adr"))
                                    If Not IsDBNull(oDrd("Location")) Then
                                        .Location = New DTOLocation(oDrd("Location"))
                                        .Location.Nom = SQLHelper.GetStringFromDataReader(oDrd("LocationNom"))
                                    End If
                                    If Not IsDBNull(oDrd("Bn1Guid")) Then
                                        .Bank = New DTOBank(oDrd("Bn1Guid"))
                                        With .Bank
                                            .Id = SQLHelper.GetStringFromDataReader(oDrd("Bn1"))
                                            .NomComercial = SQLHelper.GetStringFromDataReader(oDrd("Abr"))
                                            .RaoSocial = SQLHelper.GetStringFromDataReader(oDrd("Bn1Nom"))
                                            .Swift = SQLHelper.GetStringFromDataReader(oDrd("Swift"))
                                        End With
                                    End If
                                End With
                            End If
                        End With

                        If Not IsDBNull(oDrd("CcaRebut")) Then
                            .CcaRebut = New DTOCca(oDrd("CcaRebut"))
                            With .CcaRebut
                                .Fch = SQLHelper.GetFchFromDataReader(oDrd("CcaRebutFch"))
                                .Concept = SQLHelper.GetStringFromDataReader(oDrd("CcaRebutTxt"))
                            End With
                        End If

                        If Not IsDBNull(oDrd("CcaPresentacio")) Then
                            .CcaRebut = New DTOCca(oDrd("CcaPresentacio"))
                            With .CcaRebut
                                .Fch = SQLHelper.GetFchFromDataReader(oDrd("CcaPresentacioFch"))
                                .Concept = SQLHelper.GetStringFromDataReader(oDrd("CcaPresentacioTxt"))
                            End With
                        End If

                        If Not IsDBNull(oDrd("CcaVto")) Then
                            .CcaRebut = New DTOCca(oDrd("CcaVto"))
                            With .CcaRebut
                                .Fch = SQLHelper.GetFchFromDataReader(oDrd("CcaVtoFch"))
                                .Concept = SQLHelper.GetStringFromDataReader(oDrd("CcaVtoTxt"))
                            End With
                        End If

                        .Pnds = New List(Of DTOPnd)
                        .IsLoaded = True
                    End With
                End If

                If Not IsDBNull(oDrd("PndGuid")) Then
                    Dim item As New DTOPnd(oDrd("PndGuid"))
                    With item
                        .Cod = IIf(SQLHelper.GetStringFromDataReader(oDrd("AD")) = "A", DTOPnd.Codis.Creditor, DTOPnd.Codis.Deutor)
                        .Amt = SQLHelper.GetAmtFromDataReader(oDrd("PndEur"))
                        .Fch = SQLHelper.GetFchFromDataReader(oDrd("PndFch"))
                        .Vto = SQLHelper.GetFchFromDataReader(oDrd("PndVto"))
                        .FraNum = SQLHelper.GetStringFromDataReader(oDrd("Fra"))
                        If Not IsDBNull(oDrd("CtaGuid")) Then
                            .Cta = New DTOPgcCta(oDrd("CtaGuid"))
                            With .Cta
                                .Id = oDrd("CtaId")
                                .Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "CtaEsp", "CtaCat")
                            End With
                        End If
                    End With
                    oXec.Pnds.Add(item)
                End If
            Loop

            oDrd.Close()
        End If

        Dim retval As Boolean = oXec.IsLoaded
        Return retval
    End Function

    Shared Function Update(oXec As DTOXec, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oXec, oTrans)
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

    Shared Sub Update(oXec As DTOXec, ByRef oTrans As SqlTransaction)
        UpdateHeader(oXec, oTrans)
        UpdateItems(oXec, oTrans)
    End Sub

    Shared Sub UpdateHeader(oXec As DTOXec, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Xec ")
        sb.AppendLine("WHERE Guid='" & oXec.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oXec.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oXec
            If .Iban IsNot Nothing Then
                oRow("Iban") = .Iban.Digits
                oRow("SBankBranch") = SQLHelper.NullableBaseGuid(.Iban.BankBranch)
            End If
            oRow("XecNum") = .XecNum
            oRow("pts") = .Amt.Val
            oRow("Cur") = .Amt.Cur.Tag
            oRow("Eur") = .Amt.Eur
            oRow("Vto") = .Vto.Date
            oRow("FchRecepcio") = SQLHelper.NullableFch(.FchRecepcio)
            oRow("ContactGuid") = SQLHelper.NullableBaseGuid(.Lliurador)
            oRow("NbancGuid") = SQLHelper.NullableBaseGuid(.NBanc)
            oRow("CodPresentacio") = .CodPresentacio
            oRow("StatusCod") = .StatusCod
            oRow("CcaRebut") = SQLHelper.NullableBaseGuid(.CcaRebut)
            oRow("CcaPresentacio") = SQLHelper.NullableBaseGuid(.CcaPresentacio)
            oRow("CcaVto") = SQLHelper.NullableBaseGuid(.CcaVto)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateItems(oXec As DTOXec, ByRef oTrans As SqlTransaction)
        If Not oXec.IsNew Then DeleteItems(oXec, oTrans)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM XecDetail ")
        sb.AppendLine("WHERE Xec='" & oXec.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each item As DTOPnd In oXec.Pnds
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Xec") = oXec.Guid
            oRow("PndGuid") = item.Guid
        Next

        oDA.Update(oDs)
    End Sub

    Shared Function UpdateXecRebut(oXec As DTOXec, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            CcaLoader.Update(oXec.CcaRebut, oTrans)
            XecLoader.Update(oXec, oTrans)
            PndsLoader.SetStatus(oXec.Pnds, DTOPnd.StatusCod.saldat, oXec.CcaRebut.Guid, oTrans)
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

    Shared Function Delete(oXec As DTOXec, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oXec, oTrans)
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


    Shared Sub Delete(oXec As DTOXec, ByRef oTrans As SqlTransaction)
        For Each oPnd As DTOPnd In oXec.Pnds
            oPnd.Status = DTOPnd.StatusCod.pendent
            PndLoader.SetStatus(oPnd, oTrans)
        Next

        DeleteItems(oXec, oTrans)
        DeleteHeader(oXec, oTrans)
        If oXec.CcaRebut IsNot Nothing Then
            CcaLoader.Delete(oXec.CcaRebut, oTrans)
        End If
    End Sub

    Shared Sub DeleteHeader(oXec As DTOXec, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Xec WHERE Guid='" & oXec.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteItems(oXec As DTOXec, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE XecDetail WHERE Xec='" & oXec.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class XecsLoader

    Shared Function All(oCca As DTOCca) As List(Of DTOXec)
        Dim retval As New List(Of DTOXec)
        Dim SQL As String = "SELECT * FROM Xec WHERE (CcaRebut='" & oCca.Guid.ToString & "' OR CcaPresentacio='" & oCca.Guid.ToString & "' OR CcaVto='" & oCca.Guid.ToString & "') "
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oXec As New DTOXec(DirectCast(oDrd("Guid"), Guid))
            With oXec
                .Vto = oDrd("vto")
                .Amt = DTOAmt.Factory(CDec(oDrd("eur")), oDrd("Cur").ToString, CDec(oDrd("Pts")))
                .XecNum = oDrd("XecNum")
                .Iban = New DTOIban
                .Iban.Digits = oDrd("Iban")
                .Lliurador = New DTOContact(DirectCast(oDrd("ContactGuid"), Guid))
            End With
            retval.Add(oXec)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Headers(Optional oLliurador As DTOContact = Nothing) As List(Of DTOXec)
        Dim retval As New List(Of DTOXec)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Xec.Guid, Xec.XecNum, Xec.Eur ")
        If oLliurador IsNot Nothing Then
            sb.AppendLine(", Xec.ContactGuid, NomDebtor.FullNom AS NomDebtor ")
        End If
        sb.AppendLine(", Xec.CcaRebut ")
        sb.AppendLine(", CcaRebut.Fch ")
        sb.AppendLine(", Xec.Iban, Bn1.Guid AS Bn1Guid, Bn1.Abr, Bn2.Guid as Bn2Guid ")
        sb.AppendLine("FROM Xec ")
        sb.AppendLine("LEFT OUTER JOIN Country ON Country.ISO = SUBSTRING(Xec.Iban,1,2) ")
        sb.AppendLine("LEFT OUTER JOIN Bn2 ON Xec.SBankBranch=Bn2.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Bn1 ON Bn2.Bank = Bn1.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Cca AS CcaRebut ON Xec.CcaRebut=CcaRebut.Guid ")

        If oLliurador IsNot Nothing Then
            sb.AppendLine("INNER JOIN CliGral AS NomDebtor ON Xec.ContactGuid=NomDebtor.Guid ")
            sb.AppendLine("WHERE Xec.ContactGuid='" & oLliurador.Guid.ToString & "' ")
        End If
        sb.AppendLine("ORDER BY CcaRebut.Fch DESC ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOXec(oDrd("Guid"))
            With item
                .XecNum = oDrd("XecNum")
                .Amt = SQLHelper.GetAmtFromDataReader(oDrd("Eur"))
                If oLliurador Is Nothing Then
                    .Lliurador = New DTOContact(oDrd("ContactGuid"))
                    .Lliurador.FullNom = SQLHelper.GetStringFromDataReader(oDrd("NomDebtor"))
                Else
                    .Lliurador = oLliurador
                End If
                If Not IsDBNull(oDrd("CcaRebut")) Then
                    .CcaRebut = New DTOCca(DirectCast(oDrd("CcaRebut"), Guid))
                    .CcaRebut.Fch = SQLHelper.GetFchFromDataReader(oDrd("Fch"))
                    .FchRecepcio = .CcaRebut.Fch
                End If

                .Iban = New DTOIban
                With .Iban
                    .Digits = SQLHelper.GetStringFromDataReader(oDrd("Iban"))
                    If Not IsDBNull(oDrd("Bn2Guid")) Then
                        .BankBranch = New DTOBankBranch(oDrd("Bn2Guid"))
                        With .BankBranch
                            If Not IsDBNull(oDrd("Bn1Guid")) Then
                                .Bank = New DTOBank(oDrd("Bn1Guid"))
                                With .Bank
                                    .NomComercial = SQLHelper.GetStringFromDataReader(oDrd("Abr"))
                                End With
                            End If
                        End With
                    End If
                End With

            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Public Shared Function All(oEmp As DTOEmp, oStatusCod As DTOXec.StatusCods, Optional oCodPresentacio As DTOXec.ModalitatsPresentacio = DTOXec.ModalitatsPresentacio.NotSet) As List(Of DTOXec)
        Dim retval As New List(Of DTOXec)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Xec.Guid, Xec.XecNum, Xec.Vto, Xec.Eur, Xec.FchRecepcio ")
        sb.AppendLine(", Xec.ContactGuid, NomDebtor.FullNom AS NomDebtor, Xec.NBancGuid, NomNBanc.FullNom AS NomNBanc ")
        sb.AppendLine(", Xec.CcaRebut, Xec.CcaPresentacio, Xec.CcaVto ")
        sb.AppendLine(", Xec.CodPresentacio, Xec.StatusCod ")
        sb.AppendLine(", Xec.Iban, Bn1.Guid AS Bn1Guid, Bn1.Bn1, Bn1.Nom AS Bn1Nom, Bn1.Abr, Bn1.Swift, Bn2.Guid as Bn2Guid, Bn2.Agc, Bn2.Adr, Bn2.Location  ")
        'sb.AppendLine(", Location.Nom AS LocationNom ")
        sb.AppendLine(", XecDetail.PndGuid ")
        sb.AppendLine("FROM Xec ")
        sb.AppendLine("LEFT OUTER JOIN XecDetail ON Xec.Guid=XecDetail.Xec ")
        sb.AppendLine("LEFT OUTER JOIN Pnd ON XecDetail.PndGuid=Pnd.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Country ON Country.ISO = SUBSTRING(Xec.Iban,1,2) ")
        sb.AppendLine("LEFT OUTER JOIN Bn2 ON Xec.SBankBranch=Bn2.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Bn1 ON Bn2.Bank = Bn1.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON Xec.ContactGuid=CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral AS NomDebtor ON Xec.ContactGuid=NomDebtor.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral AS NomNBanc ON Xec.NBancGuid=NomNBanc.Guid ")
        sb.AppendLine("WHERE CliGral.Emp=" & oEmp.Id & " AND Xec.StatusCod=" & CInt(oStatusCod) & " ")


        If oCodPresentacio <> DTOXec.ModalitatsPresentacio.NotSet Then
            sb.AppendLine("AND CodPresentacio=" & CInt(oCodPresentacio) & " ")
        End If
        sb.AppendLine("ORDER BY (CASE WHEN Xec.vto IS NULL THEN 0 ELSE 1 END), Xec.vto, Xec.FchRecepcio, Xec.Guid")

        Dim SQL As String = sb.ToString
        Dim oLastXec As New DTOXec()
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oGuid As Guid = oDrd("Guid")
            'If oGuid.Equals(New Guid("7198131F-57E6-4608-A08B-17DC60A3B43B")) Then Stop
            If Not oGuid.Equals(oLastXec.Guid) Then
                oLastXec = New DTOXec(oGuid)
                With oLastXec
                    If Not IsDBNull(oDrd("NBancGuid")) Then
                        .NBanc = New DTOBanc(DirectCast(oDrd("NBancGuid"), Guid))
                    End If
                    .Vto = oDrd("vto")
                    .Amt = SQLHelper.GetAmtFromDataReader(CDec(oDrd("eur")))
                    .XecNum = oDrd("XecNum")

                    .Iban = New DTOIban
                    With .Iban
                        .Digits = SQLHelper.GetStringFromDataReader(oDrd("Iban"))
                        If Not IsDBNull(oDrd("Bn2Guid")) Then
                            .BankBranch = New DTOBankBranch(oDrd("Bn2Guid"))
                            With .BankBranch
                                .Id = SQLHelper.GetStringFromDataReader(oDrd("Agc"))
                                .Address = SQLHelper.GetStringFromDataReader(oDrd("Adr"))
                                If Not IsDBNull(oDrd("Location")) Then
                                    .Location = New DTOLocation(oDrd("Location"))
                                    ' .Location.Nom = SQLHelper.GetStringFromDataReader(oDrd("LocationNom"))
                                End If
                                If Not IsDBNull(oDrd("Bn1Guid")) Then
                                    .Bank = New DTOBank(oDrd("Bn1Guid"))
                                    With .Bank
                                        .Id = SQLHelper.GetStringFromDataReader(oDrd("Bn1"))
                                        .NomComercial = SQLHelper.GetStringFromDataReader(oDrd("Abr"))
                                        .RaoSocial = SQLHelper.GetStringFromDataReader(oDrd("Bn1Nom"))
                                        .Swift = SQLHelper.GetStringFromDataReader(oDrd("Swift"))
                                    End With
                                End If
                            End With
                        End If
                    End With


                    .Lliurador = New DTOContact(DirectCast(oDrd("ContactGuid"), Guid))
                    .Lliurador.FullNom = SQLHelper.GetStringFromDataReader(oDrd("NomDebtor"))
                    .CodPresentacio = oDrd("CodPresentacio")
                    .StatusCod = oDrd("StatusCod")
                    .Pnds = New List(Of DTOPnd)
                End With
                retval.Add(oLastXec)
            End If

            If Not IsDBNull(oDrd("PndGuid")) Then
                Dim oPnd As New DTOPnd(oDrd("PndGuid"))
                oLastXec.Pnds.Add(oPnd)
            End If

        Loop
        oDrd.Close()
        Return retval
    End Function

End Class

