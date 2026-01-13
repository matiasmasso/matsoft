Public Class CcaLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOCca
        Dim retval As DTOCca = Nothing
        Dim oCca As New DTOCca(oGuid)
        If Load(oCca) Then
            retval = oCca
        End If
        Return retval
    End Function

    Shared Function FromNum(emp As DTOEmp, yea As Integer, num As Integer) As DTOCca
        Dim retval As DTOCca = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Cca.Guid, Cca.Cca, Cca.Txt, Cca.Fch ")
        sb.AppendLine("FROM Cca ")
        sb.AppendLine("WHERE Cca.Emp = " & emp.Id & " ")
        sb.AppendLine("AND Year(Cca.Fch) = " & yea & " ")
        sb.AppendLine("AND Cca.Cca = " & num & " ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOCca(oDrd("Guid"))
            With retval
                .Id = SQLHelper.GetIntegerFromDataReader(oDrd("Cca"))
                .Concept = SQLHelper.GetStringFromDataReader(oDrd("Txt"))
                .Fch = SQLHelper.GetFchFromDataReader(oDrd("Fch"))
            End With
        End If

        oDrd.Close()
        Return retval
    End Function

    Shared Function Load(ByRef oCca As DTOCca) As Boolean
        If Not oCca.IsLoaded And Not oCca.IsNew Then
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Cca.Emp, Cca.Txt, Cca.Fch, Cca.Cca, Cca.Ccd, Cca.Cdn, Cca.Ref, Cca.Hash ")
            sb.AppendLine(", Cca.FchCreated, Cca.UsrCreatedGuid AS UsrCreated, CcaUsrCreated.adr as UsrCreatedEmailAddress, CcaUsrCreated.Nickname as UsrCreatedNickName ")
            sb.AppendLine(", Cca.FchLastEdited, Cca.UsrLastEditedGuid AS UsrLastEdited, CcaUsrLastEdited.adr AS UsrLastEditedEmailAddress, CcaUsrLastEdited.nickname as UsrLastEditedNickname")
            sb.AppendLine(", Cca.Projecte, Projecte.Nom AS ProjecteNom ")
            sb.AppendLine(", Ccb.CcaGuid, Ccb.CtaGuid ")
            sb.AppendLine(", PgcCta.Act, PgcCta.Id, PgcCta.Esp, PgcCta.Cat, PgcCta.Eng ")
            sb.AppendLine(", PgcCta.Cod AS CtaCodi, PgcCta.IsBaseImponibleIVA, PgcCta.IsQuotaIVA ")
            sb.AppendLine(", Ccb.ContactGuid, CliGral.Cli, CliGral.FullNom, Ccb.Cur, Ccb.Eur, Ccb.Pts, Ccb.Dh ")
            sb.AppendLine(", Pnd.Guid as PndGuid, Pnd.CcaGuid AS PndCca, Pnd.Fra AS FraNum, PndCca.Hash AS PndCcaHash ")
            sb.AppendLine(", VwDocfile.* ")
            sb.AppendLine("FROM Cca ")
            sb.AppendLine("INNER JOIN Ccb ON Ccb.CcaGuid = Cca.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CliGral ON Ccb.ContactGuid = CliGral.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Projecte ON Cca.Projecte= Projecte.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Pnd ON Ccb.PndGuid = Pnd.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Cca AS PndCca ON Pnd.CcaGuid = PndCca.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Email AS CcaUsrCreated ON Cca.UsrCreatedGuid = CcaUsrCreated.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Email AS CcaUsrLastEdited ON Cca.UsrLastEditedGuid = CcaUsrLastEdited.Guid ")
            sb.AppendLine("INNER JOIN PgcCta ON Ccb.CtaGuid = PgcCta.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwDocfile ON Cca.Hash = VwDocfile.DocfileHash ")
            sb.AppendLine("WHERE Cca.Guid = '" & oCca.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                If Not oCca.IsLoaded Then
                    With oCca
                        .Id = oDrd("Cca")
                        .Fch = oDrd("Fch")
                        .Exercici = New DTOExercici(New DTOEmp(oDrd("Emp")), .Fch.Year)
                        .Concept = oDrd("Txt")
                        .Ccd = oDrd("Ccd")
                        .Cdn = SQLHelper.GetIntegerFromDataReader(oDrd("Cdn"))
                        .Ref = SQLHelper.GetBaseGuidFromDataReader(oDrd("Ref"))
                        If Not IsDBNull(oDrd("Projecte")) Then
                            .Projecte = New DTOProjecte(oDrd("Projecte"))
                            .Projecte.Nom = SQLHelper.GetStringFromDataReader(oDrd("ProjecteNom"))
                        End If
                        .docFile = SQLHelper.GetDocFileFromDataReader(oDrd)
                        '.DocFile = SQLHelper.GetDocFileFromDataReaderHash(oDrd("Hash"))
                        .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd)
                        .Items = New List(Of DTOCcb)
                        .IsLoaded = True
                    End With
                End If
                If Not IsDBNull(oDrd("CcaGuid")) Then
                    Dim oCta As New DTOPgcCta(oDrd("CtaGuid"))
                    With oCta
                        .Id = oDrd("Id")
                        .Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "Esp", "Cat", "Eng")
                        .IsBaseImponibleIva = oDrd("IsBaseImponibleIVA")
                        .IsQuotaIva = oDrd("IsQuotaIva")
                        .Act = oDrd("Act")
                        .Codi = SQLHelper.GetIntegerFromDataReader(oDrd("CtaCodi"))
                    End With
                    Dim oContact As DTOContact = Nothing
                    If Not IsDBNull(oDrd("ContactGuid")) Then
                        oContact = New DTOContact(oDrd("ContactGuid"))
                        With oContact
                            .FullNom = oDrd("FullNom")
                        End With
                    End If
                    Dim item As New DTOCcb
                    With item
                        .Cca = oCca
                        .Cta = oCta
                        .Contact = oContact
                        .Amt = DTOAmt.Factory(CDec(oDrd("Eur")), oDrd("Cur").ToString, CDec(oDrd("Pts")))
                        .Dh = oDrd("dh")
                        If Not IsDBNull(oDrd("PndGuid")) Then
                            .Pnd = New DTOPnd(oDrd("PndGuid"))
                            With .Pnd
                                .Fch = oDrd("Fch")
                                If Not IsDBNull(oDrd("PndCca")) Then
                                    .Cca = New DTOCca(oDrd("PndCca"))
                                    If Not IsDBNull(oDrd("PndCcaHash")) Then
                                        .Cca.DocFile = New DTODocFile(oDrd("PndCcaHash"))
                                    End If
                                End If
                                .FraNum = oDrd("FraNum")
                            End With
                        End If
                    End With
                    oCca.Items.Add(item)
                End If
            Loop

            oDrd.Close()
        End If

        Dim retval As Boolean = oCca.IsLoaded
        Return retval
    End Function

    Shared Function Pdf(oCca As DTOCca) As Byte()
        Dim retVal = (New List(Of Byte)).ToArray()
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT DocFile.Doc ")
        sb.AppendLine("FROM Cca ")
        sb.AppendLine("INNER JOIN DocFile ON Cca.Hash = DocFile.Hash ")
        sb.AppendLine("WHERE Cca.Guid = '" & oCca.Guid.ToString() & "'")
        Dim SQL = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            If Not IsDBNull(oDrd("Doc")) Then
                retVal = oDrd("Doc")
            End If
        End If
        oDrd.Close()
        Return retVal
    End Function

    Shared Function Update(oCca As DTOCca, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oCca, oTrans)
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

    Shared Function SavePagament(oCca As DTOCca, oPnds As List(Of DTOPnd), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            SavePagament(oCca, oPnds, oTrans)

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


    Shared Sub SavePagament(oCca As DTOCca, oPnds As List(Of DTOPnd), oTrans As SqlTransaction)

        If oCca.DocFile IsNot Nothing Then
            DocFileLoader.Update(oCca.DocFile, oTrans)
        End If

        UpdateHeader(oCca, oTrans)
        UpdateItems(oCca, oTrans)
        For Each oPnd As DTOPnd In oPnds
            PndLoader.Salda(oPnd, oCca, oTrans)
        Next

    End Sub


    Shared Sub Update(ByRef oCca As DTOCca, ByRef oTrans As SqlTransaction)
        DocFileLoader.Update(oCca.DocFile, oTrans)

        UpdateHeader(oCca, oTrans)

        PndsLoader.Delete(oCca, oTrans)
        If oCca.Pnds IsNot Nothing Then
            For Each oPnd As DTOPnd In oCca.Pnds
                PndLoader.Update(oPnd, oTrans)
            Next
        End If

        UpdateItems(oCca, oTrans)

        If oCca.BookFra IsNot Nothing Then
            If oCca.BookFra.Cca Is Nothing Then oCca.BookFra.Cca = oCca
            BookFraLoader.Update(oCca.BookFra, oTrans)
        End If

    End Sub

    Shared Sub UpdateHeader(ByRef oCca As DTOCca, ByRef oTrans As SqlTransaction)
        If oCca.Id = 0 Then oCca.Id = NextId(oCca.Exercici, oTrans)
        Dim SQL As String = "SELECT * FROM Cca WHERE Guid='" & oCca.Guid.ToString & "'"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oCca.Guid
        Else
            oRow = oTb.Rows(0)
            If oRow("Yea") <> oCca.Fch.Year Then
                oCca.Id = NextId(oCca.Exercici, oTrans)
            End If
        End If

        With oCca
            oRow("Emp") = .Exercici.Emp.Id
            oRow("Yea") = .Fch.Year
            oRow("Fch") = .Fch.Date
            oRow("Cca") = .Id
            oRow("Txt") = Left(.Concept, 60)
            oRow("ccd") = .Ccd
            oRow("Cdn") = .Cdn
            oRow("Ref") = SQLHelper.NullableBaseGuid(.Ref)
            oRow("Projecte") = SQLHelper.NullableBaseGuid(.Projecte)
            If .Items.Count = 0 Then
                Throw New System.Exception("No hi ha cap partida al desar l'assentament")
            End If

            oRow("Hash") = SQLHelper.NullableDocFile(.DocFile)

            SQLHelper.SetUsrLog(.UsrLog, oRow, UsrCreatedField:="UsrCreatedGuid", UsrLastEditedField:="UsrLastEditedGuid")

        End With

        oDA.Update(oDs)
    End Sub

    Shared Function UpdateDocfile(oCca As DTOCca, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            UpdateDocfile(oCca, oTrans)
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

    Shared Sub UpdateDocfile(ByRef oCca As DTOCca, ByRef oTrans As SqlTransaction)
        DocFileLoader.Update(oCca.DocFile, oTrans)
        UpdateCcaDocfile(oCca, oTrans)
    End Sub

    Shared Sub UpdateCcaDocfile(ByRef oCca As DTOCca, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "UPDATE Cca SET Hash='" & oCca.DocFile.Hash & "' WHERE Cca.Guid='" & oCca.Guid.ToString & "'"
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub UpdateItems(oCca As DTOCca, ByRef oTrans As SqlTransaction)
        If Not oCca.IsNew Then DeleteItems(oCca, oTrans)

        Dim SQL As String = "SELECT * FROM Ccb WHERE CcaGuid='" & oCca.Guid.ToString & "'"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        For Each item As DTOCcb In oCca.Items
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)

            With oCca
                oRow("CcaGuid") = .Guid
                oRow("Lin") = .Items.IndexOf(item)
            End With

            With item
                oRow("Guid") = .Guid
                oRow("CtaGuid") = .Cta.Guid
                oRow("ContactGuid") = SQLHelper.NullableBaseGuid(.Contact)
                oRow("Pts") = .Amt.Val
                oRow("Cur") = .Amt.Cur.Tag
                oRow("Eur") = .Amt.Eur
                oRow("Dh") = .Dh

                oRow("PndGuid") = SQLHelper.NullableBaseGuid(.Pnd)
            End With

        Next
        oDA.Update(oDs)
    End Sub

    Shared Function NextId(oExercici As DTOExercici, ByRef oTrans As SqlTransaction) As Integer
        Dim lastId As Integer
        Dim SQL As String = "SELECT MAX(Cca) AS LastId FROM Cca WHERE Emp=" & oExercici.Emp.Id & " AND Year(Fch)=" & oExercici.Year & " "

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        If oTb.Rows.Count > 0 Then
            Dim oRow As DataRow = oTb.Rows(0)
            If Not IsDBNull(oRow("LastId")) Then
                lastId = CInt(oRow("LastId"))
            End If
        End If
        Return lastId + 1
    End Function

    Shared Function Delete(oCca As DTOCca, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oCca, oTrans)
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


    Shared Sub Delete(oCca As DTOCca, ByRef oTrans As SqlTransaction)
        If oCca IsNot Nothing Then
            DeletePnds(oCca, oTrans)
            PndsLoader.SaldaBack(oCca, oTrans)
            DeleteItems(oCca, oTrans)
            BookFraLoader.Delete(oCca, oTrans)
            DeleteCca(oCca, oTrans)
        End If
    End Sub

    Shared Sub DeletePnds(oCca As DTOCca, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Pnd WHERE CcaGuid='" & oCca.Guid.ToString & "'"
        Dim i As Integer = SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteItems(oCca As DTOCca, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Ccb WHERE CcaGuid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oCca.Guid.ToString())
    End Sub

    Shared Sub DeleteCca(oCca As DTOCca, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Cca WHERE Guid='" & oCca.Guid.ToString & "'"
        Dim i As Integer = SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub
#End Region


    Shared Function IvaFchUltimaDeclaracio(oEmp As DTOEmp) As Date
        Dim RetVal As Date = Date.MinValue
        Dim SQL As String = "SELECT TOP (1) FCH FROM CCA WHERE EMP=" & oEmp.Id & " AND CCD=" & CInt(DTOCca.CcdEnum.IVA) & " ORDER BY FCH DESC"
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            RetVal = CDate(oDrd("FCH"))
        End If
        oDrd.Close()
        Return RetVal
    End Function

    Shared Function FromCdn(oEmp As DTOEmp, ByVal IntYea As Integer, ByVal eCcd As DTOCca.CcdEnum, ByVal LngCdn As Long) As DTOCca
        Dim SQL As String = "SELECT Guid FROM CCA WHERE " _
        & "EMP=" & oEmp.Id & " AND " _
        & "YEA=" & IntYea & " AND " _
        & "CCD=" & CInt(eCcd) & " AND " _
        & "CDN=" & LngCdn.ToString

        Dim retval As DTOCca = Nothing
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOCca(oDrd("Guid"))
            With retval
                .Ccd = eCcd
                .Cdn = LngCdn
            End With
        End If
        oDrd.Close()

        Return retval
    End Function
End Class

Public Class CcasLoader

    Shared Function Model(oUser As DTOUser, year As Integer) As List(Of Models.Compact.Cca)

        Dim retval As New List(Of Models.Compact.Cca)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Cca.Guid, Cca.Cca, Cca.Fch, Cca.Txt, Cca.Ccd, Cca.Hash ")
        sb.AppendLine(", Cca.UsrCreatedGuid, VwUsrNickname.Nom AS UsrCreatedNickname ")
        sb.AppendLine(", SUM(CASE WHEN Ccb.Dh = 1 THEN Ccb.Eur ELSE 0 END) AS Eur ")
        sb.AppendLine("FROM Cca ")
        sb.AppendLine("LEFT OUTER JOIN Ccb ON Cca.Guid = Ccb.CcaGuid ")
        sb.AppendLine("LEFT OUTER JOIN VwUsrNickname ON Cca.UsrCreatedGuid = VwUsrNickname.Guid ")
        sb.AppendLine("WHERE Cca.Emp = " & oUser.Emp.Id & " ")
        sb.AppendLine("AND Cca.Yea = " & year & " ")
        sb.AppendLine("GROUP BY Cca.Guid, Cca.Cca, Cca.Fch, Cca.Txt, Cca.Ccd, Cca.Hash ")
        sb.AppendLine(", Cca.UsrCreatedGuid, VwUsrNickname.Nom ")
        sb.AppendLine("ORDER BY Cca.Cca DESC")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oCca As New Models.Compact.Cca
            With oCca
                .Guid = oDrd("Guid")
                .Id = oDrd("Cca")
                .Fch = oDrd("Fch")
                .Concept = oDrd("txt")
                .Ccd = oDrd("Ccd")
                .Eur = New DTO.Models.Compact.Amt
                .Eur.Eur = oDrd("Eur")
                If Not IsDBNull(oDrd("Hash")) Then
                    .DocFile = New DTO.Models.Compact.DocFile With {
                        .Hash = oDrd("Hash")
                    }
                End If
                .UsrLog = New Models.Compact.UsrLog(oDrd("UsrCreatedGuid"), oDrd("UsrCreatedNickname"))
            End With
            retval.Add(oCca)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function Headers(exercici As DTOExercici) As List(Of DTOCca)

        Dim retval As New List(Of DTOCca)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Cca.Guid, Cca.Cca, Cca.Fch, Cca.Txt, Cca.Ccd, Cca.Hash ")
        sb.AppendLine(", Cca.UsrCreatedGuid AS UsrCreated, Email.Adr AS UsrCreatedEmailAddress, Email.Nickname AS UsrCreatedNickName ")
        sb.AppendLine(", SUM(CASE WHEN Ccb.Dh = 1 THEN Ccb.Eur ELSE 0 END) AS Eur ")
        sb.AppendLine("FROM Cca ")
        sb.AppendLine("LEFT OUTER JOIN Ccb ON Cca.Guid = Ccb.CcaGuid ")
        sb.AppendLine("LEFT OUTER JOIN Email ON Cca.UsrCreatedGuid = Email.Guid ")
        sb.AppendLine("WHERE Cca.Emp = " & exercici.Emp.Id & " ")
        sb.AppendLine("AND Cca.Yea = " & exercici.Year & " ")
        sb.AppendLine("GROUP BY Cca.Guid, Cca.Cca, Cca.Fch, Cca.Txt, Cca.Ccd, Cca.Hash ")
        sb.AppendLine(", Cca.UsrCreatedGuid, Email.Adr, Email.Nickname ")
        sb.AppendLine("ORDER BY Cca.Cca DESC")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oCca As New DTOCca(oDrd("Guid"))
            With oCca
                .Exercici = exercici
                .Id = oDrd("Cca")
                .Fch = oDrd("Fch")
                .Concept = oDrd("txt")
                .Ccd = oDrd("Ccd")
                .Eur = DTOAmt.Factory(oDrd("Eur"))
                .DocFile = SQLHelper.GetDocFileFromDataReaderHash(oDrd("Hash"))
                .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd)
            End With
            retval.Add(oCca)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(exercici As DTOExercici,
                            Optional ccd As DTOCca.CcdEnum = DTOCca.CcdEnum.NotSet, Optional OnlyIvaRelateds As Boolean = False) As List(Of DTOCca)

        Dim retval As New List(Of DTOCca)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT VwCca.CcaGuid, VwCca.CcaId, VwCca.Fch, VwCca.Txt, VwCca.Ccd, VwCca.Cdn ")
        sb.AppendLine(", VwCca.CcbGuid, VwCca.Eur, VwCca.Cur, VwCca.Pts, VwCca.Dh, VwCca.PndGuid ")
        sb.AppendLine(", VwCca.CtaGuid, VwCca.CtaId, VwCca.CtaCod, VwCca.CtaAct, VwCca.CtaEsp, VwCca.CtaCat, VwCca.CtaEng ")
        sb.AppendLine(", VwCca.ContactGuid, VwCca.FullNom ")
        sb.AppendLine(", VwCca.UsrCreatedGuid AS UsrCreated, VwCca.UsrCreatedAdr AS UsrCreatedEmailAddress, VwCca.UsrCreatedNickname, VwCca.FchCreated ")
        sb.AppendLine(", VwCca.Hash ")
        sb.AppendLine("FROM VwCca ")
        If OnlyIvaRelateds Then
            sb.AppendLine("INNER JOIN PgcCta ON VwCca.CtaGuid = PgcCta.Guid AND PgcCta.IsQuotaIva=1 ")
        End If
        sb.AppendLine("WHERE VwCca.Emp = " & exercici.Emp.Id & " ")
        sb.AppendLine("AND Year(VwCca.Fch) = " & exercici.Year & " ")
        If ccd <> DTOCca.CcdEnum.NotSet Then
            sb.AppendLine("AND VwCca.Ccd = " & CInt(ccd) & " ")
        End If
        sb.AppendLine("ORDER BY VwCca.FchCreated DESC")

        Dim oCca As New DTOCca
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oCca.Guid.Equals(oDrd("CcaGuid")) Then
                oCca = New DTOCca(oDrd("CcaGuid"))
                With oCca
                    .Exercici = exercici
                    .Id = oDrd("CcaId")
                    .Fch = oDrd("Fch")
                    .Concept = oDrd("txt")
                    .Ccd = oDrd("Ccd")
                    .Cdn = oDrd("Cdn")
                    .DocFile = SQLHelper.GetDocFileFromDataReaderHash(oDrd("Hash"))
                    .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd)
                End With
                retval.Add(oCca)
            End If

            Dim oContact As DTOContact = Nothing
            If Not IsDBNull(oDrd("ContactGuid")) Then
                oContact = New DTOContact(oDrd("ContactGuid")) With {
                    .FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                }
            End If

            Dim oCta As New DTOPgcCta(oDrd("CtaGuid"))
            With oCta
                .Id = oDrd("CtaId")
                .Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "CtaEsp", "CtaCat", "CtaEng")
                .Codi = oDrd("CtaCod")
                .Act = oDrd("CtaAct")
            End With

            Dim oCcb As New DTOCcb(oDrd("CcbGuid"))
            With oCcb
                .Dh = oDrd("Dh")
                .Cca = oCca
                .Cta = oCta
                .Contact = oContact
                .Amt = DTOAmt.Factory(CDec(oDrd("Eur")), DTOCur.Factory(oDrd("Cur")), CDec(oDrd("Pts")))
            End With
            oCca.Items.Add(oCcb)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Update(oCcas As List(Of DTOCca), exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            For Each oCca As DTOCca In oCcas
                CcaLoader.Update(oCca, oTrans)
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


    Shared Function Descuadres(oExercici As DTOExercici) As List(Of DTOCca)
        Dim retval As New List(Of DTOCca)
        Dim sb As New Text.StringBuilder

        sb.AppendLine("SELECT VwCca.CcaGuid, VwCca.CcaId, VwCca.Fch, VwCca.Txt, VwCca.Ccd, VwCca.Cdn ")
        sb.AppendLine(", VwCca.CcbGuid, VwCca.Eur, VwCca.Cur, VwCca.Pts, VwCca.Dh, VwCca.PndGuid ")
        sb.AppendLine(", VwCca.CtaGuid, VwCca.CtaId, VwCca.CtaCod, VwCca.CtaAct, VwCca.CtaEsp, VwCca.CtaCat, VwCca.CtaEng ")
        sb.AppendLine(", VwCca.ContactGuid, VwCca.FullNom ")
        sb.AppendLine(", VwCca.UsrCreatedGuid AS UsrCreated, VwCca.UsrCreatedAdr AS UsrCreatedEmailAddress, VwCca.UsrCreatedNickname, VwCca.FchCreated ")
        sb.AppendLine(", VwCca.Hash ")
        sb.AppendLine("FROM VwCca ")
        sb.AppendLine("INNER JOIN ")
        sb.AppendLine("(SELECT Cca.Guid ")
        sb.AppendLine("  FROM Cca ")
        sb.AppendLine("  INNER JOIN Ccb ON Cca.Guid = Ccb.CcaGuid ")
        sb.AppendLine("  WHERE Cca.Emp = " & oExercici.Emp.Id & " AND YEAR(Cca.Fch)=" & oExercici.Year & " ")
        sb.AppendLine("  GROUP BY Cca.Guid ")
        sb.AppendLine("  HAVING SUM(CASE WHEN Ccb.Dh=1 THEN Ccb.Eur ELSE -Ccb.Eur END)-SUM(CASE WHEN Ccb.Dh=2 THEN Ccb.Eur ELSE -Ccb.Eur END)<>0 ")
        sb.AppendLine(" ) X ON VwCca.CcaGuid = X.Guid ")
        sb.AppendLine("ORDER BY VwCca.Fch DESC, VwCca.CcaId DESC ")

        Dim oCca As New DTOCca
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oCca.Guid.Equals(oDrd("CcaGuid")) Then
                oCca = New DTOCca(oDrd("CcaGuid"))
                With oCca
                    .Exercici = oExercici
                    .Id = oDrd("CcaId")
                    .Fch = oDrd("Fch")
                    .Concept = oDrd("txt")
                    .Ccd = oDrd("Ccd")
                    .Cdn = oDrd("Cdn")
                    .Eur = SQLHelper.GetAmtFromDataReader(oDrd("Eur"))
                    .DocFile = SQLHelper.GetDocFileFromDataReaderHash(oDrd("Hash"))
                    .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd)
                End With
                retval.Add(oCca)
            End If

            Dim oContact As DTOContact = Nothing
            If Not IsDBNull(oDrd("ContactGuid")) Then
                oContact = New DTOContact(oDrd("ContactGuid")) With {
                    .FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                }
            End If

            Dim oCta As New DTOPgcCta(oDrd("CtaGuid"))
            With oCta
                .Id = oDrd("CtaId")
                .Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "CtaEsp", "CtaCat", "CtaEng")
                .Codi = oDrd("CtaCod")
                .Act = oDrd("CtaAct")
            End With

            Dim oCcb As New DTOCcb(oDrd("CcbGuid"))
            With oCcb
                .Dh = oDrd("Dh")
                .Cca = oCca
                .Cta = oCta
                .Contact = oContact
                .Amt = DTOAmt.Factory(CDec(oDrd("Eur")), DTOCur.Factory(oDrd("Cur")), CDec(oDrd("Pts")))
            End With
            oCca.Items.Add(oCcb)
        Loop
        oDrd.Close()

        Return retval
    End Function
End Class
