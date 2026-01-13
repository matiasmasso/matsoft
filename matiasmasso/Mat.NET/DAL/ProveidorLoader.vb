Public Class ProveidorLoader

#Region "CRUD"

    Shared Function Exists(oGuid As Guid) As Boolean
        Dim SQL As String = "SELECT Guid FROM CliPrv WHERE Guid='" & oGuid.ToString & "'"
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim retval As Boolean = oDrd.Read
        oDrd.Close()
        Return retval
    End Function

    Shared Function Find(oGuid As Guid) As DTOProveidor
        Dim retval As DTOProveidor = Nothing
        Dim oProveidor As New DTOProveidor(oGuid)
        If Load(oProveidor) Then
            retval = oProveidor
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oProveidor As DTOProveidor) As Boolean
        Dim retval As Boolean
        If oProveidor IsNot Nothing Then
            If Not oProveidor.IsLoaded And Not oProveidor.IsNew Then

                Dim sb As New System.Text.StringBuilder
                sb.AppendLine("SELECT CliPrv.* ")
                sb.AppendLine(", CliGral.Emp, CliGral.RaoSocial, CliGral.NomCom, CliGral.LangId, CliGral.Rol, CliGral.Gln ")
                sb.AppendLine(", CliGral.NIF, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod ")
                sb.AppendLine(", VwAddress.* ")
                sb.AppendLine(", Iban.Guid as IbanGuid, Iban.Ccc ")
                sb.AppendLine(", PgcCta.Id AS CtaId, PgcCta.Esp AS CtaEsp ")
                sb.AppendLine(", PgcCta2.Id AS Cta2Id, PgcCta2.Esp AS Cta2Esp ")
                sb.AppendLine("FROM CliPrv ")
                sb.AppendLine("INNER JOIN CliGral ON CliPrv.Guid = CliGral.Guid ")
                sb.AppendLine("INNER JOIN VwAddress ON CliGral.Guid = VwAddress.SrcGuid ")
                sb.AppendLine("LEFT OUTER JOIN PgcCta ON CliPrv.CtaCarrec=PgcCta.Guid ")
                sb.AppendLine("LEFT OUTER JOIN PgcCta AS PgcCta2 ON CliPrv.CtaCreditora=PgcCta2.Guid ")
                sb.AppendLine("LEFT OUTER JOIN Iban ON CliPrv.Guid=Iban.ContactGuid AND Iban.Cod=" & CInt(DTOIban.Cods.proveidor) & " AND (Mandato_Fch IS NULL OR Mandato_Fch<=GETDATE()) AND (Caduca_Fch IS NULL OR Caduca_Fch>=GETDATE()) ")
                sb.AppendLine("WHERE CliPrv.Guid='" & oProveidor.Guid.ToString & "' ")


                'SELECT Guid, ContactGuid, COD, CCC, Mandato_Fch, Caduca_Fch From Iban

                Dim SQL As String = sb.ToString
                Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
                If oDrd.Read Then
                    With oProveidor
                        .Emp = New DTOEmp(oDrd("Emp"))
                        .Nom = SQLHelper.GetStringFromDataReader(oDrd("RaoSocial"))
                        .NomComercial = SQLHelper.GetStringFromDataReader(oDrd("NomCom"))
                        If .FullNom = "" Then
                            .FullNom = oDrd("RaoSocial")
                            If oDrd("NomCom") > "" Then
                                .FullNom = .FullNom & " '" & oDrd("NomCom") & "'"
                            End If
                        End If
                        .Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
                        .Lang = DTOLang.Factory(oDrd("LangId").ToString())
                        .Rol = New DTORol(oDrd("Rol"))
                        .Address = SQLHelper.GetAddressFromDataReader(oDrd)
                        .GLN = SQLHelper.GetEANFromDataReader(oDrd("Gln"))
                        If IsDBNull(oDrd("Cur")) Then
                            .Cur = DTOApp.Current.Cur
                        Else
                            .Cur = DTOCur.Factory(oDrd("Cur"))
                        End If
                        If Not IsDBNull(oDrd("CtaCarrec")) Then
                            .defaultCtaCarrec = New DTOPgcCta(oDrd("CtaCarrec"))
                            With .defaultCtaCarrec
                                .Id = oDrd("CtaId")
                                .Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "CtaEsp")
                            End With
                        End If
                        If Not IsDBNull(oDrd("CtaCreditora")) Then
                            .defaultCtaCreditora = New DTOPgcCta(oDrd("CtaCreditora"))
                            With .defaultCtaCreditora
                                .Id = oDrd("Cta2Id")
                                .Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "Cta2Esp")
                            End With
                        End If
                        .IRPF_Cod = oDrd("CodIrpf")
                        .IncoTerm = SQLHelper.GetIncotermFromDataReader(oDrd("IncoTerm"))
                        .paymentTerms = New DTOPaymentTerms
                        With .PaymentTerms
                            .Cod = oDrd("Cfp")
                            .Months = oDrd("Mes")
                            .PaymentDayCod = oDrd("DpgWeek")
                            .PaymentDays = CustomerLoader.GetPaymentDaysFromDataReader(oDrd("PaymentDays"))
                            .Vacaciones = CustomerLoader.DecodedVacacions(oDrd("Vacaciones"))
                            '.CustomerIban = CustomerIban.DefaultFromContact(Me, Tipus.Proveidor)
                            If Not IsDBNull(oDrd("IbanGuid")) Then
                                .Iban = New DTOIban(oDrd("IbanGuid"))
                                .Iban.Digits = oDrd("Ccc")
                            End If
                        End With
                        .CodStk = oDrd("CodStk")

                        .IsLoaded = True
                    End With
                End If

                oDrd.Close()
            End If

            retval = oProveidor.IsLoaded
        End If
        Return retval
    End Function

    Shared Function Update(oProveidor As DTOProveidor, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oProveidor, oTrans)
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


    Shared Sub Update(oProveidor As DTOProveidor, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM CliPrv ")
        sb.AppendLine("WHERE Guid='" & oProveidor.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oProveidor.Guid
            'oRow("Emp") = oProveidor.Emp.Id 'TO DEPRECATE
            'oRow("Cli") = 0 'TO DEPRECATE
        Else
            oRow = oTb.Rows(0)
        End If

        With oProveidor
            'TODO: CommercialMargin
            oRow("CtaCarrec") = SQLHelper.NullableBaseGuid(.defaultCtaCarrec)
            oRow("CtaCreditora") = SQLHelper.NullableBaseGuid(.defaultCtaCreditora)
            If .Cur IsNot Nothing Then
                oRow("Cur") = .Cur.Tag
            End If
            oRow("CodIrpf") = .IRPF_Cod
            oRow("Incoterm") = SQLHelper.NullableIncoterm(.IncoTerm)

            With .PaymentTerms
                oRow("Cfp") = .Cod
                oRow("Mes") = .Months
                oRow("DpgWeek") = .PaymentDayCod
                oRow("PaymentDays") = CustomerLoader.NullablePaymentDays(.PaymentDays)
                oRow("Vacaciones") = CustomerLoader.EncodedVacacions(.Vacaciones)
            End With

        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oProveidor As DTOProveidor, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oProveidor, oTrans)
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


    Shared Sub Delete(oProveidor As DTOContact, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE CliPrv WHERE Guid='" & oProveidor.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

    Shared Function CheckFacturaAlreadyExists(oProveidor As DTOContact, ByVal oExercici As DTOExercici, ByVal sFraNum As String) As DTOCca
        Dim retval As DTOCca = Nothing
        If sFraNum > "" Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT  Cca.Guid, Cca.Txt ")
            sb.AppendLine("FROM BookFras ")
            sb.AppendLine("INNER JOIN Cca ON BookFras.CcaGuid = Cca.Guid ")
            sb.AppendLine("WHERE BookFras.ContactGuid='" & oProveidor.Guid.ToString & "' ")
            sb.AppendLine("AND Cca.Yea = " & oExercici.Year & " ")
            sb.AppendLine("AND BookFras.FraNum = '" & sFraNum & "' ")
            sb.AppendLine("ORDER BY Cca.Yea DESC, Cca.Cca DESC")
            Dim SQL As String = sb.ToString

            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)

            If oDrd.Read Then
                retval = CcaLoader.Find(oDrd("Guid"))
            End If
            oDrd.Close()
        End If

        Return retval
    End Function


    Shared Function SaveFactura(exs As List(Of Exception), ByRef oCca As DTOCca, oPnds As IEnumerable(Of DTOPnd), Optional oImportacio As DTOImportacio = Nothing) As Boolean
        Dim retval As Boolean


        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            CcaLoader.Update(oCca, oTrans)

            If oPnds IsNot Nothing Then
                For Each oPnd In oPnds
                    DAL.PndLoader.Update(oPnd, oTrans)
                Next
            End If

            If oImportacio IsNot Nothing Then
                DAL.ImportacioLoader.Update(oImportacio, oTrans)
            End If

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
