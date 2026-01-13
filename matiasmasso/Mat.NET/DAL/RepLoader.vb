Public Class RepLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTORep
        Dim retval As DTORep = Nothing
        Dim oRep As New DTORep(oGuid)
        If Load(oRep) Then
            retval = oRep
        End If
        Return retval
    End Function

    Shared Function Exists(oContact As DTOContact) As Boolean
        Dim SQL As String = "SELECT Guid FROM CliRep WHERE Guid='" & oContact.Guid.ToString & "'"
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim retval As Boolean = oDrd.Read
        oDrd.Close()
        Return retval
    End Function

    Shared Function Load(ByRef oRep As DTORep) As Boolean
        If Not oRep.IsLoaded And Not oRep.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT CliGral.Emp, CliGral.RaoSocial, CliGral.LangId ")
            sb.AppendLine(", CliGral.NIF, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod ")
            sb.AppendLine(", CliRep.Abr, CliRep.Description, CliRep.Desde, CliRep.Hasta, CliRep.DisableLiqs, CliRep.ComStd, CliRep.ComRed, CliRep.IVA, CliRep.IRPF, CliRep.IrpfTipus, CliRep.CcxGuid ")
            sb.AppendLine(", VwAddress.* ")
            sb.AppendLine(", VwIban.* ")
            sb.AppendLine("FROM CliRep ")
            sb.AppendLine("INNER JOIN CliGral ON CliRep.Guid = CliGral.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwAddress ON CliRep.Guid = VwAddress.SrcGuid ")
            sb.AppendLine("LEFT OUTER JOIN VwIban ON CliRep.Guid = VwIban.IbanContactGuid AND VwIban.IbanCod =" & DTOIban.Cods.proveidor & " AND VwIban.IbanCaducaFch IS NULL ")
            sb.AppendLine("WHERE CliRep.Guid='" & oRep.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oRep
                    .Emp = New DTOEmp(oDrd("Emp"))
                    .Nom = oDrd("RaoSocial")
                    .NickName = oDrd("Abr")
                    .lang = SQLHelper.GetLangFromDataReader(oDrd("LangId"))
                    .FchAlta = SQLHelper.GetFchFromDataReader(oDrd("Desde"))
                    .FchBaja = SQLHelper.GetFchFromDataReader(oDrd("Hasta"))
                    .DisableLiqs = SQLHelper.GetBooleanFromDatareader(oDrd("DisableLiqs"))
                    .ComisionStandard = SQLHelper.GetDecimalFromDataReader(oDrd("ComStd"))
                    .ComisionReducida = SQLHelper.GetDecimalFromDataReader(oDrd("ComRed"))
                    .IvaCod = SQLHelper.GetIntegerFromDataReader(oDrd("IVA"))
                    .IrpfCod = SQLHelper.GetIntegerFromDataReader(oDrd("IRPF"))
                    .IrpfCustom = SQLHelper.GetDecimalFromDataReader(oDrd("IrpfTipus"))
                    .Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
                    .Address = SQLHelper.GetAddressFromDataReader(oDrd)
                    .Iban = SQLHelper.getIbanFromDataReader(oDrd)
                    .Description = SQLHelper.GetStringFromDataReader(oDrd("Description"))
                    If Not IsDBNull(oDrd("CcxGuid")) Then
                        .raoSocialFacturacio = New DTOProveidor(DirectCast(oDrd("CcxGuid"), Guid))
                    End If

                    .IsLoaded = True
                End With
            End If

            oDrd.Close()

            If oRep.RaoSocialFacturacio IsNot Nothing Then
                ProveidorLoader.Load(oRep.RaoSocialFacturacio)
            End If
            oRep.tels = ContactLoader.Tels(oRep)
            oRep.emails = UsersLoader.All(oRep, True)
        End If

        Dim retval As Boolean = oRep.IsLoaded
        Return retval
    End Function

    Shared Function Update(oRep As DTORep, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oRep, oTrans)
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

    Shared Sub Update(oRep As DTORep, ByRef oTrans As SqlTransaction)
        'If oRep.IsNew Then ContactLoader.Update(oRep, oTrans)
        UpdateRep(oRep, oTrans)
        If oRep.RepProducts IsNot Nothing Then
            RepProductsLoader.Update(oRep.RepProducts, oTrans)
        End If
    End Sub


    Shared Sub UpdateRep(oRep As DTORep, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM CliRep ")
        sb.AppendLine("WHERE Guid='" & oRep.Guid.ToString & "'")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oRep.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oRep
            oRow("Abr") = SQLHelper.NullableString(.NickName)
            oRow("Desde") = SQLHelper.NullableFch(.FchAlta)
            oRow("Hasta") = SQLHelper.NullableFch(.FchBaja)
            oRow("DisableLiqs") = .DisableLiqs
            oRow("ComStd") = .ComisionStandard
            oRow("ComRed") = .ComisionReducida
            oRow("IVA") = .IvaCod
            oRow("IRPF") = .IrpfCod
            oRow("IrpfTipus") = .IrpfCustom
            If .RaoSocialFacturacio Is Nothing Then
                oRow("CcxGuid") = System.DBNull.Value
            Else
                oRow("CcxGuid") = .RaoSocialFacturacio.Guid
            End If
            oRow("Foto") = .Foto
            oRow("Description") = SQLHelper.NullableString(.Description)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oRep As DTORep, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oRep, oTrans)
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


    Shared Sub Delete(oRep As DTOContact, ByRef oTrans As SqlTransaction)
        DeleteRepProducts(oRep, oTrans)
        DeleteRep(oRep, oTrans)
    End Sub

    Shared Sub DeleteRep(oRep As DTOContact, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE CliRep WHERE Guid = '" & oRep.Guid.ToString & "'"
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteRepProducts(oRep As DTOContact, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE RepProducts WHERE Rep = '" & oRep.Guid.ToString & "'"
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub


#End Region

    Shared Function Archive(oRep As DTORep) As List(Of DTOPurchaseOrder)
        Dim retval As New List(Of DTOPurchaseOrder)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Pnc.Guid AS PncGuid, Pnc.Qty As PncQty, Pnc.Eur As PncEur, Pnc.Dto As PncDto, Pnc.Com ")
        sb.AppendLine(" , Pnc.PdcGuid, Pdc.Pdc As PdcId, Pdc.Fch As PdcFch, Pdc.FchCreated As PdcFchCreated, Pdc.Pdd, Pdc.CliGuid, Pdc.Cod AS PdcCod, CliGral.FullNom ")
        sb.AppendLine(" , Arc.Guid As ArcGuid, Arc.Qty As ArcQty, Arc.Eur As ArcEur, Arc.Dto As ArcDto ")
        sb.AppendLine(" , Arc.AlbGuid, Alb.Alb, Alb.Fch As AlbFch ")
        sb.AppendLine(" , Alb.FraGuid, Fra.Fra, Fra.Fch As FraFch ")
        sb.AppendLine(" , Pnc.ArtGuid, VwSkuNom.SkuNomLlargEsp ")
        sb.AppendLine(" , Rps.RepLiqGuid, RepLiq.Id As RepLiqId, RepLiq.Fch AS RepLiqFch ")
        sb.AppendLine("FROM Pnc ")
        sb.AppendLine("INNER Join Pdc ON Pnc.PdcGuid = Pdc.Guid ")
        sb.AppendLine("INNER Join CliGral ON Pdc.CliGuid = CliGral.Guid ")
        sb.AppendLine("INNER JOIN VwSkuNom ON Pnc.ArtGuid = VwSkuNom.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN Arc ON Pnc.Guid = Arc.PncGuid ")
        sb.AppendLine("LEFT OUTER JOIN Alb ON Arc.AlbGuid = Alb.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Fra ON Alb.FraGuid = Fra.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Rps ON Fra.Guid = Rps.FraGuid ")
        sb.AppendLine("LEFT OUTER JOIN RepLiq ON Rps.RepliqGuid=RepLiq.Guid ")
        sb.AppendLine("WHERE Pnc.RepGuid = '" & oRep.Guid.ToString & "' ")
        sb.AppendLine("AND Year(Pdc.Fch)>" & DTO.GlobalVariables.Today().Year - 2 & " ")
        sb.AppendLine("ORDER BY Pdc.FchCreated DESC, Pdc.Guid, Pnc.Lin, Alb.fch, Alb.Alb, Arc.Lin")
        Dim SQL As String = sb.ToString
        Dim oOrder As New DTOPurchaseOrder
        Dim oOrderItem As New DTOPurchaseOrderItem
        Dim oDelivery As New DTODelivery
        Dim oDeliveryItem As New DTODeliveryItem
        Dim oInvoice As New DTOInvoice
        Dim oRepLiq As New DTORepLiq
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oOrder.Guid.Equals(oDrd("PdcGuid")) Then
                oOrder = New DTOPurchaseOrder(oDrd("PdcGuid"))
                With oOrder
                    .Cod = oDrd("PdcCod")
                    .Fch = oDrd("PdcFch")
                    .UsrLog.FchCreated = oDrd("PdcFchCreated")
                    .Num = oDrd("PdcId")
                    .Customer = New DTOCustomer(oDrd("CliGuid"))
                    .Customer.FullNom = oDrd("FullNom")
                    .Concept = oDrd("Pdd")
                End With

                retval.Add(oOrder)
            End If

            If Not oOrderItem.Guid.Equals(oDrd("PncGuid")) Then
                oOrderItem = New DTOPurchaseOrderItem(oDrd("PncGuid"))
                With oOrderItem
                    .PurchaseOrder = oOrder
                    .Qty = oDrd("PncQty")
                    .Price = DTOAmt.Factory(oDrd("PncEur"))
                    .Dto = oDrd("PncDto")
                    .Sku = New DTOProductSku(oDrd("ArtGuid"))
                    SQLHelper.LoadLangTextFromDataReader(.Sku.NomLlarg, oDrd, "SkuNomLlargEsp", "SkuNomLlargEsp", "SkuNomLlargEsp", "SkuNomLlargEsp")
                    .RepCom = New DTORepCom(oRep, oDrd("Com"))
                    .Deliveries = New List(Of DTODeliveryItem)
                End With
                oOrder.Items.Add(oOrderItem)
            End If


            If Not IsDBNull(oDrd("ArcGuid")) Then
                If Not oDeliveryItem.Guid.Equals(oDrd("ArcGuid")) Then

                    If Not oDelivery.Guid.Equals(oDrd("AlbGuid")) Then
                        If IsDBNull(oDrd("FraGuid")) Then
                            oInvoice = Nothing
                        Else
                            If oInvoice Is Nothing OrElse Not oInvoice.Guid.Equals(oDrd("FraGuid")) Then

                                If IsDBNull(oDrd("RepLiqGuid")) Then
                                    oRepLiq = Nothing
                                Else
                                    If oRepLiq Is Nothing OrElse Not oRepLiq.Guid.Equals(oDrd("RepLiqGuid")) Then
                                        oRepLiq = New DTORepLiq(oDrd("RepLiqGuid"))
                                        With oRepLiq
                                            .Id = oDrd("RepLiqId")
                                            .Fch = oDrd("RepLiqFch")
                                        End With
                                    End If
                                End If

                                oInvoice = New DTOInvoice(oDrd("FraGuid"))
                                With oInvoice
                                    .Num = oDrd("Fra")
                                    .Fch = oDrd("FraFch")
                                End With

                            End If
                        End If

                        oDelivery = New DTODelivery(oDrd("AlbGuid"))
                        With oDelivery
                            .Id = oDrd("Alb")
                            .Fch = oDrd("AlbFch")
                            .Invoice = oInvoice
                        End With
                    End If

                    oDeliveryItem = New DTODeliveryItem(oDrd("ArcGuid"))
                    With oDeliveryItem
                        .Delivery = oDelivery
                        .Qty = oDrd("ArcQty")
                        .Price = DTOAmt.Factory(oDrd("ArcEur"))
                        .Dto = oDrd("ArcDto")
                        .RepLiqs = New List(Of DTORepLiq)
                        .RepLiq = oRepLiq
                    End With
                    oOrderItem.Deliveries.Add(oDeliveryItem)
                End If


            End If
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class

Public Class RepsLoader

    Shared Function All(oEmp As DTOEmp, Optional Active As Boolean = True, Optional IncludeLogo As Boolean = False) As List(Of DTORep)
        Dim retval As New List(Of DTORep)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CliRep.* ")
        sb.AppendLine(", CliGral.RaoSocial, CliGral.Rol ")
        sb.AppendLine(", CliGral.NIF, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod ")
        sb.AppendLine(", VwAddress.* ")
        sb.AppendLine(", VwIban.* ")
        If IncludeLogo Then
            sb.AppendLine(", Clx.Img48 ")
        End If
        sb.AppendLine("FROM CliRep ")
        sb.AppendLine("INNER JOIN CliGral ON CliRep.Guid = CliGral.Guid ")
        If IncludeLogo Then
            sb.AppendLine("LEFFT OUTER JOIN Clx ON CliRep.Guid = Clx.Guid ")
        End If
        sb.AppendLine("LEFT OUTER JOIN VwAddress ON CliRep.Guid = VwAddress.SrcGuid ")
        sb.AppendLine("LEFT OUTER JOIN VwIban ON CliRep.Guid = VwIban.IbanContactGuid AND VwIban.IbanCod =" & DTOIban.Cods.proveidor & " AND VwIban.IbanCaducaFch IS NULL ")
        sb.AppendLine("WHERE CliGral.Emp=" & oEmp.Id & " ")
        If Active Then
            'sb.AppendLine("AND Desde <= GETDATE() ")
            sb.AppendLine("AND (CliRep.Hasta IS NULL OR CliRep.Hasta >= GETDATE()) ")
            sb.AppendLine("AND CliRep.DisableLiqs = 0 ")
        End If
        sb.AppendLine("ORDER BY CliRep.Abr")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTORep(oDrd("Guid"))
            With item
                .Nom = oDrd("RaoSocial")
                .FullNom = oDrd("RaoSocial")
                .Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
                .NickName = IIf(oDrd("Abr") = "", .Nom, oDrd("Abr"))
                .FchAlta = SQLHelper.GetFchFromDataReader(oDrd("Desde"))
                .FchBaja = SQLHelper.GetFchFromDataReader(oDrd("Hasta"))
                .DisableLiqs = oDrd("DisableLiqs")
                .ComisionStandard = oDrd("ComStd")
                .ComisionReducida = oDrd("ComRed")
                .IvaCod = oDrd("IVA")
                .IrpfCod = oDrd("IRPF")
                .IrpfCustom = oDrd("IrpfTipus")
                .Rol = New DTORol(SQLHelper.GetIntegerFromDataReader(oDrd("Rol")))
                .Address = SQLHelper.GetAddressFromDataReader(oDrd)
                .Iban = SQLHelper.getIbanFromDataReader(oDrd)
                .Description = SQLHelper.GetStringFromDataReader(oDrd("Description"))
                If IncludeLogo Then
                    If Not IsDBNull(oDrd("Img48")) Then
                        Dim oImg48 = LegacyHelper.ImageHelper.FromBytes(oDrd("Img48"))
                        If Not (oImg48.Height = 48 And oImg48.Width = 48) Then
                            oImg48 = LegacyHelper.ImageHelper.GetThumbnailToFill(oImg48, 48, 48)
                        End If
                        .Img48 = oImg48.Bytes()
                    End If
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oProduct As DTOProduct) As List(Of DTORep)
        Dim retval As New List(Of DTORep)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT RepProducts.Rep, CliRep.Abr ")
        sb.AppendLine("FROM RepProducts ")
        sb.AppendLine("INNER JOIN CliRep ON RepProducts.Rep=CliRep.Guid ")
        sb.AppendLine("INNER JOIN VwProductParent ON RepProducts.Product = VwProductParent.Child ")
        sb.AppendLine("WHERE RepProducts.Product='" & oProduct.Guid.ToString & "' ")
        sb.AppendLine("AND (RepProducts.FchFrom IS NULL OR RepProducts.FchFrom<=GETDATE()) ")
        sb.AppendLine("AND (RepProducts.FchTo IS NULL OR RepProducts.FchTo>GETDATE()) ")
        sb.AppendLine("GROUP BY RepProducts.Rep, CliRep.Abr ")
        sb.AppendLine("ORDER BY CliRep.Abr")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oRep As New DTORep(DirectCast(oDrd("Rep"), Guid))
            oRep.NickName = oDrd("Abr")
            retval.Add(oRep)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oArea As DTOArea) As List(Of DTORep)
        Dim retval As New List(Of DTORep)
        Dim sb As New System.Text.StringBuilder

        sb.Append("SELECT  CliRep.Guid, CliRep.Abr ")
        sb.Append("FROM            RepProducts ")
        sb.Append("INNER JOIN VwAreaParent ON RepProducts.Area = VwAreaParent.ParentGuid ")
        sb.Append("INNER JOIN CliRep ON RepProducts.Rep = CliRep.Guid ")
        sb.Append("WHERE VwAreaParent.ChildGuid='" & oArea.Guid.ToString & "' ")
        sb.Append("AND (RepProducts.FchFrom < GETDATE()) ")
        sb.Append("AND (RepProducts.FchTo IS NULL OR RepProducts.FchTo > GETDATE()) ")
        sb.Append("AND RepProducts.Cod = 1 ")
        sb.Append("GROUP BY CliRep.Guid, CliRep.Abr ")
        sb.Append("ORDER BY CliRep.Abr")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oRep As New DTORep(oDrd("Guid"))
            oRep.NickName = oDrd("Abr")
            retval.Add(oRep)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Emails(oArea As DTOArea, oChannel As DTODistributionChannel) As List(Of DTOEmail)
        Dim retval As New List(Of DTOEmail)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Email_Clis.EmailGuid, Email.Adr ")
        sb.AppendLine("FROM RepProducts ")
        sb.AppendLine("INNER JOIN CliRep ON RepProducts.Rep=CliRep.Guid ")
        sb.AppendLine("INNER JOIN Email_Clis ON CliRep.Guid = Email_Clis.ContactGuid ")
        sb.AppendLine("INNER JOIN Email ON Email_Clis.EmailGuid = Email.Guid ")
        sb.AppendLine("INNER JOIN VwAreaParent ON RepProducts.Area = VwAreaParent.ParentGuid ")
        sb.AppendLine("WHERE VwAreaParent.ChildGuid='" & oArea.Guid.ToString & "' ")
        sb.AppendLine("AND RepProducts.DistributionChannel = '" & oChannel.Guid.ToString & "' ")
        sb.AppendLine("AND (RepProducts.FchFrom IS NULL OR RepProducts.FchFrom<=GETDATE()) ")
        sb.AppendLine("AND (RepProducts.FchTo IS NULL OR RepProducts.FchTo>GETDATE()) ")
        sb.AppendLine("AND Email.Privat=0 ")
        sb.AppendLine("AND Email.Obsoleto=0 ")
        sb.AppendLine("GROUP BY Email_Clis.EmailGuid, Email.Adr ")
        sb.AppendLine("ORDER BY Email.Adr")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oEmail As New DTOEmail(DirectCast(oDrd("EmailGuid"), Guid))
            oEmail.EmailAddress = oDrd("Adr")
            retval.Add(oEmail)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Emails(oEmp As DTOEmp) As List(Of DTOEmail)
        Dim retval As New List(Of DTOEmail)
        Dim sb As New System.Text.StringBuilder

        sb.AppendLine("SELECT Email_Clis.EmailGuid, Email.Adr ")
        sb.AppendLine("FROM Email ")
        sb.AppendLine("INNER JOIN Email_Clis ON Email.Guid = Email_Clis.EmailGuid ")
        sb.AppendLine("INNER JOIN CliRep ON Email_Clis.ContactGuid=CliRep.Guid ")
        sb.AppendLine("WHERE Email.Emp =" & oEmp.Id & " ")
        sb.AppendLine("AND (CliRep.Desde IS NULL OR CliRep.Desde <=GETDATE()) ")
        sb.AppendLine("AND (CliRep.Hasta IS NULL OR CliRep.Hasta >GETDATE()) ")
        sb.AppendLine("AND Email.Privat=0 ")
        sb.AppendLine("AND Email.Obsoleto=0 ")
        sb.AppendLine("GROUP BY Email_Clis.EmailGuid, Email.Adr ")
        sb.AppendLine("ORDER BY Email.Adr")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oEmail As New DTOEmail(DirectCast(oDrd("EmailGuid"), Guid))
            oEmail.EmailAddress = oDrd("Adr")
            retval.Add(oEmail)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Emails(oProduct As DTOProduct) As List(Of DTOEmail)
        Dim retval As New List(Of DTOEmail)
        Dim sb As New System.Text.StringBuilder

        sb.AppendLine("SELECT Email_Clis.EmailGuid, Email.Adr ")
        sb.AppendLine("FROM RepProducts ")
        sb.AppendLine("INNER JOIN CliRep ON RepProducts.Rep=CliRep.Guid ")
        sb.AppendLine("INNER JOIN Email_Clis ON CliRep.Guid = Email_Clis.ContactGuid ")
        sb.AppendLine("INNER JOIN Email ON Email_Clis.EmailGuid = Email.Guid ")
        sb.AppendLine("INNER JOIN VwProductParent ON RepProducts.Product = VwProductParent.Parent ")
        sb.AppendLine("WHERE VwProductParent.Child ='" & oProduct.Guid.ToString & "' ")
        sb.AppendLine("AND (RepProducts.FchFrom IS NULL OR RepProducts.FchFrom<=GETDATE()) ")
        sb.AppendLine("AND (RepProducts.FchTo IS NULL OR RepProducts.FchTo>GETDATE()) ")
        sb.AppendLine("AND Email.Privat=0 ")
        sb.AppendLine("AND Email.Obsoleto=0 ")
        sb.AppendLine("GROUP BY Email_Clis.EmailGuid, Email.Adr ")
        sb.AppendLine("ORDER BY Email.Adr")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oEmail As New DTOEmail(DirectCast(oDrd("EmailGuid"), Guid))
            oEmail.EmailAddress = oDrd("Adr")
            retval.Add(oEmail)
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
        sb.AppendLine("INNER JOIN CliGral ON Iban.ContactGuid=CliGral.Guid AND Iban.Cod=" & CInt(DTOIban.Cods.proveidor) & " ")
        sb.AppendLine("LEFT OUTER JOIN Bn2 ON Iban.BankBranch=Bn2.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Bn1 ON Bn2.Bank=Bn1.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Location ON Bn2.Location=Location.Guid ")
        sb.AppendLine("WHERE Mandato_Fch Is NULL Or Mandato_Fch<=GETDATE() ")
        sb.AppendLine("And Caduca_Fch IS NULL OR Caduca_Fch>=GETDATE() ")
        sb.AppendLine("ORDER BY Iban.ContactGuid")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oRep As New DTORep(oDrd("ContactGuid"))
            oRep.Nom = oDrd("RaoSocial")

            Dim item As New DTOIban(oDrd("Guid"))
            With item
                .Titular = oRep
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
        Dim oCtaComissions As DTOPgcCta = PgcCtaLoader.FromCod(DTOPgcPlan.Ctas.ComisionsRepresentants, oExercici)
        Dim retval As New List(Of DTOPgcSaldo)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Ccb.ContactGuid, CliGral.RaoSocial, CliGral.FullNom ")
        sb.AppendLine(", SUM(CASE WHEN dh = 1 THEN Ccb.Eur ELSE 0 END) AS Deb ")
        sb.AppendLine(", SUM(CASE WHEN dh = 2 THEN Ccb.EUR ELSE 0 END) AS Hab ")
        sb.AppendLine("FROM Ccb ")
        sb.AppendLine("INNER JOIN Cca ON Ccb.CcaGuid = Cca.Guid ")
        sb.AppendLine("INNER JOIN CliGral ON Ccb.ContactGuid = CliGral.Guid ")
        sb.AppendLine("INNER JOIN (SELECT ContactGuid FROM Ccb WHERE CtaGuid='" & oCtaComissions.Guid.ToString & "' GROUP BY Ccb.ContactGuid) Reps ON Ccb.ContactGuid = Reps.ContactGuid ")
        sb.AppendLine("INNER JOIN PgcCta ON Ccb.CtaGuid = PgcCta.Guid AND (PgcCta.Id LIKE '43%' OR PgcCta.Id LIKE '40%' OR PgcCta.Id LIKE '41%' ) ")
        sb.AppendLine("WHERE Cca.Emp=" & oExercici.Emp.Id & " AND Year(Cca.Fch)=" & oExercici.Year & " ")
        sb.AppendLine("GROUP BY Ccb.ContactGuid, CliGral.RaoSocial, CliGral.FullNom ")
        sb.AppendLine("HAVING SUM(CASE WHEN Ccb.dh = 1 THEN Ccb.Eur ELSE 0 END)<>SUM(CASE WHEN Ccb.dh = 2 THEN Ccb.Eur ELSE 0 END) ")
        sb.AppendLine("ORDER BY CliGral.RaoSocial")

        sb = New System.Text.StringBuilder
        sb.AppendLine("SELECT Ccb.CtaGuid, Ccb.ContactGuid, CliGral.RaoSocial, PgcCta.Id AS CtaId, PgcCta.Cod AS CtaCodi ")
        sb.AppendLine(", SUM(CASE WHEN dh = 1 THEN Ccb.Eur ELSE 0 END) AS Deb ")
        sb.AppendLine(", SUM(CASE WHEN dh = 2 THEN Ccb.EUR ELSE 0 END) AS Hab ")
        sb.AppendLine("FROM Ccb ")
        sb.AppendLine("INNER JOIN Cca ON Ccb.CcaGuid = Cca.Guid ")
        sb.AppendLine("INNER JOIN PgcCta ON Ccb.CtaGuid = PgcCta.Guid ")
        sb.AppendLine("INNER JOIN CliGral ON Ccb.ContactGuid = CliGral.Guid ")
        sb.AppendLine("WHERE Cca.Emp=" & oExercici.Emp.Id & " AND Year(Cca.Fch)=" & oExercici.Year & " ")
        sb.AppendLine("GROUP BY Ccb.CtaGuid, Ccb.ContactGuid, CliGral.RaoSocial, PgcCta.Id, PgcCta.Cod ")
        sb.AppendLine("HAVING SUM(CASE WHEN Ccb.dh = 1 THEN Ccb.Eur ELSE 0 END)<>SUM(CASE WHEN Ccb.dh = 2 THEN Ccb.Eur ELSE 0 END) ")
        sb.AppendLine("ORDER BY CliGral.RaoSocial, PgcCta.Id ")

        Dim oCcds As New List(Of CtaSdo)
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oCcd As New CtaSdo()
            oCcd.Contact = New DTORep(oDrd("ContactGuid"))
            oCcd.Contact.Nom = oDrd("RaoSocial")
            oCcd.Cta = New DTOPgcCta(oDrd("CtaGuid"))
            oCcd.Cta.Id = oDrd("CtaId")
            oCcd.Cta.Codi = oDrd("CtaCodi")
            oCcd.Debe = SQLHelper.GetDecimalFromDataReader(oDrd("Deb"))
            oCcd.Haber = SQLHelper.GetDecimalFromDataReader(oDrd("Hab"))
            oCcds.Add(oCcd)
        Loop
        oDrd.Close()

        Dim oRepsWithComisions = oCcds.Where(Function(x) x.Cta.Codi = DTOPgcPlan.Ctas.ComisionsRepresentants).Select(Function(y) y.Contact).ToList()
        For Each oRep In oRepsWithComisions
            Dim oCtas = oCcds.Where(Function(x) x.Contact.Equals(oRep) And (x.Cta.Id.StartsWith("40") Or x.Cta.Id.StartsWith("41") Or x.Cta.Id.StartsWith("43"))).ToList
            Dim debe = oCtas.Sum(Function(x) x.Debe)
            Dim haber = oCtas.Sum(Function(x) x.Haber)
            If debe <> 0 Or haber <> 0 Then
                Dim item As New DTOPgcSaldo
                With item
                    .Exercici = oExercici
                    .Contact = oRep
                    .Debe = DTOAmt.Factory(debe)
                    .Haber = DTOAmt.Factory(haber)
                End With
                retval.Add(item)
            End If
        Next

        oDrd.Close()

        Return retval
    End Function

    Private Class CtaSdo
        Property Cta As DTOPgcCta
        Property Contact As DTOContact
        Property Debe As Decimal
        Property Haber As Decimal

    End Class
    Shared Function WithRetencions(oEmp As DTOEmp, FchFrom As Date, FchTo As Date) As List(Of DTORep)
        Dim retval As New List(Of DTORep)
        Dim SQL As String = "SELECT RepGuid " _
        & "From RepLiq " _
        & "WHERE IRPFpct>0 AND Fch>@FchFrom AND Fch<@FchTo " _
        & "Group BY RepGuid"
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@FchFrom", Format(FchFrom, "yyyyMMdd"), "@FchTo", Format(FchTo, "yyyyMMdd"))
        Do While oDrd.Read
            Dim oRep As New DTORep(DirectCast(oDrd("RepGuid"), Guid))
            retval.Add(oRep)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function Sprite(oGuids As List(Of Guid)) As List(Of Byte())
        Dim sb As New Text.StringBuilder
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	      Idx int NOT NULL")
        sb.AppendLine("	    , Guid uniqueidentifier NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Idx,Guid) ")

        Dim idx As Integer = 0
        For Each oGuid In oGuids
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("({0},'{1}') ", idx, oGuid.ToString())
            idx += 1
        Next

        sb.AppendLine()
        sb.AppendLine("SELECT Clx.Img48 ")
        sb.AppendLine("FROM Clx ")
        sb.AppendLine("INNER JOIN @Table X ON Clx.Guid = X.Guid ")
        sb.AppendLine("ORDER BY X.Idx")
        Dim SQL = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Dim retval As New List(Of Byte())
        Do While oDrd.Read
            Dim oImage = oDrd("Img48")
            retval.Add(oImage)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class



