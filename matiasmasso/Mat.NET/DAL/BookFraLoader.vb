Public Class BookFraLoader

#Region "CRUD"

    Shared Function Find(oCca As DTOCca) As DTOBookFra
        Dim retval As DTOBookFra = Nothing
        Dim oBookFra As New DTOBookFra(oCca)
        If Load(oBookFra) Then
            retval = oBookFra
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oBookFra As DTOBookFra) As Boolean
        If Not oBookFra.IsLoaded And Not oBookFra.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT BookFras.ContactGuid, BookFras.CtaGuid, BookFras.FraNum ")
            sb.AppendLine(", BookFras.ClaveExenta, BookFras.TipoFra, BookFras.Dsc ")
            sb.AppendLine(", BookFras.BaseSujeta, BookFras.TipoIva, BookFras.QuotaIva ")
            sb.AppendLine(", BookFras.BaseSujeta1, BookFras.TipoIva1, BookFras.QuotaIva1 ")
            sb.AppendLine(", BookFras.BaseSujeta2, BookFras.TipoIva2, BookFras.QuotaIva2 ")
            sb.AppendLine(", BookFras.BaseExenta ")
            sb.AppendLine(", BookFras.BaseIrpf, BookFras.TipoIrpf ")
            sb.AppendLine(", BookFras.ClaveRegimenEspecialOTrascendencia ")
            sb.AppendLine(", Cca.Cca, Cca.fch, Cca.Hash ")
            sb.AppendLine(", CliGral.RaoSocial, CliGral.FullNom ")
            sb.AppendLine(", CliGral.NIF, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod ")
            sb.AppendLine(", PgcCta.Id as CtaId, PgcCta.Esp as CtaEsp, PgcCta.Cat as CtaCat, PgcCta.Eng as CtaEng ")
            sb.AppendLine(", BookFras.SiiResult, BookFras.SiiFch, BookFras.SiiCsv, BookFras.SiiErr ")
            'sb.AppendLine(", BookFras.SiiEstadoCuadre, BookFras.SiiTimestampEstadoCuadre, BookFras.SiiTimestampUltimaModificacion ")
            sb.AppendLine(", VwAddress.* ")
            sb.AppendLine("FROM            BookFras ")
            sb.AppendLine("INNER JOIN Cca ON BookFras.CcaGuid = CCA.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CliGral ON BookFras.ContactGuid = CliGral.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwAddress ON BookFras.ContactGuid = VwAddress.SrcGuid ")
            sb.AppendLine("LEFT OUTER JOIN PgcCta ON BookFras.CtaGuid = PgcCta.Guid ")
            sb.AppendLine("WHERE BookFras.CcaGuid='" & oBookFra.Cca.Guid.ToString & "'")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                Dim oCta As DTOPgcCta = Nothing
                If Not IsDBNull(oDrd("CtaGuid")) Then
                    oCta = New DTOPgcCta(DirectCast(oDrd("CtaGuid"), Guid))
                    oCta.Id = oDrd("CtaId")
                    oCta.Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "CtaEsp", "CtaCat", "CtaEng")
                End If

                Dim oContact As DTOContact = Nothing
                If Not IsDBNull(oDrd("ContactGuid")) Then
                    oContact = New DTOContact(DirectCast(oDrd("ContactGuid"), Guid))
                    oContact.Nom = oDrd("RaoSocial")
                    oContact.Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
                    oContact.FullNom = oDrd("FullNom")
                    oContact.Address = SQLHelper.GetAddressFromDataReader(oDrd)
                End If

                With oBookFra
                    Dim sHash = SQLHelper.GetStringFromDataReader(oDrd("Hash"))
                    If .Cca.DocFile Is Nothing And Not String.IsNullOrEmpty(sHash) Then
                        .Cca.DocFile = New DTODocFile(sHash)
                    End If
                    .Cca.Id = oDrd("Cca")
                    .Cca.Fch = oDrd("Fch")
                    .TipoFra = SQLHelper.GetStringFromDataReader(oDrd("TipoFra"))
                    .FraNum = oDrd("FraNum").ToString
                    .ClaveRegimenEspecialOTrascendencia = SQLHelper.GetStringFromDataReader(oDrd("ClaveRegimenEspecialOTrascendencia"))
                    .Cta = oCta
                    .Contact = oContact
                    .dsc = SQLHelper.GetStringFromDataReader(oDrd("Dsc"))
                    .IvaBaseQuotas = New List(Of DTOBaseQuota)
                    If Not IsDBNull(oDrd("BaseSujeta")) Then
                        Dim oSujeta As New DTOBaseQuota(SQLHelper.GetAmtFromDataReader(oDrd("BaseSujeta")), SQLHelper.GetDecimalFromDataReader(oDrd("TipoIva")), SQLHelper.GetAmtFromDataReader(oDrd("QuotaIva")))
                        .IvaBaseQuotas.Add(oSujeta)
                    End If
                    If Not IsDBNull(oDrd("BaseSujeta1")) Then
                        Dim oSujeta1 As New DTOBaseQuota(SQLHelper.GetAmtFromDataReader(oDrd("BaseSujeta1")), SQLHelper.GetDecimalFromDataReader(oDrd("TipoIva1")), SQLHelper.GetAmtFromDataReader(oDrd("QuotaIva1")))
                        .IvaBaseQuotas.Add(oSujeta1)
                    End If
                    If Not IsDBNull(oDrd("BaseSujeta2")) Then
                        Dim oSujeta2 As New DTOBaseQuota(SQLHelper.GetAmtFromDataReader(oDrd("BaseSujeta2")), SQLHelper.GetDecimalFromDataReader(oDrd("TipoIva2")), SQLHelper.GetAmtFromDataReader(oDrd("QuotaIva2")))
                        .IvaBaseQuotas.Add(oSujeta2)
                    End If
                    If Not IsDBNull(oDrd("BaseExenta")) Then
                        Dim oExenta As New DTOBaseQuota(SQLHelper.GetAmtFromDataReader(oDrd("BaseExenta")))
                        .IvaBaseQuotas.Add(oExenta)
                        .ClaveExenta = SQLHelper.GetStringFromDataReader(oDrd("ClaveExenta"))
                    End If
                    If Not IsDBNull(oDrd("BaseIrpf")) Then
                        .IrpfBaseQuota = New DTOBaseQuota(SQLHelper.GetAmtFromDataReader(oDrd("BaseIrpf")), SQLHelper.GetDecimalFromDataReader(oDrd("TipoIrpf")))
                    End If
                    .SiiLog = SQLHelper.GetSiiLogFromDataReader(oDrd)
                    '.SiiErrCod = SQLHelper.GetIntegerFromDataReader(oDrd("SiiErrCod"))
                    '.SiiEstadoCuadre = SQLHelper.GetIntegerFromDataReader(oDrd("SiiEstadoCuadre"))
                    '.SiiTimestampEstadoCuadre = SQLHelper.GetFchFromDataReader(oDrd("SiiTimestampEstadoCuadre"))
                    '.SiiTimestampUltimaModificacion = SQLHelper.GetFchFromDataReader(oDrd("SiiTimestampUltimaModificacion"))
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oBookFra.IsLoaded
        Return retval
    End Function

    Shared Function Update(oBookFra As DTOBookFra, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oBookFra, oTrans)
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


    Shared Sub Update(oBookFra As DTOBookFra, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM BookFras WHERE CcaGuid='" & oBookFra.Cca.Guid.ToString & "'"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("CcaGuid") = oBookFra.Cca.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oBookFra
            oRow("TipoFra") = .TipoFra
            oRow("ClaveRegimenEspecialOTrascendencia") = SQLHelper.NullableString(.ClaveRegimenEspecialOTrascendencia)
            oRow("FraNum") = .FraNum
            oRow("CtaGuid") = SQLHelper.NullableBaseGuid(.Cta)
            oRow("ContactGuid") = SQLHelper.NullableBaseGuid(.Contact)
            oRow("Dsc") = SQLHelper.NullableString(.Dsc)
            Dim oSujetas As List(Of DTOBaseQuota) = .IvaBaseQuotas.Where(Function(x) x.Tipus <> 0).OrderByDescending(Function(x) x.Tipus).ToList
            If oSujetas.Count = 0 Then
                oRow("BaseSujeta") = System.DBNull.Value
                oRow("TipoIva") = System.DBNull.Value
                oRow("BaseSujeta1") = System.DBNull.Value
                oRow("TipoIva1") = System.DBNull.Value
                oRow("QuotaIva1") = System.DBNull.Value
                oRow("BaseSujeta2") = System.DBNull.Value
                oRow("TipoIva2") = System.DBNull.Value
                oRow("QuotaIva2") = System.DBNull.Value
            Else
                oRow("BaseSujeta") = SQLHelper.NullableAmt(oSujetas(0).baseImponible)
                oRow("TipoIva") = SQLHelper.NullableDecimal(oSujetas(0).Tipus)
                oRow("QuotaIva") = SQLHelper.NullableAmt(oSujetas(0).Quota)
                If oSujetas.Count > 1 Then
                    oRow("BaseSujeta1") = SQLHelper.NullableAmt(oSujetas(1).baseImponible)
                    oRow("TipoIva1") = SQLHelper.NullableDecimal(oSujetas(1).Tipus)
                    oRow("QuotaIva1") = SQLHelper.NullableAmt(oSujetas(1).Quota)
                    If oSujetas.Count > 2 Then
                        oRow("BaseSujeta2") = SQLHelper.NullableAmt(oSujetas(2).baseImponible)
                        oRow("TipoIva2") = SQLHelper.NullableDecimal(oSujetas(2).Tipus)
                        oRow("QuotaIva2") = SQLHelper.NullableAmt(oSujetas(2).Quota)
                    Else
                        oRow("BaseSujeta2") = System.DBNull.Value
                        oRow("TipoIva2") = System.DBNull.Value
                        oRow("QuotaIva2") = System.DBNull.Value
                    End If
                Else
                    oRow("BaseSujeta1") = System.DBNull.Value
                    oRow("TipoIva1") = System.DBNull.Value
                    oRow("QuotaIva1") = System.DBNull.Value
                    oRow("BaseSujeta2") = System.DBNull.Value
                    oRow("TipoIva2") = System.DBNull.Value
                    oRow("QuotaIva2") = System.DBNull.Value
                End If
            End If

            Dim oExenta As DTOBaseQuota = .IvaBaseQuotas.FirstOrDefault(Function(x) x.Tipus = 0)
            If oExenta Is Nothing Then
                oRow("BaseExenta") = System.DBNull.Value
                oRow("ClaveExenta") = System.DBNull.Value
            Else
                oRow("BaseExenta") = SQLHelper.NullableAmt(oExenta.baseImponible)
                oRow("ClaveExenta") = SQLHelper.NullableString(.ClaveExenta)
            End If
            If .IrpfBaseQuota IsNot Nothing Then
                oRow("BaseIrpf") = SQLHelper.NullableAmt(.irpfBaseQuota.baseImponible)
                oRow("TipoIrpf") = SQLHelper.NullableDecimal(.IrpfBaseQuota.Tipus)
            End If

            If .SiiLog IsNot Nothing Then
                SQLHelper.SetSiiLog(.SiiLog, oRow)
            End If
            'oRow("SiiErrCod") = SQLHelper.NullableInt(.SiiErrCod)
            'oRow("SiiEstadoCuadre") = SQLHelper.NullableInt(.SiiEstadoCuadre)
            'oRow("SiiTimestampEstadoCuadre") = SQLHelper.NullableFch(.SiiTimestampEstadoCuadre)
            'oRow("SiiTimestampUltimaModificacion") = SQLHelper.NullableFch(.SiiTimestampUltimaModificacion)

        End With

        oDA.Update(oDs)
    End Sub



    Shared Function LogSii(oBookFra As DTOBookFra, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim oLog = oBookFra.SiiLog
        If oLog IsNot Nothing Then
            Dim sb As New Text.StringBuilder
            sb.AppendLine("UPDATE BookFras ")
            sb.AppendLine("SET BookFras.SiiResult=" & oLog.Result & " ")
            sb.AppendLine(", BookFras.SiiFch='" & SQLHelper.FormatDatetime(oLog.Fch) & "' ")
            If oLog.Csv = "" Then
                sb.AppendLine(", BookFras.SiiCsv=NULL ")
            Else
                sb.AppendLine(", BookFras.SiiCsv='" & oLog.Csv & "' ")
            End If
            If (oLog.ErrMsg = "") Then
                sb.AppendLine(", BookFras.SiiErr = NULL ")
            Else
                sb.AppendLine(", BookFras.SiiErr='" & oLog.ErrMsg & "' ")
            End If
            sb.AppendLine("WHERE BookFras.CcaGuid='" & oBookFra.Cca.Guid.ToString & "'")
            Dim SQL As String = sb.ToString
            retval = SQLHelper.ExecuteNonQuery(SQL, exs)
        End If
        Return retval
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
        Dim SQL As String = "DELETE BookFras WHERE CcaGuid='" & oCca.Guid.ToString & "'"
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class BookFrasLoader

    Shared Function SiiPending(oEmp As DTOEmp) As List(Of DTOBookFra)
        Dim retval As New List(Of DTOBookFra)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT BookFras.CcaGuid, Cca.Fch ")
        sb.AppendLine("FROM BookFras ")
        sb.AppendLine("INNER JOIN Cca ON BookFras.CcaGuid=Cca.Guid ")
        sb.AppendLine("WHERE Cca.Emp=" & oEmp.Id & " ")
        sb.AppendLine("AND Cca.Yea >= 2017 ")
        sb.AppendLine("AND BookFras.SiiCsv IS NULL ")
        sb.AppendLine("ORDER BY Cca.Fch ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oCca As New DTOCca(oDrd("CcaGuid"))
            oCca.Fch = oDrd("Fch")
            Dim item As New DTOBookFra(oCca)
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function All(mode As DTOBookFra.Modes,
                        exercici As DTOExercici,
                        Optional mes As Integer = 0,
                        Optional contact As DTOContact = Nothing,
                        Optional showProgress As ProgressBarHandler = Nothing) As List(Of DTOBookFra)

        Dim retval As New List(Of DTOBookFra)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT BookFras.CcaGuid, BookFras.ContactGuid, BookFras.CtaGuid, BookFras.FraNum ")
        sb.AppendLine(", BookFras.ClaveExenta, BookFras.TipoFra, BookFras.Dsc ")
        sb.AppendLine(", BookFras.BaseSujeta, BookFras.TipoIva, BookFras.QuotaIva, BookFras.BaseExenta ")
        sb.AppendLine(", BookFras.BaseSujeta2, BookFras.TipoIva2, BookFras.QuotaIva2 ")
        sb.AppendLine(", BookFras.BaseIrpf, BookFras.TipoIrpf ")
        sb.AppendLine(", BookFras.SiiResult, BookFras.SiiFch, BookFras.SiiCsv, BookFras.SiiErr ")
        sb.AppendLine(", Cca.Cca, Cca.fch, Cca.Hash, Cca.FchCreated ")
        sb.AppendLine(", CliGral.RaoSocial ")
        sb.AppendLine(", CliGral.NIF, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod ")
        sb.AppendLine(", VwAddress.Adr, VwAddress.ZipGuid, VwAddress.ZipCod, VwAddress.LocationGuid, VwAddress.LocationNom ")
        sb.AppendLine(", VwAddress.ProvinciaGuid, VwAddress.ZonaGuid, VwAddress.ProvinciaNom, VwAddress.CountryGuid, VwAddress.CountryEsp, VwAddress.CountryISO, VwAddress.CEE ")
        sb.AppendLine(", PgcCta.Id as CtaId, PgcCta.Cod AS CtaCod, PgcCta.Esp as CtaEsp, PgcCta.Cat as CtaCat, PgcCta.Eng as CtaEng ")
        sb.AppendLine("FROM            BookFras ")
        sb.AppendLine("INNER JOIN Cca ON BookFras.CcaGuid = CCA.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON BookFras.ContactGuid = CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwAddress ON BookFras.ContactGuid = VwAddress.SrcGuid ")
        sb.AppendLine("LEFT OUTER JOIN PgcCta ON BookFras.CtaGuid = PgcCta.Guid ")
        sb.AppendLine("WHERE Cca.Emp=" & CInt(exercici.Emp.Id) & " AND Cca.Yea=" & exercici.Year & " ")

        Select Case mode
            Case DTOBookFra.Modes.OnlyIva
                sb.AppendLine("AND BookFras.Iva<>0 ")
            Case DTOBookFra.Modes.OnlyIRPF
                sb.AppendLine("AND BookFras.Irpf<>0 ")
        End Select

        If contact IsNot Nothing Then
            sb.AppendFormat("AND BookFras.ContactGuid='{0}' ", contact.Guid.ToString())
        End If

        If mes > 0 Then
            sb.AppendLine("AND Month(Cca.Fch)= " & mes & " ")
        End If

        sb.AppendLine("ORDER BY CCA.fch DESC, CliGral.RaoSocial, BookFras.FraNum")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oBookFra As DTOBookFra = Nothing


            Dim oCca As New DTOCca(DirectCast(oDrd("CcaGuid"), Guid))
            With oCca
                .Exercici = exercici
                .Id = oDrd("Cca")
                .Fch = oDrd("Fch")
                If Not IsDBNull(oDrd("Hash")) Then
                    .DocFile = New DTODocFile(oDrd("Hash").ToString())
                End If
                SQLHelper.getUsrLogFromDataReader(oDrd)
            End With

            Dim oCta As DTOPgcCta = Nothing
            If Not IsDBNull(oDrd("CtaGuid")) Then
                oCta = New DTOPgcCta(oDrd("CtaGuid"))
                oCta.Id = oDrd("CtaId")
                oCta.Codi = SQLHelper.GetIntegerFromDataReader(oDrd("CtaCod"))
                oCta.Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "CtaEsp", "CtaCat", "CtaEng")
            End If

            Dim oContact As DTOContact = Nothing
            If IsDBNull(oDrd("ContactGuid")) Then
            Else
                oContact = New DTOContact(oDrd("ContactGuid"))
                oContact.Nom = SQLHelper.GetStringFromDataReader(oDrd("RaoSocial"))

                oContact.Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
                oContact.FullNom = oContact.Nom
                oContact.address = SQLHelper.GetAddressFromDataReader(oDrd)
            End If

            oBookFra = New DTOBookFra(oCca)
            With oBookFra
                .TipoFra = SQLHelper.GetStringFromDataReader(oDrd("TipoFra"))
                .FraNum = oDrd("FraNum").ToString
                .Cta = oCta
                .Contact = oContact
                .dsc = SQLHelper.GetStringFromDataReader(oDrd("Dsc"))
                .IvaBaseQuotas = New List(Of DTOBaseQuota)
                If Not IsDBNull(oDrd("BaseSujeta")) Then
                    Dim oSujeta As New DTOBaseQuota(SQLHelper.GetAmtFromDataReader(oDrd("BaseSujeta")), SQLHelper.GetDecimalFromDataReader(oDrd("TipoIva")), SQLHelper.GetAmtFromDataReader(oDrd("QuotaIva")))
                    .IvaBaseQuotas.Add(oSujeta)
                End If
                If Not IsDBNull(oDrd("BaseSujeta2")) Then
                    Dim oSujeta2 As New DTOBaseQuota(SQLHelper.GetAmtFromDataReader(oDrd("BaseSujeta2")), SQLHelper.GetDecimalFromDataReader(oDrd("TipoIva2")), SQLHelper.GetAmtFromDataReader(oDrd("QuotaIva2")))
                    .IvaBaseQuotas.Add(oSujeta2)
                End If
                If Not IsDBNull(oDrd("BaseExenta")) Then
                    Dim oExenta As New DTOBaseQuota(SQLHelper.GetAmtFromDataReader(oDrd("BaseExenta")))
                    .IvaBaseQuotas.Add(oExenta)
                    .ClaveExenta = SQLHelper.GetStringFromDataReader(oDrd("ClaveExenta"))
                End If
                If Not IsDBNull(oDrd("BaseIrpf")) Then
                    .IrpfBaseQuota = New DTOBaseQuota(SQLHelper.GetAmtFromDataReader(oDrd("BaseIrpf")), SQLHelper.GetDecimalFromDataReader(oDrd("TipoIrpf")))
                End If
                .SiiLog = SQLHelper.GetSiiLogFromDataReader(oDrd)
            End With

            retval.Add(oBookFra)


        Loop
        oDrd.Close()

        Return retval
    End Function

    Shared Function Find(NifEmisor As String, Num As String, Year As Integer) As List(Of DTOBookFra)
        Dim retval As New List(Of DTOBookFra)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT BookFras.CcaGuid, BookFras.ContactGuid, BookFras.CtaGuid, BookFras.FraNum ")
        sb.AppendLine(", BookFras.ClaveExenta, BookFras.TipoFra, BookFras.ClaveRegimenEspecialOTrascendencia, BookFras.Dsc ")
        sb.AppendLine(", BookFras.BaseSujeta, BookFras.TipoIva, BookFras.QuotaIva, BookFras.BaseExenta ")
        sb.AppendLine(", BookFras.BaseIrpf, BookFras.TipoIrpf ")
        sb.AppendLine(", BookFras.SiiResult, BookFras.SiiFch, BookFras.SiiCsv, BookFras.SiiErr ")
        sb.AppendLine(", Cca.Cca, Cca.fch, Cca.Hash ")
        sb.AppendLine(", CliGral.RaoSocial, CliGral.FullNom ")
        sb.AppendLine(", CliGral.NIF, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod ")
        sb.AppendLine(", VwAddress.Adr, VwAddress.ZipGuid, VwAddress.ZipCod, VwAddress.LocationGuid, VwAddress.LocationNom ")
        sb.AppendLine(", VwAddress.ProvinciaGuid, VwAddress.ZonaGuid, VwAddress.ProvinciaNom, VwAddress.CountryGuid, VwAddress.CountryEsp, VwAddress.CountryISO, VwAddress.CEE ")
        sb.AppendLine(", PgcCta.Id as CtaId, PgcCta.Esp as CtaEsp, PgcCta.Cat as CtaCat, PgcCta.Eng as CtaEng ")
        sb.AppendLine("FROM            BookFras ")
        sb.AppendLine("INNER JOIN Cca ON BookFras.CcaGuid = CCA.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON BookFras.ContactGuid = CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwAddress ON BookFras.ContactGuid = VwAddress.SrcGuid ")
        sb.AppendLine("LEFT OUTER JOIN PgcCta ON BookFras.CtaGuid = PgcCta.Guid ")
        sb.AppendLine("WHERE CliGral.Nif='" & NifEmisor & "' AND BookFras.FraNum='" & Num & "' AND Year(Cca.Fch)=" & Year & " ")
        sb.AppendLine("ORDER BY Cca.fch ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oCca As New DTOCca(oDrd("CcaGuid"))
            With oCca
                .Id = oDrd("Cca")
                .Fch = oDrd("Fch")
                .DocFile = SQLHelper.GetDocFileFromDataReaderHash(oDrd("Hash"))
            End With

            Dim oCta As DTOPgcCta = Nothing
            If Not IsDBNull(oDrd("CtaGuid")) Then
                oCta = New DTOPgcCta(DirectCast(oDrd("CtaGuid"), Guid))
                oCta.Id = oDrd("CtaId")
                oCta.Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "CtaEsp", "CtaCat", "CtaEng")
            End If

            Dim oContact As DTOContact = Nothing
            If Not IsDBNull(oDrd("ContactGuid")) Then
                oContact = New DTOContact(DirectCast(oDrd("ContactGuid"), Guid))
                oContact.Nom = oDrd("RaoSocial")
                oContact.Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
                oContact.FullNom = oDrd("FullNom")
                oContact.Address = SQLHelper.GetAddressFromDataReader(oDrd)
            End If

            Dim oBookFra As New DTOBookFra(oCca)
            With oBookFra
                .TipoFra = SQLHelper.GetStringFromDataReader(oDrd("TipoFra"))
                .ClaveRegimenEspecialOTrascendencia = SQLHelper.GetStringFromDataReader(oDrd("ClaveRegimenEspecialOTrascendencia"))
                .FraNum = oDrd("FraNum").ToString
                .Cta = oCta
                .Contact = oContact
                .dsc = SQLHelper.GetStringFromDataReader(oDrd("Dsc"))
                .IvaBaseQuotas = New List(Of DTOBaseQuota)
                If Not IsDBNull(oDrd("BaseSujeta")) Then
                    Dim oSujeta As New DTOBaseQuota(SQLHelper.GetAmtFromDataReader(oDrd("BaseSujeta")), SQLHelper.GetDecimalFromDataReader(oDrd("TipoIva")), SQLHelper.GetAmtFromDataReader(oDrd("QuotaIva")))
                    .IvaBaseQuotas.Add(oSujeta)
                End If
                If Not IsDBNull(oDrd("BaseExenta")) Then
                    Dim oExenta As New DTOBaseQuota(SQLHelper.GetAmtFromDataReader(oDrd("BaseExenta")))
                    .IvaBaseQuotas.Add(oExenta)
                    .ClaveExenta = SQLHelper.GetStringFromDataReader(oDrd("ClaveExenta"))
                End If
                If Not IsDBNull(oDrd("BaseIrpf")) Then
                    .IrpfBaseQuota = New DTOBaseQuota(SQLHelper.GetAmtFromDataReader(oDrd("BaseIrpf")), SQLHelper.GetDecimalFromDataReader(oDrd("TipoIrpf")))
                End If
                .SiiLog = SQLHelper.GetSiiLogFromDataReader(oDrd)
                .IsLoaded = True
            End With

            retval.Add(oBookFra)
        Loop

        oDrd.Close()
        Return retval
    End Function
End Class



