Imports System.Xml

Public Class Delivery

    Shared Function Find(oGuid As Guid) As DTODelivery
        Dim retval As DTODelivery = DeliveryLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(oDelivery As DTODelivery) As Boolean
        Return DeliveryLoader.Load(oDelivery)
    End Function

    Shared Function FromNum(oEmp As DTOEmp, year As Integer, id As Integer) As DTODelivery
        Return DeliveryLoader.FromNum(oEmp, year, id)
    End Function

    Shared Function Update(oDelivery As DTODelivery, exs As List(Of Exception)) As Boolean
        Dim retval = DeliveryLoader.Update(oDelivery, exs)
        BEBL.ServerCache.NotifyUpdate(Models.ServerCache.Tables.Stocks)
        Return retval
    End Function

    Shared Function Delete(exs As List(Of Exception), oDelivery As DTODelivery) As Boolean
        Dim retval = DeliveryLoader.Delete(oDelivery, exs)
        BEBL.ServerCache.NotifyUpdate(Models.ServerCache.Tables.Stocks)
        Return retval
    End Function

    Shared Function isAllowedToDelete(exs As List(Of Exception), oDelivery As DTODelivery) As Boolean
        Return DeliveryLoader.isAllowedToDelete(exs, oDelivery)
    End Function

    Shared Function CobraPerVisa(oEmp As DTOEmp, ByRef oLog As DTOTpvLog, exs As List(Of Exception)) As Boolean
        Try
            Dim oDelivery As New DTODelivery(oLog.Request.Guid)
            If BEBL.Delivery.Load(oDelivery) Then
                Dim oBancDefault = BEBL.Default.Find(DTODefault.Codis.BancTpv, oEmp)
                If oBancDefault Is Nothing Then
                    exs.Add(New Exception("falta definir el banc per defecte"))
                Else
                    oLog.Request = oDelivery
                    Dim oBanc = BEBL.Banc.Find(New Guid(oBancDefault.Value))
                    oLog.Result.Concept = String.Format("{0} Tpv {1} albará {2} {3}", oBanc.Abr, oLog.Ds_Order, oDelivery.Id, oDelivery.Nom)
                    oDelivery.RetencioCod = DTODelivery.RetencioCods.Free
                    DeliveryLoader.CobraPerVisa(oLog, exs)
                End If
            End If
        Catch ex As Exception
            exs.Add(New Exception("Error en febl.Delivery.CobraPerVisa: " & ex.Message))
        End Try
        Return exs.Count = 0
    End Function

    Shared Function CobraPerTransferenciaPrevia(exs As List(Of Exception), value As DTOCobramentPerTransferencia) As DTOCca
        Dim oCtas = BEBL.PgcCtas.Current()
        Dim retval = DTOCca.Factory(value.Fch, value.User, DTOCca.CcdEnum.CobramentACompte)
        With retval
            .Concept = value.Concepte
            .DocFile = value.DocFile
        End With
        retval.AddDebit(value.Amt, oCtas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.bancs), value.Banc)
        retval.AddSaldo(oCtas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.Clients_Anticips), value.Contact)

        If BEBL.Delivery.Load(value.Delivery) Then
            With value.Delivery
                .RetencioCod = DTODelivery.RetencioCods.free
                .UsrLog.UsrLastEdited = DTOGuidNom.Factory(value.User.Guid)
            End With

            DeliveryLoader.CobraPerTransferenciaPrevia(value.Delivery, retval, exs)
        End If

        Return retval
    End Function

    Shared Function UpdateJustificante(exs As List(Of Exception), oDelivery As DTODelivery, ByVal oJustificanteCode As DTODelivery.JustificanteCodes) As Boolean
        Return DeliveryLoader.UpdateJustificante(oDelivery, oJustificanteCode, exs)
    End Function

    Shared Function Total(oDelivery As DTODelivery) As DTOAmt
        Return DeliveryLoader.Total(oDelivery)
    End Function

    Shared Function SetCurExchangeRate(exs As List(Of Exception), oDelivery As DTODelivery, oCur As DTOCur, oRate As DTOCurExchangeRate) As Boolean
        Return DeliveryLoader.SetCurExchangeRate(oDelivery, oCur, oRate, exs)
    End Function


    Shared Function FromFormattedId(oEmp As DTOEmp, sFormattedId As String) As DTODelivery
        Dim retval As DTODelivery = Nothing
        If sFormattedId.Length = 10 Then
            Dim sYear As String = sFormattedId.Substring(0, 4)
            Dim sId As String = sFormattedId.Substring(4, 6)
            If IsNumeric(sYear) And IsNumeric(sId) Then
                retval = FromNum(oEmp, CInt(sYear), CInt(sId))
            End If
        End If
        Return retval
    End Function

    Shared Function Factory(oEmp As DTOEmp, oCustomer As DTOCustomer, oUser As DTOUser) As DTODelivery
        BEBL.Customer.Load(oCustomer)
        Dim oCcx As DTOCustomer = BEBL.Customer.CcxOrMe(oCustomer)
        Dim retval As New DTODelivery
        With retval
            .Emp = oCustomer.Emp
            .Cod = DTOPurchaseOrder.Codis.Client
            .Customer = oCustomer
            .Nom = oCustomer.NomComercialOrDefault()
            .Mgz = oEmp.Mgz
            .Fch = DTO.GlobalVariables.Today()

            .CashCod = oCcx.CashCod
            .Facturable = True
            .RetencioCod = DTODelivery.RetencioCods.Free
            .Valorado = oCcx.AlbValorat

            .PortsCod = oCcx.portsCod
            .transportista = DTOTransportista.Wellknown(DTOTransportista.Wellknowns.tnt)
            If oCustomer.deliveryPlatform Is Nothing Then
                .address = BEBL.Customer.ShippingAddressOrDefault(oCustomer)
                .tel = oCustomer.telefon
                .exportCod = DTOAddress.ExportCod(.address)
            Else
                ContactLoader.Load(oCustomer.deliveryPlatform)
                .platform = oCustomer.deliveryPlatform
                .address = .platform.address
                .exportCod = DTOAddress.ExportCod(BEBL.Customer.ShippingAddressOrDefault(oCustomer))
                .tel = .platform.telefon
            End If

            .UsrLog = DTOUsrLog2.Factory(oUser)
            .items = New List(Of DTODeliveryItem)
        End With
        Return retval
    End Function

    Shared Function Factory(oEmp As DTOEmp, oSpvOut As DTOSpvOut) As DTODelivery
        BEBL.Customer.Load(oSpvOut.Customer)
        Dim retval As DTODelivery = Factory(oEmp, oSpvOut.Customer, oSpvOut.Usr)
        With retval
            .cod = DTOPurchaseOrder.Codis.reparacio
            .Mgz = DTOMgz.FromContact(oEmp.Taller)
            .fch = oSpvOut.Fch
            .bultos = oSpvOut.Bts
            .kg = oSpvOut.Kgs
            .m3 = oSpvOut.M3

            Dim oUser As DTOUser = oSpvOut.Usr
            .UsrLog = DTOUsrLog2.Factory(oUser)

            If oSpvOut.Recogeran Then
                .portsCod = DTOCustomer.PortsCodes.reculliran
            Else
                .portsCod = DTOCustomer.PortsCodes.pagats
                '.transportista = oTaller.Transportista
            End If

            For Each oSpv As DTOSpv In oSpvOut.Spvs
                If oSpv.garantia Then
                    Dim oSku = DTOProductSku.Wellknown(DTOProductSku.Wellknowns.ManoDeObraSinCargo)
                    Dim item As DTODeliveryItem = DTODeliveryItem.Factory(oSpv, oSku)
                    retval.items.Add(item)
                Else
                    If oSpv.valJob IsNot Nothing AndAlso oSpv.valJob.isNotZero Then
                        Dim oSku = DTOProductSku.Wellknown(DTOProductSku.Wellknowns.ManoDeObra)
                        Dim item As DTODeliveryItem = DTODeliveryItem.Factory(oSpv, oSku, 1, oSpv.valJob)
                        retval.items.Add(item)
                    End If

                    If oSpv.valMaterial IsNot Nothing AndAlso oSpv.valMaterial.isNotZero Then
                        Dim oSku = DTOProductSku.Wellknown(DTOProductSku.Wellknowns.MaterialEmpleado)
                        Dim item = DTODeliveryItem.Factory(oSpv, oSku, 1, oSpv.valMaterial)
                        retval.items.Add(item)
                    End If
                End If

                If oSpv.valEmbalatje IsNot Nothing AndAlso oSpv.valEmbalatje.isNotZero Then
                    Dim oSku = DTOProductSku.Wellknown(DTOProductSku.Wellknowns.Embalaje)
                    Dim item = DTODeliveryItem.Factory(oSpv, oSku, 1, oSpv.valEmbalatje)
                    retval.items.Add(item)
                End If

                If oSpv.valPorts IsNot Nothing AndAlso oSpv.valPorts.isNotZero Then
                    Dim oSku = DTOProductSku.Wellknown(DTOProductSku.Wellknowns.Transport)
                    Dim item = DTODeliveryItem.Factory(oSpv, oSku, 1, oSpv.valPorts)
                    retval.items.Add(item)
                End If
            Next
            .Import = .totalCash()
        End With

        Return retval

    End Function

    Shared Function PdfStream(oDelivery As DTODelivery, proforma As Boolean, exs As List(Of Exception)) As Byte()
        Dim oDeliveries As New List(Of DTODelivery)
        oDeliveries.Add(oDelivery)
        Dim retval = BEBL.Deliveries.PdfStream(exs, oDeliveries, proforma)
        Return retval
    End Function

    Shared Function Doc(oDelivery As DTODelivery, Optional ByVal BlProforma As Boolean = False) As DTODoc
        Dim retval As DTODoc = Nothing
        ContactLoader.Load(oDelivery.contact)

        If oDelivery.cod = DTOPurchaseOrder.Codis.proveidor Then
            retval = GeneralDoc(oDelivery, BlProforma)
        Else
            CustomerLoader.Load(oDelivery.customer)

            Dim oCcx As DTOCustomer = BEBL.Customer.CcxOrMe(oDelivery.customer)
            If oCcx.equals(DTOCustomer.Wellknown(DTOCustomer.Wellknowns.carrefour)) Then
                retval = BEBL.Carrefour.DeliveryDoc(oDelivery)
            Else
                retval = GeneralDoc(oDelivery, BlProforma)
            End If
        End If
        Return retval
    End Function

    Shared Function GeneralDoc(oDelivery As DTODelivery, Optional ByVal BlProforma As Boolean = False) As DTODoc
        Dim oOrder As New DTOPurchaseOrder
        Dim oSpv As New DTOSpv
        Dim itm As DTODeliveryItem = Nothing
        Dim oLang = oDelivery.contact.lang

        Dim oEstilo As DTODoc.Estilos = DTODoc.Estilos.albara
        If oDelivery.exportCod() = DTOInvoice.ExportCods.extracomunitari Then BlProforma = True
        If BlProforma Then oEstilo = DTODoc.Estilos.proforma

        'If mAmt Is Nothing Then mAmt = DTOAmt.Empty
        Dim sAdr As String = Nothing
        Dim oZip As DTOZip = Nothing

        Dim oDoc As New DTODoc(oEstilo, oDelivery.contact.lang, DTOApp.Current.Cur)
        With oDoc

            .valorat = oDelivery.valorado
            .incoterm = oDelivery.incoterm()

            Dim oCcx As DTOCustomer = Nothing
            If oDelivery.cod <> DTOPurchaseOrder.Codis.proveidor Then
                oCcx = BEBL.Customer.CcxOrMe(oDelivery.customer)
                .recarrecEquivalencia = oCcx.req
            End If

            If BlProforma And Not oDelivery.IsConsumer Then
                Dim sNom As String = oCcx.Nom
                .dest.Add(sNom)

                If oDelivery.Contact.NomComercial > "" Then
                    .dest.Add(oDelivery.Customer.NomComercial)
                End If

                Dim sNIF As String = oCcx.PrimaryNifValue()
                If sNIF > "" Then
                    .dest.Add("NIF: " & sNIF)
                    .estilo = DTODoc.Estilos.proforma
                End If
                'If mNom <> sNom Then .Dest.Add(mNom)
                sAdr = oCcx.Address.Text
                oZip = oCcx.Address.Zip
            Else
                .dest.Add(oDelivery.nom)
                sAdr = oDelivery.Address.Text
                oZip = oDelivery.address.Zip
            End If


            If Not String.IsNullOrEmpty(sAdr) Then
                For Each sl As String In sAdr.Split(vbCrLf)
                    If sl > "" Then .dest.Add(sl.Trim)
                Next
            End If

            If oZip IsNot Nothing Then
                .dest.Add(oZip.FullNom(oLang))
            End If

            If oDelivery.ObsTransp > "" Then
                .dest.Add("")
                .dest.Add("Entregas: " & oDelivery.ObsTransp)
            End If

            .fch = oDelivery.fch
            .num = oDelivery.id
            '.Dto = oDelivery.DtoPct
            '.Dpp = oDelivery.DppPct

            If oDelivery.ivaExempt Then
                .ivaBaseQuotas = New List(Of DTOTaxBaseQuota)
            Else
                .ivaBaseQuotas = oDelivery.taxBaseQuotas()
            End If

            Select Case oDelivery.cashCod
                Case DTOCustomer.CashCodes.reembols
                    .obs.Add(.lang.Tradueix("contra reembolso de ", "contra reembols de ", "cash against goods for ") & DTOAmt.CurFormatted(oDelivery.totalCash()))
                Case DTOCustomer.CashCodes.transferenciaPrevia
                    '.obs.Add(.lang.Tradueix("pagado", "pagat", "paid in advance")) 'DO NOT display it since the pdf is used to have the customer pay it
            End Select

            Dim s As String = .lang.Tradueix("forma de envío: ", "enviament: ", "shipment: ")
            Select Case oDelivery.portsCod
                Case DTOCustomer.PortsCodes.reculliran
                    .obs.Add(.lang.Tradueix("dirección de recogida:", "adreça de recollida:", "pick up address:"))
                    Dim oMgz As DTOMgz = oDelivery.mgz
                    ContactLoader.Load(oMgz)
                    .obs.Add(oMgz.nomComercialOrDefault())
                    .obs.Add(oMgz.Address.Text)
                    .obs.Add(oMgz.Address.Zip.Location.FullNom(oLang))
                Case DTOCustomer.PortsCodes.deguts
                    .obs.Add(s & .lang.Tradueix("portes debidos", "ports deguts", "portes debidos"))
                Case DTOCustomer.PortsCodes.pagats
                    .obs.Add(s & .lang.Tradueix("portes pagados", "ports pagats", "paid ports"))
                    If oDelivery.platform IsNot Nothing Then
                        ContactLoader.Load(oDelivery.platform)
                        .obs.Add("entrega en " & oDelivery.platform.Nom)
                        .obs.Add(oDelivery.platform.Address.Text)
                        .obs.Add(oDelivery.platform.Address.Zip.FullNom(oLang))
                    End If
            End Select

            Dim BlMostrarEANenFactura As Boolean = oDelivery.cod <> DTOPurchaseOrder.Codis.proveidor AndAlso oDelivery.customer.mostrarEANenFactura
            If oDelivery.items IsNot Nothing Then
                For Each itm In oDelivery.items

                    Select Case oDelivery.cod
                        Case DTOPurchaseOrder.Codis.reparacio
                            If Not oSpv.equals(itm.spv) Then
                                Dim i As Integer
                                oSpv = itm.spv
                                Dim oSpvTextArray As List(Of String) = oSpv.lines(oDelivery.customer.lang) ' oSpv.TextArray
                                Dim iSpvTextLines As Integer = oSpvTextArray.Count
                                .itms.Add(New DTODocItm(, , , , , , , iSpvTextLines))
                                For i = 0 To iSpvTextLines - 1
                                    .itms.Add(New DTODocItm(oSpvTextArray(i), DTODoc.FontStyles.italic, , , , , , iSpvTextLines - i + 2))
                                Next
                            End If
                        Case DTOPurchaseOrder.Codis.client, DTOPurchaseOrder.Codis.proveidor
                            If itm.purchaseOrderItem.purchaseOrder.UnEquals(oOrder) Then
                                oOrder = itm.purchaseOrderItem.purchaseOrder
                                .itms.Add(New DTODocItm(, , , , , , , 4))
                                .itms.Add(New DTODocItm(DocPdcText(oDelivery, oOrder), DTODoc.FontStyles.italic, , , , , , 2))
                            End If
                    End Select

                    If oDelivery.cod = DTOPurchaseOrder.Codis.client Then
                        If oDelivery.customer.equals(DTOCustomer.Wellknown(DTOCustomer.Wellknowns.carrefour)) Then
                            .itms.Add(New DTODocItm(, , , , , , , 4))
                        End If
                    End If

                    Dim oSku As DTOProductSku = itm.sku
                    Dim sRef As String = oSku.id.ToString
                    If BlMostrarEANenFactura Then
                        Dim sEAN As String = "0000000000000"
                        Dim oEan As DTOEan = oSku.ean13
                        If oEan IsNot Nothing Then sEAN = oEan.Value
                        sRef = sEAN
                    End If

                    Dim sSkuNom As String = oSku.RefYNomLlarg.Tradueix(oDelivery.Contact.Lang)
                    If oDelivery.cod = DTOPurchaseOrder.Codis.client AndAlso oDelivery.customer.lang.Equals(DTOLang.ENG) Then
                        sSkuNom = oSku.refYNomPrv()
                    End If

                    If (oDelivery.valorado Or BlProforma) Then
                        .itms.Add(New DTODocItm(sSkuNom, DTODoc.FontStyles.regular, itm.qty, itm.price, itm.dto, 0, 2, , sRef))
                    Else
                        .itms.Add(New DTODocItm(sSkuNom, DTODoc.FontStyles.regular, itm.qty, , , 0, 2, , sRef))
                    End If

                    If oDelivery.cod = DTOPurchaseOrder.Codis.client Then
                        If oDelivery.customer.equals(DTOCustomer.Wellknown(DTOCustomer.Wellknowns.carrefour)) Then
                            Dim BoxUnits As Integer = oSku.innerPackOrInherited
                            Dim Boxes As Integer = itm.qty / BoxUnits
                            Dim oDocitm As DTODocItm = .itms.Last
                            With oDocitm
                                .Boxes = Boxes
                                .m3 = itm.qty * oSku.volumeM3OrInherited
                                .Kg = itm.qty * oSku.weightKgOrInherited
                            End With
                            .itms.Add(New DTODocItm(String.Format("Dun 14: {0}", DTOCustomerProduct.Dun14OrDefault(oSku.customerProduct)),,,,,, 8))
                            .itms.Add(New DTODocItm(String.Format("unidades/caja: {0}", BoxUnits),,,,,, 8))
                            .itms.Add(New DTODocItm(String.Format("total cajas: {0}", Boxes),,,,,, 8))
                            .itms.Add(New DTODocItm(String.Format("volumen caja: {0} m3", oSku.volumeM3OrInherited),,,,,, 8))
                            .itms.Add(New DTODocItm(String.Format("peso bruto caja: {0} Kg", oSku.weightKgOrInherited()),,,,,, 8))
                            .itms.Add(New DTODocItm(, , , , , , , 4))
                        End If
                    End If

                    If itm.bundle.Count > 0 Then
                        .Itms.Add(New DTODocItm(.Lang.Tradueix("compuesto de los siguientes elementos:", "compost dels següents elements", "composed of the following elements:"),,,,,, 6))
                        For Each oChildItem In itm.Bundle
                            sSkuNom = oChildItem.Sku.RefYNomLlarg.Tradueix(oDelivery.Customer.Lang)
                            If oDelivery.cod = DTOPurchaseOrder.Codis.client AndAlso oDelivery.customer.lang.Equals(DTOLang.ENG) Then
                                sSkuNom = oChildItem.sku.RefYNomPrv()
                            End If
                            Dim oDocItm = New DTODocItm(sSkuNom,  , , , , , , 4, oChildItem.sku.id.ToString())
                            oDocItm.LeftPadChars = 12
                            .Itms.Add(oDocItm)
                        Next
                    End If
                Next

            End If
        End With
        Return oDoc
    End Function


    Shared Function DocPdcText(oDelivery As DTODelivery, ByVal oOrder As DTOPurchaseOrder) As String
        Dim sRetVal As String = ""

        Dim oLang As DTOLang
        oLang = oDelivery.Contact.lang
        Select Case oDelivery.cod
            Case DTOPurchaseOrder.Codis.client, DTOPurchaseOrder.Codis.reparacio
                sRetVal = oLang.Tradueix("Su pedido", "La seva comanda", "Your order")
                If oOrder.concept > "" Then
                    sRetVal += " " & oOrder.concept.Trim
                End If
            Case DTOPurchaseOrder.Codis.proveidor
                sRetVal = oLang.Tradueix("Nuestro pedido", "La nostre comanda", "Our order") & " " & oOrder.num
            Case Else
                sRetVal = ""
        End Select

        If sRetVal > "" Then
            Dim StFrom As String = oLang.Tradueix("de fecha", "de data", "from")
            sRetVal += " " & StFrom & " " & Format(oOrder.fch, "dd/MM/yy")
        End If
        Return sRetVal
    End Function


    Shared Function DOM(oDelivery As DTODelivery) As XmlDocument

        If oDelivery.Platform IsNot Nothing Then
            ContactLoader.Load(oDelivery.Platform)
        End If

        Dim retval As New XmlDocument

        Dim Doc As XmlElement = Nothing
        Dim NodeDest As XmlElement = Nothing
        Dim NodePdcs As XmlElement = Nothing
        Dim NodePdc As XmlElement = Nothing
        Dim NodePdcItms As XmlElement = Nothing
        Dim NodeItm As XmlElement = Nothing
        Dim mItem As DTODeliveryItem = Nothing
        Dim oPurchaseOrder As DTOPurchaseOrder = Nothing

        retval = New XmlDocument
        Doc = retval.CreateElement("DOCUMENTO")
        Doc.SetAttribute("TIPO", "ALBARAN")
        Doc.SetAttribute("VERSION", "2.1")
        Doc.SetAttribute("FECHA", DTO.GlobalVariables.Now())
        retval.AppendChild(Doc)

        NodeDest = retval.CreateElement("DESTINO")
        Doc.AppendChild(NodeDest)

        '=================================================

        Dim NodeDestNom = retval.CreateElement("NOMBRE")
        If oDelivery.Platform Is Nothing Then
            NodeDestNom.InnerText = oDelivery.Nom
        Else
            NodeDestNom.InnerText = oDelivery.Platform.NomComercialOrDefault()
        End If

        'Nou a la versió 2.2:
        If oDelivery.Contact.GLN IsNot Nothing Then
            NodeDestNom.SetAttribute("NADBY", oDelivery.Contact.GLN.Value)
            If oDelivery.Platform Is Nothing Then
                NodeDestNom.SetAttribute("NADDP", DTOEan.eanValue(oDelivery.Contact.GLN))
            Else
                NodeDestNom.SetAttribute("NADDP", DTOEan.eanValue(oDelivery.Platform.GLN))
            End If
        End If

        NodeDest.AppendChild(NodeDestNom)

        '=================================================


        NodeItm = retval.CreateElement("DIRECCION")
        NodeItm.InnerText = DeliveryAdr(oDelivery)
        NodeDest.AppendChild(NodeItm)

        NodeItm = retval.CreateElement("POBLACION")

        NodeItm.InnerText = DeliveryZip(oDelivery)?.Location?.Nom


        Select Case oDelivery.PortsCod
            Case DTOCustomer.PortsCodes.altres
            Case DTOCustomer.PortsCodes.reculliran
                NodeItm.SetAttribute("ZIP", "08430")
                NodeItm.SetAttribute("PAIS", "ES")
            Case Else
                NodeItm.SetAttribute("ZIP", DeliveryZip(oDelivery)?.ZipCod)
                NodeItm.SetAttribute("PAIS", DeliveryZip(oDelivery)?.Location.Zona.Country.ISO)
        End Select


        NodeDest.AppendChild(NodeItm)

        Dim oCustomer As DTOCustomer = oDelivery.Customer
        Dim oCcx As DTOCustomer = BEBL.Customer.CcxOrMe(oCustomer)
        If oCcx.isElCorteIngles Then
            ContactLoader.Load(oDelivery.Customer)
            Dim sCentro As String = ""
            Dim sDepartamento As String = ""
            Dim sPedido As String = ""
            Dim sNumProveedor As String = ""
            Dim sPlatformGln As String = ""

            If oDelivery.Items.Count > 0 Then
                oPurchaseOrder = oDelivery.Items(0).PurchaseOrderItem.PurchaseOrder
                BEBL.ElCorteIngles.GetDetailsFromPdc(oPurchaseOrder, sPedido, sCentro, sDepartamento, sNumProveedor)
                NodeItm = retval.CreateElement("ELCORTEINGLES")
                NodeItm.SetAttribute("CENTRODESTINO", sCentro)
                NodeItm.SetAttribute("DEPARTAMENTODESTINO", sDepartamento)
                NodeItm.SetAttribute("PEDIDO", sPedido)
                NodeItm.SetAttribute("NUMEROPROVEEDOR", sNumProveedor)
                If oDelivery.Platform Is Nothing OrElse oDelivery.Platform.GLN Is Nothing Then
                    If oDelivery.Customer.GLN IsNot Nothing Then
                        NodeItm.SetAttribute("PUNTOOPERACIONALENTREGA", oDelivery.Customer.GLN.Value)
                    End If
                Else
                    NodeItm.SetAttribute("PUNTOOPERACIONALENTREGA", oDelivery.Platform.GLN.Value)
                End If
                If oDelivery.Customer.GLN IsNot Nothing Then
                    NodeItm.SetAttribute("PUNTOOPERACIONALCENTRO", oDelivery.Customer.GLN.Value)
                End If
                If Not String.IsNullOrEmpty(oPurchaseOrder.NADMS) Then
                    'added on 20/12/22 to make sure DESADV is sent to its ORDER message sender
                    NodeItm.SetAttribute("NADMR", oPurchaseOrder.NADMS)
                End If
            End If

            NodeDest.AppendChild(NodeItm)
        End If


        NodeItm = retval.CreateElement("TELEFONO")
        NodeItm.InnerText = oDelivery.Tel
        NodeDest.AppendChild(NodeItm)

        NodeItm = retval.CreateElement("OBSERVACIONES")
        NodeItm.InnerText = oDelivery.ObsTransp 'mObs son observacions internes '=============================================================
        NodeDest.AppendChild(NodeItm)

        'Nou a versió 2.0 ===============================================================
        Dim NodeTax As XmlElement = retval.CreateElement("TAX")
        NodeDest.AppendChild(NodeTax)

        If Not oDelivery.IvaExempt Then

            If oCcx.Iva Then
                NodeTax.SetAttribute("IVA", DTOTax.closest(DTOTax.Codis.iva_Standard).tipus)

                If oCcx.Req Then
                    NodeTax.SetAttribute("RecEq", DTOTax.closest(DTOTax.Codis.recarrec_Equivalencia_Standard).tipus)
                End If
            End If
        End If


        Dim oNodeTrp As XmlElement = Nothing
        oNodeTrp = retval.CreateElement("TRANSPORTE")
        Doc.AppendChild(oNodeTrp)

        Dim oNodeRecogeran As XmlElement = Nothing
        oNodeRecogeran = retval.CreateElement("RECOGERAN")
        Select Case oDelivery.PortsCod
            Case DTOCustomer.PortsCodes.reculliran, DTOCustomer.PortsCodes.altres
                oNodeRecogeran.InnerText = "SI"
            Case Else
                oNodeRecogeran.InnerText = "NO"
        End Select
        oNodeTrp.AppendChild(oNodeRecogeran)

        If oDelivery.CashCod = DTOCustomer.CashCodes.reembols Then
            Dim oNodeCash As XmlElement
            oNodeCash = retval.CreateElement("REEMBOLSO")
            oNodeCash.InnerText = oDelivery.Import.Val + oDelivery.Recarrec.Val
            oNodeCash.SetAttribute("GASTOS", "PAGADOS")
            oNodeTrp.AppendChild(oNodeCash)
        End If

        Select Case oDelivery.PortsCod
            Case DTOCustomer.PortsCodes.reculliran, DTOCustomer.PortsCodes.altres
            Case Else
                Dim oNodeTransportista As XmlElement
                oNodeTransportista = retval.CreateElement("TRANSPORTISTA")
                Dim sTrpNIF As String = ""
                Dim sTrpNom As String = ""
                Dim oZona As DTOZona = oDelivery.Address.Zip?.Location?.Zona
                oNodeTransportista.SetAttribute("NIF", sTrpNIF)
                oNodeTransportista.InnerText = sTrpNom
                oNodeTrp.AppendChild(oNodeTransportista)

                Dim oNodeServicio As XmlElement
                oNodeServicio = retval.CreateElement("SERVICIO")
                oNodeServicio.InnerText = "STANDARD"
                Dim sServCod As String = ""
                Select Case oDelivery.PortsCod
                    Case DTOCustomer.PortsCodes.deguts
                        sServCod = "DEBIDOS"
                    Case DTOCustomer.PortsCodes.pagats
                        sServCod = "PAGADOS"
                End Select
                oNodeServicio.SetAttribute("PORTES", sServCod)
                oNodeTrp.AppendChild(oNodeServicio)

                Dim oNodeFactTransp As XmlElement
                oNodeFactTransp = retval.CreateElement("FACTURACION")
                oNodeFactTransp.SetAttribute("KG", oDelivery.Kg)
                oNodeFactTransp.SetAttribute("VOLUMEN", oDelivery.M3)
                oNodeTrp.AppendChild(oNodeFactTransp)

        End Select

        Dim oNodeLins As XmlElement = retval.CreateElement("LINEAS")
        Dim oNode As XmlElement = Nothing
        oPurchaseOrder = New DTOPurchaseOrder
        Doc.AppendChild(oNodeLins)
        For Each oItem As DTODeliveryItem In oDelivery.Items
            oNode = DOMLinNode(retval, oDelivery, oItem)
            oNodeLins.AppendChild(oNode)

            'If oItem.bundle.Count = 0 Then
            'oNode = DOMLinNode(retval, oDelivery, oItem)
            'oNodeLins.AppendChild(oNode)
            'Else
            'For Each oChildItem In oItem.bundle
            'oNode = DOMLinNode(retval, oDelivery, oChildItem)
            'oNodeLins.AppendChild(oNode)
            'Next
            'End If

        Next
        Return retval
    End Function

    Shared Function DOMLinNode(oDoc As XmlDocument, oDelivery As DTODelivery, oItem As DTODeliveryItem) As XmlElement
        Dim retval As XmlElement = oDoc.CreateElement("LINEA")
        retval.SetAttribute("ID", oItem.Lin)
        retval.SetAttribute("CANTIDAD", oItem.Qty)
        retval.SetAttribute("REFERENCIA", oItem.PurchaseOrderItem.Sku.Id)
        retval.SetAttribute("DESCRIPCION", oItem.PurchaseOrderItem.Sku.RefYNomLlarg.Tradueix(oDelivery.Customer.Lang))

        If oItem.PurchaseOrderItem.Sku.Ean13 IsNot Nothing Then
            retval.SetAttribute("EAN", oItem.PurchaseOrderItem.Sku.Ean13.Value)
        End If

        If oDelivery.Valorado Then
            If oItem.Price IsNot Nothing Then
                If oItem.Price.Eur <> 0 Then
                    retval.SetAttribute("PRECIO", Format(oItem.Price.Eur, "#,##0.00;-#,##0.00;#"))
                End If
            End If
            If oItem.Dto <> 0 Then
                retval.SetAttribute("DESCUENTO", oItem.Dto & "%")
            End If
        End If

        If oItem.PurchaseOrderItem.Sku.NoStk Then
            retval.SetAttribute("PICKING", "NO")
        End If

        retval.SetAttribute("PEDIDO", oItem.PurchaseOrderItem.PurchaseOrder.caption(oDelivery.Customer.Lang))
        Return retval
    End Function

    Shared Function DeliveryNom(oDelivery As DTODelivery) As String
        Dim retVal As String = ""

        'If oDelivery.Id = 12478 Then Stop

        If oDelivery.Platform Is Nothing Then
            retVal = oDelivery.Nom
        Else
            ContactLoader.Load(oDelivery.Platform)
            retVal = oDelivery.Platform.NomComercialOrDefault()
        End If
        Return retVal
    End Function

    Shared Function DeliveryAdr(oDelivery As DTODelivery) As String
        Dim retVal As String = ""
        If oDelivery.Platform Is Nothing Then
            retVal = oDelivery.Address.Text
        Else
            retVal = oDelivery.Platform.Address.Text
        End If
        If retVal > "" Then
            retVal = retVal.Replace(vbCrLf, " - ")
        End If
        Return retVal
    End Function

    Shared Function DeliveryZip(oDelivery As DTODelivery) As DTOZip
        Dim retVal As DTOZip = Nothing
        If oDelivery.Platform Is Nothing Then
            retVal = oDelivery.Address.Zip
        Else
            retVal = oDelivery.Platform.Address.Zip
        End If
        Return retVal
    End Function

    Shared Function DeliveryLocationNom(oDelivery As DTODelivery) As String
        Dim retVal As String = ""
        Dim oAddress As DTOAddress = Nothing
        If oDelivery.Platform Is Nothing Then
            oAddress = oDelivery.Address
        Else
            oAddress = oDelivery.Platform.Address
        End If
        If oAddress IsNot Nothing Then
            If oAddress.Zip IsNot Nothing Then
                If oAddress.Zip.Location IsNot Nothing Then
                    retVal = oAddress.Zip.Location.Nom
                End If
            End If
        End If
        Return retVal
    End Function


    Shared Function DeliveryGLN(oDelivery As DTODelivery) As DTOEan
        Dim retVal As DTOEan = Nothing
        If oDelivery.Platform Is Nothing Then
            retVal = oDelivery.Customer.GLN
        Else
            retVal = oDelivery.Platform.GLN
        End If
        Return retVal
    End Function

    Shared Function PickUpFchFrom(oDelivery As DTODelivery) As DateTimeOffset
        'Ha passat en transmisió?
        DeliveryLoader.Load(oDelivery)
        Dim DtFchTransm As DateTimeOffset
        If oDelivery.Transmisio Is Nothing Then
            Dim oTask As DTOTask = TaskLoader.Find(DTOTask.Cods.VivaceTransmisio)
            DtFchTransm = oTask.NextRun
        Else
            DtFchTransm = oDelivery.Transmisio.Fch
        End If

        Dim retval As DateTimeOffset
        Select Case DtFchTransm.DayOfWeek
            Case DayOfWeek.Thursday, DayOfWeek.Friday
                retval = DtFchTransm.AddDays(4)
            Case Else
                retval = DtFchTransm.AddDays(2)
        End Select
        Return retval
    End Function

    Shared Function DeliveryTerms(oDelivery As DTODelivery) As String
        Dim retval As String = ""
        Select Case oDelivery.PortsCod
            Case DTOCustomer.PortsCodes.Altres
                retval = "(altres)"
            Case DTOCustomer.PortsCodes.EntregatEnMa
                retval = "(entregat en ma)"
            Case DTOCustomer.PortsCodes.Pagats
                Dim oTransportista As DTOTransportista = oDelivery.Transportista
                If oTransportista Is Nothing Then
                    retval = "(pagats)"
                Else
                    retval = oTransportista.Abr
                End If
            Case DTOCustomer.PortsCodes.Reculliran
                retval = "(reculliran)"
        End Select
        Return retval
    End Function

    Shared Function LoadCustSkuRefs(ByRef oDelivery As DTODelivery, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = True
        DeliveryLoader.Load(oDelivery)
        Dim oCustomer As DTOCustomer = BEBL.Customer.CcxOrMe(oDelivery.Customer)
        Dim oCustomerProducts As List(Of DTOCustomerProduct) = CustomerProductsLoader.All(oCustomer)
        For Each item As DTODeliveryItem In oDelivery.Items
            Dim oCustomerProduct As DTOCustomerProduct = oCustomerProducts.FirstOrDefault(Function(x) x.Sku.Equals(item.Sku))
            If oCustomerProduct Is Nothing Then
                exs.Add(New Exception("falta ref.client de " & item.Sku.RefYNomLlarg.Tradueix(oDelivery.Customer.Lang)))
                retval = False
            Else
                item.Sku.RefCustomer = oCustomerProduct.Ref 'TO DEPRECATE
                item.Sku.CustomerProduct = oCustomerProduct
            End If
        Next
        Return retval
    End Function

    Shared Function LoadMadeIns(ByRef oDelivery As DTODelivery, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = True
        Dim oCountries As List(Of DTOCountry) = CountriesLoader.All(DTOLang.ENG)
        For Each item As DTODeliveryItem In oDelivery.Items
            Dim oSku As DTOProductSku = item.Sku
            Dim oCountry As DTOCountry = DTOProductSku.MadeInOrInherited(oSku)
            If oCountry IsNot Nothing Then
                oSku.MadeIn = oCountries.FirstOrDefault(Function(x) x.Equals(oCountry))
            End If
        Next
        Return retval
    End Function

    Shared Async Function MailToSubscriptors(exs As List(Of Exception), oEmp As DTOEmp, oDelivery As DTODelivery) As Task(Of Boolean)
        Dim oTo As New System.Net.Mail.MailAddressCollection
        Dim oSsc = DTOSubscription.Wellknown(DTOSubscription.Wellknowns.ConfirmacioEnviament)
        Dim oSubscriptors As List(Of DTOSubscriptor) = BEBL.Subscriptors.All(oSsc, oDelivery.customer)

        If oSubscriptors.Count > 0 Then
            Dim sRecipients = oSubscriptors.Select(Function(x) x.EmailAddress).ToList

            Dim oPdfStream = BEBL.Delivery.PdfStream(oDelivery, False, exs)
            If exs.Count = 0 Then
                Dim sSubject As String = ""
                Dim sBody As String = ""
                Dim sEsp As String = ""
                Dim sCat As String = ""
                Dim sEng As String = ""

                Select Case oDelivery.portsCod
                    Case DTOCustomer.PortsCodes.Pagats
                        sSubject = "ALB." & oDelivery.Id & ": " & oDelivery.Customer.Lang.Tradueix("AVISO DE MERCANCIA LISTA PARA SU EXPEDICION", "AVIS DE MERCADERIA LLESTA PER LA SEVA EXPEDICIO", "SHIPMENT READY TO LEAVE")
                        sEsp = "Confirmamos que se han dado instrucciones a nuestros almacenes para la salida de mercancía según albarán adjunto"
                        sCat = "Confirmamem que s'han donat instruccions als nostres magatzems per la sortida de mercadería segons albará adjunt"
                        sEng = "We are glad to confirm that we have given the proper instructions to ship the goods as per packing list attached"
                        sBody = oDelivery.Customer.Lang.Tradueix(sEsp, sCat, sEng)
                    Case DTOCustomer.PortsCodes.Reculliran
                        sSubject = "ALB." & oDelivery.Id & ": " & oDelivery.Customer.Lang.Tradueix("AVISO DE MERCANCIA LISTA PARA SU RECOGIDA", "AVIS DE MERCADERIA LLESTA PER LA SEVA RECULLIDA", "SHIPMENT READY FOR PICK UP")
                        sEsp = "Confirmamos que se han dado instrucciones para la salida de la mercancía según albarán adjunto, que estará a su disposición en nuestros almacenes en el plazo habitual."
                        sCat = "Confirmamem que s'han donat instruccions per la sortida de la mercadería adjunta, que estará a la seva disposició als nostres magatzems en el plaç habitual."
                        sEng = "We are glad to confirm that we have given the proper instructions to ship the goods as per document attached, which you may collect on our warehouse after its usual preparation time."
                        sBody = oDelivery.Customer.Lang.Tradueix(sEsp, sCat, sEng)
                    Case DTOCustomer.PortsCodes.Altres
                        Return True
                        Exit Function
                End Select

                Dim oMailMessage = DTOMailMessage.Factory(sRecipients, sSubject, oDelivery.Contact.Lang.Tradueix(sEsp, sCat, sEng))
                oMailMessage.AddAttachment(DTODelivery.FileName(oDelivery), oPdfStream)
                Await MailMessageHelper.Send(oEmp, oMailMessage, exs)
            End If
        End If
        Dim retVal As Boolean = exs.Count = 0
        Return retVal
    End Function

    Shared Function Url(oDelivery As DTODelivery, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = ""
        If oDelivery IsNot Nothing Then
            retval = DTOWebDomain.Default(AbsoluteUrl).Url("albaran", oDelivery.Guid.ToString())
        End If
        Return retval
    End Function
End Class

Public Class Deliveries


    Shared Function Years(oEmp As DTOEmp, Optional oContact As DTOContact = Nothing) As List(Of Integer)
        Return DeliveriesLoader.Years(oEmp, oContact)
    End Function

    Shared Function Headers(oEmp As DTOEmp,
                            Optional user As DTOUser = Nothing,
                            Optional contact As DTOContact = Nothing,
                            Optional group As Boolean = False,
                            Optional codis As List(Of DTOPurchaseOrder.Codis) = Nothing,
                            Optional year As Integer = 0,
                            Optional pendentsDeCobro As DTODelivery.RetencioCods = DTODelivery.RetencioCods.NotSet,
                            Optional altresPorts As Boolean = False
                            ) As List(Of DTODeliveryHeader)

        Dim retval As List(Of DTODeliveryHeader) = DeliveriesLoader.Headers(oEmp, user, contact, group, codis, year, pendentsDeCobro, altresPorts)
        Return retval
    End Function

    Shared Function All(oCcx As DTOCustomer) As List(Of DTODelivery)
        Dim retval As List(Of DTODelivery) = DeliveriesLoader.All(oCcx)
        Return retval
    End Function

    Shared Function All(oTransmisio As DTOTransmisio) As List(Of DTODelivery)
        Dim oTransmisions As New List(Of DTOTransmisio)
        oTransmisions.Add(oTransmisio)
        Dim retval As List(Of DTODelivery) = DeliveriesLoader.All(oTransmisions)
        Return retval
    End Function

    Shared Function All(oExercici As DTOExercici) As List(Of DTODelivery)
        Dim retval As List(Of DTODelivery) = DeliveriesLoader.All(oExercici)
        Return retval
    End Function

    Shared Function All(oInvoice As DTOInvoice) As List(Of DTODelivery)
        Dim retval As List(Of DTODelivery) = DeliveriesLoader.All(oInvoice)
        Return retval
    End Function

    Shared Function Headers(user As DTOUser) As List(Of DTODelivery.Compact)
        Dim retval = DeliveriesLoader.Headers(user, Nothing)
        Return retval
    End Function

    Shared Function Headers(customer As DTOCustomer) As List(Of DTODelivery.Compact)
        Dim retval = DeliveriesLoader.Headers(customer:=customer)
        Return retval
    End Function

    Shared Function Headers(emp As DTOEmp, year As Integer) As List(Of DTODelivery.Compact) 'TO DEPRECATE
        Dim retval = DeliveriesLoader.Headers(emp:=emp, year:=year)
        Return retval
    End Function

    Shared Function Minified(emp As DTOEmp, year As Integer) As List(Of DTODelivery)
        Dim retval = DeliveriesLoader.Minified(emp:=emp, year:=year)
        Return retval
    End Function


    Shared Function All(oEmp As DTOEmp, oArea As DTOArea) As List(Of DTODelivery)
        Return DeliveriesLoader.All(oEmp, oArea)
    End Function

    Shared Function PendentsDeTransmetre(oMgz As DTOMgz) As List(Of DTODelivery)
        Dim retval As List(Of DTODelivery) = DeliveriesLoader.PendentsDeTransmetre(oMgz)
        Return retval
    End Function

    Shared Function PendentsDeFacturar(oEmp As DTOEmp, Optional oCustomer As DTOCustomer = Nothing) As List(Of DTODelivery)
        Dim retval As List(Of DTODelivery) = DeliveriesLoader.pendentsDeFacturar(oEmp, oCustomer)
        Return retval
    End Function

    Shared Function IntrastatPending(oEmp As DTOEmp, oYearMonth As DTOYearMonth) As List(Of DTODelivery)
        Dim retval As List(Of DTODelivery) = DeliveriesLoader.IntrastatPending(oEmp, oYearMonth)
        Return retval
    End Function

    Shared Function Entrades(oProveidor As DTOProveidor) As List(Of DTODelivery)
        Return DeliveriesLoader.Entrades(oProveidor)
    End Function

    Shared Function Last(oContact As DTOContact) As DTODelivery
        Return DeliveriesLoader.Last(oContact)
    End Function

    Shared Function Centros(oUser As DTOUser) As List(Of DTOCustomer)
        Return DeliveriesLoader.Centros(oUser)
    End Function

    Shared Function Update(exs As List(Of Exception), oDeliveries As List(Of DTODelivery)) As Boolean
        Return DeliveriesLoader.Update(oDeliveries, exs)
        BEBL.ServerCache.NotifyUpdate(Models.ServerCache.Tables.Stocks)
    End Function


    Shared Function reZip(exs As List(Of Exception), oZipTo As DTOZip, oDeliveries As List(Of DTODelivery)) As Integer
        Return DeliveriesLoader.reZip(exs, oZipTo, oDeliveries)
    End Function




    Shared Function NumsToRecycle(oEmp As DTOEmp, DtFch As Date) As List(Of Integer)
        Return DeliveriesLoader.NumsToRecycle(oEmp, DtFch)
    End Function

    Shared Function SortByAddress(src As List(Of DTODelivery)) As List(Of DTODelivery)
        Dim retval As List(Of DTODelivery) = src.
            OrderBy(Function(x) x.Nom).
            OrderBy(Function(x) x.Address.Zip.Location.Nom).
            OrderBy(Function(x) x.Address.Zip.Location.Zona.Nom).
            OrderBy(Function(x) DTOArea.Country(x.Address.Zip).ISO).
            ToList
        Return retval
    End Function

    Shared Function PdfStream(exs As List(Of Exception), oDeliveries As List(Of DTODelivery), proforma As Boolean, Optional CodValorat As DTODelivery.CodsValorat = DTODelivery.CodsValorat.Inherit) As Byte()
        Dim oPdf As New BEBL.PdfDelivery(oDeliveries, proforma)
        oPdf.Print(CodValorat, exs)
        Dim retval = oPdf.Stream(exs)
        Return retval
    End Function


    Shared Function ExcelHistoric(oCcx As DTOCustomer) As MatHelper.Excel.Sheet
        BEBL.Contact.Load(oCcx)
        Dim oDeliveries = All(oCcx)
        Dim displayStore As Boolean = oDeliveries.Any(Function(x) x.Customer.Ref <> "")
        Dim displayDto As Boolean = oDeliveries.SelectMany(Function(x) x.Items).Any(Function(y) y.Dto <> 0)
        Dim sfilename As String = oCcx.Lang.Tradueix("M+O historico 3 años", "M+O historic 3 anys", "M+O last 3 years detail")
        Dim retval As New MatHelper.Excel.Sheet(oCcx.Nom, sfilename)
        With retval
            .AddColumn(oCcx.Lang.Tradueix("albarán", "albarà", "delivery"), MatHelper.Excel.Cell.NumberFormats.Integer)
            .AddColumn(oCcx.Lang.Tradueix("fecha", "data", "date"), MatHelper.Excel.Cell.NumberFormats.DDMMYY)
            If displayStore Then .AddColumn(oCcx.Lang.Tradueix("centro", "centre", "store"))
            .AddColumn(oCcx.Lang.Tradueix("factura", "factura", "invoice"))
            .AddColumn(oCcx.Lang.Tradueix("pedido", "comanda", "order"))
            .AddColumn(oCcx.Lang.Tradueix("marca", "marca", "brand"))
            .AddColumn(oCcx.Lang.Tradueix("categoria", "categoria", "category"))
            .AddColumn(oCcx.Lang.Tradueix("producto", "producte", "product"))
            .AddColumn(oCcx.Lang.Tradueix("ref M+O", "ref M+O", "ref M+O"))
            .AddColumn(oCcx.Lang.Tradueix("ref fabricante", "ref fabricant", "ref manufacturer"))
            .AddColumn(oCcx.Lang.Tradueix("Ean", "Ean", "Ean"))
            .AddColumn(oCcx.Lang.Tradueix("cantidad", "quantitat", "units"), MatHelper.Excel.Cell.NumberFormats.Integer)
            .AddColumn(oCcx.Lang.Tradueix("precio", "preu", "price"), MatHelper.Excel.Cell.NumberFormats.Euro)
            If displayDto Then .AddColumn(oCcx.Lang.Tradueix("descuento", "descompte", "discount"), MatHelper.Excel.Cell.NumberFormats.Percent)
        End With

        For Each odelivery In oDeliveries
            For Each item In odelivery.Items
                Dim oRow = retval.AddRow()
                With odelivery
                    oRow.AddCell(.Id, UrlHelper.Doc(odelivery))
                    oRow.AddCell(.Fch)
                    If displayStore Then oRow.AddCell(.Customer.Ref)
                    If .Invoice Is Nothing Then
                        oRow.AddCell()
                    Else
                        oRow.AddCell(.Invoice.NumeroYSerie, UrlHelper.Doc(odelivery.Invoice))
                    End If
                End With
                With item
                    oRow.AddCell(.PurchaseOrderItem.PurchaseOrder.Concept, UrlHelper.Doc(.PurchaseOrderItem.PurchaseOrder))
                    oRow.AddCell(.Sku.Category.Brand.Nom.Esp)
                    oRow.AddCell(.Sku.Category.Nom.Esp)
                    oRow.AddCell(.Sku.Nom.Esp)
                    oRow.AddCell(.Sku.Id, UrlHelper.LandingPage(.Sku, oCcx.Lang, AbsoluteUrl:=True))
                    oRow.AddCell(.Sku.RefProveidor)
                    oRow.AddCell(.Sku.Ean13.Value)
                    oRow.AddCell(.Qty)
                    oRow.AddCellAmt(.Price)
                    If displayDto Then oRow.AddCell(.Dto)
                End With
            Next
        Next
        Return retval
    End Function
End Class
