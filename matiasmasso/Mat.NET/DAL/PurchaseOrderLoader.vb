Imports System.Globalization
Imports Newtonsoft.Json.Linq

Public Class PurchaseOrderLoader

    Shared Function Find(oGuid As Guid, Optional oMgz As DTOMgz = Nothing) As DTOPurchaseOrder
        Dim retval As DTOPurchaseOrder = Nothing
        Dim oPurchaseOrder As New DTOPurchaseOrder(oGuid)
        If Load(oPurchaseOrder, oMgz) Then
            retval = oPurchaseOrder
        End If
        Return retval
    End Function

    Shared Function FromNum(oEmp As DTOEmp, iYear As Integer, iNum As Integer) As DTOPurchaseOrder
        Dim retval As DTOPurchaseOrder = Nothing
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Pdc.Guid ")
        sb.AppendLine("FROM Pdc ")
        sb.AppendLine("WHERE Pdc.Emp=" & oEmp.Id & " ")
        sb.AppendLine("AND Year(Pdc.Fch)=" & iYear & " ")
        sb.AppendLine("AND Pdc.Pdc=" & iNum & " ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOPurchaseOrder(oDrd("Guid"))
            retval.Emp = oEmp
            retval.Num = iNum
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function Load(ByRef oPurchaseOrder As DTOPurchaseOrder, Optional oMgz As DTOMgz = Nothing) As Boolean
        If Not oPurchaseOrder.IsLoaded And Not oPurchaseOrder.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Pdc.*, CliGral.FullNom, FacturarA.FullNom AS FacturarANom ")
            sb.AppendLine(", Pnc.Guid AS PncGuid, Pnc.Qty, Pnc.ArtGuid, Pnc.Eur as PncEur, Pnc.Pn2, Pdc.Pot, Pnc.Pts AS PncPts, Pnc.Dto AS PncDto, Pnc.Carrec, Pnc.Lin, Pnc.RepGuid, Pnc.Com, Pnc.RepCustom, Pnc.Bundle, CliRep.Abr as RepAbr ")
            sb.AppendLine(", Pnc.CustomLin, Pnc.ErrCod, Pnc.ErrDsc ")
            sb.AppendLine(", VwSkuNom.* ")
            sb.AppendLine(", VwAddress.* ")
            sb.AppendLine(", EmailCreated.Adr AS UsrCreatedEmailAddress, EmailCreated.Nickname AS UsrCreatedNickname ")
            sb.AppendLine(", EmailLastEdited.Adr AS UsrLastEditedEmailAddress, EmailLastEdited.Nickname AS UsrLastEditedNickname ")
            sb.AppendLine(", CliGral.Rol, CliGral.RaoSocial, CliGral.NomCom, CliGral.LangId, CliGral.GLN, CliClient.IVA, CliClient.Req, CliClient.NoRep ")
            sb.AppendLine(", CliClient.CcxGuid, Ccx.IVA AS CcxIVA, Ccx.Req AS CcxReq ")
            sb.AppendLine(", CliGral.ContactClass, ContactClass.DistributionChannel ")
            sb.AppendLine(", VwSkuPncs.Clients, VwSkuPncs.ClientsAlPot, VwSkuPncs.ClientsEnProgramacio, VwSkuPncs.Pn1, VwSkuPncs.ClientsBlockStock ")
            sb.AppendLine(", PromoTitle.Esp AS PromoTitleEsp, PromoTitle.Cat AS PromoTitleCat, PromoTitle.Eng AS PromoTitleEng, PromoTitle.Por AS PromoTitlePor ")
            If oMgz IsNot Nothing Then
                sb.AppendLine(", VwSkuStocks.Stock ")
            End If

            sb.AppendLine("FROM Pdc ")
            sb.AppendLine("INNER JOIN CliGral ON Pdc.CliGuid=CliGral.Guid ")
            sb.AppendLine("INNER JOIN VwAddress ON Pdc.CliGuid=VwAddress.SrcGuid ")
            sb.AppendLine("LEFT OUTER JOIN ContactClass ON CliGral.ContactClass=ContactClass.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CliClient ON Pdc.CliGuid=CliClient.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CliClient Ccx ON Ccx.Guid = (CASE WHEN FacturarA IS NULL THEN CliClient.CcxGuid ELSE Pdc.FacturarA END) ")
            sb.AppendLine("LEFT OUTER JOIN CliGral FacturarA ON Ccx.Guid=FacturarA.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Pnc ON Pnc.PdcGuid=Pdc.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CliRep ON Pnc.RepGuid=CliRep.Guid ")

            sb.AppendLine("LEFT OUTER JOIN VwSkuNom ON Pnc.ArtGuid = VwSkuNom.SkuGuid ")
            sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundlePncs VwSkuPncs ON VwSkuNom.SkuGuid = VwSkuPncs.SkuGuid ")
            If oMgz IsNot Nothing Then
                sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundleStocks VwSkuStocks ON VwSkuNom.SkuGuid = VwSkuStocks.SkuGuid And VwSkuStocks.MgzGuid = '" & oMgz.Guid.ToString & "' ")
            End If

            sb.AppendLine("LEFT OUTER JOIN Incentiu ON Pdc.Promo = Incentiu.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText AS PromoTitle ON Incentiu.Guid = PromoTitle.Guid AND PromoTitle.Src = " & DTOLangText.Srcs.IncentiuTitle & " ")

            sb.AppendLine("LEFT OUTER JOIN Email AS EmailCreated ON Pdc.UsrCreatedGuid=EmailCreated.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Email AS EmailLastedited ON Pdc.UsrLasteditedGuid=EmailLastedited.Guid ")

            sb.AppendLine("WHERE Pdc.Guid='" & oPurchaseOrder.Guid.ToString & "' ")
            sb.AppendLine("ORDER BY Pnc.Lin ")


            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                With oPurchaseOrder
                    If Not .IsLoaded Then
                        If .Emp Is Nothing Then .Emp = New DTOEmp(CInt(oDrd("Emp")))
                        .Fch = oDrd("Fch")
                        .Num = oDrd("Pdc")
                        .Cod = oDrd("Cod")
                        .Cur = DTOCur.Factory(oDrd("Cur"))
                        Select Case .Cod
                            Case DTOPurchaseOrder.Codis.proveidor
                                .Proveidor = New DTOProveidor(oDrd("CliGuid"))
                            Case Else
                                .Customer = New DTOCustomer(oDrd("CliGuid"))
                                With .Customer
                                    .Nom = SQLHelper.GetStringFromDataReader(oDrd("RaoSocial"))
                                    .NomComercial = SQLHelper.GetStringFromDataReader(oDrd("NomCom"))
                                    .NoRep = SQLHelper.GetBooleanFromDatareader(oDrd("NoRep"))
                                    .Iva = SQLHelper.GetBooleanFromDatareader(oDrd("IVA"))
                                    .Req = SQLHelper.GetBooleanFromDatareader(oDrd("Req"))
                                    .Address = SQLHelper.GetAddressFromDataReader(oDrd)
                                    If Not IsDBNull(oDrd("CcxGuid")) Then
                                        .Ccx = New DTOCustomer(oDrd("CcxGuid"))
                                        With .Ccx
                                            .Iva = SQLHelper.GetBooleanFromDatareader(oDrd("CcxIVA"))
                                            .Req = SQLHelper.GetBooleanFromDatareader(oDrd("CcxReq"))
                                            .Rol = New DTORol(oDrd("Rol"))
                                        End With
                                    End If
                                End With
                                If Not IsDBNull(oDrd("FacturarA")) Then
                                    .FacturarA = New DTOCustomer(oDrd("FacturarA"))
                                    .FacturarA.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FacturarANom"))
                                End If
                        End Select
                        With .Contact
                            .FullNom = oDrd("FullNom")
                            .Lang = DTOLang.Factory(oDrd("LangId"))
                            .GLN = SQLHelper.GetEANFromDataReader(oDrd("Gln"))
                            .Rol = New DTORol(oDrd("Rol"))
                            If Not IsDBNull(oDrd("ContactClass")) Then
                                .ContactClass = New DTOContactClass(oDrd("ContactClass"))
                                If Not IsDBNull(oDrd("DistributionChannel")) Then
                                    .ContactClass.DistributionChannel = New DTODistributionChannel(oDrd("DistributionChannel"))
                                End If
                            End If
                        End With
                        .Concept = oDrd("Pdd")
                        If Not IsDBNull(oDrd("Fpg")) Then
                            .PaymentTerms = PaymentTermsLoader.Factory(oDrd("Fpg"))
                        End If
                        .NADMS = SQLHelper.GetStringFromDataReader(oDrd("Nadms"))
                        .Source = oDrd("Src")
                        .TotJunt = oDrd("TotJunt")
                        .FchDeliveryMin = Defaults.FchOrNothing(oDrd("FchMin"))
                        .FchDeliveryMax = Defaults.FchOrNothing(oDrd("FchMax"))
                        .Pot = oDrd("Pot")
                        .BlockStock = oDrd("BlockStock")
                        .Hide = oDrd("Hide")
                        If Not IsDBNull(oDrd("Platform")) Then
                            .Platform = New DTOCustomerPlatform(oDrd("Platform"))
                        End If
                        If Not IsDBNull(oDrd("Promo")) Then
                            .Incentiu = New DTOIncentiu(oDrd("Promo"))
                            SQLHelper.LoadLangTextFromDataReader(.Incentiu.Title, oDrd, "PromoTitleEsp", "PromoTitleCat", "PromoTitleEng", "PromoTitlePor")
                        End If
                        .Obs = Defaults.StringOrEmpty(oDrd("Obs"))
                        .DocFile = SQLHelper.GetDocFileFromDataReaderHash(oDrd("Hash"))
                        .EtiquetesTransport = SQLHelper.GetDocFileFromDataReaderHash(oDrd("EtiquetesTransport"))
                        .Incoterm = SQLHelper.GetIncotermFromDataReader(oDrd("Incoterm"))

                        .Items = New List(Of DTOPurchaseOrderItem)
                        .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd, UsrCreatedField:="UsrCreatedGuid", UsrLastEditedField:="UsrLastEditedGuid")
                        .IsLoaded = True
                    End If
                End With

                If Not IsDBNull(oDrd("ArtGuid")) Then
                    Dim oSku As DTOProductSku = SQLHelper.GetProductFromDataReader(oDrd)
                    With oSku
                        .IsBundle = SQLHelper.GetBooleanFromDatareader(oDrd("isBundle"))
                        If oMgz IsNot Nothing Then
                            .Stock = SQLHelper.GetIntegerFromDataReader(oDrd("Stock"))
                            If oPurchaseOrder.BlockStock Then
                                .Stock += oDrd("Pn2")
                            End If
                        End If
                        .Clients = SQLHelper.GetIntegerFromDataReader(oDrd("Clients"))
                        .ClientsAlPot = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot"))
                        .ClientsEnProgramacio = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
                        .ClientsBlockStock = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                        .Proveidors = SQLHelper.GetIntegerFromDataReader(oDrd("Pn1"))

                        If oPurchaseOrder.BlockStock Then
                            .Clients += oDrd("Pn2")
                        End If
                    End With

                    Dim oItem As New DTOPurchaseOrderItem(oDrd("PncGuid"))
                    With oItem
                        .PurchaseOrder = oPurchaseOrder
                        .Qty = oDrd("Qty")
                        .Pending = oDrd("Pn2")
                        .Sku = oSku
                        .Price = SQLHelper.GetAmtFromDataReader2(oDrd, "PncEur", "Cur", "PncPts")
                        .Dto = oDrd("PncDto")
                        .ChargeCod = IIf(oDrd("Carrec") = 0, DTOPurchaseOrderItem.ChargeCods.FOC, DTOPurchaseOrderItem.ChargeCods.chargeable)
                        .Lin = oDrd("Lin")
                        .CustomLin = SQLHelper.GetIntegerFromDataReader(oDrd("CustomLin"))
                        .ErrCod = CType(oDrd("ErrCod"), DTOPurchaseOrderItem.ErrCods)
                        .ErrDsc = SQLHelper.GetStringFromDataReader(oDrd("ErrDsc"))

                        If Not IsDBNull(oDrd("RepGuid")) Then
                            .RepCom = New DTORepCom
                            .RepCom.Rep = New DTORep(oDrd("RepGuid"))
                            .RepCom.Rep.NickName = SQLHelper.GetStringFromDataReader(oDrd("RepAbr"))
                            .RepCom.Com = SQLHelper.GetDecimalFromDataReader(oDrd("Com"))
                            .RepCom.repCustom = SQLHelper.GetBooleanFromDatareader(oDrd("RepCustom"))
                        End If
                    End With

                    If isBundleChild(oDrd) Then
                        Dim oBundleParent = oPurchaseOrder.Items.First(Function(x) x.Guid.Equals(oDrd("Bundle")))
                        oBundleParent.Bundle.Add(oItem)
                    Else
                        oPurchaseOrder.Items.Add(oItem)
                    End If

                End If
            Loop

            oDrd.Close()
        End If

        Dim retval As Boolean = oPurchaseOrder.IsLoaded
        Return retval
    End Function


    Shared Function FindWithDeliveries(oGuid As Guid) As DTOPurchaseOrder
        Dim retval As DTOPurchaseOrder = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Pdc.Pdc, Pdc.Fch AS PdcFch, Pdc.Pdd, Pdc.CliGuid, CliGral.FullNom, Pnc.Guid AS PncGuid, Pnc.Lin ")
        sb.AppendLine(", Pnc.Guid AS PncGuid, Pnc.Lin AS PncLin, Pnc.Qty AS PncQty, Pnc.ArtGuid, VwSkuNom.* ")
        sb.AppendLine(", Arc.Guid AS ArcGuid, Arc.Qty AS ArcQty, Arc.Eur AS ArcEur, Arc.Dto AS ArcDto, Arc.RepGuid, Arc.Com, Arc.RepComLiquidable ")
        sb.AppendLine(", Arc.AlbGuid, Alb.Alb, Alb.Fch AS AlbFch ")
        sb.AppendLine(", Fra.Guid AS FraGuid, Fra.Fra, Fra.Fch AS FraFch ")
        sb.AppendLine(", RepLiq.Guid AS RepLiqGuid, RepLiq.Id AS RepLiqId, RepLiq.Fch AS RepLiqFch ")
        sb.AppendLine(", Cca.Guid as CcaGuid, Cca.Hash AS RepLiqHash ")
        sb.AppendLine("FROM Pdc INNER JOIN Pnc ON Pdc.Guid = Pnc.PdcGuid ")
        sb.AppendLine("INNER JOIN CliGral ON Pdc.CliGuid = CliGral.Guid ")
        sb.AppendLine("INNER JOIN Arc ON Pnc.Guid = Arc.PncGuid ")
        sb.AppendLine("INNER JOIN Alb ON Arc.AlbGuid = Alb.Guid ")
        sb.AppendLine("INNER JOIN VwSkuNom ON Pnc.ArtGuid = VwSkuNom.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN Fra ON Alb.FraGuid = Fra.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Rps ON Arc.RepComLiquidable = Rps.Guid ")
        sb.AppendLine("LEFT OUTER JOIN RepLiq ON Rps.RepLiqGuid= RepLiq.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Cca ON RepLiq.CcaGuid= Cca.Guid ")
        sb.AppendLine("WHERE Pdc.Guid = '" & oGuid.ToString & "' ")
        sb.AppendLine("ORDER BY Pnc.Lin, Alb.Fch, Arc.Lin ")
        Dim SQL As String = sb.ToString
        Dim oPnc As New DTOPurchaseOrderItem
        Dim oDelivery As New DTODelivery
        Dim oInvoice As New DTOInvoice
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If retval Is Nothing Then
                retval = New DTOPurchaseOrder(oGuid)
                With retval
                    .Num = oDrd("Pdc")
                    .Fch = oDrd("PdcFch")
                    .Concept = oDrd("Pdd")
                    .Customer = New DTOCustomer(oDrd("CliGuid"))
                    .Customer.FullNom = oDrd("FullNom")
                    .Items = New List(Of DTOPurchaseOrderItem)
                End With
            End If

            If Not oPnc.Guid.Equals(oDrd("PncGuid")) Then
                oPnc = New DTOPurchaseOrderItem(oDrd("PncGuid"))
                With oPnc
                    .Sku = SQLHelper.GetProductFromDataReader(oDrd)
                    .Lin = oDrd("PncLin")
                    .Qty = oDrd("PncQty")
                    .Deliveries = New List(Of DTODeliveryItem)
                End With
                retval.Items.Add(oPnc)
            End If

            If IsDBNull(oDrd("FraGuid")) Then
                oInvoice = Nothing
            ElseIf oInvoice Is Nothing OrElse Not oInvoice.Guid.Equals(oDrd("FraGuid")) Then
                oInvoice = New DTOInvoice(oDrd("FraGuid"))
                With oInvoice
                    .Num = oDrd("Fra")
                    .Fch = oDrd("FraFch")
                End With
            End If

            If Not oDelivery.Guid.Equals(oDrd("AlbGuid")) Then
                oDelivery = New DTODelivery(oDrd("AlbGuid"))
                With oDelivery
                    .Id = oDrd("Alb")
                    .Fch = oDrd("AlbFch")
                    .Invoice = oInvoice
                End With
            End If

            Dim item As New DTODeliveryItem(oDrd("ArcGuid"))
            With item
                .Qty = oDrd("ArcQty")
                .Price = DTOAmt.Factory(oDrd("ArcEur"))
                .Dto = oDrd("ArcDto")
                If Not IsDBNull(oDrd("Com")) Then
                    .RepCom = New DTORepCom
                    .RepCom.Rep = New DTORep(SQLHelper.GetGuidFromDataReader(oDrd("RepGuid")))
                    .RepCom.Com = SQLHelper.GetDecimalFromDataReader(oDrd("Com"))
                    If Not IsDBNull(oDrd("RepLiqGuid")) Then
                        .RepComLiquidable = New DTORepComLiquidable(oDrd("RepComLiquidable"))
                        .RepComLiquidable.RepLiq = New DTORepLiq(oDrd("RepLiqGuid"))
                        With .RepComLiquidable.RepLiq
                            .Fch = oDrd("RepLiqFch")
                            .Id = oDrd("RepLiqId")
                            If Not IsDBNull(oDrd("CcaGuid")) Then
                                .Cca = New DTOCca(oDrd("CcaGuid"))
                                .Cca.DocFile = New DTODocFile(oDrd("RepLiqHash"))
                            End If
                        End With
                    End If

                End If
                .Delivery = oDelivery
            End With
            oPnc.Deliveries.Add(item)

        Loop

        Return retval
    End Function

    Shared Function isBundleChild(odrd) As Boolean
        Dim retval As Boolean
        If Not IsDBNull(odrd("Bundle")) Then
            Dim oBundleGuid As Guid = odrd("Bundle")
            Dim itemGuid As Guid = odrd("PncGuid")
            retval = Not oBundleGuid.Equals(itemGuid)
        End If
        Return retval
    End Function


    Shared Function FindDuplicate(oCustomer As DTOCustomer, DtFch As Date, sConcepte As String) As DTOPurchaseOrder
        Dim retval As DTOPurchaseOrder = Nothing
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT TOP 1 Pdc.Guid, Pdc.Pdc, Pdc.FchCreated ")
        sb.AppendLine("FROM Pdc ")
        sb.AppendLine("WHERE CliGuid='" & oCustomer.Guid.ToString & "' ")
        sb.AppendLine("AND Pdc.Fch='" & Format(DtFch, "yyyyMMdd") & "' ")
        sb.AppendLine("AND Pdc.Pdd='" & sConcepte & "' ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOPurchaseOrder(oDrd("Guid"))
            With retval
                .Customer = oCustomer
                .Fch = DtFch
                .Concept = sConcepte
                .Num = oDrd("Pdc")
                .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd, UsrCreatedField:="UsrCreatedGuid", UsrLastEditedField:="UsrLastEditedGuid")
            End With
        End If
        Return retval
    End Function

    Shared Function Update(oPurchaseOrder As DTOPurchaseOrder, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oPurchaseOrder, oTrans)
            oTrans.Commit()
            retval = True
        Catch ex2 As SqlException
            'Stop

            oTrans.Rollback()
            exs.Add(ex2)
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function

    Shared Sub Update(oPurchaseOrder As DTOPurchaseOrder, ByRef oTrans As SqlTransaction)
        If oPurchaseOrder IsNot Nothing Then
            DocFileLoader.Update(oPurchaseOrder.DocFile, oTrans)
            DocFileLoader.Update(oPurchaseOrder.EtiquetesTransport, oTrans)
            UpdateHeader(oPurchaseOrder, oTrans)
            If Not oPurchaseOrder.IsNew Then DeleteItems(oPurchaseOrder, oTrans)
            UpdateItems(oPurchaseOrder, oTrans)
        End If
    End Sub

    Shared Function LastId(oPurchaseOrder As DTOPurchaseOrder, ByRef oTrans As SqlTransaction) As Integer
        Dim retval As Integer

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT TOP 1 Pdc AS LastId ")
        sb.AppendLine("FROM Pdc ")
        sb.AppendLine("WHERE Pdc.Emp=" & oPurchaseOrder.Emp.Id & " ")
        sb.AppendLine("AND Pdc.Yea=" & oPurchaseOrder.Fch.Year & " ")
        sb.AppendLine("ORDER BY Pdc DESC")

        Dim SQL As String = sb.ToString
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

    Shared Sub UpdateHeader(oPurchaseOrder As DTOPurchaseOrder, ByRef oTrans As SqlTransaction)

        Dim oAmt = DTOAmt.Factory(oPurchaseOrder.Cur)
        For Each item As DTOPurchaseOrderItem In oPurchaseOrder.Items
            If item.ChargeCod = DTOPurchaseOrderItem.ChargeCods.chargeable And item.ErrCod = DTOPurchaseOrderItem.ErrCods.Success Then
                'If Not item.Equals(item.PackParent) Then
                oAmt.Add(item.Amount)
                'End If
            End If
        Next

        Dim SQL As String = "SELECT * FROM Pdc WHERE Guid='" & oPurchaseOrder.Guid.ToString & "'"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oPurchaseOrder.Guid
            oRow("Emp") = oPurchaseOrder.Emp.Id
        Else
            oRow = oTb.Rows(0)
        End If

        With oPurchaseOrder
            If .Num = 0 Then .Num = LastId(oPurchaseOrder, oTrans) + 1
            'oRow("Emp") = .Emp.Id
            oRow("Yea") = .Fch.Year
            oRow("Pdc") = .Num
            oRow("Fch") = .Fch.Date
            oRow("CliGuid") = .Contact.Guid
            oRow("Pdd") = .Concept
            oRow("Cod") = .Cod
            oRow("Src") = .Source
            If oAmt Is Nothing Then
                If .Cur IsNot Nothing Then
                    oRow("Cur") = .Cur.Tag
                End If
            Else
                oRow("PdcPts") = oAmt.Val
                oRow("Cur") = oAmt.Cur.Tag
                oRow("Eur") = oAmt.Eur
            End If
            oRow("Promo") = SQLHelper.NullableBaseGuid(.Incentiu)
            oRow("Obs") = Defaults.NullOrValue(.Obs)
            oRow("TotJunt") = .TotJunt
            oRow("Pot") = .Pot
            oRow("BlockStock") = .BlockStock
            oRow("Hide") = .Hide
            oRow("FchMin") = Defaults.NullOrValue(.FchDeliveryMin)
            oRow("FchMax") = Defaults.NullOrValue(.FchDeliveryMax)
            oRow("Nadms") = SQLHelper.NullableString(.NADMS)
            oRow("Platform") = SQLHelper.NullableBaseGuid(.Platform)
            oRow("FacturarA") = SQLHelper.NullableBaseGuid(.FacturarA)
            oRow("CustomerDocURL") = Defaults.NullOrValue(.CustomerDocUrl)
            oRow("Hash") = SQLHelper.NullableDocFile(.DocFile)
            oRow("EtiquetesTransport") = SQLHelper.NullableDocFile(.EtiquetesTransport)
            oRow("Incoterm") = SQLHelper.NullableIncoterm(.Incoterm)

            If .PaymentTerms IsNot Nothing Then
                oRow("Fpg") = DTOPaymentTerms.XMLEncoded(.PaymentTerms)
            End If

            SQLHelper.SetUsrLog(.UsrLog, oRow, "UsrCreatedGuid", "UsrLastEditedGuid")

        End With

        oDA.Update(oDs)
    End Sub


    Shared Sub UpdateItems(oPurchaseOrder As DTOPurchaseOrder, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM Pnc WHERE PdcGuid='" & oPurchaseOrder.Guid.ToString & "'"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        For Each item As DTOPurchaseOrderItem In oPurchaseOrder.Items
            Dim oRow = UpdateItem(item, oPurchaseOrder, oTb)

            If item.isBundleParent Then
                oRow("Bundle") = item.Guid
                For Each oChildItem As DTOPurchaseOrderItem In item.Bundle
                    oRow = UpdateItem(oChildItem, oPurchaseOrder, oTb)
                    oRow("Bundle") = item.Guid
                Next
            End If


        Next
        oDA.Update(oDs)

    End Sub

    Shared Function UpdateItem(item As DTOPurchaseOrderItem, oPurchaseOrder As DTOPurchaseOrder, oTb As DataTable) As DataRow
        Dim oRow As DataRow = oTb.NewRow
        oTb.Rows.Add(oRow)

        With item
            oRow("Guid") = .Guid
            oRow("PdcGuid") = oPurchaseOrder.Guid
            oRow("Qty") = .Qty
            oRow("Pn2") = .Pending
            oRow("ArtGuid") = .Sku.Guid
            oRow("Lin") = oTb.Rows.Count
            oRow("CustomLin") = SQLHelper.NullableInteger(.CustomLin)
            oRow("Carrec") = .ChargeCod = DTOPurchaseOrderItem.ChargeCods.chargeable
            If .Price Is Nothing Or .ChargeCod = DTOPurchaseOrderItem.ChargeCods.FOC Then
                oRow("Pts") = 0
                If .PurchaseOrder.Cur Is Nothing Then
                    oRow("Cur") = "EUR"
                Else
                    oRow("Cur") = oPurchaseOrder.Cur.Tag
                End If
                oRow("Eur") = 0
            Else
                oRow("Pts") = Math.Round(.Price.Val, 2)
                oRow("Cur") = .Price.Cur.Tag
                oRow("Eur") = Math.Round(.Price.Eur, 2)
            End If
            oRow("Dto") = .Dto
            If .RepCom IsNot Nothing Then
                oRow("RepGuid") = .RepCom.Rep.Guid
                oRow("Com") = .RepCom.Com
                oRow("RepCustom") = .RepCom.repCustom
            End If

            oRow("ErrCod") = .ErrCod
            oRow("ErrDsc") = SQLHelper.NullableString(.ErrDsc)

            'If isBundleChild() Then

            'End If
        End With
        Return oRow
    End Function


    Shared Function Delete(oPurchaseOrder As DTOPurchaseOrder, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        If oPurchaseOrder Is Nothing Then
            retval = True
        Else
            Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
            Dim oTrans As SqlTransaction = oConn.BeginTransaction
            Try
                Delete(oPurchaseOrder, oTrans)
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

    Shared Sub Delete(oPurchaseOrder As DTOPurchaseOrder, ByRef oTrans As SqlTransaction)
        ClearEdiResult(oPurchaseOrder, oTrans)
        DeleteItems(oPurchaseOrder, oTrans)
        DeleteHeader(oPurchaseOrder, oTrans)
        DocFileLoader.Delete(oPurchaseOrder.DocFile, oTrans)
        DocFileLoader.Delete(oPurchaseOrder.EtiquetesTransport, oTrans)
    End Sub

    Shared Sub ClearEdiResult(oPurchaseOrder As DTOPurchaseOrder, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "UPDATE EdiversaOrderHeader SET Result=NULL WHERE Result = '" & oPurchaseOrder.Guid.ToString & "'"
        Dim i = SQLHelper.ExecuteNonQuery(SQL, oTrans)
        SQL = "UPDATE Edi SET Result=0, ResultGuid = NULL WHERE ResultGuid = '" & oPurchaseOrder.Guid.ToString & "'"
        Dim j = SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteItems(oPurchaseOrder As DTOPurchaseOrder, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Pnc WHERE PdcGuid='" & oPurchaseOrder.Guid.ToString & "'"
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteHeader(oPurchaseOrder As DTOPurchaseOrder, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Pdc WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oPurchaseOrder.Guid.ToString())
    End Sub

    Shared Function ResetPendingQty(value As DTOPurchaseOrder, exs As List(Of Exception)) As Boolean
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("UPDATE Pnc ")
        sb.AppendLine("SET Pnc.Pn2 = Pnc.Qty - (CASE WHEN X.Sortides IS NULL THEN 0 ELSE X.Sortides END) ")
        sb.AppendLine("FROM Pnc ")
        sb.AppendLine("LEFT OUTER JOIN (SELECT Arc.PncGuid, SUM(Qty) AS Sortides FROM Arc GROUP BY Arc.PncGuid) X ON Pnc.Guid = X.PncGuid ")
        sb.AppendLine("WHERE Pnc.PdcGuid='" & value.Guid.ToString & "'")

        Dim SQL As String = sb.ToString
        Dim rc As Integer = SQLHelper.ExecuteNonQuery(SQL, exs)
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Function SearchConcepte(sKey As String) As DTOPurchaseOrderConcepte
        Dim retval As DTOPurchaseOrderConcepte = Nothing
        Dim SQL As String = "SELECT ESP,CAT,ENG,SRC FROM PDD WHERE ID LIKE '" & sKey & "'"
        Dim oDrd As SqlClient.SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOPurchaseOrderConcepte
            With retval
                .Esp = oDrd("ESP")
                .Cat = oDrd("CAT")
                .Eng = oDrd("ENG")
                .Src = oDrd("SRC")
            End With
        End If
        oDrd.Close()
        Return retval
    End Function


    Shared Function CobraPerVisa(oLog As DTOTpvLog, exs As List(Of Exception))
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


    Shared Function RecalculaPendents(exs As List(Of Exception), oPurchaseOrder As DTOPurchaseOrder) As Integer
        Dim retval As Integer

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("UPDATE Pnc SET Pnc.Pn2= Pnc.Qty-(CASE WHEN X.Qty IS NULL THEN 0 ELSE X.Qty END) ")
            sb.AppendLine("FROM Pnc LEFT OUTER JOIN (SELECT Arc.PncGuid, SUM(Arc.Qty) AS Qty ")
            sb.AppendLine("FROM Arc ")
            sb.AppendLine("GROUP BY Arc.PncGuid) X ON Pnc.Guid=X.PncGuid ")
            sb.AppendLine("WHERE Pnc.PdcGuid='" & oPurchaseOrder.Guid.ToString & "' ")
            sb.AppendLine("AND Pnc.Pn2 <> (Pnc.Qty-(CASE WHEN X.Qty IS NULL THEN 0 ELSE X.Qty END))")

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

    Shared Function RemovePromo(exs As List(Of Exception), oPurchaseOrder As DTOPurchaseOrder) As Boolean
        Dim SQL As String = "UPDATE Pdc SET PROMO = NULL WHERE Guid ='" & oPurchaseOrder.Guid.ToString & "'"
        SQLHelper.ExecuteNonQuery(SQL, exs)
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

End Class

Public Class PurchaseOrdersLoader

    Shared Function Years(oCod As DTOPurchaseOrder.Codis, oContact As DTOContact, Optional IncludeGroupSalePoints As Boolean = False) As List(Of Integer)
        Dim retval As New List(Of Integer)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Yea ")
        sb.AppendLine("FROM Pdc ")
        If oContact IsNot Nothing Then
            If IncludeGroupSalePoints Then
                sb.AppendLine("INNER JOIN CliClient ON Pdc.CliGuid=CliClient.Guid AND CliClient.CcxGuid = '" & oContact.Guid.ToString & "' ")
            Else
                sb.AppendLine("AND Pdc.CliGuid='" & oContact.Guid.ToString & "' ")
            End If
        End If
        sb.AppendLine("GROUP BY Pdc.Yea ")
        sb.AppendLine("ORDER BY Pdc.Yea DESC ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            retval.Add(oDrd("Yea"))
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Exists(oUser As DTOUser, DtFchFrom As Date, DtFchTo As Date) As Boolean
        UserLoader.Load(oUser)

        Dim sb As New System.Text.StringBuilder
        Select Case oUser.Rol.id
            Case DTORol.Ids.manufacturer
                sb.AppendLine("SELECT TOP 1 Pdc.Guid ")
                sb.AppendLine("From Email_Clis ")
                sb.AppendLine("INNER JOIN Tpa ON Email_Clis.ContactGuid = Tpa.Proveidor ")
                sb.AppendLine("INNER JOIN Stp ON Tpa.Guid=Stp.Brand ")
                sb.AppendLine("INNER JOIN Art ON Stp.Guid=Art.Category ")
                sb.AppendLine("INNER JOIN Pnc ON Art.Guid = Pnc.ArtGuid ")
                sb.AppendLine("INNER JOIN Pdc ON Pnc.PdcGuid = Pdc.Guid ")
                sb.AppendLine("WHERE Email_Clis.EmailGuid='" & oUser.Guid.ToString & "' ")
                sb.AppendLine("AND Pdc.Fch BETWEEN '" & Format(DtFchFrom, "yyyyMMdd") & "' AND '" & Format(DtFchTo, "yyyyMMdd") & "' ")
                sb.AppendLine("AND Pdc.Cod=" & 2 & " ")
                sb.AppendLine("AND Stp.Codi = " & 0 & " ")
            Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.salesManager
                sb.AppendLine("SELECT TOP 1 Pdc.Guid ")
                sb.AppendLine("From Tpa  ")
                sb.AppendLine("INNER JOIN Stp ON Tpa.Guid=Stp.Brand ")
                sb.AppendLine("INNER JOIN Art ON Stp.Guid=Art.Category ")
                sb.AppendLine("INNER JOIN Pnc ON Art.Guid = Pnc.ArtGuid ")
                sb.AppendLine("INNER JOIN Pdc ON Pnc.PdcGuid = Pdc.Guid ")
                sb.AppendLine("WHERE Tpa.Emp =" & oUser.Emp.Id & " ")
                sb.AppendLine("AND Pdc.Fch BETWEEN '" & Format(DtFchFrom, "yyyyMMdd") & "' AND '" & Format(DtFchTo, "yyyyMMdd") & "' ")
                sb.AppendLine("AND Pdc.Cod=" & 2 & " ")
            Case DTORol.Ids.rep, DTORol.Ids.comercial
                sb.AppendLine("SELECT TOP 1 Pdc.Guid ")
                sb.AppendLine("From Tpa  ")
                sb.AppendLine("INNER JOIN Stp ON Tpa.Guid=Stp.Brand ")
                sb.AppendLine("INNER JOIN Art ON Stp.Guid=Art.Category ")
                sb.AppendLine("INNER JOIN Pnc ON Art.Guid = Pnc.ArtGuid ")
                sb.AppendLine("INNER JOIN Pdc ON Pnc.PdcGuid = Pdc.Guid ")
                sb.AppendLine("INNER JOIN Email_Clis ON Pnc.RepGuid = Email_Clis.ContactGuid ")
                sb.AppendLine("WHERE Email_Clis.EmailGuid='" & oUser.Guid.ToString & "' ")
                sb.AppendLine("AND Pdc.Fch BETWEEN '" & Format(DtFchFrom, "yyyyMMdd") & "' AND '" & Format(DtFchTo, "yyyyMMdd") & "' ")
                sb.AppendLine("AND Pdc.Cod=" & 2 & " ")
            Case DTORol.Ids.cliFull, DTORol.Ids.cliLite
                sb.AppendLine("SELECT TOP 1 Pdc.Guid ")
                sb.AppendLine("From Tpa  ")
                sb.AppendLine("INNER JOIN Stp ON Tpa.Guid=Stp.Brand ")
                sb.AppendLine("INNER JOIN Art ON Stp.Guid=Art.Category ")
                sb.AppendLine("INNER JOIN Pnc ON Art.Guid = Pnc.ArtGuid ")
                sb.AppendLine("INNER JOIN Pdc ON Pnc.PdcGuid = Pdc.Guid ")
                sb.AppendLine("INNER JOIN Email_Clis ON Pdc.CliGuid = Email_Clis.ContactGuid ")
                sb.AppendLine("WHERE Email_Clis.EmailGuid='" & oUser.Guid.ToString & "' ")
                sb.AppendLine("AND Pdc.Fch BETWEEN '" & Format(DtFchFrom, "yyyyMMdd") & "' AND '" & Format(DtFchTo, "yyyyMMdd") & "' ")
                sb.AppendLine("AND Pdc.Cod=" & 2 & " ")
            Case Else
                Return False
                Exit Function
        End Select

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim retval As Boolean = oDrd.Read
        oDrd.Close()

        Return retval
    End Function




    Shared Function Headers(Optional oEmp As DTOEmp = Nothing,
                            Optional Cod As DTOPurchaseOrder.Codis = DTOPurchaseOrder.Codis.notSet,
                            Optional Contact As DTOContact = Nothing,
                            Optional Ccx As DTOCustomer = Nothing,
                            Optional Rep As DTORep = Nothing,
                            Optional Year As Integer = 0,
                            Optional FchCreatedFrom As Date = Nothing,
                            Optional FchCreatedTo As Date = Nothing) As JArray

        Dim retval As New JArray()

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Pdc.Guid, Pdc.Fch, Pdc.Pdc, Pdc.Pdd, Pdc.Cur, Pdc.Eur, Pdc.PdcPts, Pdc.Src, Pdc.Hash, Pdc.EtiquetesTransport ")
        sb.AppendLine(", Pdc.Cod AS PdcCod, Pdc.CliGuid, CliGral.FullNom, Pdc.Hide ")
        sb.AppendLine(", Pdc.FchCreated, Pdc.UsrCreatedGuid ")
        sb.AppendLine(", Email.Adr AS UsrCreatedEmailAddress, Email.Nickname AS UsrCreatedNickname ")
        sb.AppendLine(", SUM(CASE WHEN Pnc.ErrCod = 0 THEN Pnc.Pn2 ELSE 0 END) AS Pn2 ")
        sb.AppendLine(", ConsumerTicket.Id AS TicketId, ConsumerTicket.Nom AS TicketNom, ConsumerTicket.Cognom1 AS TicketCognom ")
        sb.AppendLine("FROM Pdc ")
        sb.AppendLine("INNER JOIN CliGral ON Pdc.CliGuid=CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Email ON Pdc.UsrCreatedGuid=Email.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Pnc ON Pdc.Guid=Pnc.PdcGuid ")
        If Ccx IsNot Nothing Then
            sb.AppendLine("INNER JOIN CliClient ON Pdc.CliGuid=CliClient.Guid ")
            sb.AppendLine("AND (Cliclient.Guid = '" & Ccx.Guid.ToString & "' OR Cliclient.CcxGuid = '" & Ccx.Guid.ToString & "') ")
        End If
        sb.AppendLine("LEFT OUTER JOIN ConsumerTicket ON Pdc.Guid = ConsumerTicket.PurchaseOrder ")

        sb.AppendLine("WHERE 1=1 ")

        If oEmp IsNot Nothing Then
            sb.AppendLine("AND Pdc.Emp=" & oEmp.Id & " ")
        End If

        If Cod <> DTOPurchaseOrder.Codis.notSet Then
            sb.AppendFormat("AND Pdc.Cod = {0} ", CInt(Cod))
        End If
        If Contact IsNot Nothing Then
            sb.AppendFormat("AND Pdc.CliGuid='{0}' ", Contact.Guid.ToString())
        End If

        If Rep IsNot Nothing Then
            sb.AppendFormat("AND Pnc.RepGuid='{0}' ", Rep.Guid.ToString())
        End If
        If Year > 0 Then
            sb.AppendFormat("AND Pdc.Yea = {0} ", Year)
        End If
        If FchCreatedFrom <> Nothing Then
            sb.AppendFormat("AND Pdc.FchCreated >='{0:yyyyMMdd}' ", FchCreatedFrom)
        End If
        If FchCreatedTo <> Nothing Then
            sb.AppendFormat("AND Pdc.FchCreated <='{0:yyyyMMdd}' ", FchCreatedTo)
        End If

        sb.AppendLine("GROUP BY Pdc.Guid, Pdc.Emp, Pdc.Fch, Pdc.Yea, Pdc.Pdc, Pdc.Pdd, Pdc.Cur, Pdc.Eur, Pdc.PdcPts, Pdc.Src, Pdc.Hash, Pdc.EtiquetesTransport ")
        sb.AppendLine(", Pdc.Cod, Pdc.CliGuid, CliGral.FullNom, Pdc.Hide ")
        sb.AppendLine(", Pdc.FchCreated, Pdc.UsrCreatedGuid ")
        sb.AppendLine(", ConsumerTicket.Id, ConsumerTicket.Nom, ConsumerTicket.Cognom1 ")
        sb.AppendLine(", Email.Adr, Email.Nickname ")
        sb.AppendLine("ORDER BY Pdc.Yea DESC, Pdc.Pdc DESC ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oTitular As New JObject
            oTitular.Add("Guid", oDrd("CliGuid").ToString())

            If IsDBNull(oDrd("TicketId")) Then
                oTitular.Add("FullNom", SQLHelper.GetStringFromDataReader(oDrd("FullNom")))
            Else
                oTitular.Add("FullNom", String.Format("ticket {0} de consumidor {1} {2}", oDrd("TicketId"), oDrd("TicketNom"), oDrd("TicketCognom")))
            End If

            Dim oCur As New JObject
            oCur.Add("Tag", oDrd("Cur").ToString())
            Dim oAmt As New JObject
            oAmt.Add("Val", SQLHelper.GetDecimalFromDataReader(oDrd("PdcPts")).ToString("F2", CultureInfo.InvariantCulture))
            oAmt.Add("Cur", oCur)

            Dim oOrder As New JObject

            oOrder.Add("Guid", oDrd("Guid").ToString())
            oOrder.Add("Num", oDrd("Pdc").ToString())
            oOrder.Add("Fch", CDate(oDrd("Fch")).ToString("yyyy-MM-dd"))
            oOrder.Add("Cod", oDrd("PdcCod").ToString)
            oOrder.Add("Hide", (oDrd("Hide") <> 0).ToString())
            oOrder.Add("Source", oDrd("Src").ToString())
            oOrder.Add("Concept", oDrd("Pdd").ToString())
            oOrder.Add("SumaDeImports", oAmt)

            Dim oCod As DTOPurchaseOrder.Codis = oDrd("PdcCod")
            Select Case oCod
                Case DTOPurchaseOrder.Codis.proveidor
                    oOrder.Add("Proveidor", oTitular)
                Case Else
                    oOrder.Add("Customer", oTitular)
            End Select

            If Not IsDBNull(oDrd("Pn2")) Then
                Dim pendingUnits As Integer = oDrd("Pn2")
                oOrder.Add("IsOpenOrder", (pendingUnits <> 0).ToString())
            End If

            If Not IsDBNull(oDrd("Hash")) Then
                Dim oDocFile As New JObject
                oDocFile.Add("Hash", oDrd("Hash").ToString())
                oOrder.Add("Docfile", oDocFile)
            End If

            If Not IsDBNull(oDrd("EtiquetesTransport")) Then
                Dim oDocFile As New JObject
                oDocFile.Add("Hash", oDrd("EtiquetesTransport").ToString())
                oOrder.Add("EtiquetesTransport", oDocFile)
            End If

            If Not IsDBNull(oDrd("UsrCreatedGuid")) Then
                Dim oUsrCreated As New JObject
                oUsrCreated.Add("Guid", oDrd("UsrCreatedGuid").ToString())
                If IsDBNull(oDrd("UsrCreatedNickname")) OrElse String.IsNullOrEmpty(oDrd("UsrCreatedNickname")) Then
                    oUsrCreated.Add("Nickname", SQLHelper.GetStringFromDataReader(oDrd("UsrCreatedEmailAddress")))
                Else
                    oUsrCreated.Add("Nickname", SQLHelper.GetStringFromDataReader(oDrd("UsrCreatedNickname")))
                End If

                Dim oUsrLog As New JObject
                oUsrLog.Add("UsrCreated", oUsrCreated)
                oOrder.Add("UsrLog", oUsrLog)
            End If


            retval.Add(oOrder)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function Search(oUser As DTOUser, searchTerm As String) As List(Of DTOPurchaseOrder) 'iMat 3.0
        Dim retval As New List(Of DTOPurchaseOrder)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Pdc.Guid, Pdc.FchCreated, Pdc.Pdc, Pdc.CliGuid, CliGral.FullNom, Pdc.Eur, Pdc.Cod, Pdc.Hide ")
        sb.AppendLine("FROM Pdc ")
        sb.AppendLine("INNER JOIN CliGral ON Pdc.CliGuid=CliGral.Guid ")
        sb.AppendLine("WHERE Pdc.UsrCreatedGuid='" & oUser.Guid.ToString & "' ")
        sb.AppendLine("AND DATEDIFF(hh,Pdc.FchCreated,GETDATE())<=24 ")
        sb.AppendLine("ORDER BY Pdc.FchCreated DESC ")


        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oOrder As New DTOPurchaseOrder(oDrd("Guid"))
            With oOrder
                .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd, UsrCreatedField:="UsrCreatedGuid", UsrLastEditedField:="UsrLastEditedGuid")
                .Num = oDrd("Pdc")
                .Customer = New DTOCustomer(oDrd("CliGuid"))
                .Customer.FullNom = oDrd("FullNom")
                .SumaDeImports = DTOAmt.Factory(CDec(oDrd("Eur")))
                .Cod = oDrd("Cod")
                .Hide = oDrd("Hide")
                retval.Add(oOrder)
            End With
        Loop
        oDrd.Close()
        Return retval
    End Function
    Shared Function All(oCod As DTOPurchaseOrder.Codis, oContact As DTOContact, Optional iYear As Integer = 0, Optional IncludeGroupSalePoints As Boolean = False) As List(Of DTOPurchaseOrder)
        Dim retval As New List(Of DTOPurchaseOrder)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Pdc.Emp, Pdc.Guid, Pdc.Fch, Pdc.Pdc, Pdc.Pdd, Pdc.Cod, Pdc.Eur AS PdcEur, Pdc.Src, Pdc.Promo, Pdc.Hash, Pdc.EtiquetesTransport, Pdc.Hide ")
        sb.AppendLine(", Pdc.CliGuid, Pdc.TotJunt, Pdc.FchMin, Pdc.FchMax, CliGral.FullNom, UsrCreatedGuid ")
        sb.AppendLine(", Pnc.Guid AS PncGuid, Pnc.Qty, Pnc.ArtGuid, Pnc.Pn2, Pnc.Pts, Pnc.Cur, Pnc.Eur AS PncEur, Pnc.Dto, Pnc.carrec, Pnc.Lin ")
        sb.AppendLine(", VwSkuNom.* ")
        sb.AppendLine(", Email.Adr AS UsrCreatedEmail, Email.Nickname AS UsrCreatedNickname ")

        If IncludeGroupSalePoints Then
            sb.AppendLine(", CliClient.Ref ")
        End If

        sb.AppendLine("FROM Pdc ")
        sb.AppendLine("INNER JOIN CliGral ON Pdc.CliGuid=CliGral.Guid ")
        If oContact IsNot Nothing Then
            If IncludeGroupSalePoints Then
                sb.AppendLine("INNER JOIN CliClient ON Pdc.CliGuid=CliClient.Guid AND CliClient.CcxGuid = '" & oContact.Guid.ToString & "' ")
            Else
                sb.AppendLine("AND Pdc.CliGuid='" & oContact.Guid.ToString & "' ")
            End If
        End If
        sb.AppendLine("LEFT OUTER JOIN Pnc ON Pdc.Guid=Pnc.PdcGuid ")
        sb.AppendLine("LEFT OUTER JOIN Email ON Pdc.UsrCreatedGuid=Email.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuNom ON Pnc.ArtGuid = VwSkuNom.SkuGuid ")
        sb.AppendLine("WHERE Pdc.Cod = " & CInt(oCod) & " ")
        If iYear > 0 Then
            sb.AppendLine("AND Pdc.Yea = " & iYear & " ")
        End If
        sb.AppendLine("ORDER BY Pdc.Yea DESC, Pdc.Pdc DESC, Pnc.Lin ")

        Dim oOrder As New DTOPurchaseOrder
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oOrder.Guid.Equals(oDrd("Guid")) Then
                Try
                    oOrder = New DTOPurchaseOrder(oDrd("Guid"))
                    retval.Add(oOrder)
                    With oOrder
                        .Emp = New DTOEmp(oDrd("Emp"))
                        .Fch = oDrd("Fch")
                        .Num = oDrd("Pdc")
                        .Hide = oDrd("Hide")
                        Select Case oCod
                            Case DTOPurchaseOrder.Codis.proveidor
                                .Proveidor = New DTOProveidor(oDrd("CliGuid"))
                                .Proveidor.FullNom = oDrd("FullNom")
                            Case Else
                                .Customer = New DTOCustomer(oDrd("CliGuid"))
                                .Customer.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                                If IncludeGroupSalePoints Then
                                    .Customer.Ref = SQLHelper.GetStringFromDataReader(oDrd("Ref"))
                                End If
                        End Select
                        .Concept = oDrd("Pdd")
                        .Cod = oDrd("Cod")
                        .SumaDeImports = DTOAmt.Factory(CDec(oDrd("PdcEur")))
                        .Source = oDrd("Src")
                        .TotJunt = oDrd("TotJunt")
                        .FchDeliveryMin = Defaults.FchOrNothing(oDrd("FchMin"))
                        .FchDeliveryMax = Defaults.FchOrNothing(oDrd("FchMax"))

                        If Not IsDBNull(oDrd("Promo")) Then
                            .Incentiu = New DTOIncentiu(oDrd("Promo"))
                        End If

                        .DocFile = SQLHelper.GetDocFileFromDataReaderHash(oDrd("Hash"))
                        .EtiquetesTransport = SQLHelper.GetDocFileFromDataReaderHash(oDrd("EtiquetesTransport"))

                        .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd, UsrCreatedField:="UsrCreatedGuid", UsrLastEditedField:="UsrLastEditedGuid")

                        .Items = New List(Of DTOPurchaseOrderItem)
                    End With

                Catch ex As Exception
                    'Stop
                End Try
            End If

            If Not IsDBNull(oDrd("PncGuid")) Then
                Try

                    Dim oItem As New DTOPurchaseOrderItem(oDrd("PncGuid"))
                    With oItem
                        .Qty = oDrd("Qty")
                        .Pending = oDrd("Pn2")
                        .Sku = SQLHelper.GetProductFromDataReader(oDrd)
                        .Price = SQLHelper.GetAmtFromDataReader2(oDrd, "PncEur", "Cur", "Pts")
                        .Dto = oDrd("Dto")
                        .ChargeCod = IIf(oDrd("carrec") = 0, DTOPurchaseOrderItem.ChargeCods.FOC, DTOPurchaseOrderItem.ChargeCods.chargeable)
                        .Lin = oDrd("Lin")
                    End With
                    oOrder.Items.Add(oItem)
                Catch ex As Exception
                    'Stop
                End Try

            End If
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oUser As DTOUser) As List(Of DTOPurchaseOrder.HeaderModel)
        Dim retval As New List(Of DTOPurchaseOrder.HeaderModel)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Pdc.Guid, Pdc.Pdc, Pdc.Fch, Pdc.CliGuid, CliGral.FullNom, Pdc.Pdd, Pdc.Eur, Pdc.Cod, Pdc.Src, Pdc.Hash ")
        sb.AppendLine(", Email.Adr AS UsrCreatedEmail, Email.Nickname AS UsrCreatedNickname ")
        sb.AppendLine(", SUM(Pnc.Pn2) AS PendingQty ")
        sb.AppendLine("FROM Pdc ")
        sb.AppendLine("INNER JOIN Email_Clis ON Pdc.CliGuid = Email_Clis.ContactGuid ")
        sb.AppendLine("INNER JOIN CliGral ON Pdc.CliGuid = CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Pnc ON Pdc.Guid = Pnc.PdcGuid ")
        sb.AppendLine("LEFT OUTER JOIN Email ON Pdc.UsrCreatedGuid = Email.Guid ")
        sb.AppendLine("WHERE Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
        sb.AppendLine("GROUP BY Pdc.Guid, Pdc.Pdc, Pdc.Fch, Pdc.CliGuid, CliGral.FullNom, Pdc.Pdd, Pdc.Eur, Pdc.Cod, Pdc.Src, Pdc.Hash ")
        sb.AppendLine(", Email.Adr, Email.Nickname, Pdc.FchCreated ")
        sb.AppendLine("ORDER BY Pdc.FchCreated DESC ")

        Dim SQL = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oItem As New DTOPurchaseOrder.HeaderModel()
            With oItem
                .Guid = oDrd("Guid")
                .Num = oDrd("Pdc")
                .Fch = oDrd("Fch")
                .Contact = DTOGuidNom.Compact.Factory(oDrd("CliGuid"), oDrd("FullNom"))
                .Eur = oDrd("Eur")
                .Concept = SQLHelper.GetStringFromDataReader(oDrd("Pdd"))
                .Src = oDrd("Src")
                .Cod = oDrd("Cod")
                .IsOpen = SQLHelper.GetIntegerFromDataReader(oDrd("PendingQty")) > 0
                .Hash = SQLHelper.GetStringFromDataReader(oDrd("Hash"))

                Dim oUserCreated As New DTOUser()
                With oUserCreated
                    .EmailAddress = SQLHelper.GetStringFromDataReader(oDrd("UsrCreatedEmail"))
                    .NickName = SQLHelper.GetStringFromDataReader(oDrd("UsrCreatedNickname"))
                End With
                .UsrCreated = oUserCreated.NicknameOrElse()
            End With
            retval.Add(oItem)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function All(oIncentiu As DTOIncentiu, oUser As DTOUser) As List(Of DTOPurchaseOrder)
        Dim retval As New List(Of DTOPurchaseOrder)

        Dim oIncentius As New List(Of DTOIncentiu)
        oIncentius.Add(oIncentiu)
        If oIncentiu.Guid.ToString.ToUpper = "450B5F8F-DDE7-4790-B047-C5E9C7FD61AB" Then
            'si es la promo dels accessoris trilogy afegeix els clients de la promo del 10cotxes+1
            Dim oIncentiu2 As New DTOIncentiu(New Guid("2069FE91-C71D-4075-A4A2-F4BAC9E9533F"))
            oIncentius.Add(oIncentiu2)
        End If

        Dim sb As New System.Text.StringBuilder

        Select Case oUser.Rol.id
            Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.salesManager, DTORol.Ids.marketing, DTORol.Ids.operadora, DTORol.Ids.comercial, DTORol.Ids.manufacturer
                sb.AppendLine("SELECT Pdc.Guid, Pdc.Fch, Pdc.Pdc, Pdc.Pdd, Pdc.Cod, Pdc.Eur AS PdcEur, Pdc.Src, Pdc.Promo, Pdc.Hash, Pdc.EtiquetesTransport, Pdc.FchCreated, Pdc.Hide ")
                sb.AppendLine(", Pdc.CliGuid, Pdc.TotJunt, Pdc.fchMin, Pdc.fchMax, CliGral.FullNom, UsrCreatedGuid, Email.adr AS UsrCreatedEmail, Email.nickname AS UsrCreatedNickname ")
                sb.AppendLine(", Pnc.Guid AS PncGuid, Pnc.Qty, Pnc.ArtGuid, Pnc.Pn2, Pnc.Pts, Pnc.Cur, Pnc.Eur AS PncEur, Pnc.Dto, Pnc.carrec, Pnc.RepGuid, Pnc.RepCustom, Pnc.Lin ")
                sb.AppendLine(", VwSkuNom.* ")
                sb.AppendLine("FROM Pdc ")
                sb.AppendLine("INNER JOIN CliGral ON Pdc.CliGuid=CliGral.Guid ")
                sb.AppendLine("LEFT OUTER JOIN Pnc ON Pdc.Guid=Pnc.PdcGuid ")
                sb.AppendLine("LEFT OUTER JOIN Email ON Pdc.UsrCreatedGuid=Email.Guid ")
                sb.AppendLine("LEFT OUTER JOIN VwSkuNom ON Pnc.ArtGuid = VwSkuNom.SkuGuid ")
                sb.AppendLine("WHERE (")
                For Each item As DTOIncentiu In oIncentius
                    If item.UnEquals(oIncentius.First) Then
                        sb.Append("OR ")
                    End If
                    sb.Append("Pdc.Promo ='" & item.Guid.ToString & "' ")
                Next
                sb.AppendLine(") ")
                sb.AppendLine("ORDER BY Pdc.Yea DESC, Pdc.Pdc DESC, Pnc.Lin ")
            Case DTORol.Ids.rep, DTORol.Ids.comercial
                sb.AppendLine("SELECT Pdc.Guid, Pdc.Fch, Pdc.Pdc, Pdc.Pdd, Pdc.Cod, Pdc.Eur AS PdcEur, Pdc.Src, Pdc.Promo, Pdc.Hash, Pdc.EtiquetesTransport, Pdc.FchCreated, Pdc.Hide ")
                sb.AppendLine(", Pdc.CliGuid, Pdc.TotJunt, Pdc.fchMin, Pdc.fchMax, CliGral.FullNom, UsrCreatedGuid, Email.adr AS UsrCreatedEmail, Email.nickname AS UsrCreatedNickname ")
                sb.AppendLine(", Pnc.Guid AS PncGuid, Pnc.Qty, Pnc.ArtGuid, Pnc.Pn2, Pnc.Pts, Pnc.Cur, Pnc.Eur AS PncEur, Pnc.Dto, Pnc.carrec, Pnc.RepGuid, Pnc.RepCustom, Pnc.Lin ")
                sb.AppendLine(", VwSkuNom.* ")
                sb.AppendLine("FROM Incentiu ")
                sb.AppendLine("INNER JOIN Pdc ON Incentiu.Guid=Pdc.Promo ")
                sb.AppendLine("INNER JOIN Pnc ON Pdc.Guid=Pnc.PdcGuid ")
                sb.AppendLine("INNER JOIN Email_Clis ON Email_Clis.ContactGuid = Pnc.RepGuid AND Email_Clis.EmailGuid='" & oUser.Guid.ToString & "' ")
                sb.AppendLine("INNER JOIN CliGral ON Pdc.CliGuid=CliGral.Guid ")
                'sb.AppendLine("LEFT OUTER JOIN Pnc ON Pdc.Guid=Pnc.PdcGuid ")
                sb.AppendLine("LEFT OUTER JOIN Email ON Pdc.UsrCreatedGuid=Email.Guid ")
                sb.AppendLine("LEFT OUTER JOIN VwSkuNom ON Pnc.ArtGuid = VwSkuNom.SkuGuid ")
                sb.AppendLine("WHERE (")
                For Each item As DTOIncentiu In oIncentius
                    If item.UnEquals(oIncentius.First) Then
                        sb.Append("OR ")
                    End If
                    sb.Append("Pdc.Promo ='" & item.Guid.ToString & "' ")
                Next
                sb.AppendLine(") ")
                'sb.AppendLine("AND (RepProducts.FchFrom IS NULL OR RepProducts.FchFrom<=GETDATE())  ")
                'sb.AppendLine("AND (RepProducts.FchTo IS NULL OR RepProducts.FchTo>GETDATE()) ")
                'sb.AppendLine("AND GETDATE() >= Incentiu.FchFrom ") 'evita mostrar les que encara no estan actives
                sb.AppendLine("ORDER BY Pdc.Yea DESC, Pdc.Pdc DESC, Pnc.Lin ")
            Case DTORol.Ids.cliFull, DTORol.Ids.cliLite
                sb.AppendLine("SELECT Pdc.Guid, Pdc.Fch, Pdc.Pdc, Pdc.Pdd, Pdc.Cod, Pdc.Eur AS PdcEur, Pdc.Src, Pdc.Promo, Pdc.Hash, Pdc.EtiquetesTransport, Pdc.FchCreated, Pdc.Hide ")
                sb.AppendLine(", Pdc.CliGuid, Pdc.TotJunt, Pdc.fchMin, Pdc.fchMax, CliGral.FullNom, UsrCreatedGuid, Email.adr AS UsrCreatedEmail, Email.nickname AS UsrCreatedNickname ")
                sb.AppendLine(", Pnc.Guid AS PncGuid, Pnc.Qty, Pnc.ArtGuid, Pnc.Pn2, Pnc.Pts, Pnc.Cur, Pnc.Eur AS PncEur, Pnc.Dto, Pnc.carrec, Pnc.RepGuid, Pnc.RepCustom, Pnc.Lin ")
                sb.AppendLine(", VwSkuNom.* ")
                sb.AppendLine("FROM Incentiu ")
                sb.AppendLine("INNER JOIN Pdc ON Incentiu.Guid=Pdc.Promo ")
                sb.AppendLine("INNER JOIN CliGral ON Pdc.CliGuid=CliGral.Guid ")
                sb.AppendLine("INNER JOIN VwProductParent ON Incentiu.Product = VwProductParent.Child ")
                sb.AppendLine("INNER JOIN Tpa ON Tpa.Guid = VwProductParent.Parent ")
                sb.AppendLine("INNER JOIN Email_Clis ON Email_Clis.ContactGuid = Tpa.Proveidor AND Email_Clis.EmailGuid='" & oUser.Guid.ToString & "' ")
                sb.AppendLine("LEFT OUTER JOIN Pnc ON Pdc.Guid=Pnc.PdcGuid ")
                sb.AppendLine("LEFT OUTER JOIN Email ON Pdc.UsrCreatedGuid=Email.Guid ")
                sb.AppendLine("LEFT OUTER JOIN VwSkuNom ON Pnc.ArtGuid = VwSkuNom.SkuGuid ")
                sb.AppendLine("WHERE (")
                For Each item As DTOIncentiu In oIncentius
                    If item.UnEquals(oIncentius.First) Then
                        sb.Append("OR ")
                    End If
                    sb.Append("Pdc.Promo ='" & item.Guid.ToString & "' ")
                Next
                sb.AppendLine(") ")
                sb.AppendLine("ORDER BY Pdc.Yea DESC, Pdc.Pdc DESC, Pnc.Lin ")
        End Select


        Dim oOrder As New DTOPurchaseOrder
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oOrder.Guid.Equals(oDrd("Guid")) Then
                oOrder = New DTOPurchaseOrder(oDrd("Guid"))
                retval.Add(oOrder)
                With oOrder
                    .Fch = oDrd("Fch")
                    .Num = oDrd("Pdc")
                    .Customer = New DTOCustomer(oDrd("CliGuid"))
                    .Customer.FullNom = oDrd("FullNom")
                    .Concept = oDrd("Pdd")
                    .Cod = oDrd("Cod")
                    .Hide = oDrd("Hide")
                    .SumaDeImports = DTOAmt.Factory(CDec(oDrd("PdcEur")))
                    .Source = oDrd("Src")
                    .TotJunt = oDrd("TotJunt")
                    .FchDeliveryMin = Defaults.FchOrNothing(oDrd("FchMin"))
                    .FchDeliveryMax = Defaults.FchOrNothing(oDrd("FchMax"))
                    .Incentiu = oIncentius.First
                    .DocFile = SQLHelper.GetDocFileFromDataReaderHash(oDrd("Hash"))
                    .EtiquetesTransport = SQLHelper.GetDocFileFromDataReaderHash(oDrd("EtiquetesTransport"))
                    .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd, UsrCreatedField:="UsrCreatedGuid", UsrLastEditedField:="UsrLastEditedGuid")
                    .Items = New List(Of DTOPurchaseOrderItem)
                End With
            End If

            If Not IsDBNull(oDrd("ArtGuid")) Then

                Dim oItem As New DTOPurchaseOrderItem(oDrd("PncGuid"))
                With oItem
                    .Qty = oDrd("Qty")
                    .Pending = oDrd("Pn2")
                    .Sku = SQLHelper.GetProductFromDataReader(oDrd)
                    .Price = DTOAmt.Empty
                    .Price.Cur = DTOCur.Factory(oDrd("Cur"))
                    .Price.Eur = oDrd("PncEur")
                    .Price.Val = oDrd("Pts")
                    .Dto = oDrd("Dto")
                    .ChargeCod = IIf(oDrd("carrec") = 0, DTOPurchaseOrderItem.ChargeCods.FOC, DTOPurchaseOrderItem.ChargeCods.chargeable)
                    If Not IsDBNull(oDrd("RepGuid")) Then
                        .RepCom = New DTORepCom()
                        .RepCom.Rep = New DTORep(oDrd("RepGuid"))
                        .RepCom.repCustom = SQLHelper.GetBooleanFromDatareader(oDrd("RepCustom"))
                    End If

                    .Lin = oDrd("Lin")
                End With
                oOrder.Items.Add(oItem)

            End If
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function LastOrdersEntered(oUser As DTOUser) As List(Of DTOPurchaseOrder)
        Dim retval As New List(Of DTOPurchaseOrder)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Pdc.Guid, Pdc.FchCreated, Pdc.Pdc, Pdc.CliGuid, CliGral.FullNom, Pdc.Eur, Pdc.Cod, Pdc.Hide ")
        sb.AppendLine("FROM Pdc ")
        sb.AppendLine("INNER JOIN CliGral ON Pdc.CliGuid=CliGral.Guid ")
        sb.AppendLine("WHERE Pdc.UsrCreatedGuid='" & oUser.Guid.ToString & "' ")
        sb.AppendLine("AND DATEDIFF(hh,Pdc.FchCreated,GETDATE())<=24 ")
        sb.AppendLine("ORDER BY Pdc.FchCreated DESC ")


        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oOrder As New DTOPurchaseOrder(oDrd("Guid"))
            With oOrder
                .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd, UsrCreatedField:="UsrCreatedGuid", UsrLastEditedField:="UsrLastEditedGuid")
                .Num = oDrd("Pdc")
                .Customer = New DTOCustomer(oDrd("CliGuid"))
                .Customer.FullNom = oDrd("FullNom")
                .SumaDeImports = DTOAmt.Factory(CDec(oDrd("Eur")))
                .Cod = oDrd("Cod")
                .Hide = oDrd("Hide")
                retval.Add(oOrder)
            End With
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function FromIds(oEmp As DTOEmp, Ids As HashSet(Of String)) As List(Of DTOPurchaseOrder)
        Dim retval As New List(Of DTOPurchaseOrder)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	      Year int NOT NULL")
        sb.AppendLine("	    , Id int NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Year,Id) ")

        Dim idx As Integer = 0
        For Each fullId In Ids
            If fullId.Length > 4 Then
                Dim year = fullId.Substring(0, 4)
                Dim id = fullId.Substring(4).toInteger()
                sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
                sb.AppendFormat("({0},{1}) ", year, id)
                idx += 1
            End If
        Next

        sb.AppendLine("SELECT Pdc.Guid, Pdc.CliGuid, Pdc.Cod, Pdc.Fch, Pdc.Pdc ")
        sb.AppendLine(", Pnc.Guid AS PncGuid, Pnc.Qty, Pnc.Pn2, Pnc.Eur AS PncEur, Pnc.Dto AS PncDto, Pnc.ArtGuid ")
        sb.AppendLine("FROM Pdc ")
        sb.AppendLine("INNER JOIN @Table X ON YEAR(Pdc.Fch) = X.Year AND Pdc.Pdc = X.Id ")
        sb.AppendLine("LEFT OUTER JOIN Pnc ON Pdc.Guid = Pnc.PdcGuid ")
        sb.AppendLine("WHERE Pdc.Emp = " & oEmp.Id & " ")
        sb.AppendLine("ORDER BY YEAR(Pdc.Fch), Pdc.Pdc ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oPurchaseOrder As New DTOPurchaseOrder
        Do While oDrd.Read
            If Not oPurchaseOrder.Guid.Equals(oDrd("Guid")) Then
                oPurchaseOrder = New DTOPurchaseOrder(oDrd("Guid"))
                With oPurchaseOrder
                    .Cod = oDrd("Cod")
                    .Fch = oDrd("Fch")
                    .Num = oDrd("Pdc")
                    Select Case .Cod
                        Case DTOPurchaseOrder.Codis.proveidor
                            .Proveidor = New DTOProveidor(oDrd("CliGuid"))
                        Case Else
                            .Customer = New DTOCustomer(oDrd("CliGuid"))
                    End Select
                End With
                retval.Add(oPurchaseOrder)
            End If
            If Not IsDBNull(oDrd("PncGuid")) Then
                Dim item As New DTOPurchaseOrderItem(oDrd("PncGuid"))
                With item
                    .Sku = New DTOProductSku(oDrd("ArtGuid"))
                    .Qty = oDrd("Qty")
                    .Pending = oDrd("Pn2")
                    .Price = DTOAmt.Factory(oDrd("PncEur"))
                    .Dto = oDrd("PncDto")
                    .PurchaseOrder = New DTOPurchaseOrder(oPurchaseOrder.Guid)
                End With
                oPurchaseOrder.Items.Add(item)
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Pending(oEmp As DTOEmp, Optional cod As DTOPurchaseOrder.Codis = DTOPurchaseOrder.Codis.notSet, Optional contact As DTOContact = Nothing, Optional oUser As DTOUser = Nothing) As List(Of DTOPurchaseOrder)
        Dim retval As New List(Of DTOPurchaseOrder)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Pnc.Guid AS PncGuid, Pnc.qty, Pnc.pn2, Pnc.ArtGuid, Pnc.Lin, Pnc.Eur, Pnc.dto, Pdc.Hide ")
        sb.AppendLine(", Pdc.Guid as PdcGuid, Pdc.pdc, Pdc.Cod, Pdc.fch, Pdc.fchmin, Pdc.fchmax, Pdc.CliGuid, Pdc.pdd ")
        sb.AppendLine(", VwSkuNom.* ")
        sb.AppendLine("FROM Pdc ")
        sb.AppendLine("INNER JOIN Pnc on Pdc.Guid=Pnc.pdcGuid ")
        sb.AppendLine("INNER JOIN VwSkuNom on Pnc.ArtGuid=VwSkuNom.SkuGuid ")
        If oUser IsNot Nothing Then
            sb.AppendLine("INNER JOIN Email_Clis ON Pdc.CliGuid = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
        End If
        sb.AppendLine("WHERE VwSkuNom.Emp = " & oEmp.Id & " ")
        sb.AppendLine("AND Pnc.Pn2 >0 ")
        If cod <> DTOPurchaseOrder.Codis.notSet Then
            sb.AppendLine("AND Pdc.Cod = " & CInt(cod) & " ")
        End If
        If contact IsNot Nothing Then
            sb.AppendLine("AND Pdc.CliGuid = '" & contact.Guid.ToString & "' ")
        End If
        sb.AppendLine("ORDER by Pdc.fch, Pdc.pdd, Pdc.Guid, Pnc.Lin")

        Dim SQL As String = sb.ToString
        Dim oPurchaseOrder As New DTOPurchaseOrder
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oPurchaseOrder.Guid.Equals(oDrd("PdcGuid")) Then
                oPurchaseOrder = New DTOPurchaseOrder(oDrd("PdcGuid"))
                With oPurchaseOrder
                    .Concept = oDrd("Pdd")
                    .Num = oDrd("Pdc")
                    .Cod = oDrd("Cod")
                    .Hide = oDrd("Hide")
                    .Fch = oDrd("Fch")
                    .FchDeliveryMin = SQLHelper.GetFchFromDataReader(oDrd("FchMin"))
                    .FchDeliveryMax = SQLHelper.GetFchFromDataReader(oDrd("FchMax"))
                    Select Case cod
                        Case DTOPurchaseOrder.Codis.proveidor
                            .Proveidor = DTOProveidor.FromContact(contact)
                        Case Else
                            .Customer = DTOCustomer.FromContact(contact)
                    End Select
                    .Items = New List(Of DTOPurchaseOrderItem)
                End With
                retval.Add(oPurchaseOrder)
            End If

            Dim oItem As New DTOPurchaseOrderItem(oDrd("PncGuid"))
            With oItem
                .PurchaseOrder = oPurchaseOrder
                .Lin = oDrd("Lin")
                .Qty = oDrd("Qty")
                .Pending = oDrd("Pn2")
                .Sku = SQLHelper.GetProductFromDataReader(oDrd)
                .Price = DTOAmt.Factory(CDec(oDrd("Eur")))
                .Dto = oDrd("Dto")
            End With
            oPurchaseOrder.Items.Add(oItem)

        Loop
        oDrd.Close()

        Return retval
    End Function


    Shared Function PendingSkus(oCustomer As DTOCustomer) As List(Of DTOPurchaseOrderItem)
        Dim retval As New List(Of DTOPurchaseOrderItem)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Pnc.Guid AS PncGuid, Pnc.qty, Pnc.pn2, Pnc.ArtGuid, Pnc.Lin, Pnc.Eur, Pnc.dto ")
        sb.AppendLine(", Pdc.Guid as PdcGuid, Pdc.pdc, Pdc.Cod, Pdc.fch, Pdc.fchmin, Pdc.fchmax, Pdc.CliGuid, Pdc.pdd ")
        sb.AppendLine(", VwProductNom.BrandGuid, VwProductNom.BrandNom, VwProductNom.CategoryGuid, VwProductNom.CategoryNom, VwProductNom.SkuNom, VwProductNom.SkuNomLlarg ")
        sb.AppendLine("FROM Pdc ")
        sb.AppendLine("INNER JOIN Pnc on Pdc.Guid=Pnc.pdcGuid ")
        sb.AppendLine("INNER JOIN VwProductNom on Pnc.ArtGuid=VwProductNom.Guid ")
        sb.AppendLine("WHERE Pdc.CliGuid = '" & oCustomer.Guid.ToString & "' And Pnc.Pn2 > 0 ")
        sb.AppendLine("ORDER by VwProductNom.BrandNom, VwProductNom.BrandGuid, VwProductNom.CategoryNom, VwProductNom.CategoryGuid, VwProductNom.SkuNom, Pnc.ArtGuid, Pdc.Fch, Pdc.Guid")

        Dim oBrand As New DTOProductBrand
        Dim oCategory As New DTOProductCategory
        Dim SQL As String = sb.ToString
        Dim oPurchaseOrders As New List(Of DTOPurchaseOrder)
        Dim oPurchaseOrder As New DTOPurchaseOrder
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            oPurchaseOrder = oPurchaseOrders.Find(Function(x) x.Guid.Equals(oDrd("PdcGuid")))
            If oPurchaseOrder Is Nothing Then
                oPurchaseOrder = New DTOPurchaseOrder(oDrd("PdcGuid"))
                With oPurchaseOrder
                    .Concept = oDrd("Pdd")
                    .Num = oDrd("Pdc")
                    .Cod = oDrd("Cod")
                    .Fch = oDrd("Fch")
                    .FchDeliveryMin = SQLHelper.GetFchFromDataReader(oDrd("FchMin"))
                    .FchDeliveryMax = SQLHelper.GetFchFromDataReader(oDrd("FchMax"))
                    .Customer = oCustomer
                    .Items = New List(Of DTOPurchaseOrderItem)
                End With
                oPurchaseOrders.Add(oPurchaseOrder)
            End If

            If Not oCategory.Guid.Equals(oDrd("CategoryGuid")) Then
                If Not oBrand.Guid.Equals(oDrd("BrandGuid")) Then
                    oBrand = New DTOProductBrand(oDrd("BrandGuid"))
                    oBrand.Nom.Esp = oDrd("BrandNom")
                End If
                oCategory = New DTOProductCategory(oDrd("CategoryGuid"))
                oCategory.Nom.Esp = oDrd("CategoryNom")
                oCategory.Brand = oBrand
            End If

            Dim oItem As New DTOPurchaseOrderItem(oDrd("PncGuid"))
            With oItem
                .PurchaseOrder = oPurchaseOrder
                .Lin = oDrd("Lin")
                .Qty = oDrd("Qty")
                .Pending = oDrd("Pn2")
                .Sku = New DTOProductSku(DirectCast(oDrd("ArtGuid"), Guid))
                .Sku.NomLlarg.Esp = oDrd("SkuNomLlarg")
                .Sku.Nom.Esp = oDrd("SkuNom")
                .Sku.Category = oCategory
                .Price = DTOAmt.Factory(CDec(oDrd("Eur")))
                .Dto = oDrd("Dto")
            End With

            retval.Add(oItem)

        Loop
        oDrd.Close()

        Return retval
    End Function

    Shared Function PendingForPlatforms(oCcx As DTOCustomer, oHolding As DTOHolding) As List(Of DTOPurchaseOrder)
        Dim retval As New List(Of DTOPurchaseOrder)

        Dim oPlatforms As New List(Of DTOCustomerPlatform)
        Dim oNoPlatform As New DTOCustomerPlatform(System.Guid.Empty)
        oNoPlatform.Nom = "(sense plataforma)"
        oNoPlatform.BaseImponible = DTOAmt.Empty
        oNoPlatform.Deliveries = New List(Of DTODelivery)
        oPlatforms.Add(oNoPlatform)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Pnc.Guid AS PncGuid, Pnc.qty, Pnc.pn2, Pnc.ArtGuid, pnc.Lin, pnc.Eur, pnc.dto, pnc.bundle ")
        sb.AppendLine(", Pnc.ErrCod, Pnc.ErrDsc ")
        sb.AppendLine(", Pdc.Guid as PdcGuid,pdc.pdc,Pdc.Cod,pdc.fch, Pdc.FchMin, Pdc.FchMax,Pdc.CliGuid, pdc.pdd, Pdc.Obs, Pdc.Nadms ")
        sb.AppendLine(", CliClient.CcxGuid, CliClient.Ref, CliGral.RaoSocial, CliGral.NomCom, VwTelDefault.TelNum ")
        sb.AppendLine(", Pdc.[Platform], CliGral.FullNom as ClientNom, Ccx.CashCod, Ccx.[Ports], Ccx.AlbVal ")
        sb.AppendLine(", CliClient.AlbVal, CliClient.ExportCod ")
        sb.AppendLine(", P.FullNom as PlatformNom ")
        sb.AppendLine(", VwAddress.* ")
        sb.AppendLine(", VwSkuNom.* ")
        sb.AppendLine("FROM Pdc ")
        sb.AppendLine("INNER JOIN CliGral on Pdc.CliGuid=CliGral.Guid ")
        sb.AppendLine("INNER JOIN CliClient on Pdc.CliGuid=CliClient.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwTelDefault on Pdc.CliGuid = VwTelDefault.Contact ")
        sb.AppendLine("LEFT OUTER JOIN CliGral as P on P.Guid=Pdc.Platform ")
        sb.AppendLine("LEFT OUTER JOIN VwAddress on Pdc.Platform = VwAddress.SrcGuid ")
        sb.AppendLine("INNER JOIN Pnc on Pdc.Guid=Pnc.PdcGuid ")
        sb.AppendLine("INNER JOIN VwSkuNom on Pnc.ArtGuid=VwSkuNom.SkuGuid ")
        sb.AppendLine("INNER JOIN CliClient Ccx on CliClient.CcxGuid = Ccx.Guid ")
        If oHolding Is Nothing Then
            sb.AppendLine("WHERE CliClient.CcxGuid = '" & oCcx.Guid.ToString & "' And Pn2 > 0 ")
        Else
            sb.AppendLine("WHERE Ccx.Holding = '" & oHolding.Guid.ToString & "' And Pn2 > 0 ")
        End If
        sb.AppendLine("ORDER BY Pdc.FchMin, Pdc.Fch, Pdc.Pdd, Pdc.Guid, Pnc.Lin")

        Dim SQL As String = sb.ToString
        Dim oPurchaseOrder As New DTOPurchaseOrder
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            'If oDrd("PncGuid").Equals(New Guid("40E3CEF9-A8F2-485F-B219-0E9B687045F7")) Then Stop '=============================
            If Not oPurchaseOrder.Guid.Equals(oDrd("PdcGuid")) Then

                Dim oPlatform As DTOCustomerPlatform = oNoPlatform
                If Not IsDBNull(oDrd("Platform")) Then
                    Dim oPlatformGuid As Guid = oDrd("Platform")
                    If oPlatforms.Exists(Function(x) x.Guid.Equals(oPlatformGuid)) Then
                        oPlatform = oPlatforms.Find(Function(x) x.Guid.Equals(oPlatformGuid))
                    Else
                        oPlatform = New DTOCustomerPlatform(oPlatformGuid)
                        If Not IsDBNull(oDrd("PlatformNom")) Then
                            oPlatform.Nom = oDrd("PlatformNom").ToString
                            oPlatform.Address = SQLHelper.GetAddressFromDataReader(oDrd)
                        End If
                        oPlatform.BaseImponible = DTOAmt.Empty
                        oPlatform.Deliveries = New List(Of DTODelivery)
                        oPlatforms.Add(oPlatform)
                    End If
                End If



                Dim oCustomer As New DTOCustomer(oDrd("CliGuid"))
                With oCustomer
                    .Nom = oDrd("RaoSocial")
                    .NomComercial = oDrd("NomCom")
                    .Ref = SQLHelper.GetStringFromDataReader(oDrd("Ref"))
                    .FullNom = oDrd("ClientNom")
                    .Telefon = SQLHelper.GetStringFromDataReader(oDrd("TelNum"))
                    .AlbValorat = oDrd("AlbVal")
                    .ExportCod = oDrd("ExportCod")
                    .DeliveryPlatform = oPlatform

                    If oCcx Is Nothing Then
                        .Ccx = New DTOCustomer(oDrd("CcxGuid"))
                    Else
                        .Ccx = oCcx
                    End If
                    With .Ccx
                        .PortsCod = oDrd("Ports")
                        .CashCod = oDrd("CashCod")
                        .AlbValorat = oDrd("AlbVal")
                    End With
                End With

                oPurchaseOrder = New DTOPurchaseOrder(oDrd("PdcGuid"))
                With oPurchaseOrder
                    .Concept = oDrd("Pdd")
                    .Num = oDrd("Pdc")
                    .Cod = oDrd("Cod")
                    .Fch = oDrd("Fch")
                    .FchDeliveryMin = SQLHelper.GetFchFromDataReader(oDrd("FchMin"))
                    .FchDeliveryMax = SQLHelper.GetFchFromDataReader(oDrd("FchMax"))
                    .Customer = oCustomer
                    .Platform = oPlatform
                    .NADMS = SQLHelper.GetStringFromDataReader(oDrd("Nadms"))
                    .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                    .Items = New List(Of DTOPurchaseOrderItem)
                End With
                retval.Add(oPurchaseOrder)
            End If

            Dim oItem As New DTOPurchaseOrderItem(oDrd("PncGuid"))
            With oItem
                .PurchaseOrder = oPurchaseOrder
                .Lin = oDrd("Lin")
                .Qty = oDrd("Qty")
                .Pending = oDrd("Pn2")
                .Sku = SQLHelper.GetProductFromDataReader(oDrd)
                .Price = DTOAmt.Factory(CDec(oDrd("Eur")))
                .Dto = oDrd("Dto")
                .ErrCod = SQLHelper.GetIntegerFromDataReader(oDrd("ErrCod"))
                .ErrDsc = SQLHelper.GetStringFromDataReader(oDrd("ErrDsc"))
            End With

            If PurchaseOrderLoader.isBundleChild(oDrd) Then
                Dim oBundleParent = oPurchaseOrder.Items.First(Function(x) x.Guid.Equals(oDrd("Bundle")))
                oBundleParent.Bundle.Add(oItem)
            Else
                oPurchaseOrder.Items.Add(oItem)
            End If


        Loop
        oDrd.Close()

        Dim o = retval.Where(Function(x) x.FchDeliveryMin <> Nothing).ToList '------------tmp

        Return retval
    End Function


    Shared Function StocksAvailableForPlatforms(oCcx As DTOCustomer, oMgz As DTOMgz, oHolding As DTOHolding) As List(Of DTOStockAvailable)
        Dim retval As New List(Of DTOStockAvailable)

        Dim sb As New System.Text.StringBuilder

        sb.AppendLine("SELECT Pnc.ArtGuid ")
        sb.AppendLine(", VwSkuStocks.stock ")
        sb.AppendLine(", (VwSkuPncs.Clients - VwSkuPncs.ClientsAlPot - VwSkuPncs.ClientsEnProgramacio - VwSkuPncs.ClientsBlockStock) AS Pn2 ")
        sb.AppendLine(", VwProductNom.Cod as ProductCod, VwProductNom.BrandGuid, VwProductNom.BrandNom, VwProductNom.CategoryGuid, VwProductNom.CategoryNom, VwProductNom.SkuNom, VwProductNom.SkuNomLlarg ")
        sb.AppendLine(", Art.LastProduction, Art.Obsoleto ")
        sb.AppendLine("FROM Pdc ")
        sb.AppendLine("INNER JOIN VwCcxOrMe ON pdc.CliGuid=VwCcxOrMe.Guid  ")
        sb.AppendLine("INNER JOIN Pnc ON Pdc.Guid=Pnc.pdcGuid ")
        sb.AppendLine("INNER JOIN Art ON Pnc.Artguid=Art.Guid ")
        sb.AppendLine("INNER JOIN VwProductNom ON Pnc.ArtGuid = VwProductNom.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuStocks ON Pnc.ArtGuid=VwSkuStocks.SkuGuid AND VwSkuStocks.MgzGuid='" & oMgz.Guid.ToString & "' ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuPncs ON VwSkuPncs.SkuGuid = Pnc.ArtGuid ")
        If oHolding Is Nothing Then
            sb.AppendLine("WHERE VwCcxOrMe.Ccx = '" & oCcx.Guid.ToString & "' And pn2 > 0 ")
        Else
            sb.AppendLine("INNER JOIN CliClient ON VwCcxOrMe.Ccx = CliClient.Guid  ")
            sb.AppendLine("WHERE CliClient.Holding = '" & oHolding.Guid.ToString & "' And pn2 > 0 ")
        End If
        sb.AppendLine("GROUP BY Pnc.ArtGuid, VwSkuStocks.stock, VwSkuPncs.Clients, VwSkuPncs.ClientsAlPot, VwSkuPncs.ClientsEnProgramacio, VwSkuPncs.ClientsBlockStock ")
        sb.AppendLine(", VwProductNom.Cod, VwProductNom.BrandGuid, VwProductNom.BrandNom, VwProductNom.CategoryGuid, VwProductNom.CategoryNom, VwProductNom.SkuNom, VwProductNom.SkuNomLlarg ")
        sb.AppendLine(", Art.LastProduction, Art.Obsoleto ")
        sb.AppendLine("ORDER BY VwProductNom.SkuNomLlarg")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read

            Dim iStock As Integer = 0
            Dim iPn2 As Integer = 0
            If Not IsDBNull(oDrd("Stock")) Then
                iStock = oDrd("Stock")
            End If
            If Not IsDBNull(oDrd("Pn2")) Then
                iPn2 = oDrd("Pn2")
            End If

            Dim oStock As New DTOStockAvailable()
            With oStock
                .Sku = ProductLoader.NewProduct(CInt(oDrd("ProductCod")), DirectCast(oDrd("BrandGuid"), Guid), oDrd("BrandNom").ToString, oDrd("CategoryGuid"), oDrd("CategoryNom"), oDrd("ArtGuid"), oDrd("SkuNom"), oDrd("SkuNomLlarg"))
                .Sku.LastProduction = oDrd("LastProduction")
                .Sku.obsoleto = oDrd("Obsoleto")
                .OriginalStock = iStock
                .AvailableStock = iStock
                .Clients = iPn2
            End With
            retval.Add(oStock)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function DeletePendingItems(oPurchaseOrders As List(Of DTOPurchaseOrder), exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            DeletePendingQties(oPurchaseOrders, oTrans)
            DeleteEmptyItems(oPurchaseOrders, oTrans)
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

    Shared Sub DeletePendingQties(oPurchaseOrders As List(Of DTOPurchaseOrder), oTrans As SqlTransaction)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("UPDATE Pnc ")
        sb.AppendLine("SET Pnc.Qty=Pnc.Qty-Pnc.Pn2 ")
        sb.AppendLine("WHERE Pnc.Qty<>Pnc.Pn2 AND (")
        For Each order In oPurchaseOrders
            If order.UnEquals(oPurchaseOrders.First) Then sb.Append("OR ")
            sb.AppendLine("Pnc.PdcGuid = '" & order.Guid.ToString & "' ")
        Next
        sb.AppendLine(")")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteEmptyItems(oPurchaseOrders As List(Of DTOPurchaseOrder), oTrans As SqlTransaction)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("DELETE Pnc ")
        sb.AppendLine("WHERE Pnc.Qty=0 AND Pnc.Qty=Pnc.Pn2 AND (")
        For Each order In oPurchaseOrders
            If order.UnEquals(oPurchaseOrders.First) Then sb.Append("OR ")
            sb.AppendLine("PdcGuid = '" & order.Guid.ToString & "' ")
        Next
        sb.AppendLine(")")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Function Delete(exs As List(Of Exception), oGuids As List(Of Guid)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oGuids, oTrans)
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

    Shared Sub Delete(oGuids As List(Of Guid), ByRef oTrans As SqlTransaction)
        ClearEdiResults(oGuids, oTrans)
        DeleteDocfiles(oGuids, oTrans)
        DeleteItems(oGuids, oTrans)
        DeleteHeaders(oGuids, oTrans)
    End Sub

    Shared Sub ClearEdiResults(oGuids As List(Of Guid), ByRef oTrans As SqlTransaction)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	    Guid uniqueidentifier NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Guid) ")

        Dim idx As Integer = 0
        For Each oGuid In oGuids
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("('{0}') ", oGuid.ToString())
            idx += 1
        Next

        Dim sb2 As New Text.StringBuilder
        sb2.AppendLine(sb.ToString())
        sb2.AppendLine("UPDATE EdiversaOrderHeader SET Result=NULL ")
        sb2.AppendLine("FROM EdiversaOrderHeader ")
        sb2.AppendLine("INNER JOIN @Table X ON EdiversaOrderHeader.Result = X.Guid ")
        Dim SQL2 As String = sb2.ToString
        Dim i2 = SQLHelper.ExecuteNonQuery(SQL2, oTrans)

        Dim sb3 As New Text.StringBuilder
        sb3.AppendLine(sb.ToString())
        sb3.AppendLine("UPDATE Edi SET Result=0, ResultGuid = NULL ")
        sb3.AppendLine("FROM Edi ")
        sb3.AppendLine("INNER JOIN @Table X ON Edi.ResultGuid = X.Guid ")
        Dim SQL3 As String = sb3.ToString
        Dim i3 = SQLHelper.ExecuteNonQuery(SQL3, oTrans)
    End Sub

    Shared Sub DeleteItems(oGuids As List(Of Guid), ByRef oTrans As SqlTransaction)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	    Guid uniqueidentifier NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Guid) ")

        Dim idx As Integer = 0
        For Each oGuid In oGuids
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("('{0}') ", oGuid.ToString())
            idx += 1
        Next

        sb.AppendLine()
        sb.AppendLine("DELETE Pnc ")
        sb.AppendLine("FROM Pnc ")
        sb.AppendLine("INNER JOIN @Table X ON Pnc.PdcGuid = X.Guid ")
        Dim SQL As String = sb.ToString
        Dim i = SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteHeaders(oGuids As List(Of Guid), ByRef oTrans As SqlTransaction)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	    Guid uniqueidentifier NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Guid) ")

        Dim idx As Integer = 0
        For Each oGuid In oGuids
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("('{0}') ", oGuid.ToString())
            idx += 1
        Next

        sb.AppendLine()
        sb.AppendLine("DELETE Pdc ")
        sb.AppendLine("FROM Pdc ")
        sb.AppendLine("INNER JOIN @Table X ON Pdc.Guid = X.Guid ")
        Dim SQL As String = sb.ToString
        Dim i = SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteDocfiles(oGuids As List(Of Guid), ByRef oTrans As SqlTransaction)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	    Guid uniqueidentifier NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Guid) ")

        Dim idx As Integer = 0
        For Each oGuid In oGuids
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("('{0}') ", oGuid.ToString())
            idx += 1
        Next

        sb.AppendLine()
        sb.AppendLine("DELETE Docfile ")
        sb.AppendLine("FROM Pdc ")
        sb.AppendLine("INNER JOIN @Table X on pdc.Guid = X.Guid ")
        sb.AppendLine("INNER JOIN Docfile on (Pdc.hash = Docfile.hash or Pdc.EtiquetesTransport = Docfile.hash) ")
        Dim SQL As String = sb.ToString
        Dim i = SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub
End Class


