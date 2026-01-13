Public Class DeliveryLoader

    Shared Function Find(oGuid As Guid) As DTODelivery
        Dim retval As DTODelivery = Nothing
        Dim oDelivery As New DTODelivery(oGuid)
        If Load(oDelivery) Then
            retval = oDelivery
        End If
        Return retval
    End Function

    Shared Function FromNum(oEmp As DTOEmp, iYear As Integer, iNum As Integer) As DTODelivery
        Dim retval As DTODelivery = Nothing
        Dim SQL As String = "SELECT Guid FROM Alb WHERE Alb.Emp=" & oEmp.Id & " AND Alb.Yea=" & iYear & " AND Alb.Alb=" & iNum
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTODelivery(oDrd("Guid"))
        End If
        oDrd.Close()

        If retval IsNot Nothing Then
            Load(retval)
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oDelivery As DTODelivery) As Boolean
        If Not oDelivery.IsLoaded And Not oDelivery.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Alb.Emp, Alb.Alb, Alb.Fch, Alb.Cod, Alb.CliGuid, Alb.Nom, Alb.Adr, Alb.Zip, Alb.Incoterm, Alb.ExportCod, VwZip.*, Alb.Tel ")
            sb.AppendLine(", Alb.MgzGuid, Mgz.Gln AS MgzGln, Mgz.RaoSocial AS MgzRaoSocial, Mgz.NomCom AS MgzNomCom, Mgz.Alias AS MgzAlias ")
            sb.AppendLine(", MgzAdr.Adr AS MgzAdr, MgzAdr.ZipGuid AS MgzZipGuid, MgzAdr.ZipCod AS MgzZipCod, MgzAdr.LocationGuid AS MgzLocationGuid, MgzAdr.LocationNom AS MgzLocationNom, MgzAdr.ZonaGuid AS MgzZonaGuid, MgzAdr.ZonaNom AS MgzZonaNom, MgzAdr.CountryGuid AS MgzCountryGuid, MgzAdr.CountryISO AS MgzCountryISO ") 'afegit per Anazon Desadv
            sb.AppendLine(", Alb.CashCod, Alb.PortsCod, Alb.Cobro, Alb.Valorado, Alb.Pts AS AlbPts, Alb.Cur AS AlbCur, Alb.Eur AS AlbEur ")
            sb.AppendLine(", Alb.Bultos, Alb.Kgs, Alb.m3, Alb.EtiquetesTransport ")
            sb.AppendLine(", Alb.IvaExempt, Alb.Pt2, Alb.Facturable, Alb.FacturarA, FacturarAFullNom.FullNom AS FacturarANom, Alb.RetencioCod, Alb.TrpGuid, Alb.Fpg, Alb.Obs, Alb.ObsTransp, Alb.CustomerDocUrl ")
            sb.AppendLine(", CliClient.Iva, CliClient.Req, CliGral.ContactClass, CliClient.Ref AS ClientRef, CliClient.SuProveedorNum, CliClient.AlbVal AS CustomerAlbValorat ")
            sb.AppendLine(", CliClient.SuProveedorNum ")
            sb.AppendLine(", CliClient.CcxGuid, Ccx.SuProveedorNum AS CcxSuProveedorNum ")
            sb.AppendLine(", Arc.Guid AS ArcGuid, Arc.Lin, Arc.PncGuid, Arc.PdcGuid, Pdc.Pdc, Pdc.Fch AS PdcFch, Pdc.FchMin AS PdcFchMin, Pdc.Pdd, Pdc.Nadms, Pdc.Obs AS PdcObs, Pdc.Cod AS PdcCod ")
            sb.AppendLine(", Arc.ArtGuid, Arc.Qty, Arc.Eur, Arc.Cur, Arc.Pts, Arc.Dto, Arc.Cod AS ArcCod, Arc.Bundle, Arc.MgzGuid AS ArcMgzGuid")
            sb.AppendLine(", Arc.RepGuid, Arc.Com, Arc.RepComLiquidable ")
            sb.AppendLine(", Arc.SpvGuid, Spv.Id AS SpvId, Spv.FchAvis, Spv.Garantia, Spv.Contacto AS SpvContacto, Spv.sRef AS SpvSRef, Spv.Serial AS SpvSerial ")
            sb.AppendLine(", Spv.ProductGuid AS SpvProductGuid, Spv.ObsTecnic ")
            sb.AppendLine(", Spv.SpvIn, SpvIn.fch AS SpvInFch ")
            sb.AppendLine(", Spv.Incidencia, Incidencies.Id AS IncidenciaId, Incidencies.Asin AS IncidenciaAsin, Incidencies.Fch AS IncidenciaFch ")
            sb.AppendLine(", VwProductNom.BrandGuid AS SpvBrandGuid, VwProductNom.BrandNom AS SpvBrandNom ")
            sb.AppendLine(", VwProductNom.CategoryGuid AS SpvCategoryGuid, VwProductNom.CategoryNom AS SpvCategoryNom ")
            sb.AppendLine(", VwProductNom.SkuGuid AS SpvSkuGuid, VwProductNom.SkuNom AS SpvSkuNom ")
            sb.AppendLine(", VwSkuNom.*, MadeIn.Iso AS MadeInISO ")
            sb.AppendLine(", CliRep.Abr AS RepNickname ")
            sb.AppendLine(", CliGral.LangId, CliGral.GLN, CliGral.FullNom AS CustomerFullNom ")
            sb.AppendLine(", Alb.TransmGuid, Transm.Transm, Transm.Fch as TransmFch ")
            sb.AppendLine(", Alb.FchCreated, Alb.UsrCreatedGuid, UsrCreated.Nom AS UsrCreatedNickname ")
            sb.AppendLine(", Alb.FchLastEdited, Alb.UsrLastEditedGuid, UsrLastEdited.Nom AS UsrLastEditedNickname ")
            sb.AppendLine(", Alb.FraGuid, Fra.Fra, Fra.Serie, Fra.Fch as FraFch, Cca.Hash AS FraHash ")
            sb.AppendLine(", Alb.PlatformGuid, PlatfGral.FullNom AS PlatfFullNom, PlatfGral.GLN AS PlatformGln ")
            sb.AppendLine(", ConsumerTicket.Guid AS ConsumerGuid, ConsumerTicket.Nom AS ConsumerNom, ConsumerTicket.Cognom1, ConsumerTicket.Cognom2, ConsumerTicket.Nif AS  ConsumerNif ")
            sb.AppendLine("FROM Alb ")
            sb.AppendLine("LEFT OUTER JOIN Arc ON Alb.Guid=Arc.AlbGuid ")
            sb.AppendLine("LEFT OUTER JOIN CliRep ON Arc.RepGuid=CliRep.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Pdc ON Arc.PdcGuid=Pdc.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Pnc ON Arc.PncGuid=Pnc.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Spv ON Arc.SpvGuid=Spv.Guid ")
            sb.AppendLine("LEFT OUTER JOIN SpvIn ON Spv.SpvIn=SpvIn.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Incidencies ON Spv.Incidencia=Incidencies.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwProductNom ON Spv.ProductGuid=VwProductNom.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Fra ON Alb.FraGuid=Fra.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Cca ON Fra.CcaGuid = Cca.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CliGral Mgz ON Alb.MgzGuid=Mgz.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwUsrNickname AS UsrCreated ON dbo.Alb.UsrCreatedGuid = UsrCreated.guid ")
            sb.AppendLine("LEFT OUTER JOIN VwUsrNickname AS UsrLastEdited ON dbo.Alb.UsrLastEditedGuid = UsrLastEdited.guid ")
            sb.AppendLine("LEFT OUTER JOIN VwSkuNom ON Arc.ArtGuid=VwSkuNom.SkuGuid ")
            sb.AppendLine("LEFT OUTER JOIN Country AS MadeIn ON VwSkuNom.MadeIn = MadeIn.Guid ")
            sb.AppendLine("INNER JOIN CliGral ON Alb.CliGuid=CliGral.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CliClient ON Alb.CliGuid=CliClient.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CliClient Ccx ON CliClient.CcxGuid = Ccx.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CliGral AS FacturarAFullNom ON Alb.FacturarA=FacturarAFullNom.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CliGral AS PlatfGral ON Alb.PlatformGuid=PlatfGral.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Transm ON Alb.TransmGuid=Transm.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwZip ON Alb.Zip=VwZip.ZipGuid ")
            sb.AppendLine("LEFT OUTER JOIN VwAddress MgzAdr ON Alb.MgzGuid = MgzAdr.SrcGuid ")
            sb.AppendLine("LEFT OUTER JOIN ConsumerTicket ON Alb.Guid = ConsumerTicket.Delivery ")
            sb.AppendLine("WHERE Alb.Guid='" & oDelivery.Guid.ToString & "' ")
            sb.AppendLine("ORDER BY Arc.Lin, Arc.Cod desc")

            Dim oCategory As New DTOProductCategory
            Dim oBrand As New DTOProductBrand
            Dim oOrder As New DTOPurchaseOrder
            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                If Not oDelivery.IsLoaded Then
                    Dim oInvoice As DTOInvoice = Nothing
                    If Not IsDBNull(oDrd("FraGuid")) Then
                        oInvoice = New DTOInvoice(oDrd("FraGuid"))
                        With oInvoice
                            .Num = oDrd("Fra")
                            .Serie = oDrd("Serie")
                            .Fch = oDrd("FraFch")
                            If Not IsDBNull(oDrd("FraHash")) Then
                                .DocFile = New DTODocFile(oDrd("FraHash"))
                            End If
                        End With

                    End If

                    With oDelivery
                        If .Emp Is Nothing Then .Emp = New DTOEmp(oDrd("Emp"))
                        .Id = oDrd("Alb")
                        .Fch = oDrd("Fch")
                        .Cod = oDrd("Cod")
                        Select Case .Cod
                            Case DTOPurchaseOrder.Codis.proveidor
                                .Proveidor = New DTOProveidor(oDrd("CliGuid"))
                                With .Proveidor
                                    If Not IsDBNull(oDrd("ContactClass")) Then
                                        .ContactClass = New DTOContactClass(oDrd("ContactClass"))
                                    End If
                                    .Emp = oDelivery.Emp
                                    .FullNom = oDrd("CustomerFullNom")
                                    .Lang = DTOLang.Factory(oDrd("LangId"))
                                    .GLN = SQLHelper.GetEANFromDataReader(oDrd("Gln"))
                                    .Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                                    .Address = New DTOAddress
                                    .Address.Text = SQLHelper.GetStringFromDataReader(oDrd("Adr"))
                                    .Address.Zip = SQLHelper.GetZipFromDataReader(oDrd)
                                End With
                            Case Else
                                .Customer = New DTOCustomer(oDrd("CliGuid"))
                                With .Customer
                                    .Emp = oDelivery.Emp
                                    If Not IsDBNull(oDrd("ContactClass")) Then
                                        .ContactClass = New DTOContactClass(oDrd("ContactClass"))
                                    End If
                                    .FullNom = oDrd("CustomerFullNom")
                                    .Lang = DTOLang.Factory(oDrd("LangId"))
                                    .GLN = SQLHelper.GetEANFromDataReader(oDrd("Gln"))
                                    .Iva = SQLHelper.GetBooleanFromDatareader(oDrd("Iva"))
                                    .Req = SQLHelper.GetBooleanFromDatareader(oDrd("Req"))
                                    .Nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
                                    .Address = New DTOAddress
                                    .Address.Text = SQLHelper.GetStringFromDataReader(oDrd("Adr"))
                                    .Address.Zip = SQLHelper.GetZipFromDataReader(oDrd)
                                    .Ref = SQLHelper.GetStringFromDataReader(oDrd("ClientRef"))
                                    .SuProveedorNum = SQLHelper.GetStringFromDataReader(oDrd("SuProveedorNum"))
                                    .AlbValorat = SQLHelper.GetBooleanFromDatareader(oDrd("CustomerAlbValorat"))
                                    .SuProveedorNum = SQLHelper.GetStringFromDataReader(oDrd("SuProveedorNum"))
                                    If Not IsDBNull(oDrd("CcxGuid")) Then
                                        .Ccx = New DTOCustomer(oDrd("CcxGuid"))
                                        .Ccx.SuProveedorNum = SQLHelper.GetStringFromDataReader(oDrd("CcxSuProveedorNum"))
                                    End If
                                End With
                        End Select

                        .Nom = .Contact.Nom
                        .Address = .Contact.Address
                        .Tel = SQLHelper.GetStringFromDataReader(oDrd("Tel"))
                        .CashCod = oDrd("CashCod")
                        .Import = DTOAmt.Factory(CDec(oDrd("AlbEur")), oDrd("AlbCur").ToString, CDec(oDrd("AlbPts")))
                        .Valorado = oDrd("Valorado")
                        .RetencioCod = oDrd("RetencioCod")
                        .PortsCod = oDrd("PortsCod")
                        .IvaExempt = oDrd("IvaExempt")
                        .Facturable = oDrd("Facturable")
                        .Recarrec = SQLHelper.GetAmtFromDataReader(oDrd("Pt2"))
                        .Bultos = oDrd("Bultos")
                        .Kg = oDrd("Kgs")
                        .M3 = oDrd("M3")
                        .ExportCod = SQLHelper.GetIntegerFromDataReader(oDrd("ExportCod"))
                        .Incoterm = SQLHelper.GetIncotermFromDataReader(oDrd("Incoterm"))

                        If Not IsDBNull(oDrd("MgzGuid")) Then
                            .Mgz = New DTOMgz(oDrd("MgzGuid"))
                            .Mgz.Emp = oDelivery.Emp
                            If IsDBNull(oDrd("MgzAlias")) OrElse String.IsNullOrEmpty(oDrd("MgzAlias")) Then
                                If IsDBNull(oDrd("MgzNomCom")) OrElse String.IsNullOrEmpty(oDrd("MgzNomCom")) Then
                                    .Mgz.abr = SQLHelper.GetStringFromDataReader(oDrd("MgzRaoSocial"))
                                Else
                                    .Mgz.abr = SQLHelper.GetStringFromDataReader(oDrd("MgzNomCom"))
                                End If
                            Else
                                .Mgz.abr = SQLHelper.GetStringFromDataReader(oDrd("MgzAlias"))
                            End If
                            .Mgz.FullNom = .Mgz.abr
                            .Mgz.GLN = DTOEan.Factory(oDrd("MgzGln"))
                            .Mgz.Address = SQLHelper.GetAddressFromDataReader(oDrd, AdrField:="MgzAdr", ZipGuidField:="MgzZipGuid", ZipCodField:="MgzZipCod", LocationGuidField:="MgzLocationGuid", LocationNomField:="MgzLocationNom", ZonaGuidField:="MgzZonaGuid", ZonaNomField:="MgzZonaNom", CountryGuidField:="MgzCountryGuid", CountryISOField:="MgzCountryISO")
                        End If

                        If Not IsDBNull(oDrd("TransmGuid")) Then
                            .Transmisio = New DTOTransmisio(oDrd("TransmGuid"))
                            .Transmisio.Id = oDrd("Transm")
                            .Transmisio.Fch = SQLHelper.GetDateTimeOffsetFromDataReader(oDrd("TransmFch"))
                        End If

                        If Not IsDBNull(oDrd("TrpGuid")) Then
                            .Transportista = New DTOTransportista(oDrd("TrpGuid"))
                        End If

                        If Not IsDBNull(oDrd("PlatformGuid")) Then
                            .Platform = New DTOCustomerPlatform(oDrd("PlatformGuid"))
                            .Platform.FullNom = SQLHelper.GetStringFromDataReader(oDrd("PlatfFullNom"))
                            .Platform.GLN = DTOEan.Factory(SQLHelper.GetStringFromDataReader(oDrd("PlatformGln")))
                        End If

                        If Not IsDBNull(oDrd("FacturarA")) Then
                            .FacturarA = New DTOCustomer(oDrd("FacturarA"))
                            .FacturarA.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FacturarANom"))
                        End If

                        .FchCobroReembolso = SQLHelper.GetFchFromDataReader(oDrd("Cobro"))
                        .Invoice = oInvoice

                        If Not IsDBNull(oDrd("Fpg")) Then
                            '.PaymentTerms = PaymentTermsLoader.Factory(SQLHelper.GetStringFromDataReader(oDrd("Fpg")))
                        End If

                        If Not IsDBNull(oDrd("ConsumerGuid")) Then
                            .ConsumerTicket = New DTOConsumerTicket(oDrd("ConsumerGuid"))
                            With .ConsumerTicket
                                .Nom = SQLHelper.GetStringFromDataReader(oDrd("ConsumerNom"))
                                .Cognom1 = SQLHelper.GetStringFromDataReader(oDrd("Cognom1"))
                                .Cognom2 = SQLHelper.GetStringFromDataReader(oDrd("Cognom2"))
                                .Nif = SQLHelper.GetStringFromDataReader(oDrd("ConsumerNif"))
                            End With
                        End If
                        .CustomerDocURL = SQLHelper.GetStringFromDataReader(oDrd("CustomerDocURL"))
                        .EtiquetesTransport = SQLHelper.GetDocFileFromDataReaderHash(oDrd("EtiquetesTransport"))
                        .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                        .ObsTransp = SQLHelper.GetStringFromDataReader(oDrd("ObsTransp"))
                        .UsrLog = SQLHelper.getUsrLog2FromDataReader(oDrd, UsrCreatedField:="UsrCreatedGuid", UsrLastEditedField:="UsrLastEditedGuid")
                        .Items = New List(Of DTODeliveryItem)
                        .IsLoaded = True
                    End With
                End If



                If Not IsDBNull(oDrd("ArcGuid")) Then
                    If Not IsDBNull(oDrd("PdcGuid")) Then
                        If Not oOrder.Guid.Equals(oDrd("PdcGuid")) Then
                            oOrder = New DTOPurchaseOrder(oDrd("PdcGuid"))
                            With oOrder
                                .Fch = SQLHelper.GetFchFromDataReader(oDrd("PdcFch"))
                                .FchDeliveryMin = SQLHelper.GetFchFromDataReader(oDrd("PdcFchMin"))
                                .Num = SQLHelper.GetIntegerFromDataReader(oDrd("Pdc"))
                                .Concept = SQLHelper.GetStringFromDataReader(oDrd("Pdd"))
                                .Obs = SQLHelper.GetStringFromDataReader(oDrd("PdcObs"))
                                .Cod = SQLHelper.GetIntegerFromDataReader(oDrd("PdcCod"))
                                .NADMS = SQLHelper.GetStringFromDataReader(oDrd("NADMS"))
                            End With
                        End If
                    End If

                    Dim oSku As DTOProductSku = SQLHelper.GetProductFromDataReader(oDrd)
                    If oSku.MadeIn IsNot Nothing Then
                        oSku.MadeIn.ISO = SQLHelper.GetStringFromDataReader(oDrd("MadeInISO"))
                    End If

                    Dim oPnc As DTOPurchaseOrderItem = Nothing
                    If Not IsDBNull(oDrd("PncGuid")) Then
                        oPnc = New DTOPurchaseOrderItem(oDrd("PncGuid"))
                        With oPnc
                            .PurchaseOrder = oOrder
                            .Sku = oSku
                        End With
                    End If

                    Dim oSpv As DTOSpv = Nothing
                    If Not IsDBNull(oDrd("SpvGuid")) Then
                        oSpv = New DTOSpv(oDrd("SpvGuid"))
                        With oSpv
                            .id = oDrd("SpvId")
                            .customer = oDelivery.Customer
                            .fchAvis = oDrd("FchAvis")
                            .spvIn = New DTOSpvIn(oDrd("SpvIn"))
                            .spvIn.fch = SQLHelper.GetFchFromDataReader(oDrd("SpvInFch"))
                            .garantia = oDrd("Garantia")
                            .contacto = SQLHelper.GetStringFromDataReader(oDrd("SpvContacto"))
                            .sRef = SQLHelper.GetStringFromDataReader(oDrd("SpvSref"))
                            .product = SQLHelper.GetProductFromDataReader(oDrd, brandGuidField:="SpvBrandGuid",
                                                                                brandNomField:="SpvBrandNom",
                                                                                categoryGuidField:="SpvCategoryGuid",
                                                                                categoryNomField:="SpvCategoryNom",
                                                                                skuGuidField:="SpvSkuGuid",
                                                                                skuNomField:="SpvSkuNom",
                                                                                skuNomLlargField:="NoNomLlarg",
                                                                                skuNomLlargEspField:="NoNomLlargEsp")
                            If Not IsDBNull(oDrd("Incidencia")) Then
                                .incidencia = New DTOIncidencia(oDrd("Incidencia"))
                                With .incidencia
                                    .Num = SQLHelper.GetIntegerFromDataReader(oDrd("IncidenciaId"))
                                    .Asin = SQLHelper.GetStringFromDataReader(oDrd("IncidenciaAsin"))
                                    .Fch = SQLHelper.GetFchFromDataReader(oDrd("IncidenciaFch"))
                                End With
                            End If
                            .serialNumber = SQLHelper.GetStringFromDataReader(oDrd("SpvSerial"))
                            .obsTecnic = SQLHelper.GetStringFromDataReader(oDrd("ObsTecnic"))
                        End With
                    End If

                    Dim oItem As New DTODeliveryItem(oDrd("ArcGuid"))
                    With oItem
                        .Lin = oDrd("Lin")
                        .Cod = oDrd("ArcCod")
                        .Qty = oDrd("Qty")
                        .Sku = oSku
                        .Price = SQLHelper.GetAmtFromDataReader2(oDrd, "Eur", "Cur", "Pts")
                        '.Price = DTOAmt.Factory(CDec(oDrd("Eur")), oDrd("Cur").ToString, CDec(oDrd("Pts")))
                        .Dto = SQLHelper.GetDecimalFromDataReader(oDrd("Dto"))
                        .PurchaseOrderItem = oPnc
                        .Spv = oSpv
                        If Not IsDBNull(oDrd("RepGuid")) Then
                            .RepCom = New DTORepCom
                            With .RepCom
                                .Rep = New DTORep(oDrd("RepGuid"))
                                .Rep.NickName = SQLHelper.GetStringFromDataReader(oDrd("RepNickname"))
                                .Com = SQLHelper.GetDecimalFromDataReader(oDrd("Com"))
                            End With
                        End If
                        If Not IsDBNull(oDrd("RepComLiquidable")) Then
                            .RepComLiquidable = New DTORepComLiquidable(oDrd("RepComLiquidable"))
                        End If
                        .Mgz = New DTOMgz(oDrd("ArcMgzGuid"))
                    End With

                    If isBundleChild(oDrd) Then
                        Dim oBundleParent = BundleParent(oDelivery, oDrd("Bundle"))
                        oBundleParent.Bundle.Add(oItem)
                    Else
                        oDelivery.Items.Add(oItem)
                    End If
                End If
            Loop

            oDrd.Close()

            LoadPackages(oDelivery)
        End If

        Dim retval As Boolean = oDelivery.IsLoaded
        Return retval
    End Function

    Public Shared Sub LoadPackages(ByRef oDelivery As DTODelivery)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT AlbGuid, Fch, Expedition, Packages, Pallet, Package, SSCC, ArcGuid, Qty, SkuGuid, SkuEan, lin ")
        sb.AppendLine(", TrpNif, TrpGuid, TrpNom, Tracking, TrackingUrlTemplate, CubicKg, Weight, Volume, Cost, Packaging, Length, Width, Height ")
        sb.AppendLine("FROM VwDeliveryShipment ")
        sb.AppendLine("WHERE AlbGuid='" & oDelivery.Guid.ToString() & "' ")
        sb.AppendLine("ORDER BY Pallet, Package, Lin")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oPallet As New DTODelivery.Pallet(0, "fake Pallet")
        Dim oPackage As New DTODelivery.Package(0, "fake SSCC")
        Dim oItem As DTODeliveryItem = Nothing
        Dim cps As Integer = 1
        Dim firstRec As Boolean = True
        oDelivery.Pallets = New List(Of DTODelivery.Pallet)
        oDelivery.Packages = New List(Of DTODelivery.Package)

        Do While oDrd.Read
            If Not IsDBNull(oDrd("Expedition")) Then
                If firstRec Then
                    If IsDBNull(oDrd("TrpGuid")) Then
                        'no podem aplicar les següents dues linies perque trencarien la restricció relacional amb el transportista
                        'oDelivery.Transportista = New DTOTransportista()
                        'oDelivery.Transportista.Nom = SQLHelper.GetStringFromDataReader(oDrd("TrpNif"))
                    Else
                        oDelivery.Transportista = New DTOTransportista(oDrd("TrpGuid"))
                        oDelivery.Transportista.Nom = SQLHelper.GetStringFromDataReader(oDrd("TrpNom"))
                        oDelivery.Transportista.TrackingUrlTemplate = SQLHelper.GetStringFromDataReader(oDrd("TrackingUrlTemplate"))
                    End If
                    oDelivery.Tracking = SQLHelper.GetStringFromDataReader(oDrd("Tracking"))
                    oDelivery.Bultos = SQLHelper.GetIntegerFromDataReader(oDrd("Packages"))
                    oDelivery.Kg = SQLHelper.GetDecimalFromDataReader(oDrd("Weight"))
                    'oDelivery.TrackingUrl = oDelivery.GetTrackingUrl
                    firstRec = False
                End If
                If Not IsDBNull(oDrd("Pallet")) Then
                    'expedició no palletitzada
                    If oDrd("SSCC") <> oPackage.SSCC Then
                        cps += 1
                        oPackage = New DTODelivery.Package(cps, oDrd("SSCC"))
                        With oPackage
                            .Expedition = oDrd("Expedition")
                            .Fch = oDrd("Fch")
                            .Num = SQLHelper.GetIntegerFromDataReader(oDrd("Package"))
                            .Packaging = SQLHelper.GetStringFromDataReader(oDrd("Packaging"))
                            .Length = SQLHelper.GetIntegerFromDataReader(oDrd("Length"))
                            .Width = SQLHelper.GetIntegerFromDataReader(oDrd("Width"))
                            .Height = SQLHelper.GetIntegerFromDataReader(oDrd("Height"))
                        End With
                        oDelivery.Packages.Add(oPackage)
                    End If
                    oItem = oDelivery.Items.FirstOrDefault(Function(x) x.Lin = oDrd("Lin"))
                    oPackage.AddItem(oDrd("Qty"), oItem)
                Else
                    'expedició palletitzada
                    If Not IsDBNull(oDrd("Pallet")) AndAlso oDrd("Pallet") <> oPallet.SSCC Then
                        cps += 1
                        oPallet = New DTODelivery.Pallet(cps, oDrd("Pallet"))
                        With oPallet
                            .Expedition = oDrd("Expedition")
                            .Fch = oDrd("Fch")
                        End With
                        oDelivery.Pallets.Add(oPallet)
                        oPackage = New DTODelivery.Package(0, "fake SSCC")
                    End If
                    If Not IsDBNull(oDrd("SSCC")) AndAlso oDrd("SSCC") <> oPackage.SSCC Then
                        cps += 1
                        oPackage = New DTODelivery.Package(cps, oDrd("SSCC"))
                        With oPackage
                            .Expedition = oDrd("Expedition")
                            .Fch = oDrd("Fch")
                        End With
                        oPallet.Packages.Add(oPackage)
                    End If
                    oItem = oDelivery.Items.FirstOrDefault(Function(x) x.Lin = oDrd("Line"))
                    oPackage.AddItem(oDrd("Qty"), oItem)
                End If
            End If
        Loop
        oDrd.Close()
    End Sub

    Public Shared Function isBundleChild(odrd As SqlDataReader) As Boolean
        Dim retval As Boolean
        If Not IsDBNull(odrd("Bundle")) Then
            Dim oParentGuid As Guid = odrd("Bundle")
            Dim oChildGuid As Guid = odrd("ArcGuid")
            retval = Not oParentGuid.Equals(oChildGuid)
        End If
        Return retval
    End Function

    Public Shared Function BundleParent(oDelivery As DTODelivery, oGuid As Guid) As DTODeliveryItem
        Dim retval = oDelivery.Items.First(Function(x) x.Guid.Equals(oGuid))
        Return retval
    End Function

    Shared Function Update(ByRef oDelivery As DTODelivery, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oDelivery, oTrans)
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

    Shared Sub Update(ByRef oDelivery As DTODelivery, ByRef oTrans As SqlTransaction)
        If oDelivery IsNot Nothing Then
            UpdateHeader(oDelivery, oTrans)

            If oDelivery.IsNew Then
                oDelivery.EnumerateLines()
            Else
                RecuperaPendents(oDelivery, oTrans)
                DeleteItems(oDelivery, oTrans)
            End If

            UpdateItems(oDelivery, oTrans)
            UpdatePendents(oDelivery, oTrans)
            UpdateSpvs(oDelivery, oTrans)

            oDelivery.IsNew = False
        End If
    End Sub

    Shared Sub UpdateHeader(ByRef oDelivery As DTODelivery, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM Alb WHERE Guid='" & oDelivery.Guid.ToString & "' "

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oDelivery.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        If oDelivery.Id = 0 Then oDelivery.Id = LastId(oDelivery, oTrans) + 1

        With oDelivery
            oRow("Emp") = .Emp.Id
            oRow("Yea") = .Fch.Year
            oRow("Alb") = .Id
            oRow("Cod") = .Cod
            oRow("Fch") = .Fch
            oRow("CliGuid") = SQLHelper.NullableBaseGuid(.Contact)
            oRow("IvaExempt") = .IvaExempt

            oRow("FraGuid") = SQLHelper.NullableBaseGuid(.Invoice)

            'Dim oImport As DTOAmt = oDelivery.totalCash 'Total(oDelivery)
            SQLHelper.SetNullableAmt(.Import, oRow, "Eur", "Cur", "Pts")
            oRow("Pt2") = SQLHelper.NullableAmt(.Recarrec)
            oRow("MgzGuid") = SQLHelper.NullableBaseGuid(.Mgz)
            oRow("Bultos") = .Bultos
            oRow("Kgs") = .Kg
            oRow("M3") = .M3

            oRow("PlatformGuid") = SQLHelper.NullableBaseGuid(.Platform)
            oRow("TransmGuid") = SQLHelper.NullableBaseGuid(.Transmisio)
            oRow("TrpGuid") = SQLHelper.NullableBaseGuid(.Transportista)

            oRow("CashCod") = .CashCod
            oRow("PortsCod") = .PortsCod
            oRow("RetencioCod") = .RetencioCod
            oRow("ExportCod") = .ExportCod
            oRow("Valorado") = .Valorado
            oRow("Facturable") = .Facturable
            oRow("Cobro") = SQLHelper.NullableFch(.FchCobroReembolso)
            oRow("Nom") = Strings.Left(.Nom, 60)
            oRow("Adr") = .Address.Text
            oRow("Zip") = SQLHelper.NullableBaseGuid(.Address.Zip)
            oRow("Tel") = Strings.Left(.Tel, 15)
            If .PaymentTerms Is Nothing Then
                Dim xml As String = DTOPaymentTerms.XMLEncoded(.PaymentTerms)
                If String.IsNullOrEmpty(xml) Then
                    oRow("Fpg") = System.DBNull.Value
                Else
                    oRow("Fpg") = xml
                End If
            Else
                oRow("Fpg") = System.DBNull.Value
            End If

            oRow("Incoterm") = SQLHelper.NullableIncoterm(.Incoterm)
            oRow("CustomerDocURL") = SQLHelper.NullableString(.CustomerDocURL)
            oRow("EtiquetesTransport") = SQLHelper.NullableDocFile(.EtiquetesTransport)
            oRow("Obs") = SQLHelper.NullableString(.Obs)
            oRow("ObsTransp") = SQLHelper.NullableString(.ObsTransp)
            oRow("UsrCreatedGuid") = SQLHelper.NullableBaseGuid(.UsrLog.UsrCreated)
            oRow("UsrLastEditedGuid") = SQLHelper.NullableBaseGuid(.UsrLog.UsrLastEdited)
            oRow("FchLastEdited") = .UsrLog.FchLastEdited
        End With

        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateItems(oDelivery As DTODelivery, ByRef oTrans As SqlTransaction)
        If Not oDelivery.IsNew Then DeleteItems(oDelivery, oTrans)

        Dim SQL As String = "SELECT * FROM Arc WHERE AlbGuid = '" & oDelivery.Guid.ToString & "' "
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        Dim iLin As Integer = 0
        For Each oItem As DTODeliveryItem In oDelivery.Items
            Dim oRow = UpdateItem(oDelivery, oItem, oTb)

            If oItem.Bundle.Count > 0 Then
                oRow("Bundle") = oItem.Guid
                For Each oChildItem In oItem.Bundle
                    Dim oChildRow = UpdateItem(oDelivery, oChildItem, oTb)
                    oChildRow("Bundle") = oItem.Guid
                Next
            End If

        Next

        oDA.Update(oDs)
    End Sub

    Shared Function UpdateItem(oDelivery As DTODelivery, oItem As DTODeliveryItem, ByRef oTb As DataTable) As DataRow
        Dim oRow As DataRow = oTb.NewRow
        oTb.Rows.Add(oRow)

        oRow("AlbGuid") = oDelivery.Guid

        With oItem
            oRow("Guid") = .Guid
            oRow("Lin") = .Lin
            oRow("Cod") = .Cod
            oRow("Qty") = .Qty
            SQLHelper.SetNullableAmt(.Price, oRow, "Eur", "Cur", "Pts")
            oRow("Dto") = .Dto

            Select Case oDelivery.Cod
                Case DTOPurchaseOrder.Codis.client
                    oRow("Cod") = CInt(DTODeliveryItem.Cods.Sortida)
                    oRow("PncGuid") = .PurchaseOrderItem.Guid
                    oRow("PdcGuid") = .PurchaseOrderItem.PurchaseOrder.Guid
                    oRow("ArtGuid") = .PurchaseOrderItem.Sku.Guid
                    oRow("MgzGuid") = SQLHelper.NullableBaseGuid(oDelivery.Mgz)

                    If .PurchaseOrderItem.RepCom Is Nothing Then
                        oRow("RepGuid") = System.DBNull.Value
                        oRow("Com") = System.DBNull.Value
                    Else
                        oRow("RepGuid") = SQLHelper.NullableBaseGuid(.PurchaseOrderItem.RepCom.Rep)
                        oRow("Com") = SQLHelper.NullableDecimal(.PurchaseOrderItem.RepCom.Com)
                    End If
                    oRow("RepComLiquidable") = SQLHelper.NullableBaseGuid(.RepComLiquidable)

                Case DTOPurchaseOrder.Codis.reparacio
                    oRow("Cod") = CInt(DTODeliveryItem.Cods.Sortida)
                    oRow("SpvGuid") = .Spv.Guid
                    oRow("ArtGuid") = .Sku.Guid
                    oRow("MgzGuid") = SQLHelper.NullableBaseGuid(oDelivery.Mgz)

                Case DTOPurchaseOrder.Codis.proveidor
                    oRow("Cod") = CInt(DTODeliveryItem.Cods.Entrada)
                    oRow("PncGuid") = .PurchaseOrderItem.Guid
                    oRow("PdcGuid") = .PurchaseOrderItem.PurchaseOrder.Guid
                    oRow("ArtGuid") = .PurchaseOrderItem.Sku.Guid
                    oRow("MgzGuid") = SQLHelper.NullableBaseGuid(oDelivery.Mgz)
                Case DTOPurchaseOrder.Codis.traspas
                    'to implement
            End Select

        End With
        Return oRow
    End Function


    Shared Sub UpdateSpvs(oDelivery As DTODelivery, oTrans As SqlTransaction)
        If Not oDelivery.IsNew Then RemoveSpvs(oDelivery, oTrans)

        Dim oSpvs As New List(Of DTOSpv)
        For Each item In oDelivery.Items
            Dim oSpv As DTOSpv = item.Spv
            If oSpv IsNot Nothing Then
                If Not oSpvs.Any(Function(x) x.Equals(oSpv)) Then
                    oSpvs.Add(oSpv)
                End If
            End If
        Next

        For Each oSpv In oSpvs
            Dim sb As New Text.StringBuilder
            sb.AppendLine("SELECT Spv.* ")
            sb.AppendLine("FROM Spv ")
            sb.AppendLine("WHERE Spv.Guid = '" & oSpv.Guid.ToString & "' ")
            Dim SQL As String = sb.ToString
            Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oDelivery.Guid.ToString())
            Dim oDs As New DataSet
            oDA.Fill(oDs)
            Dim oTb As DataTable = oDs.Tables(0)
            Dim oRow As DataRow = oTb.Rows(0)
            With oSpv
                oRow("UsrTecnicGuid") = SQLHelper.NullableBaseGuid(.usrTecnic)
                oRow("ObsTecnic") = .obsTecnic
                oRow("Garantia") = .garantia
                oRow("AlbGuid") = oDelivery.Guid
            End With
            oDA.Update(oDs)
        Next
    End Sub

    Shared Sub RemoveSpvs(oDelivery As DTODelivery, oTrans As SqlTransaction)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("UPDATE Spv ")
        sb.AppendLine("SET Spv.AlbGuid = NULL ") ' '" & oDelivery.Guid.ToString & "' ")
        sb.AppendLine("WHERE Spv.AlbGuid = '" & oDelivery.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Function LastId(oDelivery As DTODelivery, ByRef oTrans As SqlTransaction) As Integer
        Dim retval As Integer
        Dim sb As New Text.StringBuilder
        sb.Append("SELECT TOP 1 Alb AS LastId ")
        sb.Append("FROM Alb ")
        sb.Append("WHERE Alb.Emp =" & oDelivery.Emp.Id & " ")
        sb.Append("AND Alb.Yea=" & oDelivery.Fch.Year & " ")
        sb.Append("ORDER BY Alb.Alb DESC")
        Dim SQL = sb.ToString
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

    Shared Function UpdateJustificante(oDelivery As DTODelivery, ByVal oJustificanteCode As DTODelivery.JustificanteCodes, exs As List(Of Exception)) As Boolean
        Dim sNow As String = Format(Today, "yyyyMMdd")
        Dim SQL As String = "UPDATE ALB " _
        & "SET JUSTIFICANTE= " & CInt(oJustificanteCode) & ", " _
        & "FCHJUSTIFICANTE='" & sNow & "' WHERE " _
        & "Guid='" & oDelivery.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, exs)
        Return exs.Count = 0
    End Function

    Shared Function CobraPerTransferenciaPrevia(oDelivery As DTODelivery, ByRef oCca As DTOCca, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            If oCca.DocFile IsNot Nothing Then
                DocFileLoader.Update(oCca.DocFile, oTrans)
            End If
            CcaLoader.Update(oCca, oTrans)
            Update(oDelivery, oTrans)
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

    Shared Function Total(oDelivery As DTODelivery) As DTOAmt
        Dim retval = DTOAmt.Empty 'oDelivery.BaseImponible()
        If oDelivery.Items IsNot Nothing Then
            For Each oItem As DTODeliveryItem In oDelivery.Items
                Dim oAmt As DTOAmt = DTOAmt.FromQtyPriceDto(oItem.Qty, oItem.Price, oItem.Dto)
                retval.Add(oAmt)
            Next
        End If

        Dim oBaseImponible As DTOAmt = retval.Clone

        If Not oDelivery.IvaExempt Then
            Dim BlIVA As Boolean
            Dim BlReq As Boolean
            Select Case oDelivery.Cod
                Case DTOPurchaseOrder.Codis.proveidor
                    Dim oProveidor As DTOProveidor = oDelivery.Proveidor
                    If oProveidor Is Nothing Then oProveidor = New DTOProveidor(oDelivery.Customer.Guid)
                    ContactLoader.Load(oProveidor)
                    BlIVA = oProveidor.Address.Zip.Location.Zona.Country.ISO = "ES"
                Case Else
                    Dim oCustomer As DTOCustomer = oDelivery.Customer
                    CustomerLoader.Load(oCustomer)
                    If oCustomer.Ccx IsNot Nothing Then
                        oCustomer = oCustomer.Ccx
                        CustomerLoader.Load(oCustomer)
                    End If
                    BlIVA = oCustomer.Iva
                    BlReq = oCustomer.Req
            End Select


            If BlIVA Then
                Dim DcTipus As Decimal = TaxLoader.Tipus(DTOTax.Codis.iva_Standard, oDelivery.Fch)
                Dim oIvaAmt As DTOAmt = oBaseImponible.Percent(DcTipus)
                retval.Add(oIvaAmt)
                If BlReq Then
                    DcTipus = TaxLoader.Tipus(DTOTax.Codis.recarrec_Equivalencia_Standard, oDelivery.Fch)
                    Dim oReqAmt As DTOAmt = oBaseImponible.Percent(DcTipus)
                    If DcTipus <> 0 Then
                        retval.Add(oReqAmt)
                    End If
                End If

            End If
        End If
        Return retval
    End Function

    Shared Sub UpdatePendents(oDelivery As DTODelivery, ByRef oTrans As SqlTransaction)
        Select Case oDelivery.Cod
            Case DTOPurchaseOrder.Codis.proveidor, DTOPurchaseOrder.Codis.client
                Dim SQL As String = "UPDATE Pnc SET Pn2 = Pn2 - Arc.Qty " _
                & "FROM Pnc " _
                & "INNER JOIN ARC ON PNC.Guid=Arc.PncGuid " _
                & "WHERE ARC.AlbGuid='" & oDelivery.Guid.ToString & "' "
                SQLHelper.ExecuteNonQuery(SQL, oTrans)
            Case DTOPurchaseOrder.Codis.reparacio

        End Select
    End Sub

    Shared Sub RecuperaPendents(oDelivery As DTODelivery, ByRef oTrans As SqlTransaction)
        Select Case oDelivery.Cod
            Case DTOPurchaseOrder.Codis.proveidor, DTOPurchaseOrder.Codis.client
                Dim SQL As String = "UPDATE Pnc SET Pn2 = Pn2 + Arc.Qty " _
                & "FROM Pnc " _
                & "INNER JOIN ARC ON PNC.Guid=Arc.PncGuid " _
                & "WHERE ARC.AlbGuid=@Guid"
                Dim i As Integer = SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oDelivery.Guid.ToString())
            'Stop
            Case DTOPurchaseOrder.Codis.reparacio
                'Dim SQL As String = "UPDATE SPV SET ALBYEA=0, ALBID=0 WHERE " _
                '& "EMP=@Emp AND ALBYEA=@Yea AND ALBID=@Id"
                'ExecuteNonQuery(Sql, oTrans, "@Emp", mEmp.Id, "@Yea", mYea, "@Id", mId)
        End Select
    End Sub

    Shared Function isAllowedToDelete(exs As List(Of Exception), oDelivery As DTODelivery) As Boolean
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Alb, TransmGuid, FraGuid ")
        sb.AppendLine("FROM Alb ")
        sb.AppendLine("WHERE Guid = '" & oDelivery.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            If Not IsDBNull(oDrd("TransmGuid")) Then
                exs.Add(New Exception(String.Format("L'albarà {0} ja ha estat transmès", oDrd("Alb"))))
            End If
            If Not IsDBNull(oDrd("FraGuid")) Then
                exs.Add(New Exception(String.Format("L'albarà {0} ja ha estat facturat", oDrd("Alb"))))
            End If
        Else
            exs.Add(New Exception(String.Format("No s'ha trobat l'albarà {0}", oDelivery.Id)))
        End If
        oDrd.Close()
        Return exs.Count = 0
    End Function


    Shared Function Delete(oDelivery As DTODelivery, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Delete(oDelivery, oTrans)
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

    Shared Sub Delete(oDelivery As DTODelivery, ByRef oTrans As SqlTransaction)
        Load(oDelivery)
        RestoreSpv(oDelivery, oTrans)
        RecuperaPendents(oDelivery, oTrans)
        DeleteItems(oDelivery, oTrans)
        RemoveSpvs(oDelivery, oTrans)
        If oDelivery.Cod = DTOPurchaseOrder.Codis.proveidor Then RemoveImportacions(oDelivery, oTrans)
        DeleteHeader(oDelivery, oTrans)
    End Sub

    Shared Sub RestoreSpv(oDelivery As DTODelivery, ByRef oTrans As SqlTransaction)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("UPDATE Spv ")
        sb.AppendLine("SET Spv.AlbGuid = NULL ")
        sb.AppendLine("WHERE Spv.AlbGuid = '" & oDelivery.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub RemoveImportacions(oDelivery As DTODelivery, ByRef oTrans As SqlTransaction)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("DELETE ImportDtl WHERE Guid = '" & oDelivery.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteHeader(oDelivery As DTODelivery, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Alb WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oDelivery.Guid.ToString())
    End Sub

    Shared Sub DeleteItems(oDelivery As DTODelivery, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Arc WHERE AlbGuid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oDelivery.Guid.ToString())
    End Sub

    Shared Function CobraPerVisa(oLog As DTOTpvLog, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Update(oLog.Request, oTrans)
            CcaLoader.Update(oLog.Result, oTrans)
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

    Shared Function SetCurExchangeRate(oDelivery As DTODelivery, oCur As DTOCur, oRate As DTOCurExchangeRate, exs As List(Of Exception)) As Boolean
        DeliveryLoader.Load(oDelivery)

        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Dim sRate As String = oRate.Rate.ToString.Replace(",", ".")
            Dim sb As New Text.StringBuilder
            sb.AppendLine("UPDATE Arc ")
            sb.AppendLine("SET Eur = ROUND(Pts / " & sRate & ",2)")
            sb.AppendLine(", Cur = '" & oCur.Tag & "' ")
            sb.AppendLine("WHERE AlbGuid = '" & oDelivery.Guid.ToString & "'")
            Dim SQL As String = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL, oTrans)

            sb = New Text.StringBuilder
            sb.AppendLine("UPDATE Alb ")
            sb.AppendLine("SET Eur = ROUND(Pts / " & sRate & ",2)")
            sb.AppendLine(", Cur = '" & oCur.Tag & "' ")
            sb.AppendLine("WHERE Guid = '" & oDelivery.Guid.ToString & "'")
            SQL = sb.ToString
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

    Shared Function SwitchYear(oDelivery As DTODelivery, DtNewFch As Date, exs As List(Of Exception)) As Boolean
        DeliveryLoader.Load(oDelivery)

        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oDelivery, oTrans)
            oDelivery.Id = 0
            oDelivery.Fch = DtNewFch
            Update(oDelivery, oTrans)

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

Public Class DeliveriesLoader

    Shared Function All(oCcx As DTOCustomer) As List(Of DTODelivery)
        Dim retval As New List(Of DTODelivery)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Arc.AlbGuid, Alb.Alb, Alb.Fch AS AlbFch, Alb.CliGuid ")
        sb.AppendLine(", Alb.FraGuid, Fra.Fra, Fra.Serie AS FraSerie, Fra.Fch AS FraFch, Cca.Hash ")
        sb.AppendLine(", Pnc.PdcGuid, Pdc.Pdc, Pdc.Fch AS PdcFch, Pdc.Pdd ")
        sb.AppendLine(", Arc.Guid AS ArcGuid, Arc.PncGuid, Arc.ArtGuid, Arc.Qty, Arc.Eur, Arc.Cur, Arc.Pts, Arc.Dto, Arc.Lin ")
        sb.AppendLine(", CliClient.Ref AS CustomerRef")
        sb.AppendLine(", VwSkuNom.* ")
        sb.AppendLine("FROM Alb ")
        sb.AppendLine("LEFT OUTER JOIN Fra ON Alb.FraGuid = Fra.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Cca ON Fra.CcaGuid = Cca.Guid ")
        sb.AppendLine("INNER JOIN Arc ON Alb.Guid = Arc.AlbGuid ")
        sb.AppendLine("INNER JOIN VwSkuNom ON Arc.ArtGuid = VwSkuNom.SkuGuid ")
        sb.AppendLine("INNER JOIN Pnc ON Arc.PncGuid = Pnc.Guid ")
        sb.AppendLine("INNER JOIN Pdc ON Pnc.PdcGuid = Pdc.Guid ")
        sb.AppendLine("INNER JOIN CliClient ON Alb.CliGuid = CliClient.Guid ")
        sb.AppendLine("INNER JOIN CliGral ON CliGral.Guid=CliClient.Guid ")

        If oCcx.Equals(DTOCustomer.Wellknown(DTOCustomer.Wellknowns.prenatal)) Then
            sb.AppendLine("WHERE (CliClient.CcxGuid ='" & oCcx.Guid.ToString & "' ")
            sb.AppendLine("    OR CliClient.Guid ='" & oCcx.Guid.ToString & "' ")
            sb.AppendLine("    OR CliClient.Guid ='" & DTOCustomer.Wellknown(DTOCustomer.Wellknowns.prenatalTenerife).Guid.ToString & "' ")
            sb.AppendLine("    OR CliClient.CcxGuid ='" & DTOCustomer.Wellknown(DTOCustomer.Wellknowns.prenatalTenerife).Guid.ToString & "' ")
            sb.AppendLine("    OR CliClient.Guid ='" & DTOCustomer.Wellknown(DTOCustomer.Wellknowns.prenatalPortugal).Guid.ToString & "' ")
            sb.AppendLine("    OR CliClient.CcxGuid ='" & DTOCustomer.Wellknown(DTOCustomer.Wellknowns.prenatalPortugal).Guid.ToString & "' ")
            sb.AppendLine("    ) ")
        Else
            sb.AppendLine("WHERE (CliClient.CcxGuid ='" & oCcx.Guid.ToString & "' OR CliClient.Guid ='" & oCcx.Guid.ToString & "') ")
        End If

        sb.AppendLine("AND YEAR(Alb.Fch) > YEAR(GETDATE())-3 ")
        sb.AppendLine("AND Alb.Cod = " & DTOPurchaseOrder.Codis.client & " ")
        sb.AppendLine("ORDER BY Alb.Yea, Alb.Alb, Arc.Lin ")
        Dim SQL As String = sb.ToString
        Dim oCustomer As New DTOCustomer
        Dim oInvoice As New DTOInvoice
        Dim oDelivery As New DTODelivery
        Dim oPurchaseOrder As New DTOPurchaseOrder
        Dim oCur As DTOCur = DTOApp.Current.Curs.First(Function(x) x.Tag = DTOCur.Ids.EUR.ToString())
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Try

                If Not oCustomer.Guid.Equals(oDrd("CliGuid")) Then
                    oCustomer = retval.Select(Function(x) x.Customer).FirstOrDefault(Function(y) y.Guid.Equals(oDrd("CliGuid")))
                    If oCustomer Is Nothing Then
                        oCustomer = New DTOCustomer(oDrd("CliGuid"))
                        oCustomer.Emp = oCcx.Emp
                        oCustomer.Ref = SQLHelper.GetStringFromDataReader(oDrd("CustomerRef"))
                    End If
                End If

                If oInvoice Is Nothing Then
                    If Not IsDBNull(oDrd("FraGuid")) Then
                        oInvoice = New DTOInvoice(oDrd("FraGuid"))
                        With oInvoice
                            .Num = oDrd("Fra")
                            .Serie = oDrd("FraSerie")
                            .Fch = oDrd("FraFch")
                        End With
                    End If
                Else
                    If IsDBNull(oDrd("FraGuid")) Then
                        oInvoice = Nothing
                    Else
                        If Not oInvoice.Guid.Equals(oDrd("FraGuid")) Then
                            oInvoice = New DTOInvoice(oDrd("FraGuid"))
                            With oInvoice
                                .Num = oDrd("Fra")
                                .Serie = oDrd("FraSerie")
                                .Fch = oDrd("FraFch")
                                .DocFile = SQLHelper.GetDocFileFromDataReaderHash(oDrd("Hash"))
                            End With
                        End If
                    End If

                End If

                If Not oDelivery.Guid.Equals(oDrd("AlbGuid")) Then
                    oDelivery = New DTODelivery(oDrd("AlbGuid"))
                    With oDelivery
                        .Emp = oCcx.Emp
                        .Id = oDrd("Alb")
                        .Fch = oDrd("AlbFch")
                        .Invoice = oInvoice
                        .Customer = oCustomer
                    End With
                    retval.Add(oDelivery)
                End If
                If Not oPurchaseOrder.Guid.Equals(oDrd("PdcGuid")) Then
                    oPurchaseOrder = New DTOPurchaseOrder(oDrd("PdcGuid"))
                    With oPurchaseOrder
                        .Emp = oCcx.Emp
                        .Num = oDrd("Pdc")
                        .Fch = oDrd("PdcFch")
                        .Concept = oDrd("Pdd")
                        .Customer = oCustomer
                    End With
                End If
                If oCur.Tag <> oDrd("Cur") Then
                    oCur = SQLHelper.GetCurFromDataReader(oDrd("Cur"))
                End If
                Dim oPnc As New DTOPurchaseOrderItem(oDrd("PdcGuid"))
                With oPnc
                    .Sku = SQLHelper.GetProductFromDataReader(oDrd)
                    .PurchaseOrder = oPurchaseOrder
                End With
                Dim oArc As New DTODeliveryItem(oDrd("ArcGuid"))
                With oArc
                    .PurchaseOrderItem = oPnc
                    .Sku = .PurchaseOrderItem.Sku
                    .Qty = oDrd("Qty")
                    .Price = DTOAmt.Factory(oDrd("Eur"), oCur, oDrd("Pts"))
                    .Dto = SQLHelper.GetDecimalFromDataReader(oDrd("Dto"))
                    .Lin = oDrd("Lin")
                End With
                oDelivery.Items.Add(oArc)
            Catch ex As Exception
                'Stop
            End Try
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function Years(oEmp As DTOEmp, Optional oContact As DTOContact = Nothing) As List(Of Integer)
        Dim retval As New List(Of Integer)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Alb.Yea ")
        sb.AppendLine("FROM Alb ")
        sb.AppendLine("WHERE Alb.Emp = " & oEmp.Id & " ")
        If oContact IsNot Nothing Then
            sb.AppendLine("AND Alb.CliGuid='" & oContact.Guid.ToString & "' ")
        End If
        sb.AppendLine("GROUP BY Alb.Yea ")
        sb.AppendLine("ORDER BY Alb.Yea DESC ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            retval.Add(oDrd("Yea"))
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Centros(oUser As DTOUser) As List(Of DTOCustomer)
        Dim retval As New List(Of DTOCustomer)
        Dim sb As New System.Text.StringBuilder

        sb.AppendLine("SELECT Alb.CliGuid, (CASE WHEN (CliClient.Ref IS NULL OR CliClient.Ref='') THEN CliGral.FullNom ELSE CliClient.Ref END) AS Nom ")
        sb.AppendLine("FROM Alb ")
        sb.AppendLine("INNER JOIN Email_Clis ON Alb.CliGuid = Email_Clis.ContactGuid ")
        sb.AppendLine("INNER JOIN CliClient ON Alb.CliGuid = CliClient.Guid ")
        sb.AppendLine("INNER JOIN CliGral ON Alb.CliGuid = CliGral.Guid ")
        sb.AppendLine("WHERE Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
        sb.AppendLine("GROUP BY Alb.CliGuid, CliClient.Ref, CliGral.FullNom ")
        sb.AppendLine("ORDER BY Nom ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOCustomer(oDrd("CliGuid"))
            item.Emp = oUser.Emp
            item.Nom = oDrd("Nom")
            retval.Add(item)
        Loop
        oDrd.Close()

        Return retval
    End Function


    Shared Function All(oInvoice As DTOInvoice, Optional exs As List(Of Exception) = Nothing) As List(Of DTODelivery)
        Dim retval As New List(Of DTODelivery)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Alb.Emp, Alb.Guid, Alb.Alb, Alb.Fch, Alb.CliGuid, Alb.Nom, Alb.Adr, Alb.Zip, Alb.Tel, Alb.CashCod, Alb.PortsCod, Alb.Cobro ")
        sb.AppendLine(",VwAreaNom.CountryGuid, VwAreaNom.CountryISO, VwAreaNom.CountryNomEsp, VwAreaNom.CountryNomCat, VwAreaNom.CountryNomEng, VwAreaNom.ZonaGuid, VwAreaNom.ZonaNom, VwAreaNom.LocationGuid, VwAreaNom.LocationNom, VwAreaNom.ZipCod ")
        sb.AppendLine("FROM Alb ")
        sb.AppendLine("LEFT OUTER JOIN VwAreaNom ON Alb.Zip=VwAreaNom.Guid ")
        sb.AppendLine("WHERE Alb.FraGuid = '" & oInvoice.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY Alb.Yea, Alb.Alb ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTODelivery(oDrd("Guid"))
            With item
                .Emp = New DTOEmp(oDrd("Emp"))
                .Id = oDrd("Alb")
                .Fch = oDrd("Fch")
                .Customer = New DTOCustomer(oDrd("CliGuid"))
                .Nom = oDrd("Nom")
                .Address = New DTOAddress()
                .Address.Text = oDrd("Adr")
                Try
                    .Address.Zip = ZipLoader.NewZip(oDrd("Zip"), oDrd("ZipCod"), oDrd("LocationGuid"), oDrd("LocationNom"), oDrd("ZonaGuid"), oDrd("ZonaNom"), oDrd("CountryISO"), oDrd("CountryGuid"), oDrd("CountryNomEsp"), oDrd("CountryNomCat"), oDrd("CountryNomEng"))
                Catch ex As Exception
                    If exs IsNot Nothing Then
                        exs.Add(New Exception(String.Format("error en població de l'albará {0} a {1}", .Id, .Nom)))
                    End If
                End Try
                .CashCod = oDrd("CashCod")
                .PortsCod = oDrd("PortsCod")
                .FchCobroReembolso = SQLHelper.GetFchFromDataReader(oDrd("Cobro"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oTransmisio As DTOTransmisio) As List(Of DTODelivery)
        Dim retval As New List(Of DTODelivery)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Alb.Guid, Alb.Alb, Alb.Fch, Alb.CliGuid, Alb.Nom, Alb.Adr, Alb.Zip, Alb.Tel, Alb.Eur, Alb.CashCod, Alb.PortsCod, Alb.Cobro, Alb.EtiquetesTransport ")
        sb.AppendLine(", VwAreaNom.CountryGuid, VwAreaNom.CountryISO, VwAreaNom.CountryNomEsp, VwAreaNom.CountryNomCat, VwAreaNom.CountryNomEng, VwAreaNom.ZonaGuid, VwAreaNom.ZonaNom, VwAreaNom.LocationGuid, VwAreaNom.LocationNom, VwAreaNom.ZipCod ")
        sb.AppendLine("FROM Alb ")
        sb.AppendLine("LEFT OUTER JOIN VwAreaNom ON Alb.Zip=VwAreaNom.Guid ")
        sb.AppendLine("WHERE Alb.TransmGuid = '" & oTransmisio.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY Alb.Yea, Alb.Alb ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTODelivery(oDrd("Guid"))
            With item
                .Emp = oTransmisio.Emp
                .Id = oDrd("Alb")
                .Fch = oDrd("Fch")
                .Customer = New DTOCustomer(oDrd("CliGuid"))
                .Nom = oDrd("Nom")
                .Address = New DTOAddress()
                .Address.Text = oDrd("Adr")
                .Address.Zip = ZipLoader.NewZip(oDrd("Zip"), oDrd("ZipCod"), oDrd("LocationGuid"), oDrd("LocationNom"), oDrd("ZonaGuid"), oDrd("ZonaNom"), oDrd("CountryISO"), oDrd("CountryGuid"), oDrd("CountryNomEsp"), oDrd("CountryNomCat"), oDrd("CountryNomEng"))
                .CashCod = oDrd("CashCod")
                .PortsCod = oDrd("PortsCod")
                .FchCobroReembolso = SQLHelper.GetFchFromDataReader(oDrd("Cobro"))
                .EtiquetesTransport = SQLHelper.GetDocFileFromDataReaderHash(oDrd("EtiquetesTransport"))
                .Import = DTOAmt.Factory(oDrd("Eur"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
    Shared Function All(oTransmisions As List(Of DTOTransmisio)) As List(Of DTODelivery)
        'per facturar
        Dim retval As New List(Of DTODelivery)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Arc.AlbGuid, Alb.Emp, Alb.Alb, Alb.Fch, Alb.CliGuid AS AlbCliGuid, CliGral.Guid AS FraCliGuid ")
        sb.AppendLine(", Alb.CashCod, Alb.PortsCod, Alb.Cobro, Alb.EtiquetesTransport, Alb.Eur AS AlbEur ")
        sb.AppendLine(", Alb.Nom AS DestinationNom, Alb.Adr, Alb.Zip, VwAreaNom.CountryGuid, VwAreaNom.CountryISO, VwAreaNom.CountryNomEsp, VwAreaNom.CountryNomCat, VwAreaNom.CountryNomEng, VwAreaNom.ZonaGuid, VwAreaNom.ZonaNom, VwAreaNom.LocationGuid, VwAreaNom.LocationNom, VwAreaNom.ZipCod ")
        sb.AppendLine(", Pnc.PdcGuid, Pdc.Fch AS PdcFch, Pdc.Pdd ")
        sb.AppendLine(", Arc.Guid AS ArcGuid, Arc.Lin, Arc.PncGuid, Arc.ArtGuid, Arc.Qty, Arc.Eur, Arc.Dto ")
        sb.AppendLine(", VwSkuNom.* ")
        sb.AppendLine(", CliGral.RaoSocial, CliGral.LangId, AlbCustomer.Ref AS CustomerRef")
        sb.AppendLine(", CliGral.NIF, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod ")
        sb.AppendLine(", FraCustomer.IVA, FraCustomer.Req, FraCustomer.SuProveedorNum ")
        sb.AppendLine(", FraCustomer.Cfp, FraCustomer.Mes AS CreditMonths, FraCustomer.PaymentDays ")
        sb.AppendLine(", VwAddress.Adr, VwAddress.ZipGuid, VwAddress.ZipCod, VwAddress.LocationGuid, VwAddress.LocationNom ")
        sb.AppendLine(", VwAddress.ProvinciaGuid, VwAddress.ZonaGuid, VwAddress.ProvinciaNom, VwAddress.CountryGuid, VwAddress.CountryEsp, VwAddress.CEE ")
        sb.AppendLine(", VwCustomerIban.Guid AS IbanGuid, VwCustomerIban.Customer, VwCustomerIban.Bank, VwCustomerIban.BankBranch, VwCustomerIban.CCC ")
        sb.AppendLine(", VwCustomerIban.BankNom, VwCustomerIban.BankAlias ")
        sb.AppendLine(", VwCustomerIban.Location AS BankBranchLocation,VwCustomerIban.LocationNom AS BankBranchLocationNom, VwCustomerIban.BankBranchAdr ")
        sb.AppendLine("FROM Alb ")
        sb.AppendLine("LEFT OUTER JOIN VwAreaNom ON Alb.Zip=VwAreaNom.Guid ")
        sb.AppendLine("INNER JOIN Arc ON Alb.Guid = Arc.AlbGuid ")
        sb.AppendLine("INNER JOIN VwSkuNom ON Arc.ArtGuid = VwSkuNom.SkuGuid ")
        sb.AppendLine("INNER JOIN Pnc ON Arc.PncGuid = Pnc.Guid ")
        sb.AppendLine("INNER JOIN Pdc ON Pnc.PdcGuid = Pdc.Guid ")
        sb.AppendLine("INNER JOIN CliClient AlbCustomer ON Alb.CliGuid = AlbCustomer.Guid ")
        sb.AppendLine("INNER JOIN CliGral ON CliGral.Guid=(CASE WHEN AlbCustomer.CcxGuid IS NULL THEN AlbCustomer.Guid ELSE AlbCustomer.CcxGuid END) ")
        sb.AppendLine("INNER JOIN CliClient FraCustomer ON CliGral.Guid = FraCustomer.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwCustomerIban ON CliGral.Guid = VwCustomerIban.Customer ")
        sb.AppendLine("INNER JOIN VwAddress ON CliGral.Guid = VwAddress.SrcGuid ")
        sb.AppendLine("WHERE ( ")
        For Each oTransmisio As DTOTransmisio In oTransmisions
            If oTransmisio.UnEquals(oTransmisions.First) Then
                sb.AppendLine("OR ")
            End If
            sb.AppendLine("Alb.TransmGuid = '" & oTransmisio.Guid.ToString & "' ")
        Next
        sb.AppendLine(") ")
        sb.AppendLine("ORDER BY Alb.Yea, Alb.Alb, Arc.Lin ")

        Dim oDelivery As New DTODelivery()
        Dim oPurchaseOrder As New DTOPurchaseOrder

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oDelivery.Guid.Equals(oDrd("AlbGuid")) Then

                Dim oIban As DTOIban = Nothing
                If Not IsDBNull(oDrd("IbanGuid")) Then
                    oIban = New DTOIban(oDrd("IbanGuid"))
                    With oIban
                        .Digits = SQLHelper.GetStringFromDataReader(oDrd("Ccc"))
                        If Not IsDBNull(oDrd("BankBranch")) Then
                            .BankBranch = New DTOBankBranch(oDrd("BankBranch"))
                            .BankBranch.Address = SQLHelper.GetStringFromDataReader(oDrd("BankBranchAdr"))
                            If Not IsDBNull(oDrd("BankBranchLocation")) Then
                                .BankBranch.Location = New DTOLocation(oDrd("BankBranchLocation"))
                                .BankBranch.Location.Nom = SQLHelper.GetStringFromDataReader(oDrd("BankBranchLocationNom"))
                            End If
                            If Not IsDBNull(oDrd("Bank")) Then
                                .BankBranch.Bank = New DTOBank(oDrd("Bank"))
                                With .BankBranch.Bank
                                    .RaoSocial = SQLHelper.GetStringFromDataReader(oDrd("BankNom"))
                                    .NomComercial = SQLHelper.GetStringFromDataReader(oDrd("BankAlias"))
                                End With
                            End If
                        End If
                    End With
                End If

                Dim oPaymentTerms As New DTOPaymentTerms
                With oPaymentTerms
                    .Iban = oIban
                    .Cod = SQLHelper.GetIntegerFromDataReader(oDrd("Cfp"))
                    .Months = SQLHelper.GetIntegerFromDataReader(oDrd("CreditMonths"))
                    .PaymentDays = CustomerLoader.GetPaymentDaysFromDataReader(oDrd("PaymentDays"))
                End With

                Dim oFraCustomer As New DTOCustomer(oDrd("FraCliGuid"))
                With oFraCustomer
                    .Nom = oDrd("RaoSocial")
                    .Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
                    .SuProveedorNum = SQLHelper.GetStringFromDataReader(oDrd("SuProveedorNum"))
                    .Address = SQLHelper.GetAddressFromDataReader(oDrd)
                    .Lang = DTOLang.Factory(oDrd("LangId"))
                    .Iva = oDrd("Iva")
                    .Req = oDrd("Req")
                    .PaymentTerms = oPaymentTerms
                End With

                Dim oAlbCustomer As New DTOCustomer(oDrd("AlbCliGuid"))
                With oAlbCustomer
                    .Nom = oDrd("RaoSocial")
                    .Ref = oDrd("CustomerRef")
                    .Ccx = oFraCustomer
                End With

                oDelivery = New DTODelivery(oDrd("AlbGuid"))
                With oDelivery
                    .Emp = New DTOEmp(oDrd("Emp"))
                    .Id = oDrd("Alb")
                    .Fch = oDrd("Fch")
                    .Nom = oDrd("DestinationNom")
                    .Address = New DTOAddress()
                    .Address.Text = oDrd("Adr")
                    .Address.Zip = ZipLoader.NewZip(oDrd("Zip"), oDrd("ZipCod"), oDrd("LocationGuid"), oDrd("LocationNom"), oDrd("ZonaGuid"), oDrd("ZonaNom"), oDrd("CountryISO"), oDrd("CountryGuid"), oDrd("CountryNomEsp"), oDrd("CountryNomCat"), oDrd("CountryNomEng"))
                    .Customer = oAlbCustomer
                    .CashCod = oDrd("CashCod")
                    .PortsCod = oDrd("PortsCod")
                    .FchCobroReembolso = SQLHelper.GetFchFromDataReader(oDrd("Cobro"))
                    .EtiquetesTransport = SQLHelper.GetDocFileFromDataReaderHash(oDrd("EtiquetesTransport"))
                    .Import = DTOAmt.Factory(oDrd("AlbEur"))
                    .Items = New List(Of DTODeliveryItem)
                End With
                retval.Add(oDelivery)
            End If
            If Not oPurchaseOrder.Guid.Equals(oDrd("PdcGuid")) Then
                oPurchaseOrder = New DTOPurchaseOrder(oDrd("PdcGuid"))
                With oPurchaseOrder
                    .Fch = SQLHelper.GetFchFromDataReader(oDrd("PdcFch"))
                    .Concept = SQLHelper.GetStringFromDataReader(oDrd("Pdd"))
                End With
            End If
            Dim oSku = SQLHelper.GetProductFromDataReader(oDrd)
            Dim oPurchaseOrderItem As New DTOPurchaseOrderItem(oDrd("PncGuid"))
            With oPurchaseOrderItem
                .PurchaseOrder = oPurchaseOrder
                .Sku = oSku
            End With
            Dim item As New DTODeliveryItem(oDrd("ArcGuid"))
            With item
                .Delivery = oDelivery
                .PurchaseOrderItem = oPurchaseOrderItem
                .Lin = oDrd("Lin")
                .Qty = oDrd("Qty")
                .Price = DTOAmt.Factory(oDrd("Eur"))
                .Dto = SQLHelper.GetDecimalFromDataReader(oDrd("Dto"))
                .Sku = oSku
            End With
            oDelivery.Items.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oEmp As DTOEmp, oArea As DTOArea) As List(Of DTODelivery)
        Dim retval As New List(Of DTODelivery)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Alb.Guid, Alb.Alb, Alb.Fch, Alb.CliGuid, Alb.Nom, Alb.Adr, Alb.Zip, Alb.Tel, Alb.CashCod, Alb.PortsCod, Alb.Cobro, Alb.Eur AS AlbEur, Alb.Cur AS AlbCur, Alb.Pts AS AlbPts ")
        sb.AppendLine(", VwAreaNom.CountryGuid, VwAreaNom.CountryISO, VwAreaNom.CountryNomEsp, VwAreaNom.CountryNomCat, VwAreaNom.CountryNomEng, VwAreaNom.ZonaGuid, VwAreaNom.ZonaNom, VwAreaNom.LocationGuid, VwAreaNom.LocationNom, VwAreaNom.ZipCod ")
        sb.AppendLine("FROM Alb ")
        sb.AppendLine("LEFT OUTER JOIN VwAreaParent ON Alb.Zip=VwAreaParent.ChildGuid ")
        sb.AppendLine("LEFT OUTER JOIN VwAreaNom ON Alb.Zip=VwAreaNom.Guid ")
        sb.AppendLine("WHERE VwAreaParent.parentGuid = '" & oArea.Guid.ToString & "' ")
        sb.AppendLine("AND Alb.Emp =" & oEmp.Id & " ")
        sb.AppendLine("ORDER BY Alb.Yea DESC, Alb.Alb DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTODelivery(oDrd("Guid"))
            With item
                .Emp = oEmp
                .Id = oDrd("Alb")
                .Fch = oDrd("Fch")
                .Customer = New DTOCustomer(oDrd("CliGuid"))
                .Nom = oDrd("Nom")
                .Address = New DTOAddress()
                .Address.Text = oDrd("Adr")
                .Address.Zip = ZipLoader.NewZip(oDrd("Zip"), oDrd("ZipCod"), oDrd("LocationGuid"), oDrd("LocationNom"), oDrd("ZonaGuid"), oDrd("ZonaNom"), oDrd("CountryISO"), oDrd("CountryGuid"), oDrd("CountryNomEsp"), oDrd("CountryNomCat"), oDrd("CountryNomEng"))
                .CashCod = oDrd("CashCod")
                .PortsCod = oDrd("PortsCod")
                .FchCobroReembolso = SQLHelper.GetFchFromDataReader(oDrd("Cobro"))
                .Import = DTOAmt.Factory(CDec(oDrd("AlbEur")), oDrd("AlbCur").ToString, CDec(oDrd("AlbPts")))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oExercici As DTOExercici) As List(Of DTODelivery)
        Dim retval As New List(Of DTODelivery)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Alb.Guid, Alb.Alb, Alb.Fch, Alb.Cod, Alb.Nom ")
        sb.AppendLine(", Alb.Eur AS AlbEur, Alb.Cur AS AlbCur, Alb.Pts AS AlbPts ")
        sb.AppendLine(", Alb.CliGuid, CliGral.RaoSocial ")
        sb.AppendLine(", Alb.FraGuid, Fra.Serie, Fra.Fra, Fra.Fch AS FraFch ")
        sb.AppendLine(", Fra.CcaGuid, Cca.Cca, Cca.Hash ")
        sb.AppendLine("FROM Alb ")
        sb.AppendLine("INNER JOIN CliGral ON Alb.CliGuid=CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Fra ON Alb.FraGuid = Fra.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Cca ON Fra.CcaGuid = Cca.Guid ")
        sb.AppendLine("WHERE Alb.Emp=" & oExercici.Emp.Id & " ")
        sb.AppendLine("AND YEAR(Alb.Fch)=" & oExercici.Year & " ")
        sb.AppendLine("ORDER BY Alb.Alb ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oCca As DTOCca = Nothing
            If Not IsDBNull(oDrd("CcaGuid")) Then
                oCca = New DTOCca(oDrd("CcaGuid"))
                With oCca
                    .Id = SQLHelper.GetIntegerFromDataReader(oDrd("Cca"))
                    .DocFile = SQLHelper.GetDocFileFromDataReaderHash(oDrd("Hash"))
                End With
            End If

            Dim oInvoice As DTOInvoice = Nothing
            If Not IsDBNull(oDrd("FraGuid")) Then
                oInvoice = New DTOInvoice(oDrd("FraGuid"))
                With oInvoice
                    .Num = SQLHelper.GetIntegerFromDataReader(oDrd("Fra"))
                    .Serie = SQLHelper.GetIntegerFromDataReader(oDrd("Serie"))
                    .Fch = SQLHelper.GetFchFromDataReader(oDrd("FraFch"))
                    .Cca = oCca
                    .DocFile = SQLHelper.GetDocFileFromDataReaderHash(oDrd("Hash"))
                End With
            End If

            Dim oCustomer As New DTOCustomer(oDrd("CliGuid"))
            With oCustomer
                .Nom = SQLHelper.GetStringFromDataReader(oDrd("RaoSocial"))
            End With

            Dim oDelivery As New DTODelivery(oDrd("Guid"))
            With oDelivery
                .Emp = oExercici.Emp
                .Id = SQLHelper.GetIntegerFromDataReader(oDrd("Alb"))
                .Fch = SQLHelper.GetFchFromDataReader(oDrd("Fch"))
                .Nom = oDrd("Nom")
                .Import = DTOAmt.Factory(CDec(oDrd("AlbEur")), oDrd("AlbCur").ToString, CDec(oDrd("AlbPts")))
                .Customer = oCustomer
                .Invoice = oInvoice
                .Cod = SQLHelper.GetIntegerFromDataReader(oDrd("Cod"))
            End With
            retval.Add(oDelivery)
        Loop
        oDrd.Close()
        Return retval

    End Function


    Shared Function Headers(Optional user As DTOUser = Nothing, Optional customer As DTOCustomer = Nothing, Optional emp As DTOEmp = Nothing, Optional year As Integer = 0) As List(Of DTODelivery.Compact)
        Dim retval As New List(Of DTODelivery.Compact)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT VwDeliveries.* ")
        sb.AppendLine("FROM VwDeliveries ")

        If user IsNot Nothing AndAlso user.Rol.isCustomer Then
            sb.AppendLine("INNER JOIN Email_Clis ON VwDeliveries.CliGuid = Email_Clis.ContactGuid ")
            sb.AppendLine("WHERE Email_Clis.EmailGuid = '" & user.Guid.ToString & "' ")
        ElseIf customer IsNot Nothing Then
            sb.AppendLine("WHERE VwDeliveries.CliGuid = '" & customer.Guid.ToString & "' ")
        ElseIf emp IsNot Nothing Then
            sb.AppendLine("WHERE VwDeliveries.Emp = " & emp.Id & " AND VwDeliveries.Yea = " & year & " ")
        End If
        sb.AppendLine("ORDER BY VwDeliveries.Yea DESC, VwDeliveries.AlbId DESC ")

        Dim SQL = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oItem As New DTODelivery.Compact(oDrd("AlbGuid"))
            With oItem
                .Id = oDrd("AlbId")
                .Fch = oDrd("AlbFch")
                .Customer = New DTOGuidNom(oDrd("CliGuid"), SQLHelper.GetStringFromDataReader(oDrd("FullNom")))
                If Not IsDBNull(oDrd("TrpGuid")) Then
                    .Transportista = New DTOGuidNom(oDrd("TrpGuid"), SQLHelper.GetStringFromDataReader(oDrd("TrpNom")))
                    If String.IsNullOrEmpty(.Transportista.Nom) Then .Transportista.Nom = SQLHelper.GetStringFromDataReader(oDrd("TrpNif"))
                    .Tracking = SQLHelper.GetStringFromDataReader(oDrd("Tracking"))
                End If
                .Import = SQLHelper.GetAmtCompactFromDataReader(oDrd("AlbEur"))
                .ImportAdicional = SQLHelper.GetAmtCompactFromDataReader(oDrd("AlbImportAdicional"))
                If Not IsDBNull(oDrd("FraGuid")) Then
                    .Invoice = New DTOInvoice.Compact
                    With .Invoice
                        .Guid = oDrd("FraGuid")
                        .Num = oDrd("FraId")
                        .Serie = SQLHelper.GetIntegerFromDataReader(oDrd("FraSerie"))
                    End With
                End If
                If Not IsDBNull(oDrd("TransmGuid")) Then
                    .Transmisio = New DTOTransmisio.Compact
                    With .Transmisio
                        .Guid = oDrd("TransmGuid")
                        .Id = oDrd("Transm")
                    End With
                End If
                .Cod = SQLHelper.GetIntegerFromDataReader(oDrd("Cod"))
                .CashCod = SQLHelper.GetIntegerFromDataReader(oDrd("CashCod"))
                .PortsCod = SQLHelper.GetIntegerFromDataReader(oDrd("PortsCod"))
                .RetencioCod = SQLHelper.GetIntegerFromDataReader(oDrd("AlbRetencioCod"))
                .Facturable = SQLHelper.GetBooleanFromDatareader(oDrd("Facturable"))
                .UsrLog = SQLHelper.getUsrLog2FromDataReader(oDrd)
            End With
            retval.Add(oItem)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function Minified(Optional user As DTOUser = Nothing, Optional customer As DTOCustomer = Nothing, Optional emp As DTOEmp = Nothing, Optional year As Integer = 0) As List(Of DTODelivery)
        Dim retval As New List(Of DTODelivery)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT        Alb.Guid AS AlbGuid, Alb.Emp, YEAR(Alb.Fch) AS Yea, Alb.Alb AS AlbId, Alb.Fch AS AlbFch, Alb.Cod, Alb.PortsCod, Alb.CashCod, Alb.Eur AS AlbEur, Alb.TransmGuid, Transm.Transm, ")
        sb.AppendLine("Alb.Pt2 AS AlbImportAdicional, Alb.Cobro AS AlbCobro, Alb.RetencioCod AS AlbRetencioCod, Alb.Facturable, Alb.FraGuid, Fra.Fra AS FraId, Fra.Serie AS FraSerie, Alb.CliGuid, CliGral.FullNom, ")
        sb.AppendLine("CliGral.LangId, CliClient.Ref AS ClientRef, Alb.UsrCreatedGuid AS UsrCreated, UsrCreated.Nom AS UsrCreatedNickname, ConsumerTicket.Id AS TicketId, ")
        sb.AppendLine("ConsumerTicket.Nom AS TicketNom, ConsumerTicket.Cognom1 AS TicketCognom ")
        sb.AppendLine("FROM            Alb ")
        sb.AppendLine("INNER JOIN CliGral ON Alb.CliGuid = CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliClient ON Alb.CliGuid = CliClient.Guid ")
        sb.AppendLine("LEFT OUTER JOIN ConsumerTicket ON Alb.Guid = ConsumerTicket.Delivery ")
        sb.AppendLine("LEFT OUTER JOIN Fra ON Alb.FraGuid = Fra.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Transm ON Alb.TransmGuid = Transm.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwUsrNickname AS UsrCreated ON Alb.UsrCreatedGuid = UsrCreated.guid ")

        If user IsNot Nothing AndAlso user.Rol.isCustomer Then
            sb.AppendLine("INNER JOIN Email_Clis ON Alb.CliGuid = Email_Clis.ContactGuid ")
            sb.AppendLine("WHERE Email_Clis.EmailGuid = '" & user.Guid.ToString & "' ")
        ElseIf customer IsNot Nothing Then
            sb.AppendLine("WHERE Alb.CliGuid = '" & customer.Guid.ToString & "' ")
        ElseIf emp IsNot Nothing Then
            sb.AppendLine("WHERE Alb.Emp = " & emp.Id & " AND YEAR(alb.Fch) = " & year & " ")
        End If
        sb.AppendLine("ORDER BY YEAR(Alb.Fch) DESC, Alb.Alb DESC ")

        Dim SQL = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oItem As New DTODelivery(oDrd("AlbGuid"))
            With oItem
                .Id = oDrd("AlbId")
                .Fch = oDrd("AlbFch")
                .Cod = SQLHelper.GetIntegerFromDataReader(oDrd("Cod"))
                If .Cod = DTOPurchaseOrder.Codis.proveidor Then
                    .Proveidor = New DTOProveidor(oDrd("CliGuid"))
                    .Proveidor.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                Else
                    .Customer = New DTOCustomer(oDrd("CliGuid"))
                    .Customer.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                End If
                'If Not IsDBNull(oDrd("TrpGuid")) Then
                '.Transportista = New DTOTransportista(oDrd("TrpGuid"))
                '.Transportista.Nom = SQLHelper.GetStringFromDataReader(oDrd("TrpNom"))
                'If String.IsNullOrEmpty(.Transportista.Nom) Then .Transportista.Nom = SQLHelper.GetStringFromDataReader(oDrd("TrpNif"))
                '.Tracking = SQLHelper.GetStringFromDataReader(oDrd("Tracking"))
                'End If
                .Import = SQLHelper.GetAmtFromDataReader(oDrd("AlbEur"))
                .ImportAdicional = SQLHelper.GetAmtFromDataReader(oDrd("AlbImportAdicional"))
                If Not IsDBNull(oDrd("FraGuid")) Then
                    .Invoice = New DTOInvoice
                    With .Invoice
                        .Guid = oDrd("FraGuid")
                        .Num = oDrd("FraId")
                        .Serie = SQLHelper.GetIntegerFromDataReader(oDrd("FraSerie"))
                    End With
                End If
                If Not IsDBNull(oDrd("TransmGuid")) Then
                    .Transmisio = New DTOTransmisio
                    With .Transmisio
                        .Guid = oDrd("TransmGuid")
                        .Id = oDrd("Transm")
                    End With
                End If
                .CashCod = SQLHelper.GetIntegerFromDataReader(oDrd("CashCod"))
                .PortsCod = SQLHelper.GetIntegerFromDataReader(oDrd("PortsCod"))
                .RetencioCod = SQLHelper.GetIntegerFromDataReader(oDrd("AlbRetencioCod"))
                .Facturable = SQLHelper.GetBooleanFromDatareader(oDrd("Facturable"))
                .UsrLog = SQLHelper.getUsrLog2FromDataReader(oDrd)
            End With
            retval.Add(oItem)
        Loop
        oDrd.Close()

        Return retval
    End Function

    Shared Function Headers(oEmp As DTOEmp,
                            Optional oUser As DTOUser = Nothing,
                            Optional oContact As DTOContact = Nothing,
                            Optional Group As Boolean = False,
                            Optional oCodis As List(Of DTOPurchaseOrder.Codis) = Nothing,
                            Optional iYear As Integer = 0,
                            Optional pendentsDeCobro As DTODelivery.RetencioCods = DTODelivery.RetencioCods.notSet,
                            Optional altresPorts As Boolean = False
                            ) As List(Of DTODeliveryHeader)

        Dim retval As New List(Of DTODeliveryHeader)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Alb.Guid AS AlbGuid, Alb.Alb AS AlbId, Alb.Fch AS AlbFch, Alb.Cod ")
        sb.AppendLine(", Alb.PortsCod, Alb.CashCod, Alb.Eur AS AlbEur, Alb.Cur AS AlbCur, Alb.Pts AS AlbPts ")
        sb.AppendLine(", Alb.TransmGuid, Transm.Transm ")
        sb.AppendLine(", Alb.Pt2 AS AlbImportAdicional, Alb.Cobro AS AlbCobro, Alb.RetencioCod AS AlbRetencioCod ")
        sb.AppendLine(", Alb.Facturable, Alb.FraGuid, Fra.Fra as FraId, Fra.Fch as FraFch ")
        sb.AppendLine(", Cca.Hash AS FraHash ")
        sb.AppendLine(", Alb.CliGuid, CliGral.FullNom, CliGral.LangId, CliClient.Ref AS ClientRef ")
        sb.AppendLine(", Alb.TrpGuid AS TrpGuid2, Trp.TrpGuid, Trp.TrpNom, Trp.TrpNif, Trp.Tracking, Trp2.Abr AS TrpAbr ")
        sb.AppendLine(", Alb.UsrCreatedGuid, UsrCreated.Adr AS UsrCreatedEmailAddress, UsrCreated.Nickname AS UsrCreatedNickname ")
        sb.AppendLine(", ConsumerTicket.Id AS TicketId, ConsumerTicket.Nom AS TicketNom, ConsumerTicket.Cognom1 AS TicketCognom ")
        sb.AppendLine("FROM Alb ")
        sb.AppendLine("INNER JOIN CliGral ON Alb.CliGuid = CliGral.Guid ")
        If Group Then
            sb.AppendLine("INNER JOIN CliClient ON Alb.CliGuid=CliClient.Guid ")
            sb.AppendLine("AND (Cliclient.Guid = '" & oContact.Guid.ToString & "' OR Cliclient.CcxGuid = '" & oContact.Guid.ToString & "') ")
        Else
            sb.AppendLine("LEFT OUTER JOIN CliClient ON ALB.CliGuid = CliClient.Guid ")
        End If
        sb.AppendLine("LEFT OUTER JOIN ConsumerTicket ON Alb.Guid = ConsumerTicket.Delivery ")
        sb.AppendLine("LEFT OUTER JOIN Fra ON Alb.FraGuid = Fra.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Cca ON Fra.CcaGuid = Cca.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Transm ON Alb.TransmGuid = Transm.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Email AS UsrCreated ON Alb.UsrCreatedGuid = UsrCreated.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwDeliveryTrackingTrp Trp ON Alb.Emp=Trp.Emp AND Alb.Yea = Trp.Yea AND Alb.Alb = Trp.Alb ")
        sb.AppendLine("LEFT OUTER JOIN Trp Trp2 ON Alb.TrpGuid = Trp2.Guid ")


        If oUser IsNot Nothing Then
            Select Case oUser.Rol.id
                Case DTORol.Ids.cliLite, DTORol.Ids.cliFull
                    sb.AppendLine("INNER JOIN Email_Clis ON Alb.CliGuid = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
                Case Else
                    '(veure clausula WHERE)
            End Select
        End If


        sb.AppendLine("WHERE Alb.Emp = " & oEmp.Id & " ")

        If oCodis IsNot Nothing Then
            sb.AppendLine("AND (")
            For Each oCodi In oCodis
                If Not oCodi = oCodis.First Then sb.AppendLine("OR ")
                sb.AppendLine("Alb.Cod = " & oCodi & " ")
            Next
            sb.AppendLine(") ")
        End If

        If Group = False And oContact IsNot Nothing Then
            sb.AppendLine("AND Alb.CliGuid = '" & oContact.Guid.ToString & "' ")
        End If
        If iYear <> 0 Then
            sb.AppendLine("AND Alb.Yea = " & iYear & " ")
        End If

        If oUser IsNot Nothing Then
            Select Case oUser.Rol.id
                Case DTORol.Ids.rep, DTORol.Ids.comercial
                Case DTORol.Ids.manufacturer
            End Select
        End If

        If pendentsDeCobro <> DTODelivery.RetencioCods.notSet Then
            sb.AppendFormat("AND Alb.Yea > {0} ", 2006)
            sb.AppendFormat("AND (Alb.Cod={0} Or Alb.Cod={1}) ", CInt(DTOPurchaseOrder.Codis.client), CInt(DTOPurchaseOrder.Codis.reparacio))
            sb.AppendFormat("AND Alb.RetencioCod={0} ", CInt(pendentsDeCobro))
            sb.AppendLine("AND Alb.Eur>0 ")
        End If

        If altresPorts <> DTOCustomer.PortsCodes.notSet Then
            sb.AppendFormat("AND Alb.PortsCod = {0} ", CInt(DTOCustomer.PortsCodes.altres))
            sb.AppendFormat("AND Alb.Yea > {0} ", 2016)
            'sb.AppendFormat("AND Alb.MgzGuid = '{0}' ", (DefaultLoader.Find(DTODefault.Codis.Mgz, oEmp)).Value.ToString())
            sb.AppendFormat("AND Alb.MgzGuid = '{0}' ", oEmp.Mgz.Guid.ToString())
            sb.AppendFormat("AND Alb.Cod <> {0} ", CInt(DTOPurchaseOrder.Codis.proveidor))
        End If

        sb.AppendLine("ORDER BY Alb.Yea DESC, Alb.Alb DESC")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oInvoice As DTODeliveryHeader.DTOInvoice = Nothing
            If Not IsDBNull(oDrd("FraGuid")) Then
                oInvoice = New DTODeliveryHeader.DTOInvoice()
                With oInvoice
                    .Guid = oDrd("FraGuid")
                    .num = SQLHelper.GetStringFromDataReader(oDrd("FraId"))
                End With
            End If

            Dim oTransm As DTODeliveryHeader.DTOTransmisio = Nothing
            If Not IsDBNull(oDrd("TransmGuid")) Then
                oTransm = New DTODeliveryHeader.DTOTransmisio()
                oTransm.Guid = oDrd("TransmGuid")
                oTransm.id = SQLHelper.GetIntegerFromDataReader(oDrd("Transm"))
            End If

            Dim oTrp As DTODeliveryHeader.DTOTransportista = Nothing
            If Not IsDBNull(oDrd("TrpGuid")) Then
                oTrp = New DTODeliveryHeader.DTOTransportista()
                oTrp.Guid = oDrd("TrpGuid")
                oTrp.abr = SQLHelper.GetStringFromDataReader(oDrd("TrpNom"))
                If String.IsNullOrEmpty(oTrp.abr) Then oTrp.abr = "(" & SQLHelper.GetStringFromDataReader(oDrd("TrpNif")) & ")"
            ElseIf Not IsDBNull(oDrd("TrpGuid2")) Then
                oTrp = New DTODeliveryHeader.DTOTransportista()
                oTrp.Guid = oDrd("TrpGuid2")
                oTrp.abr = SQLHelper.GetStringFromDataReader(oDrd("TrpAbr"))
            End If

            Dim oItem As New DTODeliveryHeader()
            With oItem
                .Guid = oDrd("AlbGuid")
                .Id = oDrd("AlbId")
                .Fch = oDrd("AlbFch")
                .Cod = oDrd("Cod")
                .PortsCod = oDrd("PortsCod")
                .Customer = New DTODeliveryHeader.DTOCustomer()
                .Customer.Guid = oDrd("CliGuid")
                If IsDBNull(oDrd("TicketId")) Then
                    .Customer.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                Else
                    .Customer.FullNom = String.Format("ticket {0} de consumidor {1} {2}", oDrd("TicketId"), oDrd("TicketNom"), oDrd("TicketCognom"))
                End If
                .Import = New DTODeliveryHeader.DTOAmt
                .Import.eur = SQLHelper.GetDecimalFromDataReader(oDrd("AlbEur"))
                .ImportAdicional = New DTODeliveryHeader.DTOAmt
                .ImportAdicional.eur = SQLHelper.GetDecimalFromDataReader(oDrd("AlbImportAdicional"))
                .Invoice = oInvoice
                .Facturable = SQLHelper.GetBooleanFromDatareader(oDrd("Facturable"))
                .Transmisio = oTransm
                .CashCod = SQLHelper.GetIntegerFromDataReader(oDrd("CashCod"))
                .Transportista = oTrp
                .Tracking = SQLHelper.GetStringFromDataReader(oDrd("Tracking"))
                .UsrLog = SQLHelper.getUsrLog2FromDataReader(oDrd, "UsrCreatedGuid")
            End With
            retval.Add(oItem)

        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function IntrastatPending(oEmp As DTOEmp, oYearMonth As DTOYearMonth) As List(Of DTODelivery)
        Dim retval As New List(Of DTODelivery)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Alb.Fch AS AlbFch, Alb.Alb AS AlbId, CliGral.RaoSocial, Alb.Incoterm ")
        sb.AppendLine(", Arc.Guid AS ArcGuid, Arc.Qty, Arc.Eur, Arc.Cur, Arc.Pts, Arc.Dto, Arc.Bundle ")
        sb.AppendLine(", Arc.ArtGuid, Arc.Lin, VwSkuNom.*, MadeIn.Iso AS MadeInISO ")
        sb.AppendLine(", Pnc.PdcGuid ")
        sb.AppendLine(", Arc.AlbGuid, Alb.CliGuid AS AlbCliGuid ")
        sb.AppendLine(", VwZip.ZipGuid AS AlbZipGuid, VwZip.LocationGuid AS AlbLocationGuid, VwZip.ZonaGuid AS AlbZonaGuid, VwZip.ExportCod AS AlbExportCod, VwZip.CountryGuid AS AlbCountryGuid, VwZip.CountryISO AS AlbCountryISO, VwZip.ExportCod AS AlbCountryExportCod ")
        sb.AppendLine("FROM Alb ")
        sb.AppendLine("INNER JOIN CliGral ON Alb.CliGuid = CliGral.Guid ")
        sb.AppendLine("INNER JOIN VwZip ON Alb.Zip = VwZip.ZipGuid ")
        sb.AppendLine("INNER JOIN Arc ON Alb.Guid = Arc.AlbGuid ")
        sb.AppendLine("INNER JOIN VwSkuNom ON Arc.ArtGuid = VwSkuNom.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN Country AS MadeIn ON VwSkuNom.MadeIn = MadeIn.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Pnc ON Arc.PncGuid = Pnc.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Pdc ON Pnc.PdcGuid = Pdc.Guid ")
        sb.AppendLine("LEFT OUTER JOIN IntrastatPartida ON Alb.Guid = IntrastatPartida.Tag ")
        sb.AppendLine("WHERE Alb.Emp=" & oEmp.Id & " ")
        sb.AppendLine("AND Year(Alb.Fch)=" & oYearMonth.year & " ")
        sb.AppendLine("AND Month(Alb.Fch)=" & oYearMonth.month & " ")
        sb.AppendLine("AND Arc.Qty > 0 ")
        sb.AppendLine("AND Arc.Eur > 0 ")
        sb.AppendLine("AND Alb.Cod =" & DTOPurchaseOrder.Codis.proveidor & " ")
        sb.AppendLine("AND Alb.ExportCod = " & DTOInvoice.ExportCods.intracomunitari & " ") 'Canviat de wZip.ExportCod a Alb.ExportCod el 08/06/21
        'sb.AppendLine("AND VwZip.CountryISO <> 'ES' ")
        sb.AppendLine("AND IntrastatPartida.Lin IS NULL ")
        sb.AppendLine("ORDER BY Alb.Alb, Arc.Lin ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)

        Dim oDelivery As New DTODelivery
        Dim oOrder As New DTOPurchaseOrder

        Do While oDrd.Read


            If oDrd("ArcGuid").Equals(oDrd("Bundle")) Then
                'evita els bundles i declara tant sols els components
            Else
                If Not oDelivery.Guid.Equals(oDrd("AlbGuid")) Then
                    oDelivery = New DTODelivery(oDrd("AlbGuid"))
                    With oDelivery
                        .Emp = oEmp
                        .Proveidor = New DTOProveidor(oDrd("AlbCliGuid"))
                        .Proveidor.Nom = SQLHelper.GetStringFromDataReader(oDrd("RaoSocial"))
                        .Fch = oDrd("AlbFch")
                        .Id = oDrd("AlbId")
                        .Address = New DTOAddress
                        .Address.Zip = SQLHelper.GetZipFromDataReader(oDrd, ZipGuidField:="AlbZipGuid", LocationGuidField:="AlbLocationGuid", ZonaGuidField:="AlbZonaGuid", ZonaExportField:="AlbExportCod", CountryGuidField:="AlbCountryGuid", CountryISOField:="AlbCountryISO", ExportCodField:="AlbCountryExportCod")
                        .ExportCod = .Address.Zip.Location.Zona.ExportCod
                        .Incoterm = SQLHelper.GetIncotermFromDataReader(oDrd("Incoterm"))
                        .Items = New List(Of DTODeliveryItem)
                    End With
                    retval.Add(oDelivery)
                End If

                Dim oItem As New DTODeliveryItem(oDrd("ArcGuid"))
                Dim oSku As DTOProductSku = SQLHelper.GetProductFromDataReader(oDrd)
                If oSku.MadeIn IsNot Nothing Then
                    oSku.MadeIn.ISO = SQLHelper.GetStringFromDataReader(oDrd("MadeInISO"))
                End If

                If Not oOrder.Guid.Equals(oDrd("PdcGuid")) Then
                    oOrder = New DTOPurchaseOrder(oDrd("PdcGuid"))
                End If

                Dim oPOItem As New DTOPurchaseOrderItem
                With oPOItem
                    .PurchaseOrder = oOrder
                    .Sku = oSku
                End With

                oItem.PurchaseOrderItem = oPOItem
                With oItem
                    .Lin = oDrd("Lin")
                    .Sku = oSku
                    .Price = DTOAmt.Factory(CDec(oDrd("Eur")), oDrd("Cur").ToString, CDec(oDrd("Pts")))
                    .Dto = SQLHelper.GetDecimalFromDataReader(oDrd("Dto"))
                    .Qty = oDrd("Qty")
                    .Delivery = oDelivery
                End With
                oDelivery.Items.Add(oItem)
            End If
        Loop

        oDrd.Close()

        Return retval
    End Function


    Shared Function Entrades(oProveidor As DTOProveidor) As List(Of DTODelivery)
        Dim retval As New List(Of DTODelivery)

        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Alb.Guid, Alb.alb, Alb.fch, Alb.Eur ")
        sb.AppendLine(", Importacio.Id As RemesaId, Importacio.HdrGuid As RemesaGuid ")
        sb.AppendLine("FROM Alb ")
        sb.AppendLine("LEFT OUTER JOIN (")
        sb.AppendLine("                 Select ImportDtl.Guid As DtlGuid, ImportHdr.Guid As HdrGuid, ImportHdr.Id ")
        sb.AppendLine("                 FROM ImportHdr ")
        sb.AppendLine("                 INNER JOIN ImportDtl On ImportHdr.Guid = ImportDtl.HeaderGuid ")
        sb.AppendLine("                ) Importacio On Alb.Guid = Importacio.DtlGuid ")
        sb.AppendLine("WHERE Alb.CliGuid = '" & oProveidor.Guid.ToString & "' ")
        sb.AppendLine("AND Alb.Cod = " & DTOPurchaseOrder.Codis.proveidor & " ")
        sb.AppendLine("ORDER BY Year(Alb.Fch) DESC, Alb.alb DESC ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oDelivery As New DTODelivery(oDrd("Guid"))
            With oDelivery
                .Emp = oProveidor.Emp
                .Id = oDrd("Alb")
                .Fch = oDrd("Fch")
                .Cod = DTOPurchaseOrder.Codis.proveidor
                .Proveidor = oProveidor
                .Import = DTOAmt.Factory(CDec(oDrd("Eur")))
                If Not IsDBNull(oDrd("RemesaGuid")) Then
                    .Importacio = New DTOImportacio(oDrd("RemesaGuid"))
                    .Importacio.id = oDrd("RemesaId")
                End If
            End With
            retval.Add(oDelivery)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function Update(oDeliveries As List(Of DTODelivery), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            For Each oDelivery As DTODelivery In oDeliveries
                DeliveryLoader.Update(oDelivery, oTrans)
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

    Shared Function Last(oContact As DTOContact) As DTODelivery
        Dim retval As DTODelivery = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT TOP 1 Guid, Alb, Fch FROM Alb WHERE CliGuid='" & oContact.Guid.ToString & "' ORDER BY Yea DESC, Alb DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTODelivery(oDrd("Guid"))
            With retval
                If TypeOf oContact Is DTOCustomer Then
                    .Customer = oContact
                ElseIf TypeOf oContact Is DTOProveidor Then
                    .Proveidor = oContact
                End If
                .Emp = oContact.Emp
                .Id = oDrd("Alb")
                .Fch = oDrd("Fch")
            End With
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function PendentsDeTransmetre(oMgz As DTOMgz) As List(Of DTODelivery)
        Dim retval As New List(Of DTODelivery)

        Dim SQL As String = "SELECT Alb.Guid as AlbGuid, Alb.Yea, Alb.Alb, Alb.Fch, Alb.CliGuid, Alb.Nom, Alb.Eur, Alb.CashCod, Alb.EtiquetesTransport, " _
                    & "Alb.TransmGuid, Alb.FraGuid, Fra.Fra AS FraId, " _
                    & "Alb.Zip, Zip.Location, Location.Nom AS LocationNom, " _
                    & "Email.NickName, Alb.UsrCreatedGuid, " _
                    & "Trp.Abr as TrpAbr, Alb.TrpGuid, Alb.PortsCod, " _
                    & "CliGral.FullNom " _
                    & "FROM Alb " _
                    & "INNER JOIN CliGral ON Alb.CliGuid=CliGral.Guid " _
                    & "LEFT OUTER JOIN Zip ON Alb.Zip=Zip.Guid " _
                    & "LEFT OUTER JOIN Location ON Zip.Location=Location.Guid " _
                    & "LEFT OUTER JOIN Fra ON Alb.FraGuid=Fra.Guid " _
                    & "LEFT OUTER JOIN Email ON Alb.UsrCreatedGuid=Email.Guid " _
                    & "LEFT OUTER JOIN Trp ON Alb.TrpGuid=Trp.Guid " _
                    & "WHERE TransmGuid IS NULL " _
                    & "AND Alb.MgzGuid = '" & oMgz.Guid.ToString & "' " _
                    & "AND Alb.PortsCod<>" & DTOCustomer.PortsCodes.altres & " " _
                    & "AND Alb.PortsCod<>" & DTOCustomer.PortsCodes.entregatEnMa & " " _
                    & "AND Alb.PortsCod<>10 " _
                    & "AND Alb.RetencioCod = 0 " _
                    & "AND Alb.Cod=" & CInt(DTOPurchaseOrder.Codis.client) & " " _
                    & "ORDER BY Alb.Yea, Alb.Alb"

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oDelivery As New DTODelivery(oDrd("AlbGuid"))
            With oDelivery
                .Emp = oMgz.Emp
                .Id = oDrd("Alb")
                .Fch = oDrd("Fch")
                .Customer = New DTOCustomer(oDrd("CliGuid"))
                .Customer.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                .Nom = oDrd("Nom")
                If Not IsDBNull(oDrd("Zip")) Then
                    .Address = New DTOAddress
                    .Address.Zip = New DTOZip(oDrd("Zip"))
                    If Not IsDBNull(oDrd("Location")) Then
                        .Address.Zip.Location = New DTOLocation(oDrd("Location"))
                        .Address.Zip.Location.Nom = SQLHelper.GetStringFromDataReader(oDrd("LocationNom"))
                    End If
                End If
                .Import = DTOAmt.Factory(CDec(oDrd("Eur")))
                .CashCod = oDrd("CashCod")

                .Mgz = oMgz

                If Not IsDBNull(oDrd("FraGuid")) Then
                    .Invoice = New DTOInvoice(oDrd("FraGuid"))
                    .Invoice.Num = oDrd("FraId")
                End If

                If Not IsDBNull(oDrd("UsrCreatedGuid")) Then
                    Dim oUsrCreated As New DTOUser(DirectCast(oDrd("UsrCreatedGuid"), Guid))
                    oUsrCreated.NickName = SQLHelper.GetStringFromDataReader(oDrd("Nickname"))
                End If

                .PortsCod = oDrd("PortsCod")
                If Not IsDBNull(oDrd("TrpGuid")) Then
                    .Transportista = New DTOTransportista(DirectCast(oDrd("TrpGuid"), Guid))
                    If IsDBNull(oDrd("TrpAbr")) Then
                        .Transportista.Abr = "¿transportista?"
                    Else
                        .Transportista.Abr = oDrd("TrpAbr")
                    End If
                End If

                .EtiquetesTransport = SQLHelper.GetDocFileFromDataReaderHash(oDrd("EtiquetesTransport"))
            End With

            retval.Add(oDelivery)

        Loop
        oDrd.Close()

        Return retval
    End Function

    Shared Function pendentsDeFacturar(oEmp As DTOEmp, Optional oCustomer As DTOCustomer = Nothing) As List(Of DTODelivery)
        Dim retval As New List(Of DTODelivery)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Arc.AlbGuid, Alb.Alb, Alb.Fch, Alb.CliGuid AS AlbCliGuid, CliGral.Guid AS FraCliGuid ")
        sb.AppendLine(", Alb.CashCod, Alb.PortsCod, Alb.Cobro, Alb.Cod, Alb.Nom AS AlbCustomerNom, Alb.Eur AS AlbEur, Alb.ExportCod, Alb.Incoterm, Alb.Deutor ")
        sb.AppendLine(", Alb.TransmGuid, Transm.Transm AS TransmId ")
        sb.AppendLine(", Pnc.PdcGuid, Pdc.Fch AS PdcFch, Pdc.Pdd ")
        sb.AppendLine(", Art.Ref ")
        sb.AppendLine(", Arc.Guid AS ArcGuid, Arc.PncGuid, Arc.ArtGuid, Arc.Qty, Arc.Eur, Arc.Dto, Arc.Lin, Arc.Bundle ")
        sb.AppendLine(", CliGral.RaoSocial, CliGral.FullNom, CliGral.LangId, AlbCustomer.Ref ")
        sb.AppendLine(", CliGral.NIF, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod ")
        sb.AppendLine(", FraCustomer.IVA, FraCustomer.Req, FraCustomer.SuProveedorNum ")
        sb.AppendLine(", FraCustomer.Cfp, FraCustomer.Mes AS CreditMonths, FraCustomer.PaymentDays, FraCustomer.Vacaciones ")
        sb.AppendLine(", VwAddress.Adr, VwAddress.ZipGuid, VwAddress.ZipCod, VwAddress.LocationGuid, VwAddress.LocationNom ")
        sb.AppendLine(", VwAddress.ZonaGuid, VwAddress.ZonaNom, VwAddress.ProvinciaGuid, VwAddress.ProvinciaNom, VwAddress.CountryGuid, VwAddress.CountryEsp, VwAddress.ExportCod ")
        sb.AppendLine(", SkuNomLlarg.Esp AS SkuNomLlargEsp, SkuNomLlarg.Cat AS SkuNomLlargCat, SkuNomLlarg.Eng AS SkuNomLlargEng, SkuNomLlarg.Por AS SkuNomLlargPor ")
        sb.AppendLine("FROM Alb ")
        sb.AppendLine("INNER JOIN Arc ON Alb.Guid = Arc.AlbGuid ")
        sb.AppendLine("INNER JOIN Art ON Arc.ArtGuid = Art.Guid ")
        sb.AppendLine("INNER JOIN VwLangText SkuNomLlarg ON Arc.ArtGuid = SkuNomLlarg.Guid AND SkuNomLlarg.Src = 27")
        sb.AppendLine("INNER JOIN Pnc ON Arc.PncGuid = Pnc.Guid ")
        sb.AppendLine("INNER JOIN Pdc ON Pnc.PdcGuid = Pdc.Guid ")
        sb.AppendLine("INNER JOIN CliClient AlbCustomer ON Alb.CliGuid = AlbCustomer.Guid ")
        sb.AppendLine("INNER JOIN CliGral ON CliGral.Guid=(CASE WHEN AlbCustomer.CcxGuid IS NULL THEN AlbCustomer.Guid ELSE AlbCustomer.CcxGuid END) ")
        sb.AppendLine("INNER JOIN CliClient FraCustomer ON CliGral.Guid = FraCustomer.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Transm ON Alb.TransmGuid = Transm.Guid ")
        sb.AppendLine("INNER JOIN VwAddress ON CliGral.Guid = VwAddress.SrcGuid ")
        sb.AppendLine("WHERE Alb.Emp = " & oEmp.Id & " ")
        sb.AppendLine("AND Alb.FraGuid IS NULL ")
        sb.AppendLine("AND Alb.Facturable = 1 ")
        sb.AppendLine("AND (Alb.Cod=2 OR Alb.Cod = 4) ")

        If oCustomer IsNot Nothing Then
            sb.AppendLine("AND FraCustomer.Guid = '" & oCustomer.Guid.ToString & "' ")
        End If

        sb.AppendLine("ORDER BY Alb.Yea, Alb.Alb, Arc.Lin ")

        Dim oDelivery As New DTODelivery()
        Dim oPurchaseOrder As New DTOPurchaseOrder

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oDelivery.Guid.Equals(oDrd("AlbGuid")) Then
                'If oDrd("Alb") = 4258 Then Stop
                'Dim oIban As DTOIban = Nothing
                'If Not IsDBNull(oDrd("IbanGuid")) Then
                'oIban = New DTOIban(oDrd("IbanGuid"))
                'With oIban
                '.Digits = SQLHelper.GetStringFromDataReader(oDrd("Ccc"))
                'If Not IsDBNull(oDrd("BankBranch")) Then
                '.BankBranch = New DTOBankBranch(oDrd("BankBranch"))
                '.BankBranch.Address = SQLHelper.GetStringFromDataReader(oDrd("BankBranchAdr"))
                'If Not IsDBNull(oDrd("BankBranchLocation")) Then
                '.BankBranch.Location = New DTOLocation(oDrd("BankBranchLocation"))
                '.BankBranch.Location.Nom = SQLHelper.GetStringFromDataReader(oDrd("BankBranchLocationNom"))
                'End If
                '   If Not IsDBNull(oDrd("Bank")) Then
                '  .BankBranch.Bank = New DTOBank(oDrd("Bank"))
                ' With .BankBranch.Bank
                '.RaoSocial = SQLHelper.GetStringFromDataReader(oDrd("BankNom"))
                '.NomComercial = SQLHelper.GetStringFromDataReader(oDrd("BankAlias"))
                'End With
                'End If
                'End If
                '   End With
                'End If

                Dim oPaymentTerms As New DTOPaymentTerms
                With oPaymentTerms
                    '.Iban = oIban
                    .cod = SQLHelper.GetIntegerFromDataReader(oDrd("Cfp"))
                    .months = SQLHelper.GetIntegerFromDataReader(oDrd("CreditMonths"))
                    .paymentDays = CustomerLoader.GetPaymentDaysFromDataReader(oDrd("PaymentDays"))
                    .vacaciones = CustomerLoader.DecodedVacacions(oDrd("Vacaciones"))
                End With

                Dim oFraCustomer As New DTOCustomer(oDrd("FraCliGuid"))
                With oFraCustomer
                    .Emp = oEmp
                    .Nom = oDrd("RaoSocial")
                    .FullNom = oDrd("FullNom")
                    .Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
                    .SuProveedorNum = SQLHelper.GetStringFromDataReader(oDrd("SuProveedorNum"))
                    .Address = SQLHelper.GetAddressFromDataReader(oDrd)
                    .Address.Zip.Location.Zona.IsLoaded = True 'evita roundtrip per Zona.IsExport
                    .Lang = DTOLang.Factory(oDrd("LangId"))
                    .iva = oDrd("Iva")
                    .req = oDrd("Req")
                    .paymentTerms = oPaymentTerms
                End With

                Dim oAlbCustomer As New DTOCustomer(oDrd("AlbCliGuid"))
                With oAlbCustomer
                    .Nom = oDrd("AlbCustomerNom")
                    .FullNom = oDrd("FullNom")
                    .Ref = SQLHelper.GetStringFromDataReader(oDrd("Ref"))
                    .Ccx = oFraCustomer
                    DirectCast(oAlbCustomer, DTOContact).IsLoaded = True
                End With

                oDelivery = New DTODelivery(oDrd("AlbGuid"))
                With oDelivery
                    .Emp = oEmp
                    .Id = oDrd("Alb")
                    .Fch = oDrd("Fch")
                    .Cod = oDrd("Cod")
                    .Customer = oAlbCustomer
                    .Deutor = SQLHelper.GetGuidFromDataReader(oDrd("Deutor"))
                    .CashCod = oDrd("CashCod")
                    .PortsCod = oDrd("PortsCod")
                    .Import = DTOAmt.Factory(oDrd("AlbEur"))
                    .Facturable = True
                    .ExportCod = oDrd("ExportCod")

                    .Incoterm = SQLHelper.GetIncotermFromDataReader(oDrd("Incoterm"))
                    .FchCobroReembolso = SQLHelper.GetFchFromDataReader(oDrd("Cobro"))
                    If Not IsDBNull(oDrd("TransmGuid")) Then
                        .Transmisio = New DTOTransmisio(oDrd("TransmGuid"))
                        With .Transmisio
                            .Id = oDrd("TransmId")
                        End With
                    End If
                    .Items = New List(Of DTODeliveryItem)
                End With
                retval.Add(oDelivery)
            End If
            If Not oPurchaseOrder.Guid.Equals(oDrd("PdcGuid")) Then
                oPurchaseOrder = New DTOPurchaseOrder(oDrd("PdcGuid"))
                With oPurchaseOrder
                    .Fch = SQLHelper.GetFchFromDataReader(oDrd("PdcFch"))
                    .Concept = SQLHelper.GetStringFromDataReader(oDrd("Pdd"))
                End With
            End If
            Dim oSku As New DTOProductSku(oDrd("ArtGuid"))
            oSku.RefProveidor = SQLHelper.GetStringFromDataReader(oDrd("Ref"))
            SQLHelper.LoadLangTextFromDataReader(oSku.nomLlarg, oDrd, "SkuNomLlargEsp", "SkuNomLlargCat", "SkuNomLlargEng", "SkuNomLlargPor")
            Dim oPurchaseOrderItem As New DTOPurchaseOrderItem(oDrd("PncGuid"))
            With oPurchaseOrderItem
                .PurchaseOrder = oPurchaseOrder
                .Sku = oSku
            End With
            Dim item As New DTODeliveryItem(oDrd("ArcGuid"))
            With item
                .Delivery = oDelivery
                .PurchaseOrderItem = oPurchaseOrderItem
                .Qty = oDrd("Qty")
                .Price = DTOAmt.Factory(oDrd("Eur"))
                .Dto = SQLHelper.GetDecimalFromDataReader(oDrd("Dto"))
                .sku = oSku
                .lin = oDrd("Lin")
            End With

            If DeliveryLoader.isBundleChild(oDrd) Then
                Dim oBundleParent = DeliveryLoader.BundleParent(oDelivery, oDrd("Bundle"))
                oBundleParent.Bundle.Add(item)
            Else
                oDelivery.items.Add(item)
            End If

        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function PurchaseOrders(oDeliveries As List(Of DTODelivery)) As List(Of DTOPurchaseOrder)
        Dim retval As New List(Of DTOPurchaseOrder)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Arc.PdcGuid ")
        sb.AppendLine("FROM Arc ")
        sb.AppendLine("WHERE ( ")
        For Each oDelivery As DTODelivery In oDeliveries
            If Not oDelivery.Equals(oDeliveries.First) Then
                sb.Append("OR ")
            End If
            sb.AppendLine("Arc.AlbGuid = '" & oDelivery.Guid.ToString & "' ")
        Next
        sb.AppendLine("GROUP BY Arc.PdcGuid ")


        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oPurchaseOrder As New DTOPurchaseOrder(oDrd("PdcGuid"))
            retval.Add(oPurchaseOrder)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function NumsToRecycle(oEmp As DTOEmp, DtFch As Date) As List(Of Integer)
        Dim retval As New List(Of Integer)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT TOP 1000 Alb, Fch, TransmGuid ")
        sb.AppendLine("FROM Alb ")
        sb.AppendLine("WHERE Alb.Emp =" & oEmp.Id & " ")
        sb.AppendLine("ORDER BY Alb.Yea DESC, Alb.Alb DESC")
        Dim SQL As String = sb.ToString
        Dim i As Integer
        Dim oDrd As SqlClient.SqlDataReader = DAL.SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            'If Not IsDBNull(oDrd("TransmGuid")) Then Exit Do
            Dim tmpFch As Date = CDate(oDrd("FCH"))
            'If Not (tmpFch.Year = DtFch.Year And tmpFch.DayOfYear = DtFch.DayOfYear) Then Exit Do

            If i = 0 Then
                i = oDrd("Alb")
            Else
                i = i - 1
                Do While oDrd("Alb") < i
                    retval.Add(i)
                    i = i - 1
                Loop
            End If
        Loop
        oDrd.Close()

        Return retval
    End Function

    Shared Function reZip(exs As List(Of Exception), oZipTo As DTOZip, oDeliveries As List(Of DTODelivery)) As Integer
        Dim retval As Integer

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Dim sb As New Text.StringBuilder
            sb.AppendLine("UPDATE Alb SET Alb.Zip='" & oZipTo.Guid.ToString & "' ")
            sb.AppendLine("WHERE (")
            For Each oDelivery In oDeliveries
                If Not oDelivery.Equals(oDeliveries.First) Then sb.AppendLine("OR ")
                sb.AppendLine("Alb.Guid='" & oDelivery.Guid.ToString & "' ")
            Next
            sb.AppendLine(")")
            Dim SQL As String = sb.ToString
            retval = SQLHelper.ExecuteNonQuery(SQL, oTrans)

            oTrans.Commit()
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function
End Class
