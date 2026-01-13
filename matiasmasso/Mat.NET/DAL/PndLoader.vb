Public Class PndLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOPnd
        Dim retval As DTOPnd = Nothing
        Dim oPnd As New DTOPnd(oGuid)
        If Load(oPnd) Then
            retval = oPnd
        End If
        Return retval
    End Function

    Shared Function FromFra(oContact As DTOContact, sFra As String, DcImport As Decimal) As DTOPnd
        Dim retval As DTOPnd = Nothing
        Dim sImport As String = DcImport.ToString.Replace(",", ".")
        Dim SQL As String = "SELECT Guid FROM Pnd " _
                            & "WHERE ContactGuid='" & oContact.Guid.ToString & "' AND " _
                            & "Fra ='" & sFra & "' AND " _
                            & "Eur=" & sImport & " AND " _
                            & "AD LIKE 'D' " _
                            & "AND Status<1"

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOPnd(oDrd("Guid"))
        End If
        oDrd.Close()
        Return retval
    End Function


    Shared Function Load(ByRef oPnd As DTOPnd) As Boolean
        If Not oPnd.IsLoaded And Not oPnd.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Pnd.* ")
            sb.AppendLine(", PgcCta.Id AS CtaId, PgcCta.Esp AS CtaEsp ")
            sb.AppendLine(", CliGral.FullNom, Csb.Doc AS CsbDoc, Csb.CsaGuid ")
            sb.AppendLine(", Cca.Cca, Cca.Fch AS CcaFch, Cca.Txt AS CcaConcept ")
            sb.AppendLine(", CcaVto.Cca AS CcaVtoCca, CcaVto.Fch AS CcaVtoFch, CcaVto.Txt AS CcaVtoConcept ")
            sb.AppendLine("FROM Pnd ")
            sb.AppendLine("LEFT OUTER JOIN PgcCta ON Pnd.CtaGuid = PgcCta.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CliGral ON Pnd.ContactGuid = CliGral.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Cca ON Pnd.CcaGuid = Cca.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Csb ON Pnd.CsbGuid = Csb.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Cca AS CcaVto ON Pnd.StatusGuid = CcaVto.Guid ")
            sb.AppendLine("WHERE Pnd.Guid='" & oPnd.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oPnd
                    .Emp = New DTOEmp(oDrd("Emp"))
                    .Contact = New DTOContact(DirectCast(oDrd("ContactGuid"), Guid))
                    .Contact.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                    .Amt = DTOAmt.Factory(oDrd("EUR"), oDrd("Div").ToString, oDrd("PTS"))
                    If IsDBNull(oDrd("VTO")) Then
                        .Vto = Date.MinValue
                    Else
                        .Vto = oDrd("VTO")
                    End If
                    .Cod = IIf(oDrd("AD") = "D", DTOPnd.Codis.Deutor, DTOPnd.Codis.Creditor)
                    .Yef = oDrd("YEF")
                    .FraNum = oDrd("FRA")
                    If Not IsDBNull(oDrd("FCH")) Then
                        .Fch = oDrd("fch")
                    End If
                    If Not IsDBNull(oDrd("CtaGuid")) Then
                        .Cta = New DTOPgcCta(DirectCast(oDrd("CtaGuid"), Guid))
                        .Cta.Id = oDrd("CtaId")
                        .Cta.Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "CtaEsp")
                    End If
                    .Cfp = oDrd("CFP")
                    .Fpg = oDrd("FPG")
                    If Not IsDBNull(oDrd("CcaGuid")) Then
                        .Cca = New DTOCca(DirectCast(oDrd("CcaGuid"), Guid))
                        .Cca.Id = SQLHelper.GetIntegerFromDataReader(oDrd("Cca"))
                        .Cca.Fch = SQLHelper.GetFchFromDataReader(oDrd("CcaFch"))
                        .Cca.Concept = SQLHelper.GetStringFromDataReader(oDrd("CcaConcept"))
                    End If
                    If Not IsDBNull(oDrd("StatusGuid")) Then
                        .CcaVto = New DTOCca(DirectCast(oDrd("StatusGuid"), Guid))
                        .CcaVto.Id = SQLHelper.GetIntegerFromDataReader(oDrd("CcaVtoCca"))
                        .CcaVto.Fch = SQLHelper.GetFchFromDataReader(oDrd("CcaVtoFch"))
                        .CcaVto.Concept = SQLHelper.GetStringFromDataReader(oDrd("CcaVtoConcept"))
                    End If

                    If Not IsDBNull(oDrd("CsbGuid")) Then
                        .Csb = New DTOCsb(DirectCast(oDrd("CsbGuid"), Guid))
                        .Csb.Id = oDrd("CsbDoc")
                        If Not IsDBNull(oDrd("CsaGuid")) Then
                            .Csb.Csa = New DTOCsa(oDrd("CsaGuid"))
                        End If
                    End If
                    .Status = oDrd("Status")
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oPnd.IsLoaded
        Return retval
    End Function

    Shared Function Update(oPnd As DTOPnd, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oPnd, oTrans)
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


    Shared Sub Update(oPnd As DTOPnd, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Pnd ")
        sb.AppendLine("WHERE Guid='" & oPnd.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oPnd.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oPnd
            oRow("ContactGuid") = .Contact.Guid
            oRow("EMP") = .Emp.Id
            'oRow("CLI") = .Contact.Id
            oRow("PTS") = .Amt.Val
            oRow("EUR") = .Amt.Eur
            oRow("DIV") = .Amt.Cur.Tag
            oRow("VTO") = .Vto.Date
            oRow("YEF") = .Yef
            If .FraNum.Length > 15 Then
                oRow("FRA") = ""
            Else
                oRow("FRA") = .FraNum
            End If
            oRow("FraGuid") = SQLHelper.NullableBaseGuid(.Invoice)
            oRow("FCH") = .Fch
            oRow("CtaGuid") = .Cta.Guid
            oRow("CFP") = CInt(.Cfp)
            oRow("FPG") = .Fpg

            If .Cca Is Nothing Then
                oRow("CcaGuid") = System.DBNull.Value
            Else
                oRow("CcaGuid") = .Cca.Guid
            End If
            oRow("StatusGuid") = SQLHelper.NullableBaseGuid(.CcaVto)
            oRow("Status") = .Status
            oRow("AD") = IIf(.Cod = DTOPnd.Codis.Deutor, "D", "A")

            If .Csb Is Nothing Then
                oRow("CsbGuid") = System.DBNull.Value
            Else
                If .Csb.Csa Is Nothing Then
                    oRow("CsbGuid") = System.DBNull.Value
                Else
                    oRow("CsbGuid") = .Csb.Guid
                End If
            End If
        End With

        oDA.Update(oDs)
    End Sub

    Shared Sub Salda(oPnd As DTOPnd, oCca As DTOCca, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("UPDATE Pnd ")
        sb.AppendLine("SET Status=" & CInt(DTOPnd.StatusCod.saldat) & " ")
        sb.AppendLine(", StatusGuid='" & oCca.Guid.ToString & "' ")
        sb.AppendLine("WHERE Guid='" & oPnd.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub



    Shared Function Delete(oPnd As DTOPnd, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oPnd, oTrans)
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


    Shared Sub Delete(oPnd As DTOPnd, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Pnd WHERE Guid='" & oPnd.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Function SetStatus(oPnd As DTOPnd, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            SetStatus(oPnd, oTrans)
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

    Shared Sub SetStatus(oPnd As DTOPnd, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "UPDATE Pnd SET Status=" & CInt(oPnd.Status) & " WHERE Pnd.Guid='" & oPnd.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub SetStatus(oPnds As List(Of DTOPnd), oStatus As DTOPnd.StatusCod, ByRef oTrans As SqlTransaction)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("UPDATE Pnd SET Status=" & CInt(oStatus) & " WHERE (")
        For Each oPnd In oPnds
            If oPnds.IndexOf(oPnd) > 0 Then sb.Append(" OR ")
            sb.AppendLine(" Pnd.Guid = '" & oPnd.Guid.ToString & "' ")
        Next
        sb.AppendLine(")")
        Dim SQL = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region


End Class

Public Class PndsLoader

    Shared Function All(oEmp As DTOEmp, contact As DTOContact,
                        Optional fraNum As String = "",
                        Optional fch As Date = Nothing,
                        Optional cod As DTOPnd.Codis = DTOPnd.Codis.NotSet,
                        Optional onlyPendents As Boolean = True,
                        Optional eur As Decimal = 0) As List(Of DTOPnd)
        Dim retval As New List(Of DTOPnd)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Pnd.Guid as PndGuid, Pnd.Emp, Pnd.Vto, Pnd.Status, Pnd.CtaGuid, Pnd.CcaGuid ")
        sb.AppendLine(", Pnd.CsbGuid, Csb.CsaGuid, Csa.Csb AS CsaId, Csb.Doc ")
        sb.AppendLine(", Pnd.Eur, Pnd.Div, Pnd.Pts, Pnd.Fra, Pnd.Fch, Pnd.Fpg, Pnd.Cfp, Pnd.AD ")
        sb.AppendLine(", PgcCta.Id as CtaId, PgcCta.Cod AS CtaCod, PgcCta.Esp as CtaEsp, PgcCta.Cat as CtaCat, PgcCta.Eng as CtaEng, PgcCta.Act ")
        sb.AppendLine(", Cca.Cca, Cca.Fch AS CcaFch, Cca.txt AS CcaTxt ")
        sb.AppendLine(", Pnd.ContactGuid, CliGral.RaoSocial ")
        If contact Is Nothing Then
            sb.AppendLine(", CliGral.FullNom ")
        End If
        sb.AppendLine("FROM Pnd ")
        sb.AppendLine("INNER JOIN CliGral ON Pnd.ContactGuid = CliGral.Guid ")
        If contact IsNot Nothing Then
            sb.AppendLine("AND Pnd.ContactGuid = '" & contact.Guid.ToString & "' ")
        End If
        sb.AppendLine("LEFT OUTER JOIN PgcCta ON Pnd.CtaGuid = PgcCta.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Cca ON Pnd.CcaGuid = Cca.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Csb ON Pnd.CsbGuid = Csb.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Csa ON Csb.CsaGuid = Csa.Guid ")
        sb.AppendLine("WHERE Pnd.Emp = " & oEmp.Id & "  ")
        If onlyPendents Then
            'sb.AppendLine("AND Pnd.StatusGuid IS NULL ")
            sb.AppendLine("AND Pnd.Status < " & CInt(DTOPnd.StatusCod.saldat) & " ")
        End If
        If fraNum > "" Then
            sb.AppendLine("AND Pnd.Fra = '" & fraNum & "' ")
        End If
        If fch <> Nothing Then
            sb.AppendLine("AND Pnd.Fch = '" & Format(fch, "yyyyMMdd") & "' ")
        End If
        If eur <> 0 Then
            sb.AppendLine("AND Pnd.Eur = " & eur.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) & " ")
        End If
        If cod <> DTOPnd.Codis.NotSet Then
            sb.AppendLine("AND Pnd.AD = '" & IIf(cod = DTOPnd.Codis.Deutor, "D", "A") & "' ")
        End If
        sb.AppendLine("ORDER BY Pnd.Vto, CliGral.RaoSocial, Pnd.ContactGuid, Pnd.Fch, Pnd.Fra ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read

            Dim oCta As DTOPgcCta = Nothing
            If Not IsDBNull(oDrd("CtaGuid")) Then
                oCta = New DTOPgcCta(oDrd("CtaGuid"))
                With oCta
                    .id = oDrd("CtaId")
                    .codi = oDrd("CtaCod")
                    .act = oDrd("Act")
                    .nom = SQLHelper.GetLangTextFromDataReader(oDrd, "CtaEsp", "CtaCat", "CtaEng")
                End With
            End If

            Dim oCca As DTOCca = Nothing
            If Not IsDBNull(oDrd("Cca")) Then
                oCca = New DTOCca(oDrd("CcaGuid"))
                With oCca
                    .id = oDrd("Cca")
                    .fch = oDrd("CcaFch")
                    .concept = oDrd("CcaTxt")
                End With
            End If

            Dim oCsb As DTOCsb = Nothing
            If Not IsDBNull(oDrd("CsbGuid")) Then
                oCsb = New DTOCsb(oDrd("CsbGuid"))
                oCsb.Id = SQLHelper.GetIntegerFromDataReader(oDrd("Doc"))
                If Not IsDBNull(oDrd("CsaGuid")) Then
                    oCsb.Csa = New DTOCsa(oDrd("CsaGuid"))
                    oCsb.Csa.Id = oDrd("CsaId")
                End If
            End If

            'If oDrd("Fra") = 5662 Then Stop

            Dim item As New DTOPnd(oDrd("PndGuid"))
            With item
                .Emp = New DTOEmp(oDrd("Emp"))
                .Vto = SQLHelper.GetFchFromDataReader(oDrd("Vto"))
                .Status = oDrd("Status")
                .Cta = oCta
                .Cca = oCca
                .Csb = oCsb
                If contact Is Nothing Then
                    .Contact = New DTOContact(oDrd("ContactGuid"))
                    .Contact.nom = SQLHelper.GetStringFromDataReader(oDrd("RaoSocial"))
                    .Contact.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                Else
                    .Contact = contact
                End If
                .Amt = DTOAmt.Factory(CDec(oDrd("Eur")), oDrd("Div").ToString, CDec(oDrd("Pts")))
                .FraNum = oDrd("Fra")
                .Fch = SQLHelper.GetFchFromDataReader(oDrd("Fch"))
                .Fpg = oDrd("Fpg")
                .Cfp = oDrd("Cfp")
                .Cod = IIf(oDrd("AD") = "A", DTOPnd.Codis.Creditor, DTOPnd.Codis.Deutor)
            End With

            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Pending(oEmp As DTOEmp, Optional Cod As DTOPnd.Codis = DTOPnd.Codis.NotSet, Optional IncludeDescomptats As Boolean = False) As List(Of DTOPnd)
        Dim retval As New List(Of DTOPnd)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Pnd.Guid, Pnd.ContactGuid, CliGral.Guid, CliGral.FullNom, Pnd.CtaGuid, Pnd.Vto, Pnd.Eur, Pnd.Cfp, Pnd.Ad ")
        sb.AppendLine("From Pnd ")
        sb.AppendLine("INNER JOIN CliGral ON Pnd.ContactGuid = CliGral.Guid ")
        sb.AppendLine("WHERE Pnd.Emp = " & oEmp.Id & " ")
        If IncludeDescomptats Then
            sb.Append("AND Status< " & DTOPnd.StatusCod.saldat & " ")
        Else
            sb.Append("AND Status= " & DTOPnd.StatusCod.pendent & " ")
        End If
        If Cod <> DTOPnd.Codis.NotSet Then
            sb.Append("AND Pnd.Ad= '" & IIf(Cod = DTOPnd.Codis.Deutor, "D", "A") & "' ")
        End If
        sb.AppendLine("ORDER BY Pnd.Cfp, CliGral.FullNom, Pnd.Vto ")

        Dim SQL As String = sb.ToString
        Dim oContact As New DTOContact
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oContact.Guid.Equals(oDrd("ContactGuid")) Then
                oContact = New DTOContact(oDrd("ContactGuid"))
                oContact.FullNom = oDrd("FullNom")
            End If
            Dim item As New DTOPnd(oDrd("Guid"))
            With item
                .Contact = oContact
                .Vto = oDrd("Vto")
                .Amt = DTOAmt.Factory(CDec(oDrd("Eur")))
                .Cfp = oDrd("Cfp")
                .Cod = IIf(oDrd("AD") = "A", DTOPnd.Codis.Creditor, DTOPnd.Codis.Deutor)
                If Not IsDBNull(oDrd("CtaGuid")) Then
                    .Cta = New DTOPgcCta(oDrd("CtaGuid"))
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function BankTransferReminderDeutors(oEmp As DTOEmp, Vto As Date) As List(Of DTOCustomer)
        Dim retval As New List(Of DTOCustomer)
        Dim oSsc = DTOSubscription.Wellknown(DTOSubscription.Wellknowns.BankTransferReminder)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Pnd.ContactGuid, CliGral.FullNom, CliGral.LangId ")
        sb.AppendLine("From Pnd ")
        sb.AppendLine("INNER JOIN CliGral ON Pnd.ContactGuid = CliGral.Guid ")
        sb.AppendLine("INNER JOIN Email_Clis ON Pnd.ContactGuid = Email_Clis.ContactGuid ")
        sb.AppendLine("INNER JOIN SscEmail ON Email_Clis.EmailGuid = SscEmail.Email ")
        sb.AppendLine("WHERE Pnd.Emp = " & oEmp.Id & " ")
        sb.AppendLine("AND Pnd.Vto = '" & Format(Vto, "yyyyMMdd") & "' ")
        sb.AppendLine("AND Pnd.Ad= 'D' ")
        sb.AppendLine("AND Pnd.CFP= " & DTOPaymentTerms.CodsFormaDePago.transferencia & " ")
        sb.AppendLine("AND SscEmail.SscGuid = '" & oSsc.Guid.ToString & "' ")
        sb.AppendLine("GROUP BY Pnd.ContactGuid, CliGral.FullNom, CliGral.LangId ")
        sb.AppendLine("ORDER BY CliGral.FullNom ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oCustomer As New DTOCustomer(oDrd("ContactGuid"))
            oCustomer.FullNom = oDrd("FullNom")
            oCustomer.Lang = SQLHelper.GetLangFromDataReader(oDrd("LangId"))
            retval.Add(oCustomer)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Sub SaldaBack(oCca As DTOCca, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("UPDATE Pnd ")
        sb.AppendLine("SET Status=" & CInt(DTOPnd.StatusCod.pendent) & " ")
        sb.AppendLine(", StatusGuid=NULL ")
        sb.AppendLine("WHERE StatusGuid='" & oCca.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub Delete(oCca As DTOCca, oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Pnd WHERE CcaGuid = '" & oCca.Guid.ToString & "'"
        Dim rc = SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Function Cartera(oEmp As DTOEmp, DtFch As Date) As List(Of DTOPnd)
        Dim retval As New List(Of DTOPnd)
        Dim sFch As String = Format(DtFch, "yyyyMMdd")
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Pnd.Guid, Pnd.ContactGuid, Pnd.CtaGuid ")
        sb.AppendLine(", Pnd.Fch, Pnd.Fra, Pnd.Eur, Pnd.Div, Pnd.Pts, Pnd.Ad, Pnd.Vto ")
        sb.AppendLine(", CliGral.RaoSocial ")
        sb.AppendLine(", PgcCta.Id AS CtaId, PgcCta.Cod AS CtaCod, PgcCta.Esp AS CtaNom ")
        sb.AppendLine("FROM Pnd ")
        sb.AppendLine("INNER JOIN CliGral ON Pnd.ContactGuid = CliGral.Guid ")
        sb.AppendLine("INNER JOIN PgcCta ON Pnd.CtaGuid = PgcCta.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Cca ON Pnd.StatusGuid = Cca.Guid ")
        sb.AppendLine("WHERE Pnd.Emp = " & oEmp.Id & " ")
        sb.AppendLine("AND Pnd.Fch <= '" & sFch & "' ")
        sb.AppendLine("AND (Cca.Fch > '20221231' OR(Cca.Fch IS NULL AND Pnd.Vto > '" & sFch & "')) ")
        sb.AppendLine("ORDER BY PgcCta.Id, Pnd.Fra ")
        Dim SQL As String = sb.ToString
        Dim oContact As New DTOContact
        Dim oCta As New DTOPgcCta
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oCta.Guid.Equals(oDrd("CtaGuid")) Then
                oCta = New DTOPgcCta(oDrd("CtaGuid"))
                oCta.Id = oDrd("CtaId")
                oCta.Codi = oDrd("CtaCod")
                oCta.Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "CtaNom")
            End If
            If Not oContact.Guid.Equals(oDrd("ContactGuid")) Then
                oContact = New DTOContact(oDrd("ContactGuid"))
                oContact.Nom = oDrd("RaoSocial")
                'oContact.Id = oDrd("Cli")
            End If
            Dim item As New DTOPnd(oDrd("Guid"))
            With item
                .Emp = oEmp
                .Contact = oContact
                .Cta = oCta
                .Fch = oDrd("Fch")
                .FraNum = oDrd("Fra")
                .Vto = oDrd("Vto")
                .Amt = DTOAmt.Factory(CDec(oDrd("Eur")), oDrd("Div"), CDec(oDrd("Pts")))
                .Cod = IIf(oDrd("AD") = "D", DTOPnd.Codis.Deutor, DTOPnd.Codis.Creditor)
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Sub SetStatus(oPnds As List(Of DTOPnd), oStatus As DTOPnd.StatusCod, oStatusGuid As Guid, ByRef oTrans As SqlTransaction)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("UPDATE Pnd SET Status=" & CInt(oStatus) & " ")
        If oStatusGuid = Nothing Then
            sb.AppendLine(", Pnd.StatusGuid = NULL ")
        Else
            sb.AppendLine(", Pnd.StatusGuid ='" & oStatusGuid.ToString & "' ")
        End If
        sb.AppendLine(" WHERE (")
        For Each oPnd In oPnds
            If oPnds.IndexOf(oPnd) > 0 Then sb.Append(" OR ")
            sb.AppendLine(" Pnd.Guid = '" & oPnd.Guid.ToString & "' ")
        Next
        sb.AppendLine(")")
        Dim SQL = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

End Class
