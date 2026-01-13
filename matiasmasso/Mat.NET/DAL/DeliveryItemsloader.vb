Public Class DeliveryItemsloader

    Shared Function Factory(oContact As DTOContact, oCod As DTOPurchaseOrder.Codis, oMgz As DTOMgz) As List(Of DTODeliveryItem)
        Dim retval As New List(Of DTODeliveryItem)
        Dim sb As New Text.StringBuilder

        sb.AppendLine("SELECT Pnc.Guid AS PncGuid, Pnc.PdcGuid, Pdc.Pdc, Pnc.ArtGuid ")
        sb.AppendLine(", Pnc.Lin, Pnc.Pn2, Pnc.PTS, Pnc.Dto, Pnc.Dt2, Pnc.Carrec, Pnc.Bundle ")
        sb.AppendLine(", Pnc.RepGuid, Pnc.COM, Pdc.Pot ")
        sb.AppendLine(", Pnc.ErrCod, Pnc.ErrDsc ")
        sb.AppendLine(", Pdc.Pdc, Pdc.Fch, Pdc.Pdd, Pdc.FchMin, Pdc.FchMax, Pdc.TotJunt, Pdc.Obs, Pdc.EtiquetesTransport ")
        sb.AppendLine(", VwSkuNom.* ")
        sb.AppendLine(", VwSkuStocks.Stock ")
        sb.AppendLine(", VwSkuPncs.Clients, VwSkuPncs.ClientsAlPot, VwSkuPncs.ClientsEnProgramacio, VwSkuPncs.Pn1, VwSkuPncs.ClientsBlockStock ")
        sb.AppendLine("FROM Pnc ")
        sb.AppendLine("INNER JOIN Pdc ON Pnc.PdcGuid=PDC.Guid ")
        sb.AppendLine("INNER JOIN Art ON Pnc.ArtGuid=Art.Guid ")
        sb.AppendLine("INNER JOIN VwSkuNom ON Pnc.ArtGuid=VwSkuNom.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundleStocks VwSkuStocks ON Pnc.ArtGuid=VwSkuStocks.SkuGuid AND VwSkuStocks.MgzGuid = '" & oMgz.Guid.ToString & "' ")
        sb.AppendLine("left outer JOIN VwSkuAndBundlePncs VwSkuPncs ON Pnc.ArtGuid=VwSkuPncs.SkuGuid ")
        sb.AppendLine("WHERE Pdc.CliGuid = '" & oContact.Guid.ToString & "' ")
        sb.AppendLine("AND Pnc.Pn2 <> 0 ")
        sb.AppendLine("AND Pnc.ErrCod = 0 ")
        sb.AppendLine("AND Pdc.Cod =" & CInt(oCod) & " ")
        sb.AppendLine("ORDER BY Pdc.YEA, Pdc.Pdc, Pnc.Lin")

        Dim SQL As String = sb.ToString

        Dim oDrd As SqlClient.SqlDataReader = DAL.SQLHelper.GetDataReader(SQL)
        Dim oPurchaseOrder As New DTOPurchaseOrder

        Do While oDrd.Read
            If Not oPurchaseOrder.Guid.Equals(oDrd("PdcGuid")) Then
                oPurchaseOrder = New DTOPurchaseOrder(oDrd("PdcGuid"))
                With oPurchaseOrder
                    .Cod = oCod
                    .Num = oDrd("Pdc")
                    .Fch = oDrd("Fch")
                    .Concept = oDrd("Pdd")
                    .FchDeliveryMin = SQLHelper.GetFchFromDataReader(oDrd("FchMin"))
                    .FchDeliveryMax = SQLHelper.GetFchFromDataReader(oDrd("FchMax"))
                    .TotJunt = oDrd("TotJunt")
                    .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                    Select Case oCod
                        Case DTOPurchaseOrder.Codis.proveidor
                            .Proveidor = DTOProveidor.FromContact(oContact)
                        Case Else
                            .Customer = DTOCustomer.FromContact(oContact)
                    End Select
                    .Cod = oCod
                    .Pot = SQLHelper.GetBooleanFromDatareader(oDrd("Pot"))
                    .EtiquetesTransport = SQLHelper.GetDocFileFromDataReaderHash(oDrd("EtiquetesTransport"))
                End With
            End If

            Dim oLineItmPnc As New DTOPurchaseOrderItem(DirectCast(oDrd("PncGuid"), Guid))
            With oLineItmPnc
                .PurchaseOrder = oPurchaseOrder
                .Lin = oDrd("Lin")
                .Sku = SQLHelper.GetProductFromDataReader(oDrd)
                .Pending = CInt(oDrd("Pn2"))
                .Price = DTOAmt.Factory(CDec(oDrd("Pts")))
                .Dto = SQLHelper.GetDecimalFromDataReader(oDrd("Dto"))
                .Dt2 = SQLHelper.GetDecimalFromDataReader(oDrd("Dt2"))
                If Not IsDBNull(oDrd("RepGuid")) Then
                    Dim oRepGuid As Guid = oDrd("RepGuid")
                    .RepCom = New DTORepCom
                    .RepCom.Rep = New DTORep(oDrd("RepGuid"))
                    .RepCom.Com = CDec(oDrd("Com"))
                End If
                .ErrCod = SQLHelper.GetIntegerFromDataReader(oDrd("ErrCod"))
                .ErrDsc = SQLHelper.GetStringFromDataReader(oDrd("ErrDsc"))
            End With

            Dim iStock As Integer = DAL.SQLHelper.GetIntegerFromDataReader(oDrd("Stock"))
            Dim iClients As Integer = DAL.SQLHelper.GetIntegerFromDataReader(oDrd("Clients"))
            Dim iClientsAlPot As Integer = DAL.SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot"))
            Dim iClientsEnProgramacio As Integer = DAL.SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
            Dim iClientsBlockStock As Integer = DAL.SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))


            Dim oItem As New DTODeliveryItem
            With oItem
                .PurchaseOrderItem = oLineItmPnc
                .RepCom = .PurchaseOrderItem.RepCom
                .Sku = .PurchaseOrderItem.Sku
                .Price = .PurchaseOrderItem.Price
                .Dto = .PurchaseOrderItem.Dto
                .Dt2 = .PurchaseOrderItem.Dt2
                .Sku.Stock = iStock - iClientsBlockStock
                .Sku.Clients = iClients - iClientsAlPot - iClientsEnProgramacio - iClientsBlockStock
                If oCod = DTOPurchaseOrder.Codis.client Then
                    .Qty = SuggestedSortida(oItem)
                End If
            End With

            If isBundleChild(oDrd) Then
                Dim oBundleParent = BundleParent(retval, oDrd("Bundle"))
                oBundleParent.Bundle.Add(oItem)
            Else
                retval.Add(oItem)
            End If

        Loop
        oDrd.Close()
        Return retval
    End Function

    Private Shared Function BundleParent(items As List(Of DTODeliveryItem), oPncGuid As Guid) As DTODeliveryItem
        Dim retval = items.FirstOrDefault(Function(x) x.PurchaseOrderItem.Guid.Equals(oPncGuid))
        Return retval
    End Function

    Private Shared Function isBundleChild(odrd As SqlDataReader) As Boolean
        Dim retval As Boolean
        If Not IsDBNull(odrd("Bundle")) Then
            retval = Not odrd("Bundle").Equals(odrd("PncGuid"))
        End If
        Return retval
    End Function

    Shared Function SuggestedSortida(oItem As DTODeliveryItem) As Integer
        Dim retval As Integer
        If oItem.PurchaseOrderItem.ErrCod = DTOPurchaseOrderItem.ErrCods.Success Then
            Dim oPurchaseOrder = oItem.PurchaseOrderItem.PurchaseOrder
            Dim oSku = oItem.PurchaseOrderItem.Sku
            Dim DtFchMin As Date = oPurchaseOrder.FchDeliveryMin
            If (DtFchMin - DTO.GlobalVariables.Now()).Days > 7 Then
                retval = 0
            Else
                Dim BlNoStk As Boolean = oSku.NoStk
                Dim iPendent As Integer = oItem.PurchaseOrderItem.Pending

                If Not oPurchaseOrder.Pot Then
                    If BlNoStk Then
                        retval = iPendent
                    Else
                        If oSku.Stock <= 0 Then
                            retval = 0
                        ElseIf oSku.Stock > iPendent Then
                            retval = iPendent
                        Else
                            retval = oSku.Stock
                        End If
                    End If
                End If
            End If
        End If

        Return retval
    End Function

    Shared Function All(oPurchaseOrderItem As DTOPurchaseOrderItem) As List(Of DTODeliveryItem)
        Dim retval As New List(Of DTODeliveryItem)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Arc.* ")
        sb.AppendLine(", Alb.Alb AS AlbId, Alb.Fch, Alb.FraGuid, Alb.CliGuid, Alb.Cod AS AlbCod ")
        sb.AppendLine(", CliGral.FullNom ")
        sb.AppendLine(", Fra.Fra ")
        sb.AppendLine("FROM Arc ")
        sb.AppendLine("INNER JOIN Alb ON Arc.AlbGuid= Alb.Guid ")
        sb.AppendLine("INNER JOIN CliGral ON Alb.CliGuid= CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Fra ON Alb.FraGuid = Fra.Guid ")
        sb.AppendLine("WHERE Arc.PncGuid = '" & oPurchaseOrderItem.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY Alb.Fch, Arc.AlbGuid, Arc.Lin")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oDelivery As New DTODelivery
        Do While oDrd.Read

            If Not oDelivery.Guid.Equals(oDrd("AlbGuid")) Then
                Dim oInvoice As DTOInvoice = Nothing
                If Not IsDBNull(oDrd("FraGuid")) Then
                    oInvoice = New DTOInvoice(oDrd("FraGuid"))
                    oInvoice.Num = oDrd("Fra")
                End If

                oDelivery = New DTODelivery(oDrd("AlbGuid"))
                With oDelivery
                    .Id = oDrd("AlbId")
                    .Fch = oDrd("Fch")
                    .Cod = oDrd("AlbCod")
                    If .Cod = DTOPurchaseOrder.Codis.proveidor Then
                        .Proveidor = New DTOProveidor(oDrd("CliGuid"))
                    Else
                        .Customer = New DTOCustomer(oDrd("CliGuid"))
                    End If
                    .Contact.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                    .Invoice = oInvoice
                End With
            End If

            Dim item As New DTODeliveryItem(oDrd("Guid"))
            With item
                .Delivery = oDelivery
                .Qty = oDrd("Qty")
                .Price = DTOAmt.Factory(CDec(oDrd("Eur")))
                .Dto = SQLHelper.GetDecimalFromDataReader(oDrd("Dto"))
                .Dt2 = SQLHelper.GetDecimalFromDataReader(oDrd("Dt2"))
                .PurchaseOrderItem = oPurchaseOrderItem
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oProduct As DTOProduct) As Models.SkuInOutModel
        Dim retval As New Models.SkuInOutModel
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Arc.Guid AS ArcGuid, Arc.Qty, Arc.Eur, Arc.Dto, Arc.Dt2, Arc.Pmc, Arc.Cod AS ArcCod ")
        sb.AppendLine(", Alb.Guid AS DeliveryGuid, Alb.Alb, Alb.Fch, Alb.CliGuid, CliGral.FullNom ")
        sb.AppendLine(", Arc.MgzGuid, Mgz.Nom AS MgzNom, Mgz2.RaoSocial AS MgzRaoSocial ")
        sb.AppendLine(", Transm.Transm ")
        sb.AppendLine("FROM Arc ")
        sb.AppendLine("INNER JOIN Alb ON Arc.AlbGuid = Alb.Guid ")
        sb.AppendLine("INNER JOIN CliGral ON Alb.CliGuid = CliGral.Guid ")
        sb.AppendLine("INNER JOIN VwProductParent ON Arc.ArtGuid = VwProductParent.Child ")
        sb.AppendLine("LEFT OUTER JOIN Mgz ON arc.mgzGuid = Mgz.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral Mgz2 ON arc.mgzGuid = Mgz2.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Transm ON Alb.TransmGuid = Transm.Guid ")
        sb.AppendLine("WHERE VwProductParent.Parent='" & oProduct.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY Alb.Fch DESC, Alb.Alb DESC, Arc.Lin")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oDelivery As New DTODelivery
        Dim oContact As New DTOCustomer
        Dim oMgz As New DTOGuidNom.Compact
        oMgz.Guid = Guid.NewGuid()
        Dim item As Models.SkuInOutModel.Item = Nothing
        Do While oDrd.Read
            oMgz = Mgz(oMgz, oDrd, retval.Mgzs)
            item = New Models.SkuInOutModel.Item

            With item
                .Guid = oDrd("ArcGuid")
                .DeliveryId = oDrd("Alb")
                .TransmId = SQLHelper.GetIntegerFromDataReader(oDrd("Transm"))
                .Fch = oDrd("Fch")
                .Qty = oDrd("Qty")
                .Eur = oDrd("Eur")
                .Dto = SQLHelper.GetDecimalFromDataReader(oDrd("Dto"))
                .Dt2 = SQLHelper.GetDecimalFromDataReader(oDrd("Dt2"))
                .Pmc = SQLHelper.GetDecimalFromDataReader(oDrd("Pmc"))
                .Cod = oDrd("ArcCod")
                .DeliveryGuid = oDrd("DeliveryGuid")
                .ContactGuid = oDrd("CliGuid")
                .Nom = oDrd("FullNom")
                If oMgz IsNot Nothing Then .Mgz = oMgz.Guid
            End With

            retval.Items.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Private Shared Function Mgz(oMgz As DTOGuidNom.Compact, oDrd As SqlDataReader, oMgzs As List(Of DTOGuidNom.Compact)) As DTOGuidNom.Compact
        Dim retval As DTOGuidNom.Compact = oMgz
        Dim keepSameMgz As Boolean = True
        If oMgz Is Nothing Then
            keepSameMgz = IsDBNull(oDrd("MgzGuid"))
        Else
            keepSameMgz = oMgz.Guid.Equals(oDrd("MgzGuid"))
        End If

        If Not keepSameMgz Then
            If IsDBNull(oDrd("MgzGuid")) Then
                retval = Nothing
            Else
                retval = oMgzs.FirstOrDefault(Function(x) x.Guid.Equals(oDrd("MgzGuid")))
                If retval Is Nothing Then
                    Dim mgzNom As String = IIf(IsDBNull(oDrd("MgzNom")), SQLHelper.GetStringFromDataReader(oDrd("MgzRaoSocial")), oDrd("MgzNom"))
                    retval = DTOGuidNom.Compact.Factory(oDrd("MgzGuid"), mgzNom)
                    oMgzs.Add(retval)
                End If
            End If
        End If
        Return retval
    End Function


    Shared Function All_Deprecated(oProduct As DTOProduct, Optional oMgz As DTOMgz = Nothing) As List(Of DTODeliveryItem)
        Dim retval As New List(Of DTODeliveryItem)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Arc.Guid AS ArcGuid, Arc.Qty, Arc.Eur, Arc.Dto, Arc.Dt2, Arc.Pmc, Arc.Cod AS ArcCod, Arc.PncGuid, Arc.PdcGuid, Arc.SpvGuid ")
        sb.AppendLine(", Alb.Guid AS AlbGuid, Alb.Alb, Alb.Fch, Alb.Cod AS AlbCod, Alb.CliGuid, CliGral.FullNom ")
        sb.AppendLine(", Arc.ArtGuid, VwSkuNom.SkuNomEsp AS ArtNom ")
        sb.AppendLine(", Arc.MgzGuid, Mgz.Nom AS MgzNom ")
        sb.AppendLine(", Alb.TransmGuid, Transm.Transm ")
        sb.AppendLine("FROM Arc ")
        sb.AppendLine("INNER JOIN Alb ON Arc.AlbGuid = Alb.Guid ")
        sb.AppendLine("INNER JOIN CliGral ON Alb.CliGuid = CliGral.Guid ")
        sb.AppendLine("INNER JOIN Art ON Arc.ArtGuid = Art.Guid ")
        sb.AppendLine("INNER JOIN VwSkuNom ON Art.Guid = VwSkuNom.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN Mgz ON arc.mgzGuid = Mgz.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Transm ON Alb.TransmGuid = Transm.Guid ")
        sb.AppendLine("WHERE (Art.Guid='" & oProduct.Guid.ToString & "' OR Art.Category='" & oProduct.Guid.ToString & "') ")
        If oMgz Is Nothing Then
            oMgz = New DTOMgz
        Else
            sb.AppendLine("AND Arc.MgzGuid = '" & oMgz.Guid.ToString & "' ")
        End If
        sb.AppendLine("ORDER BY Alb.Fch, Alb.Alb, Arc.Lin")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oDelivery As New DTODelivery
        Dim oContact As New DTOCustomer

        Dim oMgzs As New List(Of DTOMgz)
        Do While oDrd.Read
            If Not oContact.Guid.Equals(oDrd("CliGuid")) Then
                oContact = New DTOCustomer(oDrd("CliGuid"))
                oContact.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
            End If

            If IsDBNull(oDrd("MgzGuid")) Then
                oMgz = Nothing
            ElseIf oMgz Is Nothing Then
            ElseIf Not oMgz.Guid.Equals(oDrd("MgzGuid")) Then
                If oMgzs.Any(Function(x) x.Guid.Equals(oDrd("MgzGuid"))) Then
                    oMgz = oMgzs.FirstOrDefault(Function(x) x.Guid.Equals(oDrd("MgzGuid")))
                Else
                    oMgz = New DTOMgz(oDrd("MgzGuid"))
                    oMgz.abr = SQLHelper.GetStringFromDataReader(oDrd("MgzNom"))
                    oMgzs.Add(oMgz)
                End If
            End If

            Dim oTransm As DTOTransmisio = Nothing
            If Not IsDBNull(oDrd("TransmGuid")) Then
                oTransm = New DTOTransmisio(oDrd("TransmGuid"))
                oTransm.Id = oDrd("Transm")
            End If

            If Not oDelivery.Guid.Equals(oDrd("AlbGuid")) Then
                oDelivery = New DTODelivery(oDrd("AlbGuid"))
                With oDelivery
                    .Id = oDrd("Alb")
                    .Fch = oDrd("Fch")
                    .Customer = oContact
                    .Mgz = oMgz
                    .Cod = oDrd("AlbCod")
                    .Transmisio = oTransm
                End With
            End If

            Dim oSku As DTOProductSku = Nothing
            If TypeOf oProduct Is DTOProductCategory Then
                oSku = New DTOProductSku(oDrd("ArtGuid"))
                SQLHelper.LoadLangTextFromDataReader(oSku.NomLlarg, oDrd, "ArtNom", "ArtNom", "ArtNom", "ArtNom")
                oSku.Category = oProduct
            ElseIf TypeOf oProduct Is DTOProductSku Then
                oSku = oProduct
            End If


            Dim item As New DTODeliveryItem(oDrd("ArcGuid"))
            With item
                .Delivery = oDelivery
                .Mgz = oMgz
                .Qty = oDrd("Qty")
                If Not IsDBNull(oDrd("Eur")) Then
                    .Price = DTOAmt.Factory(CDec(oDrd("Eur")))
                End If
                .Dto = SQLHelper.GetDecimalFromDataReader(oDrd("Dto"))
                .Dt2 = SQLHelper.GetDecimalFromDataReader(oDrd("Dt2"))
                .Cod = oDrd("ArcCod")
                .Sku = oSku
                .Pmc = SQLHelper.GetDecimalFromDataReader(oDrd("Pmc"))

                If Not IsDBNull(oDrd("PdcGuid")) Then
                    Dim oPurchaseOrder As New DTOPurchaseOrder(oDrd("PdcGuid"))
                    If IsDBNull(oDrd("PncGuid")) Then
                        .PurchaseOrderItem = New DTOPurchaseOrderItem()
                    Else
                        .PurchaseOrderItem = New DTOPurchaseOrderItem(oDrd("PncGuid"))
                    End If
                    .PurchaseOrderItem.Sku = oSku
                    .PurchaseOrderItem.PurchaseOrder = oPurchaseOrder
                ElseIf Not IsDBNull(oDrd("SpvGuid")) Then
                    .Spv = New DTOSpv(oDrd("SpvGuid"))
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function All(oProveidor As DTOProveidor, oMgz As DTOMgz) As List(Of DTODeliveryItem)
        Dim retval As New List(Of DTODeliveryItem)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Arc.Guid AS ArcGuid, Arc.Qty, Arc.Eur, Arc.Dto, Arc.Dt2, Arc.Pmc, Arc.Cod AS ArcCod, Arc.PncGuid, Arc.PdcGuid, Arc.SpvGuid, Arc.ArtGuid ")
        sb.AppendLine(", Alb.Guid AS AlbGuid, Alb.Alb, Alb.Fch, Alb.Cod AS AlbCod, VwCcxOrMe.Ccx AS CliGuid, CliGral.FullNom ")
        sb.AppendLine(", VwSkuNom.* ")
        sb.AppendLine("FROM Arc ")
        sb.AppendLine("INNER JOIN Alb ON Arc.AlbGuid = Alb.Guid ")
        sb.AppendLine("INNER JOIN VwCcxOrMe ON Alb.CliGuid = VwCcxOrMe.Guid ")
        sb.AppendLine("INNER JOIN CliGral ON VwCcxOrMe.Ccx = CliGral.Guid ")
        sb.AppendLine("INNER JOIN VwSkuNom ON Arc.ArtGuid=VwSkuNom.SkuGuid ")
        sb.AppendLine("INNER JOIN Tpa ON VwProductNom.BrandGuid = Tpa.Guid ")
        sb.AppendLine("WHERE Tpa.Proveidor = '" & oProveidor.Guid.ToString & "' ")
        sb.AppendLine("AND Arc.MgzGuid = '" & oMgz.Guid.ToString & "' ")
        sb.AppendLine("AND Alb.Cod = 2 ")
        sb.AppendLine("ORDER BY Alb.Fch, Alb.Alb, Arc.Lin")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oDelivery As New DTODelivery
        Dim oContact As New DTOCustomer
        Do While oDrd.Read


            If Not oContact.Guid.Equals(oDrd("CliGuid")) Then
                oContact = New DTOCustomer(oDrd("CliGuid"))
                oContact.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
            End If

            If Not oDelivery.Guid.Equals(oDrd("AlbGuid")) Then
                oDelivery = New DTODelivery(oDrd("AlbGuid"))
                With oDelivery
                    .Id = oDrd("Alb")
                    .Fch = oDrd("Fch")
                    .Customer = oContact
                    .Cod = oDrd("AlbCod")
                End With
            End If

            Dim oSku As DTOProductSku = SQLHelper.GetProductFromDataReader(oDrd)

            Dim item As New DTODeliveryItem(oDrd("ArcGuid"))
            With item
                .Delivery = oDelivery
                .Qty = oDrd("Qty")
                .Price = DTOAmt.Factory(CDec(oDrd("Eur")))
                .Dto = SQLHelper.GetDecimalFromDataReader(oDrd("Dto"))
                .Dt2 = SQLHelper.GetDecimalFromDataReader(oDrd("Dt2"))
                .Cod = oDrd("ArcCod")
                .Pmc = SQLHelper.GetDecimalFromDataReader(oDrd("Pmc"))
                .Sku = oSku
                If Not IsDBNull(oDrd("PdcGuid")) Then
                    Dim oPurchaseOrder As New DTOPurchaseOrder(oDrd("PdcGuid"))
                    If IsDBNull(oDrd("PncGuid")) Then
                        .PurchaseOrderItem = New DTOPurchaseOrderItem()
                    Else
                        .PurchaseOrderItem = New DTOPurchaseOrderItem(oDrd("PncGuid"))
                    End If
                    .PurchaseOrderItem.Sku = oSku
                ElseIf Not IsDBNull(oDrd("SpvGuid")) Then
                    .Spv = New DTOSpv(oDrd("SpvGuid"))
                End If
            End With

            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(Optional oCcx As DTOCustomer = Nothing, Optional oMgz As DTOMgz = Nothing) As List(Of DTODeliveryItem)
        Dim retval As New List(Of DTODeliveryItem)
        Dim sb As New System.Text.StringBuilder
        Dim sbWhere As New Text.StringBuilder
        sb.AppendLine("SELECT Arc.Guid AS ArcGuid, Arc.Qty, Arc.Eur, Arc.Dto, Arc.Dt2, Arc.Pmc, Arc.Cod AS ArcCod, Arc.PncGuid, Arc.PdcGuid, Arc.SpvGuid, Arc.ArtGuid ")
        sb.AppendLine(", Alb.Guid AS AlbGuid, Alb.Alb, Alb.Fch, Alb.Cod AS AlbCod, Alb.CliGuid, CliGral.FullNom ")
        sb.AppendLine(", VwSkuNom.* ")
        sb.AppendLine("FROM Arc ")
        sb.AppendLine("INNER JOIN Alb ON Arc.AlbGuid = Alb.Guid ")
        sb.AppendLine("INNER JOIN CliGral ON Alb.CliGuid = CliGral.Guid ")
        sb.AppendLine("INNER JOIN VwCcxOrMe ON Alb.CliGuid = VwCcxOrMe.Guid ")
        sb.AppendLine("INNER JOIN VwSkuNom ON Arc.ArtGuid=VwSkuNom.SkuGuid ")
        sb.AppendLine("WHERE Alb.Emp = 1 ")
        sb.AppendLine("AND Alb.Cod = 2 ")

        If oCcx IsNot Nothing Then
            sb.AppendLine("AND VwCcxOrMe.Ccx = '" & oCcx.Guid.ToString & "' ")
        End If
        If oMgz IsNot Nothing Then
            sb.AppendLine("AND Arc.MgzGuid = '" & oMgz.Guid.ToString & "' ")
        End If

        sb.Append(sbWhere.ToString())
        sb.AppendLine("ORDER BY Alb.Fch, Alb.Alb, Arc.Lin")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oDelivery As New DTODelivery
        Dim oContact As New DTOCustomer
        Do While oDrd.Read
            If Not oContact.Guid.Equals(oDrd("CliGuid")) Then
                oContact = New DTOCustomer(oDrd("CliGuid"))
                oContact.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
            End If

            If Not oDelivery.Guid.Equals(oDrd("AlbGuid")) Then
                oDelivery = New DTODelivery(oDrd("AlbGuid"))
                With oDelivery
                    .Id = oDrd("Alb")
                    .Fch = oDrd("Fch")
                    .Customer = oContact
                    .Cod = oDrd("AlbCod")
                End With
            End If

            Dim oSku As DTOProductSku = SQLHelper.GetProductFromDataReader(oDrd)

            Dim item As New DTODeliveryItem(oDrd("ArcGuid"))
            With item
                .Delivery = oDelivery
                .Qty = oDrd("Qty")
                .Price = DTOAmt.Factory(CDec(oDrd("Eur")))
                .Dto = SQLHelper.GetDecimalFromDataReader(oDrd("Dto"))
                .Dt2 = SQLHelper.GetDecimalFromDataReader(oDrd("Dt2"))
                .Cod = oDrd("ArcCod")
                .Pmc = SQLHelper.GetDecimalFromDataReader(oDrd("Pmc"))
                .Sku = oSku
                If Not IsDBNull(oDrd("PdcGuid")) Then
                    Dim oPurchaseOrder As New DTOPurchaseOrder(oDrd("PdcGuid"))
                    If IsDBNull(oDrd("PncGuid")) Then
                        .PurchaseOrderItem = New DTOPurchaseOrderItem()
                    Else
                        .PurchaseOrderItem = New DTOPurchaseOrderItem(oDrd("PncGuid"))
                    End If
                    .PurchaseOrderItem.Sku = oSku
                ElseIf Not IsDBNull(oDrd("SpvGuid")) Then
                    .Spv = New DTOSpv(oDrd("SpvGuid"))
                End If
            End With

            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oOrder As DTOPurchaseOrder) As List(Of DTODeliveryItem)
        Dim retval As New List(Of DTODeliveryItem)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Arc.*, Alb.Alb AS AlbId, Alb.Fch, Alb.FraGuid, Fra.Fra ")
        sb.AppendLine(", VwSkuNom.* ")
        sb.AppendLine("FROM Arc ")
        sb.AppendLine("INNER JOIN Alb ON Arc.AlbGuid= Alb.Guid AND Arc.PdcGuid = '" & oOrder.Guid.ToString & "' ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuNom ON Arc.ArtGuid=VwSkuNom.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN Fra ON Alb.FraGuid = Fra.Guid ")
        sb.AppendLine("ORDER BY Alb.Fch, Arc.AlbGuid, Arc.Lin")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oDelivery As New DTODelivery
        Do While oDrd.Read


            If Not oDelivery.Guid.Equals(oDrd("AlbGuid")) Then
                Dim oInvoice As DTOInvoice = Nothing
                If Not IsDBNull(oDrd("FraGuid")) Then
                    oInvoice = New DTOInvoice(oDrd("FraGuid"))
                    oInvoice.Num = oDrd("Fra")
                End If

                oDelivery = New DTODelivery(oDrd("AlbGuid"))
                With oDelivery
                    .Id = oDrd("AlbId")
                    .Fch = oDrd("Fch")
                    .Invoice = oInvoice
                End With
            End If

            Dim oPurchaseOrderItem As New DTOPurchaseOrderItem
            With oPurchaseOrderItem
                .Sku = SQLHelper.GetProductFromDataReader(oDrd)
            End With

            Dim item As New DTODeliveryItem(oDrd("Guid"))
            With item
                .Delivery = oDelivery
                .Qty = oDrd("Qty")
                .Price = SQLHelper.GetAmtFromDataReader(oDrd("Eur"))
                .Dto = SQLHelper.GetDecimalFromDataReader(oDrd("Dto"))
                .Dt2 = SQLHelper.GetDecimalFromDataReader(oDrd("Dt2"))
                .PurchaseOrderItem = oPurchaseOrderItem
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Garantias(oEmp As DTOEmp, FchFrom As Date, FchTo As Date) As List(Of DTODeliveryItem)
        Dim retval As New List(Of DTODeliveryItem)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Arc.Guid AS ArcGuid, Arc.AlbGuid, Arc.PdcGuid, Arc.PncGuid, Arc.ArtGuid, Arc.qty ")
        sb.AppendLine(", Alb.Alb, Alb.fch, Alb.CliGuid ")
        sb.AppendLine(", CliGral.FullNom, Pdc.Pdd, VwSkuNom.SkuNomLlargEsp, VwSkuCost.Price, VwSkuCost.Discount_OnInvoice, VwSkuCost.Discount_OffInvoice ")
        sb.AppendLine("FROM Pdc ")
        sb.AppendLine("INNER JOIN Arc ON Pdc.Guid = Arc.PdcGuid ")
        sb.AppendLine("INNER JOIN Alb ON Arc.AlbGuid = Alb.Guid ")
        sb.AppendLine("INNER JOIN VwSkuNom ON Arc.ArtGuid = VwSkuNom.SkuGuid ")
        sb.AppendLine("INNER JOIN CliGral ON Pdc.CliGuid = CliGral.Guid ")
        sb.AppendLine("INNER JOIN VwSkuCost ON Arc.ArtGuid = VwSkuCost.SkuGuid ")
        sb.AppendLine("WHERE Pdc.Emp = " & oEmp.Id & " ")
        sb.AppendLine("AND Pdc.Pdd LIKE '%garantía%' ")
        sb.AppendLine("AND Alb.Fch BETWEEN '" & Format(FchFrom, "yyyyMMdd") & "' AND '" & Format(FchTo, "yyyyMMdd") & "' ")
        sb.AppendLine("AND VwSkuCost.Price > 0 ")
        sb.AppendLine("AND Pdc.Cod = 2 ")
        sb.AppendLine("ORDER BY Alb.Yea DESC, Alb.Alb DESC, Arc.lin ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oCustomer As New DTOCustomer(oDrd("CliGuid"))
            With oCustomer
                .FullNom = oDrd("FullNom")
            End With
            Dim oOrder As New DTOPurchaseOrder(oDrd("PdcGuid"))
            With oOrder
                .Customer = oCustomer
                .Concept = oDrd("Pdd")
            End With
            Dim oSku As New DTOProductSku(oDrd("ArtGuid"))
            With oSku
                .NomLlarg.Esp = oDrd("SkuNomLlargEsp")
                .Cost = DTOAmt.Factory(oDrd("Price") * (100 - oDrd("Discount_OnInvoice") - oDrd("Discount_OffInvoice")) / 100)
            End With
            Dim oPurchaseOrderItem As New DTOPurchaseOrderItem(oDrd("PncGuid"))
            With oPurchaseOrderItem
                .PurchaseOrder = oOrder
                .Sku = oSku
            End With
            Dim oDelivery As New DTODelivery(oDrd("AlbGuid"))
            With oDelivery
                .Id = oDrd("Alb")
                .Fch = oDrd("Fch")
                .Customer = oCustomer
            End With
            Dim item As New DTODeliveryItem(oDrd("ArcGuid"))
            With item
                .Delivery = oDelivery
                .PurchaseOrderItem = oPurchaseOrderItem
                .Sku = oSku
                .Qty = oDrd("Qty")
            End With

            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
