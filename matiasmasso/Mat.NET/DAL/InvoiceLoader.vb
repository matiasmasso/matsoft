Public Class InvoiceLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOInvoice
        Dim retval As DTOInvoice = Nothing
        Dim oInvoice As New DTOInvoice(oGuid)
        If Load(oInvoice) Then
            retval = oInvoice
        End If
        Return retval
    End Function

    Shared Function Pdf(oInvoice As DTOInvoice) As Byte()
        Dim retVal = (New List(Of Byte)).ToArray()
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT DocFile.Doc ")
        sb.AppendLine("FROM Fra ")
        sb.AppendLine("INNER JOIN Cca ON Fra.CcaGuid = Cca.Guid ")
        sb.AppendLine("INNER JOIN DocFile ON Cca.Hash = DocFile.Hash ")
        sb.AppendLine("WHERE Fra.Guid = '" & oInvoice.Guid.ToString() & "'")
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

    Shared Function FromNum(oEmp As DTOEmp, iYea As Integer, iSerie As DTOInvoice.Series, iNum As Integer) As DTOInvoice
        Dim retval As DTOInvoice = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Guid, Fch FROM Fra ")
        sb.AppendLine("WHERE Emp=" & oEmp.Id & " AND Yea=" & iYea & " AND Fra=" & iNum & " AND Serie=" & iSerie)
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOInvoice(oDrd("Guid"))
            With retval
                .Emp = oEmp
                .Fch = oDrd("Fch")
                .Num = iNum
                .Serie = iSerie
            End With
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function Load(ByRef oInvoice As DTOInvoice, Optional exs As List(Of Exception) = Nothing) As Boolean
        Dim retval As Boolean
        If Not oInvoice.IsLoaded And Not oInvoice.IsNew Then

            Dim sb As New System.Text.StringBuilder

            sb.AppendLine("SELECT Arc.Guid AS ArcGuid, Arc.Qty, Arc.Eur, Arc.Pts, Arc.Dto, Arc.Bundle, Arc.Lin ")
            sb.AppendLine(", Arc.ArtGuid ")
            sb.AppendLine(", Pnc.PdcGuid, Pdc.Fch as PdcFch, Pdc.Pdd ")
            sb.AppendLine(", Arc.SpvGuid, Spv.SpvIn, SpvIn.Fch AS SpvInFch, Spv.FchAvis, Spv.Id AS SpvId ")
            sb.AppendLine(", Arc.RepGuid, Arc.Com, Arc.RepComLiquidable ")
            sb.AppendLine(", Spv.Garantia, Spv.Contacto, Spv.sRef AS SpvSRef, Spv.ObsTecnic AS SpvObsTecnic ")
            sb.AppendLine(", Spv.ProductGuid AS SpvProductGuid, Spv.Serial ")
            sb.AppendLine(", VwProductNom.BrandGuid AS SpvBrandGuid, VwProductNom.BrandNom AS SpvBrandNom ")
            sb.AppendLine(", VwProductNom.CategoryGuid AS SpvCategoryGuid, VwProductNom.CategoryNom AS SpvCategoryNom ")
            sb.AppendLine(", VwProductNom.SkuGuid AS SpvSkuGuid, VwProductNom.SkuNom AS SpvSkuNom ")
            sb.AppendLine(", Arc.AlbGuid, Alb.Alb, Alb.Fch as AlbFch, Alb.Cod as AlbCod, Alb.CliGuid AS AlbCliGuid, CliClient.Ref AS AlbCliRef, Alb.CashCod AS AlbCashCod ")
            sb.AppendLine(", Fra.Emp, Fra.Serie, Fra.Fra, Fra.Lang, Fra.Fch as FraFch, Fra.Cur, Fra.IvaStdPct, Fra.ReqStdPct, Fra.EurBase, Fra.IvaStdAmt, Fra.ReqStdAmt, Fra.EurLiq, Fra.ExportCod, Fra.Incoterm ")
            sb.AppendLine(", Fra.Cfp, Fra.Vto, Fra.Fpg, Fra.Ob1, Fra.Ob2, Fra.Ob3, Fra.TipoFactura ")
            sb.AppendLine(", Fra.PrintMode, Fra.FchLastPrinted, Fra.UsrLastPrintedGuid ")
            sb.AppendLine(", Fra.CcaGuid, Cca.Cca, Cca.Hash ")
            sb.AppendLine(", VwSkuNom.* ")
            sb.AppendLine(", Fra.CliGuid, Fra.Nom AS FraNom, Fra.Nif, Fra.NifCod, Fra.Nif2, Fra.Nif2Cod, CliGral.Gln, CliClient.SuProveedorNum, Fra.IvaStdPct, Fra.ReqStdPct ")
            sb.AppendLine(", Fra.Adr, VwZip.ZipGuid, VwZip.ZipCod, VwZip.LocationGuid, VwZip.LocationNom ")
            sb.AppendLine(", VwZip.ProvinciaGuid, VwZip.ZonaGuid, VwZip.ZonaNom, VwZip.ProvinciaNom, VwZip.CountryGuid, VwZip.CountryEsp, VwZip.CountryISO, VwZip.CEE ")

            'sb.AppendLine(", TicketFraAdr.Adr AS TicketFraAdrAdr, TicketFraAdr.ZipGuid AS TicketFraAdrZipGuid, TicketFraAdr.ZipCod AS TicketFraAdrZipCod, TicketFraAdr.LocationGuid AS TicketFraAdrLocationGuid, TicketFraAdr.LocationNom AS TicketFraAdrLocationNom ")
            'sb.AppendLine(", TicketFraAdr.ProvinciaGuid AS TicketFraAdrProvinciaGuid, TicketFraAdr.ZonaGuid AS TicketFraAdrZonaGuid, TicketFraAdr.ProvinciaNom AS TicketFraAdrProvinciaNom, TicketFraAdr.CountryGuid AS TicketFraAdrCountryGuid, TicketFraAdr.CountryEsp AS TicketFraAdrCountryEsp, TicketFraAdr.ExportCod AS TicketFraAdrExportCod ")

            sb.AppendLine(", Fra.SiiResult, Fra.SiiCsv, Fra.SiiFch, Fra.SiiErr, Fra.SiiL9, Fra.Concepte, Fra.RegimenEspecialOTrascendencia ")
            sb.AppendLine(", DeliveryCustomer.RaoSocial AS DeliveryNom, DeliveryCustomer.NifCod AS DeliveryNifCod, DeliveryCustomer.Nif AS DeliveryNif, DeliveryCustomer.GLN as DeliveryGLN ")
            sb.AppendLine(", Alb.Adr AS DeliveryAddress, Alb.Zip AS DeliveryZip ")
            sb.AppendLine(", VwZipDelivery.ZipCod AS DeliveryZipCod, VwZipDelivery.LocationGuid AS DeliveryLocationGuid, VwZipDelivery.LocationNom AS DeliveryLocationNom ")
            'sb.AppendLine(", ConsumerTicket.FraNom As ConsumerRaoSocial, ConsumerTicket.Nom As ConsumerNom, ConsumerTicket.Cognom1 As ConsumerCognom1, ConsumerTicket.Cognom2 As ConsumerCognom2, ConsumerTicket.Nif As ConsumerNif, ConsumerTicket.Lang As ConsumerLang ")
            sb.AppendLine("FROM Fra ")
            sb.AppendLine("INNER JOIN Cca On Fra.CcaGuid = Cca.Guid ")
            sb.AppendLine("INNER JOIN CliGral On Fra.CliGuid = CliGral.Guid ")
            'sb.AppendLine("LEFT OUTER JOIN CliClient As FraCli On Fra.CliGuid = FraCli.Guid ")
            sb.AppendLine("INNER JOIN Alb On Fra.Guid = Alb.FraGuid ")
            sb.AppendLine("LEFT OUTER JOIN CliClient On Alb.CliGuid = CliClient.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CliGral DeliveryCustomer ON Alb.CliGuid = DeliveryCustomer.Guid ")
            sb.AppendLine("INNER JOIN Arc On Alb.Guid = Arc.AlbGuid ")
            'sb.AppendLine("INNER JOIN Art On Arc.ArtGuid = Art.Guid ")
            sb.AppendLine("INNER JOIN VwSkuNom On Arc.ArtGuid = VwSkuNom.SkuGuid ")
            sb.AppendLine("LEFT OUTER JOIN Pnc On Arc.PncGuid = Pnc.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Pdc On Pnc.PdcGuid = Pdc.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Spv On Arc.SpvGuid = Spv.Guid ")
            sb.AppendLine("LEFT OUTER JOIN SpvIn On Spv.SpvIn = SpvIn.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwProductNom On Spv.ProductGuid = VwProductNom.Guid ")
            'sb.AppendLine("LEFT OUTER JOIN ConsumerTicket On Alb.Guid = ConsumerTicket.Delivery ")
            sb.AppendLine("LEFT OUTER JOIN VwZip On Fra.Zip = VwZip.ZipGuid ")
            sb.AppendLine("LEFT OUTER JOIN VwZip VwZipDelivery On Alb.Zip = VwZipDelivery.ZipGuid ")
            'sb.AppendLine("LEFT OUTER JOIN VwAddressBase TicketFraAdr On TicketFraAdr.SrcGuid = ConsumerTicket.Guid And TicketFraAdr.Cod = " & DTOAddress.Codis.FraConsumidor & " ")
            sb.AppendLine("WHERE Fra.Guid='" & oInvoice.Guid.ToString & "' ")
            sb.AppendLine("ORDER BY Alb.Alb, Arc.Lin ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)

            Dim oDelivery As New DTODelivery
            Dim oOrder As New DTOPurchaseOrder
            Dim oSpv As New DTOSpv
            Try
                Do While oDrd.Read
                    If Not oInvoice.IsLoaded Then
                        Dim oCustomer As New DTOCustomer(oDrd("CliGuid"))
                        With oCustomer
                            .GLN = SQLHelper.GetEANFromDataReader(oDrd("Gln"))
                            .SuProveedorNum = SQLHelper.GetStringFromDataReader(oDrd("SuProveedorNum"))
                        End With

                        With oInvoice
                            .Emp = New DTOEmp(oDrd("Emp"))
                            .Serie = oDrd("Serie")
                            .Num = oDrd("Fra")
                            .Fch = oDrd("FraFch")
                            .Lang = DTOLang.Factory(oDrd("Lang"))
                            .Concepte = SQLHelper.GetIntegerFromDataReader(oDrd("Concepte"))
                            .Customer = oCustomer
                            .Nom = oDrd("FraNom")
                            .Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
                            .Adr = SQLHelper.GetStringFromDataReader(oDrd("Adr"))
                            .Zip = SQLHelper.GetZipFromDataReader(oDrd)
                            .TipoFactura = SQLHelper.GetStringFromDataReader(oDrd("TipoFactura"))
                            .Iva = oDrd("IvaStdPct")
                            .Req = oDrd("ReqStdPct")
                            .Cfp = SQLHelper.GetIntegerFromDataReader(oDrd("Cfp"))
                            .Vto = SQLHelper.GetFchFromDataReader(oDrd("Vto"))
                            .Fpg = SQLHelper.GetStringFromDataReader(oDrd("Fpg"))
                            .Ob1 = SQLHelper.GetStringFromDataReader(oDrd("Ob1"))
                            .Ob2 = SQLHelper.GetStringFromDataReader(oDrd("Ob2"))
                            .Ob3 = SQLHelper.GetStringFromDataReader(oDrd("Ob3"))
                            .PrintMode = oDrd("PrintMode")
                            .SiiLog = SQLHelper.GetSiiLogFromDataReader(oDrd)
                            .SiiL9 = SQLHelper.GetStringFromDataReader(oDrd("SiiL9"))
                            .RegimenEspecialOTrascendencia = SQLHelper.GetStringFromDataReader(oDrd("RegimenEspecialOTrascendencia"))
                            .ExportCod = SQLHelper.GetIntegerFromDataReader(oDrd("ExportCod"))
                            .Incoterm = SQLHelper.GetIncotermFromDataReader(oDrd("Incoterm"))
                            If Not IsDBNull(oDrd("UsrLastPrintedGuid")) Then
                                .UserLastPrinted = New DTOUser(DirectCast(oDrd("UsrLastPrintedGuid"), Guid))
                            End If
                            .FchLastPrinted = SQLHelper.GetFchFromDataReader(oDrd("FchLastPrinted"))
                            .Cca = New DTOCca(oDrd("CcaGuid"))
                            .Cca.Id = SQLHelper.GetIntegerFromDataReader(oDrd("Cca"))
                            .DocFile = SQLHelper.GetDocFileFromDataReaderHash(oDrd("Hash"))
                            .IsLoaded = True
                            .Deliveries = New List(Of DTODelivery)

                            .BaseImponible = DTOAmt.Factory(DTOCur.Factory(oDrd("Cur")), CDec(oDrd("EurBase")))
                            .IvaBaseQuotas = New List(Of DTOTaxBaseQuota)
                            .Total = DTOAmt.Factory(DTOCur.Factory(oDrd("Cur")), CDec(oDrd("EurLiq")))

                            If .ExportCod = DTOInvoice.ExportCods.nacional Then
                                Dim DcIvaTipus As Decimal = SQLHelper.GetDecimalFromDataReader(oDrd("IvaStdPct"))
                                If DcIvaTipus = 0 Then
                                    .TipoSujeccionIva = DTOInvoice.TiposSujeccionIva.sujetoExento
                                Else
                                    .TipoSujeccionIva = DTOInvoice.TiposSujeccionIva.sujetoNoExento
                                    .IvaBaseQuotas.Add(New DTOTaxBaseQuota(DTOTax.Codis.iva_Standard, .BaseImponible.Eur, DcIvaTipus, oDrd("IvaStdAmt")))

                                    If Not IsDBNull(oDrd("ReqStdPct")) Then
                                        Dim DcReqTipus As Decimal = SQLHelper.GetDecimalFromDataReader(oDrd("ReqStdPct"))
                                        If DcReqTipus <> 0 Then
                                            Dim oReq As New DTOTax
                                            oReq.tipus = DcReqTipus
                                            oReq.codi = DTOTax.Codis.recarrec_Equivalencia_Standard
                                            .IvaBaseQuotas.Add(New DTOTaxBaseQuota(DTOTax.Codis.recarrec_Equivalencia_Standard, .BaseImponible.Eur, DcReqTipus, oDrd("ReqStdAmt")))
                                        End If
                                    End If

                                End If
                            Else
                                .TipoSujeccionIva = DTOInvoice.TiposSujeccionIva.sujetoExento
                            End If

                        End With
                    End If

                    If Not oDelivery.Guid.Equals(oDrd("AlbGuid")) Then
                        oDelivery = New DTODelivery(oDrd("AlbGuid"))
                        With oDelivery
                            .Id = oDrd("Alb")
                            .Fch = oDrd("AlbFch")
                            .Cod = oDrd("AlbCod")
                            .CashCod = oDrd("AlbCashCod")
                            .Customer = New DTOCustomer(oDrd("AlbCliGuid"))
                            .Customer.Ref = SQLHelper.GetStringFromDataReader(oDrd("AlbCliRef"))
                            .Customer.Lang = oInvoice.Lang
                            .Customer.Nom = SQLHelper.GetStringFromDataReader(oDrd("DeliveryNom"))
                            .Customer.GLN = SQLHelper.GetEANFromDataReader(oDrd("DeliveryGLN"))
                            If Not IsDBNull(oDrd("DeliveryNif")) Then
                                .Customer.Nifs.Add(DTONif.Factory(SQLHelper.GetIntegerFromDataReader(oDrd("DeliveryNifCod")), SQLHelper.GetStringFromDataReader(oDrd("DeliveryNif"))))
                            End If
                            .Address = DTOAddress.Factory(.Customer, DTOAddress.Codis.Entregas)
                            .Address.Text = SQLHelper.GetStringFromDataReader(oDrd("DeliveryAddress"))
                            .Address.Zip = New DTOZip(oDrd("DeliveryZip"))
                            .Address.Zip.ZipCod = SQLHelper.GetStringFromDataReader(oDrd("DeliveryZipCod"))
                            .Address.Zip.Location = New DTOLocation(oDrd("DeliveryLocationGuid"))
                            .Address.Zip.Location.Nom = SQLHelper.GetStringFromDataReader(oDrd("DeliveryLocationNom"))
                            .Items = New List(Of DTODeliveryItem)
                            .Invoice = oInvoice
                        End With
                        oInvoice.Deliveries.Add(oDelivery)
                    End If

                    Dim oItem As New DTODeliveryItem(oDrd("ArcGuid"))
                    Dim oSku = SQLHelper.GetProductFromDataReader(oDrd)

                    Select Case oDelivery.Cod
                        Case DTOPurchaseOrder.Codis.client
                            If Not IsDBNull(oDrd("PdcGuid")) AndAlso Not oOrder.Guid.Equals(oDrd("PdcGuid")) Then
                                'If Not oOrder.Guid.Equals(oDrd("PdcGuid")) Then
                                oOrder = New DTOPurchaseOrder(oDrd("PdcGuid"))
                                With oOrder
                                    .Fch = oDrd("PdcFch")
                                    .Concept = oDrd("Pdd")
                                End With
                            End If

                            Dim oPOItem As New DTOPurchaseOrderItem
                            With oPOItem
                                .PurchaseOrder = oOrder
                                .Lin = oDrd("Lin")
                                .Sku = oSku
                            End With

                            oItem.PurchaseOrderItem = oPOItem

                        Case DTOPurchaseOrder.Codis.reparacio
                            If Not oSpv.Guid.Equals(oDrd("SpvGuid")) Then
                                Dim oSpvIn As New DTOSpvIn(oDrd("SpvIn"))
                                With oSpvIn
                                    .fch = oDrd("SpvInFch")
                                End With
                                oSpv = New DTOSpv(oDrd("SpvGuid"))
                                With oSpv
                                    .id = oDrd("SpvId")
                                    .spvIn = oSpvIn
                                    .fchAvis = oDrd("FchAvis")
                                    .garantia = oDrd("Garantia")
                                    .contacto = SQLHelper.GetStringFromDataReader(oDrd("Contacto"))
                                    .sRef = oDrd("SpvSRef")
                                    .obsTecnic = SQLHelper.GetStringFromDataReader(oDrd("SpvObsTecnic"))
                                    .product = SQLHelper.GetProductFromDataReader(oDrd, brandGuidField:="SpvBrandGuid",
                                                                                    brandNomField:="SpvBrandNom",
                                                                                    categoryGuidField:="SpvCategoryGuid",
                                                                                    categoryNomField:="SpvCategoryNom",
                                                                                    skuGuidField:="SpvSkuGuid",
                                                                                    skuNomField:="SpvSkuNom")

                                    .serialNumber = SQLHelper.GetStringFromDataReader(oDrd("Serial"))
                                    .customer = oInvoice.Customer
                                End With

                            End If

                            oItem.Spv = oSpv
                    End Select

                    With oItem
                        .Sku = oSku
                        .Price = DTOAmt.Factory(CDec(oDrd("Eur")), oDrd("Cur").ToString, CDec(oDrd("Pts")))
                        .Dto = SQLHelper.GetDecimalFromDataReader(oDrd("Dto"))
                        .Qty = oDrd("Qty")
                        '.Delivery = oDelivery prevent recursion
                        If Not IsDBNull(oDrd("RepGuid")) Then
                            .RepCom = New DTORepCom
                            With .RepCom
                                .rep = New DTORep(oDrd("RepGuid"))
                                .com = SQLHelper.GetDecimalFromDataReader(oDrd("Com"))
                            End With
                        End If
                        If Not IsDBNull(oDrd("RepComLiquidable")) Then
                            .RepComLiquidable = New DTORepComLiquidable(oDrd("RepComLiquidable"))
                        End If
                    End With


                    If isBundleChild(oDrd) Then
                        Dim oBundleParent = BundleParent(oDelivery, oDrd("Bundle"))
                        oBundleParent.Bundle.Add(oItem)
                    Else
                        oDelivery.Items.Add(oItem)
                    End If

                Loop

                retval = True
            Catch ex As Exception
                If exs IsNot Nothing Then
                    exs.Add(ex)
                End If
            Finally
                If oDrd IsNot Nothing Then oDrd.Close()
            End Try
        Else
            retval = True
        End If

        If oInvoice.IsConsumer Then
            'switch name and address to Consumer ticket details
            Dim oFirstDelivery = oInvoice.Deliveries.First()
            Dim oConsumerTicket = ConsumerTicketLoader.FromDelivery(oFirstDelivery)

        End If
        Return retval
    End Function

    Private Shared Function isBundleChild(odrd As SqlDataReader) As Boolean
        Dim retval As Boolean
        If Not IsDBNull(odrd("Bundle")) Then
            Dim oParentGuid As Guid = odrd("Bundle")
            Dim oChildGuid As Guid = odrd("ArcGuid")
            retval = Not oParentGuid.Equals(oChildGuid)
        End If
        Return retval
    End Function

    Private Shared Function BundleParent(oDelivery As DTODelivery, oGuid As Guid) As DTODeliveryItem
        Dim retval = oDelivery.Items.First(Function(x) x.Guid.Equals(oGuid))
        Return retval
    End Function

    Shared Function Update(oInvoice As DTOInvoice, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oInvoice, oTrans)
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

    Shared Sub Update(oInvoice As DTOInvoice, ByRef oTrans As SqlTransaction)
        CcaLoader.Update(oInvoice.Cca, oTrans)
        UpdateInvoice(oInvoice, oTrans)
        UpdateDeliveries(oInvoice, oTrans)

        DTOInvoice.setRepComLiquidables(oInvoice)
        UpdateRepComLiquidables(oInvoice, oTrans)
    End Sub

    Private Shared Sub UpdateDeliveries(oInvoice As DTOInvoice, oTrans As SqlTransaction)
        Dim SQL As String = ""
        If oInvoice.Deliveries IsNot Nothing Then
            If Not oInvoice.IsNew Then
                SQL = "UPDATE Alb SET Alb.FraGuid = NULL WHERE Alb.FraGuid ='" & oInvoice.Guid.ToString & "'"
                SQLHelper.ExecuteNonQuery(SQL, oTrans)
            End If

            Dim sb As New Text.StringBuilder
            sb.AppendLine("UPDATE Alb ")
            sb.AppendLine("SET Alb.FraGuid = '" & oInvoice.Guid.ToString & "' ")
            sb.AppendLine("WHERE ( ")
            For Each oDelivery As DTODelivery In oInvoice.Deliveries
                If Not oDelivery Is oInvoice.Deliveries.First Then
                    sb.AppendLine("OR ")
                End If
                sb.AppendLine("Alb.Guid = '" & oDelivery.Guid.ToString & "' ")
            Next
            sb.AppendLine(") ")
            SQL = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL, oTrans)
        End If
    End Sub

    Private Shared Sub UpdateRepComLiquidables(oInvoice As DTOInvoice, ByRef oTrans As SqlTransaction)
        If Not oInvoice.IsNew Then BackUp_Reps(oInvoice, oTrans)
        For Each oRepComLiquidable In oInvoice.RepComLiquidables
            RepComLiquidableLoader.Update(oRepComLiquidable, oTrans)
        Next
    End Sub

    Shared Sub UpdateInvoice(oInvoice As DTOInvoice, ByRef oTrans As SqlTransaction)
        If oInvoice.Num = 0 Then oInvoice.Num = LastId(oInvoice, oTrans) + 1
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Fra ")
        sb.AppendLine("WHERE Fra.Guid='" & oInvoice.Guid.ToString & "'")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oInvoice.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oInvoice
            oRow("Serie") = .Serie
            oRow("Fra") = .Num
            oRow("Fch") = .Fch
            oRow("Emp") = .Emp.Id
            oRow("Lang") = .Lang.Tag
            oRow("Yea") = .Fch.Year 'TO DEPRECATE--------------------------
            oRow("CliGuid") = .Customer.Guid
            oRow("Nom") = SQLHelper.NullableString(.Nom)
            oRow("Adr") = SQLHelper.NullableString(.Adr)
            oRow("Zip") = SQLHelper.NullableBaseGuid(.Zip)
            SQLHelper.SetNullableNifs(oRow, .Nifs)

            oRow("Cfp") = .Cfp
            If .Fpg.Length > 50 Then Throw New Exception("forma de pagament massa llarga, no pot passar de 50 caracters:" & vbCrLf & .Fpg)
            oRow("Fpg") = .Fpg
            If .Ob1.Length > 50 Then Throw New Exception("primera linia de observacions massa llarga, no pot passar de 50 caracters:" & vbCrLf & .Ob1)
            oRow("OB1") = .Ob1
            If .Ob2.Length > 50 Then Throw New Exception("segona linia de observacions massa llarga, no pot passar de 50 caracters:" & vbCrLf & .Ob2)
            oRow("OB2") = .Ob2
            If .Ob3.Length > 50 Then Throw New Exception("tercera linia de observacions massa llarga, no pot passar de 50 caracters:" & vbCrLf & .Ob3)
            oRow("OB3") = .Ob3
            oRow("VTO") = IIf(.Vto = Nothing, .Fch.Date, .Vto.Date)

            oRow("SUMITEMS") = .BaseImponible.Eur
            oRow("EURBASE") = .BaseImponible.Eur

            'oRow("DTOPCT") = mDtoPct
            'oRow("DTOBASE") = mDtoBas.Eur
            'oRow("DTOAMT") = mDtoAmt.Eur

            'oRow("DPPPCT") = mDppPct
            'oRow("DPPBASE") = mDppBas.Eur
            'oRow("DPPAMT") = mDppAmt.Eur

            oRow("IVASTDBASE") = 0
            oRow("IVASTDPCT") = 0
            oRow("IVASTDAMT") = 0
            oRow("REQSTDPCT") = 0
            oRow("REQSTDAMT") = 0

            oRow("IVAREDBASE") = 0
            oRow("IVAREDPCT") = 0
            oRow("IVAREDAMT") = 0
            oRow("REQREDPCT") = 0
            oRow("REQREDAMT") = 0

            oRow("IVASUPERREDBASE") = 0
            oRow("IVASUPERREDPCT") = 0
            oRow("IVASUPERREDAMT") = 0
            oRow("REQSUPERREDPCT") = 0
            oRow("REQSUPERREDAMT") = 0

            Dim BlRecarrecEquivalencia As Boolean = .Req > 0
            For Each oQuota As DTOTaxBaseQuota In .IvaBaseQuotas
                Select Case oQuota.tax.codi
                    Case DTOTax.Codis.iva_Standard
                        oRow("IVASTDBASE") = oQuota.baseImponible.Eur
                        oRow("IVASTDPCT") = oQuota.tax.tipus
                        oRow("IVASTDAMT") = SQLHelper.NullableAmt(oQuota.quota)
                    Case DTOTax.Codis.recarrec_Equivalencia_Standard
                        oRow("REQSTDPCT") = oQuota.tax.tipus
                        oRow("REQSTDAMT") = SQLHelper.NullableAmt(oQuota.quota)
                    Case DTOTax.Codis.iva_Reduit
                        oRow("IVAREDBASE") = oQuota.baseImponible.Eur
                        oRow("IVAREDPCT") = oQuota.tax.tipus
                        oRow("IVAREDAMT") = SQLHelper.NullableAmt(oQuota.quota)
                    Case DTOTax.Codis.recarrec_Equivalencia_Reduit
                        oRow("REQREDPCT") = oQuota.tax.tipus
                        oRow("REQREDAMT") = SQLHelper.NullableAmt(oQuota.quota)
                    Case DTOTax.Codis.iva_SuperReduit
                        oRow("IVASUPERREDBASE") = oQuota.baseImponible.Eur
                        oRow("IVASUPERREDPCT") = oQuota.tax.tipus
                        oRow("IVASUPERREDAMT") = SQLHelper.NullableAmt(oQuota.quota)
                    Case DTOTax.Codis.recarrec_Equivalencia_SuperReduit
                        oRow("REQSUPERREDPCT") = oQuota.tax.tipus
                        oRow("REQSUPERREDAMT") = SQLHelper.NullableAmt(oQuota.quota)
                End Select
            Next

            oRow("PTSLIQ") = .Total.Val
            oRow("CUR") = .Total.Cur.Tag
            oRow("EURLIQ") = .Total.Eur

            oRow("CCAGUID") = .Cca.Guid

            oRow("TipoFactura") = SQLHelper.NullableString(.TipoFactura)
            SQLHelper.SetSiiLog(.SiiLog, oRow)
            oRow("SiiL9") = SQLHelper.NullableString(.SiiL9)
            oRow("Concepte") = SQLHelper.NullableString(.Concepte)
            oRow("RegimenEspecialOTrascendencia") = SQLHelper.NullableString(.RegimenEspecialOTrascendencia)
            oRow("ExportCod") = SQLHelper.NullableInteger(.ExportCod)
            oRow("Incoterm") = SQLHelper.NullableIncoterm(.Incoterm)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function LastId(oInvoice As DTOInvoice, ByRef oTrans As SqlTransaction) As Integer
        Dim retval As Integer
        Dim SQL As String = "SELECT TOP 1 Fra AS LastId FROM Fra " _
        & "WHERE Emp=" & oInvoice.Emp.Id & " AND Yea=" & oInvoice.Fch.Year & " AND Serie=" & oInvoice.Serie & " " _
        & "ORDER BY Fra DESC"

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        If oTb.Rows.Count > 0 Then
            Dim oRow As DataRow = oTb.Rows(0)
            If Not IsDBNull(oRow("LastId")) Then
                retval = CInt(oRow("LastId"))
            End If
        End If
        Return retval
    End Function

    Shared Function LogSii(oInvoice As DTOInvoice, exs As List(Of Exception)) As Boolean
        Dim oLog = oInvoice.SiiLog
        If oLog IsNot Nothing Then
            Dim sb As New Text.StringBuilder
            sb.AppendLine("UPDATE Fra ")
            sb.AppendLine("SET Fra.SiiResult=" & oLog.Result & " ")
            sb.AppendLine(", Fra.SiiFch='" & SQLHelper.FormatDatetime(oLog.Fch) & "' ")
            If oLog.Csv = "" Then
                sb.AppendLine(", Fra.SiiCsv=NULL ")
            Else
                sb.AppendLine(", Fra.SiiCsv='" & oLog.Csv & "' ")
            End If
            If (oLog.ErrMsg = "") Then
                sb.AppendLine(", Fra.SiiErr = NULL ")
            Else
                sb.AppendLine(", Fra.SiiErr='" & oLog.ErrMsg & "' ")
            End If
            sb.AppendLine("WHERE Fra.Guid = '" & oInvoice.Guid.ToString & "'")
            Dim SQL As String = sb.ToString
            Dim i As Integer = SQLHelper.ExecuteNonQuery(SQL, exs)

        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Function AvailableNums(oEmp As DTOEmp, iYear As Integer, serie As DTOInvoice.Series) As List(Of Integer)
        Dim retval As New List(Of Integer)
        Dim franums As New List(Of Integer)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Fra.Fra FROM Fra WHERE Emp=" & oEmp.Id & " AND Year(Fra.Fch)=" & iYear & " AND Fra.Serie = " & CInt(serie) & " ORDER BY Fra DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            franums.Add(oDrd("Fra"))
        Loop
        oDrd.Close()

        'afegeix el numero que li tocaría a la propera factura
        Dim previous As Integer = 0
        If franums.Count > 0 Then
            previous = franums.First
        End If
        retval.Add(previous + 1)

        'afegeix els numeros lliures que puguin quedar en ordre descendent
        Dim idx As Integer = 1

        If previous >= 0 Then
            For num As Integer = previous - 1 To 1 Step -1
                If idx < franums.Count Then
                    If franums(idx) < num Then
                        retval.Add(num)
                    Else
                        idx += 1
                    End If
                End If
            Next
        End If

        'afegeix els lliures abans del primer
        If franums.Count > 0 Then
            For num = franums.Last - 1 To 1 Step -1
                retval.Add(num)
            Next
        End If
        Return retval
    End Function

    Shared Function Delete(oInvoice As DTOInvoice, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Load(oInvoice, exs)
        If exs.Count = 0 Then
            Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
            Dim oTrans As SqlTransaction = oConn.BeginTransaction
            Try
                BackUp_Reps(oInvoice, oTrans)
                BackUp_Albs(oInvoice, oTrans)
                Delete(oInvoice, oTrans)
                CcaLoader.Delete(oInvoice.Cca, oTrans)
                oTrans.Commit()
                retval = True
            Catch ex As Exception
                oTrans.Rollback()
                exs.Add(ex)
            Finally
                oConn.Close()
            End Try
        End If
        Return retval
    End Function


    Shared Sub Delete(oInvoice As DTOInvoice, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE FRA WHERE Guid='" & oInvoice.Guid.ToString & "'"
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub


    Shared Sub BackUp_Albs(oInvoice As DTOInvoice, oTrans As SqlTransaction)
        Dim SQL As String = "UPDATE Alb SET FraGuid = NULL WHERE FraGuid='" & oInvoice.Guid.ToString & "'"
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub BackUp_Reps(oInvoice As DTOInvoice, oTrans As SqlTransaction)
        Dim SQL As String = ""

        SQL = "UPDATE Arc SET RepComLiquidable=NULL " _
            & "FROM Arc INNER JOIN Alb ON Arc.AlbGuid=Alb.Guid " _
            & "WHERE Alb.FraGuid = '" & oInvoice.Guid.ToString & "'"
        SQLHelper.ExecuteNonQuery(SQL, oTrans)

        SQL = "DELETE Rps WHERE FraGuid='" & oInvoice.Guid.ToString & "'"
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub



#End Region

    'oLog As DTOInvoicePrintLog

    Shared Function LogPrint(oInvoice As DTOInvoice, oPrintMode As DTOInvoice.PrintModes, oWinUser As DTOUser, oDestUser As DTOUser, DtFch As Date, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            LogPrint(oInvoice, oPrintMode, oWinUser, oDestUser, DtFch, oTrans)
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

    Shared Sub LogPrint(oLog As DTOInvoicePrintLog, oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("UPDATE Fra ")
        sb.AppendLine("SET FchLastPrinted='" & SQLHelper.FormatDatetime(oLog.Fch) & "' ")
        sb.AppendLine(", PrintMode=" & CInt(oLog.PrintMode) & " ")
        If oLog.WinUser IsNot Nothing Then
            sb.AppendLine(", UsrLastPrintedGuid='" & oLog.WinUser.Guid.ToString & "' ")
        End If
        If oLog.DestUser IsNot Nothing Then
            sb.AppendLine(", EmailedToGuid='" & oLog.DestUser.Guid.ToString & "' ")
        End If
        sb.AppendLine("WHERE Fra.Guid = '" & oLog.Invoice.Guid.ToString & "' ")

        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub LogPrint(oInvoice As DTOInvoice, oPrintMode As DTOInvoice.PrintModes, oWinUser As DTOUser, oDestUser As DTOUser, DtFch As Date, oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("UPDATE Fra ")
        sb.AppendLine("SET FchLastPrinted='" & SQLHelper.FormatDatetime(DtFch) & "' ")
        sb.AppendLine(", PrintMode=" & CInt(oPrintMode) & " ")
        If oWinUser IsNot Nothing Then
            sb.AppendLine(", UsrLastPrintedGuid='" & oWinUser.Guid.ToString & "' ")
        End If
        If oDestUser IsNot Nothing Then
            sb.AppendLine(", EmailedToGuid='" & oDestUser.Guid.ToString & "' ")
        End If
        sb.AppendLine("WHERE Fra.Guid = '" & oInvoice.Guid.ToString & "' ")

        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub


    Shared Function ediOrderFiles(oInvoice As DTOInvoice) As List(Of DTOEdiversaFile)
        Dim retval As New List(Of DTOEdiversaFile)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Edi.Guid, Edi.Text ")
        sb.AppendLine("FROM Edi ")
        sb.AppendLine("INNER JOIN EdiversaOrderHeader ON Edi.Guid = EdiversaOrderHeader.Guid ")
        sb.AppendLine("INNER JOIN Pnc ON EdiversaOrderHeader.Result = Pnc.PdcGuid ")
        sb.AppendLine("INNER JOIN Arc ON Pnc.Guid = Arc.PncGuid ")
        sb.AppendLine("INNER JOIN Alb ON Arc.AlbGuid = Alb.Guid ")
        sb.AppendLine("WHERE Alb.FraGuid = '" & oInvoice.Guid.ToString() & "' ")

        Dim SQL As String = sb.ToString
        Dim odrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While odrd.Read
            Dim oEdiversaFile As New DTOEdiversaFile(odrd("Guid"))
            With oEdiversaFile
                .stream = odrd("Text")
            End With
            retval.Add(oEdiversaFile)
        Loop
        odrd.Close()
        Return retval
    End Function

End Class

Public Class InvoicesLoader

    Shared Function ClearPrintLog(oInvoices As List(Of DTOInvoice), exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("UPDATE Fra ")
            sb.AppendLine("SET FchLastPrinted = NULL ")
            sb.AppendLine(", PrintMode=" & CInt(DTOInvoice.PrintModes.pending) & " ")
            sb.AppendLine(", UsrLastPrintedGuid = NULL")
            sb.AppendLine(", EmailedToGuid = NULL ")
            sb.AppendLine("WHERE (")
            For Each oInvoice In oInvoices
                If Not oInvoice.Equals(oInvoices.First) Then sb.Append("OR ")
                sb.AppendLine("Fra.Guid = '" & oInvoice.Guid.ToString & "' ")
            Next
            sb.AppendLine(")")

            Dim SQL As String = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL, oTrans)

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


    Shared Function Summary(oEmp As DTOEmp) As List(Of DTOYearMonth) ' List(Of KeyValuePair(Of DTOYearMonth, DTOAmt))
        Dim retval As New List(Of DTOYearMonth) ' List(Of KeyValuePair(Of DTOYearMonth, DTOAmt))

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT YEAR(Fra.Fch) AS Year, MONTH(Fra.Fch) AS Month, SUM(Fra.EurBase) AS Amt ")
        sb.AppendLine("FROM Fra ")
        sb.AppendLine("INNER JOIN CliGral ON FRA.CliGuid = CliGral.Guid ")
        sb.AppendLine("WHERE CliGral.Emp=" & oEmp.Id & " ")
        sb.AppendLine("GROUP BY YEAR(Fra.Fch), MONTH(Fra.Fch) ")
        sb.AppendLine("ORDER BY YEAR(Fra.Fch) DESC, MONTH(Fra.Fch) DESC")

        Dim SQL As String = sb.ToString
        Dim odrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While odrd.Read
            Dim iYear As Integer = odrd("Year")
            Dim iMonth As Integer = odrd("Month")
            Dim oYearMonth As New DTOYearMonth(iYear, iMonth, SQLHelper.GetDecimalFromDataReader(odrd("Amt")))
            'Dim item As New KeyValuePair(Of DTOYearMonth, DTOAmt)(oYearMonth, oAmt)
            'retval.Add(item)
            retval.Add(oYearMonth)
        Loop
        odrd.Close()
        Return retval
    End Function

    Shared Function All(oExercici As DTOExercici, iMes As Integer) As List(Of DTOInvoice)
        'per declaració IVA

        Dim retval As New List(Of DTOInvoice)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Fra.Guid, Fra.Serie, Fra.Fra, Fra.Fch, Fra.Vto, Fra.CliGuid ")
        sb.AppendLine(", Fra.EurBase, Fra.SiiL9, Fra.SiiResult ")
        sb.AppendLine(", Fra.IvaStdBase, Fra.IvaStdPct, Fra.IvaStdAmt, Fra.ReqStdPct, Fra.ReqStdAmt ")
        sb.AppendLine(", Fra.IvaRedBase, Fra.IvaRedPct, Fra.IvaRedAmt, Fra.ReqRedPct, Fra.ReqRedAmt ")
        sb.AppendLine(", Fra.IvaSuperRedBase, Fra.IvaSuperRedPct, Fra.IvaSuperRedAmt, Fra.ReqSuperRedPct, Fra.ReqSuperRedAmt ")
        sb.AppendLine(", Fra.CcaGuid, Cca.Cca ")
        sb.AppendLine(", CliGral.RaoSocial, CliGral.Nif, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod ")
        sb.AppendLine("FROM Fra ")
        sb.AppendLine("INNER JOIN Cca ON Fra.CcaGuid = Cca.Guid ")
        sb.AppendLine("INNER JOIN CliGral ON Fra.CliGuid = CliGral.Guid ")
        sb.AppendLine("WHERE Fra.Emp=" & oExercici.Emp.Id & " ")
        sb.AppendLine("AND Fra.Yea=" & oExercici.Year & " ")
        sb.AppendLine("AND MONTH(Fra.Fch)=" & iMes & " ")
        sb.AppendLine("ORDER BY Fra.Serie, Fra.Fra")
        Dim SQL As String = sb.ToString
        Dim odrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While odrd.Read
            Dim oBaseQuotas As New List(Of DTOTaxBaseQuota)

            If SQLHelper.IsNotZero(odrd("IvaStdPct")) Then
                Dim oIvaStd As New DTOTaxBaseQuota(DTOTax.Codis.iva_Standard, odrd("IvaStdBase"), odrd("IvaStdPct"), odrd("IvaStdAmt"))
                oBaseQuotas.Add(oIvaStd)
                If SQLHelper.IsNotZero(odrd("ReqStdPct")) Then
                    Dim oReqStd As New DTOTaxBaseQuota(DTOTax.Codis.recarrec_Equivalencia_Standard, odrd("IvaStdBase"), odrd("ReqStdPct"), odrd("ReqStdAmt"))
                    oBaseQuotas.Add(oReqStd)
                End If
            End If

            If SQLHelper.IsNotZero(odrd("IvaRedPct")) Then
                Dim oIvaRed As New DTOTaxBaseQuota(DTOTax.Codis.iva_Standard, odrd("IvaRedBase"), odrd("IvaRedPct"), odrd("IvaRedAmt"))
                oBaseQuotas.Add(oIvaRed)
                If SQLHelper.IsNotZero(odrd("ReqRedPct")) Then
                    Dim oReqRed As New DTOTaxBaseQuota(DTOTax.Codis.iva_Standard, odrd("IvaRedBase"), odrd("ReqRedPct"), odrd("ReqRedAmt"))
                    oBaseQuotas.Add(oReqRed)
                End If
            End If

            If SQLHelper.IsNotZero(odrd("IvaSuperRedPct")) Then
                Dim oIvaSuperRed As New DTOTaxBaseQuota(DTOTax.Codis.iva_Standard, odrd("IvaSuperRedBase"), odrd("IvaSuperRedPct"), odrd("IvaSuperRedAmt"))
                oBaseQuotas.Add(oIvaSuperRed)
                If SQLHelper.IsNotZero(odrd("ReqSuperRedPct")) Then
                    Dim oReqSuperRed As New DTOTaxBaseQuota(DTOTax.Codis.iva_Standard, odrd("IvaSuperRedBase"), odrd("ReqSuperRedPct"), odrd("ReqSuperRedAmt"))
                    oBaseQuotas.Add(oReqSuperRed)
                End If
            End If

            If oBaseQuotas.Count = 0 Then
                Dim oExento As New DTOTaxBaseQuota(DTOTax.Codis.exempt, odrd("EurBase"), 0)
                oBaseQuotas.Add(oExento)
            End If

            Dim oInvoice As New DTOInvoice(odrd("Guid"))
            With oInvoice
                .Serie = odrd("Serie")
                .Num = odrd("Fra")
                .Fch = odrd("Fch")
                .Vto = SQLHelper.GetFchFromDataReader(odrd("Vto"))
                .Customer = New DTOCustomer(odrd("CliGuid"))
                .Customer.Nom = odrd("RaoSocial")
                .Customer.Nifs = SQLHelper.GetNifsFromDataReader(odrd)
                '.Customer.Nif = odrd("Nif")
                .SiiL9 = SQLHelper.GetStringFromDataReader(odrd("SiiL9"))
                .SiiLog = SQLHelper.GetSiiLogFromDataReader(odrd)
                .IvaBaseQuotas = oBaseQuotas
                .Cca = New DTOCca(odrd("CcaGuid"))
                .Cca.Id = odrd("Cca")
            End With
            retval.Add(oInvoice)
        Loop
        odrd.Close()
        Return retval

    End Function

    Shared Function Headers(oEmp As DTOEmp, oYearMonth As DTOYearMonth) As List(Of DTOInvoice)
        Dim retval As New List(Of DTOInvoice)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Fra.Guid, Fra.Serie, Fra.Fra, Fra.Fch, Fra.EurLiq, Fra.CliGuid, Fra.Fpg ")
        sb.AppendLine(", Fra.SiiResult, Fra.PrintMode ")
        sb.AppendLine(", Fra.TipoFactura, Fra.SiiL9, Fra.Concepte, Fra.RegimenEspecialOTrascendencia ")
        sb.AppendLine(", CliGral.FullNom, Cca.Hash ")
        sb.AppendLine(", X.Ticket, X.Nom AS TicketNom, X.Cognom1 AS TicketCognom1, X.Cognom2 AS TicketCognom2 ")
        sb.AppendLine("FROM Fra ")
        sb.AppendLine("INNER JOIN CliGral ON Fra.CliGuid = CliGral.Guid ")
        sb.AppendLine("INNER JOIN Cca On Fra.CcaGuid = Cca.Guid ")
        sb.AppendLine("LEFT OUTER JOIN ( ")
        sb.AppendLine("     SELECT Alb.FraGuid, ConsumerTicket.Id AS Ticket, ConsumerTicket.Nom, ConsumerTicket.Cognom1, ConsumerTicket.Cognom2 ")
        sb.AppendLine("     FROM Alb INNER JOIN ConsumerTicket ON Alb.Guid = ConsumerTicket.Delivery  ")
        sb.AppendLine("     GROUP BY Alb.FraGuid, ConsumerTicket.Id, ConsumerTicket.Nom, ConsumerTicket.Cognom1, ConsumerTicket.Cognom2 ")
        sb.AppendLine(") X ON Fra.Guid = X.FraGuid ")
        sb.AppendLine("WHERE CliGral.Emp=" & oEmp.Id & " ")
        sb.AppendLine("AND Year(Fra.Fch)=" & oYearMonth.year & " ")
        sb.AppendLine("AND Month(Fra.Fch)=" & oYearMonth.month & " ")
        sb.AppendLine("ORDER BY Fra.Serie, Fra.Fra DESC ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oItem As New DTOInvoice(oDrd("Guid"))
            With oItem
                .Serie = oDrd("Serie")
                .Num = oDrd("Fra")
                .Fch = oDrd("Fch")
                .Customer = New DTOCustomer(oDrd("CliGuid"))
                If IsDBNull(oDrd("Ticket")) Then
                    .Customer.FullNom = oDrd("FullNom")
                Else
                    If IsDBNull(oDrd("TicketCognom2")) Then
                        .Customer.FullNom = String.Format("{0}, {1}", oDrd("TicketCognom1"), oDrd("TicketNom"))
                    Else
                        .Customer.FullNom = String.Format("{0} {1}, {2}", oDrd("TicketCognom1"), oDrd("TicketCognom2"), oDrd("TicketNom"))
                    End If
                End If
                .DocFile = SQLHelper.GetDocFileFromDataReaderHash(oDrd("Hash"))
                .Fpg = SQLHelper.GetStringFromDataReader(oDrd("Fpg"))
                .Total = SQLHelper.GetAmtFromDataReader(oDrd("EurLiq"))
                .SiiLog = SQLHelper.GetSiiLogFromDataReader(oDrd)
                .SiiL9 = SQLHelper.GetStringFromDataReader(oDrd("SiiL9"))
                .PrintMode = SQLHelper.GetIntegerFromDataReader(oDrd("PrintMode"))
                .TipoFactura = SQLHelper.GetStringFromDataReader(oDrd("TipoFactura"))
                .RegimenEspecialOTrascendencia = SQLHelper.GetStringFromDataReader(oDrd("RegimenEspecialOTrascendencia"))
                .Concepte = oDrd("Concepte")
            End With
            retval.Add(oItem)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function IntrastatPending(oEmp As DTOEmp, oYearMonth As DTOYearMonth) As List(Of DTOInvoice)
        Dim retval As New List(Of DTOInvoice)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Fra.Guid AS FraGuid, Fra.Fch AS FraFch, Fra.Fra AS FraId, CliGral.RaoSocial, CliGral.Nif ")
        sb.AppendLine(", Arc.Guid AS ArcGuid, Arc.Qty, Arc.Eur, Arc.Cur, Arc.Pts, Arc.Dto ")
        sb.AppendLine(", Arc.ArtGuid, VwSkuNom.*, MadeIn.Iso AS MadeInISO ")
        sb.AppendLine(", Pnc.PdcGuid ")
        sb.AppendLine(", Arc.AlbGuid, Alb.CliGuid AS AlbCliGuid ")
        sb.AppendLine(", VwZip.ZipGuid AS AlbZipGuid, VwZip.LocationGuid AS AlbLocationGuid, VwZip.ProvinciaGuid AS AlbProvinciaGuid, VwZip.ProvinciaIntrastat AS AlbProvinciaIntrastat,  VwZip.ZonaGuid AS AlbZonaGuid, Fra.ExportCod AS AlbExportCod, VwZip.CountryGuid AS AlbCountryGuid, VwZip.CountryISO AS AlbCountryISO, VwZip.ExportCod AS AlbCountryExportCod ")
        sb.AppendLine(", Fra.CliGuid, Fra.Incoterm ")
        sb.AppendLine(", VwAddress.ExportCod, VwAddress.CountryGuid, VwAddress.CountryISO, VwAddress.CEE ")
        sb.AppendLine("FROM Fra ")
        sb.AppendLine("INNER JOIN CliGral ON Fra.CliGuid = CliGral.Guid ")
        sb.AppendLine("INNER JOIN Alb ON Fra.Guid = Alb.FraGuid ")
        sb.AppendLine("INNER JOIN VwZip ON Alb.Zip = VwZip.ZipGuid ")
        sb.AppendLine("INNER JOIN Arc ON Alb.Guid = Arc.AlbGuid ")
        sb.AppendLine("INNER JOIN VwSkuNom ON Arc.ArtGuid = VwSkuNom.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN Country AS MadeIn ON VwSkuNom.MadeIn = MadeIn.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Pnc ON Arc.PncGuid = Pnc.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Pdc ON Pnc.PdcGuid = Pdc.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwAddress ON CliGral.Guid = VwAddress.SrcGuid ")
        sb.AppendLine("LEFT OUTER JOIN IntrastatPartida ON Fra.Guid = IntrastatPartida.Tag ")
        sb.AppendLine("WHERE Fra.Emp=" & oEmp.Id & " ")
        sb.AppendLine("AND Year(Fra.Fch)=" & oYearMonth.year & " ")
        sb.AppendLine("AND Month(Fra.Fch)=" & oYearMonth.month & " ")
        sb.AppendLine("AND Alb.Cod =" & DTOPurchaseOrder.Codis.client & " ")
        sb.AppendLine("AND Fra.ExportCod = " & DTOInvoice.ExportCods.intracomunitari & " ")
        sb.AppendLine("AND VwZip.CountryISO <> 'ES' ")
        sb.AppendLine("AND VwSkuNom.SkuNoStk = 0 ")
        sb.AppendLine("AND IntrastatPartida.Lin IS NULL ")
        sb.AppendLine("ORDER BY Fra.Fra, Alb.Alb, Arc.Lin ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)

        Dim oInvoice As New DTOInvoice
        Dim oDelivery As New DTODelivery
        Dim oOrder As New DTOPurchaseOrder
        Dim oSpv As New DTOSpv

        Do While oDrd.Read
            Try
                'If oDrd("FraId") = 8891 Then Stop
                If Not oInvoice.Guid.Equals(oDrd("FraGuid")) Then
                    oInvoice = New DTOInvoice(oDrd("FraGuid"))
                    With oInvoice
                        .Num = oDrd("FraId")
                        .Fch = oDrd("FraFch")
                        .Customer = New DTOCustomer(oDrd("CliGuid"))
                        .Customer.Nom = oDrd("RaoSocial")
                        .Customer.Nifs.Add(DTONif.Factory(DTONif.Cods.Nif, SQLHelper.GetStringFromDataReader(oDrd("Nif"))))
                        .Customer.Address = SQLHelper.GetAddressFromDataReader(oDrd, ZipGuidField:="AlbZipGuid", LocationGuidField:="AlbLocationGuid", ZonaGuidField:="AlbZonaGuid", CountryISOField:="AlbCountryIso")
                        .Deliveries = New List(Of DTODelivery)
                    End With

                    retval.Add(oInvoice)
                End If


                If Not oDelivery.Guid.Equals(oDrd("AlbGuid")) Then
                    oDelivery = New DTODelivery(oDrd("AlbGuid"))
                    With oDelivery
                        .Customer = New DTOCustomer(oDrd("AlbCliGuid"))
                        .Address = New DTOAddress
                        .Address.Zip = SQLHelper.GetZipFromDataReader(oDrd, "AlbZipGuid", LocationGuidField:="AlbLocationGuid", ProvinciaGuidField:="AlbProvinciaGuid", ProvinciaIntrastatField:="AlbProvinciaIntrastat", ZonaGuidField:="AlbZonaGuid", ZonaExportField:="AlbExportCod", CountryGuidField:="AlbCountryGuid", CountryISOField:="AlbCountryISO", ExportCodField:="AlbCountryExportCod")
                        .Items = New List(Of DTODeliveryItem)

                        .Invoice = oInvoice
                        oInvoice.ExportCod = oDrd("ExportCod")
                        oInvoice.Incoterm = SQLHelper.GetIncotermFromDataReader(oDrd("Incoterm"))
                    End With
                    oInvoice.Deliveries.Add(oDelivery)
                End If

                Dim oItem As New DTODeliveryItem(oDrd("ArcGuid"))
                Dim oSku As DTOProductSku = SQLHelper.GetProductFromDataReader(oDrd)
                If oSku.MadeIn IsNot Nothing Then
                    oSku.MadeIn.ISO = SQLHelper.GetStringFromDataReader(oDrd("MadeInISO"))
                End If

                'If oSku.MadeIn Is Nothing Then Stop

                If Not oOrder.Guid.Equals(oDrd("PdcGuid")) Then
                    If IsDBNull(oDrd("PdcGuid")) Then
                        oOrder = New DTOPurchaseOrder()
                    Else
                        oOrder = New DTOPurchaseOrder(oDrd("PdcGuid"))
                    End If
                End If

                Dim oPOItem As New DTOPurchaseOrderItem
                With oPOItem
                    .PurchaseOrder = oOrder
                    .Sku = oSku
                End With

                oItem.PurchaseOrderItem = oPOItem

                With oItem
                    .Sku = oSku
                    .Price = DTOAmt.Factory(CDec(oDrd("Eur")), oDrd("Cur").ToString, CDec(oDrd("Pts")))
                    .Dto = SQLHelper.GetDecimalFromDataReader(oDrd("Dto"))
                    .Qty = oDrd("Qty")
                    .Delivery = oDelivery

                End With
                oDelivery.Items.Add(oItem)

            Catch ex As Exception
                Stop
            End Try
        Loop

        oDrd.Close()

        Return retval
    End Function


    Shared Function All(oCustomer As DTOCustomer) As List(Of DTOInvoice)
        Dim retval As New List(Of DTOInvoice)
        Dim SQL As String = "Select Fra.Guid, Fra.Serie, Fra.Fra, Fra.Fch, Fra.EurLiq, Fra.EurBase, Fra.Cur, Cca.Hash, Fra.FchLastPrinted, Fra.PrintMode, Fra.Fpg " _
        & ", Fra.TipoFactura, Fra.SiiL9, Fra.SiiResult, Fra.Concepte " _
        & "FROM Fra " _
        & "INNER JOIN Cca On Fra.CcaGuid = Cca.Guid " _
        & "WHERE Fra.CliGuid=@Customer And Cca.Hash Is Not NULL " _
        & "ORDER BY Fra.Yea DESC, Fra.Fra DESC"

        Dim odrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Customer", oCustomer.Guid.ToString())
        Do While odrd.Read
            Dim oItem As New DTOInvoice(odrd("Guid"))
            With oItem
                .Serie = odrd("Serie")
                .Num = odrd("Fra")
                .Fch = odrd("Fch")
                .Customer = oCustomer
                If Not IsDBNull(odrd("Hash")) Then
                    .DocFile = New DTODocFile
                    .DocFile.hash = odrd("Hash")
                End If

                If Not IsDBNull(odrd("EurLiq")) Then
                    .Total = DTOAmt.Factory(CDec(odrd("EurLiq")))
                End If

                .Fpg = SQLHelper.GetStringFromDataReader(odrd("Fpg"))
                .PrintMode = odrd("PrintMode")
                .FchLastPrinted = SQLHelper.GetFchFromDataReader(odrd("FchLastPrinted"))
                .TipoFactura = SQLHelper.GetStringFromDataReader(odrd("TipoFactura"))
                .SiiL9 = SQLHelper.GetStringFromDataReader(odrd("SiiL9"))
                .SiiLog = SQLHelper.GetSiiLogFromDataReader(odrd)
                .Concepte = odrd("Concepte")
            End With
            retval.Add(oItem)
        Loop
        odrd.Close()
        Return retval
    End Function

    Shared Function Compact(Optional oUser As DTOUser = Nothing, Optional oCustomer As DTOCustomer = Nothing) As List(Of DTOInvoice.Compact)
        Dim retval As New List(Of DTOInvoice.Compact)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Fra.Guid, Fra.Serie, Fra.Fra, Fra.Fch, Fra.EurLiq, Fra.EurBase, Cca.Hash ")
        sb.AppendLine(", Fra.CliGuid, CliGral.RaoSocial ")
        sb.AppendLine("FROM Fra ")
        sb.AppendLine("INNER JOIN CliGral ON Fra.CliGuid = CliGral.Guid ")
        sb.AppendLine("INNER JOIN Cca ON Fra.CcaGuid = Cca.Guid ")
        If oUser Is Nothing Then
            sb.AppendLine("WHERE Fra.CliGuid = '" & oCustomer.Guid.ToString & "' ")
        ElseIf oCustomer Is Nothing Then
            sb.AppendLine("INNER JOIN Email_Clis ON Fra.CliGuid = Email_Clis.ContactGuid ")
            sb.AppendLine("WHERE Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
        Else
            sb.AppendLine("WHERE 1 = 2 ")
        End If
        sb.AppendLine("ORDER BY Fra.Fch DESC, Fra.Fra DESC ")

        Dim SQL = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oItem As New DTOInvoice.Compact()
            With oItem
                .Guid = oDrd("Guid")
                .Serie = oDrd("Serie")
                .Num = oDrd("Fra")
                .Fch = oDrd("Fch")
                .BaseImponible = DTOAmt.Compact.Factory(oDrd("EurBase"))
                .Total = DTOAmt.Compact.Factory(oDrd("EurLiq"))
                If Not IsDBNull(oDrd("Hash")) Then
                    .DocFile = DTODocFile.Compact.Factory(oDrd("Hash"))
                End If
                .Customer = DTOGuidNom.Compact.Factory(oDrd("CliGuid"), oDrd("RaoSocial"))
            End With
            retval.Add(oItem)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function DocFilesFromCustomer(oCustomer As DTOCustomer) As List(Of DTOInvoice)
        Dim retval As New List(Of DTOInvoice)
        Dim SQL As String = "Select Fra.Guid, Fra.Serie, Fra.Fra, Fra.Fch, Cca.Hash " _
        & "FROM Fra " _
        & "INNER JOIN Cca On Fra.CcaGuid = Cca.Guid " _
        & "WHERE Fra.CliGuid=@Customer And Cca.Hash Is Not NULL " _
        & "ORDER BY Fra.Yea DESC, Fra.Fra DESC"

        Dim odrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Customer", oCustomer.Guid.ToString())
        Do While odrd.Read
            Dim oItem As New DTOInvoice(odrd("Guid"))
            With oItem
                .Serie = odrd("Serie")
                .Num = odrd("Fra")
                .Fch = odrd("Fch")
                .Customer = oCustomer
                .DocFile = New DTODocFile
                .DocFile.hash = odrd("Hash")
            End With
            retval.Add(oItem)
        Loop
        odrd.Close()
        Return retval
    End Function

    Shared Function LlibreDeFactures(oExercici As DTOExercici, ToFch As Date) As List(Of DTOInvoice)
        Dim iCount As Integer
        Dim sb As New System.Text.StringBuilder

        sb.AppendLine("SELECT COUNT(Fra.Guid) AS FraCount ")
        sb.AppendLine("FROM  Fra ")
        sb.AppendLine("WHERE Fra.emp =" & oExercici.Emp.Id & " ")
        sb.AppendLine("AND Year(Fra.Fch) =" & oExercici.Year & " ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        oDrd.Read()
        iCount = SQLHelper.GetIntegerFromDataReader(oDrd("FraCount"))
        oDrd.Close()

        Dim retval As New List(Of DTOInvoice)
        sb = New System.Text.StringBuilder
        sb.AppendLine("SELECT Fra.Serie, Fra.Fra, Fra.Fch as FraFch, Fra.IvaStdPct, Fra.ReqStdPct, Fra.EurBase, Fra.IvaStdAmt, Fra.ReqStdAmt, Fra.EurLiq ")
        sb.AppendLine(", Fra.CliGuid, CliGral.RaoSocial, CliGral.Nif, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod ")
        sb.AppendLine(", Fra.CcaGuid, Cca.Cca, Cca.Hash ")
        sb.AppendLine("FROM Fra ")
        sb.AppendLine("INNER JOIN Cca ON Fra.CcaGuid = Cca.Guid ")
        sb.AppendLine("INNER JOIN CliGral ON Fra.CliGuid = CliGral.Guid ")
        sb.AppendLine("WHERE Fra.emp =" & oExercici.Emp.Id & " ")
        sb.AppendLine("AND Year(Fra.Fch) =" & oExercici.Year & " ")
        If ToFch <> Nothing Then
            sb.AppendLine("AND Fra.Fch<='" & Format(ToFch, "yyyyMMdd") & "' ")
        End If
        sb.AppendLine("ORDER BY Fra.Serie, Fra.Fra ")

        SQL = sb.ToString
        oDrd = SQLHelper.GetDataReader(SQL)
        Dim exs As New List(Of Exception)
        Do While oDrd.Read
            Dim oCustomer As DTOCustomer = Nothing
            If Not IsDBNull(oDrd("CliGuid")) Then
                oCustomer = New DTOCustomer(oDrd("CliGuid"))
                With oCustomer
                    .Nom = SQLHelper.GetStringFromDataReader(oDrd("RaoSocial"))
                    '.Nif = SQLHelper.GetStringFromDataReader(oDrd("Nif"))
                    .Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
                End With
            End If

            Dim oCca As New DTOCca(oDrd("CcaGuid"))
            With oCca
                .id = SQLHelper.GetIntegerFromDataReader(oDrd("Cca"))
                .fch = SQLHelper.GetFchFromDataReader(oDrd("FraFch"))
                .docFile = SQLHelper.GetDocFileFromDataReaderHash(oDrd("Hash"))
            End With

            'If oDrd("NIF").StartsWith("PT") Then Stop

            Dim oInvoice As New DTOInvoice()
            With oInvoice
                .Num = oDrd("Fra")
                .Serie = oDrd("Serie")
                .Fch = oDrd("FraFch")
                .Customer = oCustomer
                .Cca = oCca
                .BaseImponible = SQLHelper.GetAmtFromDataReader(oDrd("EurBase"))
                .IvaBaseQuotas = New List(Of DTOTaxBaseQuota)
                .Total = DTOAmt.Factory(CDec(oDrd("EurLiq")))

                If IsDBNull(oDrd("IvaStdPct")) Then
                    .TipoSujeccionIva = DTOInvoice.TiposSujeccionIva.sujetoExento
                    .IvaBaseQuotas.Add(New DTOTaxBaseQuota(DTOTax.Codis.exempt, .BaseImponible.Eur, 0))
                Else
                    Dim DcIvaTipus As Decimal = SQLHelper.GetDecimalFromDataReader(oDrd("IvaStdPct"))
                    If DcIvaTipus = 0 Then
                        .TipoSujeccionIva = DTOInvoice.TiposSujeccionIva.sujetoExento
                        .IvaBaseQuotas.Add(New DTOTaxBaseQuota(DTOTax.Codis.exempt, .BaseImponible.Eur, 0))
                    Else
                        .TipoSujeccionIva = DTOInvoice.TiposSujeccionIva.sujetoNoExento
                        .IvaBaseQuotas.Add(New DTOTaxBaseQuota(DTOTax.Codis.iva_Standard, .BaseImponible.Eur, DcIvaTipus, oDrd("IvaStdAmt")))

                        If Not IsDBNull(oDrd("ReqStdPct")) Then
                            Dim DcReqTipus As Decimal = SQLHelper.GetDecimalFromDataReader(oDrd("ReqStdPct"))
                            If DcReqTipus <> 0 Then
                                Dim oReq As New DTOTax
                                oReq.tipus = DcReqTipus
                                oReq.codi = DTOTax.Codis.recarrec_Equivalencia_Standard
                                .IvaBaseQuotas.Add(New DTOTaxBaseQuota(DTOTax.Codis.recarrec_Equivalencia_Standard, .BaseImponible.Eur, DcReqTipus, oDrd("ReqStdAmt")))
                            End If
                        End If

                    End If
                End If

            End With
            retval.Add(oInvoice)

        Loop
        Return retval
    End Function

    Shared Function PrintPending(oEmp As DTOEmp, Optional DtFch As Date = Nothing) As List(Of DTOInvoice)
        Dim retval As New List(Of DTOInvoice)
        Dim oSubscription = DTOSubscription.Wellknown(DTOSubscription.Wellknowns.Facturacio)
        Dim sb As New System.Text.StringBuilder

        sb.AppendLine("SELECT Fra.Guid, Fra.CliGuid ")
        sb.AppendLine(", Fra.Serie, Fra.Fra, Fra.Fch, Fra.EurLiq, CliGral.FullNom ")

        sb.AppendLine(", (CASE WHEN CliClient.FraPrintMode=" & DTOCustomer.FraPrintModes.edi & " THEN FraPrintMode ")
        sb.AppendLine("        WHEN Efras.ContactGuid IS NULL THEN " & DTOCustomer.FraPrintModes.printer & " ")
        sb.AppendLine("        ELSE " & DTOCustomer.FraPrintModes.email & " END) AS PrintMode ")

        sb.AppendLine("FROM  Fra ")
        sb.AppendLine("INNER JOIN CliGral ON Fra.CliGuid = CliGral.Guid ")
        sb.AppendLine("INNER JOIN CliClient ON Fra.CliGuid=CliClient.Guid ")
        sb.AppendLine("LEFT OUTER JOIN (SELECT Email_Clis.ContactGuid ")
        sb.AppendLine("					FROM Email_Clis ")
        sb.AppendLine("					INNER JOIN Email ON Email_Clis.EmailGuid = Email.Guid ")
        sb.AppendLine("					INNER JOIN SscEmail ON Email.GUID = SscEmail.Email AND SscEmail.SscGuid='" & oSubscription.Guid.ToString() & "' ")
        sb.AppendLine("					GROUP BY Email_Clis.ContactGuid) EfraS ON EfraS.ContactGuid=Fra.CliGuid ")
        sb.AppendLine("WHERE CliGral.Emp =" & oEmp.Id & " ")
        If DtFch = Nothing Then
            sb.AppendLine("And Fra.yea >= 2006 And Fra.PrintMode=0  And Fra.FchLastPrinted Is NULL ")
        Else
            sb.AppendLine("AND Fra.fch ='" & Format(DtFch, "yyyyMMdd HH:mm:ss") & "' ")
        End If
        sb.AppendLine("GROUP BY Fra.Guid, Fra.EurLiq, Fra.CliGuid, CliClient.FraPrintMode, Efras.ContactGuid, Fra.Yea ")
        sb.AppendLine(", Fra.Serie, Fra.Fra, Fra.Fch, Fra.EurLiq, CliGral.FullNom ")
        sb.AppendLine("ORDER BY Fra.yea, Fra.Fra ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oCustomer As New DTOCustomer(oDrd("CliGuid"))
            oCustomer.FullNom = oDrd("FullNom")

            Dim oItem As New DTOInvoice(oDrd("Guid"))
            With oItem
                .Serie = oDrd("Serie")
                .Num = oDrd("Fra")
                .Fch = oDrd("Fch")
                .Total = DTOAmt.Factory(oDrd("EurLiq"))
                .Customer = oCustomer
                .PrintMode = oDrd("PrintMode")
            End With
            retval.Add(oItem)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function PrintedCollection(oEmp As DTOEmp) As List(Of DTOInvoice)
        Dim retval As New List(Of DTOInvoice)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Fra.Guid, Fra.Fch, Fra.FchLastPrinted, Fra.Serie, Fra.Fra, Fra.UsrLastPrintedGuid, Fra.PrintMode ")
        sb.AppendLine(", Email.Adr, Email.Nickname, Fra.PrintMode ")
        sb.AppendLine(", Fra.CliGuid, CliGral.FullNom ")
        sb.AppendLine("FROM Fra  ")
        sb.AppendLine("INNER JOIN CliGral ON Fra.CliGuid = CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Email ON Fra.UsrLastPrintedGuid=Email.Guid ")
        sb.AppendLine("WHERE CliGral.Emp=" & oEmp.Id & " ")
        sb.AppendLine("AND year(Fra.Fch) >" & DTO.GlobalVariables.Today().Year - 2 & " ")
        sb.AppendLine("ORDER BY year(Fra.fch) DESC, fra.fch DESC ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOInvoice(oDrd("Guid"))
            With item
                .Serie = oDrd("Serie")
                .Num = oDrd("Fra")
                .Fch = SQLHelper.GetFchFromDataReader(oDrd("Fch"))
                .FchLastPrinted = SQLHelper.GetFchFromDataReader(oDrd("FchLastPrinted"))
                .PrintMode = oDrd("PrintMode")
                .Customer = New DTOCustomer(oDrd("CliGuid"))
                .Customer.FullNom = oDrd("FullNom")
                If Not IsDBNull(oDrd("UsrLastPrintedGuid")) Then
                    .UserLastPrinted = New DTOUser(DirectCast(oDrd("UsrLastPrintedGuid"), Guid))
                    With .UserLastPrinted
                        .emailAddress = SQLHelper.GetStringFromDataReader(oDrd("Adr"))
                        .nickName = SQLHelper.GetStringFromDataReader(oDrd("NickName"))
                    End With
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()

        Return retval
    End Function

    Shared Function SetNoPrint(oInvoices As List(Of DTOInvoice), oUser As DTOUser, exs As List(Of Exception)) As Boolean

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("UPDATE Fra SET ")
        sb.AppendLine("Fra.PrintMode = " & DTOInvoice.PrintModes.noPrint & " ")
        sb.AppendLine(", Fra.UsrLastPrintedGuid = '" & oUser.Guid.ToString & "' ")
        sb.AppendLine(", Fra.FchLastPrinted = GETDATE() ")
        sb.AppendLine("WHERE (")
        For Each item As DTOInvoice In oInvoices
            If item.UnEquals(oInvoices.First) Then
                sb.Append("OR ")
            End If
            sb.AppendLine("Fra.Guid = '" & item.Guid.ToString & "' ")
        Next
        sb.AppendLine(") ")

        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, exs)
        Dim retval As Boolean = exs.Count = 0

        Return retval
    End Function

    Shared Function Last(oExercici As DTOExercici, oSerie As DTOInvoice.Series) As DTOInvoice
        Dim retval As DTOInvoice = Nothing
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT TOP 1 Fra.Guid, Fra.Fch, Fra.Fra ")
        sb.AppendLine("FROM Fra ")
        sb.AppendLine("INNER JOIN CliGral ON FRA.CliGuid = CliGral.Guid ")
        sb.AppendLine("WHERE CliGral.Emp=" & oExercici.Emp.Id & " ")
        sb.AppendLine("AND Fra.Emp=" & oExercici.Emp.Id & " ")
        sb.AppendLine("AND Fra.Yea=" & oExercici.Year & " ")
        sb.AppendLine("AND Fra.Serie=" & oSerie & " ")
        sb.AppendLine("ORDER BY Year(Fra.Fch) DESC, Fra.Fra DESC ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOInvoice(oDrd("Guid"))
            With retval
                .Fch = oDrd("Fch")
                .Num = oDrd("Fra")
                .Serie = oSerie
            End With
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function LastEachSerie(oExercici As DTOExercici) As List(Of DTOInvoice)
        Dim retval As New List(Of DTOInvoice)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Fra.Guid, Fra.Fra, Fra.Serie, Fra.Fch ")
        sb.AppendLine("FROM Fra INNER JOIN ( ")
        sb.AppendLine("SELECT Fra.Emp, Fra.serie, Fra.Yea, MAX(Fra.Fra) AS LastFra ")
        sb.AppendLine("FROM Fra ")
        sb.AppendLine("WHERE Fra.Emp=" & oExercici.Emp.Id & " AND Fra.Yea=" & oExercici.Year & " ")
        'sb.AppendLine("AND Fra.Fra<3788 ") 'PROVISIONAL =============================================================
        sb.AppendLine("GROUP BY Fra.Emp, Fra.Serie, Fra.Yea) X ON Fra.Emp = X.Emp AND Fra.Yea=X.Yea AND Fra.Serie = X.Serie AND Fra.Fra = X.LastFra ")
        sb.AppendLine("ORDER BY Fra.Serie ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oInvoice As New DTOInvoice(oDrd("Guid"))
            With oInvoice
                .Fch = oDrd("Fch")
                .Num = oDrd("Fra")
                .Serie = oDrd("Serie")
            End With
            retval.Add(oInvoice)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function SiiPending(oEmp As DTOEmp) As List(Of DTOInvoice)
        Dim retval As New List(Of DTOInvoice)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Fra.Guid, Fra.Serie, Fra.Fra, Fra.Fch ")
        sb.AppendLine("FROM Fra ")
        sb.AppendLine("WHERE Fra.Emp=" & oEmp.Id & " ")
        sb.AppendLine("AND Fra.Yea >= 2017 ")
        sb.AppendLine("AND Fra.SiiCsv IS NULL ")
        'sb.AppendLine("AND Fra.SiiResult<>1 ")
        sb.AppendLine("ORDER BY Year(Fra.Fch), Fra.Fra ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOInvoice(oDrd("Guid"))
            With item
                .Serie = oDrd("Serie")
                .Num = oDrd("Fra")
                .Fch = oDrd("Fch")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Update(exs As List(Of Exception), oInvoices As List(Of DTOInvoice), Optional Progress As ProgressBarHandler = Nothing) As Boolean
        Dim retval As Boolean
        Dim oInvoice As DTOInvoice = Nothing

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction

        For Each oInvoice In oInvoices
            Try
                InvoiceLoader.Update(oInvoice, oTrans)
            Catch ex As Exception
                Dim sMessage As String = String.Format("error amb la factura {0} de {1} per {2:N2}€", oInvoice.Num, oInvoice.Customer.Nom, oInvoice.Total.Eur)
                exs.Add(New Exception(sMessage))
                exs.Add(ex)
            Finally
                If Progress IsNot Nothing Then
                    Dim sText As String = String.Format("desant factura {0} per {1:N2}€ a {2}", oInvoice.Num, oInvoice.Total.Eur, oInvoice.Customer.FullNom)
                    Progress(0, oInvoices.Count, oInvoices.IndexOf(oInvoice), sText, CancelRequest:=False)
                End If
            End Try
        Next

        If exs.Count = 0 Then
            oTrans.Commit()
            retval = True
        Else
            oTrans.Rollback()
        End If

        oConn.Close()

        Return retval
    End Function

End Class