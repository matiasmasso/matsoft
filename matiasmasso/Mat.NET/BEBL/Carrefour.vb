Imports DTO.Integracions

Public Class Carrefour

    Shared Function LogisticItems(oDeliveries As List(Of DTODelivery)) As List(Of DTOCarrefourItem)
        Dim retval As New List(Of DTOCarrefourItem)
        Dim oCountries As List(Of DTOCountry) = CountriesLoader.All(DTOLang.ENG)

        Dim exs As New List(Of Exception)
        For Each oDelivery As DTODelivery In oDeliveries
            DeliveryLoader.Load(oDelivery)
            BEBL.Delivery.LoadCustSkuRefs(oDelivery, exs)
            BEBL.Delivery.LoadMadeIns(oDelivery, exs)

            Dim idx As Integer = 0
            For Each line As DTODeliveryItem In oDelivery.Items
                idx += 1
                Dim QtyPerBulto As Integer = line.Sku.outerPackOrInherited
                Dim Bultos As Integer = line.Qty / QtyPerBulto
                For i As Integer = 1 To Bultos
                    Dim item As New DTOCarrefourItem
                    With item
                        .Albaran = oDelivery.Formatted()
                        .Linea = idx
                        .SkuCustomRef = Ref(line.Sku)
                        '.Implantation = "610087"
                        .MasterBarCode = DUN14(line.Sku)
                        .Height = line.Sku.dimensionHOrInherited
                        .Width = line.Sku.dimensionWOrInherited
                        .Length = line.Sku.dimensionLOrInherited
                        .SkuColor = Color(line.Sku)
                        .SkuDsc = Dsc(line.Sku)
                        .UnitsPerInnerBox = line.Sku.innerPackOrInherited
                        .UnitsPerMasterBox = QtyPerBulto
                        .Weight = line.Sku.weightKgOrInherited
                        .MadeIn = MadeIn(line.Sku, oCountries)
                    End With
                    retval.Add(item)
                Next
            Next
        Next


        Return retval
    End Function

    Private Shared Function Ref(oSku As DTOProductSku) As String
        Dim retval As String = ""
        If oSku.CustomerProduct IsNot Nothing Then
            retval = oSku.CustomerProduct.ref
        End If
        Return retval
    End Function

    Private Shared Function DUN14(oSku As DTOProductSku) As String
        Dim retval As String = ""
        If oSku.CustomerProduct IsNot Nothing Then
            retval = oSku.CustomerProduct.DUN14
        End If
        Return retval
    End Function
    Private Shared Function Dsc(oSku As DTOProductSku) As String
        Dim retval As String = oSku.NomLlarg.Esp
        If oSku.CustomerProduct IsNot Nothing Then
            retval = oSku.CustomerProduct.dsc
        End If
        Return retval
    End Function
    Private Shared Function Color(oSku As DTOProductSku) As String
        Dim retval As String = oSku.Nom.Esp
        If oSku.CustomerProduct IsNot Nothing Then
            retval = oSku.CustomerProduct.color
        End If
        Return retval
    End Function

    Private Shared Function MadeIn(oSku As DTOProductSku, oCountries As List(Of DTOCountry)) As String
        Dim retval As String = ""
        Dim oCountry As DTOCountry = oSku.MadeIn
        If oCountry IsNot Nothing Then
            oCountry = oCountries.FirstOrDefault(Function(x) x.Equals(oCountry))
            If oCountry IsNot Nothing Then
                retval = oCountry.LangNom.Tradueix(DTOLang.ENG)
            End If
        End If
        Return retval
    End Function


    Shared Function ExcelStocks(oMgz As DTOMgz) As MatHelper.Excel.Sheet
        Dim oCarrefour As DTOCustomer = DTOCustomer.Wellknown(DTOCustomer.Wellknowns.carrefour)
        Dim items = ProductStocksLoader.Custom(oCarrefour, oMgz)
        Dim sTitle As String = "M+O Stocks " & Format(Now, "yyyy.MM.dd.hh.mm.ss")
        Dim retval As New MatHelper.Excel.Sheet(sTitle, sTitle)
        With retval
            .AddColumn("EAN", MatHelper.Excel.Cell.NumberFormats.Integer)
            .AddColumn("Ref", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("Descripción", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("Color", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("Stock", MatHelper.Excel.Cell.NumberFormats.Integer)
        End With

        For Each item In items
            Dim oRow = retval.AddRow
            oRow.AddCell(DTOProductSku.Ean(item.sku))
            oRow.AddCell(item.ref)
            oRow.AddCell(item.dsc)
            oRow.AddCell(item.color)
            oRow.AddCell(item.sku.stockAvailable())
        Next

        Return retval
    End Function

    Shared Function DeliveryDoc(oDelivery As DTODelivery) As DTODoc
        Dim oOrder As New DTOPurchaseOrder
        Dim oSpv As New DTOSpv
        Dim itm As DTODeliveryItem = Nothing


        Dim oEstilo As DTODoc.Estilos = DTODoc.Estilos.albara

        'If mAmt Is Nothing Then mAmt = DTOAmt.Empty
        Dim sAdr As String = Nothing
        Dim oZip As DTOZip = Nothing
        Dim oLang = oDelivery.Contact.Lang
        Dim oDoc As New DTODoc(oEstilo, oLang, DTOApp.Current.Cur)
        With oDoc
            .valorat = oDelivery.Valorado
            .incoterm = oDelivery.Incoterm()

            Dim exs As New List(Of Exception)
            BEBL.Delivery.LoadCustSkuRefs(oDelivery, exs)
            .displayTotalLogistic = True

            Dim oCcx As DTOCustomer = BEBL.Customer.CcxOrMe(oDelivery.Customer)
            .recarrecEquivalencia = oCcx.Req
            .dest.Add(oCcx.Nom)
            .dest.Add(String.Format("NIF: {0}", oCcx.PrimaryNifValue()))
            sAdr = oCcx.Address.Text
            oZip = oCcx.Address.Zip


            If Not String.IsNullOrEmpty(sAdr) Then
                For Each sl As String In sAdr.Split(vbCrLf)
                    If sl > "" Then .dest.Add(sl.Trim)
                Next
            End If

            If oZip IsNot Nothing Then
                .dest.Add(oZip.FullNom(oDelivery.Customer.Lang))
            End If

            .fch = oDelivery.Fch
            .num = oDelivery.Id
            '.Dto = oDelivery.DtoPct
            '.Dpp = oDelivery.DppPct

            If oDelivery.IvaExempt Then
                .ivaBaseQuotas = New List(Of DTOTaxBaseQuota)
            Else
                .ivaBaseQuotas = oDelivery.taxBaseQuotas()
            End If

            Dim s As String = .lang.Tradueix("forma de envío: ", "enviament: ", "shipment: ")
            Select Case oDelivery.PortsCod
                Case DTOCustomer.PortsCodes.reculliran
                    .obs.Add(s & .lang.Tradueix("recogerán en ", "recullirán en ", "to be collected on "))
                    Dim oMgz As DTOMgz = oDelivery.Mgz
                    ContactLoader.Load(oMgz)
                    .obs.Add(oMgz.nomComercialOrDefault())
                    .obs.Add(oMgz.Address.Text)
                    .obs.Add(oMgz.Address.Zip.Location.FullNom(oLang))
                Case DTOCustomer.PortsCodes.deguts
                    .obs.Add(s & .lang.Tradueix("portes debidos", "ports deguts", "portes debidos"))
                Case DTOCustomer.PortsCodes.pagats
                    .obs.Add(s & .lang.Tradueix("portes pagados", "ports pagats", "paid ports"))
                    If oDelivery.Platform IsNot Nothing Then
                        ContactLoader.Load(oDelivery.Platform)
                        .obs.Add("entrega en " & oDelivery.Platform.Nom)
                        .obs.Add(oDelivery.Platform.Address.Text)
                        .obs.Add(oDelivery.Platform.Address.Zip.FullNom(oLang))
                    End If
            End Select

            If oDelivery.Items IsNot Nothing Then
                For Each itm In oDelivery.Items

                    Select Case oDelivery.Cod
                        Case DTOPurchaseOrder.Codis.reparacio
                            If Not oSpv.Equals(itm.Spv) Then
                                Dim i As Integer
                                oSpv = itm.Spv
                                Dim oSpvTextArray As List(Of String) = oSpv.lines(oDelivery.Customer.Lang) ' oSpv.TextArray
                                Dim iSpvTextLines As Integer = oSpvTextArray.Count
                                .itms.Add(New DTODocItm(, , , , , , , iSpvTextLines))
                                For i = 0 To iSpvTextLines - 1
                                    .itms.Add(New DTODocItm(oSpvTextArray(i), DTODoc.FontStyles.italic, , , , , , iSpvTextLines - i + 2))
                                Next
                            End If
                        Case DTOPurchaseOrder.Codis.client
                            If itm.PurchaseOrderItem.PurchaseOrder.UnEquals(oOrder) Then
                                oOrder = itm.PurchaseOrderItem.PurchaseOrder
                                .itms.Add(New DTODocItm(IntMinLinesBeforeEndPage:=6))
                                .itms.Add(New DTODocItm(BEBL.Delivery.DocPdcText(oDelivery, oOrder), DTODoc.FontStyles.bold, IntMinLinesBeforeEndPage:=5))
                                .itms.Add(New DTODocItm(IntMinLinesBeforeEndPage:=4))
                            End If
                    End Select

                    Dim oSku As DTOProductSku = itm.Sku
                    Dim sRef As String = oSku.Id.ToString
                    Dim sSkuNom As String = oSku.NomLlarg.Esp
                    If oDelivery.Cod = DTOPurchaseOrder.Codis.client AndAlso oDelivery.Customer.Lang.Equals(DTOLang.ENG) Then
                        sSkuNom = oSku.refYNomPrv()
                    End If

                    If (oDelivery.Valorado) Then
                        .itms.Add(New DTODocItm(sSkuNom, DTODoc.FontStyles.regular, itm.Qty, itm.Price, itm.Dto, 0, 2, , sRef))
                    Else
                        .itms.Add(New DTODocItm(sSkuNom, DTODoc.FontStyles.regular, itm.Qty, , , 0, 2, , sRef))
                    End If

                    Dim BoxUnits As Integer = oSku.innerPackOrInherited
                    Dim Boxes As Integer = itm.Qty / BoxUnits
                    Dim oDocitm As DTODocItm = .itms.Last
                    With oDocitm
                        .Boxes = Boxes
                        .m3 = itm.Qty * oSku.volumeM3OrInherited
                        .Kg = itm.Qty * oSku.weightKgOrInherited
                    End With
                    .itms.Add(New DTODocItm(String.Format("Dun 14: {0}", DTOCustomerProduct.Dun14OrDefault(oSku.CustomerProduct)), IntLeftPadChars:=8))
                    .itms.Add(New DTODocItm(String.Format("unidades/caja: {0}", BoxUnits), IntLeftPadChars:=8))
                    .itms.Add(New DTODocItm(String.Format("total cajas: {0}", Boxes), IntLeftPadChars:=8))
                    .itms.Add(New DTODocItm(String.Format("volumen caja: {0} m3", oSku.volumeM3OrInherited), IntLeftPadChars:=8))
                    .itms.Add(New DTODocItm(String.Format("peso bruto caja: {0} Kg", oSku.weightKgOrInherited), IntLeftPadChars:=8))
                    .itms.Add(New DTODocItm(IntMinLinesBeforeEndPage:=2))
                Next

            End If
        End With
        Return oDoc
    End Function
End Class

